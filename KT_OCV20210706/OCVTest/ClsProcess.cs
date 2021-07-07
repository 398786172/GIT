
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
using OCV.MESHelper;
using System.Threading.Tasks;

namespace OCV
{
    public delegate void InfoSend(string Info);             //委托处理流程记录
    //过程处理
    public class ClsProcess
    {
        public enum eTransState
        {
            Stop = -1, //停止
            Init = 0, //初始状态
            Ready = 1, //就绪状态
            TrayIn = 2, //进托盘
            TestWork = 3, //测试工作
            NGSel = 4, //NG分选
            TrayOut = 5, // 出托盘

            //InAlarm = 9,  //报警状态
            TestAlarm = 10, //测试报警
        }

        /// <summary>
        /// 输送部工作状态
        /// </summary>
        private string mUnit; //使用OCV的单元: A,B

        private Thread myThread;
        public eTransState mStateFlag; //流程状态标识
        private int mStep; //步标识
        private bool mPauseFlag; //暂停标识
        private bool mStopFlag; //停止标识

        public bool mAlarmFlag; //报警标识 

        //OCV测试
        public string TrayCode; //托盘条码
        private string CurrTrayCode = ""; //当前托盘条码
        public string LastTrayCode; //上一次托盘条码
        public bool isTestAgainState = false; //是否复测状态的标识
        private int mRunState; //过程处理的运行状态    

        /// <summary>
        /// 运行状态  0:停止  1:运行  2:暂停  3:报警
        /// </summary>
        public int RunState
        {
            get { return mRunState; }
        }

        private int mSaveStep; //保存步
        private eTransState mSaveStateFlag; //保存状态标识

        //信息输出
        public InfoSend mInfoSend;
        ClsPLCContr mPLCContr;
        AlarmInfo mAlarmInfo = new AlarmInfo(); //报警信息     
        Mutex Mut = new Mutex();

        DBCOM_DevInfo mDBCOM_DevInfo; //设备信息
        ClsTestAnalysis mTestAnalysis; //数据分析

        public ManualScanCode ManScanCode;

        public delegate bool ManualScanCode(out string Code);

        private double[] ArrChannelTemp = new double[ClsGlobal.TrayType]; //温度数组
        private bool ExportToLocFinish = false;
        private bool ExportToServerFinish = false;
        private bool ErrDataSaveLocSqlFail = false; //保存到本地数据库失败
        private bool ErrDataSaveQTFail = false; //保存到擎天数据库失败
        private bool ErrDataSaveCSVFail = false; //保存到本地csv失败
        private bool ErrUpLoadngData = false; //保存NG信息失败
        private bool ExportToMesFinish = false; //保存MES信息完成

        private FrmSys mForm;
        TimeSpan Ts; //计时
        DateTime Time1;

        public ClsProcess(string Unit, InfoSend infoSend, ManualScanCode ManualScanCode, FrmSys frm)
        {
            mInfoSend = infoSend;
            ManScanCode = ManualScanCode;
            mPLCContr = ClsGlobal.mPLCContr;
            mDBCOM_DevInfo = new DBCOM_DevInfo(ClsGlobal.mDevInfoPath); //设备信息初始化
            mTestAnalysis = new ClsTestAnalysis(mDBCOM_DevInfo); //测试分析初始化
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
                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_自动流程启动, 0);
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

        //继续
        public void Resume()
        {
            mPauseFlag = false;
            mRunState = 1; //运行状态
            //mStep = mStepflag;   
        }

        //结束 
        public void Stop()
        {
            mStopFlag = true; //线程停止    
            mRunState = 0; //停止状态
            // mAlaFlag = true;
        }

        public void SetTempFinish(bool value)
        {
            mTestTempFinish = value;
        }

        //测试流程
        //复位
        public void Reset()
        {

            isTestAgainState = false;
            ////实现复位
            //mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示复位, 1);
            //Thread.Sleep(200);
            //mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示复位, 0);
            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 0);

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
                        mStateFlag = eTransState.Stop; //初始状态
                        isTestAgainState = false; //重测 
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
                        mRunState = 2; //暂停状态                      
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
                        ////报警状态
                        //AlarmState();
                        //break;
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
                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
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
            //short tmp;
            //short tmp2;
            switch (mStep)
            {
                case 1:
                    mInfoSend("启动自动测试流程");
                    mRunState = 1;
                    if (ClsPLCValue.PlcValue.Plc_AutoManual == 1) //设备在自动情况下,而且不在待机状态
                    {
                        //mInfoSend("设备复位");
                        //标识清0
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_启动, 1);
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答扫码请求, 0);
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 0);
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
                    mStateFlag = eTransState.Ready; //就绪状态
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
            //short tmp, tmp1;
            switch (mStep)
            {
                case 1: //检查是否有托盘进入
                    if (ClsPLCValue.PlcValue.Plc_HaveTray == 1)
                    {
                        Time1 = System.DateTime.Now;
                        mInfoSend("托盘已进入.");
                        ClsGlobal.OCVTestContr.InitPara(); //表数据清空
                        mInfoSend("等待PLC发扫码请求信号...");
                        mStep = 2;
                    }

                    break;
                case 2:
                    if (ClsPLCValue.PlcValue.Plc_RequestTest_A == 1)
                    {
                        mInfoSend("PLC请求扫描托盘条码");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答扫码请求, 1);
                        mInfoSend("PC应答测试请求");
                        mStep = 3;
                        Time1 = System.DateTime.Now;
                    }

                    break;
                case 3: //是否有测试请求
                    if (ClsPLCValue.PlcValue.Plc_RequestTest_A == 0)
                    {
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答扫码请求, 0);
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
                        mInfoSend("异常: PLC清零测试请求信号超时");
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
                        mStopFlag = true;
                    }

                    break;
                case 4:
                    try
                    {
                        string temp;
                        if (ClsGlobal.CodeScanMode == 1)
                        {
                            temp = ClsGlobal.CodeScan.SocketReadCode();
                        }
                        else
                        {
                            temp = ClsGlobal.CodeScan.ReadCode();
                        }

                        //temp = "JHWX0001";
                        bool val = false;
                        if (temp == "NG" || temp == "ERROR")
                        {
                            Thread.Sleep(500);
                            for (int i = 0; i < 2; i++)
                            {
                                if (ClsGlobal.CodeScanMode == 1)
                                {
                                    temp = ClsGlobal.CodeScan.SocketReadCode();
                                }
                                else
                                {
                                    temp = ClsGlobal.CodeScan.ReadCode();
                                }

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
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
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
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
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
                        if (isTestAgainState == false && ClsGlobal.IsAutoMode == false)
                        {
                            mInfoSend("正在获取手动绑定托盘[" + TrayCode + "] 当前的工序");
                            mInfoSend("正在获取手动绑定托盘[" + TrayCode + "] 电池信息数据");
                            DataSet ds = new DataSet();
                            ClsGlobal.listETCELL = new List<ET_CELL>() //新建电池信息 TrayType(16个)
                            {
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                                new ET_CELL(),
                            };
                            ds = CellBindBLL.Instance.GetList("TrayCode=" + TrayCode);
                            int rowsCount = ds.Tables[0].Rows.Count;
                            if (rowsCount > 0)
                            {
                                ET_CELL model = new ET_CELL();
                                for (int n = 0; n < rowsCount; n++)
                                {
                                    if (ds.Tables[0].Rows[n]["TrayCode"] != "")
                                    {
                                        model.Pallet_ID = ds.Tables[0].Rows[n]["TrayCode"].ToString();
                                    }

                                    if (ds.Tables[0].Rows[n]["CellCode"] != "")
                                    {
                                        model.Cell_ID = ds.Tables[0].Rows[n]["CellCode"].ToString();
                                    }

                                    if (ds.Tables[0].Rows[n]["Channel"].ToString() != "")
                                    {
                                        model.MODEL_NO = ClsGlobal.MODEL_NO;
                                    }

                                    ClsGlobal.listETCELL[n] = model;
                                }
                            }

                            mInfoSend("托盘电池信息获取成功！");

                        }

                        if (isTestAgainState == false && ClsGlobal.IsAutoMode == true)
                        {
                            //ClsGlobal.BattInfoReqFlag = -1;

                            mInfoSend("正在获取托盘[" + TrayCode + "] 当前的工序");
                            // string nowstep = "";
                            // ClsGlobal.OCVType = ClsGlobal.WCSCOM.Get_NowStepFormWCS(TrayCode, out nowstep);
                            int reTryTime = 3;
                            MesHelper mes = new MesHelper();
                            ClsGlobal.OCVType = mes.Get_PermissionFormMES(TrayCode, ClsGlobal.process_id);
                            if (ClsGlobal.OCVType == 0)
                            {
                                for (int i = 0; i < reTryTime; i++)
                                {
                                    ClsGlobal.OCVType = mes.Get_PermissionFormMES(TrayCode, ClsGlobal.process_id);
                                    if (ClsGlobal.OCVType != 0)
                                    {
                                        break;
                                    }
                                }

                                if (ClsGlobal.OCVType == 0)
                                {
                                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
                                    mInfoSend("MES系统返回托盘工序信息失败");
                                    //MessageBox.Show("MES系统返回托盘工序信息失败");
                                    throw new Exception("MES OCV入栈校验失败");
                                }
                            }

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

                            // mInfoSend("获取托盘当前的工序信息为："+nowstep);
                            mInfoSend("正在获取托盘[" + TrayCode + "] 电池信息数据");
                            // ClsGlobal.WCSCOM.Get_BattInfoFormWCS(TrayCode, out ClsGlobal.listETCELL);
                            ClsGlobal.listETCELL = mes.Get_BattInfoFormMES(TrayCode);
                            mInfoSend("托盘电池信息获取成功！");
                            //获取比亚迪化成分容数据库数据
                            //GetBatInfoFromQT(TrayCode);
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
                        int ret = ClsGlobal.mDBCOM_OCV_QT.Get_BattInfo(ClsGlobal.OCVType, TrayCode,
                            ref ClsGlobal.listETCELL);
                        if (ret == 1 || ret == 2)
                        {
                            strMsn = "异常:获取[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "数据异常,请查询此托盘是否有做OCV" +
                                     (ClsGlobal.OCVType - 1);
                            mInfoSend(strMsn);
                            mStateFlag = eTransState.TestAlarm;
                        }
                        else
                        {
                            strMsn = "托盘[" + TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据获取成功";
                            mInfoSend(strMsn);
                            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                            {
                                if (ClsGlobal.listETCELL[i].TEST_DATE != "" &&
                                    ClsGlobal.listETCELL[i].TEST_DATE != null)
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

                            ClsGlobal.TestStartTime = System.DateTime.Now;
                            TimeSpan date = ClsGlobal.TestStartTime - Convert.ToDateTime(FlowEndTime);
                            ClsGlobal.GetHours = date.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                        }
                    }

                    mStep = 9;
                    break;

                #endregion

                case 8:

                    #region 获取工艺信息

                    ClsGlobal.OCVTestContr.ShowCellid(); //页面展示CELLID
                    strMsn = "";
                    ClsGlobal.BattInfoReqFlag = -1;
                    strMsn = "正在获取托盘[" + TrayCode + "] 测试工艺";
                    mInfoSend(strMsn);
                    //单机
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
                    //校准
                    else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                    {

                    }
                    //正常                                
                    else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                    {
                        if (isTestAgainState == false)
                        {
                            ClsGlobal.BattInfoReqFlag =
                                ClsGetConfigDetail.GetProjectInfo(ClsGlobal.ProjectSetType, ClsGlobal.OCVType);
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
                            mStateFlag = eTransState.TestAlarm;
                            break;
                        }
                        else
                        {
                            strMsn = "托盘[" + TrayCode + "]获取电池工艺成功！";
                            mInfoSend(strMsn);
                            mStep = 7;
                        }
                    }

                    break;

                #endregion

                case 9:
                    mStep = 15;
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12: //直接排出去
                    //  mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_扫码完成, 2);
                    Time1 = System.DateTime.Now;
                    mStateFlag = eTransState.TestWork;
                    mStep = 13;

                    break;
                case 13:
                    break;
                case 14:
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
            //short tmp;
            try
            {
                switch (mStep)
                {
                    case 1: //确认电池类型  
                        //mInfoSend("等待PLC发送启动测试请求...");
                        Time1 = DateTime.Now;
                        mStep = 6; //不测试温度
                        break;
                    case 2: //是否有测试请求        
                        if (ClsPLCValue.PlcValue.Plc_RequestTest_A == 1)
                        {
                            mInfoSend("PLC请求启动测试");
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 1); //请求应答->1
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
                    case 3: //确认请求测试 
                        if (ClsPLCValue.PlcValue.Plc_RequestTest_A == 0)
                        {
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_应答检测请求, 0); //应答清0  
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
                    case 5: //OCV测试    

                        #region 测试温度

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
                        mInfoSend("开始测试工序:OCV" + ClsGlobal.OCVType.ToString());
                        //测试控制参数更新
                        ClsGlobal.OCVTestContr.StartTestAction();
                        mInfoSend("OCV" + ClsGlobal.OCVType.ToString() + "测试中...");
                        mStep = 7;
                        break;
                    case 7:

                        #region OCV测试完成判断

                        //异常判断
                        if (ClsGlobal.OCVTestContr.AutoTestFinish == false &&
                            ClsGlobal.OCV_TestState == eTestState.ErrOCVTest)
                        {
                            mInfoSend("OCV测试异常! " + ClsGlobal.OCV_TestErrDetail);
                            mStateFlag = eTransState.TestAlarm;
                        }

                        if (ClsGlobal.OCVTestContr.AutoTestFinish == true)
                        {
                            mTestTempFinish = true;
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
                                    ExportLocalData(); //保存本地后结束    
                                    ClsGlobal.OCV_TestState = eTestState.OfflineTestEnd;
                                }

                                mStep = 11;
                            }

                            #region 正常测试

                            else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                            {
                                if (ClsGlobal.OCV_TestState == eTestState.TestOK) //测试OK
                                {
                                    mInfoSend("OCV" + ClsGlobal.OCVType + "测试完成");
                                    mStep = 9;
                                }
                                else if (ClsGlobal.OCV_TestState == eTestState.TestAgain) //再测定
                                {
                                    //mInfoSend("再测定...");
                                    mStep = 11; //重新测量
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

                        //联机测试下保存
                        ExportToLocFinish = false;
                        ExportToServerFinish = false;
                        ErrDataSaveCSVFail = false;
                        ErrDataSaveQTFail = false;
                        ErrUpLoadngData = false;
                        ExportToMesFinish = false;
                        //保存到本地                           
                        Thread threadExportData = new Thread(new ThreadStart(ExportLocalData));
                        threadExportData.IsBackground = true;
                        threadExportData.Start();

                        //保存到服务器的数据库
                        //包括本地服务器以及擎天服务器 20181018
                        Thread threadExportToServer = new Thread(new ThreadStart(ExportToServer));
                        threadExportToServer.IsBackground = true;
                        threadExportToServer.Start();
                        ExportToServerFinish = true;
                        //保存NG信息到wcs
                        //Thread threadUploadNGToWsc = new Thread(new ThreadStart(UploadNGToWsc));
                        //threadUploadNGToWsc.IsBackground = true;
                        //threadUploadNGToWsc.Start();

                        ErrUpLoadngData = true;
                        //保存到MES                  
                        UploadResultToMES();
                        ExportToMesFinish = true;
                        mStep = 10;

                        #endregion

                        break;
                    case 10:

                        #region 保存数据完成

                        if (ErrDataSaveQTFail == true || ErrDataSaveQTFail == true || ErrDataSaveCSVFail == true)
                        {
                            mStateFlag = eTransState.TestAlarm;
                        }

                        if (ExportToLocFinish == true && ExportToServerFinish == true && ErrUpLoadngData == true &&
                            ExportToMesFinish == true)
                        {
                            ClsGlobal.OCV_TestState = eTestState.TestEnd;

                            mStep = 11;
                        }

                        #endregion

                        break;
                    case 11: //OCV测试         

                        if (ClsGlobal.OCV_TestState == eTestState.TestAgain) //处理再次测试
                        {
                            mInfoSend("再次测试...");
                            mStep = 16;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.AdjustAgain)
                        {
                            mInfoSend("再次校准...");
                            mStep = 16;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.AdjustOK) //校准完成
                        {
                            mInfoSend("内阻校准完成");
                            mStep = 19;
                        }
                        else if (ClsGlobal.OCV_TestState == eTestState.OfflineTestEnd) //单机测试运行
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
                    case 12: //测试数据分析
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
                        mSET_Info.ACIR_SetEN = lstSetInfo[0].ACIR_SetEN;
                        ;
                        mSET_Info.ACIR_UCL = lstSetInfo[0].ACIR_UCL;
                        ;
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
                            if (ClsGlobal.TestType == 1 || ClsGlobal.TestType == 2)
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

                                    mInfoSend("异常:通道[" + str.Substring(0, str.Length - 1) +
                                              "]出现壳体测试连续异常，请检查对应通道探针是否有异常!");
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

                                    mInfoSend("异常:通道[" + str.Substring(0, str.Length - 1) +
                                              "]出现ACIR测试连续异常，请检查对应通道探针是否有异常!");
                                    errFlag = true;
                                }
                            }

                        }

                        //有异常情况时处理                    
                        if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                        {
                            if (errFlag == true)
                            {
                                mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
                                mInfoSend("确认是否继续...");
                                if (MessageBox.Show("测试通道出现异常,请确认是否继续?", "提示", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
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
                            mStep = 13; //设备到待机、
                        }

                        break;
                    //测试完成----------------------------------------------
                    case 13: //测试完成
                        isTestAgainState = false; //不重测

                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 1); //测试完成,搬送托盘
                        mInfoSend("发送测试结束信号给PLC");
                        Time1 = DateTime.Now;
                        mStep = 14;
                        break;
                    case 14: //测试完成应答
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply_A == 1)
                        {
                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 0);
                            Time1 = DateTime.Now;
                            mStep = 15;
                        }

                        Ts = System.DateTime.Now - Time1;
                        if (Ts.Seconds > 30)
                        {
                            mInfoSend("异常:PLC应答测定结束超时");
                            mStateFlag = eTransState.TestAlarm;
                        }

                        break;
                    case 15: //测试完成清0
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply_A == 0)
                        {
                            mInfoSend("测试完成等待移出托盘...");
                            mStateFlag = eTransState.TrayOut;
                            mStep = 1;
                        }

                        Ts = System.DateTime.Now - Time1;
                        if (Ts.Seconds > 30)
                        {
                            mInfoSend("异常:PLC清除应答测定结束信号超时");
                            mStateFlag = eTransState.TestAlarm;
                        }

                        break;
                    //重测 ---------------------------------------------------
                    case 16:
                        isTestAgainState = true; //重测 
                        if (ClsGlobal.OCV_TestState == eTestState.TestAgain)
                        {
                            string Msn = "";
                            //foreach (double item in ClsGlobal.lstACIRErrNo)
                            //{
                            //    string ShowMsn = (item + 1).ToString();
                            //    Msn += "【" + ShowMsn + "】,";
                            //}
                            mInfoSend(Msn + "有通道异常，进行复测");
                        }

                        Time1 = DateTime.Now;
                        mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 2); //再次测定
                        mStep = 17;
                        break;
                    case 17:
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply_A == 2)
                        {

                            mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示测定结束, 0);
                            Time1 = DateTime.Now;
                            mStep = 18;
                        }

                        Ts = System.DateTime.Now - Time1;
                        if (Ts.Seconds > 20)
                        {
                            mInfoSend("异常:PLC复测应答测定超时");
                            mStateFlag = eTransState.TestAlarm;
                        }

                        break;
                    case 18:
                        if (ClsPLCValue.PlcValue.Plc_TestFinshReply_A == 0)
                        {
                            mStateFlag = eTransState.TestWork; //复测，到托盘进入状态
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
                        mStep = 20; //单机测试结束
                        break;
                    case 20:
                        //mPLCContr.ReadDB(mPLCContr.mPlcAddr.PLC应答电池检测完成, out tmp);
                        //if (tmp == 1)
                        //{
                        //    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC指示电池检测完成, 0);
                        //    mStep = 21;
                        //}
                        //if (ClsPLCValue.PlcValue.Plc_AutoStepNO >= 65 || ClsPLCValue.PlcValue.Plc_AutoStepNO == 5)
                        //{
                        //    mInfoSend("托盘移除出...");
                        //    mStateFlag = eTransState.TrayOut;
                        //    mStep = 1;
                        //}
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
            catch (Exception)
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
                    mPLCContr.WriteDB(mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
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

        //private void VerifyTestTime()
        //{
        //    string Msn, TimeMsn;
        //    string FlowEndTime = "";

        //    DateTime TestTime = DateTime.Now;
        //    DateTime TestTime0;

        //    if (ClsGlobal.OCVType == 1)
        //    {
        //        for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
        //        {
        //            if (ClsGlobal.listETCELL[i].FlowEndTime != "" && ClsGlobal.listETCELL[i].FlowEndTime != null)
        //            {
        //                FlowEndTime = ClsGlobal.listETCELL[i].FlowEndTime;
        //                break;
        //            }
        //        }
        //        if (DateTime.TryParse(FlowEndTime, out TestTime0) == false)
        //        {
        //            Msn = "异常:" + "托盘[" + TrayCode + "]无对应的分容结束时间!";
        //            mInfoSend(Msn);
        //            throw new Exception("测试被终止");
        //        }
        //        TimeSpan date = TestTime - Convert.ToDateTime(FlowEndTime);
        //        ClsGlobal.GetHours = date.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
        //        TimeMsn = "分容测试时间：" + Convert.ToDateTime(FlowEndTime).ToString("yyy-MM-dd HH:mm:ss") + "\r\n";
        //        TimeMsn += "当前测试时间：" + TestTime.ToString("yyy-MM-dd HH:mm:ss");
        //         ClsGlobal.FlowTimeA = TimeMsn;

        //    }
        //    else if (ClsGlobal.OCVType == 2)
        //    {
        //        for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
        //        {
        //            if (ClsGlobal.listETCELL[i].TEST_DATE != "" && ClsGlobal.listETCELL[i].TEST_DATE != null)
        //            {
        //                FlowEndTime = ClsGlobal.listETCELL[i].TEST_DATE;
        //                break;
        //            }
        //        }
        //        if (DateTime.TryParse(FlowEndTime, out TestTime0) == false)
        //        {
        //            Msn = "异常:" + "托盘[" + TrayCode + "]无对应的OCV1的测试时间!";
        //            mInfoSend(Msn);
        //            throw new Exception("测试被终止");
        //        }
        //        TimeSpan date = TestTime - Convert.ToDateTime(FlowEndTime);
        //        ClsGlobal.GetHours = date.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
        //        TimeMsn = "OCV1测试时间：" + Convert.ToDateTime(FlowEndTime).ToString("yyy-MM-dd HH:mm:ss") + "\r\n";
        //        TimeMsn += "当前测试时间：" + TestTime.ToString("yyy-MM-dd HH:mm:ss");

        //        ClsGlobal.FlowTimeA = TimeMsn;


        //    }
        //    else
        //    {
        //        for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
        //        {
        //            if (ClsGlobal.listETCELL[i].TEST_DATE2 != "" && ClsGlobal.listETCELL[i].TEST_DATE2 != null)
        //            {
        //                FlowEndTime = ClsGlobal.listETCELL[i].TEST_DATE2;
        //                break;
        //            }
        //        }
        //        if (DateTime.TryParse(FlowEndTime, out TestTime0) == false)
        //        {
        //            Msn = "异常:" + "托盘[" + TrayCode + "]无对应的OCV2的测试时间!";
        //            mInfoSend(Msn);
        //            throw new Exception("测试被终止");
        //        }
        //        TimeSpan date = TestTime - Convert.ToDateTime(FlowEndTime);
        //        ClsGlobal.GetHours = date.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
        //        TimeMsn = "OCV2测试时间：" + Convert.ToDateTime(FlowEndTime).ToString("yyy-MM-dd HH:mm:ss") + "\r\n";
        //        TimeMsn += "当前测试时间：" + TestTime.ToString("yyy-MM-dd HH:mm:ss");

        //        ClsGlobal.FlowTimeA = TimeMsn;

        //    }

        //}

        //private int VerifyBatCode(List<ET_CELL> QTlstCell, List<ET_CELL_VS> OCVlstCell)
        //{
        //    int Count = 0;
        //    if (QTlstCell.Count != OCVlstCell.Count)
        //    {
        //        //throw new Exception("当前测试托盘电池数量与服务器托盘最新登录的电池数量不一致");
        //        return 1;
        //    }
        //    for (int i = 0; i < QTlstCell.Count; i++)
        //    {
        //        Count = 0;
        //        for (int j = 0; j < OCVlstCell.Count; j++)
        //        {
        //            if (QTlstCell[i].Cell_ID.Trim().ToUpper() == OCVlstCell[j].Cell_ID.Trim().ToUpper())
        //            {
        //                Count = Count + 1;
        //                break;
        //            }
        //        }
        //        if (Count == 0)
        //        {
        //            break;
        //        }
        //    }
        //    if (Count == 0)
        //    {
        //        return 2;
        //    }
        //    return 0;
        //}

        private bool mTestTempFinish;

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
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[i]); //温度通道映射
                    //ClsGlobal.G_dbl_P_TempArr[i] = ArrChannelTemp[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);
                    ClsGlobal.G_dbl_P_TempArr[i] = ClsGlobal.TempContr.Anodetemperature[ActualNum - 1] +
                                                   double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);
                    //负极温度
                    ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[i]);
                    //ClsGlobal.G_dbl_N_TempArr[i] = ArrChannelTemp[ActualNum - 1 + 38] + double.Parse(ClsGlobal.mTempAdjustVal_N[ActualNum - 1]);
                    ClsGlobal.G_dbl_N_TempArr[i] = ClsGlobal.TempContr.Poletemperature[ActualNum - 1] +
                                                   double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]);

                    ClsGlobal.listETCELL[i].PostiveTMP = ClsGlobal.G_dbl_P_TempArr[i];
                    ClsGlobal.listETCELL[i].NegativeTMP = ClsGlobal.G_dbl_N_TempArr[i];
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
            DataTable dtTemplate = new DataTable(); //表模板
            try
            {
                if (ClsGlobal.listETCELL.Count == 0)
                {
                    return;
                }

                //取得开始时间和结束时间
                string _sdtStartTime = ClsGlobal.TestStartTime.ToString("yyy-MM-dd HH:mm:ss"); //开始时间
                string _sdtEndTime = ClsGlobal.TestEndTime.ToString("yyy-MM-dd HH:mm:ss"); //结束时间 
                string _sExcelPath = Application.StartupPath;

                //创建导出数据文件夹
                string _sFilePath = _sExcelPath + "\\" + "ExportStepData";
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }

                //创建OCV型号数据文件夹
                string _sOCVname = "OCV-" + ClsGlobal.OCVType;
                _sFilePath = _sFilePath + "\\" + _sOCVname;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }

                //创建月份文件夹
                string _sYM = System.DateTime.Now.ToString("yyyyMM");
                _sFilePath = _sFilePath + "\\" + _sYM;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }

                //创建当天文件夹
                int _iDay = System.DateTime.Now.Day;
                _sFilePath = _sFilePath + "\\" + _iDay;
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }

                int Savemode = 2;

                //保存模式
                if (Savemode == 0)
                {
                    //ExportDataToExcel(_sFilePath, _sdtStartTime, _sdtEndTime);
                }

                if (Savemode == 2)
                {
                    //ExportDataToCSV(_sFilePath); //不用这个函数
                    ExportDataToCSV(_sFilePath);
                }

                ExportToLocFinish = true;

            }
            catch (Exception ex)
            {
                ErrDataSaveCSVFail = true; //保存到本地csv失败
                ExportToLocFinish = false;
                string strMsg = "保存本地CSV文件出错";
                ClsLogs.OCVInfologNet.WriteFatal(mUnit, strMsg + ex.Message);
                mInfoSend(strMsg);
            }
            finally
            {

            }
        }

        private void ExportDataToCSV(string CSVPath)
        {
            string addr = CSVPath + "\\OCV" + ClsGlobal.OCVType.ToString() + "_" + TrayCode + "_" +
                          DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
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
            SWR.WriteLine(
                "通道号, 托盘条码,电池条码,总判定结果,开路电压(mV),ACIR(mΩ),测试结果,NG原因,壳体电压(mV),壳体电压检测结果,壳体电压NG原因,正极温度(℃),负极温度(℃),压降△V,压降△V极差,压降△V极差判定结果,压降△V极差NG原因,ACIR极差,ACIR极差判定结果,ACIR极差NG原因,补偿前电压(mV),补偿后电压(mV),容量,测试时间,工站");
            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
            {
                DataCsv.Cell_Position = ClsGlobal.listETCELL[i].Cell_Position;
                DataCsv.Pallet_ID = TrayCode;
                DataCsv.Cell_ID = ClsGlobal.listETCELL[i].Cell_ID.ToString();
                DataCsv.OCV_Now = ClsGlobal.listETCELL[i].OCV_Now.ToString();
                DataCsv.NgState = ClsGlobal.listETCELL[i].NgState;
                //OCV1无自放电
                if (ClsGlobal.OCVType == 3)
                {
                    if (ClsGlobal.ENVoltDrop == "Y")
                    {
                        DataCsv.VoltDrop_Now = ClsGlobal.listETCELL[i].VoltDrop_Now.ToString();

                        if (ClsGlobal.IS_Enable_DropRange == "Y")
                        {
                            DataCsv.DROP_range = ClsGlobal.listETCELL[i].DROP_range.ToString();
                            DataCsv.DROP_NgResult = ClsGlobal.listETCELL[i].DROP_NgResult;
                        }
                    }
                }

                if (ClsGlobal.TestType > 0)
                {
                    DataCsv.OCV_Shell_Now = ClsGlobal.listETCELL[i].OCV_Shell_Now.ToString();
                    DataCsv.SV_NgResult = ClsGlobal.listETCELL[i].SV_NgResult;
                }

                if (ClsGlobal.TestType == 2)
                {
                    if (ClsGlobal.IS_Enable_ACIRRange == "Y")
                    {
                        DataCsv.ACIR_range = ClsGlobal.listETCELL[i].ACIR_range.ToString();
                        DataCsv.ACIR_NgResult = ClsGlobal.listETCELL[i].ACIR_NgResult;
                    }

                    DataCsv.ACIR_Now = ClsGlobal.listETCELL[i].ACIR_Now.ToString();
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
                    DataCsv.OCV_Shell_Now + "," +
                    DataCsv.SV_NgResult.NgState + "," +
                    DataCsv.SV_NgResult.NgDescribe + "," +
                    DataCsv.PostiveTMP + "," +
                    DataCsv.NegativeTMP + "," +
                    DataCsv.VoltDrop_Now + "," +
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
                    DataCsv.OCVType);
            }

            SWR.Close();
            SWR = null;
        }

        //保存到服务器
        private void ExportToServer()
        {
            string msn;
            int iResult = 0;
            int iResult_loc = 0;
            try
            {
                ExportToServerFinish = false;
                //本地与远程都上传
                try
                {
                    iResult = ClsGlobal.mDBCOM_OCV_QT.InsertOCVACIRData(ClsGlobal.OCVType, ClsGlobal.listETCELL, 1);
                    msn = "托盘[" + TrayCode + "]上传擎天数据库电池数据数量:" + iResult;
                    mInfoSend(msn);
                }
                catch (Exception ex)
                {
                    ErrDataSaveQTFail = true;
                    msn = "异常：上传擎天数据库失败";
                    mInfoSend(msn);
                    ClsLogs.OCVInfologNet.WriteFatal(mUnit, msn + ex.Message);
                    throw ex;
                }

                //try
                //{
                //    iResult_loc = ClsGlobal.mDBCOM_SVSQT.InsertOCVData_SVS(ClsGlobal.OCVType, ClsGlobal.listETCELL);
                //    msn = "托盘[" + TrayCode + "]上传BYD数据库电池数据数量:" + iResult;
                //    mInfoSend(msn);
                //}
                //catch (Exception ex)
                //{
                //    ErrDataSaveLocSqlFail = true;
                //    msn = "异常：保存byd数据库失败";
                //    mInfoSend(msn);
                //    ClsLogs.OCVInfologNet.WriteFatal(mUnit, msn + ex.Message);
                //    throw ex;
                //}
                //ClsGlobal.OCVUpload = false;
                ExportToServerFinish = true;

                //取消工艺保存 
                //try
                //{
                //    mDBCOM_OCV_QT.AddProjInfo();
                //    mDBCOM_OCV_Local.AddProjInfo();
                //}
                //catch (Exception ex)
                //{
                //    iResult_Info = true;
                //    throw ex;
                //}


            }
            catch (Exception ex)
            {
                //ClsGlobal.OCVUpload = false;
            }

        }

        //给wcs传NG信息
        private void UploadNGToWsc()
        {
            ErrUpLoadngData = false;
            try
            {
                bool upload = ClsGlobal.WCSCOM.UpLoadngData(TrayCode, ClsGlobal.listETCELL);
                if (upload == true)
                {
                    mInfoSend("托盘[" + TrayCode + "] 电池NG数据上传成功");
                }
            }
            catch (Exception ex)
            {

                mInfoSend("托盘[" + TrayCode + "] " + ex.Message);
                mStateFlag = eTransState.TestAlarm;
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

        private void UploadResultToMES()
        {

            bool pass = false;
            try
            {
                OCVResultUpLoad postData = new OCVResultUpLoad()
                {
                    equipment_id = ClsGlobal.DeviceCode,
                    traycode = TrayCode,
                    process_id = ClsGlobal.process_id,
                    procedure = ClsGlobal.OCVType.ToString()

                };
                for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                {
                    postData.data.Add(new OCVResultUpLoadData()
                    {
                        bar_code = ClsGlobal.listETCELL[i].Cell_ID,
                        result = ClsGlobal.listETCELL[i].NgState == "NG" ? "03" : "01",
                        data2 = new List<OCVResultUpLoadData2>()
                        {
                            new OCVResultUpLoadData2()
                            {
                                parameter_code = ClsGlobal.listETCELL[i].NgState,
                                parameter_name = ClsGlobal.listETCELL[i].NgState,
                                parameter_value = ClsGlobal.listETCELL[i].NgState
                            }
                        }
                    });
                }

                var result =
                    new MesHelper().HttpPostOCVResultUpLoad("http://127.0.0.1:8080/IEAM/userManagement/loginAPP",
                        postData);
                if (result.message == "请求成功")
                {
                    mInfoSend("托盘[" + TrayCode + "] 电池数据上传成功");
                }
            }
            catch (Exception ex)
            {
                mInfoSend("托盘[" + TrayCode + "] " + ex.Message);
                mStateFlag = eTransState.TestAlarm;
            }
        }

    }
}


