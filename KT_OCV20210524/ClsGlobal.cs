using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DB_OCV.DAL;


using System.Reflection;
using System.Text.RegularExpressions;

namespace OCV
{
    /// <summary>
    /// //标记密码权限类型  1：普通   2： 高级     3：工艺
    /// </summary>
    public enum PwdType
    {
        USER = 1,
        PROCESS = 2,
        ADVCMD = 3,
        kinte = 4,
    }

    public class ClsGlobal
    {
        public static DBCOM_OCV mDBCOM_OCV_QT;     //OCV数据库接口  擎天服务器

        //public static DBCOM_OCV mDBCOM_OCV_Local;  //OCV数据库接口  本地服务器

        //public static DBCOM_SVS_QT mDBCOM_SVSQT;       //分容数据库接口  

        public static ClsWCSCOM WCSCOM;      //物流系统接口  

        public static ClsOCVContr OCVTestContr;    //OCV测试控制

        public static ClsTempContrT2 TempContr;     //温度测试控制

        public static ClsCodeScan CodeScan = new ClsCodeScan();  //扫码器

        public static SqLiteHelper sql;            //sqlLite

        //public static ClsLocaRange LocaContr;     //传感器位移控制
     
        public static double LocaRange;

        public static string FlowTimeA;
        public static string TraycodeA;

        public static bool SetParflag = false;   //是否有设置参数
        //public static bool OCVUpload = false;
        //public static bool MesUpload = false;

        public static string SystemPwd = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.DayOfWeek.ToString("d");

        #region 共用部    

        //工程设定获取类型
        public static int ProjectSetType;               //本地获取->1 ,MES获取->2 ,服务器获取->2
        public static string Project;                   //工程名
        //设备名称
        public static string DeviceName;
        //设备序号
        public static string DeviceNo;                  //一种类型的OCV可有多台

        //OCV类型 
        public static int OCVType;                      //OCV1: 充放电后OCV , OCV2:老化后OCV, OCV3:分选前OCV

     //   public static int mShellTestType;

        public static eTestState OCV_TestState;          //OCV测试状态
        public static string DeviceCode;   //设备代码
        public static string OPEATION_ID; //资源编号
        public static string JT_NO;      //机台编号
        public static string SITE;       //站点
        public static string RESRCE;      //岗位条码
        public static string PCID;      //PCID
        public static string USER_NO;     //作业员    
        public static string USER_Name;     //作业员 
        public static int UserAuthority = 0;  //登录的权限
        public static ushort BatNum = 0;    //托盘实际电池数量
        public static ushort NGBatNum = 0;  // NG电池数量
        public static string ProcessName;   //分容工艺名称
        public static string Technology;   //分容工艺标志
      
        //托盘类型
        public static int TrayType;                     //托盘电池数: 80
        public static int TrayTypeFlag=1;               //托盘类型标志
        public static int Lastboard;                    //切换板个数

        public static int CHCount = 19;                 //每个切换箱得通道数

        //public static int StartA;                        //A单元开始的位置
        //public static int StartB;                        //B单元开始的位置
        public static string TrayCodeRegEx = "";
        public static string CellCodeRegEx = "";

        //测量类型
        public static int TestType;                     // 0 电压  OR  1  电压壳压   2 电压壳压 ACIR

        public static bool InitOK;                //参数化成功标识
        public static bool sysOK;                 //初始化成功标识

        public static int BattInfoReqFlag;        //读取电池信息状态标志

        public static string ManualMessInfo = "";

        //上位机

        public static eRunMode OCV_RunMode;              //设备运行模式 
        public static int ResRangeMode;
        public static string OCV_TestErrDetail;          //OCV测试的错误详情     20190109

        public static string xmlPath = Application.StartupPath + "\\Setting\\Parameter.xml";

        public static string mSwitchPath = Application.StartupPath + "\\Setting\\SwitchCH.ini";         //路径:电池通道换映射文件(调试)

        public static string mSettingPath = Application.StartupPath + "\\Setting\\Setting.ini";         //设置文件
        public static string mTempAdjustPath = Application.StartupPath + "\\Setting\\TempAdjust.ini";   //温度设置文件
        public static string mSwitchTempPath = Application.StartupPath + "\\Setting\\SwitchTempCH.ini"; //路径:温度通道换映射文件(调试)

        public static string mIRAdjustPath = Application.StartupPath + "\\Setting\\ACIRAdjust.ini";     //内阻校准文件

        public static string mSettingProjectPath = Application.StartupPath + "\\Processset";            //工程文件
        public static string mSettingBatModelPath = Application.StartupPath + "\\Setting\\BatModel.ini";            //电池型号
        public static string mDevInfoPath = Application.StartupPath + "\\" + "DevInfo.db";
        public static string mVoltAdjustPath = Application.StartupPath + "\\Setting\\VoltAdjust.ini";     //电压校准文件

        //条码长度
        public static int TrayCodeLengh = 20;   //托盘条码长度
                                                //  public static int CellCodeLengh = 16;   //
        //电池条码长度工程设定
        public static List<struBatSet> lstBatSet = new List<struBatSet>();
        /// <summary>
        /// 托盘号合法性检查（正则）  电芯条码合法性检查（正则）
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        public static bool CodeRegexCheck(string Code, string RegEx)
        {
            bool flag = false;
            if (Code == ""|| Code ==null)
            {
                return flag;
            }
            Regex r = new Regex(RegEx);

            flag = r.IsMatch(Code);

            return flag;
        }
        /// <summary>
        /// 进程是否存在
        /// </summary>
        /// <returns>判断程序是否存在</returns>
        public static bool CheckProcessOn(string ProgrammName)
        {
            bool bExist = false;
            Process[] processes = Process.GetProcessesByName(ProgrammName);
            if (processes.Length > 1)
            {
                bExist = true;
            }
            return bExist;
        }

        /// <summary>
        /// 二进制字符串转成十进制数
        /// </summary>
        /// <param name="StrNum">1:数值 
        public static int CheckInt10(string StrNum)
        {
            try
            {
                int mum = System.Convert.ToInt32(StrNum, 2);

                return mum;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 十进制转成16位二进制数组
        /// </summary>
        /// <param name="Num">1:数值 
        public static string[] CheckInt2(int Num)
        {
            try
            {
                string strmum = System.Convert.ToString(Num, 2).PadLeft(16, '0');
                int j = 0;
                string[] Data = new string[16];
                foreach (char c in strmum)
                {
                    int mum = c;
                    Data[j] = c.ToString();
                    j++;
                }
                return Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region OCV运行控制部
        public static string PLCAddr = "10.4.14.220";
        public static int PLCPort =3000;
        public static ClsPLCContr mPLCContr;               //PLC控制

        //皮带暂停前速度状态
        public static double CurrentSpeed;

        //烟雾报警cancel
        public static bool CancelSmokeDetect = false;

        public static int SetPos1;
        public static int SetPos2;
        public static int SetPos3;
        public static int SetPos4;
        public static int SetPos5;
        public static int SetPos6;
        public static int SetPos7;
        public static int SetPos8;
        public static int SetPos9;

        public static int MoveSpeed = 8000;
        public static int AccTime = 500;

        public ClsGlobal()
        {
            SetPos1 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos1Value"), 24100);
            SetPos2 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos2Value"), 16000);
            SetPos3 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos3Value"), 8000);
            SetPos4 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos4Value"), 80);
            SetPos5 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos5Value"), 80);
            SetPos6 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos6Value"), 80);
            SetPos7 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos7Value"), 80);
            SetPos8 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos8Value"), 80);
            SetPos9 = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "Pos9Value"), 80);

            MoveSpeed = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "MoveSpeed"), 8000);
            AccTime = StringToInt(Helper.SelectXmlNode(xmlPath, "root", "AccTime"), 500);
        }
        /// <summary>
        /// 字符串转为int
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="delValue"></param>
        /// <returns></returns>
        private static int StringToInt(string strValue, int defaultValue)
        {
            int result = defaultValue;
            result = int.TryParse(strValue, out result) ? result : result;
            return result;


        }
        #endregion

        #region OCV测试部


        //使能测试，debug
        public static int EN_TestOCV=1;
        public static int EN_TestACIR;
        public static int EN_TestTemp;

        public static int SleepTime = 30;               //切换通道时间   单位毫秒


        //电池信息获取成功
        //public static bool BatInfoOk;

        public static bool IsAWorking;

        public static int SampleRateType = 2;            //内阻仪采样速率: SLOW->1 MED->2, FAST->3</param>
        public static int SWDelayTime = 40;               //切换通道时间   单位毫秒

        //数据库
        //MES地址
        public static string MESURL_bindtray;
        public static string MESURL_OCV;
        public static string MES_SITE;

        //擎天服务器
        public static string Server_QT_IP;
        public static string Server_QT_id;
        public static string Server_QT_Pwd;
        public static string Server_QT_DB;
        //public static string mTableName="BatInfo";


        //本地数据库
        public static string Server_Local_IP;
        public static string Server_Local_id;
        public static string Server_Local_Pwd;
        public static string Server_Local_DB;
        //public static string mOCVTable = "OCVInfo";

        //OCV数据库
        public static string Server_OCV_IP;
        public static string Server_OCV_id;
        public static string Server_OCV_Pwd;
        public static string Server_OCV_DB;

        public static int DebugLog = 1 ;
        public static int LocalDays;



        //电池通道映射表
        public static string[] mSwitchCH;                     //用于电池号和实际切换通道值进行对应.

        //内阻校准
        public static bool IRTrueValSetFlag;                     //是否已设置校准真实值
        public static double[] ArrIRTrueVal = new double[88];    //真实内阻数组
        //public static string[] ArrIRAdjustPara;                  //内阻校准值
        public static string[] mIRAdjustVal;                     //校准值数组采用实际切换通道号进行保存
        public static double[] ArrAdjustACIR = new double[160];   //内阻校准数组
        public static int AdjustCount;                           //校准计数
        public static int AdjustNum = 3;                         //校准测量次数


        public static string[] ArrVoltAdjustPara;                  //电压校准值
        public static double[] ArrAdjustVolt = new double[88];   //电压校准数组

        //电池通道温度校准值

        //电池温度通道映射表
        public static string[] mSwitchCHTemp_P;
        public static string[] mSwitchCHTemp_N;

        public static string[] mTempAdjustVal_P;    //正极校准值
        public static string[] mTempAdjustVal_N;    //负极校准值



        //public static int[] negVolt;
        //public static int[] negC_Volt;
        //public static double[] G_dbl_VoltArr = new double[88];      //电压数组
        //public static double[] G_dbl_ACIRArr = new double[88];      //内阻数组

        //public static double[] G_dbl_VshellArr = new double[88];    //壳电压数组
        //public static string[] G_dbl_TimeArr = new string[88];      //时间数组
        public static double[] G_dbl_P_TempArr = new double[88];    //正极温度
        public static double[] G_dbl_N_TempArr = new double[88];    //负极温度

        public static bool ShellVolIsErr;
        public static int[] arrShellVolErrChannel;

        public static DateTime TestStartTime;       //起始测试时间
        public static DateTime TestEndTime;         //结束测试时间

        //电池测试数据
        public static List<ET_CELL> listETCELL;
        //public static List<ET_CELL_VS> listETCELL_VS;
     

        //万用表通讯
        public static int DMTComMode;
        public static string DMT_USBAddr;
        public static string DMT_Port;
        public static SerialPort DMTCom;

        //内阻仪通讯
        public static SerialPort RTCom;
        public static string RT_Port;

        public static string []RT4560_Port;
        public static string []TestFreq;
        public static ClsHIOKI4560.RANG [] BT4560RANG;
        public static ClsHIOKI4560.InitBT4560[]InitBT4560;
        public static string[] ChNo;
        public static int BTcount;

        public static string Scan_Port;
        

        //切换箱控制
        public static string  Switch_Port;       //Port口
        public static int SwitchVersion;    //切换箱版本  1宝龙版本  2 C42最新版
        public static string SwitchVersionStr;    //切换箱版本  1宝龙版本  2 C42最新版
        public static int Switch_Count;       //切换系统数量
        public static string SwitchChNo;

        public static int ACIRAddr = 0;        //ACIR切换基板有关
        public static int OCVAddr = 0;         //OCV切换基板有关
        public static SerialPort SwitchCom;

        //测温板
        public static string Temp_Port;
        public static SerialPort TempCom;

        public static int EnDelayTEMPTest;      //测温延迟使能标识
        public static int DelayTEMPTime;        //测温延迟时间
        public static int EnCH = 1;            //使用的通道

        //位移传感器
        public static string Loca_Port;
        public static SerialPort LocaCom;

        //工艺版本
        public static List<string> MESConfig = new List<string>();
        public static int MESConfigIndex;

        //复测判断条件
        public static double ReTestLmt_ACIR;
        //最大测试次数
        public static int MaxTestNum;
        //通道测试异常计数复测设置
        public static double SetEnOCV = 0;

        public static string Config;
        public static string BattTyp;

        //电池上下限
        //容量NG设定
        public static double UpLmt_CAP;
		public static double DownLmt_CAP;
		//电压NG设定
		public static double UpLmt_V;
        public static double DownLmt_V;

        //壳压NG设定
        public static double UpLmt_SV;
        public static double DownLmt_SV;

        //休眠电压NG设定
        public static double UpSleep_V;
		public static double DownSleep_V;

		//内阻NG设定
		public static double UpLmt_ACIR;
        public static double DownLmt_ACIR;
        //内阻极差NG设定
        public static double DownLMT_ACIRRange;
        public static double UpLMT_ACIRRange;
        //压降NG设定
        public static double MaxVoltDrop;
        public static double VoltDrop;
        public static double MinVoltDrop;
       
        //压降极差NG设定
        public static double DownLMT_DropRange;
        public static double UpLMT_DropRange;

        //K值
        public static double K_VALUE;
        public static double UpLmt_time;
        public static double DownLmt_time;
      

        //public static bool SYbat=false;   //true实验电池

        //电压预警值（大于）
        //public static double VOLTAGE_ALARM_VALUE;

       // 是否测试内阻（Y或N）
        public static string IS_TEST_RESISTANCE;

        //是否获取容量数据（Y或N）
        public static string IS_GET_CAPACITY;

        //隔离条件
        public static string ISOLATION_CONDITION;
        //是否验证容量（Y或N）
        public static string IS_VERIFY_CAPACITY;

        //是否时间限制(Y或N)
        public static string IS_DATE_LIMIT;

        public static double GetHours;   //实际实际间隔


        //是否验证休眠电压(Y或N)
        public static string IS_VERIFY_SleepVoltage;

        //是否启用ACIR极差功能(Y或N)
        public static string IS_Enable_ACIRRange;

        //是否启用压降极差功能(Y或N)
        public static string IS_Enable_DropRange;
        //是否启用压降功能(Y或N)
        public static string ENVoltDrop;
        //温度修正值
        public static double TempBase;
        public static double TempParaModify;
        public static double AdjustTempBase;

        //ACIR中值或最小值(Y或N)
        public static string ACIR_MinOrMedian;
       
        //Drop中值或最小值(Y或N)
        public static string Drop_MinOrMedian;

        ////右位移
        //public static double L_Dispiacement;
        ////右位移
        //public static double R_Dispiacement;
        //工程设定
        //public static List<struProjSet> lstProjSet = new List<struProjSet>();

        //public static DataRow struProSet;

        ////电池型号设置
        //public static List<struBatModelset> lstBatModelset = new List<struBatModelset>();
        //public static string ModelName;
        ////位移传感器安装位置
        //public static int Position;

        public static string MODEL_NO;   //电池型号
        public static string BATCH_NO;   //批次号
        public static string PROJECT_NO; //项目标号

        ////写OCV LOG日志
        //public static void WriteLogOCV(string sInfo)
        //{
        //    //日志
        //    using (StreamWriter ew = new StreamWriter(sLogsOCVpath, true))
        //    {
        //        ew.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + sInfo);
        //        ew.Close();
        //        //ew = null;
        //    }
        //}

        //写OCV LOG日志
        //public static void WriteMESOCV(string sInfo)
        //{
        //    //日志
        //    using (StreamWriter ew = new StreamWriter(sLogsMESpath, true))
        //    {
        //        ew.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + sInfo);
        //        ew.Close();
        //        //ew = null;
        //    }
        //}


        //获得内阻校准值
        public static string[] GetIRAdjust(string Path)
        {
            int CountNum;
            string[] ArrAdjust;
            string count = INIAPI.INIGetStringValue(Path, "ACIRAdjust", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                ArrAdjust = new string[CountNum];

                for (int i = 0; i < ArrAdjust.Count(); i++)
                {
                    ArrAdjust[i] = INIAPI.INIGetStringValue(Path, "ACIRAdjust", "CH" + (i + 1).ToString(), null);
                }
                return ArrAdjust;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return null;
            }

        }
        //获得电池温度值校准值
        public static string[] GetAdjustVal_Temp(string Path ,string pos)
        {
            int CountNum;
            string[] ArrTempAdjust;
            string count = INIAPI.INIGetStringValue(Path, "TempAdjust_"+pos, "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                ArrTempAdjust = new string[CountNum];

                for (int i = 0; i < ArrTempAdjust.Count(); i++)
                {
                    ArrTempAdjust[i] = INIAPI.INIGetStringValue(Path, "TempAdjust_" + pos, "CH" + (i + 1).ToString(), null);
                }
                return ArrTempAdjust;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return null;
            }

        }

        //获得温度通道切换映射表
        public static string[] GetSwitchChannel_Temp(string SwitchPath, string pos)
        {
            int CountNum;
            string[] SWChannel;
            string count = INIAPI.INIGetStringValue(SwitchPath, "switch_"+ pos, "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                SWChannel = new string[CountNum];

                for (int i = 0; i < SWChannel.Count(); i++)
                {
                    SWChannel[i] = INIAPI.INIGetStringValue(SwitchPath, "switch_" + pos, "CH" + (i + 1).ToString(), null);
                }
                return SWChannel;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", SwitchPath);
                return null;
            }
        }



        //获得电池通道切换映射表
        public static string[] GetSwitchChannel(string SwitchPath)
        {
            int CountNum;
            string[] SWChannel;
            string count = INIAPI.INIGetStringValue(SwitchPath, "switch", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                SWChannel = new string[CountNum];

                for (int i = 0; i < SWChannel.Count(); i++)
                {
                    SWChannel[i] = INIAPI.INIGetStringValue(SwitchPath, "switch", "CH" + (i + 1).ToString(), null);
                }
                return SWChannel;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", SwitchPath);
                return null;
            }
        }

        //获得电池内阻校准值
        public static string[] GetAdjustVal_ACIR(string Path)
        {
            int CountNum;
            string[] ArrIRAdjust;
            string count = INIAPI.INIGetStringValue(Path, "ACIRAdjust", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                ArrIRAdjust = new string[CountNum];

                for (int i = 0; i < ArrIRAdjust.Count(); i++)
                {
                    ArrIRAdjust[i] = INIAPI.INIGetStringValue(Path, "ACIRAdjust", "CH" + (i + 1).ToString(), null);
                }
                return ArrIRAdjust;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return null;
            }

        }
        #endregion

    }
    public enum eTestState
    {

        TestOK = 4,             //测试成功
        TestAgain = 5,          //再测试
        TestEnd = 10,           //测试结束
        AdjustOK = 6,           //校准测试完成
        AdjustAgain = 7,        //再次校准测试
        OfflineTestEnd = 11,          //单机测试完成
        StopTest = 12,                //停止测试
        ErrTempTest = 20,            //温度测试异常
        ErrOCVTest = 21,             //测试异常
        ErrOCVDataGetFail = 22,      //获取电池数据失败
        ErrDataSaveMESFail = 23,      //保存到MES失败
        ErrDataSaveLocSqlFail = 25,      //保存到本地数据库失败
        ErrDataSaveQTFail = 24,      //保存到擎天数据库失败
        ErrAdjustTestNoSet = 27,      //没有设置校准参数
        ErrAdjustFail = 28,          //内阻校准出错
        ErrCellStyleCode = 29,
        ErrOCVXDataGetFail = 30,      //获取OCV数据数据失败
        ErrGetProject = 31,            //获取工艺数据数据失败
        ErrNoSetBatModel = 32,         //未设置电池型号
        ErrMecAction = 21,             //机械动作异常
    }
   
    //运行模式
    public enum eRunMode
    {
        NormalTest = 1,     //正常OCV测试
        GoAhead = 2,        //不做测试,直接排出
        OfflineTest = 3,    //单机测试
        ACIRAdjust = 4,     //ACIR校准
    }

    public struct struBatSet
    {
        //工程设置名称
        public int P_CellCodeLength;
        public int P_KeyStart;
        public int P_ModelLenth;
    }

    // OCV使用管理
    public class OCVToken
    {
        private static int mState;         //1: 允许使用  0: 被使用中
        private static string mClient;     //使用者: 针床A , 针床B
        private static string mNote;       //备注
        private static Mutex MT1 = new Mutex();

        public static void Init(string Note)
        {
            MT1.WaitOne();
            mState = 1;
            mClient = "";
            mNote = Note;

            MT1.ReleaseMutex();
        }

        /// <summary>
        /// 获取OCV使用权限
        /// </summary>
        /// <param name="Client">申请者</param>
        /// <returns>成功->true, 失败->false</returns>
        public static bool GetToken(string Client)
        {
            MT1.WaitOne();
            if (mState == 1)
            {
                mState = 0;
                mClient = Client;
                MT1.ReleaseMutex();
                return true;
            }
            else
            {
                MT1.ReleaseMutex();
                return false;
            }

        }

        /// <summary>
        /// 释放OCV使用权限
        /// </summary>
        /// <param name="Client">申请者</param>
        /// <returns>成功->true, 失败->false</returns>
        public static bool Release(string Client)
        {
            MT1.WaitOne();
            if (Client == mClient)
            {
                mState = 1;
                mClient = "";
                MT1.ReleaseMutex();
                return true;
            }
            else
            {
                MT1.ReleaseMutex();
                return false;
            }
        }

        /// <summary>
        /// OCV使用状态
        /// </summary>
        public static bool IsAvailable()
        {
            if (mState == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //说明
        public static string Note
        {
            get { return mNote; }
        }

        //使用者
        public static string Client
        {
            get { return mClient; }
        }


    }

    public class INIAPI
    {
        #region INI文件操作

        /* 
        * 针对INI文件的API操作方法，其中的节点（Section)、键（KEY）都不区分大小写 
        * 如果指定的INI文件不存在，会自动创建该文件。 
        *  
        * CharSet定义的时候使用了什么类型，在使用相关方法时必须要使用相应的类型 
        *      例如 GetPrivateProfileSectionNames声明为CharSet.Auto,那么就应该使用 Marshal.PtrToStringAuto来读取相关内容 
        *      如果使用的是CharSet.Ansi，就应该使用Marshal.PtrToStringAnsi来读取内容 
        *       
        */

        #region API声明

        /// <summary>  
        /// 获取所有节点名称(Section)  
        /// </summary>  
        /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>  
        /// 获取某个指定节点(Section)中所有KEY和Value  
        /// </summary>  
        /// <param name="lpAppName">节点名称</param>  
        /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>  
        /// 读取INI文件中指定的Key的值  
        /// </summary>  
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>  
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>  
        /// <param name="lpDefault">读取失败时的默认值</param>  
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>  
        /// <param name="nSize">内容缓冲区的长度</param>  
        /// <param name="lpFileName">INI文件名</param>  
        /// <returns>实际读取到的长度</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

        //另一种声明方式,使用 StringBuilder 作为缓冲区类型的缺点是不能接受\0字符，会将\0及其后的字符截断,  
        //所以对于lpAppName或lpKeyName为null的情况就不适用  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        //再一种声明，使用string作为缓冲区的类型同char[]  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>  
        /// 将指定的键值对写到指定的节点，如果已经存在则替换。  
        /// </summary>  
        /// <param name="lpAppName">节点，如果不存在此节点，则创建此节点</param>  
        /// <param name="lpString">Item键值对，多个用\0分隔,形如key1=value1\0key2=value2  
        /// <para>如果为string.Empty，则删除指定节点下的所有内容，保留节点</para>  
        /// <para>如果为null，则删除指定节点下的所有内容，并且删除该节点</para>  
        /// </param>  
        /// <param name="lpFileName">INI文件</param>  
        /// <returns>是否成功写入</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]     //可以没有此行  
        private static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        /// <summary>  
        /// 将指定的键和值写到指定的节点，如果已经存在则替换  
        /// </summary>  
        /// <param name="lpAppName">节点名称</param>  
        /// <param name="lpKeyName">键名称。如果为null，则删除指定的节点及其所有的项目</param>  
        /// <param name="lpString">值内容。如果为null，则删除指定节点中指定的键。</param>  
        /// <param name="lpFileName">INI文件</param>  
        /// <returns>操作是否成功</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region 封装

        //20180918
        //替换键的名字,保留原键的值不变
        //如果找不到旧键值或者旧键没有值,则不能替换
        public static bool INIModifyKeyName(string iniFile, string section, string OldKey, string NewKey)
        {
            string keyVal = null;
            const int SIZE = 1024 * 10;

            //获取key和值

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(OldKey))
            {
                throw new ArgumentException("必须指定键名称(OldKey)", "OldKey");
            }

            if (string.IsNullOrEmpty(NewKey))
            {
                throw new ArgumentException("必须指定键名称(NewKey)", "NewKey");
            }


            StringBuilder sb = new StringBuilder(SIZE);
            uint bytesReturned = INIAPI.GetPrivateProfileString(section, OldKey, null, sb, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                keyVal = sb.ToString();
            }
            sb = null;

            //添加新键和值 ,删除原来的键和值
            if (keyVal != null)
            {
                INIAPI.WritePrivateProfileString(section, NewKey, keyVal, iniFile);
                INIAPI.WritePrivateProfileString(section, OldKey, null, iniFile);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>  
        /// 读取INI文件中指定INI文件中的所有节点名称(Section)  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <returns>所有节点,没有内容返回string[0]</returns>  
        public static string[] INIGetAllSectionNames(string iniFile)
        {
            uint MAX_BUFFER = 32767;    //默认为32767  

            string[] sections = new string[0];      //返回值  

            //申请内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = INIAPI.GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFile);
            if (bytesReturned != 0)
            {
                //读取指定内存的内容  
                string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();

                //每个节点之间用\0分隔,末尾有一个\0  
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            //释放内存  
            Marshal.FreeCoTaskMem(pReturnedString);

            return sections;
        }

        /// <summary>  
        /// 获取INI文件中指定节点(Section)中的所有条目(key=value形式)  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <returns>指定节点中的所有项目,没有内容返回string[0]</returns>  
        public static string[] INIGetAllItems(string iniFile, string section)
        {
            //返回值形式为 key=value,例如 Color=Red  
            uint MAX_BUFFER = 32767;    //默认为32767  

            string[] items = new string[0];      //返回值  

            //分配内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint bytesReturned = INIAPI.GetPrivateProfileSection(section, pReturnedString, MAX_BUFFER, iniFile);

            if (!(bytesReturned == MAX_BUFFER - 2) || !(bytesReturned == 0))
            {

                string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned);
                items = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(pReturnedString);     //释放内存  

            return items;
        }

        /// <summary>  
        /// 获取INI文件中指定节点(Section)中的所有条目的Key列表  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <returns>如果没有内容,反回string[0]</returns>  
        public static string[] INIGetAllItemKeys(string iniFile, string section)
        {
            string[] value = new string[0];
            const int SIZE = 1024 * 10;

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            char[] chars = new char[SIZE];
            uint bytesReturned = INIAPI.GetPrivateProfileString(section, null, null, chars, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                value = new string(chars).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            chars = null;

            return value;
        }

        /// <summary>  
        /// 读取INI文件中指定KEY的字符串型值  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <param name="key">键名称</param>  
        /// <param name="defaultValue">如果没此KEY所使用的默认值</param>  
        /// <returns>读取到的值</returns>  
        public static string INIGetStringValue(string iniFile, string section, string key, string defaultValue)
        {
            string value = defaultValue;
            const int SIZE = 1024 * 10;

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称(key)", "key");
            }

            StringBuilder sb = new StringBuilder(SIZE);
            uint bytesReturned = INIAPI.GetPrivateProfileString(section, key, defaultValue, sb, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                value = sb.ToString();
            }
            sb = null;

            return value;
        }

        /// <summary>  
        /// 在INI文件中，将指定的键值对写到指定的节点，如果已经存在则替换  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点，如果不存在此节点，则创建此节点</param>  
        /// <param name="items">键值对，多个用\0分隔,形如key1=value1\0key2=value2</param>  
        /// <returns></returns>  
        public static bool INIWriteItems(string iniFile, string section, string items)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(items))
            {
                throw new ArgumentException("必须指定键值对", "items");
            }

            return INIAPI.WritePrivateProfileSection(section, items, iniFile);
        }

        /// <summary>  
        /// 在INI文件中，指定节点写入指定的键及值。如果已经存在，则替换。如果没有则创建。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <param name="key">键</param>  
        /// <param name="value">值</param>  
        /// <returns>操作是否成功</returns>  
        public static bool INIWriteValue(string iniFile, string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称", "key");
            }

            if (value == null)
            {
                throw new ArgumentException("值不能为null", "value");
            }

            return INIAPI.WritePrivateProfileString(section, key, value, iniFile);

        }

        /// <summary>  
        /// 在INI文件中，删除指定节点中的指定的键。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <param name="key">键</param>  
        /// <returns>操作是否成功</returns>  
        public static bool INIDeleteKey(string iniFile, string section, string key)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称", "key");
            }

            return INIAPI.WritePrivateProfileString(section, key, null, iniFile);
        }

        /// <summary>  
        /// 在INI文件中，删除指定的节点。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <returns>操作是否成功</returns>  
        public static bool INIDeleteSection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            return INIAPI.WritePrivateProfileString(section, null, null, iniFile);
        }

        /// <summary>  
        /// 在INI文件中，删除指定节点中的所有内容。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <returns>操作是否成功</returns>  
        public static bool INIEmptySection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            return INIAPI.WritePrivateProfileSection(section, string.Empty, iniFile);
        }

        #endregion

        #endregion
    }

    public class ClsAbout

    {
        #region 程序集特性访问器

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return String.Format("关于 {0}", (System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase)));

            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return String.Format("版本: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }

                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return String.Format("产品名称: {0}", ((AssemblyProductAttribute)attributes[0]).Product);
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
   

}
