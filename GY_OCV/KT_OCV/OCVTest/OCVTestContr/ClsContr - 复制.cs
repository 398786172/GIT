/****************通道切换IO控制*******************

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


namespace OCV
{
    //OCV测试
    public class ClsOCVContr1
    {
        FrmSys mForm; 
        FrmManualTest ManualTest;
        FrmManualAdjust ManualAdjust;
        Thread ThreadTestAction;

        private ClsSWControl SWControl;       //切换控制
        private ClsDMM_Ag344X DMM_Ag344X;     //万用表
        private ClsHIOKI4560 HIOKI4560;       //内阻仪BT4560控制
        private ClsHIOKI365X HIOKI365X;       //内阻仪BT356x控制
        //测试参数
        private int ShowHalfVal = ClsGlobal.TrayType / 2;                           //界面更新用
                                                                                    // int mStartBattNum;                                                //开始测量的电池通道号,随当前测试位而变动                
        private int CHCount = ClsGlobal.TrayType;                             //测试通道数 

        //手动测试
        private bool mManualTestStop;
        private bool mManualTestFinish;
        public bool ManualTestFinish { get { return mManualTestFinish; } set { mManualTestFinish = value; } }

        //电压清0 
        private bool mManualVoltZeroStop;
        private bool mManualVoltZeroFinish;
        public bool ManualVoltZerotFinish { get { return mManualVoltZeroFinish; } set { mManualVoltZeroFinish = value; } }

        //内阻校准
        private bool mManualIRAdjustStop;
        private bool mManualIRAdjustFinish;
        public bool ManualIRAdjustFinish { get { return mManualIRAdjustFinish; } set { mManualIRAdjustFinish = value; } }

        //内阻计量
        private bool mManualIRMeterStop;
        private bool mManualIRMeterFinish;
        public bool ManualIRMeterFinish { get { return mManualIRMeterFinish; } set { mManualIRMeterFinish = value; } }

        //自动测试
        private bool mAutoTestStop;
        private bool mAutoTestFinish;
        public bool AutoTestFinish { get { return mAutoTestFinish; } set { mAutoTestFinish = value; } }

        public void StopAction()
        {
            mAutoTestStop = true;
        }


        public ClsOCVContr1(ClsSWControl swControl, ClsDMM_Ag344X dmm_Ag344X)
        {
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
        }

        public ClsOCVContr1(ClsSWControl swControl, ClsDMM_Ag344X dmm_Ag344X, ClsHIOKI365X hioki365X)
        {
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.HIOKI365X = hioki365X;
        }
        public ClsOCVContr1(ClsSWControl swControl, ClsDMM_Ag344X dmm_Ag344X, ClsHIOKI4560 hioki4560)
        {
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.HIOKI4560 = hioki4560;
        }

        //测试流程
        public void StartTestAction()
        {
            try
            {
                mAutoTestStop = false;
                mAutoTestFinish = false;
                ThreadTestAction = new Thread(new ThreadStart(TestProcess));
                ThreadTestAction.IsBackground = true;
                ThreadTestAction.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //手动测试流程
        public void StartManualTestVolt_PosNeg_Action(FrmManualTest MuTest)
        {
            ManualTest = MuTest;
            ThreadTestAction = new Thread(new ThreadStart(ManualTestVolt_PosNeg));
            ThreadTestAction.Start();
        }

        //手动测试壳体电压
        public void StartManualTestVolt_ShellNeg_Action(FrmManualTest MuTest)
        {
            ManualTest = MuTest;
            ThreadTestAction = new Thread(new ThreadStart(ManualTestVolt_ShellNeg));
            ThreadTestAction.Start();
        }

        //手动测试内阻
        public void StartManualTestIR_BT4560_Action(FrmManualTest MuTest)
        {
            ManualTest = MuTest;
            ThreadTestAction = new Thread(new ThreadStart(ManualTestIR_BT4560));
            ThreadTestAction.Start();
        }

        //手动电压清零
        public void StartManualVoltZero_Action(FrmManualAdjust MauAdjust)
        {
            ManualAdjust = MauAdjust;
            ThreadTestAction = new Thread(new ThreadStart(ManualVoltZero));
            ThreadTestAction.Start();
        }

        //手动内阻测试(真实值和已校准的内阻值)
        public void StartManualIRAdjust_Test_Action(FrmManualAdjust MauAdjust)
        {
            ManualAdjust = MauAdjust;
            ThreadTestAction = new Thread(new ThreadStart(ManualIRAdjust_Test));
            ThreadTestAction.Start();
        }
        

        //手动内阻校准
        public void StartManualIRAdjust_Action(FrmManualAdjust MauAdjust)
        {
            ManualAdjust = MauAdjust;
            ThreadTestAction = new Thread(new ThreadStart(ManualIRAdjust));
            ThreadTestAction.Start();
        }

        //手动内阻计量
        public void StartManualIRMetering_Action(FrmManualAdjust MauAdjust)
        {
            ManualAdjust = MauAdjust;
            ThreadTestAction = new Thread(new ThreadStart(ManualIRMetering));
            ThreadTestAction.Start();
        }
        //测试流程
        private void TestProcess()
        {
            try
            {
                mAutoTestFinish = false;
                mAutoTestStop = false;
                ClsGlobal.TestStartTime = System.DateTime.Now;
                switch (ClsGlobal.TestType)   //电压:0   电压壳压:1   电压 壳压 内阻 2
                {
                    case 0:
                        #region 测正负极 
                        this.TestVoltForProc();
                        #endregion
                        this.CalVoltDrop();
                        this.NGStateOutput();
                        this.CalDROPRange();
                        break;
                    case 1:

                        #region 测正负极 
                        this.TestVoltForProc();
                        #endregion

                        #region 测壳体电压
                        this.TestShellVoltForProc();
                        #endregion

                        this.CalVoltDrop();
                        this.NGStateOutput();
                        this.CheckSVNG();
                        this.CalDROPRange();

                        break;
                    case 2:
                        #region 测正负极 
                        this.TestVoltForProc();
                        #endregion

                        #region 测壳体电压
                        this.TestShellVoltForProc();
                        #endregion

                        #region 测内阻 
                        this.TestACIRForProc();
                        #endregion

                        this.CalVoltDrop();
                        this.NGStateOutput();
                        this.CheckSVNG();
                        this.CalDROPRange();

                        if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                        {
                            this.CalACIRRange();
                        }


                        break;
                    default:
                        break;
                }
                #region  测试结束总NG判定

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                    {
                        if (ClsGlobal.listETCELL[i].Test_NgResult.NgType == 1)
                        {
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            continue;
                        }

                        if (ClsGlobal.OCVType == 3)
                        {
                            if (ClsGlobal.ENVoltDrop == "Y" && ClsGlobal.IS_Enable_DropRange == "Y")
                            {
                                if (ClsGlobal.listETCELL[i].DROP_NgResult.NgType == 1)
                                {
                                    ClsGlobal.listETCELL[i].NgState = "NG";
                                    continue;
                                }
                            }
                        }

                        if (ClsGlobal.TestType > 0)
                        {
                            if (ClsGlobal.listETCELL[i].SV_NgResult.NgType == 1)
                            {
                                ClsGlobal.listETCELL[i].NgState = "NG";
                                continue;
                            }
                        }
                        if (ClsGlobal.TestType == 2)
                        {
                            if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                            {
                                if (ClsGlobal.listETCELL[i].ACIR_NgResult.NgType == 1)
                                {
                                    ClsGlobal.listETCELL[i].NgState = "NG";
                                }
                            }

                        }

                    }
                }

                #endregion

                #region 显示数据
                ClsGlobal.TestEndTime = System.DateTime.Now;

                if (mForm.IsHandleCreated == true)
                {
                    mForm.Invoke(new EventHandler(delegate
                    {
                        for (int i = 0; i < ClsGlobal.TrayType; i++)
                        {
                            ClsGlobal.listETCELL[i].End_Write_Time = ClsGlobal.TestEndTime.ToString("yyy-MM-dd HH:mm:ss");    //测试时间;

                            if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                            {
                                //mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].Test_NgResult.NgCode;
                                //mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].Test_NgResult.NgDescribe;
                                //if (ClsGlobal.listETCELL[i].Test_NgResult.NgDescribe !="合格")
                                //{
                                //    mForm.dgvTest.Rows[i].Cells["Col_CODE"].Style.ForeColor = Color.Red;
                                //    mForm.dgvTest.Rows[i].Cells["Col_Des"].Style.ForeColor = Color.Red;
                                //}
                                if (ClsGlobal.listETCELL[i].NgState == "NG")
                                {
                                    mForm.dgvTest.Rows[i].Cells["Col_CODE"].Style.ForeColor = Color.Red;
                                    mForm.dgvTest.Rows[i].Cells["Col_Des"].Style.ForeColor = Color.Red;
                                    if (ClsGlobal.listETCELL[i].Test_NgResult.NgDescribe.Contains("电压"))
                                    {
                                        mForm.dgvTest.Rows[i].Cells["Col_OCV"].Style.ForeColor = Color.Red;
                                        mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].Test_NgResult.NgCode;
                                        mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].Test_NgResult.NgDescribe;
                                    }

                                    if (ClsGlobal.OCVType == 3)
                                    {
                                        if (ClsGlobal.ENVoltDrop == "Y")
                                        {
                                            if (ClsGlobal.IS_Enable_DropRange == "Y")
                                            {
                                                if (ClsGlobal.listETCELL[i].DROP_NgResult.NgType == 1)
                                                {
                                                    if (mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value.ToString() != "")
                                                    {
                                                        mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value += "| " + ClsGlobal.listETCELL[i].DROP_NgResult.NgCode;
                                                        mForm.dgvTest.Rows[i].Cells["Col_Des"].Value += "| " + ClsGlobal.listETCELL[i].DROP_NgResult.NgDescribe;
                                                    }
                                                    else
                                                    {
                                                        mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].DROP_NgResult.NgCode;
                                                        mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].DROP_NgResult.NgDescribe;
                                                    }

                                                }

                                            }
                                        }
                                    }

                                    if (ClsGlobal.TestType > 0)
                                    {
                                        if (ClsGlobal.listETCELL[i].SV_NgResult.NgType == 1)
                                        {
                                            mForm.dgvTest.Rows[i].Cells["Col_CaseOCV"].Style.ForeColor = Color.Red;
                                            if (mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value.ToString() != "")
                                            {
                                                mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value += "| " + ClsGlobal.listETCELL[i].SV_NgResult.NgCode;
                                                mForm.dgvTest.Rows[i].Cells["Col_Des"].Value += "| " + ClsGlobal.listETCELL[i].SV_NgResult.NgDescribe;
                                            }
                                            else
                                            {
                                                mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].SV_NgResult.NgCode;
                                                mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].SV_NgResult.NgDescribe;
                                            }
                                        }

                                    }

                                    if (ClsGlobal.TestType == 2)
                                    {
                                        if (ClsGlobal.listETCELL[i].Test_NgResult.NgCode.Contains("ACIR"))
                                        {
                                            mForm.dgvTest.Rows[i].Cells["Col_ACIR"].Style.ForeColor = Color.Red;
                                            mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].Test_NgResult.NgCode;
                                            mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].Test_NgResult.NgDescribe;
                                        }

                                        if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                                        {
                                            if (ClsGlobal.listETCELL[i].ACIR_NgResult.NgType == 1)
                                            {
                                                if (mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value.ToString() != "")
                                                {
                                                    mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value += "| " + ClsGlobal.listETCELL[i].ACIR_NgResult.NgCode;
                                                    mForm.dgvTest.Rows[i].Cells["Col_Des"].Value += "| " + ClsGlobal.listETCELL[i].ACIR_NgResult.NgDescribe;
                                                }
                                                else
                                                {
                                                    mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].ACIR_NgResult.NgCode;
                                                    mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].ACIR_NgResult.NgDescribe;
                                                }
                                            }
                                        }

                                    }
                                }

                                mForm.dgvTest.Rows[i].Cells["Col_TEMP_P"].Value = ClsGlobal.listETCELL[i].PostiveTMP;
                                mForm.dgvTest.Rows[i].Cells["Col_TEMP_N"].Value = ClsGlobal.listETCELL[i].NegativeTMP;
                            }

                        }
                    }
                    ));
                }
                #endregion

                #region  测试结束，判断是否复测

                //测试次数判断
                ClsGlobal.TestCount++;
                //ACIR有异常测试数据,重测
                if (ClsGlobal.lstACIRErrNo.Count > ClsGlobal.SetEnOCV && ClsGlobal.TestCount < ClsGlobal.MaxTestNum)
                {
                    ClsGlobal.OCV_TestState = eTestState.TestAgain;
                    //用大量程                  
                }
                else
                {
                    ClsGlobal.OCV_TestState = eTestState.TestOK;            //测试成功
                    ClsGlobal.lstACIRErrNo.Clear();////清空数据      
                    ClsGlobal.TestCount = 0;
                }
                #endregion

                //ClsGlobal.OCV_TestState = eTestState.TestOK;            //测试成功

                mAutoTestFinish = true;//该次测试完成 
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());

            }
        }
        //流程中测试电池 (电压)
        private void TestVoltForProc()
        {
            double theDMMVolt = 0;

            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            double theVolt = 0;

            try
            {
                #region 测电压

                //仪表初始化
                try
                {
                    mDmm.InitControl_IMM();
                    Thread.Sleep(30);
                }
                catch (Exception ex)
                {
                    throw new Exception("万用表出错," + ex.Message, ex);
                }
                #region 测正负极电压
                ClsGlobal.negVolt = new int[88];
                for (int Num = 0; Num < CHCount; Num++)
                {
                    if (mAutoTestStop == true)
                    {
                        SwitchDev.ChannelVoltSwitch(1, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]); //通道选择

                    SwitchDev.ChannelVoltSwitch(1, ActualNum);        //正极对负极

                    Thread.Sleep(ClsGlobal.SleepTime);

                    mDmm.ReadVolt(out theDMMVolt);
                    arrDMMVolt[Num] = theDMMVolt;

                    #region 数据载入

                    if (Math.Abs(theVolt) < 1e+6)
                    {
                        ClsGlobal.G_dbl_VoltArr[Num] = Math.Round(arrDMMVolt[Num] * 1000, 4);
                    }
                    else
                    {
                        ClsGlobal.G_dbl_VoltArr[Num] = 99999;
                    }

                    //负值记录
                    if (ClsGlobal.G_dbl_VoltArr[Num] < 0)
                    {
                        ClsGlobal.negVolt[Num] = 1;
                    }
                    ClsGlobal.listETCELL[Num].OCV_Now = ClsGlobal.G_dbl_VoltArr[Num];
                    ClsGlobal.listETCELL[Num].Rev_OCV = Math.Round((ClsGlobal.G_dbl_VoltArr[Num] +
                       ((ClsGlobal.G_dbl_P_TempArr[Num] + ClsGlobal.G_dbl_P_TempArr[Num]) / 2 - ClsGlobal.TempBase) * ClsGlobal.TempParaModify), 4);    //计算电压修正值    
                    //  ClsGlobal.listETCELL[i].OCV_Write_Time = ClsGlobal.TestStartTime.ToString("yyy-MM-dd HH:mm:ss");  //开始时间
                    #endregion

                    #region 显示数据
                    if (mForm.IsHandleCreated == true)
                    {
                        mForm.Invoke(new EventHandler(delegate
                        {
                            if (!ClsGlobal.listETCELL[Num].Cell_ID.Contains("NullCellCode"))
                            {
                                mForm.dgvTest.Rows[Num].Cells["Col_OCV"].Value = ClsGlobal.G_dbl_VoltArr[Num].ToString("F4");
                            }
                        }
                        ));
                    }
                    #endregion
                }
                SwitchDev.ChannelVoltSwitch(1, 0);      //结束,通道全部关断
                Thread.Sleep(20);
                #endregion

                #region  //数据载入
                //ClsGlobal.negVolt = new int[88];

                //for (int i = 0; i < CHCount; i++)
                //{
                //    if (Math.Abs(theVolt) < 1e+6)
                //    {
                //        ClsGlobal.G_dbl_VoltArr[i] = Math.Round(arrDMMVolt[i] * 1000, 4);
                //    }
                //    else
                //    {
                //        ClsGlobal.G_dbl_VoltArr[i] = 99999;
                //    }

                //    //负值记录
                //    if (ClsGlobal.G_dbl_VoltArr[i] < 0)
                //    {
                //        ClsGlobal.negVolt[i] = 1;
                //    }
                //    ClsGlobal.listETCELL[i].OCV_Now = ClsGlobal.G_dbl_VoltArr[i];
                //    ClsGlobal.listETCELL[i].Rev_OCV = Math.Round((ClsGlobal.G_dbl_VoltArr[i] +
                //       ((ClsGlobal.G_dbl_P_TempArr[i] + ClsGlobal.G_dbl_P_TempArr[i]) / 2 - ClsGlobal.TempBase) * ClsGlobal.TempParaModify), 4);    //计算电压修正值    
                //                                                                                                                                    //  ClsGlobal.listETCELL[i].OCV_Write_Time = ClsGlobal.TestStartTime.ToString("yyy-MM-dd HH:mm:ss");  //开始时间
                //}

                #endregion

                #region //显示数据
                //if (mForm.IsHandleCreated == true)
                //{
                //    mForm.Invoke(new EventHandler(delegate
                //    {
                //        for (int i = 0; i < ClsGlobal.TrayType; i++)
                //        {
                //            if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") && !ClsGlobal.listETCELL[i].Cell_ID.Contains("BYD"))
                //            {
                //                mForm.dgvTest.Rows[i].Cells["Col_OCV"].Value = ClsGlobal.G_dbl_VoltArr[i].ToString("F4");
                //            }

                //        }
                //    }
                //    ));
                //}
                #endregion
                #endregion

            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
        }
        //流程中测试电池 (内阻)   
        private void TestACIRForProc()
        {
            double theIRSample = 0;
            double[] arrACIR_BT4 = new double[ClsGlobal.TrayType];
            try
            {
                #region 测内阻 
                if (ClsGlobal.OCV_TestState != eTestState.TestAgain)
                {
                    #region 复测  仅把第一次测量失败的进行更新
                    for (int Num = 0; Num < CHCount; Num++)
                    {
                        if (mAutoTestStop == true)
                        {
                            SwitchDev.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断   
                            throw new Exception("测试被终止");
                        }

                        int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]); //通道选择

                        SwitchDev.ChannelAcirSwitchContr(2, ActualNum);     //内阻测量

                        Thread.Sleep(ClsGlobal.SWDelayTime);

                        mRTester_BT4.ReadRData(out theIRSample);

                        arrACIR_BT4[Num] = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[ActualNum - 1]);   //经过adjust     

                        #region 数据载入
                        //内阻数据
                        if (0 < arrACIR_BT4[Num] && arrACIR_BT4[Num] < ClsGlobal.ReTestLmt_ACIR)
                        {
                            //内阻数据
                            ClsGlobal.G_dbl_ACIRArr[Num] = Math.Round(arrACIR_BT4[Num], 4);
                        }
                        else
                        {
                            ClsGlobal.lstACIRErrNo.Add(Num);
                            ClsGlobal.G_dbl_ACIRArr[Num] = 99999;
                        }
                        ClsGlobal.listETCELL[Num].ACIR_Now = ClsGlobal.G_dbl_ACIRArr[Num];


                        #endregion

                        #region 显示数据
                        if (mForm.IsHandleCreated == true)
                        {
                            mForm.Invoke(new EventHandler(delegate
                            {
                                if (!ClsGlobal.listETCELL[Num].Cell_ID.Contains("NullCellCode"))
                                {
                                    mForm.dgvTest.Rows[Num].Cells["Col_ACIR"].Value = ClsGlobal.G_dbl_ACIRArr[Num].ToString("F4");
                                }
                            }
                            ));
                        }


                        #endregion
                    }
                    SwitchDev.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断

                    #endregion
                }
                else
                {

                    #region 复测  仅把第一次测量失败的进行更新

                    int lstErrNo = ClsGlobal.lstACIRErrNo.Count;
                    List<int> lstACIRErrNo = new List<int>();   //内阻测量失败电池号,从0开始计算

                    SwitchDev.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断

                    #region 测试内阻
                    for (int i = 0; i < lstErrNo; i++)
                    {
                        if (mAutoTestStop == true)
                        {
                            SwitchDev.ChannelAcirSwitchContr(2, 0);      //结束,通道全部关断
                            throw new Exception("测试被终止");
                        }

                        int item = ClsGlobal.lstACIRErrNo[i];
                        SwitchDev.ChannelAcirSwitchContr(2, item + 1);     //内阻测量

                        Thread.Sleep(100);
                        mRTester_BT4.ReadRData(out theIRSample);

                        arrACIR_BT4[item] = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[item]);   //经过adjust     

                        #region 数据载入
                        //内阻数据
                        if (0 < arrACIR_BT4[item] && arrACIR_BT4[item] < ClsGlobal.ReTestLmt_ACIR)
                        {
                            //内阻数据
                            ClsGlobal.G_dbl_ACIRArr[item] = Math.Round(arrACIR_BT4[item], 4);
                        }
                        else
                        {
                            lstACIRErrNo.Add(item);
                            ClsGlobal.G_dbl_ACIRArr[item] = 99999;
                        }
                        ClsGlobal.listETCELL[item].ACIR_Now = ClsGlobal.G_dbl_ACIRArr[item];
                        #endregion

                        #region 显示数据
                        if (mForm.IsHandleCreated == true)
                        {
                            mForm.Invoke(new EventHandler(delegate
                            {
                                if (!ClsGlobal.listETCELL[item].Cell_ID.Contains("NullCellCode"))
                                {
                                    mForm.dgvTest.Rows[item].Cells["Col_ACIR"].Value = ClsGlobal.G_dbl_ACIRArr[item].ToString("F4");
                                }
                            }
                            ));
                        }
                        #endregion
                    }


                    ClsGlobal.lstACIRErrNo.Clear();
                    if (lstACIRErrNo != null)
                    {
                        for (int i = 0; i < lstACIRErrNo.Count; i++)
                        {
                            ClsGlobal.lstACIRErrNo.Add(lstACIRErrNo[i]);
                        }
                    }

                    #endregion
                    #endregion
                }

                #region //数据载入
                //for (int i = 0; i < CHCount; i++)
                //{
                //    //电压数据
                //    if (Math.Abs(arrACIR_BT4[i]) < 1e+6)
                //    {
                //        //内阻数据
                //        ClsGlobal.G_dbl_ACIRArr[i] = Math.Round(arrACIR_BT4[i], 4);
                //    }
                //    else
                //    {
                //        ClsGlobal.G_dbl_ACIRArr[i] = 99999;
                //    }
                //    ClsGlobal.listETCELL[i].ACIR_Now = ClsGlobal.G_dbl_ACIRArr[i];
                //}

                #endregion

                #region 显示数据
                //if (mForm.IsHandleCreated == true)
                //{
                //    mForm.Invoke(new EventHandler(delegate
                //    {
                //        for (int i = 0; i < ClsGlobal.TrayType; i++)
                //        {
                //            if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") && !ClsGlobal.listETCELL[i].Cell_ID.Contains("BYD"))
                //            {
                //                mForm.dgvTest.Rows[i].Cells["Col_ACIR"].Value = ClsGlobal.G_dbl_ACIRArr[i].ToString("F4");
                //            }  
                //        }
                //    }
                //    ));
                //}

                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //流程中测试电池 (壳压)
        private void TestShellVoltForProc()
        {
            double theDMMVolt = 0;
            double[] arrShellVolt = new double[ClsGlobal.TrayType];
            double theVolt = 0;
            //string sEndTestTime = "";               //测试开始时间

            try
            {
                mAutoTestFinish = false;
                mAutoTestStop = false;

                //壳体电压判断参数初始化
                ClsGlobal.ShellVolIsErr = false;
                ClsGlobal.arrShellVolErrChannel = new int[88];
                #region 测电压

                //仪表初始化
                try
                {
                    //Thread.Sleep(150);
                    mDmm.InitControl_IMM();
                    Thread.Sleep(30);
                }
                catch (Exception ex)
                {
                    throw new Exception("万用表出错," + ex.Message, ex);
                }

                #region 壳体电压
                ClsGlobal.negC_Volt = new int[88];
                //测壳体电压
                for (int Num = 0; Num < CHCount; Num++)
                {
                    if (mAutoTestStop == true)
                    {
                        SwitchDev.ChannelVoltSwitchContr(2, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]); //通道选择
                    SwitchDev.ChannelVoltSwitch(2, ActualNum); //壳体负极测量
                    Thread.Sleep(ClsGlobal.SleepTime);

                    mDmm.ReadVolt(out theDMMVolt);
                    arrShellVolt[Num] = theDMMVolt;

                    #region 数据载入

                    if (Math.Abs(theVolt) < 1e+6)
                    {
                        ClsGlobal.G_dbl_VshellArr[Num] = Math.Round(arrShellVolt[Num] * 1000, 4);
                    }
                    else
                    {
                        ClsGlobal.G_dbl_VshellArr[Num] = 99999;
                    }

                    //负值记录
                    if (ClsGlobal.G_dbl_VoltArr[Num] < 0 || ClsGlobal.G_dbl_VshellArr[Num] < 0)
                    {
                        ClsGlobal.negC_Volt[Num] = 1;
                    }

                    ClsGlobal.listETCELL[Num].OCV_Shell_Now = ClsGlobal.G_dbl_VshellArr[Num];

                    #endregion

                    #region 显示数据
                    if (mForm.IsHandleCreated == true)
                    {
                        mForm.Invoke(new EventHandler(delegate
                        {
                            if (!ClsGlobal.listETCELL[Num].Cell_ID.Contains("NullCellCode"))
                            {
                                mForm.dgvTest.Rows[Num].Cells["Col_CaseOCV"].Value = ClsGlobal.G_dbl_VshellArr[Num].ToString("F4");
                            }

                        }
                        ));
                    }
                    #endregion
                }

                SwitchDev.ChannelVoltSwitchContr(2, 0);      //结束,通道全部关断

                #endregion

                #region //数据载入
                //ClsGlobal.negC_Volt = new int[88];

                //for (int i = 0; i < CHCount; i++)
                //{
                //    if (Math.Abs(theVolt) < 1e+6)
                //    {
                //        ClsGlobal.G_dbl_VshellArr[i] = Math.Round(arrShellVolt[i] * 1000, 4);
                //    }
                //    else
                //    {
                //        ClsGlobal.G_dbl_VshellArr[i] = 99999;
                //    }

                //    //负值记录
                //    if (ClsGlobal.G_dbl_VoltArr[i] < 0 || ClsGlobal.G_dbl_VshellArr[i] < 0)
                //    {
                //        ClsGlobal.negC_Volt[i] = 1;
                //    }

                //    ClsGlobal.listETCELL[i].OCV_Shell_Now = ClsGlobal.G_dbl_VshellArr[i];
                //}

                #endregion

                #region //显示数据
                //if (mForm.IsHandleCreated == true)
                //{
                //    mForm.Invoke(new EventHandler(delegate
                //    {
                //        for (int i = 0; i < ClsGlobal.TrayType; i++)
                //        {
                //            if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") && !ClsGlobal.listETCELL[i].Cell_ID.Contains("BYD"))
                //            {
                //                mForm.dgvTest.Rows[i].Cells["Col_CaseOCV"].Value = ClsGlobal.G_dbl_VshellArr[i].ToString("F4");
                //            }

                //        }
                //    }
                //    ));
                //}


                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //电池的ocv，acir ，压降，NG状态输出
        private void NGStateOutput()
        {
            #region NG判断
            NgResult mNgResult;
            //--------------------------------------------------------
            //NG判断
            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                mNgResult.NgType = 0;
                mNgResult.NgCode = "00";
                mNgResult.NgDescribe = "合格";
                mNgResult.NgState = "OK";
                if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                {
                    if (ClsGlobal.TestType == 0)
                    {
                        //电压
                        if (ClsGlobal.G_dbl_VoltArr[i] > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType+"X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                           // return;
                        }
                        else if (ClsGlobal.G_dbl_VoltArr[i] < ClsGlobal.DownLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超下限";
                           // return;
                        }
                        else if(ClsGlobal.OCVType == 3 && ClsGlobal.ENVoltDrop=="Y")
                        {
                            //压降
                            if (ClsGlobal.listETCELL[i].VoltDrop_Now > ClsGlobal.MaxVoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZX2";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "压降超上限";
                               
                            }
                            //压降
                            else if (ClsGlobal.listETCELL[i].VoltDrop_Now > ClsGlobal.VoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZF";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "返自放电";

                            }
                            //压降
                            else if (ClsGlobal.listETCELL[i].VoltDrop_Now < ClsGlobal.MinVoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZX1";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "压降超下限";
                               
                            }
                        }

                    }
                    else if (ClsGlobal.TestType == 1)
                    {
                        //电压
                        if ((ClsGlobal.G_dbl_VoltArr[i]) > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                           // return;
                        }
                        else if ((ClsGlobal.G_dbl_VoltArr[i]) < ClsGlobal.DownLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超下限";
                           // return;
                        }
                        else if (ClsGlobal.OCVType == 3 && ClsGlobal.ENVoltDrop == "Y")
                        {
                            //压降
                            if (ClsGlobal.listETCELL[i].VoltDrop_Now > ClsGlobal.MaxVoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZX2";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "压降超上限";

                            }
                            //压降
                            else if (ClsGlobal.listETCELL[i].VoltDrop_Now > ClsGlobal.VoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZF";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "返自放电";

                            }
                            //压降
                            else if (ClsGlobal.listETCELL[i].VoltDrop_Now < ClsGlobal.MinVoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZX1";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "压降超下限";

                            }
                        }
                        ////壳压
                        //if ((ClsGlobal.G_dbl_VshellArr[i]) > ClsGlobal.UpLmt_SV)
                        //{
                        //    mNgResult.NgType = '2';
                        //    mNgResult.NgCode = "C1";
                        //    mNgResult.NgState = "NG";
                        //    mNgResult.NgDescribe = "大于上限壳压";
                        //    return;
                        //}
                        //if ((ClsGlobal.G_dbl_VshellArr[i]) < ClsGlobal.DownLmt_SV)
                        //{
                        //    mNgResult.NgType = '2';
                        //    mNgResult.NgCode = "C2";
                        //    mNgResult.NgState = "NG";
                        //    mNgResult.NgDescribe = "小于下限壳压";
                        //    return;
                        //}
                    }
                    else if (ClsGlobal.TestType == 2)
                    {
                        //电压
                        if ((ClsGlobal.G_dbl_VoltArr[i]) > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                           // return;
                        }
                        else if ((ClsGlobal.G_dbl_VoltArr[i]) < ClsGlobal.DownLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超下限";
                           // return;
                        }

                        ////壳压
                        //if ((ClsGlobal.G_dbl_VshellArr[i]) > ClsGlobal.UpLmt_SV)
                        //{
                        //    mNgResult.NgType = '2';
                        //    mNgResult.NgCode = "S1";
                        //    mNgResult.NgState = "NG";
                        //    mNgResult.NgDescribe = "大于上限壳压";
                        //    return;
                        //}
                        //if ((ClsGlobal.G_dbl_VshellArr[i]) < ClsGlobal.DownLmt_SV)
                        //{
                        //    mNgResult.NgType = '2';
                        //    mNgResult.NgCode = "S2";
                        //    mNgResult.NgState = "NG";
                        //    mNgResult.NgDescribe = "小于下限壳压";
                        //    return;
                        //}

                        else if(ClsGlobal.G_dbl_ACIRArr[i]> ClsGlobal.UpLmt_ACIR)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = "AX2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "ACIR超上限";
                           // return;
                        }
                        else if(ClsGlobal.G_dbl_ACIRArr[i] < ClsGlobal.DownLmt_ACIR)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = "AX1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "ACIR超下限";
                           // return;
                        }
                        else if (ClsGlobal.OCVType ==3 && ClsGlobal.ENVoltDrop == "Y")
                        {
                            //压降
                            if (ClsGlobal.listETCELL[i].VoltDrop_Now > ClsGlobal.MaxVoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZX2";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "压降超上限";

                            }
                            //压降
                            else if (ClsGlobal.listETCELL[i].VoltDrop_Now > ClsGlobal.VoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZF";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "返自放电";

                            }
                            //压降
                            else if (ClsGlobal.listETCELL[i].VoltDrop_Now < ClsGlobal.MinVoltDrop)
                            {
                                mNgResult.NgType = 1;
                                mNgResult.NgCode = "ZX1";
                                mNgResult.NgState = "NG";
                                mNgResult.NgDescribe = "压降超下限";

                            }
                        }
                    }
                   
                    //ClsGlobal.listETCELL[i].NGStatus = mNgResult.NgType;
                    //ClsGlobal.listETCELL[i].NGSt = mNgResult.NgState;
                    //ClsGlobal.listETCELL[i].NGReason = mNgResult.NgCode;
                    //ClsGlobal.listETCELL[i].NGRea = mNgResult.NgDescribe;
                }
                else
                {
                    mNgResult.NgType = 1;
                    mNgResult.NgCode = "C";
                    mNgResult.NgState = "NG";
                    mNgResult.NgDescribe = "无电池";
                    //ClsGlobal.listETCELL[i].NGStatus = '0';
                    //ClsGlobal.listETCELL[i].NGSt = "NG";
                    //ClsGlobal.listETCELL[i].NGReason ="C";
                    //ClsGlobal.listETCELL[i].NGRea = "无电池";
                }

                ClsGlobal.listETCELL[i].Test_NgResult = mNgResult;
            }
            #endregion  
        }

        //计算ACIR极差
        public void CalACIRRange()
        {
            List<double> lstVSAcirVal = new List<double>();
            try
            {
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") )
                    {
                        if (ClsGlobal.TestType == 2)
                        {
                            if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK")
                            {
                                lstVSAcirVal.Add(ClsGlobal.listETCELL[i].ACIR_Now);
                            }
                        }
                    }
                }
                double VSVal = 0;
                if (  lstVSAcirVal.Count != 0)
                {
                    if (ClsGlobal.ACIR_MinOrMedian == "Y")
                    {
                        VSVal = Math.Round(this.CalMedian(lstVSAcirVal), 4); 
                    }
                    else
                    {
                        VSVal = lstVSAcirVal.Min();
                    }
                }
               
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") )
                    {
                        if (ClsGlobal.TestType == 2)
                        {
                            if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK" && lstVSAcirVal.Count != 0)
                            {
                                ClsGlobal.listETCELL[i].ACIR_range = Math.Round(ClsGlobal.listETCELL[i].ACIR_Now - VSVal,4);     //获得ACIR对比值的极差
                                ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(2,ClsGlobal.listETCELL[i].ACIR_range);
                            }
                            else
                            {
                                ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(1, ClsGlobal.listETCELL[i].ACIR_range);
                            }
                        }
                    }
                    else
                    {
                        ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(0, ClsGlobal.listETCELL[i].ACIR_range);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       
        //计算压降极差
        public void CalDROPRange()
        {
            List<double> lstVSVal = new List<double>();
            try
            {
                if (ClsGlobal.IS_Enable_DropRange == "Y" && ClsGlobal.ENVoltDrop == "Y" && ClsGlobal.OCVType == 3)
                {
                    for (int i = 0; i < ClsGlobal.TrayType; i++)
                    {
                        if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") )
                        {
                            if (ClsGlobal.TestType > 0)
                            {
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK")
                                {
                                    lstVSVal.Add(ClsGlobal.listETCELL[i].VoltDrop_Now);
                                }
                            }
                            else
                            {
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK")
                                {
                                    lstVSVal.Add(ClsGlobal.listETCELL[i].VoltDrop_Now);
                                }
                            }
                        }
                    }

                    double VSVal = 0;
                    if (lstVSVal.Count != 0)
                    {
                        if (ClsGlobal.Drop_MinOrMedian == "Y")
                        {
                            VSVal = Math.Round(this.CalMedian(lstVSVal), 4); 

                        }
                        else
                        {
                            VSVal = lstVSVal.Min();
                        }
                    }

                    for (int i = 0; i < ClsGlobal.TrayType; i++)
                    {
                        if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") )
                        {
                            if (ClsGlobal.TestType > 0)
                            {
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK" && lstVSVal.Count != 0)
                                {
                                    ClsGlobal.listETCELL[i].DROP_range = Math.Round(ClsGlobal.listETCELL[i].VoltDrop_Now - VSVal,4);     //获得压降对比值的极差
                                    ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(2, ClsGlobal.listETCELL[i].DROP_range);
                                }
                                else
                                {
                                    ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(1, ClsGlobal.listETCELL[i].DROP_range);
                                }
                            }
                            else
                            {
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && lstVSVal.Count != 0)
                                {
                                    ClsGlobal.listETCELL[i].DROP_range = Math.Round(ClsGlobal.listETCELL[i].VoltDrop_Now - VSVal,4);     //获得压降对比值的极差
                                    ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(2, ClsGlobal.listETCELL[i].DROP_range);
                                }
                                else
                                {
                                    ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(1, ClsGlobal.listETCELL[i].DROP_range);
                                }
                            }
                        }
                        else
                        {
                            ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(0, ClsGlobal.listETCELL[i].DROP_range);
                        }

                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //中值计算
        public double CalMedian(List<double> lstVal )
        {
            double[] VSVal = lstVal.ToArray();
            Array.Sort(VSVal);
            double Median;
            int len = VSVal.Length;
            if (len % 2 == 0)
                Median = (VSVal[len / 2] + VSVal[len / 2 - 1]) / 2d;
            else
                Median = VSVal[len / 2];
            return Median;
        }
        //压降极差判断
        private NgResult CheckVDropRangeNG(int flag, double Val)
        {
            NgResult mNgResult;
            if (flag == 0)
            {
                mNgResult.NgType =1;
                mNgResult.NgCode = "C";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "无电池";
                return mNgResult;
            }
            else if (flag == 1)
            {
                mNgResult.NgType =1;
                mNgResult.NgCode = "N";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "测试不合格";
                return mNgResult;
            }
            
            mNgResult.NgType = 0;
            mNgResult.NgCode = "00";
            mNgResult.NgDescribe = "合格";
            mNgResult.NgState = "OK";

            if (Val > ClsGlobal.UpLMT_DropRange)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "JZ2";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "压降极差超上限";
                // return;
            }
            else if (Val < ClsGlobal.DownLMT_DropRange)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "JZ1";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "压降极差超下限";
                // return;
            }
            return mNgResult;
        }
    
        //ACIR极差判断
        private NgResult CheckACIRRangeNG(int flag, double Val)
        {
            //自放电异常判断
            NgResult mNgResult;
          
            if (flag == 0)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "C";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "无电池";
                return mNgResult;
            }
            else if (flag == 1)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "N";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "测试不合格";
                return mNgResult;
            }

            mNgResult.NgType = 0;
            mNgResult.NgCode = "00";
            mNgResult.NgDescribe = "合格";
            mNgResult.NgState = "OK";

            if (Val > ClsGlobal.UpLMT_ACIRRange)
            {
                mNgResult.NgType =1;
                mNgResult.NgCode = "AJ2";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "ACIR极差超上限";
                // return;
            }
            else if (Val < ClsGlobal.DownLMT_ACIRRange)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "AJ1";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "ACIR极差超下限";
                // return;
            }
            return mNgResult;

        }

        //壳压判断
        private void CheckSVNG()
        {
            NgResult mNgResult=new NgResult();         
            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                mNgResult.NgType = 0;
                mNgResult.NgCode = "00";
                mNgResult.NgDescribe = "合格";
                mNgResult.NgState = "OK";

                if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                {
                    if (ClsGlobal.G_dbl_VshellArr[i] > ClsGlobal.UpLmt_SV)
                    {
                        mNgResult.NgType = 1;
                        mNgResult.NgCode = "TX2";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳压超上限";
                        // return;
                    }
                    else if (ClsGlobal.G_dbl_VshellArr[i] < ClsGlobal.DownLmt_SV)
                    {
                        mNgResult.NgType = 1;
                        mNgResult.NgCode = "TX1";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳压超下限";
                        // return;
                    }
                }
                else
                {
                    mNgResult.NgType = 1;
                    mNgResult.NgCode = "C";
                    mNgResult.NgState = "NG";
                    mNgResult.NgDescribe = "无电池";
                }
                ClsGlobal.listETCELL[i].SV_NgResult = mNgResult;
            }
        }
        //压降、K值计算
        private void CalVoltDrop()
        {
            if (ClsGlobal.ENVoltDrop == "Y" && ClsGlobal.OCVType == 3)
            {
                if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                {

                    for (int i = 0; i < ClsGlobal.TrayType; i++)
                    {
                        Random RD = new Random(i);
                        ClsGlobal.listETCELL[i].OCV_1or2 = ClsGlobal.listETCELL[i].OCV_Now + 10 * RD.Next(8, 11) * 0.1;

                    }
                }

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                    {
                        double VoltToComp = ClsGlobal.listETCELL[i].OCV_1or2 - ClsGlobal.listETCELL[i].OCV_Now;
                        ClsGlobal.listETCELL[i].VoltDrop_Now = Math.Round(VoltToComp, 4);
                        ClsGlobal.listETCELL[i].K_Now = Math.Round((VoltToComp / ClsGlobal.GetHours), 6);

                        //if (ClsGlobal.listETCELL[i].HNGSt == "OK")
                        //{
                        //    double VoltToComp = ClsGlobal.listETCELL[i].OCV_1or2 - ClsGlobal.listETCELL[i].OCV_Now;
                        //    ClsGlobal.listETCELL[i].VoltDrop_Now = VoltToComp;
                        //    ClsGlobal.listETCELL[i].K_Now = Math.Round((VoltToComp / ClsGlobal.GetHours), 6);
                        //    //ClsGlobal.listETCELL[i].K_1_2 = ClsGlobal.listETCELL[i].K_Now;
                        //}
                        //else
                        //{
                        //    ClsGlobal.listETCELL[i].VoltDrop_Now = -1;
                        //    ClsGlobal.listETCELL[i].K_Now = 0;
                        //}
                    }

                }
            }
        }

        public void InitPara()
        {
            //ShowHalfVal = ClsGlobal.TrayType;                  //界面更新用     
            mForm.Invoke(new EventHandler(delegate
            {
                //界面处理->表格数据清空
                int Val = ClsGlobal.TrayType ;

                for (int i = 0; i < Val; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        this.mForm.dgvTest.Rows[i].Cells[j].Value = "";
                        this.mForm.dgvTest.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                        this.mForm.dgvTest.Rows[i].Cells["Col_OCV"].Style.ForeColor = Color.Black;
                    }
                }
                //this.mForm.dgvTest.Rows.Clear();

            }));
        }

        public void ShowCellid()
        {
            
            mForm.Invoke(new EventHandler(delegate
            {
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode") )
                    {
                        mForm.dgvTest.Rows[i].Cells["SFC"].Value = ClsGlobal.listETCELL[i].Cell_ID;
                    }
                   
                }

            }));
        }
     
        //停止
        public void StopManualTest()
        {
            mManualTestStop = true;
        }

        //停止校准/计量
        public void StopManualVoltZero_Meter()
        {
            mManualVoltZeroStop = true;
            mManualIRAdjustStop = true;
            mManualIRMeterStop = true;
        }

        //停止校准/计量
        public void StopManualIR_Adjust_Meter()
        {
            mManualVoltZeroStop = true;
            mManualIRAdjustStop = true;
            mManualIRMeterStop = true;
        }

        //手动测试正负极电压
        private void ManualTestVolt_PosNeg()
        {

            double theDMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            try
            {
                this.ClearDgv(1);
                mManualTestFinish = false;
                mManualTestStop = false;
                SwitchDev.ChannelVoltSwitch(1, 0);        //正极对负极

                #region 测电压

                Thread.Sleep(2);

                for (int Num = 0; Num < CHCount; Num++)
                {
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]); //通道选择

                    SwitchDev.ChannelVoltSwitch(1, ActualNum);        //正极对负极

                    Thread.Sleep(ClsGlobal.SleepTime);

                    if (ClsGlobal.EN_TestOCV == 1)
                    {
                        mDmm.ReadVolt(out theDMMVolt);
                    }
                    arrDMMVolt[Num] = theDMMVolt;

                   
                    if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                    {
                        ManualTest.Invoke(new EventHandler(delegate
                        {
                            arrDMMVolt[Num] = arrDMMVolt[Num] * 1000;
                            ManualTest.dgvManualTest.Rows[Num].Cells[1].Value = arrDMMVolt[Num].ToString("F4");   //刷新界面
                        }));
                    }
                    if (mManualTestStop == true)
                    {
                        break;
                    }
                }

                SwitchDev.ChannelVoltSwitch(1, 0);      //结束,通道全部关断
              
                #endregion

                mManualTestFinish = true;

            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }

        }

        //手动测试壳体与负极电压
        private void ManualTestVolt_ShellNeg()
        {

            double theDMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            int theCHCount = ClsGlobal.TrayType;

            try
            {
                this.ClearDgv(3);
                mManualTestFinish = false;
                mManualTestStop = false;

                #region 测壳体电压
 
                Thread.Sleep(2);
                for (int Num = 0; Num < theCHCount; Num++)
                {
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]); //通道选择

                    SwitchDev.ChannelVoltSwitchContr(2, ActualNum);        //壳体负极测量

                    Thread.Sleep(ClsGlobal.SleepTime);

                    if (ClsGlobal.EN_TestOCV == 1)
                    {
                        mDmm.ReadVolt(out theDMMVolt);
                    }
                    arrDMMVolt[Num] = theDMMVolt;
                    //数值显示
                    if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                    {
                        ManualTest.Invoke(new EventHandler(delegate
                        {
                            arrDMMVolt[Num] = arrDMMVolt[Num] * 1000;
                            ManualTest.dgvManualTest.Rows[Num].Cells[2].Value = arrDMMVolt[Num].ToString("F4");   //刷新界面
                        }));
                    }
                    if (mManualTestStop == true)
                    {
                        break;
                    }  

                }
                SwitchDev.ChannelVoltSwitchContr(2, 0);      //结束,通道全部关断

              
                #endregion

                mManualTestFinish = true;

            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }

        }

        //手动测试内阻
        private void ManualTestIR_BT4560()
        {
            double theIRSample = 0;
            
            //string theStrIMPVal = "0";    //内阻
            //string theStrXval = "0";      //电抗
            //string theStrVval = "0";      //电压

            double[] arrIMPVal = new double[ClsGlobal.TrayType];
            double[] arrXval = new double[ClsGlobal.TrayType];
            double[] arrVval = new double[ClsGlobal.TrayType];

            int theCHCount = ClsGlobal.TrayType;
            
            try
            {
                this.ClearDgv(3);
                mManualTestFinish = false;
                mManualTestStop = false;

                #region 测内阻 BT4560

                Thread.Sleep(2);

                for (int Num = 0; Num < theCHCount; Num++)
                {
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]); //通道选择

                    SwitchDev.ChannelAcirSwitchContr(2, ActualNum);     //内阻测量

                    Thread.Sleep(100);

                    if (ClsGlobal.EN_TestOCV == 1)
                    {
                        //mRTester_BT4.ReadRData(out theStrIMPVal);

                        mRTester_BT4.ReadRData(out theIRSample);
                    }

                    arrIMPVal[Num] = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[ActualNum - 1]);   //经过adjust

                    //arrIMPVal[i] = double.Parse(theStrIMPVal);
                    //arrXval[i] = double.Parse(theStrXval);
                    //arrVval[i] = double.Parse(theStrVval);
                    //数值显示
                    if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                    {
                        ManualTest.Invoke(new EventHandler(delegate
                        {
                            ManualTest.dgvManualTest.Rows[Num].Cells[3].Value = arrIMPVal[Num].ToString("F4");   //刷新界面
                          
                        }));
                    }

                    if (mManualTestStop == true)
                    {
                        break;
                    }  

                }
                SwitchDev.ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断

              
                #endregion

                mManualTestFinish = true;

            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }

        }

        public void ClearDgv(int index)
        {
          
            ManualTest.Invoke(new EventHandler(delegate
            {
                //界面处理->表格数据清空
                int Val = ClsGlobal.TrayType;

                for (int i = 0; i < Val; i++)
                {
                    this.ManualTest.dgvManualTest.Rows[i].Cells[index].Value = "";

                }
                //this.mForm.dgvTest.Rows.Clear();

            }));
        }
        //手动电压清0
        private void ManualVoltZero()
        {
            double theVoltSample = 0;
            double theAdjusVolt = 0;
            try
            {
                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelVoltZero.Enabled = false;
                    ManualAdjust.btnVoltZeroAllStart.Enabled = false;
                }));

                mManualVoltZeroFinish = false;
                mManualVoltZeroStop = false;

                //值清空
                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                    {
                        ManualAdjust.arrTxt_VoltZeroSampleShow[Num].Text = "";
                    }
                }));

                for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                {
                    if (mManualVoltZeroStop == true)
                    {
                        Thread.Sleep(50);
                        SwitchDev.ChannelVoltSwitch(1, 0);  
                        break;
                    }

                    //通道选择
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]);
                    SwitchDev.ChannelVoltSwitch(1, ActualNum);         //电压切换
                    Thread.Sleep(200);
                    mDmm.ReadVolt(out theVoltSample);       //电压值
                                                           
                    theAdjusVolt = 0 - theVoltSample * 1000; //计算得到校准值
                    //更改和保存校准值
                    INIAPI.INIWriteValue(ClsGlobal.mVoltAdjustPath, "VoltAdjust", "CH" + (ActualNum), theAdjusVolt.ToString("F4"));
                    ClsGlobal.ArrVoltAdjustPara[ActualNum - 1] = theAdjusVolt.ToString("F4");
                    //值显示
                    ManualAdjust.Invoke(new EventHandler(delegate
                    {
                        ManualAdjust.arrTxt_VoltZeroSampleShow[Num].Text = (theAdjusVolt).ToString("F4");
                    }));
                }
                mManualVoltZeroFinish = true;
            }
            catch (Exception ex)
            {
                mManualVoltZeroFinish = true;
                MessageBox.Show(ex.Message);
            }
        }

        //手动内阻校准界面的测试 ( 用短路块工装 )
        private void ManualIRAdjust_Test()
        {
            double theIRSample = 0;

            try
            {
                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelIRAdjust.Enabled = false;
                    ManualAdjust.btnIRAdjustSampleAllStart.Enabled = false;
                    ManualAdjust.btnIRAdjustAllStart.Enabled = false;
                    ManualAdjust.btnIRAdjustAllValClr.Enabled = false;
                }));

                mManualIRAdjustFinish = false;
                mManualIRAdjustStop = false;

                for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                {
                    if (mManualIRAdjustStop == true)
                    {
                        Thread.Sleep(50);
                        SwitchDev.ChannelAcirSwitchContr(2, 0);
                        break;
                    }
                                        
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]);
                    SwitchDev.ChannelAcirSwitchContr(2, ActualNum);             //内阻通道选择          
                    Thread.Sleep(300);
                    mRTester_BT4.ReadRData(out theIRSample);                //内阻采样

                    //显示阻值
                    ManualAdjust.Invoke(new EventHandler(delegate
                    {
                        if (theIRSample > 100)
                        {
                            ManualAdjust.arrTxt_IRSampleShow[Num].BackColor = Color.Red;
                        }
                        else
                        {
                            ManualAdjust.arrTxt_IRSampleShow[Num].BackColor = Color.LightGreen;
                        }

                        ManualAdjust.arrTxt_IRSampleShow[Num].Text = (theIRSample * 1000).ToString("F3") + "/" +
                        (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[ActualNum - 1])).ToString("F3");
                    }));
                }
                mManualIRAdjustFinish = true;
            }
            catch (Exception ex)
            {
                mManualIRAdjustFinish = true;
                MessageBox.Show(ex.Message);
            }

        }

        //手动内阻校准( 用短路块工装 )
        private void ManualIRAdjust()
        {
            double theBaseVal;
            double theAdjust;
            double theIRSample;
            
            try
            {
                //得到基准值
                if (double.TryParse(ManualAdjust.txtIRBase_Adjust.Text, out theBaseVal) == false)
                {
                    ManualAdjust.lblNote_IRAdjust.Text = "请先填入正确的内阻基准值，再进行校准";
                    return;
                }

                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelIRAdjust.Enabled = false;
                    ManualAdjust.btnIRAdjustSampleAllStart.Enabled = false;
                    ManualAdjust.btnIRAdjustAllStart.Enabled = false;
                    ManualAdjust.btnIRAdjustAllValClr.Enabled = false;
                }));
 
                mManualIRAdjustFinish = false;
                mManualIRAdjustStop = false;

                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                    {
                        ManualAdjust.arrTxt_IRSampleShow[Num].Text = "";
                    }
                }));

                
                for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                {
                    if (mManualIRAdjustStop == true)
                    {
                        Thread.Sleep(50);
                        SwitchDev.ChannelAcirSwitchContr(2, 0);
                        break;
                    }

                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]);     //内阻通道选择
                    SwitchDev.ChannelAcirSwitchContr(2, ActualNum);
                    Thread.Sleep(300);
                    mRTester_BT4.ReadRData(out theIRSample);        //内阻采样


                    if (theIRSample * 1000 > 100)
                    {
                        ManualAdjust.Invoke(new EventHandler(delegate
                        {
                            ManualAdjust.lblNote_IRAdjust.Text = ("[通道" + (Num + 1) + "]:\r\n" + "测得阻值>100mΩ,校准失败!");
                        }));
                        break;
                    }
                    
                    //计算得到校准值
                    theAdjust = theBaseVal - theIRSample * 1000;

                    //更改和保存校准值
                    INIAPI.INIWriteValue(ClsGlobal.mIRAdjustPath, "ACIRAdjust", "CH" + (ActualNum), theAdjust.ToString("F3"));
                    ClsGlobal.mIRAdjustVal[ActualNum - 1] = theAdjust.ToString();

                    //显示阻值
                    if (ManualAdjust.IsHandleCreated == true && mManualIRAdjustStop == false)
                    {
                        ManualAdjust.Invoke(new EventHandler(delegate
                        {
                            ManualAdjust.arrTxt_IRSampleShow[Num].BackColor = Color.LightGreen;
                            ManualAdjust.arrTxt_IRSampleShow[Num].Text = (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[ActualNum - 1])).ToString("F3");

                        }));
                    }
                }

                mManualIRAdjustFinish = true;
            }
            catch (Exception ex)
            {
                mManualIRAdjustFinish = true;
                MessageBox.Show(ex.Message);
            }
        }

        //手动内阻计量( 用标准电阻工装)
        private void ManualIRMetering()
        {
            double theMeterVal = 0;
            double MeterErrRange = 0;
            double theIRSample;
            double theIR;       //毫欧单位

            try
            {
                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    //已输入计量值
                    if (double.TryParse(ManualAdjust.txtIRSet_Metering.Text, out theMeterVal) == false)
                    {
                        ManualAdjust.lblNote_IRMetering.Text = ("请先填入正确的计量值，再进行计量");
                        return;
                    }
                    
                    if (double.TryParse(ManualAdjust.txtIRMeterErrRange.Text, out MeterErrRange) == false)
                    {
                        ManualAdjust.lblNote_IRMetering.Text = ("请先填入正确的计量误差值，再进行计量");
                        return;
                    }
                }));

                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelIRMetering.Enabled = false;
                    ManualAdjust.btnIRMeteringAllStart.Enabled = false;
                }));

                mManualIRMeterFinish = false;
                mManualIRMeterStop = false;

                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                    {
                        ManualAdjust.arrTxt_IRMeterShow[Num].Text = "";
                    }
                }));

                
                for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
                {
                    if (mManualIRMeterStop == true)
                    {
                        Thread.Sleep(50);
                        SwitchDev.ChannelAcirSwitchContr(2, 0);
                        break;
                    }

                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[Num]);
                    SwitchDev.ChannelAcirSwitchContr(2, ActualNum);     //切换内阻通道
                    Thread.Sleep(500);
                    mRTester_BT4.ReadRData(out theIRSample);            //内阻采样

                    theIR = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[ActualNum - 1]);  //经过adjust

                    //计量测试
                    ManualAdjust.Invoke(new EventHandler(delegate
                    {
                        ManualAdjust.arrTxt_IRMeterShow[Num].Text = theIR.ToString("F3");
                        if (Math.Abs(theMeterVal - theIR) <= MeterErrRange)
                        {
                            ManualAdjust.arrTxt_IRMeterShow[Num].BackColor = Color.LightGreen;
                            ManualAdjust.mToolTip_IRMeter.SetToolTip(ManualAdjust.arrTxt_IRMeterShow[Num], "计量通过");
                            ManualAdjust.arrLbl_IRMeterJudge[Num].Text = "OK";
                        }
                        else
                        {
                            ManualAdjust.arrTxt_IRMeterShow[Num].BackColor = Color.Red;
                            ManualAdjust.mToolTip_IRMeter.SetToolTip(ManualAdjust.arrTxt_IRMeterShow[Num], "计量不通过");
                            ManualAdjust.arrLbl_IRMeterJudge[Num].Text = "NG";
                        }
                    }));
                }
                mManualIRMeterFinish = true;
            }
            catch (Exception ex)
            {
                mManualIRMeterFinish = true;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
