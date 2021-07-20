using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO.Ports;

namespace OCV
{
    public class ClsGlobal
    {
        #region 公共部

        /// <summary>
        /// 是否FCT电池
        /// </summary>
        public static bool ISFCT { get; set; } = false;

        public static UserInfo UserInfo { get; set; }
        //设备名称
        public static string DeviceName;
        //设备序号
        public static string DeviceNo;
        //OCV类型号 OCV1,2
        static int _OCVType = -1;
        /// <summary>
        /// 重启程序标志
        /// </summary>
        public static bool Restart{get;set;}
        public static int OCVType
        {
            get
            {
                if (_OCVType == -1)
                {
                    int value = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "OCVType", null));
                    _OCVType = value;

                }
                return _OCVType;
            }
            set
            {
                _OCVType = value;
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "OCVType", value.ToString());
            }
        }
        //OCV类型子标记
        public static int OCVType_Sub1;

        public static bool ID_InitOK;                       //初始化成功标识

        public static string Passward;                      //密码

        public static bool En_Debug_Offline = false;        //脱机调试标志

        public static bool En_Debug_TrayInOut = false;      //手动输送对接调试

        public static bool IsTestRuning { get; set; }

        public static bool IsStartTest { get; set; }

        //PASSWARD
        public static string UserPwd;
        public static string AdvUserPwd;

        //结束标识
        public static bool bCloseFrm = false;

        //流程控制
        public static bool InitOK;                  //初始化成功标识   
        public static string TrayCode = "0604-3";  //当前托盘条码
        public static string LastTrayCode;          //上一次托盘条码
        public static bool isTestAgainState;        //是否复测状态的标识
        public static int Trans_RequestTest { get; set; } = 0;        //申请进行测试  
        public static eTransState Trans_State { get; set; }     //定位设备状态  -1->停止, 0->初始状态，1->就绪, 2->进托盘，3->测试, 4->出托盘 9->报警状态
        public static eTrayLoc Trans_TrayLoc;       //托盘定位状态  0->没有定位， 1->到位， 2->压合

        //OCVIR测试控制
        public static eTestState OCV_TestState { get; set; }     //OCV设备状态
        public static eRunMode OCV_RunMode;         //设备运行模式 
        public static string TestOCVMsg;            //测试信息传递

        public static string OCV_BatteryType = "N/A";

        //测试错误信息
        public static string ErrMsg;

        //文件路径
        public static string sLogsOCVpath = "";             //OCV测试log
        public static string sLogspath
        {
            get
            {
                string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
                return Application.StartupPath.ToString() + "\\log\\Logs_" + TodayDate + ".txt";
            }
        }
        //public static string sLogAlarmpath = "";
        public static string sLogAlarmpath
        {
            get
            {
                string TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
                return Application.StartupPath.ToString() + "\\log\\AlarmLogs_" + TodayDate + ".txt";
            }
        }

        public static string sDebugInfoPath
        {
            get
            {
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                return Application.StartupPath.ToString() + "\\log\\Debugs_Logs_" + todayDate + ".txt";
            }
        }

        public static string sDebugOCVSelectionPath
        {
            get
            {
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                return Application.StartupPath.ToString() + "\\log\\DebugOCVSelection_Logs_" + todayDate + ".txt";
            }

        }



        public static string sLogEventPath = "";
        public static string mSwitchPath = Application.StartupPath + "\\Setting\\SwitchCH.ini";  //路径:电池通道换映射文件(调试)
        public static string mSwitchTempPath = Application.StartupPath + "\\Setting\\SwitchCHTemp.ini";  //温度通道映射文件
        public static string mSettingPath = Application.StartupPath + "\\Setting\\Setting.ini";  //设置文件
        public static string mTempAdjustPath = Application.StartupPath + "\\Setting\\TempAdjust.ini";  //温度设置文件
        public static string mProjSettingPath = Application.StartupPath + "\\Setting\\ProjectSetting.ini";  //工程设置文件
        public static string mIRAdjustPath = Application.StartupPath + "\\Setting\\ACIRAdjust.ini";     //内阻校准文件
        public static string mDataUploadFilePath;

        //条码
        public static bool Flag_TrayCodeIsInserted = false;

        public static int TrayCodeLengh = 20;   //托盘条码长度
        public static int CellCodeLengh = 16;   //电池条码长度

        //托盘类型
        public static int TrayType { get; set; }             //托盘类型   
        //public static int mShellTestType;    //壳体测量类型

        //电池类型
        public static string BatteryType;      // 18650   21700

        //刷新IO调试标志
        public static bool EN_DebugFresh = false;
        public static bool EN_DebugAdvFresh = false;

        //针床类型
        public static int ProbeBoardType;

        //关闭程序 
        public static bool CloseProg;

        //输送部写日志
        public static void WriteLog(string sInfo)
        {
            //日志
            using (StreamWriter sw = new StreamWriter(sLogspath, true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + ": " + sInfo);
                sw.Close();
                //sw = null;
            }
        }

        //OCV测试部日志
        public static void WriteLogOCV(string sInfo)
        {
            //日志
            using (StreamWriter sw = new StreamWriter(sLogspath, true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + ": " + sInfo);
                sw.Close();
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="sInfo"></param>
        public static void WriteLog(string sInfo, string Logspath)
        {
            //日志
            using (StreamWriter sw = new StreamWriter(Logspath, true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + ": " + sInfo);
                sw.Close();
            }
        }

        /// <summary>
        /// 判断日志文件是否存在，不存在则创建
        /// </summary>
        public static void CreateLogsFile(string filePath)
        {
            string path = Path.GetDirectoryName(filePath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs1);
                sw.Close();
                fs1.Close();
                sw = null;
                fs1 = null;
            }

        }

        public static void CreateDebugFile(string filePath)
        {
            string path = Path.GetDirectoryName(filePath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs1);
                sw.Close();
                fs1.Close();
                fs1 = null;
                sw = null;
            }

        }

        /// <summary>
        /// 字符串转化为十六进制
        /// </summary>
        /// <param name="SendStr"></param>
        /// <returns></returns>
        public static byte[] StringToHex(string SendStr)
        {
            byte[] tempb = new byte[50];
            int j = 0;
            for (int i = 0; i < SendStr.Length; i = i + 2, j++)
                tempb[j] = Convert.ToByte(SendStr.Substring(i, 2), 16);
            byte[] send = new byte[j];
            Array.Copy(tempb, send, j);
            return send;
        }

        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 16进制转换成ascii 开始和结束标志
        /// </summary>
        /// <param name="s16"> "0x02" "0x03"</param>
        /// <returns></returns>
        public string From16ToAscii(string sx16)
        {
            int _iValue;
            byte[] _b_byte;
            string _sValue;
            // 16进制->10进制 
            _iValue = Convert.ToInt32(sx16, 16);
            //int->byte[] 
            _b_byte = System.BitConverter.GetBytes(_iValue);
            //byte[]-> ASCII
            _sValue = System.Text.Encoding.ASCII.GetString(_b_byte);
            _b_byte = null;
            return _sValue;
        }

        public static bool CheckOCVProcessOn()//进程是否存在
        {
            bool bExist = false;
            Process[] processes = Process.GetProcessesByName("OCV");
            if (processes.Length > 1)
            {
                bExist = true;
            }
            return bExist;
        }

        public static bool Ping(string ip)
        {
            bool _bping = false;
            try
            {
                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
                options.DontFragment = true;
                string data = "Test Data!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000; // Timeout 时间，单位：毫秒  
                System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    _bping = true;
                else
                    _bping = false;
            }
            catch (Exception ex)
            {
                ClsGlobal.WriteLog(ex.Message);
            }
            finally
            {

            }
            return _bping;
        }


        #endregion

        #region 测试部分

        //使能测试，debug
        public static int EN_TestOCV = 1;
        public static int EN_TestACIR = 1;
        public static int EN_TestTemp = 1;

        public static int DelayTime = 30;                   //通道切换延时测量  单位毫秒

        public static ClsIOControl mIOControl { get; set; }            //IO控制

        //万用表通讯
        public static string DMT_USBAddr;
        SerialPort DMTCom = new SerialPort();               //万用表通讯(已经很少再用串口)

        //内阻仪通讯
        public static string RT_Port;
        public static SerialPort RTCom = new SerialPort();    //内阻仪通讯
        public static int RT_Speed;                           //1: 慢速 ,  2:中速

        //切换系统通讯
        public static string SW_Port;
        public static SerialPort SWCom { get; set; } = new SerialPort();    //切换系统通讯

        //需要保存到数据库才能继续
        public static int NeedSaveServer;

        //测温板
        public static string Temp_Port;
        public static int NoTestTemp;

        public static int EnDelayTEMPTest;      //延迟使能标识
        public static int DelayTEMPTime;        //延迟时间




        public static ClsOCVIRTest OCVIRTest { get; set; }


        public static bool ServerSaveFinish = false;                //保存到服务器标识
        //数据库的电池数据列表
        public static List<ET_CELL> listETCELL { get; set; }

        //public static DB_KT.Model.FlowValue mFlowValue;           //流程数据
        public static int BatInfoCount { get; set; }                          //电池数据数量

        //OCV1和OCV2的FlowType号
        public const int FlowType_OCV1 = 3;
        public const int FlowType_OCV2 = 6;

        //内阻校准
        public static bool IRTrueValSetFlag;                        //是否已设置校准真实值
        public static double[] ArrIRTrueVal = new double[256];      //真实内阻数组
        public static string[] ArrIRAdjustPara;                     //内阻校准值
        public static double[] ArrAdjustACIR = new double[256];     //内阻校准数组
        public static int AdjustCount;                              //校准计数
        public static int AdjustNum = 2;                            //校准测量次数

        //OCV测试
        public static int TestCount;                            //测试计数
        public static int MaxTestNum = 2;                       //测试最大次数 (正常只测一次,有问题需重测,直到最大测量次数)

        //电池通道映射表
        public static string[] mSwitchCH;                        //用于电池号和实际切换通道值进行对应.

        //电池通道温度校准值
        public static string[] mTempAdjustCH;

        public static List<int> lstACIRErrNo = new List<int>();   //内阻测量失败电池号,通道从1开始计算
        public static int[] ChannelNGRecord = new int[256];
        public static double[] G_dbl_VDataArr = new double[256];  //电压数组
        public static double[] G_dbl_ACIRArr = new double[256];  //内阻数组
        public static double[] G_dbl_VshellArr = new double[256];//壳电压数组
        public static string[] G_dbl_TimeArr = new string[256];  //时间数组
                                                                 //public static double[] G_dbl_TempArr = new double[256];


        //温度修正值
        //public static double TempBase;
        //public static double TempParaModify;

        public static int[] negVolt = new int[256];    //记录电压负值

        public static int[] G_il_NGArr = new int[256];                   //NG数组
        //public static int[] G_il_NGVshellArr = new int[256];             //NG数组带壳

        public static DateTime TestStartTime;       //起始测试时间
        public static DateTime TestEndTime;         //结束测试时间




        //工程设定------------------------------------------------------------------
        public static List<struProjSet> lstProjSet = new List<struProjSet>();
        public static int ProjSetIndex;

        #region  改用工程管理方式设定 20200827 由ajone屏蔽
        //电池上下限                    
        //public static double VolDownLmt { get; set; }= 0;
        //public static double VolUpLmt { get; set; } = 9999;

        //内阻上下限
        //public static double ACIRDownLmt { get; set; }
        //public static double ACIRUpLmt { get; set; }
        #endregion


        //20200702 zxz 通道映射表
        public static List<ChannelItem> ChannelMapping { get; set; } = new List<ChannelItem>();
        //工程设定------------------------------------------------------------------

        #endregion


        #region 全局唯一扫描枪交互对象 ajone 20200810
        /// <summary>
        /// 是否手动输入托盘码
        /// </summary>
        public static bool NontUsingScaner { get; set; }

        private static OCVTest.ClsCodeScaner _ClsCodeScan;

        public static OCVTest.ClsCodeScaner BuildClsCodeScan()
        {
            if (_ClsCodeScan == null)
            {
                try
                {
                    _ClsCodeScan = new OCVTest.ClsCodeScaner();
                    string scanPort = ClsSysSetting.SysSetting.ScanCodeCOM;
                    _ClsCodeScan.SetPortProperty(scanPort, 115200, Parity.None, 8, StopBits.One);
                    _ClsCodeScan.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception($"初始扫码枪失败,请检查扫码枪设置:{ex.Message}");
                }
            }
            return _ClsCodeScan;

        }
        #endregion


        #region 全局唯一温度获取组件 ajone 20200828
        private static ClsTempControl _ClsTempControl;
        public static ClsTempControl BuildClsTempControl()
        {

            if (_ClsTempControl == null)
            {
                try
                {
                    _ClsTempControl = new ClsTempControl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"初始化温度组件失败:{ex.Message}");
                }
            }
            return _ClsTempControl;


        }


        #endregion
    }

    //20200702 zxz 通道映射结构
    public class ChannelItem : IComparable
    {
        public int Channel;
        public int RealChannel;
        public int CompareTo(object other)
        {
            if (this.RealChannel > ((ChannelItem)other).RealChannel)
                return 1;
            else if (this.RealChannel < ((ChannelItem)other).RealChannel)
                return -1;
            else
                return 0;
        }
    }

    //定位机构状态   
    public enum eTransState
    {
        Stop = -1,          //停止
        Init = 0,           //初始状态
        Ready = 1,          //就绪状态
        TrayIn = 2,         //进托盘
        TestWork = 3,       //测试工作中,针床已压合
        TrayTestFinish = 4, //托盘测试结束,针床已打开
        ReTest = 5 ,         //复测状态
    }

    //OCV设备状态
    public enum eTestState
    {
        Idle = 0,               //空闲状态
        Standby = 1,            //就绪
        GetData = 2,            //获取数据
        Testing = 3,            //测试中
        TestOK = 4,             //测试成功
        TestAgain = 5,          //再测试
        TestEnd = 6,           //[联网模式]测试完成
        OfflineTestEnd = 7,    //[单机模式]测试完成
        AdjustEnd = 8,          //[校准模式]测试完成
        AdjustAgain = 9,        //再次校准测试
        //TestAllNG = 10,           //测试全盘NG

        ErrTempTest = 20,            //温度测试异常
        ErrOCVTest = 21,             //测试异常
        ErrAdjust = 25,              //校验错误
        ErrOCVDataGetFail = 22,      //获取电池数据失败
        //ErrDataSaveZDFail = 23,      //保存到中鼎数据库失败
        ErrDataSaveKTFail = 24,      //保存到擎天数据库失败


        ErrAdjustDataNoSet = 27,     //没有设置校准参数
        ErrOCVFlowErr = 28,             //OCV流程出错
        ErrNoTrayInfo = 30,            //没有相应的托盘数据
        ErrDataSaveLocal = 29,        // 保存本地出错

        ErrTransChange = 31,          //流程变化出错
        ErrOther = 32                 //其他错误
    }

    //托盘定位状态 
    public enum eTrayLoc
    {
        NotInPlace = 0,
        InPlace = 1,
        Pressing = 2,
    }

    public enum eRunMode
    {
        NormalTest = 1,
        OfflineTest = 2,
        ACIRAdjust = 3,     //ACIR校准
    }

    public struct struProjSet
    {
        //工程设置名称
        public string P_SettingName;

        public int P_CellCodeLength;

        //判断关键字,(第9个字母)
        //0808,zxz 字符序号可以编辑，可以为字符串
        public string P_KeyString;
        public int P_KeyStart;


        //电池上下限
        public double P_VolDownLmt;
        public double P_VolUpLmt;

        //温度设置
        public double P_TempBase;
        public double P_TempParaModify;

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

}
