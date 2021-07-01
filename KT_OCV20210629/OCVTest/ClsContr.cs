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
    public class ClsOCVContr
    {
        FrmSys mForm;
        FrmManualTest ManualTest;
        FrmManualAdjust ManualAdjust;
        Thread ThreadTestAction;
        public delegate void InfoSend(string Info);             //委托处理流程记录
        public InfoSend mInfoSend;

        public ClsSWControl SWControl;       //切换控制
        public ClsDMM_Ag344X DMM_Ag344X;     //万用表
        public ClsHIOKI4560 HIOKI4560;       //内阻仪BT4560控制
        public ClsHIOKI365X HIOKI365X;       //内阻仪BT356x控制

        //测试参数
        private int ShowHalfVal = ClsGlobal.TrayType / 2;                     //界面更新用

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
        public Action FuncManualTestProcess(ClsProcess autoProcess, string name)
        {
            return () =>
            {
                autoProcess.TrayInState(4);
                autoProcess.TrayInState(5);
                autoProcess.TrayInState(6);
                autoProcess.TrayInState(8);
                autoProcess.TestWorkState(6);
                autoProcess.SetTempFinish(true);
                //autoProcess.TestWorkState(6);
                autoProcess.mInfoSend("测试完成！");
                autoProcess.mInfoSend("保存信息中，请稍等！......");
                autoProcess.TestWorkState(9);
                autoProcess.mInfoSend("信息保存完毕......");
            };
        }

        public ClsOCVContr(ClsSWControl swControl, ClsDMM_Ag344X dmm_Ag344X, InfoSend infoSend, FrmSys Fm)
        {
            this.mInfoSend = infoSend;
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.mForm = Fm;
        }

        public ClsOCVContr(ClsSWControl swControl, ClsDMM_Ag344X dmm_Ag344X, ClsHIOKI365X hioki365X, InfoSend infoSend, FrmSys Fm)
        {
            this.mInfoSend = infoSend;
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.HIOKI365X = hioki365X;
            this.mForm = Fm;
        }
        public ClsOCVContr(ClsSWControl swControl, ClsDMM_Ag344X dmm_Ag344X, ClsHIOKI4560 hioki4560, InfoSend infoSend, FrmSys Fm)
        {
            this.mInfoSend = infoSend;
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.HIOKI4560 = hioki4560;
            this.mForm = Fm;
        }

        //测试流程
        public void StartTestAction()
        {
            try
            {
                mAutoTestStop = false;
                mAutoTestFinish = false;
                //ThreadTestAction = new Thread(new ThreadStart(TestProcess));
                //ThreadTestAction.IsBackground = true;
                //ThreadTestAction.Start();
                TestProcess();
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
            ThreadTestAction = new Thread(new ParameterizedThreadStart(ManualTestVolt));
            ThreadTestAction.Start(0);
        }

        //手动测试壳体电压
        public void StartManualTestVolt_ShellNeg_Action(FrmManualTest MuTest)
        {
            ManualTest = MuTest;
            ThreadTestAction = new Thread(new ParameterizedThreadStart(ManualTestVolt));
            ThreadTestAction.Start(1);
        }

        //手动测试内阻
        public void StartManualTestIR_BT4560_Action(FrmManualTest MuTest)
        {
            ManualTest = MuTest;
            ThreadTestAction = new Thread(new ParameterizedThreadStart(ManualTestVolt));
            ThreadTestAction.Start(1);
        }

        ////手动电压清零
        //public void StartManualVoltZero_Action(FrmManualAdjust MauAdjust)
        //{
        //    ManualAdjust = MauAdjust;
        //    ThreadTestAction = new Thread(new ThreadStart(ManualVoltZero));
        //    ThreadTestAction.Start();
        //}

        ////手动内阻测试(真实值和已校准的内阻值)
        //public void StartManualIRAdjust_Test_Action(FrmManualAdjust MauAdjust)
        //{
        //    ManualAdjust = MauAdjust;
        //    ThreadTestAction = new Thread(new ThreadStart(ManualIRAdjust_Test));
        //    ThreadTestAction.Start();
        //}


        ////手动内阻校准
        //public void StartManualIRAdjust_Action(FrmManualAdjust MauAdjust)
        //{
        //    ManualAdjust = MauAdjust;
        //    ThreadTestAction = new Thread(new ThreadStart(ManualIRAdjust));
        //    ThreadTestAction.Start();
        //}

        ////手动内阻计量
        //public void StartManualIRMetering_Action(FrmManualAdjust MauAdjust)
        //{
        //    ManualAdjust = MauAdjust;
        //    ThreadTestAction = new Thread(new ThreadStart(ManualIRMetering));
        //    ThreadTestAction.Start();
        //}

        private int mStep_TestReq;
        DateTime TimeTestReqThread;
        TimeSpan TsTestReqThread;                        //计时
        //测试流程
        private void TestProcess()
        {
            try
            {
                mAutoTestFinish = false;
                mAutoTestStop = false;
                mStep_TestReq = 1;
                short pos = 0;
                for (int i = 0; i < 100; i++) //ClsGlobal.TestType / 2
                {
                    switch (mStep_TestReq)
                    {
                        case 1:
                            ClsGlobal.mPLCContr.Set_CylBlock_Up();
                            mInfoSend("PC指示探针松开");
                            TimeTestReqThread = System.DateTime.Now;
                            mStep_TestReq = 2;
                            break;
                        case 2:
                            if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen == 1 && ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose == 0)
                            {
                                if (pos >= 8)
                                {
                                    mStep_TestReq = 7;
                                }
                                else
                                {
                                    pos = (short)(i + 1);
                                    ClsGlobal.mPLCContr.DevMove_AbsNO(0, pos);
                                    mInfoSend("PC指示X轴移动至位置：" + pos);
                                    mStep_TestReq = 3;
                                    Thread.Sleep(500);
                                    TimeTestReqThread = DateTime.Now;
                                }

                            }
                            TsTestReqThread = DateTime.Now - TimeTestReqThread;
                            if (TsTestReqThread.TotalSeconds > 5)
                            {
                                mInfoSend("探针气缸松开异常...");
                                throw new Exception("探针气缸松开异常...");
                            }
                            break;
                        case 3:
                            if (ClsGlobal.mPLCContr.GetState_MoveInPlace(pos))
                            {
                                mInfoSend("X轴移动到位置：" + pos + "到位");
                                TimeTestReqThread = DateTime.Now;
                                mStep_TestReq = 4;
                            }
                            TsTestReqThread = DateTime.Now - TimeTestReqThread;
                            if (TsTestReqThread.TotalSeconds > 5)
                            {
                                mInfoSend("X轴动作超时...");
                                throw new Exception("X轴动作超时...");
                            }
                            break;
                        case 4:
                            ClsGlobal.mPLCContr.Set_CylBlock_Down();
                            mInfoSend("PC指示压合探针");
                            TimeTestReqThread = DateTime.Now;
                            mStep_TestReq = 5;
                            break;
                        case 5:
                            if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen == 0 && ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose == 1)
                            {
                                mInfoSend("开始测试位置：" + pos + "...");
                                mStep_TestReq = 6;
                                Thread.Sleep(500);
                                TimeTestReqThread = DateTime.Now;
                            }
                            TsTestReqThread = DateTime.Now - TimeTestReqThread;
                            if (TsTestReqThread.TotalSeconds > 5)
                            {
                                mInfoSend("探针气缸压合异常...");
                                throw new Exception("探针气缸压合异常...");
                            }
                            break;
                        case 6:
                            switch (ClsGlobal.TestType)   //电压:0   电压壳压:1   电压 壳压 内阻 2
                            {
                                case 0:
                                    #region 测正负极 
                                    this.TestVoltForProc(pos);
                                    #endregion
                                    #region 测试温度
                                    TestTemp(pos);
                                    #endregion
                                    this.CalVoltDrop();
                                    this.NGStateOutput();
                                    break;
                                case 1:
                                    #region 测正负极 
                                    this.TestVoltForProc(pos);
                                    #endregion
                                    #region 测壳体电压
                                    this.TestShellVoltForProc(pos);
                                    #endregion
                                    #region 测试温度
                                    TestTemp(pos);
                                    #endregion
                                    this.CalVoltDrop();
                                    this.NGStateOutput();
                                    this.CheckSVNG();
                                    break;
                                case 2:
                                    #region 测正负极
                                    this.TestVoltForProc(pos);
                                    #endregion
                                    #region 测壳体电压
                                    this.TestShellVoltForProc(pos);
                                    #endregion
                                    #region 测内阻
                                    this.TestACIRForProc(pos);
                                    #endregion
                                    #region 测试温度
                                    TestTemp(pos);
                                    #endregion
                                    this.CalVoltDrop();
                                    this.NGStateOutput();
                                    this.CheckSVNG();
                                    break;
                                case 3:
                                    #region 测正负极
                                    this.TestVoltForProc(pos);
                                    #endregion
                                    #region 测壳体电压
                                    this.TestShellVoltForProc(pos);
                                    #endregion
                                    #region 测内阻
                                    this.TestACIRForProc(pos);
                                    #endregion
                                    #region 测试温度
                                    TestTemp(pos);
                                    #endregion
                                    this.CalVoltDrop();
                                    this.NGStateOutput();
                                    this.CheckSVNG();
                                    this.CalDROPRange();
                                    break;

                                    //#region 测正负极 
                                    //this.TestVoltForProc(pos);
                                    //#endregion

                                    //#region 测壳体电压
                                    ////TestShellVoltForProc_Postive();
                                    //this.TestShellVoltForProc(pos);
                                    //#endregion

                                    //#region 测内阻 
                                    //this.TestACIRForProc(pos);
                                    //#endregion
                                    //#region 测试温度
                                    //TestTemp(pos);
                                    //this.CalVoltDrop();
                                    //this.NGStateOutput();
                                    //this.CheckSVNG();
                                    ////this.CheckSVNG_Postive();
                                    //this.CalDROPRange();
                                //if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                                    //{
                                    //    this.CalACIRRange();
                                    //}
                                    //break;
                                default:
                                    break;
                            }
                            mInfoSend("位置：" + pos + "测试完成");
                            mStep_TestReq = 1;
                            break;
                        case 7:
                            break;
                        default:
                            mStep_TestReq = 0;
                            break;
                    }
                }
                this.CalDROPRange();
                if (ClsGlobal.IS_Enable_ACIRRange == "Y" && ClsGlobal.TestType == 2)
                {
                    this.CalACIRRange();
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
                                //mForm.dgvTest.Rows[i].Cells["Col_TEMP_P"].Value = ClsGlobal.listETCELL[i].PostiveTMP;
                                //mForm.dgvTest.Rows[i].Cells["Col_TEMP_N"].Value = ClsGlobal.listETCELL[i].NegativeTMP;
                            }
                        }
                    }
                    ));
                }
                #endregion

                #region  测试结束，判断是否复测

                ////测试次数判断
                //ClsGlobal.TestCount++;
                ////ACIR有异常测试数据,重测
                //if (ClsGlobal.lstACIRErrNo.Count > ClsGlobal.SetEnOCV && ClsGlobal.TestCount < ClsGlobal.MaxTestNum)
                //{
                //    ClsGlobal.OCV_TestState = eTestState.TestAgain;
                //    //用大量程                  
                //}
                //else
                //{
                //    ClsGlobal.OCV_TestState = eTestState.TestOK;            //测试成功
                //    ClsGlobal.lstACIRErrNo.Clear();////清空数据      
                //    ClsGlobal.TestCount = 0;
                //}
                #endregion
                ClsGlobal.OCV_TestState = eTestState.TestOK;            //测试成功
                mAutoTestFinish = true;//该次测试完成 
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
            }
        }
        //流程中测试电池 (电压)
        private void TestVoltForProc(short pos)
        {
            try
            {
                double theDMMVolt = 0;
                int iSW;
                for (int i = 1; i <= 2; i++)
                {
                    if (mAutoTestStop == true)
                    {
                        this.SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    this.SWControl.ChannelVoltSwitch(1, i);  //单通道测电压
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    DMM_Ag344X.ReadVolt(out theDMMVolt);
                    int index = 2 * (pos - 1) + i;
                    iSW = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道
                    if (Math.Abs(theDMMVolt) < 1e+6)
                    {
                        ClsGlobal.listETCELL[iSW].OCV_Now = Math.Round(theDMMVolt * 1000, 4);
                    }
                    else
                    {
                        ClsGlobal.listETCELL[iSW].OCV_Now = 99999;
                    }
                    #region 显示数据
                    if (mForm.IsHandleCreated == true)
                    {
                        mForm.Invoke(new EventHandler(delegate
                        {
                            mForm.dgvTest.Rows[iSW].Cells["Col_OCV"].Value = ClsGlobal.listETCELL[iSW].OCV_Now.ToString("F4");
                        }
                        ));
                    }
                    #endregion
                }
                this.SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断   
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //流程中测试电池 (内阻)   
        private void TestACIRForProc(short pos)
        {
            try
            {
                int iSW;
                double theIRAcir = 0;
                double theIRSample = 0;
                for (int i = 1; i <= 2; i++)
                {
                    if (mAutoTestStop == true)
                    {
                        this.SWControl.ChannelVoltIRSwitchContr(2, 0, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    this.SWControl.ChannelVoltIRSwitchContr(2, 1, i);  //单通道测内阻
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    this.HIOKI365X.ReadData(out theIRSample);     //获取内阻结果

                    int index = 2 * (pos - 1) + i;
                    iSW = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道
                    theIRAcir = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[iSW - 1]);   //经过adjust
                    //内阻数据
                    if (0 < theIRAcir && theIRAcir < ClsGlobal.ReTestLmt_ACIR)
                    {
                        //内阻数据
                        ClsGlobal.listETCELL[iSW].ACIR_Now = Math.Round(theIRAcir, 4);
                    }
                    else
                    {
                        ClsGlobal.listETCELL[iSW].ACIR_Now = 9999;
                    }

                    #region 显示数据
                    if (mForm.IsHandleCreated == true)
                    {
                        mForm.Invoke(new EventHandler(delegate
                        {
                            mForm.dgvTest.Rows[iSW].Cells["Col_ACIR"].Value = ClsGlobal.listETCELL[iSW].ACIR_Now.ToString("F4");
                        }
                        ));
                    }
                    #endregion
                }

                this.SWControl.ChannelVoltIRSwitchContr(2, 0, 0);      //结束,通道全部关断   
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //流程中测试电池 (壳压)
        private void TestShellVoltForProc(short pos)
        {
            try
            {
                double theDMMVolt = 0;
                int iSW;
                for (int i = 1; i <= 2; i++)
                {
                    if (mAutoTestStop == true)
                    {
                        this.SWControl.ChannelVoltSwitch(2, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    this.SWControl.ChannelVoltSwitch(2, i);  //单通道测电压
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    DMM_Ag344X.ReadVolt(out theDMMVolt);
                    int index = 2 * (pos - 1) + i;
                    iSW = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道
                    if (Math.Abs(theDMMVolt) < 1e+6)
                    {
                        ClsGlobal.listETCELL[iSW].OCV_Shell_Now = Math.Round(theDMMVolt * 1000, 4);
                    }
                    else
                    {
                        ClsGlobal.listETCELL[iSW].OCV_Shell_Now = 99999;
                    }
                    #region 显示数据
                    if (mForm.IsHandleCreated == true)
                    {
                        mForm.Invoke(new EventHandler(delegate
                        {
                            mForm.dgvTest.Rows[iSW].Cells["Col_CaseOCV"].Value = ClsGlobal.listETCELL[iSW].OCV_Shell_Now.ToString("F4");
                        }
                        ));
                    }
                    #endregion
                }
                this.SWControl.ChannelVoltSwitch(2, 0);      //结束,通道全部关断   
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TestTemp(short pos)
        {
            try
            {
                //调试开关 温度
                int EN_TEMP = 1;
                //调试开关
                if (EN_TEMP == 1)
                {
                    //ClsGlobal.TempContr.GetTemp(out ArrChannelTemp);
                }
                for (int i = 0; i < 2; i++)
                {
                    int index = 2 * (pos - 1) + i;
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道
                    //正极温度
                    ClsGlobal.G_dbl_P_TempArr[i] = ClsGlobal.TempContr.Anodetemperature[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);
                    //负极温度
                    ClsGlobal.G_dbl_N_TempArr[i] = ClsGlobal.TempContr.Poletemperature[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);
                    ClsGlobal.listETCELL[ActualNum].PostiveTMP = ClsGlobal.G_dbl_P_TempArr[ActualNum];
                    ClsGlobal.listETCELL[ActualNum].NegativeTMP = ClsGlobal.G_dbl_N_TempArr[ActualNum];
                    if (mForm.IsHandleCreated == true)
                    {
                        mForm.Invoke(new EventHandler(delegate
                        {
                            mForm.dgvTest.Rows[i].Cells["Col_TEMP_P"].Value = ClsGlobal.listETCELL[ActualNum].PostiveTMP;
                            //mForm.dgvTest.Rows[i].Cells["Col_TEMP_N"].Value = ClsGlobal.listETCELL[i].NegativeTMP;
                        }
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                //测试温度异常   
                ClsGlobal.OCV_TestState = eTestState.ErrTempTest;
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
                        if (ClsGlobal.listETCELL[i].OCV_Now > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].OCV_Now < ClsGlobal.DownLmt_V)
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

                    }
                    else if (ClsGlobal.TestType == 1)
                    {
                        //电压
                        if (ClsGlobal.listETCELL[i].OCV_Now > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].OCV_Now < ClsGlobal.DownLmt_V)
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
                        if (ClsGlobal.listETCELL[i].OCV_Now > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].OCV_Now < ClsGlobal.DownLmt_V)
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

                        else if (ClsGlobal.listETCELL[i].ACIR_Now > ClsGlobal.UpLmt_ACIR)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = "AX2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "ACIR超上限";
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].ACIR_Now < ClsGlobal.DownLmt_ACIR)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = "AX1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "ACIR超下限";
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
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
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
                if (lstVSAcirVal.Count != 0)
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
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                    {
                        if (ClsGlobal.TestType == 2)
                        {
                            if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK" && lstVSAcirVal.Count != 0)
                            {
                                ClsGlobal.listETCELL[i].ACIR_range = Math.Round(ClsGlobal.listETCELL[i].ACIR_Now - VSVal, 4);     //获得ACIR对比值的极差
                                ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(2, ClsGlobal.listETCELL[i].ACIR_range);
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
                        if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
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
                        if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                        {
                            if (ClsGlobal.TestType > 0)
                            {
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK" && lstVSVal.Count != 0)
                                {
                                    ClsGlobal.listETCELL[i].DROP_range = Math.Round(ClsGlobal.listETCELL[i].VoltDrop_Now - VSVal, 4);     //获得压降对比值的极差
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
                                    ClsGlobal.listETCELL[i].DROP_range = Math.Round(ClsGlobal.listETCELL[i].VoltDrop_Now - VSVal, 4);     //获得压降对比值的极差
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
        public double CalMedian(List<double> lstVal)
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
                mNgResult.NgType = 1;
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
            NgResult mNgResult = new NgResult();
            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                mNgResult.NgType = 0;
                mNgResult.NgCode = "00";
                mNgResult.NgDescribe = "合格";
                mNgResult.NgState = "OK";

                if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
                {
                    if (ClsGlobal.listETCELL[i].OCV_Shell_Now > ClsGlobal.UpLmt_SV)
                    {
                        mNgResult.NgType = 1;
                        mNgResult.NgCode = "TX2";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳压超上限";
                        // return;
                    }
                    else if (ClsGlobal.listETCELL[i].OCV_Shell_Now < ClsGlobal.DownLmt_SV)
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
                int Val = ClsGlobal.TrayType;

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
                    if (!ClsGlobal.listETCELL[i].Cell_ID.Contains("NullCellCode"))
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
        private void ManualTestVolt(object mTestType)
        {
            try
            {
                int TestType = int.Parse(mTestType.ToString());
                mStep_TestReq = 1;
                short pos = 0;
                for (int i = 0; i < 100; i++)//ClsGlobal.TestType / 2
                {
                    switch (mStep_TestReq)
                    {
                        case 1:
                            ClsGlobal.mPLCContr.Set_CylBlock_Up();
                            ClsGlobal.ManualMessInfo = "PC指示探针松开";
                            TimeTestReqThread = System.DateTime.Now;
                            mStep_TestReq = 2;
                            break;
                        case 2:
                            if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen == 1 && ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose == 0)
                            {
                                if (pos >= 8)
                                {
                                    mStep_TestReq = 7;
                                }
                                else
                                {
                                    pos = (short)(i + 1);
                                    ClsGlobal.mPLCContr.DevMove_AbsNO(0, pos);
                                    ClsGlobal.ManualMessInfo = "PC指示X轴移动至位置：" + pos;
                                    mStep_TestReq = 3;
                                    Thread.Sleep(500);
                                    TimeTestReqThread = DateTime.Now;
                                }

                            }
                            TsTestReqThread = DateTime.Now - TimeTestReqThread;
                            if (TsTestReqThread.TotalSeconds > 5)
                            {
                                ClsGlobal.ManualMessInfo = "探针气缸松开异常...";
                                throw new Exception("探针气缸松开异常...");
                            }
                            break;
                        case 3:
                            if (ClsGlobal.mPLCContr.GetState_MoveInPlace(pos))
                            {
                                ClsGlobal.ManualMessInfo = "X轴移动到位置：" + pos + "到位";
                                TimeTestReqThread = DateTime.Now;
                                mStep_TestReq = 4;
                            }
                            TsTestReqThread = DateTime.Now - TimeTestReqThread;
                            if (TsTestReqThread.TotalSeconds > 5)
                            {
                                ClsGlobal.ManualMessInfo = "X轴动作超时...";
                                throw new Exception("X轴动作超时...");
                            }
                            break;
                        case 4:
                            ClsGlobal.mPLCContr.Set_CylBlock_Down();
                            ClsGlobal.ManualMessInfo = "PC指示压合探针";
                            TimeTestReqThread = DateTime.Now;
                            mStep_TestReq = 5;
                            break;
                        case 5:
                            if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen == 0 && ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose == 1)
                            {
                                ClsGlobal.ManualMessInfo = "开始测试位置：" + pos + "...";
                                mStep_TestReq = 6;
                                Thread.Sleep(500);
                                TimeTestReqThread = DateTime.Now;
                            }
                            TsTestReqThread = DateTime.Now - TimeTestReqThread;
                            if (TsTestReqThread.TotalSeconds > 5)
                            {
                                ClsGlobal.ManualMessInfo = "探针气缸压合异常...";
                                throw new Exception("探针气缸压合异常...");
                            }
                            break;
                        case 6:

                            switch (TestType)   //电压:0   壳压:1   内阻 2
                            {
                                case 0:
                                    #region 测正负极 
                                    this.ManualTestVolt_PosNeg(pos);
                                    #endregion
                                    break;
                                case 1:

                                    #region 测正负极 
                                    // this.TestVoltForProc(pos);
                                    #endregion
                                    #region 测壳体电压
                                    this.ManualTestVolt_ShellNeg(pos);
                                    #endregion
                                    break;
                                case 2:
                                    #region 测正负极
                                    //this.TestVoltForProc(pos);
                                    #endregion

                                    #region 测壳体电压
                                    // this.TestShellVoltForProc(pos);
                                    #endregion

                                    #region 测内阻
                                    this.ManualTestIR(pos);
                                    #endregion
                                    break;
                                default:
                                    break;
                            }
                            ClsGlobal.ManualMessInfo = "位置：" + pos + "测试完成";
                            mStep_TestReq = 1;
                            break;
                        case 7:
                            break;
                        default:
                            mStep_TestReq = 0;
                            break;
                    }
                }
                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;

            }
        }

        //手动测试正负极电压
        private void ManualTestVolt_PosNeg(short pos)
        {
            try
            {
                this.ClearDgv(1);
                mManualTestFinish = false;
                mManualTestStop = false;
                SWControl.ChannelVoltSwitch(1, 0);        //正极对负极

                double theDMMVolt = 0;
                int iSW;
                for (int i = 1; i <= 2; i++)
                {
                    if (mManualTestStop == true)
                    {
                        this.SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    this.SWControl.ChannelVoltSwitch(1, i);  //单通道测电压

                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    DMM_Ag344X.ReadVolt(out theDMMVolt);
                    int index = 2 * (pos - 1) + i;
                    iSW = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道
                    if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                    {
                        ManualTest.Invoke(new EventHandler(delegate
                        {
                            ManualTest.dgvManualTest.Rows[iSW].Cells[1].Value = Math.Round(theDMMVolt * 1000, 4).ToString("F4");   //刷新界面
                        }));
                    }
                }
                this.SWControl.ChannelVoltSwitch(1, 0);      //结束,通道全部关断   

                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //手动测试壳体与负极电压
        private void ManualTestVolt_ShellNeg(short pos)
        {
            try
            {
                this.ClearDgv(2);
                mManualTestFinish = false;
                mManualTestStop = false;
                SWControl.ChannelVoltSwitch(2, 0);        //正极对负极

                double theDMMVolt = 0;
                int iSW;
                for (int i = 1; i <= 2; i++)
                {
                    if (mManualTestStop == true)
                    {
                        this.SWControl.ChannelVoltSwitch(2, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    this.SWControl.ChannelVoltSwitch(2, i);  //单通道测电压

                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    DMM_Ag344X.ReadVolt(out theDMMVolt);
                    int index = 2 * (pos - 1) + i;
                    iSW = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道
                    if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                    {
                        ManualTest.Invoke(new EventHandler(delegate
                        {
                            ManualTest.dgvManualTest.Rows[iSW].Cells[2].Value = Math.Round(theDMMVolt * 1000, 4).ToString("F4");   //刷新界面
                        }));
                    }
                }
                this.SWControl.ChannelVoltSwitch(2, 0);      //结束,通道全部关断   

                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //手动测试内阻
        private void ManualTestIR(short pos)
        {
            try
            {
                int iSW;
                double theIRAcir = 0;
                double theIRSample = 0;

                this.ClearDgv(3);
                mManualTestFinish = false;
                mManualTestStop = false;
                for (int i = 1; i <= 2; i++)
                {
                    if (mAutoTestStop == true)
                    {
                        this.SWControl.ChannelVoltIRSwitchContr(2, 0, 0);      //结束,通道全部关断   
                        throw new Exception("测试被终止");
                    }
                    this.SWControl.ChannelVoltIRSwitchContr(2, 1, i);  //单通道测内阻
                    Thread.Sleep(ClsGlobal.SWDelayTime);
                    this.HIOKI365X.ReadData(out theIRSample);     //获取内阻结果
                    int index = 2 * (pos - 1) + i;
                    iSW = Convert.ToUInt16(ClsGlobal.mSwitchCH[index]);    //转换为真实对应的通道                                                      //内阻数据
                    theIRAcir = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[iSW - 1]);   //经过adjust
                    if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                    {
                        ManualTest.Invoke(new EventHandler(delegate
                        {
                            ManualTest.dgvManualTest.Rows[iSW].Cells[3].Value = Math.Round(theIRAcir, 4).ToString("F4");   //刷新界面
                        }));
                    }
                }

                this.SWControl.ChannelVoltIRSwitchContr(2, 0, 0);      //结束,通道全部关断   
                mManualTestFinish = true;
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
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

        ////手动电压清0
        //private void ManualVoltZero()
        //{
        //    int theCHCount = ClsGlobal.TrayType / 2;
        //    double theVoltSample = 0;
        //    double theAdjusVolt = 0;
        //    try
        //    {
        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            ManualAdjust.panelVoltZero.Enabled = false;
        //            ManualAdjust.btnVoltZeroAllStart.Enabled = false;
        //        }));

        //        mManualVoltZeroFinish = false;
        //        mManualVoltZeroStop = false;

        //        //值清空
        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
        //            {
        //                ManualAdjust.arrTxt_VoltZeroSampleShow[Num].Text = "";
        //            }
        //        }));
        //        for (int i = 0; i < this.SWCount; i++)
        //        {
        //            for (int Num = 0; Num < theCHCount; Num++)
        //            {
        //                if (mManualVoltZeroStop == true)
        //                {
        //                    Thread.Sleep(50);
        //                    SWControl[i].ChannelVoltSwitch(1, 0);
        //                    break;
        //                }

        //                SWControl[i].ChannelVoltSwitch(1, Num + 1);         //电压切换
        //                Thread.Sleep(200);
        //                DMM_Ag344X.ReadVolt(out theVoltSample);       //电压值

        //                theAdjusVolt = 0 - theVoltSample * 1000; //计算得到校准值
        //                                                         //更改和保存校准值
        //                INIAPI.INIWriteValue(ClsGlobal.mVoltAdjustPath, "VoltAdjust", "CH" + (Num + 1 + i * 19), theAdjusVolt.ToString("F4"));
        //                ClsGlobal.ArrVoltAdjustPara[Num - 1] = theAdjusVolt.ToString("F4");
        //                //值显示
        //                ManualAdjust.Invoke(new EventHandler(delegate
        //                {
        //                    ManualAdjust.arrTxt_VoltZeroSampleShow[Num].Text = (theAdjusVolt).ToString("F4");
        //                }));
        //            }
        //        }

        //        mManualVoltZeroFinish = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mManualVoltZeroFinish = true;
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        ////手动内阻校准界面的测试 ( 用短路块工装 )
        //private void ManualIRAdjust_Test()
        //{
        //    double theIRSample = 0;

        //    int theCHCount = ClsGlobal.TrayType / 2;

        //    try
        //    {
        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            ManualAdjust.panelIRAdjust.Enabled = false;
        //            ManualAdjust.btnIRAdjustSampleAllStart.Enabled = false;
        //            ManualAdjust.btnIRAdjustAllStart.Enabled = false;
        //            ManualAdjust.btnIRAdjustAllValClr.Enabled = false;
        //        }));

        //        mManualIRAdjustFinish = false;
        //        mManualIRAdjustStop = false;
        //        for (int i = 0; i < this.SWCount; i++)
        //        {
        //            for (int Num = 0; Num < theCHCount; Num++)
        //            {
        //                if (mManualIRAdjustStop == true)
        //                {
        //                    Thread.Sleep(50);
        //                    SWControl[i].ChannelAcirSwitchContr(2, 0);
        //                    break;
        //                }
        //                SWControl[i].ChannelAcirSwitchContr(2, Num +1 );             //内阻通道选择          
        //                Thread.Sleep(300);
        //                HIOKI4560[i].ReadRData(out theIRSample);                //内阻采样

        //                //显示阻值
        //                ManualAdjust.Invoke(new EventHandler(delegate
        //                {
        //                    if (theIRSample > 100)
        //                    {
        //                        ManualAdjust.arrTxt_IRSampleShow[Num].BackColor = Color.Red;
        //                    }
        //                    else
        //                    {
        //                        ManualAdjust.arrTxt_IRSampleShow[Num].BackColor = Color.LightGreen;
        //                    }

        //                    ManualAdjust.arrTxt_IRSampleShow[Num].Text = (theIRSample * 1000).ToString("F3") + "/" +
        //                    (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num + i * 19])).ToString("F3");
        //                }));
        //            }
        //        }

        //        mManualIRAdjustFinish = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mManualIRAdjustFinish = true;
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        ////手动内阻校准( 用短路块工装 )
        //private void ManualIRAdjust()
        //{
        //    double theBaseVal;
        //    double theAdjust;
        //    double theIRSample;
        //    int theCHCount = ClsGlobal.TrayType / 2;

        //    try
        //    {
        //        //得到基准值
        //        if (double.TryParse(ManualAdjust.txtIRBase_Adjust.Text, out theBaseVal) == false)
        //        {
        //            ManualAdjust.lblNote_IRAdjust.Text = "请先填入正确的内阻基准值，再进行校准";
        //            return;
        //        }

        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            ManualAdjust.panelIRAdjust.Enabled = false;
        //            ManualAdjust.btnIRAdjustSampleAllStart.Enabled = false;
        //            ManualAdjust.btnIRAdjustAllStart.Enabled = false;
        //            ManualAdjust.btnIRAdjustAllValClr.Enabled = false;
        //        }));

        //        mManualIRAdjustFinish = false;
        //        mManualIRAdjustStop = false;

        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
        //            {
        //                ManualAdjust.arrTxt_IRSampleShow[Num].Text = "";
        //            }
        //        }));

        //        for (int i = 0; i < this.SWCount; i++)
        //        {
        //            for (int Num = 0; Num < theCHCount; Num++)
        //            {
        //                if (mManualIRAdjustStop == true)
        //                {
        //                    Thread.Sleep(50);
        //                    SWControl[i].ChannelAcirSwitchContr(2, 0);
        //                    break;
        //                }
        //                SWControl[i].ChannelAcirSwitchContr(2, Num + 1);             //内阻通道选择          
        //                Thread.Sleep(300);
        //                HIOKI4560[i].ReadRData(out theIRSample);                //内阻采样

        //                if (theIRSample * 1000 > 100)
        //                {
        //                    ManualAdjust.Invoke(new EventHandler(delegate
        //                    {
        //                        ManualAdjust.lblNote_IRAdjust.Text = ("[通道" + ((Num + 1) + this.SWCount * theCHCount) + "]:\r\n" + "测得阻值>100mΩ,校准失败!");
        //                    }));
        //                    break;
        //                }

        //                //计算得到校准值
        //                theAdjust = theBaseVal - theIRSample * 1000;

        //                //更改和保存校准值
        //                INIAPI.INIWriteValue(ClsGlobal.mIRAdjustPath, "ACIRAdjust", "CH" + ((Num + 1) + this.SWCount * theCHCount), theAdjust.ToString("F3"));
        //                ClsGlobal.mIRAdjustVal[((Num + 1) + this.SWCount * theCHCount) - 1] = theAdjust.ToString();

        //                //显示阻值
        //                if (ManualAdjust.IsHandleCreated == true && mManualIRAdjustStop == false)
        //                {
        //                    ManualAdjust.Invoke(new EventHandler(delegate
        //                    {
        //                        ManualAdjust.arrTxt_IRSampleShow[Num + this.SWCount * theCHCount].BackColor = Color.LightGreen;
        //                        ManualAdjust.arrTxt_IRSampleShow[Num + this.SWCount * theCHCount].Text = (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[((Num + 1) + this.SWCount * theCHCount) - 1])).ToString("F3");

        //                    }));
        //                }
        //            }
        //        }

        //        mManualIRAdjustFinish = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mManualIRAdjustFinish = true;
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        ////手动内阻计量( 用标准电阻工装)
        //private void ManualIRMetering()
        //{
        //    double theMeterVal = 0;
        //    double MeterErrRange = 0;
        //    double theIRSample;
        //    double theIR;       //毫欧单位
        //    int theCHCount = ClsGlobal.TrayType / 2;
        //    try
        //    {
        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            //已输入计量值
        //            if (double.TryParse(ManualAdjust.txtIRSet_Metering.Text, out theMeterVal) == false)
        //            {
        //                ManualAdjust.lblNote_IRMetering.Text = ("请先填入正确的计量值，再进行计量");
        //                return;
        //            }

        //            if (double.TryParse(ManualAdjust.txtIRMeterErrRange.Text, out MeterErrRange) == false)
        //            {
        //                ManualAdjust.lblNote_IRMetering.Text = ("请先填入正确的计量误差值，再进行计量");
        //                return;
        //            }
        //        }));

        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            ManualAdjust.panelIRMetering.Enabled = false;
        //            ManualAdjust.btnIRMeteringAllStart.Enabled = false;
        //        }));

        //        mManualIRMeterFinish = false;
        //        mManualIRMeterStop = false;

        //        ManualAdjust.Invoke(new EventHandler(delegate
        //        {
        //            for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
        //            {
        //                ManualAdjust.arrTxt_IRMeterShow[Num].Text = "";
        //            }
        //        }));

        //        for (int i = 0; i < this.SWCount; i++)
        //        {
        //            for (int Num = 0; Num < theCHCount; Num++)
        //            {
        //                if (mManualIRAdjustStop == true)
        //                {
        //                    Thread.Sleep(50);
        //                    SWControl[i].ChannelAcirSwitchContr(2, 0);
        //                    break;
        //                }
        //                SWControl[i].ChannelAcirSwitchContr(2, Num + 1);             //内阻通道选择          
        //                Thread.Sleep(300);
        //                HIOKI4560[i].ReadRData(out theIRSample);                //内阻采样

        //                theIR = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[(Num + 1) + this.SWCount * theCHCount - 1]);  //经过adjust
        //                                                                                                                               //计量测试
        //                ManualAdjust.Invoke(new EventHandler(delegate
        //                {
        //                    ManualAdjust.arrTxt_IRMeterShow[Num + this.SWCount * theCHCount].Text = theIR.ToString("F3");
        //                    if (Math.Abs(theMeterVal - theIR) <= MeterErrRange)
        //                    {
        //                        ManualAdjust.arrTxt_IRMeterShow[Num + this.SWCount * theCHCount].BackColor = Color.LightGreen;
        //                        ManualAdjust.mToolTip_IRMeter.SetToolTip(ManualAdjust.arrTxt_IRMeterShow[Num + this.SWCount * theCHCount], "计量通过");
        //                        ManualAdjust.arrLbl_IRMeterJudge[Num + this.SWCount * theCHCount].Text = "OK";
        //                    }
        //                    else
        //                    {
        //                        ManualAdjust.arrTxt_IRMeterShow[Num + this.SWCount * theCHCount].BackColor = Color.Red;
        //                        ManualAdjust.mToolTip_IRMeter.SetToolTip(ManualAdjust.arrTxt_IRMeterShow[Num + this.SWCount * theCHCount], "计量不通过");
        //                        ManualAdjust.arrLbl_IRMeterJudge[Num + this.SWCount * theCHCount].Text = "NG";
        //                    }
        //                }));
        //            }
        //        }

        //        mManualIRMeterFinish = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mManualIRMeterFinish = true;
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
