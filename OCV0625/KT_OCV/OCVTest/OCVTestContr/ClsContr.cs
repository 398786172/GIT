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
using OCV.Models;

namespace OCV
{
    //OCV测试
    public class ClsOCVContr
    {
        public InfoSend mInfoSend;
        FrmSys mForm;
        FrmManualTest ManualTest;
        FrmManualAdjust ManualAdjust;
        Thread ThreadTestAction;

        public ClsSWControl[] SWControl;       //切换控制
        public ClsDMM_Ag344X DMM_Ag344X;     //万用表
        public ClsHIOKI4560[] HIOKI4560;       //内阻仪BT4560控制
        public ClsHIOKI365X HIOKI365X;       //内阻仪BT356x控制

        public ClsACIRTestData ACIRTestData_A;  //1-16通道 
        public ClsACIRTestData ACIRTestData_B;  /////14-26通道 
        public ClsACIRTestData ACIRTestData_C;  //27-38通道 

        public ClsOCVTestData OCVTestData_A;  //1-16通道 
        public ClsOCVTestData OCVTestData_B;  /////14-26通道 
        public ClsOCVTestData OCVTestData_C;  /////27-38通道 

        private int SWCount = 1;


        //测试参数
        private int ShowHalfVal = ClsGlobal.TrayType / 2;                     //界面更新用
                                                                              // int mStartBattNum;                                                //开始测量的电池通道号,随当前测试位而变动                


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

        private eTestType TestType = eTestType.Null;
        public enum eTestType
        {
            Null = -1,
            A_V = 0,
            B_V = 1,
            A_SV = 2,
            B_SV = 3,
            ACIR = 4,
            END = 5
        }

        public void StopAction()
        {
            try
            {
                if (OCVTestData_A != null)
                {
                    OCVTestData_A.StopTestOCV = true;
                }
                if (OCVTestData_B != null)
                {
                    OCVTestData_B.StopTestOCV = true;
                }
                if (ACIRTestData_A != null)
                {
                    ACIRTestData_A.StopTestAcir = true;
                }
                if (ACIRTestData_B != null)
                {
                    ACIRTestData_B.StopTestAcir = true;
                }
                mAutoTestStop = true;
            }
            finally
            {

            }
        }


        public ClsOCVContr(ClsSWControl[] swControl, ClsDMM_Ag344X dmm_Ag344X, FrmSys Fm)
        {
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.SWCount = this.SWControl.Length;
            this.mForm = Fm;
            int flag = 0;

            for (int i = 0; i < this.SWControl.Length; i++)
            {
                if (ClsGlobal.SwitchChNo[i] == "01--16")
                {
                    this.OCVTestData_A = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 13, 1, this.mForm);
                    flag++;
                }
                //else if (ClsGlobal.SwitchChNo[i] == "14--26")
                //{
                //    this.OCVTestData_B = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 13, 13, this.mForm);
                //    flag++;
                //}
                //else if (ClsGlobal.SwitchChNo[i] == "27--38")
                //{
                //    this.OCVTestData_C = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 12, 26, this.mForm);
                //    flag++;
                //}
                else if (ClsGlobal.SwitchChNo[i] == "01--38")
                {

                    this.OCVTestData_A = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 38, 1, this.mForm);
                    flag++;
                }
            }
            if (this.SWControl.Length != flag)
            {
                throw new Exception("初始化失败,请查看硬件参数是否设置正常");
            }

        }

        public ClsOCVContr(ClsSWControl[] swControl, ClsDMM_Ag344X dmm_Ag344X, ClsHIOKI365X hioki365X, FrmSys Fm)
        {
            this.SWControl = swControl;
            this.DMM_Ag344X = dmm_Ag344X;
            this.HIOKI365X = hioki365X;
            this.mForm = Fm;
        }
        public ClsOCVContr(ClsSWControl[] swControl, ClsDMM_Ag344X dmm_Ag344X, ClsHIOKI4560[] hioki4560, FrmSys Fm)
        {
            int flag = 0;
            this.SWControl = swControl;
            this.SWCount = this.SWControl.Length;
            this.DMM_Ag344X = dmm_Ag344X;
            this.HIOKI4560 = hioki4560;
            this.mForm = Fm;
            for (int i = 0; i < this.SWControl.Length; i++)
            {
                for (int j = 0; j < this.HIOKI4560.Length; j++)
                {
                    if (ClsGlobal.SwitchChNo[i] == "01--13")
                    {
                        if (ClsGlobal.ChNo[j] == "01--13")
                        {

                            this.ACIRTestData_A = new ClsACIRTestData(this.SWControl[i], this.HIOKI4560[j], 13, 1, this.mForm);
                            this.OCVTestData_A = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 13, 1, this.mForm);
                            flag++;
                        }
                    }
                    else if (ClsGlobal.SwitchChNo[i] == "14--26")
                    {
                        if (ClsGlobal.ChNo[j] == "14--26")
                        {
                            this.ACIRTestData_B = new ClsACIRTestData(this.SWControl[i], this.HIOKI4560[j], 13, 13, this.mForm);
                            this.OCVTestData_B = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 13, 13, this.mForm);
                            flag++;
                        }
                    }
                    else if (ClsGlobal.SwitchChNo[i] == "27--38")
                    {
                        if (ClsGlobal.ChNo[j] == "27--38")
                        {
                            this.ACIRTestData_C = new ClsACIRTestData(this.SWControl[i], this.HIOKI4560[j], 12, 26, this.mForm);
                            this.OCVTestData_C = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 12, 26, this.mForm);
                            flag++;
                        }
                    }
                    else if (ClsGlobal.SwitchChNo[i] == "01--38")
                    {
                        if (ClsGlobal.ChNo[j] == "01--38")
                        {
                            this.ACIRTestData_A = new ClsACIRTestData(this.SWControl[i], this.HIOKI4560[j], 38, 1, this.mForm);
                            this.OCVTestData_A = new ClsOCVTestData(this.SWControl[i], this.DMM_Ag344X, 38, 1, this.mForm);
                            flag++;
                        }
                    }
                }
            }
            if (this.SWControl.Length != flag)
            {
                throw new Exception("初始化失败,请查看硬件参数是否设置正常");
            }
        }

        //测试流程
        public void StartTestAction()
        {
            try
            {
                mAutoTestStop = false;
                mAutoTestFinish = false;
                this.OCVTestData_A.TestOCVState = ClsOCVTestData.eTestOCVState.StopTest;
                if (this.SWCount == 3)
                {
                    this.OCVTestData_B.TestOCVState = ClsOCVTestData.eTestOCVState.StopTest;
                    this.OCVTestData_C.TestOCVState = ClsOCVTestData.eTestOCVState.StopTest;
                }
                TestProcess();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CreateData(string path)
        {
            Action act = delegate
              {
                  for (int i = 0; i < 25; i++)
                  {
                      ManualTest.SetNum(i + 1); ;
                      ManualTestVolt_PosNeg();
                      ManualTestVolt_ShellNeg(2);
                      ManualTestIR_BT4560();
                      string filepath = path + "\\" + (i + 1) + ".csv";
                      ManualTest.SaveData(filepath);

                  }

              };
            act.BeginInvoke(iar => { ManualTest.SetEnable(true); }, null);
        }

        //手动测试流程
        public void StartManualTestVolt_PosNeg_Action(FrmManualTest MuTest, Action act)
        {
            ManualTest = MuTest;
            Action testColt = delegate
              {
                  ManualTestVolt_PosNeg();
              };
            testColt.BeginInvoke((iar) =>
            {
                if (act != null)
                    act();
            }, null);
            //ThreadTestAction = new Thread(new ThreadStart(ManualTestVolt_PosNeg));
            //ThreadTestAction.Start();

        }

        //手动测试壳体电压
        public void StartManualTestVolt_ShellNeg_Action(FrmManualTest MuTest, int type)
        {
            ManualTest = MuTest;
            ThreadTestAction = new Thread(ManualTestVolt_ShellNeg);
            ThreadTestAction.Start(type);
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
        public void StartManualIRAdjust_Test_Action(FrmManualAdjust MauAdjust, Action act)
        {
            ManualAdjust = MauAdjust;
            Action actAdjust = () =>
             {
                 ManualIRAdjust_Test();
             };
            actAdjust.BeginInvoke((iar) => { if (act != null) act(); }, null);

        }


        //手动内阻校准
        public void StartManualIRAdjust_Action(FrmManualAdjust MauAdjust, Action callBack)
        {
            ManualAdjust = MauAdjust;
            Action actIRAdjust = () =>
              {
                  ManualIRAdjust();
              };
            actIRAdjust.BeginInvoke(iar => { if (callBack != null) callBack(); }, null);
        }

        //手动内阻计量
        public void StartManualIRMetering_Action(FrmManualAdjust MauAdjust, Action callback)
        {
            ManualAdjust = MauAdjust;
            Action actadjust = () =>
              {
                  ManualIRMetering();
              };
            actadjust.BeginInvoke(iar => { if (callback != null) callback(); }, null);

        }

        private void ClearOCVTestData()
        {
            for (int i = 0; i < OCVTestData_A.mOcvRealData.Length; i++)
            {
                OCVTestData_A.mOcvRealData[i].ACIR_Now = 0;
                OCVTestData_A.mOcvRealData[i].OCV_Now = 0;
                OCVTestData_A.mOcvRealData[i].NegativeTMP = 0;
                OCVTestData_A.mOcvRealData[i].PostiveTMP = 0;
                OCVTestData_A.mOcvRealData[i].Negative_Shell = 0;
                OCVTestData_A.mOcvRealData[i].Postive_Shell = 0;
                if (this.SWCount == 3)
                {
                    OCVTestData_B.mOcvRealData[i].ACIR_Now = 0;
                    OCVTestData_B.mOcvRealData[i].OCV_Now = 0;
                    OCVTestData_B.mOcvRealData[i].NegativeTMP = 0;
                    OCVTestData_B.mOcvRealData[i].PostiveTMP = 0;
                    OCVTestData_B.mOcvRealData[i].Negative_Shell = 0;
                    OCVTestData_B.mOcvRealData[i].Postive_Shell = 0;

                    if (i < 12)
                    {
                        OCVTestData_C.mOcvRealData[i].ACIR_Now = 0;
                        OCVTestData_C.mOcvRealData[i].OCV_Now = 0;
                        OCVTestData_C.mOcvRealData[i].NegativeTMP = 0;
                        OCVTestData_C.mOcvRealData[i].PostiveTMP = 0;
                        OCVTestData_C.mOcvRealData[i].Negative_Shell = 0;
                        OCVTestData_C.mOcvRealData[i].Postive_Shell = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 手动测试流程
        /// </summary>
        /// <param name="manulaTestType"></param>
        /// <returns></returns>
        public Action FuncManualTestProcess(ClsProcess autoProcess, string name)
        {
            return () =>
            {
                autoProcess.TrayInState(4);
                autoProcess.TrayInState(5);
                autoProcess.TrayInState(6);
                autoProcess.TrayInState(8);
                autoProcess.TestWorkState(5);
                autoProcess.SetTempFinish(true);
                autoProcess.TestWorkState(6);
                autoProcess.mInfoSend("测试完成！");
                autoProcess.TestWorkState(9);
            };

        }
        private int mStep_TestReq;
        DateTime TimeTestReqThread;
        TimeSpan TsTestReqThread;
        //测试流程
        private void TestProcess()
        {
            try
            {
                mAutoTestFinish = false;
                mAutoTestStop = false;
                ClearOCVTestData();
                mStep_TestReq = 1;
                short pos = 0;
            retest:
                for (int i = 0; i < 100; i++)
                {
                    switch (mStep_TestReq)
                    {
                        case 1:
                            ClsGlobal.mPLCContr.Set_CylBlock_Up();
                            mInfoSend("PC指示探针松开");
                            TimeTestReqThread = System.DateTime.Now;
                            mStep_TestReq = 2;
                            break;
                    }

                    switch (ClsGlobal.TestType) //电压:0   电压壳压:1   电压 壳压 内阻 2    电压  正极 负极壳压 内阻 3
                    {
                        case 0:

                            #region 测正负极 

                            this.TestVoltForProc();
                            this.LoadOCVData(this.SWCount);
                            this.CalVoltDrop();
                            this.NGStateOutput();
                            this.CalDROPRange();
                            break;

                        #endregion

                        case 1:

                            #region 测正负极 

                            this.TestVoltForProc();

                            #endregion

                            #region 测壳体电压

                            // TestShellVoltForProc_Postive();
                            this.TestShellVoltForProc();

                            #endregion

                            this.LoadOCVData(this.SWCount);
                            this.LoadSVData(this.SWCount);
                            this.CalVoltDrop();
                            this.NGStateOutput();
                            this.CheckSVNG();
                            //this.CheckSVNG_Postive();
                            this.CalDROPRange();

                            break;
                        case 2:

                            #region 测正负极 

                            this.TestVoltForProc();

                            #endregion

                            #region 测壳体电压

                            //  TestShellVoltForProc_Postive();
                            this.TestShellVoltForProc();


                            #endregion

                            #region 测内阻 

                            this.TestACIRForProc();

                            #endregion

                            this.LoadOCVData(this.SWCount);
                            this.LoadSVData(this.SWCount);
                            this.LoadAcirData(this.SWCount);

                            this.CalVoltDrop();
                            this.NGStateOutput();
                            this.CheckSVNG();
                            // this.CheckSVNG_Postive();
                            this.CalDROPRange();

                            if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                            {
                                this.CalACIRRange();
                            }

                            break;

                        case 3:

                            #region 测正负极 

                            this.TestVoltForProc();

                            #endregion

                            #region 测壳体电压

                            TestShellVoltForProc_Postive();
                            this.TestShellVoltForProc();

                            #endregion

                            #region 测内阻 

                            this.TestACIRForProc();

                            #endregion

                            this.LoadOCVData(this.SWCount);
                            this.LoadSVData(this.SWCount);
                            this.LoadAcirData(this.SWCount);

                            this.CalVoltDrop();
                            this.NGStateOutput();
                            this.CheckSVNG();
                            this.CheckSVNG_Postive();
                            this.CalDROPRange();

                            if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                            {
                                this.CalACIRRange();
                            }

                            break;
                        default:
                            break;
                    }
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
                if (ClsGlobal.IsRetest == false && ClsGlobal.RetestList.Count > 0)
                {
                    mInfoSend("通道数据存在NG，现对通道进行复测..");
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_指示测定结束, 2);        //再次测定
                    double timeValue = 0;
                    bool isRequestTest = false;
                    DateTime dtStart = DateTime.Now;
                    do
                    {
                        Thread.Sleep(1000);
                        if (ClsPLCValue.PlcValue.Plc_RequestTest == 1)
                        {
                            isRequestTest = true;
                            ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_指示测定结束, 0);
                        }
                        timeValue = (DateTime.Now - dtStart).Seconds;
                        if (timeValue >= 40)
                        {
                            ClsGlobal.IsRetest = true;
                            mInfoSend("复测应答超时");
                            throw new Exception("复测应答超时！");
                        }
                    } while (isRequestTest == false);
                    ClsGlobal.IsRetest = true;

                    DateTime dtRequestEnd = DateTime.Now;
                    bool isClearRequest = false;
                    do
                    {
                        Thread.Sleep(1000);
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply == 0)
                        {
                            isClearRequest = true;
                            Thread.Sleep(100);
                            ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_应答检测请求, 1);
                            Thread.Sleep(100);
                        }
                        timeValue = (DateTime.Now - dtStart).Seconds;
                        if (timeValue >= 50)
                        {
                            mInfoSend("复测请求清空信号应答超时");
                            throw new Exception("复测请求清空信号应答超时！");
                        }
                    }
                    while (isClearRequest == false);


                    isRequestTest = false;
                    dtStart = DateTime.Now;
                    do
                    {
                        Thread.Sleep(1000);
                        if (ClsPLCValue.PlcValue.Plc_RequestTest == 0)
                        {
                            ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_应答检测请求, 0);
                            isRequestTest = true;
                        }
                        timeValue = (DateTime.Now - dtStart).Seconds;
                        if (timeValue >= 40)
                        {
                            mInfoSend("复测请求信号清空应答超时");
                            throw new Exception("复测请求信号清空应答超时！");
                        }
                    } while (isRequestTest == false);

                    goto retest;
                }

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

                                        if (ClsGlobal.listETCELL[i].PostiveSV_NgResult.NgType == 1)
                                        {
                                            mForm.dgvTest.Rows[i].Cells["Col_CaseOCV2"].Style.ForeColor = Color.Red;
                                            if (mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value.ToString() != "")
                                            {
                                                mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value += "| " + ClsGlobal.listETCELL[i].PostiveSV_NgResult.NgCode;
                                                mForm.dgvTest.Rows[i].Cells["Col_Des"].Value += "| " + ClsGlobal.listETCELL[i].PostiveSV_NgResult.NgDescribe;
                                            }
                                            else
                                            {
                                                mForm.dgvTest.Rows[i].Cells["Col_CODE"].Value = ClsGlobal.listETCELL[i].PostiveSV_NgResult.NgCode;
                                                mForm.dgvTest.Rows[i].Cells["Col_Des"].Value = ClsGlobal.listETCELL[i].PostiveSV_NgResult.NgDescribe;
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
                //  ClsGlobal.OCV_TestState = eTestState.TestOK;            //测试成功
                ClsGlobal.OCV_TestState = GetTestState();
                mAutoTestFinish = true;//该次测试完成 
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;

            }
        }

        private eTestState GetTestState()
        {
            return eTestState.TestOK;
            if (ClsGlobal.IsTestAgain == true)
                return eTestState.TestOK;

            if (ClsGlobal.OCVType != 3)
                return eTestState.TestOK;
            var query = from p in ClsGlobal.listETCELL where p.ACIR_Now >= 800000000 select p;
            if (query != null && query.Count() > 0)
            {
                ClsGlobal.IsTestAgain = true;
                return eTestState.TestAgain;
            }
            else
            {
                return eTestState.TestOK;
            }
        }
        //流程中测试电池 (电压)
        private void TestVoltForProc()
        {
            this.TestType = eTestType.A_V;

            for (int i = 0; i < this.SWCount; i++)
            {
                SWControl[i].ChannelVoltSwitch(1, 0);        //正极对负极
                Thread.Sleep(15);
            }
            int channel = this.SWCount == 3 ? (ClsGlobal.TrayType + 1) / 3 : ClsGlobal.TrayType;
            for (int i = 0; i < this.SWCount; i++)
            {
                Thread.Sleep(200);
                for (int Num = 0; Num < channel; Num++)
                {
                    int position = Num + i * channel;
                    if (ClsGlobal.IsRetest)
                    {
                        var query = (from p in ClsGlobal.RetestList where p.Position == position && p.RetestTypelist.Contains(RetestTypeEnum.Voltage) select p);
                        if (query == null || query.Count() == 0)
                        {
                            continue;
                        }
                    }
                    if (i == 2 && Num >= 12)
                        continue;
                    SWControl[i].ChannelVoltSwitch(1, Num + 1);        //正极对负极
                    Thread.Sleep(ClsGlobal.SleepTime);
                    double theDMMVolt = 0;
                    DMM_Ag344X.ReadVolt(out theDMMVolt);
                    var myValue = theDMMVolt * 1000;
                    int tempValue = Num + i * 13;
                    if (this.SWCount == 1)
                    {
                        OCVTestData_A.mOcvRealData[Num].OCV_Now = Math.Round(myValue, 4);
                    }
                    else
                    {
                        double bcValue = 0;
                        if (ClsGlobal.OCVType == 2)
                        {
                            bcValue = ClsGlobal.OCV23BCValue;
                        }
                        if (ClsGlobal.OCVType == 3)
                        {
                            if (DateTime.Now > ClsGlobal.OCV3BCTime)
                            {
                                bcValue = ClsGlobal.OCV23BCValue;
                            }
                        }
                        myValue = Math.Round(myValue, 4) + bcValue;
                        if (tempValue <= 12)
                        {

                            OCVTestData_A.mOcvRealData[Num].OCV_Now = myValue;
                        }
                        if (tempValue >= 13 && tempValue <= 25)
                        {
                            OCVTestData_B.mOcvRealData[Num].OCV_Now = myValue;
                        }
                        if (tempValue >= 26)
                        {
                            OCVTestData_C.mOcvRealData[Num].OCV_Now = myValue;
                        }
                    }
                    mForm.SetValueToUI(Num + i * 13, "Col_OCV", myValue, 4);

                }
                SWControl[i].ChannelVoltSwitch(1, 0);        //正极对负极
            }


        }


        //流程中测试电池 (内阻)   
        private void TestACIRForProc()
        {

            try
            {
                this.TestType = eTestType.ACIR;
                #region 测内阻 BT4560
                Thread.Sleep(2);
                Action actA = delegate
                  {
                      SWControl[0].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                      for (int Num = 0; Num < 13; Num++)
                      {
                          int position = Num;
                          if (ClsGlobal.IsRetest)
                          {
                              var query = (from p in ClsGlobal.RetestList where p.Position == position && p.RetestTypelist.Contains(RetestTypeEnum.ACIR) select p);
                              if (query == null || query.Count() == 0)
                              {
                                  continue;
                              }
                          }
                          SWControl[0].ChannelAcirSwitchContr(2, Num + 1);     //内阻测量
                          Thread.Sleep(200);
                          double theIRSample;
                          HIOKI4560[0].ReadRData(out theIRSample);
                          var myValue = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num]);   //经过adjust
                          if (myValue > 1000000)
                          {
                              myValue = 0;
                          }
                          OCVTestData_A.mOcvRealData[Num].ACIR_Now = myValue;
                          ClsGlobal.listETCELL[Num].ACIR_Now = myValue;
                          mForm.SetValueToUI(Num, "Col_ACIR", myValue, 4);
                      }
                  };
                Action actB = delegate
                {
                    SWControl[1].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                    for (int Num = 0; Num < 13; Num++)
                    {
                        int position = Num + 13;
                        if (ClsGlobal.IsRetest)
                        {
                            var query = (from p in ClsGlobal.RetestList where p.Position == position && p.RetestTypelist.Contains(RetestTypeEnum.ACIR) select p);
                            if (query == null || query.Count() == 0)
                            {
                                continue;
                            }
                        }
                        SWControl[1].ChannelAcirSwitchContr(2, Num + 1);     //内阻测量
                        Thread.Sleep(200);
                        double theIRSample;
                        HIOKI4560[1].ReadRData(out theIRSample);
                        var myValue = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num + 13]);   //经过adjust
                        if (myValue > 1000000)
                        {
                            myValue = 0;
                        }
                        OCVTestData_B.mOcvRealData[Num].ACIR_Now = myValue;
                        ClsGlobal.listETCELL[Num + 13].ACIR_Now = myValue;
                        mForm.SetValueToUI(Num + 13, "Col_ACIR", myValue, 4);
                    }
                };

                Action actC = delegate
                {
                    SWControl[2].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                    for (int Num = 0; Num < 13; Num++)
                    {
                        if (Num >= 12)
                            continue;
                        int position = Num + 26;
                        if (ClsGlobal.IsRetest)
                        {
                            var query = (from p in ClsGlobal.RetestList where p.Position == position && p.RetestTypelist.Contains(RetestTypeEnum.ACIR) select p);
                            if (query == null || query.Count() == 0)
                            {
                                continue;
                            }
                        }
                        SWControl[2].ChannelAcirSwitchContr(2, Num + 1);     //内阻测量
                        Thread.Sleep(200);
                        double theIRSample;
                        HIOKI4560[2].ReadRData(out theIRSample);
                        var myValue = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num + 26]);   //经过adjust
                        if (myValue > 1000000)
                        {
                            myValue = 0;
                        }
                        OCVTestData_C.mOcvRealData[Num].ACIR_Now = myValue;
                        ClsGlobal.listETCELL[Num + 26].ACIR_Now = myValue;
                        mForm.SetValueToUI(Num + 26, "Col_ACIR", myValue, 4);
                    }
                };
                var resultA = actA.BeginInvoke(null, null);
                var resultB = actB.BeginInvoke(null, null);
                var resultC = actC.BeginInvoke(null, null);
                actA.EndInvoke(resultA);
                actB.EndInvoke(resultB);
                actC.EndInvoke(resultC);
                #endregion
            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                throw ex;
                // MessageBox.Show(ex.Message.ToString());

            }
        }

        //流程中测试电池 (壳压)
        private void TestShellVoltForProc()
        {
            try
            {
                for (int i = 0; i < this.SWCount; i++)
                {
                    SWControl[i].ChannelVoltSwitch(2, 0);       //壳体对负极
                    Thread.Sleep(15);
                }
                for (int i = 0; i < this.SWCount; i++)
                {
                    Thread.Sleep(200);
                    for (int Num = 0; Num < 13; Num++)
                    {
                        int position = Num + i * 13;
                        if (ClsGlobal.IsRetest)
                        {
                            var query = (from p in ClsGlobal.RetestList where p.Position == position && p.RetestTypelist.Contains(RetestTypeEnum.SNVolage) select p);
                            if (query == null || query.Count() == 0)
                            {
                                continue;
                            }
                        }
                        if (i == 2 && Num >= 12)
                        {
                            continue;
                        }
                        SWControl[i].ChannelVoltSwitch(2, Num + 1);        //壳体对负极
                        Thread.Sleep(ClsGlobal.SleepTime);
                        double theDMMVolt = 0;
                        DMM_Ag344X.ReadVolt(out theDMMVolt);
                        var myValue = theDMMVolt * 1000;
                        int tempValue = Num + i * 13;
                        if (tempValue <= 12)
                        {
                            OCVTestData_A.mOcvRealData[Num].Negative_Shell = Math.Round(myValue, 4);
                        }
                        if (tempValue >= 13 && tempValue <= 25)
                        {
                            OCVTestData_B.mOcvRealData[Num].Negative_Shell = Math.Round(myValue, 4);
                        }
                        if (tempValue >= 26)
                        {
                            OCVTestData_C.mOcvRealData[Num].Negative_Shell = Math.Round(myValue, 4);
                        }
                        mForm.SetValueToUI(Num + i * 13, "Col_CaseOCV", myValue, 4);

                    }
                    SWControl[i].ChannelVoltSwitch(2, 0);       //壳体对负极
                }

            }
            catch (Exception ex)
            {
                //测试异常   
                ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //流程中测试电池 (壳压 正)
        private void TestShellVoltForProc_Postive()
        {
            try
            {
                for (int i = 0; i < this.SWCount; i++)
                {
                    SWControl[i].ChannelVoltSwitch(3, 0);       //壳体对正极
                    Thread.Sleep(15);
                }
                for (int i = 0; i < this.SWCount; i++)
                {
                    Thread.Sleep(200);
                    for (int Num = 0; Num < 13; Num++)
                    {
                        int position = Num + i * 13;
                        if (ClsGlobal.IsRetest)
                        {
                            var query = (from p in ClsGlobal.RetestList where p.Position == position && p.RetestTypelist.Contains(RetestTypeEnum.SPVolage) select p);
                            if (query == null || query.Count() == 0)
                            {
                                continue;
                            }
                        }
                        if (i == 2 && Num >= 12)
                            continue;
                        SWControl[i].ChannelVoltSwitch(3, Num + 1);        //壳体对正极
                        Thread.Sleep(ClsGlobal.SleepTime);
                        double theDMMVolt = 0;
                        DMM_Ag344X.ReadVolt(out theDMMVolt);
                        var myValue = theDMMVolt * 1000;
                        int tempValue = Num + i * 13;
                        if (tempValue <= 12)
                        {
                            OCVTestData_A.mOcvRealData[Num].Postive_Shell = Math.Round(myValue, 4);
                        }
                        if (tempValue >= 13 && tempValue <= 25)
                        {
                            OCVTestData_B.mOcvRealData[Num].Postive_Shell = Math.Round(myValue, 4);
                        }
                        if (tempValue >= 26)
                        {
                            OCVTestData_C.mOcvRealData[Num].Postive_Shell = Math.Round(myValue, 4);
                        }
                        // mForm.SetValueToUI(Num + i * 19, 3, myValue, 4);
                        mForm.SetValueToUI(Num + i * 13, "Col_CaseOCV2", myValue, 4);

                    }
                    SWControl[i].ChannelVoltSwitch(3, 0);       //壳体对正极
                }

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
                ClsGlobal.listETCELL[i].NgState = "OK";
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
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].OCV_Now < ClsGlobal.DownLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超下限";
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                            ClsGlobal.RetestList.Add(model);
                            // return;
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
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].OCV_Now < ClsGlobal.DownLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超下限";
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }


                    }
                    else if (ClsGlobal.TestType == 2 || ClsGlobal.TestType == 3)
                    {
                        //电压
                        if (ClsGlobal.listETCELL[i].OCV_Now > ClsGlobal.UpLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超上限";
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].OCV_Now < ClsGlobal.DownLmt_V)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = ClsGlobal.OCVType + "X1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "电压超下限";
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].ACIR_Now > ClsGlobal.UpLmt_ACIR)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = "AX2";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "ACIR超上限";
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.ACIR);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }
                        else if (ClsGlobal.listETCELL[i].ACIR_Now < ClsGlobal.DownLmt_ACIR)
                        {
                            mNgResult.NgType = 1;
                            mNgResult.NgCode = "AX1";
                            mNgResult.NgState = "NG";
                            mNgResult.NgDescribe = "ACIR超下限";
                            ClsGlobal.listETCELL[i].NgState = "NG";
                            var model = new RetestData() { Position = i, ChannelNo = i + 1 };
                            model.RetestTypelist.Add(RetestTypeEnum.ACIR);
                            ClsGlobal.RetestList.Add(model);
                            // return;
                        }

                    }

                }
                else
                {
                    mNgResult.NgType = 1;
                    mNgResult.NgCode = "C";
                    mNgResult.NgState = "NG";
                    mNgResult.NgDescribe = "无电池";
                    ClsGlobal.listETCELL[i].NgState = "NG";
                    //ClsGlobal.listETCELL[i].NGSt = "NG";
                    //ClsGlobal.listETCELL[i].NGReason ="C";
                    //ClsGlobal.listETCELL[i].NGRea = "无电池";
                }

                ClsGlobal.listETCELL[i].Test_NgResult = mNgResult;
            }
            #endregion  
        }

        //整合OCV数据
        public void LoadOCVData(int SwCount)
        {
            #region 数据载入

            for (int i = 0; i < (ClsGlobal.TrayType + 1) / SwCount; i++)
            {
                ClsGlobal.listETCELL[i].OCV_Now = OCVTestData_A.mOcvRealData[i].OCV_Now;
                ClsGlobal.listETCELL[i].Rev_OCV = Math.Round((OCVTestData_A.mOcvRealData[i].OCV_Now + ((ClsGlobal.G_dbl_P_TempArr[i] + ClsGlobal.G_dbl_P_TempArr[i]) / 2
                    - ClsGlobal.TempBase) * ClsGlobal.TempParaModify), 4);    //计算电压修正值    
                if (SwCount == 3)
                {
                    ClsGlobal.listETCELL[i].OCV_Now = OCVTestData_A.mOcvRealData[i].OCV_Now;
                    ClsGlobal.listETCELL[i].Rev_OCV = Math.Round((OCVTestData_A.mOcvRealData[i].OCV_Now + ((ClsGlobal.G_dbl_P_TempArr[i] + ClsGlobal.G_dbl_P_TempArr[i]) / 2
                        - ClsGlobal.TempBase) * ClsGlobal.TempParaModify), 4);    //计算电压修正值  
                    //mForm.SetValueToUI(i, "Col_OCV", ClsGlobal.listETCELL[i].Rev_OCV, 4);

                    ClsGlobal.listETCELL[i + 13].OCV_Now = OCVTestData_B.mOcvRealData[i].OCV_Now;
                    ClsGlobal.listETCELL[i + 13].Rev_OCV = Math.Round((OCVTestData_B.mOcvRealData[i].OCV_Now + ((ClsGlobal.G_dbl_P_TempArr[i + 13] + ClsGlobal.G_dbl_P_TempArr[i + 13]) / 2
                        - ClsGlobal.TempBase) * ClsGlobal.TempParaModify), 4);    //计算电压修正值      
                                                                                  //  mForm.SetValueToUI(i + 13, "Col_OCV", ClsGlobal.listETCELL[i + 13].Rev_OCV, 4);

                    if (i < 12)
                    {
                        ClsGlobal.listETCELL[i + 26].OCV_Now = OCVTestData_C.mOcvRealData[i].OCV_Now;
                        ClsGlobal.listETCELL[i + 26].Rev_OCV = Math.Round((OCVTestData_C.mOcvRealData[i].OCV_Now + ((ClsGlobal.G_dbl_P_TempArr[i + 26] + ClsGlobal.G_dbl_P_TempArr[i + 26]) / 2
                            - ClsGlobal.TempBase) * ClsGlobal.TempParaModify), 4);    //计算电压修正值      
                                                                                      //  mForm.SetValueToUI(i + 26, "Col_OCV", ClsGlobal.listETCELL[i + 26].Rev_OCV, 4);
                    }
                }
            }

            #endregion
        }

        //整合壳压数据
        public void LoadSVData(int SwCount)
        {
            #region 数据载入

            for (int i = 0; i < (ClsGlobal.TrayType + 1) / 3; i++)
            {
                ClsGlobal.listETCELL[i].OCV_Shell_Now = OCVTestData_A.mOcvRealData[i].Negative_Shell;
                ClsGlobal.listETCELL[i].OCV_ShellPostive_Now = OCVTestData_A.mOcvRealData[i].Postive_Shell;
                if (SwCount == 3)
                {
                    ClsGlobal.listETCELL[i + 13].OCV_Shell_Now = OCVTestData_B.mOcvRealData[i].Negative_Shell;
                    ClsGlobal.listETCELL[i + 13].OCV_ShellPostive_Now = OCVTestData_B.mOcvRealData[i].Postive_Shell;
                    if (i < 12)
                    {
                        ClsGlobal.listETCELL[i + 26].OCV_Shell_Now = OCVTestData_C.mOcvRealData[i].Negative_Shell;
                        ClsGlobal.listETCELL[i + 26].OCV_ShellPostive_Now = OCVTestData_C.mOcvRealData[i].Postive_Shell;
                    }
                }
            }

            #endregion
        }

        //整合壳压数据
        public void LoadAcirData(int SwCount)
        {
            #region 数据载入

            for (int i = 0; i < (ClsGlobal.TrayType + 1) / 3; i++)
            {
                ClsGlobal.listETCELL[i].ACIR_Now = OCVTestData_A.mOcvRealData[i].ACIR_Now + double.Parse(ClsGlobal.mIRAdjustVal[i]);

                if (SwCount == 3)
                {
                    ClsGlobal.listETCELL[i + 13].ACIR_Now = OCVTestData_B.mOcvRealData[i].ACIR_Now + double.Parse(ClsGlobal.mIRAdjustVal[i + 13]);

                    if (i < 12)
                    {
                        ClsGlobal.listETCELL[i + 26].ACIR_Now = OCVTestData_C.mOcvRealData[i].ACIR_Now + double.Parse(ClsGlobal.mIRAdjustVal[i + 26]);
                    }
                }
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
                        if (ClsGlobal.TestType == 2 || ClsGlobal.TestType == 3)
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
                        if (ClsGlobal.TestType == 2 || ClsGlobal.TestType == 3)
                        {
                            if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK" && lstVSAcirVal.Count != 0)
                            {
                                ClsGlobal.listETCELL[i].ACIR_range = Math.Round(ClsGlobal.listETCELL[i].ACIR_Now - VSVal, 4);     //获得ACIR对比值的极差
                                ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(i, 2, ClsGlobal.listETCELL[i].ACIR_range);
                            }
                            else
                            {
                                ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(i, 1, ClsGlobal.listETCELL[i].ACIR_range);
                            }
                        }
                    }
                    else
                    {
                        ClsGlobal.listETCELL[i].ACIR_NgResult = CheckACIRRangeNG(i, 0, ClsGlobal.listETCELL[i].ACIR_range);
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
                if (ClsGlobal.ENVoltDrop == "Y" && ClsGlobal.OCVType == 3)
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
                                #region TestType > 0
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && ClsGlobal.listETCELL[i].SV_NgResult.NgState == "OK")
                                {
                                    ClsGlobal.listETCELL[i].VoltDrop_NowState = CheckVDropNG(i, 2, ClsGlobal.listETCELL[i].VoltDrop_Now);
                                    if (ClsGlobal.IS_Enable_DropRange == "Y" && lstVSVal.Count != 0)
                                    {
                                        ClsGlobal.listETCELL[i].DROP_range = Math.Round(ClsGlobal.listETCELL[i].VoltDrop_Now - VSVal, 4);     //获得压降对比值的极差
                                        ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(i, 2, ClsGlobal.listETCELL[i].DROP_range);
                                        // mForm.SetValueToUI(i, "VoltDropRange", ClsGlobal.listETCELL[i].DROP_range / 1000, 7);
                                    }
                                }
                                else
                                {
                                    ClsGlobal.listETCELL[i].VoltDrop_NowState = CheckVDropNG(i, 1, ClsGlobal.listETCELL[i].VoltDrop_Now);
                                    if (ClsGlobal.IS_Enable_DropRange == "Y" && lstVSVal.Count != 0)
                                    {
                                        ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(i, 1, ClsGlobal.listETCELL[i].DROP_range);
                                        // mForm.SetValueToUI(i, "VoltDropRange", ClsGlobal.listETCELL[i].DROP_range / 1000, 7);
                                    }
                                }
                                #endregion TestType < 0
                            }
                            else
                            {
                                #region TestType > 0
                                if (ClsGlobal.listETCELL[i].Test_NgResult.NgState == "OK" && lstVSVal.Count != 0)
                                {
                                    ClsGlobal.listETCELL[i].VoltDrop_NowState = CheckVDropNG(i, 2, ClsGlobal.listETCELL[i].VoltDrop_Now);
                                    if (ClsGlobal.IS_Enable_DropRange == "Y" && lstVSVal.Count != 0)
                                    {
                                        ClsGlobal.listETCELL[i].DROP_range = Math.Round(ClsGlobal.listETCELL[i].VoltDrop_Now - VSVal, 4);     //获得压降对比值的极差
                                        ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(i, 2, ClsGlobal.listETCELL[i].DROP_range);
                                        // mForm.SetValueToUI(i, "VoltDropRange", ClsGlobal.listETCELL[i].DROP_range / 1000, 7);
                                    }
                                }
                                else
                                {
                                    ClsGlobal.listETCELL[i].VoltDrop_NowState = CheckVDropNG(i, 1, ClsGlobal.listETCELL[i].VoltDrop_Now);
                                    if (ClsGlobal.IS_Enable_DropRange == "Y")
                                    {
                                        ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(i, 1, ClsGlobal.listETCELL[i].DROP_range);
                                        // mForm.SetValueToUI(i, "VoltDropRange", ClsGlobal.listETCELL[i].DROP_range / 1000, 7);
                                    }
                                }
                                #endregion TestType > 0
                            }
                        }
                        else
                        {
                            ClsGlobal.listETCELL[i].VoltDrop_NowState = CheckVDropNG(i, 0, ClsGlobal.listETCELL[i].VoltDrop_Now);
                            if (ClsGlobal.IS_Enable_DropRange == "Y")
                            {
                                ClsGlobal.listETCELL[i].DROP_NgResult = CheckVDropRangeNG(i, 0, ClsGlobal.listETCELL[i].DROP_range);
                                //mForm.SetValueToUI(i, "VoltDropRange", ClsGlobal.listETCELL[i].DROP_range, 4);
                            }
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

        /// <summary>
        /// 压降判定
        /// </summary>
        /// <returns></returns>
        public NgResult CheckVDropNG(int position, int flag, double voltage)
        {
            NgResult mNgResult = new NgResult();
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

            if (voltage > ClsGlobal.MaxVoltDrop)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "JZ2";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "压降超上限";
                var model = new RetestData() { Position = position, ChannelNo = position + 1 };
                model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                ClsGlobal.RetestList.Add(model);
                // return;
            }
            else if (voltage < ClsGlobal.MinVoltDrop)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "JZ1";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "压降超下限";
                var model = new RetestData() { Position = position, ChannelNo = position + 1 };
                model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                ClsGlobal.RetestList.Add(model);
                // return;
            }
            return mNgResult;
        }

        /// <summary>
        /// 压降极差判断
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="Val"></param>
        /// <returns></returns>
        private NgResult CheckVDropRangeNG(int position, int flag, double Val)
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
                var model = new RetestData() { Position = position, ChannelNo = position + 1 };
                model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                ClsGlobal.RetestList.Add(model);
                // return;
            }
            else if (Val < ClsGlobal.DownLMT_DropRange)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "JZ1";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "压降极差超下限";
                var model = new RetestData() { Position = position, ChannelNo = position + 1 };
                model.RetestTypelist.Add(RetestTypeEnum.Voltage);
                ClsGlobal.RetestList.Add(model);
                // return;
            }
            return mNgResult;
        }

        //ACIR极差判断
        private NgResult CheckACIRRangeNG(int position, int flag, double Val)
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
                var model = new RetestData() { Position = position, ChannelNo = position + 1 };
                model.RetestTypelist.Add(RetestTypeEnum.ACIR);
                ClsGlobal.RetestList.Add(model);
                // return;
            }
            else if (Val < ClsGlobal.DownLMT_ACIRRange)
            {
                mNgResult.NgType = 1;
                mNgResult.NgCode = "AJ1";
                mNgResult.NgState = "NG";
                mNgResult.NgDescribe = "ACIR极差超下限";
                var model = new RetestData() { Position = position, ChannelNo = position + 1 };
                model.RetestTypelist.Add(RetestTypeEnum.ACIR);
                ClsGlobal.RetestList.Add(model);
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
                        mNgResult.NgCode = "C1";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳/负极压超上限";
                        ClsGlobal.listETCELL[i].NgState = "NG";
                        var model = new RetestData() { Position = i, ChannelNo = ClsGlobal.listETCELL[i].Cell_Position };
                        model.RetestTypelist.Add(RetestTypeEnum.SNVolage);
                        ClsGlobal.RetestList.Add(model);
                        // return;
                    }
                    else if (ClsGlobal.listETCELL[i].OCV_Shell_Now < ClsGlobal.DownLmt_SV)
                    {
                        mNgResult.NgType = 1;
                        mNgResult.NgCode = "C2";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳/负极压超下限";
                        ClsGlobal.listETCELL[i].NgState = "NG";
                        var model = new RetestData() { Position = i, ChannelNo = ClsGlobal.listETCELL[i].Cell_Position };
                        model.RetestTypelist.Add(RetestTypeEnum.SNVolage);
                        ClsGlobal.RetestList.Add(model);
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

        private void CheckSVNG_Postive()
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
                    if (ClsGlobal.listETCELL[i].OCV_ShellPostive_Now > ClsGlobal.UpLmt_SVP)
                    {
                        mNgResult.NgType = 1;
                        mNgResult.NgCode = "C3";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳/正极压超上限";
                        ClsGlobal.listETCELL[i].NgState = "NG";
                        var model = new RetestData() { Position = i, ChannelNo = ClsGlobal.listETCELL[i].Cell_Position };
                        model.RetestTypelist.Add(RetestTypeEnum.SPVolage);
                        ClsGlobal.RetestList.Add(model);
                        // return;
                    }
                    else if (ClsGlobal.listETCELL[i].OCV_ShellPostive_Now < ClsGlobal.DownLmt_SVP)
                    {
                        mNgResult.NgType = 1;
                        mNgResult.NgCode = "C4";
                        mNgResult.NgState = "NG";
                        mNgResult.NgDescribe = "壳/正极压超下限";
                        ClsGlobal.listETCELL[i].NgState = "NG";
                        var model = new RetestData() { Position = i, ChannelNo = ClsGlobal.listETCELL[i].Cell_Position };
                        model.RetestTypelist.Add(RetestTypeEnum.SPVolage);
                        ClsGlobal.RetestList.Add(model);
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
                ClsGlobal.listETCELL[i].PostiveSV_NgResult = mNgResult;
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
                        if (ClsGlobal.GetHours != 0)
                        {
                            ClsGlobal.listETCELL[i].K_Now = Math.Round((VoltToComp / ClsGlobal.GetHours), 6);
                        }
                        else
                        {
                            ClsGlobal.listETCELL[i].K_Now = 0;
                        }
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
                    for (int j = 1; j < 10; j++)
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
        public void SetForm(FrmManualTest frmTest)
        {
            ManualTest = frmTest;
        }

        //手动测试正负极电压
        private void ManualTestVolt_PosNeg()
        {

            int theCHCount = ClsGlobal.OCVType == 1 ? ClsGlobal.TrayType : (ClsGlobal.TrayType + 1) / 3;
            double theDMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            try
            {
                ManualTest.ClearDgv("Volt_PosNeg");
                mManualTestFinish = false;
                mManualTestStop = false;
                for (int i = 0; i < this.SWCount; i++)
                {
                    SWControl[i].ChannelVoltSwitch(1, 0);        //正极对负极
                    Thread.Sleep(15);
                }
                #region 测电压


                for (int i = 0; i < this.SWCount; i++)
                {
                    Thread.Sleep(250);
                    for (int Num = 0; Num < theCHCount; Num++)
                    {
                        if (i == 2 && Num == 12)
                            continue;
                        SWControl[i].ChannelVoltSwitch(1, Num + 1);        //正极对负极

                        Thread.Sleep(ClsGlobal.SleepTime);

                        if (ClsGlobal.EN_TestOCV == 1)
                        {
                            DMM_Ag344X.ReadVolt(out theDMMVolt);
                        }
                        arrDMMVolt[Num] = theDMMVolt * 1000;


                        if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                        {
                            ManualTest.Invoke(new EventHandler(delegate
                            {
                                arrDMMVolt[Num] = arrDMMVolt[Num];
                                ManualTest.dgvManualTest.Rows[Num + i * 13].Cells["Volt_PosNeg"].Value = arrDMMVolt[Num].ToString("F4");   //刷新界面
                            }));
                        }
                        if (mManualTestStop == true)
                        {
                            break;
                        }
                    }
                    SWControl[i].ChannelVoltSwitch(1, 0);        //正极对负极
                }
                #endregion
                //测正负极电压
                mManualTestFinish = true;

            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }

        }

        //手动测试壳体与负极电压
        private void ManualTestVolt_ShellNeg(object obj)
        {
            int type = (int)obj;
            double theDMMVolt = 0;
            double[] arrDMMVolt = new double[ClsGlobal.TrayType];
            int theCHCount = this.SWCount == 3 ? (ClsGlobal.TrayType + 1) / 3 : ClsGlobal.TrayType;

            try
            {
                if (type == 3)
                    ManualTest.ClearDgv("Volt_ShellNeg2");
                if (type == 2)
                    ManualTest.ClearDgv("Volt_ShellNeg");
                mManualTestFinish = false;
                mManualTestStop = false;
                for (int i = 0; i < this.SWCount; i++)
                {
                    SWControl[i].ChannelVoltSwitch(type, 0);
                }
                #region 测壳体电压
                for (int i = 0; i < this.SWCount; i++)
                {
                    Thread.Sleep(250);
                    for (int Num = 0; Num < theCHCount; Num++)
                    {
                        if (i == 2 && Num == 12)
                            continue;

                        SWControl[i].ChannelVoltSwitchContr(type, Num + 1);        //壳体负极测量
                        Thread.Sleep(ClsGlobal.SleepTime);

                        if (ClsGlobal.EN_TestOCV == 1)
                        {
                            DMM_Ag344X.ReadVolt(out theDMMVolt);
                        }
                        arrDMMVolt[Num] = theDMMVolt * 1000;
                        //数值显示
                        if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                        {
                            ManualTest.Invoke(new EventHandler(delegate
                            {
                                arrDMMVolt[Num] = arrDMMVolt[Num];
                                if (type == 2)
                                    ManualTest.dgvManualTest.Rows[Num + i * 13].Cells["Volt_ShellNeg"].Value = arrDMMVolt[Num].ToString("F4");   //刷新界面
                                else
                                    ManualTest.dgvManualTest.Rows[Num + i * 13].Cells["Volt_ShellNeg2"].Value = arrDMMVolt[Num].ToString("F4");   //刷新界面
                            }));
                        }
                        if (mManualTestStop == true)
                        {
                            break;
                        }

                    }
                    SWControl[i].ChannelVoltSwitch(type, 0);        //正极对负极
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

            int theCHCount = (ClsGlobal.TrayType + 1) / 3;

            try
            {
                ManualTest.ClearDgv("ACIR_BT4");
                mManualTestFinish = false;
                mManualTestStop = false;

                #region 测内阻 BT4560

                Thread.Sleep(10);
                SWControl[0].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                SWControl[1].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                SWControl[2].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                Action actA = delegate
                {
                    for (int Num = 0; Num < theCHCount; Num++)
                    {

                        SWControl[0].ChannelAcirSwitchContr(2, Num + 1);     //内阻测量
                        Thread.Sleep(140);
                        HIOKI4560[0].ReadRData(out theIRSample);
                        arrIMPVal[Num] = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num]);   //经过adjust

                        //数值显示
                        if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                        {
                            ManualTest.Invoke(new EventHandler(delegate
                            {
                                ManualTest.dgvManualTest.Rows[Num].Cells["ACIR_BT4"].Value = arrIMPVal[Num].ToString("F7");   //刷新界面

                            }));
                        }

                    }
                };

                Action actB = delegate
                {
                    for (int Num = 0; Num < theCHCount; Num++)
                    {

                        SWControl[1].ChannelAcirSwitchContr(2, Num + 1);     //内阻测量
                        Thread.Sleep(150);
                        HIOKI4560[1].ReadRData(out theIRSample);
                        arrIMPVal[Num + 13] = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num + 13]);   //经过adjust

                        //数值显示
                        if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                        {
                            ManualTest.Invoke(new EventHandler(delegate
                            {
                                ManualTest.dgvManualTest.Rows[Num + 13].Cells["ACIR_BT4"].Value = arrIMPVal[Num + 13].ToString("F7");   //刷新界面

                            }));
                        }

                    }
                };
                Action actC = delegate
                {
                    for (int Num = 0; Num < theCHCount - 1; Num++)
                    {

                        SWControl[2].ChannelAcirSwitchContr(2, Num + 1);     //内阻测量
                        Thread.Sleep(150);
                        HIOKI4560[2].ReadRData(out theIRSample);
                        arrIMPVal[Num + 26] = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num + 26]);   //经过adjust

                        //数值显示
                        if (ManualTest.IsHandleCreated == true && mManualTestStop == false)
                        {
                            ManualTest.Invoke(new EventHandler(delegate
                            {
                                ManualTest.dgvManualTest.Rows[Num + 26].Cells["ACIR_BT4"].Value = arrIMPVal[Num + 26].ToString("F7");   //刷新界面

                            }));
                        }

                    }
                };
                var iarB = actB.BeginInvoke(null, null);
                var iarA = actA.BeginInvoke(null, null);
                var iarC = actC.BeginInvoke(null, null);
                actB.EndInvoke(iarB);
                actA.EndInvoke(iarA);
                actC.EndInvoke(iarC);

                SWControl[0].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                SWControl[1].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                SWControl[2].ChannelAcirSwitchContr(2, 0);     //结束,通道全部关断
                #endregion

                mManualTestFinish = true;

            }
            catch (Exception ex)
            {
                mManualTestFinish = true;
                MessageBox.Show(ex.Message);
            }

        }


        //手动电压清0
        private void ManualVoltZero()
        {
            int theCHCount = this.SWCount == 3 ? (ClsGlobal.TrayType + 1) / 3 : ClsGlobal.TrayType;
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
                for (int i = 0; i < this.SWCount; i++)
                {
                    for (int Num = 0; Num < theCHCount; Num++)
                    {
                        if (Num == 12 && i == 2)
                            continue;
                        if (mManualVoltZeroStop == true)
                        {
                            Thread.Sleep(50);
                            SWControl[i].ChannelVoltSwitch(1, 0);
                            break;
                        }

                        SWControl[i].ChannelVoltSwitch(1, Num + 1);         //电压切换
                        Thread.Sleep(200);
                        DMM_Ag344X.ReadVolt(out theVoltSample);       //电压值

                        theAdjusVolt = 0 - theVoltSample * 1000; //计算得到校准值
                                                                 //更改和保存校准值
                        INIAPI.INIWriteValue(ClsGlobal.mVoltAdjustPath, "VoltAdjust", "CH" + (Num + 1 + i * 13), theAdjusVolt.ToString("F4"));
                        //ClsGlobal.ArrVoltAdjustPara[Num - 1] = theAdjusVolt.ToString("F4");
                        //值显示
                        ManualAdjust.Invoke(new EventHandler(delegate
                        {
                            ManualAdjust.arrTxt_VoltZeroSampleShow[Num].Text = (theAdjusVolt).ToString("F4");
                        }));

                    }
                    ManualAdjust.Invoke(new EventHandler(delegate
                    {
                        ManualAdjust.btnVoltZeroAllStart.Text = "所有通道电压清零";
                        ManualAdjust.panelVoltZero.Enabled = true;
                        ManualAdjust.btnVoltZeroAllStart.Enabled = true;
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

            int theCHCount = (ClsGlobal.TrayType + 1) / 3;

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
                SWControl[0].ChannelAcirSwitchContr(2, 0);
                SWControl[1].ChannelAcirSwitchContr(2, 0);
                for (int i = 0; i < this.SWCount; i++)
                {
                    for (int Num = 0; Num < theCHCount; Num++)
                    {

                        if (mManualIRAdjustStop == true)
                        {
                            Thread.Sleep(50);
                            SWControl[i].ChannelAcirSwitchContr(2, 0);
                            break;
                        }
                        if (i == 2 && Num == 12)
                            continue;
                        SWControl[i].ChannelAcirSwitchContr(2, Num + 1);             //内阻通道选择          
                        Thread.Sleep(300);
                        HIOKI4560[i].ReadRData(out theIRSample);                //内阻采样

                        //显示阻值
                        ManualAdjust.Invoke(new EventHandler(delegate
                        {
                            if (theIRSample > 100)
                            {
                                ManualAdjust.arrTxt_IRSampleShow[Num + i * 13].BackColor = Color.Red;
                            }
                            else
                            {
                                ManualAdjust.arrTxt_IRSampleShow[Num + i * 13].BackColor = Color.LightGreen;
                            }

                            ManualAdjust.arrTxt_IRSampleShow[Num + i * 13].Text = (theIRSample * 1000).ToString("F3") + "/" +
                            (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num + i * 13])).ToString("F3");
                        }));
                    }
                }

                mManualIRAdjustFinish = true;
                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelIRAdjust.Enabled = true;
                    ManualAdjust.btnIRAdjustSampleAllStart.Enabled = true;
                    ManualAdjust.btnIRAdjustAllStart.Enabled = true;
                    ManualAdjust.btnIRAdjustAllValClr.Enabled = true;
                    ManualAdjust.lblNote_IRAdjust.Text = ("全部通道测试完成!");
                }));
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
            int theCHCount = (ClsGlobal.TrayType + 1) / 3;

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

                for (int i = 0; i < this.SWCount; i++)
                {
                    for (int Num = 0; Num < theCHCount; Num++)
                    {
                        if (mManualIRAdjustStop == true)
                        {
                            Thread.Sleep(50);
                            SWControl[i].ChannelAcirSwitchContr(2, 0);
                            break;
                        }
                        if (i == 2 && Num == 12)
                            continue;
                        SWControl[i].ChannelAcirSwitchContr(2, Num + 1);             //内阻通道选择          
                        Thread.Sleep(300);
                        HIOKI4560[i].ReadRData(out theIRSample);                //内阻采样

                        if (theIRSample * 1000 > 100)
                        {
                            ManualAdjust.Invoke(new EventHandler(delegate
                            {
                                ManualAdjust.lblNote_IRAdjust.Text = ("[通道" + ((Num + 1) + i * theCHCount) + "]:\r\n" + "测得阻值>100mΩ,校准失败!");
                            }));
                            break;
                        }

                        //计算得到校准值
                        theAdjust = theBaseVal - theIRSample * 1000;

                        //更改和保存校准值
                        INIAPI.INIWriteValue(ClsGlobal.mIRAdjustPath, "ACIRAdjust", "CH" + ((Num + 1) + i * theCHCount), theAdjust.ToString("F3"));
                        ClsGlobal.mIRAdjustVal[((Num + 1) + i * theCHCount) - 1] = theAdjust.ToString();

                        //显示阻值
                        if (ManualAdjust.IsHandleCreated == true && mManualIRAdjustStop == false)
                        {
                            ManualAdjust.Invoke(new EventHandler(delegate
                            {
                                ManualAdjust.arrTxt_IRSampleShow[Num + i * theCHCount].BackColor = Color.LightGreen;
                                ManualAdjust.arrTxt_IRSampleShow[Num + i * theCHCount].Text = (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[((Num + 1) + i * theCHCount) - 1])).ToString("F3");

                            }));
                        }
                    }
                }
                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelIRAdjust.Enabled = true;
                    ManualAdjust.btnIRAdjustSampleAllStart.Enabled = true;
                    ManualAdjust.btnIRAdjustAllStart.Enabled = true;
                    ManualAdjust.btnIRAdjustAllValClr.Enabled = true;
                    ManualAdjust.lblNote_IRAdjust.Text = ("全部通道测试完成!");
                }));
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
            int theCHCount = (ClsGlobal.TrayType + 1) / 3;
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

                for (int i = 0; i < this.SWCount; i++)
                {
                    for (int Num = 0; Num < theCHCount; Num++)
                    {
                        if (mManualIRAdjustStop == true)
                        {
                            Thread.Sleep(50);
                            SWControl[i].ChannelAcirSwitchContr(2, 0);
                            break;
                        }
                        if (i == 2 && Num == 12)
                            continue;
                        SWControl[i].ChannelAcirSwitchContr(2, Num + 1);             //内阻通道选择          
                        Thread.Sleep(300);
                        HIOKI4560[i].ReadRData(out theIRSample);                //内阻采样

                        theIR = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[(Num + 1) + i * theCHCount - 1]);  //经过adjust
                                                                                                                            //计量测试
                        ManualAdjust.Invoke(new EventHandler(delegate
                        {
                            ManualAdjust.arrTxt_IRMeterShow[Num + i * theCHCount].Text = theIR.ToString("F3");
                            if (Math.Abs(theMeterVal - theIR) <= MeterErrRange)
                            {
                                ManualAdjust.arrTxt_IRMeterShow[Num + i * theCHCount].BackColor = Color.LightGreen;
                                ManualAdjust.mToolTip_IRMeter.SetToolTip(ManualAdjust.arrTxt_IRMeterShow[Num + i * theCHCount], "计量通过");
                                ManualAdjust.arrLbl_IRMeterJudge[Num + i * theCHCount].Text = "OK";
                            }
                            else
                            {
                                ManualAdjust.arrTxt_IRMeterShow[Num + i * theCHCount].BackColor = Color.Red;
                                ManualAdjust.mToolTip_IRMeter.SetToolTip(ManualAdjust.arrTxt_IRMeterShow[Num + i * theCHCount], "计量不通过");
                                ManualAdjust.arrLbl_IRMeterJudge[Num + i * theCHCount].Text = "NG";
                            }
                        }));
                    }
                }

                ManualAdjust.Invoke(new EventHandler(delegate
                {
                    ManualAdjust.panelIRMetering.Enabled = true;
                    ManualAdjust.btnIRMeteringAllStart.Enabled = true;
                }));
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
