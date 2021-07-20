
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using OCV;
using System.IO;
using OCV.OCVTest;

namespace OCV
{

    //OCV过程处理
    public class ClsProcess
    {
        private Thread myThread;
        private eTransState mStateFlag { get; set; }      //流程状态标识
        private int mStep;                   //步标识
        private bool mPauseFlag;             //暂停标识
        private bool mStopFlag { get; set; }            //停止标识
        private bool mResetFlag;             //复位标识

        private int mRunState;               //过程处理的运行状态  

        FrmMonitor mForm;
        FrmOCV frmOCV;

        ClsCommWithMES mesManager = new ClsCommWithMES();
        ClsCodeScaner trayCodeScaner = ClsGlobal.BuildClsCodeScan();

        /// <summary>
        /// 运行状态  0:停止  1:运行  2:暂停  3:报警
        /// </summary>
        public int RunState { get { return mRunState; } }

        private int mSaveStep;               //保存步
        private eTransState mSaveStateFlag;  //保存状态标识

        //信息输出
        public delegate void InfoSend(string Info);             //委托处理流程记录
        public InfoSend mInfoSend;

        public delegate bool ManualScanCode(out string Code);
        public ManualScanCode ManScanCode;


        bool Val_1 = false;

        TimeSpan Ts;                        //计时
        DateTime Time1;

        Mutex Mut = new Mutex();

        public ClsProcess(InfoSend infoSend, ManualScanCode ManualScanCode, FrmMonitor F1)
        {
            mInfoSend = infoSend;
            ManScanCode = ManualScanCode;

            mStopFlag = true;
            mRunState = 0;
            ClsGlobal.Trans_State = eTransState.Stop;           //停止

            mForm = F1;
        }

        //启动线程
        public void StartAction()
        {
            try
            {
                myThread = new Thread(ThreadProcess);
                mStateFlag = eTransState.Init;
                mStep = 1;
                mStopFlag = false;
                mPauseFlag = false;
                myThread.Start();
                mRunState = 1;
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

            Thread.Sleep(100);
            Mut.ReleaseMutex();
        }

        //继续
        public void Resume()
        {
            mPauseFlag = false;
            mRunState = 1;                  //运行状态
        }

        //结束 
        public void Stop()
        {
            mStopFlag = true;                //线程停止    
            mRunState = 0;                   //停止状态
        }

        //复位
        public void AlarmReset()
        {
            //ClsGlobal.isTestAgainState = false;                                    
        }

        //控制流程
        private void ThreadProcess()
        {
            while (true)
            {
                try
                {
                    //停止
                    if (mStopFlag == true)
                    {
                        ClsGlobal.mIOControl.Set_ReqReset(1);
                        Thread.Sleep(200);
                        ClsGlobal.mIOControl.Set_ReqReset(0);
                        mPauseFlag = false;
                        ClsGlobal.Trans_State = eTransState.Stop;           //停止w
                        ClsGlobal.Trans_TrayLoc = eTrayLoc.NotInPlace;      //->托盘没定位
                        ClsGlobal.OCV_TestState = eTestState.Idle;
                        mStateFlag = eTransState.Stop;                      //停止状态
                        ClsGlobal.IRTrueValSetFlag = false;
                        mStep = 1;
                        mInfoSend("程序停止!");
                        mRunState = 0;
                        ClsGlobal.LastTrayCode = "";
                        break;
                    }

                    //暂停
                    if (mPauseFlag == true)
                    {
                        Time1 = System.DateTime.Now;
                        mRunState = 2;                  //暂停状态
                        Thread.Sleep(100);
                        continue;
                    }

                    #region 异常处理

                    //设备输入输出状态

                    //设备异常处理
                    //设备报警
                    ushort DevAlarmState = 0;

                    if (mStateFlag != eTransState.TestWork)
                    {
                        DevAlarmState = ClsGlobal.mIOControl.Get_M_Alarm1();
                    }

                    //DevAlarmState = ClsGlobal.mIOControl.Get_M_Alarm1();

                    if (DevAlarmState > 0)
                    {
                        if ((DevAlarmState & (1 << 0)) > 0)
                        {
                            mInfoSend("设备报警: 气压低...");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 1)) > 0)
                        {
                            mInfoSend("设备报警：急停按钮按下!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 2)) > 0)
                        {
                            mInfoSend("设备报警: 针床打开不到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 3)) > 0)
                        {
                            mInfoSend("设备报警: 针床压合不到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 4)) > 0)
                        {
                            mInfoSend("设备报警: 托盘推进位置没有复位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 5)) > 0)
                        {
                            mInfoSend("设备报警:托盘推进位置没有到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 6)) > 0)
                        {
                            mInfoSend("设备报警: 托盘上升复位不到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 7)) > 0)
                        {
                            mInfoSend("设备报警: 托盘下降不到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 8)) > 0)
                        {
                            mInfoSend("设备报警: 托盘夹爪复位不到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 9)) > 0)
                        {
                            mInfoSend("设备报警: 托盘夹爪夹紧不到位!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 10)) > 0)
                        {
                            mInfoSend("设备报警:烟雾报警1!");
                            mStopFlag = true;
                            continue;
                        }
                        else if ((DevAlarmState & (1 << 11)) > 0)
                        {
                            mInfoSend("设备报警: 烟雾报警2!");
                            mStopFlag = true;
                            continue;
                        }
                    }

                    //测试异常
                    if (ClsGlobal.OCV_TestState == eTestState.ErrOCVTest)
                    {
                        mStopFlag = true;
                        mInfoSend("OCV测试异常!");
                        continue;
                    }
                    //校验异常
                    if (ClsGlobal.OCV_TestState == eTestState.ErrAdjust)
                    {
                        mStopFlag = true;
                        mInfoSend("校验异常: " + ClsGlobal.TestOCVMsg);
                        continue;
                    }
                    //没进行master设置
                    if (ClsGlobal.OCV_TestState == eTestState.ErrAdjustDataNoSet)
                    {
                        mInfoSend("[校准]启动校准前, 请先进行OCV Master设置");
                        ClsGlobal.IRTrueValSetFlag = false;
                        mStopFlag = true;
                        continue;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.ErrOCVDataGetFail)
                    {
                        mInfoSend("异常：" + ClsGlobal.ErrMsg);
                        mStopFlag = true;
                        continue;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.ErrOCVFlowErr)
                    {
                        mInfoSend("异常：" + ClsGlobal.ErrMsg);
                        mStopFlag = true;
                        continue;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.ErrOther)
                    {
                        mInfoSend("异常：" + ClsGlobal.ErrMsg);
                        mStopFlag = true;
                        continue;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.ErrDataSaveLocal)
                    {
                        mInfoSend("异常：" + ClsGlobal.ErrMsg);
                        mStopFlag = true;
                        continue;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.ErrDataSaveKTFail)
                    {
                        mStopFlag = true;
                        mInfoSend("异常：" + ClsGlobal.ErrMsg);
                        continue;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.ErrOther)
                    {
                        mStopFlag = true;
                        mInfoSend("异常：OCV测试出现错误...");
                        continue;
                    }

                    #endregion

                    ClsGlobal.Trans_State = mStateFlag;    //流程状态更新
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
                        case eTransState.TrayTestFinish:
                            //测试结束
                            TrayTestFinishState();
                            break;
                        //case eTransState.ReTest:
                        //    ReTestWork();
                        //    break;
                        default:
                            break;
                    }

                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    mInfoSend(ex.ToString());
                    //return;
                }
            }
        }
        #region 复测新增的方法 20201019 ajone
        private void ReTestWork()
        {
            if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
            {
                ClsGlobal.OCVIRTest.TestFinish = true;
                return;
            }

            try
            {
                //获取复测通道列表
                List<int> lstReTestChannel = ClsGlobal.listETCELL.Where(a => a.CODE.ToLower().Contains("ng") && !a.Cell_ID.StartsWith("000")).Select(a => a.Cell_Position).ToList(); ;
                //等待托盘进入,等待启动信号
                WaitTrayInAndStart();
                //启动测试工作
                TestWithChannelList(lstReTestChannel);

                //完成测试并生成数据
                ClsGlobal.OCVIRTest.TestFinish = true;
                //IO控制托盘退出
                WaitTrayOpen();
                //等待托盘拉出
                WaitTrayOut();
            }
            catch (Exception ex)
            {
                ClsGlobal.OCVIRTest.TestFinish = true;
                throw ex;
            }
        }

        private void WaitTrayInAndStart()
        {
            int stepValue = 1;
            while (true)
            {
                switch (stepValue)
                {

                    case 1:    ///等待托盘进入
                        if (ClsGlobal.mIOControl.Get_M_TrayIn() == 1)
                        {

                            ClsGlobal.Trans_TrayLoc = eTrayLoc.InPlace;
                            if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                            {
                                mInfoSend("OCV Master托盘进入.");
                            }
                            else
                            {
                                mInfoSend("托盘进入.");
                            }
                            stepValue = 2;
                            break;
                        }
                        else
                        {
                            ClsGlobal.Trans_TrayLoc = eTrayLoc.NotInPlace;
                        }
                        break;
                    case 2:  //发送启动信号到IO,由IO等待启动按钮
                        if (ClsGlobal.mIOControl.Get_M_TrayIn() == 0)
                        {
                            mInfoSend("托盘被移出");
                            stepValue = 1;
                        }
                        try
                        {
                            if (ClsProbeRecover.ProbeSet.Probes.Max(a => a.Times) > ClsProbeRecover.ProbeSet.StopCount)
                            {
                                MessageBox.Show("探针寿命已经达到上限,无法继续工作,请更换探针后继续工作!", "警告");
                                mInfoSend("探针寿命已经达到上限,无法继续工作,,请更换探针后继续工作!");
                                throw new Exception("探针寿命已经达到上限,无法继续工作,,请更换探针后继续工作!");
                            }
                            else if (ClsProbeRecover.ProbeSet.Probes.Max(a => a.Times) > ClsProbeRecover.ProbeSet.WarningCount)
                            {
                                mInfoSend("探针寿命即将达到寿命上限,请预备好更换的探针!");
                            }
                            ClsProbeRecover.Add();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        mInfoSend("按启动按钮,压合针床....");
                        ClsGlobal.mIOControl.Set_DebugPressPB();  //发送启动请求
                        stepValue = 3;
                        break;
                    case 3:
                        if (ClsGlobal.mIOControl.Get_M_MECHA_IsWork() == 1) //托盘压合完成
                        {
                            Thread.Sleep(1 * 1000);
                            ClsGlobal.mIOControl.Set_DebugUnPressPB();  //工作启动后复位 启动请求
                            stepValue = 255;
                            mInfoSend("针床压合完成");
                        }
                        break;
                }
                if (stepValue == 255)
                {
                    break;
                }
                System.Threading.Thread.Sleep(1 * 1000);
            }

        }
        private void WaitTrayOut()
        {
            while (true)
            {
                if (ClsGlobal.mIOControl.Get_M_TrayIn() == 0)
                {
                    mInfoSend("托盘移出完成");
                    mStateFlag = eTransState.TrayTestFinish;
                    ClsGlobal.Trans_TrayLoc = eTrayLoc.NotInPlace;
                    mStep = 0;
                    break;
                }
                Thread.Sleep(1 * 1000);
            }
        }

        private void WaitTrayOpen()
        {
            int stepValue = 1;
            while (true)
            {

                switch (stepValue)
                {
                    case 1:

                        mInfoSend("复测完成.");
                        ClsGlobal.mIOControl.Set_TestFinish();
                        stepValue = 2;
                        break;
                    case 2:
                        if (ClsGlobal.mIOControl.Get_IOResetCompleted() == 1)
                        {
                            ClsGlobal.mIOControl.Set_ResetTestFinish();      //标志复位
                            stepValue = 255;
                        }
                        break;
                }
                if (stepValue == 255)
                {
                    break;
                }
            }


        }

        private void TestWithChannelList(List<int> lstChannel)
        {
            ClsGlobal.IsTestRuning = true;
            ClsGlobal.OCVIRTest.ReTest(lstChannel);
            ClsGlobal.IsTestRuning = false;
        }

        #endregion

        //初始状态
        private void InitState()
        {

            switch (mStep)
            {
                case 1:
                    //初始化设备
                    ClsGlobal.mIOControl.Set_ReqReset(1);
                    mInfoSend("设备复位...");
                    mStep = 2;
                    break;
                case 2:
                    //托盘针床操作过程由下位机负责  由ajone于20200811 屏蔽 
                    //if (ClsGlobal.mIOControl.Get_M_EchoReset() == 1)
                    //{
                    //    Thread.Sleep(1 * 1000);
                    //    ClsGlobal.mIOControl.Set_ReqReset(0);
                    //    mStep = 3;
                    //}


                    //托盘针床操作过程由下位机负责 由ajone于20200811 新增
                    Thread.Sleep(1 * 1000);
                    ClsGlobal.mIOControl.Set_ReqReset(0);
                    mStep = 3;

                    break;
                case 3:
                    //托盘针床操作过程由下位机负责  由ajone于20200811 屏蔽 
                    //if (ClsGlobal.mIOControl.Get_M_EchoReset() == 0 &&
                    //    ClsGlobal.mIOControl.Get_M_PBPress() == 0)      //托盘打开
                    //{
                    //    mInfoSend("设备复位完成, 等待托盘进入...");
                    //    mStateFlag = eTransState.Ready;
                    //    mStep = 1;
                    //}

                    //托盘针床操作过程由下位机负责 由ajone于20200811 新增
                    Thread.Sleep(1 * 1000);
                    mInfoSend("设备复位完成, 等待托盘进入...");
                    mStateFlag = eTransState.Ready;
                    mStep = 1;
                    break;
            }
        }

        //就绪状态
        private void ReadyState()
        {

            switch (mStep)
            {
                case 1:
                    if (ClsGlobal.mIOControl.Get_M_TrayIn() == 1)
                    {

                        ClsGlobal.Trans_TrayLoc = eTrayLoc.InPlace;
                        if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                        {
                            mInfoSend("OCV Master托盘进入");
                        }
                        else
                        {
                            mInfoSend("托盘进入");
                        }
                        mInfoSend("请输入托盘条码...");
                        ClsGlobal.TrayCode = "";

                        mStateFlag = eTransState.TrayIn;
                        mStep = 1;
                    }
                    else
                    {
                        ClsGlobal.Trans_TrayLoc = eTrayLoc.NotInPlace;
                    }
                    break;
                default:
                    mStep = 0;
                    break;
            }
        }

        //进托盘状态
        private void TrayInState()
        {
            string strVal;
            if (ClsGlobal.mIOControl.Get_M_TrayIn() == 0)
            {
                mInfoSend("托盘被移出");
                mStateFlag = eTransState.Ready;
                mStep = 1;
                return;
            }

            switch (mStep)
            {
                case 1:
                    //判断是否输入托盘条码
                    if (GetTrayCode(out strVal) == 1)
                    {
                        //托盘扫码完成
                        ClsGlobal.TrayCode = strVal;

                        //设置电池类型
                        //用系统设置 或 托盘条码判定

                        mInfoSend("托盘条码: [" + strVal + "]");
                        mStep = 2;
                    }

                    break;
                case 2:
                    //托盘针床操作过程由下位机负责 由ajone于20200811 屏蔽
                    //if (ClsGlobal.BatteryType == "18650")
                    //{
                    //    ClsGlobal.mIOControl.Set_ReqBattType(1);
                    //}
                    //else
                    //{
                    //    ClsGlobal.mIOControl.Set_ReqBattType(2);
                    //}
                    ////请求托盘压合
                    //ClsGlobal.mIOControl.Set_ReqPressPB(1);
                    //mInfoSend("按启动按钮,压合针床....");
                    //mStep = 3;

                    //托盘针床操作过程由下位机负责 由ajone于20200811 新增
                    Thread.Sleep(1 * 1000);
                    try
                    {
                        if (ClsProbeRecover.ProbeSet.Probes.Max(a => a.Times) > ClsProbeRecover.ProbeSet.StopCount)
                        {
                            MessageBox.Show("探针寿命已经达到上限,无法继续工作,请更换探针后继续工作!", "警告");
                            mInfoSend("探针寿命已经达到上限,无法继续工作,,请更换探针后继续工作!");
                            throw new Exception("探针寿命已经达到上限,无法继续工作,,请更换探针后继续工作!");
                        }
                        else if (ClsProbeRecover.ProbeSet.Probes.Max(a => a.Times) > ClsProbeRecover.ProbeSet.WarningCount)
                        {
                            mInfoSend("探针寿命即将达到寿命上限,请预备好更换的探针!");
                        }
                        ClsProbeRecover.Add();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    mInfoSend("按启动按钮,压合针床....");
                    ClsGlobal.mIOControl.Set_DebugPressPB();  //发送启动请求

                    mStep = 3;
                    break;
                case 3:
                    //托盘针床操作过程由下位机负责 由ajone于20200811 屏蔽
                    ////托盘压合完成
                    //if (ClsGlobal.mIOControl.Get_M_EchoPressPB() == 1)
                    //{
                    //    ClsGlobal.mIOControl.Set_ReqPressPB(0);     //标志复位
                    //    mStep = 4;
                    //}


                    //托盘针床操作过程由下位机负责 由ajone于20200811 新增
                    if (ClsGlobal.mIOControl.Get_M_MECHA_IsWork() == 1) //托盘压合完成
                    {
                        Thread.Sleep(1 * 1000);
                        ClsGlobal.mIOControl.Set_DebugUnPressPB();  //工作启动后复位 启动请求
                        mStep = 4;
                    }

                    break;
                case 4:
                    //托盘针床操作过程由下位机负责 由ajone于20200811 屏蔽
                    //if (ClsGlobal.mIOControl.Get_M_EchoPressPB() == 0 &&
                    //    ClsGlobal.mIOControl.Get_M_PBPress() == 1)          //标志复位对接完成
                    //{
                    //    Time1 = System.DateTime.Now;
                    //    mStep = 5;
                    //}

                    //托盘针床操作过程由下位机负责 由ajone于20200811 新增
                    if (ClsGlobal.mIOControl.Get_M_MECHA_IsWork() == 1)          //针盘压合完成
                    {
                        Time1 = System.DateTime.Now;
                        mStep = 5;
                    }
                    else
                    {
                        //调试时使用,
                        Time1 = System.DateTime.Now;
                        mStep = 5;
                    }


                    break;
                case 5:
                    Ts = DateTime.Now - Time1;
                    if (Ts.TotalSeconds > 2)
                    {
                        //调试:延时等待针床压合稳定  
                        mInfoSend("针床压合完成");
                        ClsGlobal.Trans_TrayLoc = eTrayLoc.Pressing;
                        mStateFlag = eTransState.TestWork;
                        mStep = 1;
                        Val_1 = true;
                    }
                    break;
                default:
                    mStep = 0;
                    break;
            }
        }

        //测试状态
        private void TestWorkState()
        {
            switch (mStep)
            {
                case 1:
                    //测试开始
                    if (ClsGlobal.OCV_TestState == eTestState.Idle && Val_1 == true)
                    {
                        mInfoSend("准备做测试...");
                        ClsGlobal.Trans_RequestTest = 1;        //请求进行测试
                        Val_1 = false;
                    }
                    Thread.Sleep(10);
                    if (ClsGlobal.OCV_TestState == eTestState.Standby)
                    {
                        ClsGlobal.Trans_RequestTest = 0;        //请求清0
                        Val_1 = true;
                        mStep = 2;
                    }

                    break;
                case 2:
                    //OCV测试启动
                    //根据OCV测试的状态进行响应, 错误处理放在外面进行
                    if (ClsGlobal.OCV_TestState == eTestState.TestAgain)     //处理再次测试
                    {
                        mInfoSend("再次测试....");
                        mStep = 5;
                        break;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.AdjustAgain)
                    {
                        mInfoSend("再次校准测试....");
                        mStep = 5;
                        break;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.AdjustEnd)
                    {
                        mInfoSend("[校准]测试完成");
                        ClsGlobal.IRTrueValSetFlag = false;
                        ClsGlobal.mIOControl.Set_TestFinish();
                        mStep = 3;
                        break;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.TestEnd)
                    {
                        mInfoSend("[联网]测试完成");
                        ClsGlobal.mIOControl.Set_TestFinish();
                        mStep = 3;
                        break;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.OfflineTestEnd)
                    {
                        mInfoSend("[单机]测试完成");
                        ClsGlobal.mIOControl.Set_TestFinish();
                        mStep = 3;
                        break;
                    }

                    else if (ClsGlobal.OCV_TestState == eTestState.Testing && Val_1 == true)
                    {
                        Val_1 = false;
                        mInfoSend("测试中...");
                        break;
                    }
                    else if (ClsGlobal.OCV_TestState == eTestState.TestOK)
                    {
                        mInfoSend("测试完成.");
                        ClsGlobal.mIOControl.Set_TestFinish();
                        mStep = 3;
                    }
                    break;
                case 3:
                    if (ClsGlobal.mIOControl.Get_IOResetCompleted() == 1)
                    {
                        ClsGlobal.mIOControl.Set_ResetTestFinish();      //标志复位
                        mStep = 4;
                    }
                    break;
                case 4:
                    if (ClsGlobal.mIOControl.Get_M_MECHA_TrayClose() == 0)       //托盘压紧已经打开
                    {
                        mInfoSend("针床已打开,可移出托盘");
                        mStep = 5;
                    }
                    break;
                //再次测试的处理----------------------------------------------------
                //重复测试方案1: 在测试函数内部实现多次重复测试, 针床不打开.
                //重复测试方案2: 针床先打开再压合, 下位机将提供可设置的重复压合的专有命令.
                case 5:
                    if (ClsGlobal.OCV_TestState == eTestState.AdjustEnd)
                    {
                        mStep = 1;
                        ClsGlobal.OCVIRTest.TestFinish = true;
                        mStateFlag = eTransState.TrayTestFinish;
                        break;
                    }
                    //重复测试方案2...... 待续
                    if (ClsSysSetting.SysSetting.IsNGCheck)
                    {
                        //复测检查
                        if (ClsGlobal.listETCELL.Count(a => !string.IsNullOrEmpty(a.CODE) && !a.Cell_ID.StartsWith("000")) > 0)
                        {

                            if (MessageBox.Show($"当前还存在若干通道不良,是否需要进行复测?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ReTestWork();
                            }
                            else
                            {
                                ClsGlobal.OCVIRTest.TestFinish = true;
                                mStateFlag = eTransState.TrayTestFinish;
                            }
                        }
                        else
                        {
                            ClsGlobal.OCVIRTest.TestFinish = true;
                            mStateFlag = eTransState.TrayTestFinish;
                        }
                    }
                    else
                    {
                        ClsGlobal.OCVIRTest.TestFinish = true;
                        mStateFlag = eTransState.TrayTestFinish;
                    }
                    //良率预警检查
                    double pp = ((double)ClsGlobal.listETCELL.Count(a => string.IsNullOrEmpty(a.CODE) && !a.Cell_ID.StartsWith("000"))) / ClsGlobal.listETCELL.Count(a => !a.Cell_ID.StartsWith("000"));
                    pp = pp * 100;
                    if (ClsSysSetting.SysSetting.PassPropcent > pp)
                    {
                        mForm.Invoke(new EventHandler((oj, ea) =>
                        {
                            MessageBox.Show($"当前良率{pp.ToString("0.00")}%,良率设定值为{ClsSysSetting.SysSetting.PassPropcent.ToString("0.00")}%,请注意检查电池,并确认是否需要复测!");
                        }));
                    }
                    mStep = 1;
                    break;
                default:
                    mStep = 0;
                    break;
            }
        }

        //测试完成状态
        private void TrayTestFinishState()
        {

            switch (mStep)
            {
                case 0:

                    mStep = 1;
                    break;
                case 1:
                    //托盘是否移出
                    if (ClsGlobal.mIOControl.Get_M_TrayIn() == 0)
                    {
                        mInfoSend("托盘移出完成");
                        mStep = 2;
                    }
                    break;
                case 2:
                    ClsGlobal.Trans_TrayLoc = eTrayLoc.NotInPlace;
                    mStateFlag = eTransState.Ready;
                    mStep = 1;
                    break;
                default:
                    mStep = 0;
                    break;
            }

        }

        /// <summary>
        /// 获取条码在此处
        /// </summary>
        /// <param name="Val"></param>
        /// <returns></returns>
        int GetTrayCode(out string Val)
        {
            string str = "";
            ClsGlobal.IsTestRuning = true;
            if (ClsGlobal.NontUsingScaner)
            {
                //手动输入托盘码
                str = mForm.TxtTrayCodeScan.Text;
                if (str.Count() > 0 && str.Contains("\r\n") == true)
                {
                    try
                    {
                        //使用扫码枪
                        str = str.Trim();
                        mForm.SetTrayCode(str);
                        if (str.Count() > 0 && !str.Contains("NG") == true)
                        {
                            Val = str.Trim();
                            if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                            {
                                var batCodes = GetBatCode(Val);

                                LoadProcess(batCodes);
                                //var result = true;
                                var result = CheckTrayCode(Val); //暂时屏蔽托盘校验功能
                                if (result)
                                {
                                    ClsGlobal.TrayCode = Val;
                                    ClsGlobal.listETCELL = new List<ET_CELL>();
                                    for (int i = 0; i < batCodes.Count; i++)
                                    {
                                        ChanleMapingSetting.ListBatTestData.ForEach(a =>
                                        {
                                            if (a.TrayCodeChanle - 1 == i)
                                            {
                                                a.BatCode = batCodes[i];
                                                a.OCV = 0;
                                                a.ACIR = 0;
                                                a.CODE = "";
                                            }
                                        });
                                        ClsGlobal.listETCELL.Add(new ET_CELL()
                                        {

                                            Cell_ID = batCodes[i],
                                            Cell_Position = i,
                                            Pallet_ID = ClsGlobal.TrayCode
                                        });
                                    }
                                    return 1;
                                }
                                else
                                {
                                    mInfoSend($"验证托盘码失败");
                                    return 0;
                                }
                            }
                            else if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                            {
                                ClsGlobal.TrayCode = Val;
                                var batCodes = GetBatCode(Val);
                                LoadProcess(batCodes);
                                ClsGlobal.listETCELL = new List<ET_CELL>();
                                for (int i = 0; i < batCodes.Count; i++)
                                {
                                    ChanleMapingSetting.ListBatTestData.ForEach(a =>
                                    {
                                        if (a.TrayCodeChanle - 1 == i)
                                        {
                                            a.BatCode = batCodes[i];
                                            a.OCV = 0;
                                            a.ACIR = 0;
                                            a.CODE = "";
                                        }
                                    });
                                    ClsGlobal.listETCELL.Add(new ET_CELL()
                                    {
                                        Cell_ID = batCodes[i],
                                        Cell_Position = i,
                                        Pallet_ID = ClsGlobal.TrayCode
                                    });
                                }
                                return 1;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                        else
                        {
                            Val = null;
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        mInfoSend($"获取托盘码失败:{ex.Message}");
                        Val = null;
                        return 0;
                    }
                }
                else
                {
                    Val = null;
                    return 0;
                }
            }
            else
            {
                try
                {
                    //使用扫码枪
                    str = trayCodeScaner.ReadCode();
                    mForm.SetTrayCode(str);
                    if (str.Count() > 0 && !str.Contains("NG") == true)
                    {
                        Val = str.Trim();
                        if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                        {
                            var batCodes = GetBatCode(Val);
                            LoadProcess(batCodes);
                            //var result = true;
                            var result = CheckTrayCode(Val); //暂时屏蔽托盘校验功能
                            if (result)
                            {
                                ClsGlobal.TrayCode = Val;
                                ClsGlobal.listETCELL = new List<ET_CELL>();

                                for (int i = 0; i < batCodes.Count; i++)
                                {
                                    ChanleMapingSetting.ListBatTestData.ForEach(a =>
                                    {
                                        if (a.TrayCodeChanle - 1 == i)
                                        {
                                            a.BatCode = batCodes[i];
                                            a.OCV = 0;
                                            a.ACIR = 0;
                                            a.CODE = "";
                                        }
                                    });
                                    ClsGlobal.listETCELL.Add(new ET_CELL()
                                    {
                                        Cell_ID = batCodes[i],
                                        Cell_Position = i,
                                        Pallet_ID = ClsGlobal.TrayCode
                                    });
                                }
                                return 1;
                            }
                            else
                            {
                                mInfoSend($"验证托盘码失败");
                                return 0;
                            }
                        }
                        else if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                        {
                            ClsGlobal.TrayCode = Val;
                            var batCodes = GetBatCode(Val);
                            LoadProcess(batCodes);
                            ClsGlobal.listETCELL = new List<ET_CELL>();

                            for (int i = 0; i < batCodes.Count; i++)
                            {
                                ChanleMapingSetting.ListBatTestData.ForEach(a =>
                                {
                                    if (a.TrayCodeChanle - 1 == i)
                                    {
                                        a.BatCode = batCodes[i];
                                        a.OCV = 0;
                                        a.ACIR = 0;
                                        a.CODE = "";
                                    }
                                });
                                ClsGlobal.listETCELL.Add(new ET_CELL()
                                {
                                    Cell_ID = batCodes[i],
                                    Cell_Position = i,
                                    Pallet_ID = ClsGlobal.TrayCode
                                });
                            }
                            return 1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        Val = null;
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    mInfoSend($"获取托盘码失败:{ex.Message}");
                    Val = null;
                    return 0;
                }

            }
        }

        List<string> GetBatCode(string trayCode)
        {
            List<string> result = new List<string>();
            if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
            {
                result = GetBatCodeFormFile(trayCode);
            }
            else
            {
                result = GetBatCodeFormMES(trayCode);
            }
            return result;
        }

        bool LoadProcess(List<string> batCodes)
        {


            var miCode = batCodes.Select(a => a.Substring(0, 3)).Where(a => a != "000").Distinct().ToList();
            if (miCode.Count != 1)
            {
                throw new Exception("托盘中发现多个型号电池,停止测试!");
            }
            ClsProcessSet.LstProcess.ForEach(a =>
            {
                if (a.ProcessName.Substring((a.ProcessName.Length - 3), 3) == miCode[0] && (a.Type + 1) == ClsGlobal.OCVType)
                {
                    a.IsCurrent = true;
                }
                else
                {
                    a.IsCurrent = false;
                }
            });

            if (ClsProcessSet.WorkingProcess == null)
            {
                throw new Exception($"未发现与MI号[{miCode[0]}]相匹配的工程!");
            }
            else
            {
                return true;
            }


            //var name = ClsProcessSet.WorkingProcess.ProcessName;
            // name = name.Substring(name.Length-3, 3) ;
            //if (name == miCode[0])
            //{
            //    return true;
            //}
            //else
            //{
            //    throw new Exception("当前工程与托盘内电池码不匹配!");
            //}




        }

        List<string> GetBatCodeFormFile(string trayCode)
        {
            List<string> result = new List<string>();
            string path = ClsSysSetting.SysSetting.BatCodeSavePath;
            string fileName = "";
            DirectoryInfo directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            foreach (var f in files)
            {
                if (f.Name.Replace(f.Extension, "") == trayCode)
                {
                    fileName = f.FullName;
                    break;
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception($"离线模式下,未能找到与托盘码[{ClsGlobal.TrayCode}]相匹配的电池码文件!");
            }
            StreamReader str = new StreamReader(fileName);
            while (true)
            {
                if (str.EndOfStream)
                {
                    break;
                }
                string batCode = str.ReadLine();
                result.Add(batCode);
            }
            str.Close();
            for (int i = result.Count; i < 256; i++)
            {
                result.Add("00000000");
            }
            if (result.Count > 256)
            {
                result = result.Take(256).ToList(); ;
            }
            return result;
        }

        List<string> GetBatCodeFormMES(string trayCode)
        {
            List<string> result = new List<string>();
            string[] resultArry = new string[256];  //防止出现返回结果有空条码的情况
            var httpResult = mesManager.GetBattyCodesByTrayCode(trayCode);
            if (httpResult.status)
            {
                var lstCode = httpResult.result.OrderBy(a => a.TARY_NUMBER).ToList();
                foreach (var b in lstCode)
                {
                    resultArry[b.TARY_NUMBER - 1] = b.SFC_NO;
                }
            }
            result = resultArry.ToList();
            result.ForEach(a =>
            {
                if (string.IsNullOrEmpty(a))
                {
                    a = "00000000";
                }
            });
            return result;
        }

        bool CheckTrayCode(string trayCode)
        {
            bool result = false;
            try
            {

                string type = "";
                if (ClsGlobal.OCVType == 1)
                {
                    type = "OCV1";
                }
                else
                {
                    type = "OCV2";
                }
                var processName = "";
                HttpResult2 result2 = null;
                var count = ClsProcessSet.LstProcess.Count(a => a.IsCurrent);
                if (count > 1)
                {
                    mInfoSend($"发现{count}个符合条件的工程,开始逐个进行校验!");
                }
                foreach (var p in ClsProcessSet.LstProcess.Where(a => a.IsCurrent).ToList())
                {
                    string projectName = $"{p.ProcessName}";
                    mInfoSend($"开始校验[{trayCode}]-工程名[{p.ProcessName}]-{type}成功.");
                    result2 = mesManager.CheckTrayNoExist(trayCode, type, projectName);
                    if (result2.status)
                    {
                        processName = p.ProcessName;
                        mInfoSend($"托盘码[{trayCode}]-工程名[{p.ProcessName}]校验{type}成功.以当前工程进行测试.");
                        break;
                    }
                    else
                    {
                        mInfoSend($"托盘码[{trayCode}]-工程名[{p.ProcessName}]校验{type}失败:{result2.errMessage}");

                    }
                }
                ClsProcessSet.LstProcess.ForEach(a =>
                {
                    if (processName == a.ProcessName && (a.Type + 1) == ClsGlobal.OCVType)
                    {
                        a.IsCurrent = true;
                    }
                    else
                    {
                        a.IsCurrent = false;
                    }
                });
                if (ClsProcessSet.WorkingProcess == null)
                {
                    mInfoSend($"未发现符合当前托盘的工程.");
                    return false;
                }

                var span = DateTime.Now - DateTime.Parse(result2.dateTime);
                if (span.TotalHours > ClsProcessSet.WorkingProcess.SpanHourt)
                {
                    mInfoSend($"托盘码[{trayCode}]-工程名[{processName}]校验静置事件失败,当前要求最多静置{ClsProcessSet.WorkingProcess.SpanHourt}小时,实际静置{Math.Round(span.TotalHours, 2)}");
                    result = false;
                }
                else if (span.TotalHours < ClsProcessSet.WorkingProcess.SpanMinute)
                {
                    mInfoSend($"托盘码[{trayCode}]-工程名[{processName}]校验静置事件失败,当前要求最少静置{ClsProcessSet.WorkingProcess.SpanMinute}小时,实际静置{Math.Round(span.TotalHours, 2)}");
                    result = false;

                }
                else
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                mInfoSend(ex.Message);
            }
            return result;
        }
    }
}
