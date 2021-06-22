using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClsDevComm;

namespace OCV.OCVTest
{
    public class ClsOCVTest
    {
        //流程控制 
        private void Start()
        {
            while (true)
            {
                try
                {

                    string strTemp;
                    string msn;
                    try
                    {
                        if (OCVToken.Client == "A")
                        {
                            mProc = mFrmSys.ProcA;
                        }
                        else if (OCVToken.Client == "B")
                        {
                            mProc = mFrmSys.ProcB;
                        }

                        //复位初始化
                        //if (mProc.Trans_State == eTransState.Stop && InitFlag == 0)
                        if (mProc == null && InitFlag == 0)
                        {
                            //ClsGlobal.OCV_AdjustWorkOn = false;
                            ClsGlobal.IRTrueValSetFlag = false;
                            ClsGlobal.TestCount = 0;
                            ClsGlobal.AdjustCount = 0;
                            ClsGlobal.ArrAdjustACIR = new double[160];
                            ClsGlobal.ResRangeMode = 1;         //小量程
                            InitFlag = 1;
                            mProcStep = 0;
                        }

                        if (mProc != null && mProc.Trans_State == eTransState.Init)
                        {
                            InitFlag = 0;
                        }
                        if (mProc != null && mProc.RunState == 0)
                        {
                            OCVTestContr.StopAction();
                        }

                        switch (mProcStep)
                        {
                            case 0:   //OCV状态复位1 
                                Ts2 = DateTime.Now - DTStart2;
                                if (Ts2.TotalSeconds > 1)
                                {
                                    ClsGlobal.OCV_TestState = eTestState.Idle;
                                    ClsGlobal.OCV_TestState_Shadow = eTestState.Idle;
                                    mProcStep = 1;
                                }
                                break;
                            case 1:   //OCV状态复位2                        
                                      //测试结束,置OCV空闲状态    
                                if (ClsGlobal.OCV_TestState == eTestState.TestEnd && mProc.Trans_TrayLoc == eTrayLoc.NotInPlace)
                                {
                                    ClsGlobal.OCV_TestState = eTestState.Idle;             //OCV置为空闲 
                                    ClsGlobal.OCV_TestState_Shadow = eTestState.Idle;
                                    //不清空
                                    ClsGlobal.listETCELL = null;
                                    //禁止手动
                                    if (chkEnManual.Checked == true)
                                    {
                                        chkEnManual.Checked = false;
                                    }

                                    if (chkEnMultiTest.Checked == true)
                                    {
                                        chkEnMultiTest.Checked = false;
                                    }

                                    mProcStep = 2;
                                    break;
                                }

                                else if (mProc != null && ClsGlobal.OCV_TestState == eTestState.Idle) //&&
                                                                                                      //(mProc.Trans_TrayLoc == eTrayLoc.InPlace ||
                                                                                                      //mProc.Trans_TrayLoc == eTrayLoc.Pressing))
                                {
                                    ClsGlobal.OCV_TestState_Shadow = eTestState.Idle;
                                    mProcStep = 2;
                                    break;
                                }

                                break;
                            case 2:
                                #region 有申请测试情况
                                //if (mProc.Trans_TrayLoc == eTrayLoc.Pressing || mProc.Trans_TrayLoc == eTrayLoc.InPlace &&
                                //    mProc.Trans_RequestTest == 1)   
                                #endregion

                                #region 有申请测试情况 托盘电池数据获取
                                //增加电池信号检查功能
                                //if (mProc.Trans_TrayLoc == eTrayLoc.Pressing || mProc.Trans_TrayLoc == eTrayLoc.InPlace &&
                                //    mProc.Trans_RequestTest == 1 && ClsGlobal.CurrentTestPart == 1 && mProc.Trans_State == eTransState.TestWork)       //输送设备的请求已清0
                                //增加电池信号检查功能Trans_RequestVerify
                                if (mProc.Trans_TrayLoc == eTrayLoc.NotInPlace && mProc.Trans_RequestVerify == 1 &&
                                    mProc.Trans_RequestTest == 0 && ClsGlobal.CurrentTestPart == 1 && mProc.Trans_State == eTransState.TrayIn)       //输送设备的请求已清0
                                {
                                    ClsGlobal.OCV_TestState = eTestState.GetData;       //获取数据  
                                    string strMsn = "";
                                    //单机
                                    if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                                    {
                                        mDBCOM_OCV_QT.Get_ETCell_Offline(mProc.TrayCode, out ClsGlobal.listETCELL);
                                        ClsGlobal.BattInfoReqFlag = ClsGetConfigDetail.GetProjectInfo(1, ClsGlobal.OCVType);
                                        strMsn = "单机运行中,不获取托盘电池信息";
                                        mProc.mInfoSend(strMsn);
                                        ClsGlobal.sLogspath = OCVToken.Client;
                                        ClsGlobal.WriteLog(strMsn);
                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                        //测试就绪
                                        //ClsGlobal.OCV_TestState = eTestState.Standby;
                                        //mProcStep = 6;
                                        ClsGlobal.OCV_TestState = eTestState.OKBatModel;
                                        mProcStep = 16;
                                    }
                                    //校准
                                    else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                                    {
                                        mDBCOM_OCV_QT.Get_ETCell_Offline(mProc.TrayCode, out ClsGlobal.listETCELL); ;
                                        strMsn = "校准测试中,不获取托盘电池信息";
                                        mProc.mInfoSend(strMsn);
                                        ClsGlobal.sLogspath = OCVToken.Client;
                                        ClsGlobal.WriteLog(strMsn);
                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;

                                        if (ClsGlobal.IRTrueValSetFlag == false)
                                        {
                                            strMsn = "尚未设置校准托盘内阻基准值,校准取消";
                                            ClsGlobal.sLogspath = OCVToken.Client;
                                            ClsGlobal.WriteLog(strMsn);
                                            this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                            ClsGlobal.OCV_TestState = eTestState.ErrAdjustTestNoSet;
                                            DTStart2 = DateTime.Now;
                                            mProcStep = 0;
                                            return;
                                        }
                                        //测试就绪
                                        //ClsGlobal.OCV_TestState = eTestState.Standby;
                                        //mProcStep = 6;
                                        ClsGlobal.OCV_TestState = eTestState.OKBatModel;
                                        mProcStep = 16;
                                    }
                                    //正常                                
                                    else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                                    {
                                        if (mProc.isTestAgainState == false)
                                        {
                                            ClsGlobal.BattInfoReqFlag = -1;
                                            strMsn = "正在获取托盘[" + mProc.TrayCode + "]电池信息数据及工艺";
                                            mProc.mInfoSend(strMsn);
                                            this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                            //获取擎天化成分容数据库数据
                                            GetBatInfoFromQT(mProc.TrayCode);

                                        }
                                        else
                                        {
                                            ClsGlobal.BattInfoReqFlag = 0;
                                        }
                                        mProcStep = 3;
                                    }
                                }
                                break;
                            #endregion

                            case 3:
                                #region  刷新读取电池信息
                                if (ClsGlobal.BattInfoReqFlag != -1)
                                {
                                    string strMsn = "";

                                    if (ClsGlobal.BattInfoReqFlag == 1)
                                    {
                                        strMsn = "托盘[" + mProc.TrayCode + "]电池数据获取失败！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 2)
                                    {
                                        strMsn = "托盘[" + mProc.TrayCode + "]电池条码重复！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 3)
                                    {
                                        strMsn = "托盘[" + mProc.TrayCode + "]电池位置号重复！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 4)
                                    {
                                        strMsn = "托盘条码为空！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 5)
                                    {
                                        strMsn = "托盘[" + mProc.TrayCode + "]电池项目号不唯一！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 7)
                                    {
                                        strMsn = "托盘[" + mProc.TrayCode + "]没有登录时间信息！";
                                    }

                                    if (ClsGlobal.BattInfoReqFlag != 0)
                                    {
                                        ClsGlobal.sLogspath = OCVToken.Client;
                                        ClsGlobal.WriteLog(strMsn);
                                        mProc.mInfoSend(strMsn);
                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                        ClsGlobal.OCV_TestState = eTestState.ErrOCVDataGetFail;
                                        DTStart2 = DateTime.Now;
                                        mProcStep = 0;
                                        break;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < ClsGlobal.TrayType; i++)
                                        {

                                            if (ClsGlobal.listETCELL[i].MODEL_NO != "" && ClsGlobal.listETCELL[i].MODEL_NO != null
                                                && ClsGlobal.listETCELL[i].BATCH_NO != "" && ClsGlobal.listETCELL[i].BATCH_NO != null)
                                            {
                                                ClsGlobal.MODEL_NO = ClsGlobal.listETCELL[i].MODEL_NO;
                                                ClsGlobal.BATCH_NO = ClsGlobal.listETCELL[i].BATCH_NO;
                                                ClsGlobal.PROJECT_NO = ClsGlobal.listETCELL[i].PROJECT_NO;

                                            }
                                        }
                                        strMsn = "托盘[" + mProc.TrayCode + "]绑定的电池型号为：" + ClsGlobal.MODEL_NO + "  批次号为：" + ClsGlobal.BATCH_NO + "！";
                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                        mProc.mInfoSend(strMsn);
                                    }
                                    int IsVerifyBatModel = 0;
                                    if (ClsGlobal.EnBatModel == 1)
                                    {
                                        IsVerifyBatModel = VerifyBatModel();
                                    }
                                    else
                                    {
                                        IsVerifyBatModel = 1;
                                        strMsn = "设备设置不检验电池型号";
                                        ClsGlobal.WriteLog(strMsn);
                                    }
                                    if (IsVerifyBatModel == 1)
                                    {

                                        if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                                        {
                                            int Bret = 0;
                                            for (int i = 0; i < ClsGlobal.listETCELL.Count; i++)
                                            {
                                                string sfc = ClsGlobal.listETCELL[i].Cell_ID;
                                                if (sfc.Substring(0, 12) != "NullCellCode" && !sfc.Contains("BYD"))
                                                {
                                                    Bret = mDBCOM_SVSQT.Get_BatTestCount(ClsGlobal.OCVType, ClsGlobal.listETCELL[i].Cell_ID);
                                                    if (Bret > ClsGlobal.ProcTestCount)
                                                    {
                                                        strMsn = "异常:电池[" + sfc + "】超过返工工艺测试次数，不允许测试！";
                                                        mProc.mInfoSend(strMsn);
                                                        ClsGlobal.sLogspath = OCVToken.Client;
                                                        ClsGlobal.WriteLog(strMsn);
                                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                                        ClsGlobal.OCV_TestState = eTestState.ErrOCVXDataGetFail;
                                                        DTStart2 = DateTime.Now;
                                                        mProc.InitReset();
                                                        mProcStep = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (Bret <= ClsGlobal.ProcTestCount)
                                            {
                                                //当设备测试OCV2,OCV3时从本地数据库读取OCV1  OCV的值
                                                if ((ClsGlobal.OCVType == 2 || ClsGlobal.OCVType == 3) && mProc.isTestAgainState == false)
                                                {
                                                    strMsn = "正在获取托盘[" + mProc.TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据";
                                                    mProc.mInfoSend(strMsn);
                                                    //获取
                                                    int ret = mDBCOM_OCV_QT.Get_BattInfo(ClsGlobal.OCVType, mProc.TrayCode, ref ClsGlobal.listETCELL, ref ClsGlobal.listETCELL_VS);   //ref
                                                                                                                                                                                      // int ret2 = mDBCOM_OCV_QT.Get_BattVSInfo(ClsGlobal.OCVType, mProc.TrayCode, ref ClsGlobal.listETCELL_VS);   //ref
                                                    if (ret == 1 || ret == 2)
                                                    {
                                                        strMsn = "异常:获取[" + mProc.TrayCode + "]的OCV" + ret + "数据异常!";
                                                        mProc.mInfoSend(strMsn);
                                                        ClsGlobal.sLogspath = OCVToken.Client;
                                                        ClsGlobal.WriteLog(strMsn);
                                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                                        ClsGlobal.OCV_TestState = eTestState.ErrOCVXDataGetFail;
                                                        // Thread.Sleep(200);
                                                        DTStart2 = DateTime.Now;
                                                        mProc.InitReset();
                                                        mProcStep = 0;
                                                        break;

                                                    }
                                                    else
                                                    {
                                                        strMsn = "获取托盘[" + mProc.TrayCode + "]的OCV" + (ClsGlobal.OCVType - 1) + "的测试数据成功";
                                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                                        mProc.mInfoSend(strMsn);
                                                        int Mret = VerifyBatCode(ClsGlobal.listETCELL, ClsGlobal.listETCELL_VS);
                                                        if (Mret == 1)
                                                        {
                                                            strMsn = "异常:当前测试托盘电池数量与服务器托盘最新登录的电池数量不一致";
                                                            mProc.mInfoSend(strMsn);
                                                            ClsGlobal.sLogspath = OCVToken.Client;
                                                            ClsGlobal.WriteLog(strMsn);
                                                            this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                                            ClsGlobal.OCV_TestState = eTestState.ErrOCVXDataGetFail;
                                                            // Thread.Sleep(200);
                                                            DTStart2 = DateTime.Now;
                                                            mProc.InitReset();

                                                            mProcStep = 0;
                                                            break;
                                                        }
                                                        else if (Mret == 2)
                                                        {
                                                            strMsn = "异常:当前测试托盘的OCV" + (ClsGlobal.OCVType - 1) + "电池条码与服务器托盘最新登录的电池不一致,请检查此托盘是否有做OCV" + (ClsGlobal.OCVType - 1);
                                                            this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                                            mProc.mInfoSend(strMsn);
                                                            ClsGlobal.sLogspath = OCVToken.Client;
                                                            ClsGlobal.WriteLog(strMsn);
                                                            this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
                                                            ClsGlobal.OCV_TestState = eTestState.ErrOCVXDataGetFail;
                                                            // Thread.Sleep(200);
                                                            DTStart2 = DateTime.Now;
                                                            mProc.InitReset();
                                                            mProcStep = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                                ClsGlobal.OCV_TestState = eTestState.OKBatModel;
                                                mProcStep = 15;
                                            }
                                        }

                                    }
                                    else if (IsVerifyBatModel == 2)
                                    {
                                        ClsGlobal.OCV_TestState = eTestState.ErrNoSetBatModel;
                                        strMsn = "异常: 当前设备未设置测试电池型号";
                                        mProc.mInfoSend(strMsn);
                                        mProcStep = 0;
                                        break;
                                    }
                                    else if (IsVerifyBatModel == 3)
                                    {
                                        ClsGlobal.OCV_TestState = eTestState.NoBatModel;
                                        strMsn = "异常: 针床位置错误！";
                                        mProc.mInfoSend(strMsn);
                                        mProcStep = 0;
                                        break;
                                    }
                                    else
                                    {
                                        ClsGlobal.OCV_TestState = eTestState.NoBatModel;
                                        strMsn = "异常: 托盘[" + mProc.TrayCode + "]中电池型号不适用与当前设备";
                                        mProc.mInfoSend(strMsn);
                                        mProcStep = 0;
                                        break;
                                    }
                                }
                                else
                                {
                                    mProcStep = 3;
                                }
                                break;
                            #endregion
                            case 15:
                                //输送部请求检验电池型号清0
                                if (mProc.Trans_RequestVerify == 0)
                                {
                                    if (mProc.Trans_State == eTransState.Stop)
                                    {
                                        mProcStep = 1;
                                    }
                                    else
                                    {
                                        mProcStep = 4;
                                    }
                                }
                                break;
                            case 16:
                                //输送部请求做测试
                                if (mProc.Trans_RequestVerify == 0 && mProc.Trans_TrayLoc == eTrayLoc.Pressing || mProc.Trans_TrayLoc == eTrayLoc.InPlace &&
                                    mProc.Trans_RequestTest == 1 && mProc.Trans_State == eTransState.TestWork)       //输送设备的请求已清0           
                                {
                                    //测试就绪
                                    ClsGlobal.OCV_TestState = eTestState.Standby;
                                    mProcStep = 6;
                                }
                                break;
                            case 4:
                                #region 获取工艺信息
                                string sMsn = "";
                                if (mProc.isTestAgainState == false)
                                {
                                    string strMsn = "";
                                    ClsGlobal.BattInfoReqFlag = -1;
                                    strMsn = "正在获取托盘[" + mProc.TrayCode + "]电池工艺";
                                    mProc.mInfoSend(strMsn);
                                    this.txtLog.Text = System.DateTime.Now.ToString() + ":" + strMsn + "\r\n" + this.txtLog.Text;
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
                                        sMsn = "无工艺文件，请设置工艺！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 21)
                                    {
                                        sMsn = "托盘[" + mProc.TrayCode + "]此电池型号无此电池型号工艺，请设置。";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 22)
                                    {
                                        sMsn = "托盘[" + mProc.TrayCode + "]此电池型号没有设置默认工艺，请设置。";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 23)
                                    {
                                        sMsn = "托盘[" + mProc.TrayCode + "]此电池型号无对应工艺，请设置。";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 24)
                                    {
                                        sMsn = "没有工艺文件！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 25)
                                    {
                                        sMsn = "获取工艺参数失败！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 26)
                                    {
                                        sMsn = "MES无对应工艺版本返回！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 27)
                                    {
                                        sMsn = "MES返回工艺中没有默认的工艺,请设置后重新启动！";
                                    }
                                    else if (ClsGlobal.BattInfoReqFlag == 28)
                                    {
                                        sMsn = "MES无返回工艺参数信息返回！";
                                    }

                                    ClsGlobal.sLogspath = OCVToken.Client;
                                    ClsGlobal.WriteLog(sMsn);
                                    mProc.mInfoSend(sMsn);
                                    this.txtLog.Text = System.DateTime.Now.ToString() + ":" + sMsn + "\r\n" + this.txtLog.Text;
                                    ClsGlobal.OCV_TestState = eTestState.ErrGetProject;
                                    DTStart2 = DateTime.Now;
                                    mProcStep = 0;

                                    break;
                                }
                                else
                                {
                                    sMsn = "托盘[" + mProc.TrayCode + "]获取电池工艺成功！";
                                    this.txtLog.Text = System.DateTime.Now.ToString() + ":" + sMsn + "\r\n" + this.txtLog.Text;
                                    mProc.mInfoSend(sMsn);
                                    try
                                    {
                                        ClsGetConfigDetail.CheckIsolation();  //判断隔离
                                    }
                                    catch (Exception ex)
                                    {
                                        sMsn = "托盘[" + mProc.TrayCode + "]验证隔离条件异常！";
                                        this.txtLog.Text = System.DateTime.Now.ToString() + ":" + sMsn + "\r\n" + this.txtLog.Text;
                                        mProc.mInfoSend(sMsn);
                                        throw new Exception(sMsn + ex.Message);
                                    }
                                    mProcStep = 5;
                                }
                                #endregion
                                break;
                            case 5:
                                #region  测试就绪
                                //验证时间
                                VerifyTestTime();
                                if (ClsGlobal.DownLmt_time > ClsGlobal.GetHours)
                                {
                                    msn = "工艺间隔时间小于下限";
                                    mProc.mInfoSend("工艺间隔时间小于下限");
                                    ClsGlobal.sLogspath = OCVToken.Client;
                                    ClsGlobal.WriteLog(msn);
                                    this.txtLog.Text = System.DateTime.Now.ToString() + ":" + msn + "\r\n" + this.txtLog.Text;
                                    ClsGlobal.OCV_TestState = eTestState.ErrOCVXDataGetFail;
                                    mProcStep = 0;
                                    break;
                                }
                                else if (ClsGlobal.GetHours > ClsGlobal.UpLmt_time)
                                {
                                    msn = "工艺间隔时间大于下限";
                                    mProc.mInfoSend("工艺间隔时间大于上限");
                                    ClsGlobal.sLogspath = OCVToken.Client;
                                    ClsGlobal.WriteLog(msn);
                                    this.txtLog.Text = System.DateTime.Now.ToString() + ":" + msn + "\r\n" + this.txtLog.Text;
                                    ClsGlobal.OCV_TestState = eTestState.ErrOCVXDataGetFail;
                                    mProcStep = 0;
                                    break;
                                }
                                //测试控制参数更新
                                OCVTestContr.InitPara();
                                if (ClsGlobal.CellStyle == 0)
                                {
                                    msn = "获取电池品种失败!";
                                    ClsGlobal.OCV_TestState = eTestState.ErrCellStyleCode;
                                    DTStart2 = DateTime.Now;
                                    mProcStep = 0;
                                }
                                else
                                {
                                    //界面处理->DGView重建
                                    if (ClsGlobal.DataDGVShow != ClsGlobal.TrayType)
                                    {
                                        ClsGlobal.DataDGVShow = ClsGlobal.TrayType;

                                        int Rows = 0;
                                        Rows = ClsGlobal.TrayType / 2;

                                        dgvTest.Rows.Clear();
                                        dgvTest.Rows.Add(Rows);
                                        for (int i = 0; i < Rows; i++)
                                        {
                                            dgvTest.Rows[i].Cells[0].Value = (i + 1).ToString();
                                            dgvTest.Rows[i].Cells[7].Value = (i + 1 + Rows).ToString();
                                        }
                                    }

                                    //测试就绪
                                    //ClsGlobal.OCV_TestState = eTestState.Standby;
                                    mProcStep = 16;
                                }
                                #endregion
                                break;
                            case 6:
                                //准备OCV测试
                                //输送部请求清0
                                if (mProc.Trans_RequestTest == 0)
                                {
                                    mProcStep = 7;
                                }
                                break;
                            case 7:
                                #region OCV测试中

                                //界面处理->表格数据清空
                                if (ClsGlobal.CurrentTestPart == 1)
                                {
                                    int Val = ClsGlobal.TrayType / 2;
                                    for (int i = 0; i < Val; i++)
                                    {
                                        for (int j = 1; j < 6; j++)
                                        {
                                            dgvTest.Rows[i].Cells[j].Value = "";
                                            dgvTest.Rows[i].Cells[4].Style.ForeColor = Color.Black;
                                            dgvTest.Rows[i].Cells[1].Style.BackColor = Color.White;
                                            dgvTest.Rows[i].Cells[2].Style.BackColor = Color.White;
                                            dgvTest.Rows[i].Cells[3].Style.BackColor = Color.White;
                                            dgvTest.Rows[i].Cells[4].Style.BackColor = Color.White;
                                        }

                                        for (int j = 8; j < 13; j++)
                                        {
                                            dgvTest.Rows[i].Cells[j].Value = "";
                                            dgvTest.Rows[i].Cells[11].Style.ForeColor = Color.Black;
                                            dgvTest.Rows[i].Cells[8].Style.BackColor = Color.White;
                                            dgvTest.Rows[i].Cells[9].Style.BackColor = Color.White;
                                            dgvTest.Rows[i].Cells[10].Style.BackColor = Color.White;
                                            dgvTest.Rows[i].Cells[11].Style.BackColor = Color.White;
                                        }
                                    }
                                }
                                //进行测试
                                ClsGlobal.OCV_TestState = eTestState.Testing;
                                try
                                {
                                    System.Threading.Thread.Sleep(200);
                                    OCVTestContr.StartTestAction();
                                    System.Threading.Thread.Sleep(100);
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message == "测试被终止")
                                    {

                                    }
                                    else
                                    {
                                        ClsGlobal.OCV_TestState = eTestState.ErrOCVTest;
                                        ClsGlobal.OCV_TestErrDetail = "准备进行测试时出错";
                                        OCVTestContr.StopAction();
                                    }
                                    throw ex;
                                }
                                mProcStep = 8;

                                #endregion
                                break;
                            case 8:
                                #region OCV测试完成判断

                                //异常判断
                                if (OCVTestContr.TestFinish == false &&
                                    ClsGlobal.OCV_TestState == eTestState.ErrOCVTest)
                                {
                                    msn = "OCV测试异常! " + ClsGlobal.OCV_TestErrDetail;
                                    ClsGlobal.sLogspath = OCVToken.Client;
                                    ClsGlobal.WriteLogOCV(msn);
                                    ShowMsn(msn);
                                    DTStart2 = DateTime.Now;
                                    mProcStep = 0;
                                    return;
                                }
                                if (OCVTestContr.TestFinish == true)
                                {
                                    mProcStep = 9;

                                    if (ClsGlobal.OCV_TestState != eTestState.TestAgain)
                                    {
                                        mProc.mInfoSend("OCV测试完成");
                                        mProc.mInfoSend("开始测试温度");
                                    }

                                }
                                //if ((ClsGlobal.OCV_RunMode == eRunMode.NormalTest ||
                                //    ClsGlobal.OCV_RunMode == eRunMode.OfflineTest) && OCVTestContr.TestFinish == true)
                                //{
                                //    //测试位全部做完,才进行温度测试
                                //    if (ClsGlobal.CurrentTestPart == ClsGlobal.TestPartsCount)
                                //    {
                                //        mProcStep = 10;    //mProcStep = 7;不测温度,直接跳过
                                //    }
                                //    else
                                //    {
                                //        mProcStep = 10;
                                //    }
                                //}
                                //else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                                //{
                                //    mProcStep = 10;
                                //}
                                #endregion
                                break;
                            case 9:
                                #region 测试温度
                                if (OCVTestContr.TestFinish == true)
                                {
                                    if (ClsGlobal.OCV_TestState == eTestState.TestAgain)
                                    {
                                        TempContr.TestFinish = true;
                                        mProcStep = 10;
                                        break;
                                    }
                                    if (ClsGlobal.EN_TestTemp == 1)
                                    {
                                        //延迟测试功能
                                        if (ClsGlobal.EnDelayTEMPTest == 0)
                                        {
                                            StartTestAction();
                                            mProcStep = 10;
                                        }
                                        else if (ClsGlobal.EnDelayTEMPTest == 1)
                                        {
                                            Ts = DateTime.Now - ClsGlobal.TestStartTime;
                                            if (Ts.TotalSeconds > ClsGlobal.DelayTEMPTime)
                                            {
                                                StartTestAction();
                                                mProcStep = 10;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TempContr.TestFinish = true;
                                        mProcStep = 10;
                                    }
                                }
                                #endregion
                                break;
                            case 10:
                                #region 数据处理
                                if (OCVTestContr.TestFinish == true && TempContr.TestFinish == true)  //
                                {

                                    //根据运行模式进行处理
                                    //脱机测试
                                    if (ClsGlobal.OCV_RunMode == eRunMode.OfflineTest)
                                    {
                                        if (ClsGlobal.OCV_TestState == eTestState.TestOK)
                                        {
                                            if (ClsGlobal.CurrentTestPart == ClsGlobal.TestPartsCount)
                                            {
                                                msn = "单机测试OK，不保存数据库数据!";
                                                ClsGlobal.sLogspath = OCVToken.Client;
                                                ClsGlobal.WriteLog(msn);
                                                ShowMsn(msn);
                                                ExportLocalData();      //保存本地后结束    
                                                                        //try
                                                                        //{
                                                                        //    ExportToSqlServer();
                                                                        //}
                                                                        //catch (Exception ex)
                                                                        //{
                                                                        //    ClsGlobal.sLogspath = OCVToken.Client;
                                                                        //    ClsGlobal.WriteLog(ex.Message + ex.StackTrace);//将错误信息写入到Log里,便于观察
                                                                        //    MessageBox.Show(ex.ToString() + ex.StackTrace + "666");
                                                                        //}
                                                ClsGlobal.OCV_TestState = eTestState.OfflineTestEnd;
                                                DTStart2 = DateTime.Now;
                                                mProcStep = 0;
                                            }
                                            else
                                            {
                                                ClsGlobal.OCV_TestState = eTestState.TestNextPart;  //到下一个测试位
                                                msn = "本测试位完成,继续下一测试位...";
                                                ClsGlobal.sLogspath = OCVToken.Client;
                                                ClsGlobal.WriteLog(msn);
                                                ShowMsn(msn);
                                                DTStart2 = DateTime.Now;
                                                mProcStep = 0;
                                            }
                                        }
                                        else
                                        {
                                            msn = "再测定...";
                                            ClsGlobal.sLogspath = OCVToken.Client;
                                            ClsGlobal.WriteLog(msn);
                                            //ShowMsn(msn);
                                            mProcStep = 2;       //重新测量
                                        }
                                    }
                                    #region 正常测试
                                    else if (ClsGlobal.OCV_RunMode == eRunMode.NormalTest)
                                    {
                                        if (ClsGlobal.OCV_TestState == eTestState.TestOK)   //测试OK
                                        {
                                            if (ClsGlobal.CurrentTestPart == ClsGlobal.TestPartsCount)  //全部测试完成
                                            {
                                                msn = "测试完成...";
                                                ClsGlobal.sLogspath = OCVToken.Client;
                                                ClsGlobal.WriteLog(msn);
                                                mProc.mInfoSend("测试完成...");
                                                ShowMsn(msn);
                                                mProcStep = 11;
                                            }
                                            else
                                            {
                                                ClsGlobal.OCV_TestState = eTestState.TestNextPart;  //到下一个测试位
                                                msn = "本测试位测试完成,继续下一测试位...";
                                                ClsGlobal.sLogspath = OCVToken.Client;
                                                ClsGlobal.WriteLog(msn);
                                                ShowMsn(msn);
                                                DTStart2 = DateTime.Now;
                                                mProcStep = 0;
                                            }
                                        }
                                        else if (ClsGlobal.OCV_TestState == eTestState.TestAgain)  //再测定
                                        {
                                            msn = "再测定...";
                                            ClsGlobal.sLogspath = OCVToken.Client;
                                            ClsGlobal.WriteLog(msn);
                                            //ShowMsn(msn);
                                            mProcStep = 2;
                                        }
                                    }
                                    #endregion

                                    #region 校准测试
                                    else if (ClsGlobal.OCV_RunMode == eRunMode.ACIRAdjust)
                                    {
                                        if (ClsGlobal.OCV_TestState == eTestState.AdjustOK)
                                        {
                                            msn = "校准数据保存";
                                            ClsGlobal.sLogspath = OCVToken.Client;
                                            ClsGlobal.WriteLog(msn);
                                            ShowMsn(msn);
                                            SaveAdjustData();

                                            //复位校准参数
                                            ClsGlobal.IRTrueValSetFlag = false;
                                            ClsGlobal.AdjustCount = 0;
                                            ClsGlobal.ArrAdjustACIR = new double[88];
                                            DTStart2 = DateTime.Now;
                                            mProcStep = 0;
                                        }
                                        else if (ClsGlobal.OCV_TestState == eTestState.AdjustAgain)
                                        {
                                            msn = "再校准测定...";
                                            ClsGlobal.sLogspath = OCVToken.Client;
                                            ClsGlobal.WriteLog(msn);
                                            //ShowMsn(msn);
                                            mProcStep = 2;
                                        }
                                        return;
                                    }
                                    #endregion
                                }
                                #endregion
                                break;
                            case 11:
                                #region 保存数据
                                strTemp = "保存数据...";
                                ClsGlobal.sLogspath = OCVToken.Client;
                                ClsGlobal.WriteLogOCV(strTemp);
                                ShowMsn(strTemp);

                                //联机测试下保存
                                ExportToLocFinish = false;
                                ExportToServerFinish = false;


                                //保存到本地                           
                                Thread threadExportData = new Thread(new ThreadStart(ExportLocalData));
                                threadExportData.IsBackground = true;
                                threadExportData.Start();

                                //保存到服务器的数据库
                                //包括本地服务器以及擎天服务器 20181018
                                Thread threadExportToServer = new Thread(new ThreadStart(ExportToServer));
                                threadExportToServer.IsBackground = true;
                                threadExportToServer.Start();

                                //保存到本地数据库     不使用wpl   20180724                       
                                //Thread threadWriteToSqlServer = new Thread(new ThreadStart(ExportToSqlServer));
                                //threadWriteToSqlServer.IsBackground = true;
                                //threadWriteToSqlServer.Start();

                                mProcStep = 12;
                                #endregion
                                break;
                            case 12:
                                #region 保存数据完成
                                if (ClsGlobal.OCV_TestState == eTestState.ErrDataSaveQTFail)
                                {
                                    DTStart2 = DateTime.Now;
                                    mProcStep = 0;
                                    return;
                                }
                                if (ExportToLocFinish == true && ExportToServerFinish == true)
                                {
                                    ClsGlobal.OCV_TestState = eTestState.TestEnd;
                                    DTStart2 = DateTime.Now;
                                    mProcStep = 1;
                                }
                                #endregion
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        TimeStopCheck = false;
                        ClsGlobal.AdjustCount = 0;
                        ClsGlobal.sLogspath = OCVToken.Client;
                        ClsGlobal.OCV_TestState = eTestState.ErrOCVDataGetFail;
                        //mProc.Stop();
                        mProcStep = 0;
                        ClsGlobal.WriteLog(ex.Message + ex.StackTrace);    //将错误信息写入到Log里,便于观察
                                                                           // MessageBox.Show(ex.ToString()+ex.StackTrace);
                    }
                    finally
                    {
                        //if (ClsGlobal.CloseProg == true)
                        //{
                        //    this.Close();
                        //}
                        TimeStopCheck = false;
                    }
                }
                catch (Exception exp)
                {
                    Logger.Log("PLCControl:" + exp.Message + exp.StackTrace, true);

                    errMsg = exp.Message;

                    Thread.Sleep(1000);
                }

                //3s
                Thread.Sleep(800);
            }
        }
    }
}
