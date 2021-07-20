/****************通道切换IO控制*******************
 * 功能: OCV通道切换功能,默认前提:探针号与切换通道号是一一对应
 * 
 * 160323:
 * 1.常规32通道切换板和24通道切换板的切换功能
 * 2.Agilent 34401A读数
 * 3.批量和单通道读数
 *  
 * *************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;
using OCV.OCVTest;
using System.Threading.Tasks;

namespace OCV
{
    //OCV测试
    public class ClsOCVIRTest
    {
        FrmOCV mForm;

        public ClsIOControl SwitchDev;          //切换控制
        public ClsDMM_Ag344X mDmm;              //万用表
        public ClsHIOKI365X mRTester;           //内阻仪 

        Thread ThreadTestAction;

        private bool mStopManual;
        private bool mManualTestFinish;
        public bool ManualTestFinish { get { return mManualTestFinish; } }

        private bool mStop;         //停止
        private bool mTestFinish;   //单次测量完成标志
        public bool TestFinish { get { return mTestFinish; } set { mTestFinish = value; } }

        //测试参数
        int ShowHalfVal = ClsGlobal.TrayType / 2;                           //界面更新用
        int mStartBattNum;                                                  //开始测量的电池通道号,随当前测试位而变动                
        int CHCount = ClsGlobal.ProbeBoardType;                             //测试通道数 

        public void StopAction()
        {
            mStop = true;
        }

        public ClsOCVIRTest(ClsIOControl Contr, SerialPort RTester_SP, int RT_Speed, string DMM_OCV, FrmOCV f1)
        {
            try
            {
                //内阻仪
                mRTester = new ClsHIOKI365X(RTester_SP);
                if (RT_Speed == 1 || RT_Speed == 2)
                {
                    mRTester.InitControl_IMM(RT_Speed, 2);
                }
                else
                {
                    mRTester.InitControl_IMM(1, 2);
                }
                mRTester.ClearData();

                //万用表
                mDmm = new ClsDMM_Ag344X(DMM_OCV);
                mDmm.InitControl_IMM();


                //IO控制
                SwitchDev = Contr;
                mForm = f1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClsOCVIRTest(ClsIOControl Contr, string DMM_OCV, FrmOCV f1)
        {
            try
            {
                //万用表
                mDmm = new ClsDMM_Ag344X(DMM_OCV);
                mDmm.InitControl_IMM();

                //IO控制
                SwitchDev = Contr;
                mForm = f1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //测试流程
        public void StartTestAction()
        {
            ClsGlobal.IsTestRuning = true;
            ClsGlobal.IsStartTest = true;
            try
            {

                mStop = false;
                mTestFinish = false;

                if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest || ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                {
                    if (ClsProcessSet.WorkingProcess.Type == 0)
                    {
                        ThreadTestAction = new Thread(new ThreadStart(TestOCVACIR));    //OCV IR测试
                    }
                    else if (ClsProcessSet.WorkingProcess.Type == 1)
                    {
                        ThreadTestAction = new Thread(new ThreadStart(TestOCV));        //OCV测试
                    }
                }
                else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                {
                    ThreadTestAction = new Thread(new ThreadStart(AdjustACIR));     //校准                 
                }
                ThreadTestAction.Start();
            }
            catch (Exception ex)
            {
                ClsGlobal.IsTestRuning = false;
                throw ex;
            }

        }

        //手动测试流程
        public void StartManualTestAction()
        {
            if (ClsProcessSet.WorkingProcess.Type == 0)
            {

                ThreadTestAction = new Thread(new ThreadStart(ManualTestOCVACIR));
            }
            else if (ClsProcessSet.WorkingProcess.Type == 1)
            {
                ThreadTestAction = new Thread(new ThreadStart(ManualTestVolt));
            }
            ThreadTestAction.Start();
        }


        //手动测试电压
        private void ManualTestVolt()
        {
            double theDMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            int theCHCount = ClsGlobal.ProbeBoardType;

            //将1~x通道分为A区或B区两部分
            //先测完A区的通道, 再测试B区通道
            List<int> lstRegionA = new List<int>();
            List<int> lstRegionB = new List<int>();

            try
            {
                mManualTestFinish = false;
                mStopManual = false;

                #region 测电压
                #region 20200702 zxz 按照实际通道切换测试，测试完成后数据按照客户定义的通道顺序排序
                int err = 0;
                double[] rV = new double[ClsGlobal.TrayType];
                for (int i = 1; i <= theCHCount; i++)
                {
                    if (mStopManual == true)
                    {
                        mManualTestFinish = true;
                        theCHCount = i;
                        break;
                    }

                    SwitchDev.ChanndlVoltSwitchContr(lstRegionB[i], out err);

                    Thread.Sleep(ClsGlobal.DelayTime);

                    if (ClsGlobal.EN_TestOCV == 1)
                    {
                        mDmm.ReadVolt(out theDMMVolt);
                        rV[i - 1] = theDMMVolt;
                    }
                }
                SwitchDev.ChannelVoltIRSwitchContr(1, 2, 0);     //结束,通道全部关断

                for (int i = 0; i < theCHCount; i++)
                {
                    foreach (ChannelItem itm in ClsGlobal.ChannelMapping)
                    {
                        if (itm.RealChannel == i + 1)
                        {
                            arrDMMVolt[itm.Channel - 1] = rV[i - 1];
                        }
                    }
                }
                #endregion

                //数值显示
                if (mForm.IsHandleCreated == true)
                {
                    mForm.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < theCHCount; i++)
                        {
                            arrDMMVolt[i] = arrDMMVolt[i] * 1000;
                            mForm.dgvManualTest.Rows[i].Cells[3].Value = arrDMMVolt[i].ToString("F1");
                        }
                        mForm.dgvManualTest.Refresh();
                    }));
                }

                #endregion
                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }
        }


        //手动测试电压内阻
        private void ManualTestOCVACIR()
        {
            double theIRVolt = 0;
            double theIRAcir = 0;
            double[] arrIRVolt = new double[ClsGlobal.TrayType];
            double[] arrIRAcir = new double[ClsGlobal.TrayType];
            double[] rV = new double[ClsGlobal.TrayType];
            double[] rA = new double[ClsGlobal.TrayType];

            int theCHCount = ClsGlobal.ProbeBoardType;

            //将1~x通道分为A区或B区两部分
            //先测完A区的通道, 再测试B区通道
            List<int> lstRegionA = new List<int>();
            List<int> lstRegionB = new List<int>();

            try
            {
                mManualTestFinish = false;
                mStopManual = false;

                #region 测内阻电压

                //如果没有短接正极电阻, 需要继电器接入
                //SwitchDev.PosProbeSwitchContr(1, 1);
                #region 20200702 zxz old

                for (int i = 1; i <= theCHCount; i++)
                {
                    if (((i - 1) / 16) % 2 == 0)
                    {
                        lstRegionA.Add(i);
                    }
                    else
                    {
                        lstRegionB.Add(i);
                    }
                }

                //测试方式1：A区电压，B区内阻
                for (int i = 0; i < lstRegionA.Count; i++)
                {
                    if (mStopManual == true)
                    {
                        mManualTestFinish = true;
                        theCHCount = lstRegionA[i];
                        break;
                    }
                    SwitchDev.ChannelVoltIRSwitchContr(2, 1, lstRegionA[i]);

                    Thread.Sleep(ClsGlobal.DelayTime);
                    mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                    rV[lstRegionA[i] - 1] = theIRVolt;
                    rA[lstRegionB[i] - 1] = theIRAcir; //wjp,2020/7/9
                }


                //测试方式2：A区内阻，B区电压
                for (int i = 0; i < lstRegionA.Count; i++)
                {
                    if (mStopManual == true)
                    {
                        mManualTestFinish = true;
                        theCHCount = lstRegionB[i];
                        break;
                    }
                    SwitchDev.ChannelVoltIRSwitchContr(2, 2, lstRegionA[i]);
                    Thread.Sleep(ClsGlobal.DelayTime);
                    mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                    rA[lstRegionA[i] - 1] = theIRAcir;
                    rV[lstRegionB[i] - 1] = theIRVolt;//wjp,2020/7/9
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0);
                #endregion
                #region 20200702 zxz 通道映射
                for (int i = 0; i < theCHCount; i++)
                {
                    foreach (ChannelItem itm in ClsGlobal.ChannelMapping)
                    {
                        if (itm.RealChannel == i + 1)
                        {
                            arrIRVolt[itm.Channel - 1] = rV[i];
                            arrIRAcir[itm.Channel - 1] = rA[i];
                        }
                    }
                }
                #endregion


                //数值显示
                if (mForm.IsHandleCreated == true)
                {
                    mForm.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < theCHCount; i++)
                        {
                            arrIRVolt[i] = arrIRVolt[i] * 1000;
                            mForm.dgvManualTest.Rows[i].Cells[3].Value = arrIRVolt[i].ToString("F1");
                            arrIRAcir[i] = arrIRAcir[i] * 1000;
                            mForm.dgvManualTest.Rows[i].Cells[4].Value = arrIRAcir[i].ToString("F2");
                        }
                        mForm.dgvManualTest.Refresh();
                    }));
                }
                #endregion


                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }
        }


        //手动测试电压内阻
        //按顺序切换方式(否定该方法)
        private void ManualTestOCVACIR_InOrder()
        {
            double theIRVolt = 0;
            double theIRAcir = 0;
            double[] arrIRVolt = new double[ClsGlobal.TrayType];
            double[] arrIRAcir = new double[ClsGlobal.TrayType];
            int theCHCount = ClsGlobal.ProbeBoardType;

            try
            {
                mManualTestFinish = false;
                mStopManual = false;

                #region 测电压内阻

                //通道切换
                int iStart = 1;

                //如果没有短接正极电阻, 需要继电器接入
                //SwitchDev.PosProbeSwitchContr(1, 1);

                Thread.Sleep(2);
                for (int i = 0; i < theCHCount; i++)
                {
                    if (mStopManual == true)
                    {
                        mManualTestFinish = true;
                        theCHCount = i;
                        break;
                    }

                    SwitchDev.ChannelVoltIRSwitchContr(1, 2, i + iStart);

                    Thread.Sleep(ClsGlobal.DelayTime);

                    if (ClsGlobal.EN_TestACIR == 1)
                    {
                        mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                    }
                    arrIRVolt[i] = theIRVolt;
                    arrIRAcir[i] = theIRAcir;
                }

                SwitchDev.ChannelVoltIRSwitchContr(1, 2, 0);     //结束,通道全部关断

                //数值显示
                if (mForm.IsHandleCreated == true)
                {
                    mForm.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < theCHCount; i++)
                        {
                            arrIRVolt[i] = arrIRVolt[i] * 1000;
                            mForm.dgvManualTest.Rows[i].Cells[3].Value = arrIRVolt[i].ToString("F1");
                            arrIRAcir[i] = arrIRAcir[i] * 1000;
                            mForm.dgvManualTest.Rows[i].Cells[4].Value = arrIRAcir[i].ToString("F2");
                        }
                        mForm.dgvManualTest.Refresh();
                    }));
                }

                #endregion
                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }
        }

        //查询电池ACIR NG状态
        private string CheckOCVTestNG(double volVal)
        {

            if (volVal > ClsProcessSet.WorkingProcess.MaxV)
            {
                return "NG-" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1) + "-Vu";
            }
            else if (volVal < ClsProcessSet.WorkingProcess.MinV)
            {
                return "NG-" + (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1) + "-Vd";
            }
            else
            {
                return "";
            }

        }

        // NG状态代码输出
        private string GetNGCode(double volVal, double acirVal)
        {
            string Res = "";

            if (volVal > ClsProcessSet.WorkingProcess.MaxV)
            {
                Res += (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1) + "-Vu";
            }
            else if (volVal < ClsProcessSet.WorkingProcess.MinV)
            {
                Res += (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1) + "-Vd";
            }

            if (acirVal > ClsProcessSet.WorkingProcess.MaxIR)
            {
                Res += (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1) + "-Ru";
            }
            else if (acirVal < ClsProcessSet.WorkingProcess.MinIR)
            {
                Res += (ClsProcessSet.WorkingProcess.Type + 1 + ClsGlobal.OCVType_Sub1) + "-Rd";
            }

            if (Res.Count() > 0)
            {
                Res = "NG-" + Res;
            }

            return Res;
        }



        //自动测试电压
        private void TestOCV()
        {

            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            int theCHCount = ClsGlobal.ProbeBoardType;
            double theDMMVolt = 0;
            //将1~x通道分为A区或B区两部分
            //先测完A区的通道, 再测试B区通道
            List<int> lstRegionA = new List<int>();
            List<int> lstRegionB = new List<int>();

            DateTime StartTime;
            DateTime EndTime;

            try
            {
                SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0);
                Thread.Sleep(500);
                SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);
                //SwitchDev.ChanndlVoltSwitchContr(0, out int cc);
                mTestFinish = false;
                mStop = false;

                #region 测电压

                //只测试OCV的情况, 目前不进行重复测试

                //测试开始
                StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //仪表初始化
                //mRTester.InitControl_IMM(2, 1);
                mDmm.InitControl_IMM();

                Thread.Sleep(200);

                //测量OCV ACIR

                //(否定)如果没有焊正极短路电阻
                //SwitchDev.PosProbeSwitchContr(xxx);
                #region 20200702 zxz old
                //lstRegionA.Clear();
                //lstRegionB.Clear();

                //for (int i = 1; i <= theCHCount; i++)
                //{
                //    if (((i - 1) / 32) % 2 == 0)
                //    {
                //        lstRegionA.Add(i);
                //    }
                //    else
                //    {
                //        lstRegionB.Add(i);
                //    }
                //}

                //int err = 0;
                //    //A区测试
                //    for (int i = 0; i < lstRegionA.Count; i++)
                //    {
                //        if (ClsGlobal.Trans_State != eTransState.TestWork)
                //        {
                //            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                //            throw new Exception();
                //        }

                //        //SwitchDev.ChannelVoltIRSwitchContr(1, 2, lstRegionA[i]);
                //        SwitchDev.ChanndlVoltSwitchContr(lstRegionA[i],out err);        //电压类型切换

                //        Thread.Sleep(ClsGlobal.DelayTime);

                //        if (ClsGlobal.EN_TestACIR == 1)
                //        {
                //            mDmm.ReadVolt(out theDMMVolt);
                //            arrDMMVolt[lstRegionA[i] - 1] = theDMMVolt;
                //        }
                //    }

                //    //B区测试
                //    for (int i = 0; i < lstRegionB.Count; i++)
                //    {
                //        if (ClsGlobal.Trans_State != eTransState.TestWork)
                //        {
                //            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                //            throw new Exception();
                //        }

                //        //SwitchDev.ChannelVoltIRSwitchContr(1, 2, lstRegionB[i]);
                //        SwitchDev.ChanndlVoltSwitchContr(lstRegionB[i],out err);        //电压类型切换
                //        Thread.Sleep(ClsGlobal.DelayTime);

                //        if (ClsGlobal.EN_TestACIR == 1)
                //        {
                //            mDmm.ReadVolt(out theDMMVolt);
                //            arrDMMVolt[lstRegionB[i] - 1] = theDMMVolt;
                //        }
                //    }
                #endregion
                #region 20200702 zxz 通道映射
                int err = 0;
                double[] rV = new double[ClsGlobal.TrayType];
                for (int i = 1; i <= theCHCount; i++)
                {
                    var flag1 = ChanleMapingSetting.DicDevIndexMaping[i].BatCode.StartsWith("000");

                    if (flag1)
                    {

                        rV[i - 1] = 0;

                        continue;
                    }
                    try
                    {
                        if (ClsGlobal.Trans_State != eTransState.TestWork)
                        {
                            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                            throw new Exception();
                        }
                        //SwitchDev.ChanndlVoltSwitchContr(i, out err);        //电压类型切换
                        SwitchDev.ChannelVoltIRSwitchContr(2, 1, i);

                        Thread.Sleep(ClsGlobal.DelayTime);
                        if (ClsGlobal.EN_TestACIR == 1)
                        {
                            mDmm.ReadVolt(out theDMMVolt);
                            rV[i - 1] = theDMMVolt;
                            //arrDMMVolt[lstRegionA[i] - 1] = theDMMVolt;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                int ngCount = 0;
                #endregion
                if (ClsSysSetting.SysSetting.IsNGCheck)
                {
                    #region 复测判断
                    List<int> lstNGChanle = new List<int>();
                    List<string> ngBatCode = new List<string>();
                    int ngCheckCount = ClsSysSetting.SysSetting.NGCheckCount;

                    //ng复测次数大于0时执行复测
                    for (int ngc = 0; ngc < ngCheckCount; ngc++)
                    {
                        for (int i = 1; i < theCHCount; i++)
                        {
                            if (CheckOCVTestNG(rV[i - 1] * 1000).Contains("NG") && !ChanleMapingSetting.DicDevIndexMaping[i].BatCode.StartsWith("000"))
                            {
                                lstNGChanle.Add(i);
                                ngCount++;
                            }
                        }
                        if (ngCount > 0)
                        {
                            for (int i = 1; i <= theCHCount; i++)
                            {
                                if (CheckOCVTestNG(rV[i - 1] * 1000).Contains("NG") && !ChanleMapingSetting.DicDevIndexMaping[i].BatCode.StartsWith("000"))
                                {
                                    ChannelItem map = ClsGlobal.ChannelMapping.Find(a => a.RealChannel == i);
                                    if (map != null)
                                    {
                                        ngBatCode.Add(ClsGlobal.listETCELL[map.Channel - 1].Cell_ID);
                                    }
                                }

                            }
                            //弹出对话框,询问复测
                            bool askResult = true;
                            if (ngc >= 1)
                            {
                                Func<List<string>, bool> func = (batCodes) => { return mForm.AskNGCheck(batCodes); };
                                askResult = (bool)mForm.Invoke(func, ngBatCode);
                            }
                            if (askResult)
                            {
                                //对NG通道进行复测
                                foreach (var c in lstNGChanle)
                                {
                                    try
                                    {
                                        if (ClsGlobal.Trans_State != eTransState.TestWork)
                                        {
                                            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                                            throw new Exception();
                                        }
                                        //SwitchDev.ChanndlVoltSwitchContr(c, out err);        //电压类型切换
                                        SwitchDev.ChannelVoltIRSwitchContr(2, 1, c);
                                        Thread.Sleep(ClsGlobal.DelayTime);
                                        mDmm.ReadVolt(out theDMMVolt);
                                        rV[c - 1] = theDMMVolt;

                                    }
                                    catch (Exception ex)
                                    {
                                        var ms = ex.Message;
                                    }
                                }
                                ngCount = 0;
                                lstNGChanle.Clear();
                                ngBatCode.Clear();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    #endregion
                }
                //结束,通道全部关断
                SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);
                //SwitchDev.ChanndlVoltSwitchContr(0, out err);
                EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                for (int i = 1; i <= theCHCount; i++)
                {
                    arrDMMVolt[ChanleMapingSetting.DicDevIndexMaping[i].TrayCodeChanle - 1] = rV[i - 1];
                }
                ClsTempControl clsTemp = ClsGlobal.BuildClsTempControl();
                //数据载入 
                for (int i = 0; i < theCHCount; i++)
                {
                    ClsGlobal.G_dbl_VDataArr[i] = arrDMMVolt[i] * 1000;
                    ClsGlobal.listETCELL[i].OCV_V1 = ClsGlobal.G_dbl_VDataArr[i];
                    ClsGlobal.listETCELL[i].OCV_V2 = 0;
                    ClsGlobal.listETCELL[i].StartTime = StartTime;
                    ClsGlobal.listETCELL[i].EndTime = EndTime;
                    ClsGlobal.listETCELL[i].TMP = clsTemp.GetAvgNowTem();
                }
                #endregion

                #region NG判断
                ngCount = 0;
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    ClsGlobal.listETCELL[i].CODE = CheckOCVTestNG(ClsGlobal.G_dbl_VDataArr[i]);  //NG代码
                    if (ClsGlobal.listETCELL[i].Cell_ID != "" && ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") == false)
                    {
                        if (ClsGlobal.listETCELL[i].CODE.Contains("NG"))
                        {
                            ngCount++;
                        }
                    }
                }
                #endregion


                #region  界面更新

                if (mForm.IsHandleCreated == true)
                {
                    mForm.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < ClsGlobal.TrayType; i++)
                        {
                            ChanleMapingSetting.ListBatTestData[i].ACIR = Math.Round(0.0, 2);
                            ChanleMapingSetting.ListBatTestData[i].OCV = Math.Round(ClsGlobal.G_dbl_VDataArr[i], 6);
                            ChanleMapingSetting.ListBatTestData[i].CODE = ClsGlobal.listETCELL[i].CODE;
                        }

                        mForm.dgvTestData1.DataSource = null;
                        mForm.dgvTestData1.DataSource = ChanleMapingSetting.ListBatTestData.Where(a => a.TrayCodeChanle < 256 / 2).ToList();
                        mForm.dgvTestData2.DataSource = null;
                        mForm.dgvTestData2.DataSource = ChanleMapingSetting.ListBatTestData.Where(a => a.TrayCodeChanle >= 256 / 2).ToList();
                    }));
                }
                #endregion


                ClsGlobal.OCV_TestState = eTestState.TestOK;
                ClsGlobal.TestCount = 0;

                //  mTestFinish = true;     //该次测试完成 改由复测处处理该值

            }
            catch (Exception ex)
            {
                string msg = "";
                //测试异常   
                if (ClsGlobal.OCV_TestState == eTestState.ErrTransChange)
                {
                    msg = "测试停止!";
                }
                else
                {
                    ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                    msg = ex.Message;
                }
                mForm.Invoke(new EventHandler(delegate
                {
                    mForm.txtLog.Text = System.DateTime.Now.ToString() + ":" + msg + "\r\n" + mForm.txtLog.Text;

                }));
            }
            ClsGlobal.IsTestRuning = false;
        }


        //自动测试电池内阻电压
        private void TestOCVACIR()
        {
            double theIRVolt = 0;
            double theIRAcir = 0;
            double[] arrIRVolt = new double[ClsGlobal.TrayType];
            double[] arrIRAcir = new double[ClsGlobal.TrayType];
            double[] rV = new double[ClsGlobal.TrayType];
            double[] rA = new double[ClsGlobal.TrayType];
            int theCHCount = ClsGlobal.ProbeBoardType;

            //将1~x通道分为A区或B区两部分
            //先测完A区的通道, 再测试B区通道
            List<int> lstRegionA = new List<int>();
            List<int> lstRegionB = new List<int>();

            DateTime StartTime;
            DateTime EndTime;

            int pos = 0;
            try
            {
                mTestFinish = false;
                mStop = false;

                #region 测电压内阻

                //首次测量,全盘测,再次测量,只测有ID的问题通道

                //测试开始
                StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //ClsGlobal.WriteLog("[OCV+InR]开始时间"+StartTime, ClsGlobal.sDebugOCVSelectionPath);

                //仪表初始化
                mRTester.InitControl_IMM(ClsGlobal.RT_Speed, 2);
                Thread.Sleep(200);

                //ClsGlobal.WriteLog("[OCV+InR]仪表初始化", ClsGlobal.sDebugOCVSelectionPath);
                //测量OCV ACIR

                //(否定)如果没有焊正极短路电阻
                //SwitchDev.PosProbeSwitchContr(xxx);
                #region zxz 20200706 分段测试
                lstRegionA.Clear();
                lstRegionB.Clear();
                for (int i = 1; i <= theCHCount; i++)
                {
                    if (((i - 1) / 16) % 2 == 0)
                    {
                        lstRegionA.Add(i);
                    }
                    else
                    {
                        lstRegionB.Add(i);
                    }
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0);
                Thread.Sleep(1 * 1000);
                SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);
                Thread.Sleep(1 * 1000);
                SwitchDev.ChanndlVoltSwitchContr(0, out int cc);
                #region  //首次测量
                //测试方式1：A区电压，B区内阻
                for (int i = 0; i < lstRegionA.Count; i++)
                {
                    var flag1 = ChanleMapingSetting.DicDevIndexMaping[lstRegionA[i]].BatCode.StartsWith("000");
                    var flag2 = ChanleMapingSetting.DicDevIndexMaping[lstRegionB[i]].BatCode.StartsWith("000");
                    if (flag1 && flag2)
                    {

                        rV[lstRegionA[i] - 1] = 0;
                        rA[lstRegionB[i] - 1] = 0;
                        continue;
                    }
                    try
                    {
                        if (ClsGlobal.Trans_State != eTransState.TestWork)
                        {
                            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                            throw new Exception("111");
                        }

                        #region 采用并发读取的方式优化速度 20200828 由ajone修改
                        CurrentTestStaticData.SWType = 2;
                        CurrentTestStaticData.TestType = 1;
                        CurrentTestStaticData.Channle = lstRegionA[i];

                        ReadTestData();

                        theIRVolt = CurrentTestStaticData.IRVolt;
                        theIRAcir = CurrentTestStaticData.IRAcir;

                        //采用并发读取的方式优化速度 20200828 由ajone屏蔽
                        //SwitchDev.ChannelVoltIRSwitchContr(2, 1, lstRegionA[i]);
                        //Thread.Sleep(ClsGlobal.DelayTime);
                        //mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                        //mDmm.ReadVolt(out theIRVolt);
                        #endregion

                        rV[lstRegionA[i] - 1] = theIRVolt;
                        rA[lstRegionB[i] - 1] = theIRAcir;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);
                //测试方式2：A区内阻，B区电压
                for (int i = 0; i < lstRegionA.Count; i++)
                {
                    var flag1 = ChanleMapingSetting.DicDevIndexMaping[lstRegionA[i]].BatCode.StartsWith("000");
                    var flag2 = ChanleMapingSetting.DicDevIndexMaping[lstRegionB[i]].BatCode.StartsWith("000");
                    if (flag1 && flag2)
                    {

                        rV[lstRegionA[i] - 1] = 0;
                        rA[lstRegionB[i] - 1] = 0;
                        continue;
                    }
                    try
                    {
                        if (ClsGlobal.Trans_State != eTransState.TestWork)
                        {
                            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                            throw new Exception();
                        }
                        CurrentTestStaticData.SWType = 2;
                        CurrentTestStaticData.TestType = 2;
                        CurrentTestStaticData.Channle = lstRegionA[i];

                        ReadTestData();

                        theIRVolt = CurrentTestStaticData.IRVolt;
                        theIRAcir = CurrentTestStaticData.IRAcir;

                        rA[lstRegionA[i] - 1] = theIRAcir;
                        rV[lstRegionB[i] - 1] = theIRVolt;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0);
                #endregion

                for (int i = 0; i < theCHCount; i++)
                {
                    foreach (ChannelItem itm in ClsGlobal.ChannelMapping)
                    {
                        if (itm.RealChannel == i + 1)
                        {
                            arrIRVolt[itm.Channel - 1] = rV[i];
                            arrIRAcir[itm.Channel - 1] = rA[i];
                        }
                    }
                }
                ClsGlobal.lstACIRErrNo.Clear();
                ClsTempControl clsTemp = ClsGlobal.BuildClsTempControl();
                //数据载入 
                for (int i = 0; i < theCHCount; i++)
                {
                    ClsGlobal.G_dbl_VDataArr[i] = arrIRVolt[i] * 1000;
                    if (arrIRAcir[i] < 1000000)
                    {
                        ClsGlobal.G_dbl_ACIRArr[i] = Math.Round(arrIRAcir[i] * 1000 + double.Parse(ClsGlobal.ArrIRAdjustPara[i]), 2);
                    }
                    else
                    {
                        ClsGlobal.G_dbl_ACIRArr[i] = arrIRAcir[i];
                        if (ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") == false)
                        {
                            ClsGlobal.lstACIRErrNo.Add(i + 1);
                            ClsGlobal.ChannelNGRecord[i]++;
                            string msn = "通道" + (i + 1).ToString() + "内阻测试失败!";
                            ClsGlobal.WriteLog(msn);
                        }
                    }
                    ClsGlobal.listETCELL[i].OCV_V1 = ClsGlobal.G_dbl_VDataArr[i];
                    ClsGlobal.listETCELL[i].OCV_V2 = 0;
                    ClsGlobal.listETCELL[i].ACIR = ClsGlobal.G_dbl_ACIRArr[i];
                    ClsGlobal.listETCELL[i].StartTime = StartTime;
                    ClsGlobal.listETCELL[i].TMP = clsTemp.GetAvgNowTem();
                }

                #endregion
                if (ClsSysSetting.SysSetting.IsNGCheck)
                {
                    #region 复测
                    for (int ngc = 0; ngc < ClsSysSetting.SysSetting.NGCheckCount; ngc++)    //再次测量
                    {
                        int ngTestCount = 0;
                        List<int> lstNGChannle = new List<int>();
                        for (int i = 0; i < theCHCount; i++)
                        {
                            if (GetNGCode(ClsGlobal.G_dbl_VDataArr[i], ClsGlobal.G_dbl_ACIRArr[i]).Contains("NG") && !ChanleMapingSetting.DicTrayIndexMaping[i + 1].BatCode.StartsWith("000"))
                            {
                                ngTestCount++;
                                lstNGChannle.Add(i + 1);
                            }
                        }
                        if (ngTestCount > 0)
                        {
                            List<string> ngBatCode = new List<string>();
                            for (int i = 0; i < theCHCount; i++)
                            {
                                if (GetNGCode(ClsGlobal.G_dbl_VDataArr[i], ClsGlobal.G_dbl_ACIRArr[i]).Contains("NG") && !ChanleMapingSetting.DicTrayIndexMaping[i + 1].BatCode.StartsWith("000"))
                                {
                                    ngBatCode.Add(ClsGlobal.listETCELL[i].Cell_ID);
                                }
                            }
                            bool askResult = true;
                            if (ngc >= 2)
                            {
                                Func<List<string>, bool> func = (batCodes) => { return mForm.AskNGCheck(batCodes); };
                                askResult = (bool)mForm.Invoke(func, ngBatCode);
                            }
                            //bool resut = true;
                            if (askResult)
                            {
                                lstRegionA.Clear();
                                lstRegionB.Clear();

                                //只测试有问题的通道
                                #region 复测，只测试有问题的通道，切换为实际通道测试
                                foreach (int ChannelNo in lstNGChannle)
                                {
                                    if (ChannelNo > ClsGlobal.TrayType)
                                    {
                                        break;
                                    }
                                    if (ClsGlobal.Trans_State != eTransState.TestWork)
                                    {
                                        ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                                        throw new Exception();
                                    }
                                    int rCh = 0;
                                    foreach (ChannelItem itm in ClsGlobal.ChannelMapping)
                                    {
                                        if (itm.Channel == ChannelNo)
                                        {
                                            rCh = itm.RealChannel;
                                            break;
                                        }
                                    }
                                    SwitchDev.ChannelVoltIRSwitchContr(2, 2, rCh);
                                    Thread.Sleep(ClsGlobal.DelayTime);
                                    mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                                    double reTestIR = theIRAcir;
                                    SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0);
                                    Thread.Sleep(ClsGlobal.DelayTime);
                                    SwitchDev.ChannelVoltIRSwitchContr(2, 1, rCh);
                                    Thread.Sleep(ClsGlobal.DelayTime);
                                    mDmm.ReadVolt(out theIRVolt);
                                    double reTestV = theIRVolt;
                                    SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);

                                    ClsGlobal.G_dbl_ACIRArr[ChannelNo - 1] = Math.Round((reTestIR * 1000) + double.Parse(ClsGlobal.ArrIRAdjustPara[ChannelNo - 1]),2);
                                    ClsGlobal.G_dbl_VDataArr[ChannelNo - 1] = Math.Round(reTestV * 1000, 2);
                                    ClsGlobal.listETCELL[ChannelNo - 1].OCV_V1 = ClsGlobal.G_dbl_VDataArr[ChannelNo - 1];
                                    ClsGlobal.listETCELL[ChannelNo - 1].OCV_V2 = 0;
                                    ClsGlobal.listETCELL[ChannelNo - 1].ACIR = ClsGlobal.G_dbl_ACIRArr[ChannelNo - 1];
                                    ClsGlobal.listETCELL[ChannelNo - 1].StartTime = StartTime;

                                }

                                #endregion}
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    #endregion
                }

                pos = 99;
                //结束时间
                EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //ClsGlobal.WriteLog("[OCV+InR]结束时间"+EndTime, ClsGlobal.sDebugOCVSelectionPath);

                for (int i = 0; i < theCHCount; i++)
                {
                    ClsGlobal.listETCELL[i].EndTime = EndTime;
                }
                pos = 100;
                #endregion

                #region NG判断
                int ngCount = 0;
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    ClsGlobal.listETCELL[i].CODE = GetNGCode(ClsGlobal.G_dbl_VDataArr[i], ClsGlobal.G_dbl_ACIRArr[i]);  //NG代码
                    if (ClsGlobal.listETCELL[i].Cell_ID != "" && ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") == false)
                    {
                        if (ClsGlobal.listETCELL[i].CODE.Contains("NG"))
                        {
                            ngCount++;
                        }
                    }
                }
                pos = 101;
                #endregion

                #region  界面更新
                //ClsGlobal.WriteLog("[OCV+InR]刷新界面", ClsGlobal.sDebugOCVSelectionPath);
                if (mForm.IsHandleCreated == true)
                {
                    mForm.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < ClsGlobal.TrayType; i++)
                        {
                            ChanleMapingSetting.ListBatTestData[i].ACIR = Math.Round(ClsGlobal.G_dbl_ACIRArr[i], 6);
                            ChanleMapingSetting.ListBatTestData[i].OCV = Math.Round(ClsGlobal.G_dbl_VDataArr[i], 6);
                            ChanleMapingSetting.ListBatTestData[i].CODE = ClsGlobal.listETCELL[i].CODE;
                        }
                        mForm.dgvTestData1.DataSource = null;
                        mForm.dgvTestData1.DataSource = ChanleMapingSetting.ListBatTestData.Where(a => a.TrayCodeChanle < 256 / 2).ToList();
                        mForm.dgvTestData2.DataSource = null;
                        mForm.dgvTestData2.DataSource = ChanleMapingSetting.ListBatTestData.Where(a => a.TrayCodeChanle >= 256 / 2).ToList();

                    }));
                }
                #endregion
                ClsGlobal.OCV_TestState = eTestState.TestOK;
                ClsGlobal.TestCount = 0;
                // mTestFinish = true;     //该次测试完成 改由复测处处理该值

            }
            catch (Exception ex)
            {
                string msg = "";
                //测试异常   
                if (ClsGlobal.OCV_TestState == eTestState.ErrTransChange)
                {
                    msg = $"测试停止:{ex.Message }";

                }
                else
                {
                    ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                    msg = ex.Message + ex.StackTrace;

                }
                ClsGlobal.WriteLog("[OCV+InR]异常：" + ClsGlobal.OCV_TestState + "异常信息" + msg + "pos=" + pos, ClsGlobal.sDebugOCVSelectionPath);

                mForm.Invoke(new EventHandler(delegate
                    {
                        mForm.txtLog.Text = System.DateTime.Now.ToString() + ":" + msg + "\r\n" + mForm.txtLog.Text;

                    }));
            }
            ClsGlobal.IsTestRuning = false;
        }

        public void ReTest(List<int> lst)
        {



            if (ClsGlobal.OCVType == 1)  //OCV1 模式下 测试电压,内阻
            {
                foreach (var c in lst)
                {
                    if (!ChanleMapingSetting.DicTrayIndexMaping.ContainsKey(c + 1))
                    {
                        continue;
                    }
                    var devChannel = ChanleMapingSetting.DicTrayIndexMaping[c + 1].DeviceChanle;
                    double theIRVolt = 0;
                    //测试电压
                    CurrentTestStaticData.SWType = 2;
                    CurrentTestStaticData.TestType = 1;
                    CurrentTestStaticData.Channle = devChannel;
                    CurrentTestStaticData.IRAcir = 0;
                    CurrentTestStaticData.IRVolt = 0;
                    ReadTestData();

                    //更新数据
                    theIRVolt = CurrentTestStaticData.IRVolt;
                    theIRVolt = theIRVolt * 1000;
                    ClsGlobal.listETCELL[c].OCV_V1 = theIRVolt;
                    ChanleMapingSetting.ListBatTestData[c].OCV = theIRVolt;
                    ClsGlobal.G_dbl_VDataArr[c] = theIRVolt;
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);  //关闭通道
                foreach (var c in lst)
                {
                    if (!ChanleMapingSetting.DicTrayIndexMaping.ContainsKey(c + 1))
                    {
                        continue;
                    }
                    var devChannel = ChanleMapingSetting.DicTrayIndexMaping[c + 1].DeviceChanle;
                    double theIRAcir = 0;
                    //测试内阻
                    CurrentTestStaticData.SWType = 2;
                    CurrentTestStaticData.TestType = 2;
                    CurrentTestStaticData.Channle = devChannel;
                    CurrentTestStaticData.IRAcir = 0;
                    CurrentTestStaticData.IRVolt = 0;
                    ReadTestData();

                    //更新数据
                    theIRAcir = CurrentTestStaticData.IRAcir;
                    theIRAcir = Math.Round(theIRAcir * 1000 + double.Parse(ClsGlobal.ArrIRAdjustPara[c]),2); 
                    ClsGlobal.listETCELL[c].ACIR = theIRAcir;
                    ChanleMapingSetting.ListBatTestData[c].ACIR = theIRAcir;

                    ClsGlobal.G_dbl_ACIRArr[c] = theIRAcir;
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0); //关闭通道

                foreach (var c in lst)
                {
                    ClsGlobal.listETCELL[c].CODE = GetNGCode(ClsGlobal.listETCELL[c].OCV_V1, ClsGlobal.listETCELL[c].ACIR);
                }
            }
            else  //OCV2 模式下 只测试电压
            {
                foreach (var c in lst)
                {
                    if (!ChanleMapingSetting.DicTrayIndexMaping.ContainsKey(c + 1))
                    {
                        continue;
                    }
                    var devChannel = ChanleMapingSetting.DicTrayIndexMaping[c + 1].DeviceChanle;
                    SwitchDev.ChannelVoltIRSwitchContr(2, 1, devChannel);
                    Thread.Sleep(ClsGlobal.DelayTime);
                    double theDMMVolt = 0;
                    mDmm.ReadVolt(out theDMMVolt);
                    theDMMVolt = theDMMVolt * 1000;

                    //更新数据
                    ClsGlobal.listETCELL[c].CODE = CheckOCVTestNG(theDMMVolt);
                    ClsGlobal.listETCELL[c].OCV_V1 = theDMMVolt;
                    ChanleMapingSetting.ListBatTestData[c].OCV = theDMMVolt;
                    ClsGlobal.G_dbl_VDataArr[c] = theDMMVolt;
                }
                SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);  //关闭通道
            }




            #region  界面更新
            //ClsGlobal.WriteLog("[OCV+InR]刷新界面", ClsGlobal.sDebugOCVSelectionPath);
            if (mForm.IsHandleCreated == true)
            {
                mForm.Invoke(new EventHandler(delegate
                {
                    mForm.dgvTestData1.DataSource = null;
                    mForm.dgvTestData1.DataSource = ChanleMapingSetting.ListBatTestData.Where(a => a.TrayCodeChanle < 256 / 2).ToList();
                    mForm.dgvTestData2.DataSource = null;
                    mForm.dgvTestData2.DataSource = ChanleMapingSetting.ListBatTestData.Where(a => a.TrayCodeChanle >= 256 / 2).ToList();

                }));
            }
            #endregion
        }

        //校准内阻
        private void AdjustACIR()
        {
            double theIRVolt = 0;
            double theIRAcir = 0;
            double[] arrIRVolt = new double[ClsGlobal.TrayType];
            double[] arrIRAcir = new double[ClsGlobal.TrayType];
            double[] rV = new double[ClsGlobal.TrayType];
            double[] rA = new double[ClsGlobal.TrayType];
            //string strVal;
            //string[] arrACIRValStr;
            int theCHCount = ClsGlobal.ProbeBoardType;

            //将1~x通道分为A区或B区两部分
            //先测完A区的通道, 再测试B区通道
            List<int> lstRegionA = new List<int>();
            List<int> lstRegionB = new List<int>();

            DateTime StartTime;
            DateTime EndTime;

            try
            {
                mTestFinish = false;
                mStop = false;

                while (ClsGlobal.AdjustCount != ClsGlobal.AdjustNum)        //重复测试方案1
                {
                    #region 测内阻
                    mRTester.ClearData();

                    //测试开始
                    StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    //仪表初始化
                    mRTester.InitControl_IMM(2, 2);
                    Thread.Sleep(200);

                    //测量OCV ACIR
                    #region 20200705 zxz 修改为双通道测试
                    lstRegionA.Clear();
                    lstRegionB.Clear();

                    for (int i = 1; i <= theCHCount; i++)
                    {
                        if (((i - 1) / 16) % 2 == 0)
                        {
                            lstRegionA.Add(i);
                        }
                        else
                        {
                            lstRegionB.Add(i);
                        }
                    }

                    //测试方式1：A区电压，B区内阻
                    for (int i = 0; i < lstRegionA.Count; i++)
                    {
                        if (ClsGlobal.Trans_State != eTransState.TestWork)
                        {
                            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                            throw new Exception();
                        }

                        //SwitchDev.ChannelVoltIRSwitchContr(1, 2, lstRegionA[i]);
                        SwitchDev.ChannelVoltIRSwitchContr(2, 1, lstRegionA[i]);

                        Thread.Sleep(ClsGlobal.DelayTime);
                        mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                        rV[lstRegionA[i] - 1] = theIRVolt;
                        rA[lstRegionB[i] - 1] = theIRAcir == 10000000000 ? 0 : theIRAcir;
                    }
                    SwitchDev.ChannelVoltIRSwitchContr(2, 1, 0);

                    //测试方式2：A区内阻，B区电压
                    for (int i = 0; i < lstRegionA.Count; i++)
                    {
                        if (ClsGlobal.Trans_State != eTransState.TestWork)
                        {
                            ClsGlobal.OCV_TestState = eTestState.ErrTransChange;
                            throw new Exception();
                        }
                        SwitchDev.ChannelVoltIRSwitchContr(2, 2, lstRegionA[i]);
                        Thread.Sleep(ClsGlobal.DelayTime);

                        mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                        rA[lstRegionA[i] - 1] = theIRAcir;
                        rV[lstRegionB[i] - 1] = theIRVolt;

                        if (arrIRAcir[i] * 1000 > 50)                               //校准时, 50毫欧作为上限值.
                        {
                            throw new Exception("校准时,通道" + (i + 1) + "测试阻抗过大,校准停止!");
                        }
                    }
                    SwitchDev.ChannelVoltIRSwitchContr(2, 2, 0);
                    #endregion
                    #region 20200702 zxz 通道映射
                    for (int i = 0; i < theCHCount; i++)
                    {
                        foreach (ChannelItem itm in ClsGlobal.ChannelMapping)
                        {
                            if (itm.RealChannel == i + 1)
                            {
                                arrIRVolt[itm.Channel - 1] = rV[i];
                                arrIRAcir[itm.Channel - 1] = rA[i];
                            }
                        }
                    }
                    #endregion

                    SwitchDev.ChannelVoltIRSwitchContr(1, 2, 0);            //结束,通道全部关断
                    EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    //数据载入 
                    for (int i = 0; i < theCHCount; i++)
                    {
                        ClsGlobal.ArrAdjustACIR[i] = arrIRAcir[i] * 1000;      //不进行数据累加,每次校准后采用最新校准值作为内阻补偿值
                    }

                    ClsGlobal.AdjustCount++;
                }
                #endregion
                ClsGlobal.OCV_TestState = eTestState.AdjustEnd;        //校准OK
            
            }
            catch (Exception ex)
            {
                //校准异常   
                ClsGlobal.OCV_TestState = eTestState.ErrAdjust;
                ClsGlobal.TestOCVMsg = ex.Message;
                //MessageBox.Show(ex.Message.ToString());
                ClsGlobal.AdjustCount = 0;
            }
            ClsGlobal.IsTestRuning = false;
        }


        public void StopManualTest()
        {
            mStopManual = true;
        }


        public double GetRAdjust(int index)
        {
            string path = ClsGlobal.mIRAdjustPath;
            double[] ArrAdjust = new double[ClsGlobal.TrayType];

            double result = 0;
            var strValue = INIAPI.INIGetStringValue(path, "ACIRAdjust", "CH" + index.ToString(), "0");
            try
            {
                result = double.Parse(strValue);
            }
            catch { }
            return result;

        }

        #region 采用并发读取的方式优化速度 20200828 由ajone新增

        void ReadTestData()
        {
            Task task1 = new Task(ChannelVoltIRSwitchContr);
            Task task2 = new Task(ReadIR);
            Task task3 = new Task(ReadV);
            task1.Start();
            task1.Wait();
            task2.Start();
            task3.Start();
            task3.Wait();
            task2.Wait();
        }

        /// <summary>
        /// 仅仅用于同步读取两个仪器的测试数值,请勿作他用
        /// </summary>
        class CurrentTestStaticData
        {
            public static int SWType { get; set; }
            public static int TestType { get; set; }
            public static int Channle { get; set; }

            public static double IRAcir { get; set; }
            public static double IRVolt { get; set; }
        }
        void ChannelVoltIRSwitchContr()
        {
            try
            {
                SwitchDev.ChannelVoltIRSwitchContr(CurrentTestStaticData.SWType, CurrentTestStaticData.TestType, CurrentTestStaticData.Channle);
                Thread.Sleep(ClsGlobal.DelayTime);
            }
            catch
            {

            }
        }

        void ReadIR()
        {
            try
            {
                double theIRVolt;
                double theIRAcir;
                mRTester.ReadDataRV(out theIRVolt, out theIRAcir);
                CurrentTestStaticData.IRAcir = theIRAcir;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取内阻异常{ex.Message}");
                throw ex;
            }
        }
        void ReadV()
        {
            try
            {
                double theIRVolt;
                mDmm.ReadVolt(out theIRVolt);
                CurrentTestStaticData.IRVolt = theIRVolt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取电压异常:{ex.Message}");
                throw ex;
            }
        }
        #endregion

        ////校准ACIR
        //public void StartAdjustTestACIRAction()
        //{
        //    ThreadTestAction = new Thread(new ThreadStart(AdjustACIR));
        //    ThreadTestAction.Start();

        //}

        ////手动测试ACIR
        //public void StartManualTestACIRAction()
        //{            
        //    if (ClsGlobal.OCV_RunMode != eRunMode.ACIRAdjust)
        //    {
        //        ThreadTestAction = new Thread(new ThreadStart(ManualTestOCVACIR));
        //    }
        //    ThreadTestAction.Start();
        //}

    }



}