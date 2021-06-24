
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using OCV.OCVLogs;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using DevInfo;
using System.Threading.Tasks;

namespace OCV
{
    //过程处理
    public delegate void InfoSend(string Info);             //委托处理流程记录
    public class ClsProcess
    {

        /// <summary>
        /// 输送部工作状态
        /// </summary>
        public enum eTransState
        {
            Stop = -1,      //停止
            Init = 0,       //初始状态
            Ready = 1,      //就绪状态
            TrayIn = 2,     //进托盘
            TestWork = 3,   //测试工作
            NGSel = 4,      //NG分选
            TrayOut = 5,     // 出托盘
                             //InAlarm = 9,     //报警状态
            TestAlarm = 10,   //测试报警
        }

        private string mUnit;                 //使用OCV的单元: A,B
        private Thread myThread;
        public eTransState mStateFlag;       //流程状态标识
        private int mStep;                    //步标识
        private bool mPauseFlag;              //暂停标识
        private bool mStopFlag;               //停止标识
        public bool mAlarmFlag;               //报警标识 
        //OCV测试
        public string TrayCode;          //托盘条码
        private string CurrTrayCode = "";          //当前托盘条码
        public string LastTrayCode;      //上一次托盘条码
        public bool isTestAgainState = false;    //是否复测状态的标识
        private int mRunState;                //过程处理的运行状态    

        /// <summary>
        /// 运行状态  0:停止  1:运行  2:暂停  3:报警
        /// </summary>
        public int RunState { get { return mRunState; } }
        private int mSaveStep;               //保存步
        private eTransState mSaveStateFlag;  //保存状态标识

        //信息输出
   
        public InfoSend mInfoSend;
        ClsPLCContr mPLCContr;
        AlarmInfo mAlarmInfo = new AlarmInfo();                 //报警信息     
        Mutex Mut = new Mutex();

        DBCOM_DevInfo mDBCOM_DevInfo;           //设备信息
        ClsTestAnalysis mTestAnalysis;          //数据分析

        public ManualScanCode ManScanCode;
        public delegate bool ManualScanCode(out string Code);
        private double[] ArrChannelTemp = new double[ClsGlobal.TrayType];      //温度数组
        private bool ExportToLocFinish = false;
        private bool ExportToServerFinish = false;
        private bool ErrDataSaveLocSqlFail = false;   //保存到本地数据库失败
        private bool ErrDataSaveQTFail = false;     //保存到擎天数据库失败
        private bool ErrDataSaveCSVFail = false;     //保存到本地csv失败
        private bool ErrUpLoadngData = false;     //保存NG信息失败
        private FrmSys mForm;

        TimeSpan Ts;                            //计时
        DateTime Time1;
        public ClsProcess(string Unit, InfoSend infoSend, ManualScanCode ManualScanCode, FrmSys frm)
        {
            mInfoSend = infoSend;
            ManScanCode = ManualScanCode;
            mPLCContr = ClsGlobal.mPLCContr;
            mDBCOM_DevInfo = new DBCOM_DevInfo(ClsGlobal.mDevInfoPath);     //设备信息初始化
            mTestAnalysis = new ClsTestAnalysis(mDBCOM_DevInfo);            //测试分析初始化
            mStopFlag = true;
            mRunState = 0;
            this.mForm = frm;
        }

        //启动线程
        public void StartAction()
        {
            try
            {
                mRunState = 1;
                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0);
                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_自动流程启动, 1);
                myThread = new Thread(ThreadProcess);
                mStateFlag = eTransState.Init;
                mStep = 1;
                isTestAgainState = false;
                mStopFlag = false;
                mPauseFlag = false;
                mAlarmFlag = false;
                myThread.Start();
            }
            catch (Exception ex)
            {
                mInfoSend(ex.ToString());
            }
        }

        //暂停
        public void Pause()
        {
            Mut.WaitOne();
            mPauseFlag = true;
            //mStepflag = mStep;          //记忆暂停时的工步
            Thread.Sleep(100);
            Mut.ReleaseMutex();
        }

        //手自动
        public void Plc_AutoManual(ushort val)
        {
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_手自动模式, val);
        }

        //继续
        public void Resume()
        {
            mPauseFlag = false;
            mRunState = 1;                  //运行状态
            //mStep = mStepflag;   
        }

        //结束 
        public void Stop()
        {
            mStopFlag = true;                //线程停止    
            mRunState = 0;                   //停止状态
            // mAlaFlag = true;
        }

        //清除报警
        public void ResetPlc_Alarm()
        {
            ////实现复位
            mInfoSend("清除报警");
            ClsGlobal.mAlarmFlag = false;
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示清除报警, 1);
            Thread.Sleep(500);
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示清除报警, 0);

        }
        //初始化
        public void InitPlc()
        {

            ClsGlobal.mAlarmFlag = false;
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0); //清除红灯
            mInfoSend("开始初始化设备...");
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_初始化, 1);
            ClsPLCValue.PlcValue.Plc_InitReply = 0;
            Action act = delegate
            {
                DateTime sTime = DateTime.Now;
                double dSeconds = 0;
                do
                {
                    dSeconds = (sTime - DateTime.Now).TotalSeconds;
                    if (ClsPLCValue.PlcValue.Plc_InitReply == 1)
                    {
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_初始化, 0);
                        mInfoSend("初始化完成");
                        break;
                    }
                    Thread.Sleep(100);
                } while (dSeconds < 30);
            };
            act.BeginInvoke(null, null);
        }

        //整体复位
        public void Reset()
        {
            mInfoSend("整体复位设备");
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_整体复位, 1);
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0);
            ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_蜂鸣器, (ushort)0);
            Action act = delegate
            {
                Thread.Sleep(1000);
                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_整体复位, 0);
            };
            act.BeginInvoke(null, null);
        }



        private void Plc_Reset()
        {
            int mResetStep = 0;
            while (true)
            {
                try
                {

                    switch (mResetStep)
                    {
                        case 0:
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0); //清除红灯
                            mInfoSend("开始初始化设备...");
                            mResetStep = 1;
                            break;
                        case 1:
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_初始化, 1);
                            mResetStep = 2;
                            break;
                        case 2:
                            if (ClsPLCValue.PlcValue.Plc_InitReply == 1)
                            {
                                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_初始化, 0);
                                mInfoSend("初始化完成");
                            }
                            mResetStep = 3;
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        //控制流程
        private void ThreadProcess()
        {
            int[] flag = new int[4];
            while (true)
            {
                try
                {
                    //停止
                    if (mStopFlag == true)
                    {

                        ClsGlobal.IsAWorking = false;
                        Thread.Sleep(50);
                        mPauseFlag = false;
                        mStateFlag = eTransState.Stop;                  //初始状态

                        isTestAgainState = false;                     //重测 
                        mStep = 1;
                        mRunState = 0;
                        //ClsGlobal.lstACIRErrNo.Clear();////清空数据  
                        //ClsGlobal.TestCount = 0;
                        ClsGlobal.OCVTestContr.StopAction();
                        Thread.Sleep(200);
                        ClsGlobal.OCV_TestState = eTestState.StopTest;
                        mInfoSend("程序停止!");
                        break;
                    }
                    //暂停
                    if (mPauseFlag == true)
                    {
                        Time1 = System.DateTime.Now;
                        mRunState = 2;                  //暂停状态                      
                        //mInfoSend("程序停暂停!");  
                        Thread.Sleep(100);
                        continue;
                    }

                    #region 异常处理

                    mAlarmInfo.GetAlarmInfo(ClsPLCValue.PlcValue.Plc_Alarm1);

                    if (ClsPLCValue.PlcValue.Plc_Alarm1 != 0 && ClsGlobal.mAlarmFlag == false)
                    {
                        //显示报警信息            
                        for (int i = 0; i < mAlarmInfo.arrValue.Length; i++)
                        {
                            if (mAlarmInfo.arrValue[i] == 1)
                            {
                                mInfoSend("异常:" + mAlarmInfo.dictAlarm[i]);
                            }
                        }
                        ClsGlobal.mAlarmFlag = true;
                    }
                    #endregion


                    switch (mStateFlag)
                    {
                        case eTransState.Init:
                            //初始状态
                            InitState();
                            break;
                        case eTransState.Ready:
                            //就绪状态
                            ReadyState();
                            break;
                        case eTransState.TrayIn:
                            //进入托盘 
                            TrayInState();
                            break;
                        case eTransState.TestWork:
                            //测试工作
                            TestWorkState();
                            break;
                        case eTransState.NGSel:
                            //NG分选
                            //NGSelState();
                            break;
                        case eTransState.TrayOut:
                            //排出托盘
                            TrayOutState();
                            break;
                        //case eTransState.InAlarm:
                        //    //报警状态
                        //    AlarmState();
                        //    break;
                        case eTransState.TestAlarm:
                            //报警状态
                            TestAlarmState();
                            break;
                        case eTransState.Stop:


                            break;
                    }

                    Thread.Sleep(100);

                }
                catch (Exception ex)
                {
                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                    ClsGlobal.mPLCContr.WriteDB("W11", (ushort)1);
                    mInfoSend("异常：程序停止" + ex.Message);
                    mStopFlag = true;
                }
            }
        }

        private void SetToStandby()
        {
            //mPLCContr.WriteDB(ClsDevAddr.PC_运行模式, 1);           //手动  
            //mPLCContr.WriteDB(ClsDevAddr.PC_测定结束, 3);           //NG待机
        }

        //初始状态
        private void InitState()
        {
            switch (mStep)
            {
                case 1:
                    mInfoSend("启动自动测试流程");
                    mRunState = 1;
                    if (ClsPLCValue.PlcValue.Plc_AutoManual == 1)           //设备在自动情况下,而且不在待机状态
                    {
                        //mInfoSend("设备复位");
                        //标识清0
                        //mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答扫码请求, 0);
                        //mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 0);
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 0);
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 0);
                        mStep = 3;
                    }
                    else
                    {
                        mInfoSend("设备处于非自动模式...");
                        mStopFlag = true;
                        mStep = 1;
                    }
                    break;
                case 2:

                    break;
                case 3:

                    mInfoSend("等待托盘进入");
                    mStateFlag = eTransState.Ready;  //就绪状态
                    mStep = 1;

                    break;
                default:
                    mStep = 0;
                    break;
            }
        }

        //就绪状态
        private void ReadyState()
        {
            switch (mStep)
            {
                case 1: //查询主机状态(网络)              
                    mStep = 2;
                    break;
                case 2: //参数设置    
                    //isTestAgainState = false;          //默认非重测情况
                    mStep = 3;
                    break;
                case 3:
                    mStateFlag = eTransState.TrayIn; //进托盘流程
                    mStep = 1;
                    break;
                default:
                    mStep = 0;
                    break;
            }

        }

        public void TrayInState(int stepNo)
        {
            mStep = stepNo;
            TrayInState();
        }

        private string OCVName = "";
        public void TrayInState(int stepNo, string name)
        {
            mStep = stepNo;
            OCVName = name;
            TrayInState();
        }

        //进托盘状态
        private void TrayInState()
        {
            short tmp, tmp1;

            switch (mStep)
            {
                case 1: //检查是否有托盘进入
                    if (ClsPLCValue.PlcValue.Plc_HaveTray == 1)
                    {
                        Time1 = System.DateTime.Now;
                        ClsGlobal.IsTestAgain = false;
                        ClsGlobal.IsRetest = false;
                        ClsGlobal.RetestList.Clear();
                        mInfoSend("托盘已进入.");
                        ClsGlobal.OCVTestContr.InitPara();
                        mInfoSend("等待PLC发请求测试信号...");
                        mStep = 2;
                    }
                    break;
                case 2:

                    if (ClsPLCValue.PlcValue.Plc_RequestTest == 1)
                    {
                        mInfoSend("托盘已压合PLC请求测试");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 1);
                        mInfoSend("PC应答测试请求");
                        mStep = 3;
                        Time1 = System.DateTime.Now;
                    }
                    break;
                case 3:  //是否有测试请求

                    if (ClsPLCValue.PlcValue.Plc_RequestTest == 0)
                    {
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 0);
                        mInfoSend("开始扫码...");
                        if (ClsGlobal.OCV_RunMode == eRunMode.GoAhead)
                        {
                            mStep = 12;
                            mInfoSend("排托盘模式,托盘直接排出...");
                        }
                        else
                        {
                            mStep = 4;
                        }
                    }
                    Ts = System.DateTime.Now - Time1;
                    if (Ts.TotalSeconds > 10)
                    {
                        mInfoSend("异常: 等待PLC除请求信号超时");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
                        mStopFlag = true;
                    }
                    break;
                case 4:
                    try
                    {

                        string temp = ClsGlobal.CodeScan.ReadCode();
                        //temp = "JHWX0001";
                        bool val = false;
                        if (temp == "NG" || temp == "ERROR")
                        {
                            Thread.Sleep(500);
                            for (int i = 0; i < 2; i++)
                            {
                                temp = ClsGlobal.CodeScan.ReadCode();

                                if (temp != "NG" && temp != "ERROR")
                                {
                                    break;
                                }
                                else
                                {
                                    Thread.Sleep(100);
                                }
                            }

                        }
                        if (temp == "NG" || temp == "ERROR")
                        {
                            mInfoSend("测试时,托盘条码扫描异常:" + temp + "! ,设备暂停");
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 2);

                            if (MessageBox.Show("是否手工输入条码?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0);
                                if (ManScanCode(out temp) == true)
                                {
                                    mInfoSend("扫码成功,托盘条码: " + temp.Trim());
                                    CurrTrayCode = Regex.Replace(temp.Trim(), "[^0-9A-Za-z_-]", "");
                                    val = true;
                                }
                            }
                            else
                            {
                                mPauseFlag = true;
                                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0);
                            }
                        }
                        else
                        {
                            // ClsWCSCOM.Instance.StepCheckin(temp.Trim(),ClsGlobal.OPEATION_ID);
                            mInfoSend("扫码成功,托盘条码为: " + temp.Trim());
                            CurrTrayCode = Regex.Replace(temp.Trim(), "[^0-9A-Za-z_-]", "");
                            if (ClsGlobal.CodeRegexCheck(CurrTrayCode, ClsGlobal.TrayCodeRegEx) == false)
                            {
                                mInfoSend("异常:获取[" + CurrTrayCode + "]条码不符合规则!");
                                mPauseFlag = true;
                                break;
                            }
                            else
                            {
                                val = true;
                            }
                        }

                        if (ClsGlobal.TrayCodeLengh != CurrTrayCode.Length)
                        {
                            mInfoSend("异常:获取[" + CurrTrayCode + "]条码长度不正确!");
                            mPauseFlag = true;
                            break;
                        }
                        else
                        {
                            if (val)
                            {
                                if (isTestAgainState == true && CurrTrayCode != TrayCode)
                                {
                                    mInfoSend("复测托盘条码异常，与上次不一致!");
                                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
                                    mStopFlag = true;
                                    break;
                                }
                                else
                                {
                                    TrayCode = CurrTrayCode;
                                    ClsGlobal.TraycodeA = TrayCode;
                                    Time1 = System.DateTime.Now;
                                    mStep = 5;
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                        mInfoSend("托盘条码扫描异常:" + ex.Message);
                        mStopFlag = true;
                        break;
                    }
                    break;

                case 5:
                    #region   托盘电池数据获取
                    //单机
                    if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                    {
                        mInfoSend("单机运行中,不获取托盘电池信息");
                        ClsGlobal.mDBCOM_OCV_QT.Get_ETCell_Offline(TrayCode, out ClsGlobal.listETCELL);
                        mInfoSend("单机测试使用默认工艺: 001");
                        ClsGlobal.MODEL_NO = "001";
                        mStep = 8;
                    }
                    else if (ClsGlobal.OCV_RunMode == eRunMode.GoAhead)
                    {
                        mStep = 12;
                        mInfoSend("排托盘模式,托盘直接排出...");

                    }
                    //正常                                
                    else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                    {
                        if (isTestAgainState == false)
                        {
                            //ClsGlobal.BattInfoReqFlag = -1;

                            mInfoSend("正在获取托盘[" + TrayCode + "] 当前的工序");
                            string nowstep = "";
                            if (ClsGlobal.IsLocalOCVType == 0 && ClsGlobal.IsAutoMode == true)//远程
                            {
                                int currType = ClsGlobal.WCSCOM.Get_NowStepFormWCS(TrayCode, out nowstep);

                                ClsGlobal.OCVType = currType;
                                ClsGlobal.OPEATION_ID = "OCV" + ClsGlobal.OCVType;
                                if (ClsGlobal.OCVType == 1 || ClsGlobal.OCVType == 2)
                                {
                                    ClsGlobal.TestType = 0;
                                }
                                if (ClsGlobal.OCVType == 3 && ClsGlobal.UIsettingTestType != 3)
                                {
                                    ClsGlobal.TestType = 2;
                                }
                                if (ClsGlobal.OCVType == 3 && ClsGlobal.UIsettingTestType == 3)
                                {
                                    ClsGlobal.TestType = 3;
                                }
                                if (currType != 2 && currType != 3)
                                {
                                    string txt = "得到的工序为OCV{0}，从WCS获取工序出错，\r\n请联系中控室处理。";
                                    string info = string.Format(txt, currType);
                                    using (FrmTopMessage frm = new FrmTopMessage(info))
                                    {
                                        frm.ShowDialog();
                                        break;
                                    }
                                }
                            }
                            if (ClsGlobal.IsAutoMode == true)
                                mInfoSend("获取托盘当前的工序信息为：" + nowstep);
                            else
                                mInfoSend("托盘当前的工序信息为：" + OCVName);
                            mInfoSend("正在获取托盘[" + TrayCode + "] 电池信息数据");
                            ClsGlobal.listETCELL = new List<ET_CELL>();

                            ClsGlobal.WCSCOM.Get_BattInfoFormWCS(TrayCode, out ClsGlobal.listETCELL);
                            var query = ClsGlobal.listETCELL.GroupBy(o => o.Class_ID).ToList();
                            if (query.Count >= 2)
                            {
                                mInfoSend("托盘内存在多种电芯区别码的电芯");

                                using (FrmTopMessage frm = new FrmTopMessage("托盘内存在多种电芯区别码的电芯"))
                                {
                                    frm.ShowDialog();
                                    throw new Exception("托盘内存在多种电芯区别码的电芯");
                                }
                            }
                            mInfoSend("托盘电池信息获取成功！");

                        }
                        else
                        {
                            //ClsGlobal.BattInfoReqFlag = 0;
                        }
                        mStep = 6;
                    }
                    break;
                #endregion
                case 6:
                    #region  刷新读取电池型号
                    for (int i = 0; i < ClsGlobal.TrayType; i++)
                    {
                        if (ClsGlobal.listETCELL[i].MODEL_NO != "" && ClsGlobal.listETCELL[i].MODEL_NO != null)
                        {
                            ClsGlobal.MODEL_NO = ClsGlobal.listETCELL[i].MODEL_NO.Trim().ToUpper();
                            //ClsGlobal.BATCH_NO = ClsGlobal.listETCELL[i].BATCH_NO.Trim().ToUpper();
                            //ClsGlobal.PROJECT_NO = ClsGlobal.listETCELL[i].PROJECT_NO.Trim().ToUpper();
                        }
                    }
                    if (ClsGlobal.MODEL_NO == "" || ClsGlobal.MODEL_NO == null)
                    {
                        mInfoSend("异常:获取[" + TrayCode + "]的电池型号异常!");
                        mStateFlag = eTransState.TestAlarm;
                    }
                    mStep = 8;
                    break;
                #endregion
                case 7:
                    #region 获取OCV1的数据
                    // 当设备测试OCV3时从本地数据库读取OCV2的值
                    string strMsn = "";
                    string FlowEndTime = "";
                    if (ClsGlobal.OCVType == 3 && isTestAgainState == false && ClsGlobal.ENVoltDrop == "Y")
                    {
                        strMsn = "正在获取托盘[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据";
                        mInfoSend(strMsn);
                        //获取
                        int ret = ClsGlobal.mDBCOM_OCV_QT.Get_BattInfo(ClsGlobal.OCVType, TrayCode, ref ClsGlobal.listETCELL);
                        if (ret == 1 || ret == 2)
                        {
                            strMsn = "异常:获取[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "数据异常,请查询此托盘是否有做OCV" + (ClsGlobal.OCVType - 1);
                            mInfoSend(strMsn);
                            mStateFlag = eTransState.TestAlarm;
                        }
                        else
                        {
                            strMsn = "托盘[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据获取成功";
                            mInfoSend(strMsn);
                            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                            {
                                if (ClsGlobal.listETCELL[i].TEST_DATE != "" && ClsGlobal.listETCELL[i].TEST_DATE != null)
                                {
                                    FlowEndTime = ClsGlobal.listETCELL[i].TEST_DATE;
                                    break;
                                }
                            }
                            DateTime TestTime;
                            if (DateTime.TryParse(FlowEndTime, out TestTime) == false)
                            {
                                strMsn = "异常:" + "托盘[" + TrayCode + "]无对应的OCV2的测试时间!";
                                mInfoSend(strMsn);
                                throw new Exception("测试被终止");
                            }
                            TimeSpan date = System.DateTime.Now - Convert.ToDateTime(FlowEndTime);
                            ClsGlobal.GetHours = date.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                        }
                    }
                    mStep = 9;
                    break;
                #endregion
                case 8:
                    #region 获取工艺信息
                    ClsGlobal.OCVTestContr.ShowCellid();
                    strMsn = "";
                    ClsGlobal.BattInfoReqFlag = -1;
                    strMsn = "正在获取托盘[" + TrayCode + "] 测试工艺";
                    mInfoSend(strMsn);
                    #region local
                    //单机local 
                    if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                    {
                        ClsGlobal.BattInfoReqFlag = ClsGetConfigDetail.GetProjectInfo(1, ClsGlobal.OCVType);
                        if (ClsGlobal.BattInfoReqFlag != 0)
                        {
                            if (ClsGlobal.BattInfoReqFlag == 20)
                            {
                                strMsn = "无工艺文件，请设置工艺！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 21)
                            {
                                strMsn = "托盘[" + TrayCode + "]此电池型号无此工艺参数，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 22)
                            {
                                strMsn = "托盘[" + TrayCode + "]此电池型号没有设置默认工艺，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 23)
                            {
                                strMsn = "托盘[" + TrayCode + "]此电池型号无对应工艺，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 24)
                            {
                                strMsn = "没有工艺文件！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 25)
                            {
                                strMsn = "获取工艺参数失败！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 26)
                            {
                                strMsn = "MES无对应工艺版本返回！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 27)
                            {
                                strMsn = "MES返回工艺中没有默认的工艺,请设置后重新启动！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 28)
                            {
                                strMsn = "MES无返回工艺参数信息返回！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 31)
                            {
                                strMsn = "服务器无此电池型号工艺，请设置。";
                            }
                            mInfoSend(strMsn);
                            mStateFlag = eTransState.TestAlarm;
                        }
                        else
                        {
                            strMsn = "托盘[" + TrayCode + "]获取电池工艺成功！";
                            mInfoSend(strMsn);
                            mStep = 7;
                        }
                    }
                    #endregion 
                    //校准
                    else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                    {

                    }
                    //正常                                
                    else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                    {
                        if (isTestAgainState == false)
                        {
                            ClsGlobal.BattInfoReqFlag = ClsGetConfigDetail.GetProjectInfo(ClsGlobal.ProjectSetType, ClsGlobal.OCVType);
                        }
                        else
                        {
                            ClsGlobal.BattInfoReqFlag = 0;
                        }
                        if (ClsGlobal.BattInfoReqFlag != 0)
                        {
                            if (ClsGlobal.BattInfoReqFlag == 20)
                            {
                                strMsn = "无工艺文件，请设置工艺！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 21)
                            {
                                strMsn = "托盘[" + TrayCode + "]此电池型号无工艺，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 22)
                            {
                                strMsn = "托盘[" + TrayCode + "]此电池型号没有设置默认工艺，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 23)
                            {
                                strMsn = "托盘[" + TrayCode + "]此电池型号无对应工艺，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 24)
                            {
                                strMsn = "没有工艺文件！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 25)
                            {
                                strMsn = "获取工艺参数失败！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 26)
                            {
                                strMsn = "MES无对应工艺版本返回！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 27)
                            {
                                strMsn = "MES返回工艺中没有默认的工艺,请设置后重新启动！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 28)
                            {
                                strMsn = "MES无返回工艺参数信息返回！";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 31)
                            {
                                strMsn = "服务器中无此电池型号工艺，请设置。";
                            }
                            else if (ClsGlobal.BattInfoReqFlag == 32)
                            {
                                strMsn = "此电池型号在服务器中无要做工艺的参数，请设置。";
                            }
                            mInfoSend(strMsn);
                            using (FrmTopMessage frm = new FrmTopMessage(strMsn))
                            {
                                frm.ShowDialog();
                            }
                            mStateFlag = eTransState.TestAlarm;
                            break;
                        }
                        else
                        {
                            strMsn = "托盘[" + TrayCode + "]获取电池工艺成功！";
                            mInfoSend(strMsn);
                            if (ClsGlobal.OCVType == 3 && ClsGlobal.ENVoltDrop == "Y")
                            {
                                string txt = "正在获取托盘[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据";
                                mInfoSend(txt);
                                //获取
                                int ret = ClsGlobal.mDBCOM_OCV_QT.Get_BattInfo(ClsGlobal.OCVType, TrayCode, ref ClsGlobal.listETCELL);
                                if (ret == 1 || ret == 2)
                                {
                                    strMsn = "异常:获取[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "数据异常,请查询此托盘是否有做OCV" + (ClsGlobal.OCVType - 1);
                                    mInfoSend(strMsn);
                                    mStateFlag = eTransState.TestAlarm;
                                }
                                else
                                {
                                    strMsn = "托盘[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据获取成功";
                                    mInfoSend(strMsn);
                                    string FlowEndTimex = "";
                                    for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                                    {
                                        if (ClsGlobal.listETCELL[i].TEST_DATE != "" && ClsGlobal.listETCELL[i].TEST_DATE != null)
                                        {
                                            FlowEndTimex = ClsGlobal.listETCELL[i].TEST_DATE;
                                            break;
                                        }
                                    }
                                    DateTime TestTime;
                                    if (DateTime.TryParse(FlowEndTimex, out TestTime) == false)
                                    {
                                        strMsn = "异常:" + "托盘[" + TrayCode + "]无对应的OCV2的测试时间!";
                                        mInfoSend(strMsn);
                                        throw new Exception("测试被终止");
                                    }
                                    TimeSpan date = System.DateTime.Now - Convert.ToDateTime(FlowEndTimex);
                                    ClsGlobal.GetHours = date.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                                }
                            }
                            mStep = 15;
                        }
                    }
                    break;

                #endregion
                case 9:
                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 1);
                    Time1 = System.DateTime.Now;
                    mStep = 10;
                    break;
                case 10:
                    if (ClsPLCValue.PlcValue.Plc_ScanFinshReply == 1)
                    {
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 0);
                        mStep = 11;
                    }
                    Ts = DateTime.Now - Time1;
                    if (Ts.TotalSeconds > 10)
                    {
                        mInfoSend("异常: 超时，PLC未应答扫码完成信号,停止测试");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                        mStopFlag = true;
                        break;
                    }
                    break;
                case 11:
                    if (ClsPLCValue.PlcValue.Plc_ScanFinshReply == 0)
                    {
                        mStep = 15;
                    }
                    Ts = DateTime.Now - Time1;
                    if (Ts.TotalSeconds > 10)
                    {
                        mInfoSend("异常: 超时，PLC未清除扫码扫码完成应答信号,停止测试");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                        mStopFlag = true;
                    }
                    break;
                case 12:   //直接排出去
                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 2);
                    Time1 = System.DateTime.Now;
                    mStep = 13;
                    break;
                case 13:
                    if (ClsPLCValue.PlcValue.Plc_ScanFinshReply == 2)
                    {
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 0);
                        Time1 = DateTime.Now;
                        mStep = 14;
                    }
                    Ts = DateTime.Now - Time1;
                    if (Ts.TotalSeconds > 10)
                    {
                        mInfoSend("异常: 超时，PLC未应答扫码完成信号,停止测试");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                        mStopFlag = true;
                        break;
                    }

                    break;
                case 14:
                    if (ClsPLCValue.PlcValue.Plc_ScanFinshReply == 0)
                    {
                        mStep = 16;
                    }
                    Ts = DateTime.Now - Time1;
                    if (Ts.TotalSeconds > 10)
                    {
                        mInfoSend("异常: 超时，PLC未清除扫码扫码完成应答信号,停止测试");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                        mStopFlag = true;
                    }
                    break;
                case 15:
                    mStateFlag = eTransState.TestWork;
                    mStep = 1;
                    break;
                case 16:
                    mStateFlag = eTransState.TrayOut;
                    mStep = 1;
                    break;
                default:
                    mStep = 0;
                    break;
            }

        }

        public void TestWorkState(int stepNo)
        {
            mStep = stepNo;
            TestWorkState();
        }

        //测试状态
        private void TestWorkState()
        {
            short tmp;
            try
            {
                switch (mStep)
                {
                    case 1:     //确认电池类型  
                        //mInfoSend("等待PLC发送启动测试请求...");
                        Time1 = DateTime.Now;
                        mStep = 4;
                        break;
                    case 2:    //是否有测试请求        
                        if (ClsPLCValue.PlcValue.Plc_RequestTest == 1)
                        {
                            mInfoSend("PLC请求启动测试");
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 1);   //请求应答->1
                            mStep = 3;
                            Time1 = DateTime.Now;
                            break;
                        }
                        Ts = DateTime.Now - Time1;
                        if (Ts.TotalSeconds > 300)
                        {
                            mStateFlag = eTransState.TestAlarm;
                            mInfoSend("异常: 获取PLC请求测试信号超时,PLC长时间未反馈,程序停止");
                        }
                        break;
                    case 3:   //确认请求测试 
                        if (ClsPLCValue.PlcValue.Plc_RequestTest == 0)
                        {
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 0);    //应答清0  
                            mStep = 4;
                        }
                        Ts = DateTime.Now - Time1;
                        if (Ts.TotalSeconds > 30)
                        {
                            mInfoSend("异常: 超时,PLC未清除请求测试信号,程序停止");
                            mStateFlag = eTransState.TestAlarm;
                        }
                        break;
                    case 4:
                        mStep = 5;
                        break;
                    case 5:   //OCV测试    
                        #region 测试温度
                        ClsGlobal.TestStartTime = System.DateTime.Now;
                        if (ClsGlobal.OCV_TestState == eTestState.TestAgain)
                        {
                            mTestTempFinish = true;
                        }
                        else if (ClsGlobal.EN_TestTemp == 1)
                        {
                            mInfoSend("开始测试温度");
                        
                            StartTestTempAction();

                        }
                        else
                        {
                            mTestTempFinish = true;
                        }
                        mStep = 6;
                        #endregion
                        break;

                    case 6:
                        if (mTestTempFinish == true)
                        {
                            #region OCV测试
                            if (ClsGlobal.OCV_TestState != eTestState.TestAgain)
                            {
                                mInfoSend("温度测试完成.");
                            }
                            mInfoSend("开始测试工序:OCV" + ClsGlobal.OCVType.ToString());
                            //测试控制参数更新
                            ClsGlobal.OCVTestContr.StartTestAction();
                            mInfoSend("OCV" + ClsGlobal.OCVType.ToString() + "测试中...");
                            mStep = 7;
                            #endregion
                        }
                        break;
                    case 7:
                        #region OCV测试完成判断
                        //异常判断
                        if (ClsGlobal.OCVTestContr.AutoTestFinish == false && ClsGlobal.OCV_TestState == eTestState.ErrOCVTest)
                        {
                            mInfoSend("OCV测试异常! " + ClsGlobal.OCV_TestErrDetail);
                            mStateFlag = eTransState.TestAlarm;
                        }
                        if (ClsGlobal.OCVTestContr.AutoTestFinish == true)
                        {
                            mStep = 8;
                        }
                        #endregion
                        break;
                    case 8:
                        #region 数据处理
                        if (ClsGlobal.OCVTestContr.AutoTestFinish == true && mTestTempFinish == true)
                        {

                            //根据运行模式进行处理
                            //脱机测试
                            if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                            {
                                if (ClsGlobal.OCV_TestState == eTestState.TestOK)
                                {
                                    mInfoSend("单机测试OK，不保存数据库数据!");
                                    ExportLocalData();      //保存本地后结束    
                                    ClsGlobal.OCV_TestState = eTestState.OfflineTestEnd;
                                }
                                mStep = 11;
                            }
                            #region 正常测试
                            else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                            {
                                if (ClsGlobal.OCV_TestState == eTestState.TestOK)   //测试OK
                                {
                                    mInfoSend("OCV" + ClsGlobal.OCVType + "测试完成");
                                    mStep = 9;
                                }
                                else if (ClsGlobal.OCV_TestState == eTestState.TestAgain)  //再测定
                                {
                                    //mInfoSend("再测定...");
                                    mStep = 11;       //重新测量
                                }
                            }
                            #endregion

                            #region 校准测试

                            else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                            {
                                if (ClsGlobal.OCV_TestState == eTestState.AdjustOK)
                                {
                                    mInfoSend("校准数据保存");
                                    //SaveAdjustData();
                                    //复位校准参数
                                    ClsGlobal.IRTrueValSetFlag = false;
                                    ClsGlobal.AdjustCount = 0;
                                    ClsGlobal.ArrAdjustACIR = new double[88];
                                }
                                mStep = 11;
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    case 9:
                        #region 保存数据
                        mInfoSend("保存数据...");
                        var query = (from p in ClsGlobal.listETCELL where p.Cell_ID.Contains("NullCellCode") select p).ToList();
                        if (query.Count > 0)
                        {
                            mInfoSend("存在空条码， 保存数据失败！");
                            throw new Exception("存在空条码， 保存数据失败");
                        }
                        //联机测试下保存
                        ExportToLocFinish = false;
                        ExportToServerFinish = false;
                        ErrDataSaveCSVFail = false;
                        ErrDataSaveQTFail = false;
                        ErrUpLoadngData = false;
                        ExportLocalData();
                        if (ExportToServer() == false)
                        {
                            throw new Exception("上传擎天数据库失败");
                        }

                        if (ClsGlobal.ExpEnable == 1) // is enable
                        {
                            #region 异常统计
                            var trayExpList = EventLogBLL.Instance.GetExpTrayExpNum();
                            if (trayExpList != null && trayExpList.Count > 0)
                            {
                                string info = GetChannelExpInfo(trayExpList);
                                ClsGlobal.mPLCContr.WriteDB("W11", (ushort)1);//蜂鸣器
                                using (FrmTopMessage frm = new FrmTopMessage(info, trayExpList, "OCVLog"))
                                {
                                    frm.ShowDialog();
                                }
                            }
                            var channellist = ChannelExpCountBLL.Instance.ChannelAlaysis(ClsGlobal.listETCELL);
                            if (channellist != null && channellist.Count > 0)
                            {
                                var info = GetChannelExpInfo(channellist);
                                ClsGlobal.mPLCContr.WriteDB("W11", (ushort)1);//蜂鸣器
                                List<ExpData> expDatas = new List<ExpData>();
                                foreach (var item in channellist)
                                {
                                    ExpData data = new ExpData() { ChannelNo = item.ChannelNo.ToString(), ExpCount = 1 };
                                    expDatas.Add(data);
                                }

                                using (FrmTopMessage frm = new FrmTopMessage(info, expDatas, "ChannelExpCount"))
                                {
                                    frm.ShowDialog();
                                }
                            }
                            #endregion 异常统计
                        }
                        ExportToServerFinish = true;
                        ExportToLocFinish = true;
                        //保存NG信息到wcs

                        if (ClsGlobal.OCV_RunMode != eRunMode.OfflineTest)
                        {
                            //var resultCheckin = ClsWCSCOM.Instance.StepCheckin(TrayCode, "OCV" + ClsGlobal.OCVType);
                            //if (resultCheckin.code != 200)
                            //{

                            //    throw new Exception("物流系统checkin方法返回异常，返回值为：" + resultCheckin.msg);
                            //}
                            //var resultChange = ClsWCSCOM.Instance.StepChange(TrayCode, "1", "OCV" + ClsGlobal.OCVType);
                            //if (resultChange.code != 200)
                            //{
                            //    throw new Exception("物流系统StepChang方法返回异常，返回值为：" + resultChange.msg);
                            //}
                            if (UploadNGToWsc() == false)
                            {
                                throw new Exception("结果上传WCS失败！");
                            }
                            else
                            {
                                ErrUpLoadngData = true;
                            }
                        }
                        else
                        {
                            ErrUpLoadngData = true;
                        }
                        mStep = 10;
                        #endregion
                        break;
                    case 10:

                        #region 保存数据完成
                        if (ErrDataSaveQTFail == true || ErrDataSaveQTFail == true || ErrDataSaveCSVFail == true)
                        {
                            mStateFlag = eTransState.TestAlarm;
                        }
                        if (ExportToLocFinish == true && ExportToServerFinish == true && ErrUpLoadngData == true)
                        {
                            ClsGlobal.OCV_TestState = eTestState.TestEnd;

                            mStep = 11;
                        }
                        #endregion

                        break;
                    case 11:   //OCV测试         

                        if (ClsGlobal.OCV_TestState == eTestState.TestAgain)           //处理再次测试
                        {
                            mInfoSend("再次测试...");
                            mStep = 16;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.AdjustAgain)
                        {
                            mInfoSend("再次校准...");
                            mStep = 16;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.AdjustOK)            //校准完成
                        {
                            mInfoSend("内阻校准完成");
                            mStep = 19;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.OfflineTestEnd)      //单机测试运行
                        {
                            mInfoSend("单机测试完成...");
                            mStep = 12;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.TestEnd)
                        {
                            mInfoSend("OCV" + ClsGlobal.OCVType + "数据上传完成");
                            Thread.Sleep(500);
                            mStep = 12;
                        }
                        break;
                    case 12:  //测试数据分析
                        List<int> lstOCVChannelNo = new List<int>();
                        List<int> lstShellChannelNo = new List<int>();
                        List<int> lstACIRChannelNo = new List<int>();
                        bool ret = false;
                        bool errFlag = false;
                        string str;

                        List<DevInfo.Model.SET_Info> lstSetInfo;
                        mDBCOM_DevInfo = new DBCOM_DevInfo(ClsGlobal.mDevInfoPath);
                        DevInfo.Model.SET_Info mSET_Info = new DevInfo.Model.SET_Info();

                        //获取参数
                        mDBCOM_DevInfo.GetSetInfoList(out lstSetInfo);

                        mSET_Info.SetName = lstSetInfo[0].SetName;
                        mSET_Info.OCV_SetEN = lstSetInfo[0].OCV_SetEN;
                        mSET_Info.OCV_UCL = lstSetInfo[0].OCV_UCL;
                        mSET_Info.OCV_LCL = lstSetInfo[0].OCV_LCL;
                        mSET_Info.OCV_TestTimes = lstSetInfo[0].OCV_TestTimes;

                        mSET_Info.Shell_SetEN = lstSetInfo[0].Shell_SetEN;
                        mSET_Info.Shell_UCL = lstSetInfo[0].Shell_UCL;
                        mSET_Info.Shell_LCL = lstSetInfo[0].Shell_LCL;
                        mSET_Info.Shell_TestTimes = lstSetInfo[0].Shell_TestTimes;

                        mSET_Info.ACIR_SetEN = lstSetInfo[0].ACIR_SetEN; ;
                        mSET_Info.ACIR_UCL = lstSetInfo[0].ACIR_UCL; ;
                        mSET_Info.ACIR_LCL = lstSetInfo[0].ACIR_LCL;
                        mSET_Info.ACIR_TestTimes = lstSetInfo[0].ACIR_TestTimes;

                        //刷新设置信息
                        mTestAnalysis.RefreshSetInfo();

                        //OCV异常分析
                        if (mSET_Info.OCV_SetEN == true)
                        {
                            if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                            {
                                ret = mTestAnalysis.OCVAnalysis(ClsGlobal.listETCELL, 2, out lstOCVChannelNo);
                            }
                            else
                            {
                                ret = mTestAnalysis.OCVAnalysis(ClsGlobal.listETCELL, 1, out lstOCVChannelNo);
                            }

                            if (ret == true)
                            {
                                str = "";
                                for (int i = 0; i < lstOCVChannelNo.Count; i++)
                                {
                                    str += lstOCVChannelNo[i] + ",";
                                }
                                mInfoSend("异常:通道[" + str.Substring(0, str.Length - 1) + "]出现电压测试值连续异常,请检查对应通道探针是否有异常!");
                                errFlag = true;
                            }
                        }

                        //壳体数据分析
                        if (mSET_Info.Shell_SetEN == true)
                        {
                            if (ClsGlobal.TestType == 1 && ClsGlobal.TestType == 2)
                            {
                                if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                                {
                                    ret = mTestAnalysis.ShellAnalysis(ClsGlobal.listETCELL, 2, out lstShellChannelNo);
                                }
                                else
                                {
                                    ret = mTestAnalysis.ShellAnalysis(ClsGlobal.listETCELL, 1, out lstShellChannelNo);
                                }
                                if (ret == true)
                                {
                                    str = "";
                                    for (int i = 0; i < lstShellChannelNo.Count; i++)
                                    {
                                        str += lstShellChannelNo[i] + ",";
                                    }
                                    mInfoSend("异常:通道[" + str.Substring(0, str.Length - 1) + "]出现壳体测试连续异常，请检查对应通道探针是否有异常!");
                                    errFlag = true;
                                }
                            }
                        }

                        //ACIR数据分析
                        if (mSET_Info.ACIR_SetEN == true)
                        {
                            if (ClsGlobal.TestType == 2)
                            {
                                if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                                {
                                    ret = mTestAnalysis.ACIRAnalysis(ClsGlobal.listETCELL, 2, out lstACIRChannelNo);
                                }
                                else
                                {
                                    ret = mTestAnalysis.ACIRAnalysis(ClsGlobal.listETCELL, 1, out lstACIRChannelNo);
                                }

                                if (ret == true)
                                {
                                    str = "";
                                    for (int i = 0; i < lstACIRChannelNo.Count; i++)
                                    {
                                        str += lstACIRChannelNo[i] + ",";
                                    }
                                    mInfoSend("异常:通道[" + str.Substring(0, str.Length - 1) + "]出现ACIR测试连续异常，请检查对应通道探针是否有异常!");
                                    errFlag = true;
                                }
                            }

                        }

                        //有异常情况时处理                    
                        if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                        {
                            if (errFlag == true)
                            {
                                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                                mInfoSend("确认是否继续...");
                                if (MessageBox.Show("测试通道出现异常,请确认是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    mStep = 13;
                                }
                                else
                                {
                                    //设备转到待机状态
                                    mStateFlag = eTransState.TestAlarm;
                                }
                            }
                            else
                            {
                                mStep = 13;
                            }
                        }
                        else if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                        {
                            mInfoSend("单机模式测试结束!");
                            mStep = 13;     //设备到待机、
                        }
                        break;
                    //测试完成----------------------------------------------
                    case 13:   //测试完成
                        isTestAgainState = false;                 //不重测

                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 1);     //测试完成,搬送托盘
                        mInfoSend("发送测试结束信号给PLC");

                        mStep = 14;
                        break;
                    case 14:   //测试完成应答
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply == 1)
                        {
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 0);
                            // Time1 = DateTime.Now;
                            mStep = 15;
                        }
                        //Ts = System.DateTime.Now - Time1;
                        //if (Ts.Seconds > 30)
                        //{
                        //   // mInfoSend("异常:PLC应答测定结束超时");
                        //    //mStateFlag = eTransState.TestAlarm;
                        //}
                        break;
                    case 15:   //测试完成清0
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply == 0)
                        {
                            mInfoSend("测试完成等待移出托盘...");
                            // mStateFlag = eTransState.TrayOut;
                            mStep = 20;
                            // mStep = 1;
                        }

                        //Ts = System.DateTime.Now - Time1;
                        //if (Ts.Seconds > 30)
                        //{
                        //    mInfoSend("异常:PLC清除应答测定结束信号超时");
                        //    mStateFlag = eTransState.TestAlarm;
                        //}
                        break;
                    //重测 ---------------------------------------------------
                    case 16:
                        isTestAgainState = true;                     //重测 
                        if (ClsGlobal.OCV_TestState == eTestState.TestAgain)
                        {
                            string Msn = "";
                            mInfoSend(Msn + "有通道异常，进行复测");
                        }
                        Time1 = DateTime.Now;
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 2);        //再次测定
                        mStep = 17;
                        break;
                    case 17:
                        if (ClsPLCValue.PlcValue.Plc_RequestTest == 1)
                        {

                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 0);
                            Time1 = DateTime.Now;
                            mStep = 18;
                        }
                        Ts = DateTime.Now - Time1;
                        if (Ts.Seconds > 200000)
                        {
                            mInfoSend("异常:PLC复测应答测定超时");
                            mStateFlag = eTransState.TestAlarm;
                        }
                        break;
                    case 18:
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply == 0)
                        {
                            mStateFlag = eTransState.TestWork;                //复测，到托盘进入状态
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 1);
                            mStep = 1;
                        }

                        Ts = System.DateTime.Now - Time1;
                        if (Ts.Seconds > 20)
                        {
                            mInfoSend("异常:PLC复测应答测定超时");
                            mStateFlag = eTransState.TestAlarm;
                        }
                        break;
                    //OCV测试有异常,待机-----------------------------------------------------
                    case 19:
                        //mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC指示电池检测完成, 3);        //测试待机状态，报警处                                                    //mStopFlag = true;
                        mStep = 20;   //单机测试结束
                        break;
                    case 20:
                        if (ClsPLCValue.PlcValue.Plc_AutoStepNO >= 65 || ClsPLCValue.PlcValue.Plc_AutoStepNO == 5)
                        {
                            mInfoSend("托盘移除出...");
                            mStateFlag = eTransState.TrayOut;
                            mStep = 1;
                        }
                        break;
                    case 21:
                        //mPLCContr.ReadDB(mPLCContr.mPlcAddr.PLC应答电池检测完成, out tmp);
                        //if (tmp == 0)
                        //{
                        //    mStopFlag = true;
                        //}
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //出托盘
        private void TrayOutState()
        {
            short tmp;
            switch (mStep)
            {
                case 1:
                    mStep = 2;
                    break;
                case 2:
                    if (ClsPLCValue.PlcValue.Plc_HaveTray == 0)
                    {
                        mStep = 3;
                    }
                    break;
                case 3:
                    mInfoSend("托盘移出完成");
                    var checkOutResult = ClsWCSCOM.Instance.StepCheckOut("OCV" + ClsGlobal.OCVType, ClsGlobal.TraycodeA);
                    if (checkOutResult.code != 200)
                    {
                        mInfoSend("出站设置失败！" + checkOutResult.msg);
                        throw new Exception("出站设置失败");
                    }
                    var StaionChangeResult = ClsWCSCOM.Instance.StepTrayStaionChange(LocStatusType.Free, ClsGlobal.TraycodeA);
                    if (StaionChangeResult.code != 200)
                    {
                        mInfoSend("工作状态设置失败！" + StaionChangeResult.msg);
                    }
                    mInfoSend("等待托盘进入...");
                    mStateFlag = eTransState.TrayIn;
                    mStep = 1;
                    break;
                default:
                    mStep = 0;
                    break;
            }
        }
        //报警状态
        private void TestAlarmState()
        {
            mStep = 1;
            short tmp;
            switch (mStep)
            {

                //OCV测试有异常,待机-----------------------------------------------------
                case 1:
                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 8);
                    Stop();
                    break;
            }
        }

        //获取电池信息
        Thread ThreadTestAction;
        public void GetBatInfoFromQT(string Pallet_ID)
        {
            ThreadTestAction = new Thread(new ParameterizedThreadStart(mGet_BattInfo));
            ThreadTestAction.Start(Pallet_ID);
        }

        private void mGet_BattInfo(object Para)
        {
            try
            {
                //ClsGlobal.BattInfoReqFlag = ClsGlobal.mDBCOM_SVSQT.Get_BattInfo(Para.ToString(), out ClsGlobal.listETCELL);
            }
            catch (Exception ex)
            {
                string strMsg = "查询对应电池数据异常";
                ClsLogs.OCVInfologNet.WriteFatal(mUnit, strMsg + ex.Message);
                ClsLogs.SqllogNet.WriteFatal(mUnit, strMsg + ex.Message);
                ClsGlobal.BattInfoReqFlag = 1;
            }
        }

        private bool mTestTempFinish;

        public void SetTempFinish(bool value)
        {
            mTestTempFinish = value;
        }
        //测试流程
        private void StartTestTempAction()
        {
            mTestTempFinish = false;
            ThreadTestAction = new Thread(new ThreadStart(TestTemp));
            ThreadTestAction.IsBackground = true;
            ThreadTestAction.Start();
        }

        //测试电池温度流程

        private void TestTemp()
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
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {

                    //正极温度
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[i]);    //温度通道映射
                    //ClsGlobal.G_dbl_P_TempArr[i] = ArrChannelTemp[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);
                    ClsGlobal.G_dbl_P_TempArr[i] = ClsGlobal.TempContr.Anodetemperature[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);
                    //负极温度
                    ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[i]);
                    //ClsGlobal.G_dbl_N_TempArr[i] = ArrChannelTemp[ActualNum - 1 + 38] + double.Parse(ClsGlobal.mTempAdjustVal_N[ActualNum - 1]);
                    ClsGlobal.G_dbl_N_TempArr[i] = ClsGlobal.TempContr.Poletemperature[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);

                    ClsGlobal.listETCELL[i].PostiveTMP = 0;//ClsGlobal.G_dbl_P_TempArr[i];
                    ClsGlobal.listETCELL[i].NegativeTMP = ClsGlobal.G_dbl_N_TempArr[i];
                    mForm.SetValueToUI(i, "Col_TEMP_P", ClsGlobal.listETCELL[i].PostiveTMP, 2);
                    mForm.SetValueToUI(i, "Col_TEMP_N", ClsGlobal.listETCELL[i].NegativeTMP, 2);

                }
                mTestTempFinish = true;
            }
            catch (Exception ex)
            {
                //测试温度异常   
                ClsGlobal.OCV_TestState = eTestState.ErrTempTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ExportLocalData()
        {
            DataTable dtTemplate = new DataTable();                             //表模板
            try
            {
                if (ClsGlobal.listETCELL.Count == 0)
                {
                    return;
                }

                //取得开始时间和结束时间
                string _sdtStartTime = ClsGlobal.TestStartTime.ToString("yyy-MM-dd HH:mm:ss");       //开始时间
                string _sdtEndTime = ClsGlobal.TestEndTime.ToString("yyy-MM-dd HH:mm:ss");           //结束时间 
                                                                                                     //  string _sExcelPath = Application.StartupPath;

                //创建导出数据文件夹
                string _sFilePath = ClsGlobal.ReportPath;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }

                //创建月份文件夹
                string _sYM = DateTime.Now.ToString("yyyy-MM-dd");
                _sFilePath = _sFilePath + "\\" + _sYM;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }
                string[] arr = ClsGlobal.TraycodeA.Split('-');
                string trayType = "";
                if (arr.Length >= 2)
                {
                    trayType = arr[1];
                }
                //创建OCV型号数据文件夹
                string _sOCVname = "OCV-" + ClsGlobal.OCVType;
                _sFilePath = _sFilePath + "\\" + _sOCVname+"\\"+ trayType;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }
                ExportDataToCSV(_sFilePath);
                // ExportDataToCSV_CSBYD(_sFilePath);
                ExpAnalysis();


            }
            catch (Exception ex)
            {

                ErrDataSaveCSVFail = true;     //保存到本地csv失败
                ExportToLocFinish = false;
                string strMsg = "保存本地CSV文件出错";
                ClsLogs.OCVInfologNet.WriteFatal(mUnit, strMsg + ex.Message);
                mInfoSend(strMsg);
                throw ex;
            }

        }
        /// <summary>
        /// 长沙比亚迪导出数据
        /// </summary>ti
        /// <param name="CSVPath"></param>
        private void ExportDataToCSV_CSBYD(string CSVPath)
        {
            string addr = CSVPath + "\\" + TrayCode + "_OCV" + ClsGlobal.OCVType.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            List<double> listVoltData = new List<double>();
            if (ClsGlobal.listETCELL == null)
            {
                return;
            }
            using (StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default))
            {
                SWR.WriteLine("序号,托盘码,电池条码,电池型号,电池区分,设备号,电池电压,正极电压,负极电压,ACIR内阻,温度(℃),比较结果,开始时间,结束时间,修正值OCV");
                for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                {
                    string cellTypeCode = ClsGlobal.listETCELL[i].Cell_ID.Substring(ClsGlobal.StartIndex - 1, ClsGlobal.BatTypeLen);
                    string cellClassCode = ClsGlobal.listETCELL[i].Cell_ID.Substring(ClsGlobal.StartIndex - 1, ClsGlobal.BatClassLen);
                    double acir = Math.Round(ClsGlobal.listETCELL[i].ACIR_Now, 4);
                    SWR.WriteLine(
                        ClsGlobal.listETCELL[i].Cell_Position + "," +
                        TrayCode + "," +
                        ClsGlobal.listETCELL[i].Cell_ID.ToString() + "," +
                        cellTypeCode + "," +
                        cellClassCode + "," +
                         "QT-" + ClsGlobal.DeviceCode + "," +
                        Math.Round(ClsGlobal.listETCELL[i].OCV_Now, 4).ToString() + "," +
                        Math.Round(ClsGlobal.listETCELL[i].OCV_ShellPostive_Now, 4).ToString() + "," +
                        Math.Round(ClsGlobal.listETCELL[i].OCV_Shell_Now, 4).ToString() + "," +
                         acir + "," +
                        ClsGlobal.listETCELL[i].PostiveTMP.ToString() + "," +
                        ClsGlobal.listETCELL[i].NgState + "," +
                        ClsGlobal.TestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "," +
                        ClsGlobal.TestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "," +
                        Math.Round(ClsGlobal.listETCELL[i].OCV_Now, 4).ToString()
                        );
                }
            }
            Thread.Sleep(200);
            Tools.FileHelper.SetFileReadAccess(addr, true);
        }
        private void ExportDataToCSV(string CSVPath)
        {
            string addr = CSVPath + "\\" + TrayCode + "_OCV" + ClsGlobal.OCVType.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            List<double> listVoltData = new List<double>();
            if (ClsGlobal.listETCELL == null)
            {
                return;
            }
            SaveDataCsv DataCsv = new SaveDataCsv();

            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
            {
                listVoltData.Add(ClsGlobal.listETCELL[i].OCV_Now);
            }
            StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default);
            SWR.WriteLine("[OCVResult]");
            SWR.WriteLine("[ProcessInfo]=" + ClsGlobal.Config);
            SWR.WriteLine("TrayNo=" + TrayCode);
            SWR.WriteLine("Start=" + ClsGlobal.TestStartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            SWR.WriteLine("End=" + ClsGlobal.TestEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            SWR.WriteLine("MinVolt=" + listVoltData.Min() + " " + "MaxVolt=" + listVoltData.Max());
            SWR.WriteLine("通道号, 托盘条码,电池条码,总判定结果,开路电压(mV),ACIR(mΩ),测试结果,NG原因,壳体/正极电压(mV),壳体/正极检测结果,壳体/正极NG原因,壳体/负极电压(mV),壳体/负极检测结果,壳体/负极NG原因,正极温度(℃),负极温度(℃),压降△V,压降判定,压降△V极差,压降△V极差判定结果,压降△V极差NG原因,ACIR极差,ACIR极差判定结果,ACIR极差NG原因,补偿前电压(mV),补偿后电压(mV),容量,测试时间,工站,设备编号,自放电时间(H)");
            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
            {
                DataCsv.Cell_Position = ClsGlobal.listETCELL[i].Cell_Position;
                DataCsv.Pallet_ID = TrayCode;
                DataCsv.Cell_ID = ClsGlobal.listETCELL[i].Cell_ID.ToString();
                DataCsv.OCV_Now = Math.Round(ClsGlobal.listETCELL[i].OCV_Now, 4).ToString();
                DataCsv.NgState = ClsGlobal.listETCELL[i].NgState;
                DataCsv.DevNo = ClsGlobal.JT_NO;
                DataCsv.DisChargeTime = 0;
                //OCV1无自放电
                if (ClsGlobal.OCVType == 3)
                {
                    DataCsv.DisChargeTime = ClsGlobal.GetHours;
                    if (ClsGlobal.ENVoltDrop == "Y")
                    {
                        DataCsv.VoltDrop_Now = ClsGlobal.listETCELL[i].VoltDrop_Now.ToString();
                        if (ClsGlobal.listETCELL[i].VoltDrop_NowState.NgState != null)
                            DataCsv.VoltDrop_NgResult = ClsGlobal.listETCELL[i].VoltDrop_NowState.NgState.ToString();
                        else
                            DataCsv.VoltDrop_NgResult = "";
                        if (ClsGlobal.IS_Enable_DropRange == "Y")
                        {
                            DataCsv.DROP_range = ClsGlobal.listETCELL[i].DROP_range.ToString();
                            DataCsv.DROP_NgResult = ClsGlobal.listETCELL[i].DROP_NgResult;
                        }
                    }
                }

                if (ClsGlobal.TestType > 0)
                {
                    DataCsv.OCV_Shell_Now = Math.Round(ClsGlobal.listETCELL[i].OCV_Shell_Now, 4).ToString();
                    DataCsv.SV_NgResult = ClsGlobal.listETCELL[i].SV_NgResult;

                    DataCsv.PostiveOCV_Shell_Now = Math.Round(ClsGlobal.listETCELL[i].OCV_ShellPostive_Now, 4).ToString();
                    DataCsv.PostiveSV_NgResult = ClsGlobal.listETCELL[i].PostiveSV_NgResult;
                }

                if (ClsGlobal.TestType == 2 || ClsGlobal.TestType == 3)
                {
                    if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                    {
                        DataCsv.ACIR_range = ClsGlobal.listETCELL[i].ACIR_range.ToString();
                        DataCsv.ACIR_NgResult = ClsGlobal.listETCELL[i].ACIR_NgResult;
                    }
                    DataCsv.ACIR_Now = Math.Round(ClsGlobal.listETCELL[i].ACIR_Now, 4).ToString();
                }
                DataCsv.Test_NgResult = ClsGlobal.listETCELL[i].Test_NgResult;
                DataCsv.PostiveTMP = ClsGlobal.listETCELL[i].PostiveTMP.ToString();
                DataCsv.NegativeTMP = ClsGlobal.listETCELL[i].NegativeTMP.ToString();
                DataCsv.Rev_OCV = ClsGlobal.listETCELL[i].Rev_OCV.ToString();
                //DataCsv.Capacity = ClsGlobal.listETCELL[i].Capacity.ToString();
                DataCsv.Capacity = "";
                //DataCsv.End_Write_Time = ClsGlobal.listETCELL[i].End_Write_Time;
                DataCsv.OCVType = "OCV" + ClsGlobal.OCVType.ToString();


                SWR.WriteLine(
                           DataCsv.Cell_Position + "," +
                           DataCsv.Pallet_ID + "," +
                           DataCsv.Cell_ID + "," +
                           DataCsv.NgState + "," +
                           DataCsv.OCV_Now + "," +
                           DataCsv.ACIR_Now + "," +
                           DataCsv.Test_NgResult.NgState + "," +
                           DataCsv.Test_NgResult.NgDescribe + "," +
                           DataCsv.PostiveOCV_Shell_Now + "," +
                           DataCsv.PostiveSV_NgResult.NgState + "," +
                           DataCsv.PostiveSV_NgResult.NgDescribe + "," +
                           DataCsv.OCV_Shell_Now + "," +
                           DataCsv.SV_NgResult.NgState + "," +
                           DataCsv.SV_NgResult.NgDescribe + "," +
                           DataCsv.PostiveTMP + "," +
                           DataCsv.NegativeTMP + "," +
                           DataCsv.VoltDrop_Now + "," +
                           DataCsv.VoltDrop_NgResult + "," +
                           DataCsv.DROP_range + "," +
                           DataCsv.DROP_NgResult.NgState + "," +
                           DataCsv.DROP_NgResult.NgDescribe + "," +
                           DataCsv.ACIR_range + "," +
                           DataCsv.ACIR_NgResult.NgState + "," +
                           DataCsv.ACIR_NgResult.NgDescribe + "," +
                           DataCsv.OCV_Now + "," +
                           DataCsv.Rev_OCV + "," +
                           DataCsv.Capacity + "," +
                           ClsGlobal.TestEndTime.ToString("yyy-MM-dd HH:mm:ss") + "," +
                           DataCsv.OCVType+","+
                           DataCsv.DevNo + "," +
                           DataCsv.DisChargeTime
                           );
            }
            SWR.Close();
            SWR = null;
        }
        //保存到服务器
        private bool ExportToServer()
        {
            string msn;
            int iResult = 0;
            try
            {
                ExportToServerFinish = false;
                //ExportToServerFinish = true;
                //return true;
                //本地与远程都上传
                try
                {

                    iResult = ClsGlobal.mDBCOM_OCV_QT.InsertOCVACIRData(ClsGlobal.OCVType, ClsGlobal.listETCELL, 1);
                    //int count = ClsGlobal.mDBCOM_OCV_QT.InsertOCVACIRData(ClsGlobal.OCVType, ClsGlobal.listETCELL, 1);
                    msn = "托盘[" + TrayCode + "]上传擎天数据库电池数据数量:" + iResult;
                    mInfoSend(msn);
                    if (iResult != 38)
                    {
                        throw new Exception("上传擎天数据库数量不符合条件,请重新测试");
                    }
                    if (iResult <= 0)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ErrDataSaveQTFail = true;
                    msn = "异常：上传擎天数据库失败";
                    mInfoSend(msn);
                    ClsLogs.OCVInfologNet.WriteFatal(mUnit, msn + ex.Message);
                    throw ex;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //给wcs传NG信息
        private bool UploadNGToWsc()
        {
            ErrUpLoadngData = false;
            try
            {
                var resultCheckin = ClsWCSCOM.Instance.StepCheckin(TrayCode, "OCV"+ ClsGlobal.OCVType);
                if (resultCheckin.code != 200)
                {

                    throw new Exception("物流系统checkin方法返回异常，返回值为：" + resultCheckin.msg);
                }
                var resultChange = ClsWCSCOM.Instance.StepChange(TrayCode, "1", "OCV" + ClsGlobal.OCVType);
                if (resultChange.code != 200)
                {
                    throw new Exception("物流系统StepChang方法返回异常，返回值为：" + resultChange.msg);
                }
                bool upload = ClsGlobal.WCSCOM.UpLoadngData(TrayCode, ClsGlobal.listETCELL);
                if (upload == true)
                {
                    mInfoSend("托盘[" + TrayCode + "] 电池NG数据上传成功");
                }
                return upload;
            }
            catch (Exception ex)
            {

                mInfoSend("托盘[" + TrayCode + "] " + ex.Message);
                mStateFlag = eTransState.TestAlarm;
                return false;
            }
            ErrUpLoadngData = true;

        }

        private void SaveAdjustData()
        {
            //ClsGlobal.SetIRAdjust(ClsGlobal.mIRAdjustPath);
        }

        //转换数组数据到16位字    
        public UInt16 ConvertArrayTo16Int0(int[] arrData)
        {
            UInt16 theResult = 0;
            for (uint i = 0; i < 16; i++)
            {
                if (arrData[15 - i] > 0)
                {
                    theResult = Convert.ToUInt16(theResult * 2 + 1);
                }
                else
                {
                    theResult = Convert.ToUInt16(theResult * 2);
                }
            }

            return theResult;
        }



        public int[] BatModelToPLCArray(string strEncode)
        {
            int[] strReturn = new int[5];
            List<int> Return = new List<int>();
            strEncode = strEncode.PadLeft(10, '0');

            char[] values = strEncode.ToCharArray(0, 10);
            for (int i = 0; i < values.Length; i++)
            {

                Return.Add(Convert.ToInt32(values[9 - i]));

            }
            for (int n = 0; n < values.Length / 2; n++)
            {
                strReturn[n] = (Return[0] * 256 + Return[1]);
                Return.Remove(Return[0]);
                Return.Remove(Return[0]);
            }
            return strReturn;
        }

        private string GetNgCode(ET_CELL cell, out string desc)
        {
            string strCode = "";
            strCode = cell.Test_NgResult.NgState == "NG" ? strCode + "|" + cell.Test_NgResult.NgCode : strCode + "";
            strCode = cell.SV_NgResult.NgState == "NG" ? strCode + "|" + cell.SV_NgResult.NgCode : strCode + "";
            strCode = cell.PostiveSV_NgResult.NgState == "NG" ? strCode + "|" + cell.PostiveSV_NgResult.NgCode : strCode + "";
            strCode = cell.ACIR_NgResult.NgState == "NG" ? strCode + "|" + cell.ACIR_NgResult.NgCode : strCode + "";

            desc = "";
            desc = cell.Test_NgResult.NgState == "NG" ? desc + "|" + cell.Test_NgResult.NgDescribe : desc + "";
            desc = cell.SV_NgResult.NgState == "NG" ? desc + "|" + cell.SV_NgResult.NgDescribe : desc + "";
            desc = cell.PostiveSV_NgResult.NgState == "NG" ? desc + "|" + cell.PostiveSV_NgResult.NgDescribe : desc + "";
            desc = cell.ACIR_NgResult.NgState == "NG" ? desc + "|" + cell.ACIR_NgResult.NgDescribe : desc + "";
            return strCode;
        }
        private void ExpAnalysis()
        {
            List<OCVLog> list = new List<OCVLog>();
            string id = Guid.NewGuid().ToString("N");

            foreach (var item in ClsGlobal.listETCELL)
            {
                string desc = "";
                string strCode = GetNgCode(item, out desc);
                if (item.NgState == "NG")
                {
                    OCVLog model = new OCVLog()
                    {
                        ChannelNo = item.Cell_Position,
                        CellCode = item.Cell_ID,
                        PCName = "QT-" + ClsGlobal.DeviceCode,
                        TrayCode = TrayCode,
                        ExpTime = ClsGlobal.TestEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        Describe = desc,
                        ExpCode = strCode,
                        ID = id,
                        TimeCut = EventLogBLL.Instance.DateTimeToDouble(ClsGlobal.TestEndTime),
                    };
                    list.Add(model);
                }
            }
            if (list.Count > 0)
                EventLogBLL.Instance.Add(list);
        }

        private string GetChannelExpInfo(List<ChannelExpCount> list)
        {
            string txt = "在{0}小时内通道({1})连续发生超过{2}次NG，需要维修!";
            string strChannel = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count - 1)
                    strChannel += list[i].ChannelNo.ToString() + ",";
                else
                    strChannel += list[i].ChannelNo.ToString();

            }
            string info = string.Format(txt, ClsGlobal.Time_t, strChannel, ClsGlobal.ChannelExp_n);
            return info;
        }


        private string GetChannelExpInfo(List<ExpData> list)
        {

            string strChannel = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count - 1)
                    strChannel += list[i].ChannelNo.ToString() + ",";
                else
                    strChannel += list[i].ChannelNo.ToString();

            }
            string txt = "最近{0}小时内通道({1})累计发生NG超过{2}次，需要维修!";
            string info = string.Format(txt, ClsGlobal.Time_t, strChannel, ClsGlobal.TrayExp_m);
            return info;
        }

    }

}
