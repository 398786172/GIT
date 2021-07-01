using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace OCV
{

    //万用表Ag344x
    public class ClsDMM_Ag344X
    {
        private SerialPort mSerialPort;
        private string mUSBDesc;

        public SerialPort SerialPort
        {
            get { return mSerialPort; }
        }
        public int ComMode;     //ComMode=1: 232串口  , ComMode=2: USB
        public int TrigMode;    //触发模式 =1:IMM  =2:EXT
        private uint mTrigNum = 1;
        public Ag344xIDinfo ag344XIDinfo;

        /// <summary>
        /// 万用表构造
        /// </summary>
        /// <param name="port">串口端口</param>
        public ClsDMM_Ag344X(string port, int comMode)
        {
            try
            {
                this.ComMode = comMode;
                if (ComMode == 1)
                {
                    mSerialPort = new SerialPort(ClsGlobal.DMT_Port, 9600, Parity.None, 8, StopBits.One);
                    mSerialPort.ReadTimeout = 500;           //设置超时读取时间 
                    mSerialPort.DtrEnable = true;
                    mSerialPort.Encoding = Encoding.UTF8;
                    if (mSerialPort.IsOpen)
                    {
                        mSerialPort.Close();
                    }
                    ag344XIDinfo = new Ag344xIDinfo();
                    this.InitControl_IMM();
                }
                else
                {
                    ag344XIDinfo = new Ag344xIDinfo();
                    mUSBDesc = port;
                    ComMode = 2;    //USB方式        
                }

            }
            catch (Exception ex)
            {
                throw new Exception("万用表串口初始化失败" + ex.Message);
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SP">串口</param>
        public ClsDMM_Ag344X(SerialPort SP)
        {
            ag344XIDinfo = new Ag344xIDinfo();
            mSerialPort = SP;

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="USBDesc">USB地址</param>
        public ClsDMM_Ag344X(string USBDesc)
        {
            ag344XIDinfo = new Ag344xIDinfo();
            mUSBDesc = USBDesc;
            ComMode = 2;    //USB方式        
        }

        /// <summary>
        /// 初始化万用表控制(内部触发方式)
        /// </summary>
        public void InitControl_IMM()
        {
            try
            {
                TrigMode = 1;
                if (ComMode == 1)
                {
                    if (mSerialPort.IsOpen == false)
                    {
                        mSerialPort.Open();
                    }

                    mSerialPort.WriteLine("SYST:REM");
                    mSerialPort.WriteLine("*CLS");
                    mSerialPort.WriteLine("*RST");
                    mSerialPort.WriteLine("CONF:VOLT:DC 10, MAX");  //1E-6  0.000001
                    mSerialPort.WriteLine("VOLT:DC:NPLC 10");  //1E-6  0.000001
                    mSerialPort.WriteLine("TRIG:SOUR IMM\n");


                }
                else if (ComMode == 2)
                {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier       
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);
                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc, AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);
                    viError = AgVisa32.viPrintf(session, "*CLS\n");
                    viError = AgVisa32.viPrintf(session, "*RST\n");
                    viError = AgVisa32.viPrintf(session, "CONF:VOLT:DC 10, MAX\n");
                    viError = AgVisa32.viPrintf(session, "VOLT:DC:NPLC 10\n");

                    viError = AgVisa32.viPrintf(session, "TRIG:SOUR IMM\n");

                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 初始化万用表控制(外部触发方式)
        /// </summary>
        /// <param name="TrigCount">触发数据次数</param>
        public void InitControl_EXT(uint TrigCount)
        {
            try
            {
                TrigMode = 2;
                mTrigNum = TrigCount;

                if (ComMode == 1)
                {
                    if (mSerialPort.IsOpen == false)
                    {
                        mSerialPort.Open();
                    }

                    mSerialPort.WriteLine("SYST:REM");
                    mSerialPort.WriteLine("*CLS");
                    mSerialPort.WriteLine("*RST");
                    mSerialPort.WriteLine("CONF:VOLT:DC 10, MAX");  //1E-6  0.000001
                    mSerialPort.WriteLine("VOLT:DC:NPLC 10");  //1E-6  0.000001

                    mSerialPort.WriteLine("TRIG:SOUR EXT\n");
                    mSerialPort.WriteLine("SAMP:COUN " + mTrigNum + "\n");
                    mSerialPort.WriteLine("INIT\n");

                }
                else if (ComMode == 2)
                {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier       
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);
                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                        AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);
                    viError = AgVisa32.viPrintf(session, "*CLS\n");
                    viError = AgVisa32.viPrintf(session, "*RST\n");
                    viError = AgVisa32.viPrintf(session, "CONF:VOLT:DC 10, MAX\n");
                    viError = AgVisa32.viPrintf(session, "VOLT:DC:NPLC 10\n");

                    viError = AgVisa32.viPrintf(session, "TRIG:COUNT " + mTrigNum + "\n");
                    viError = AgVisa32.viPrintf(session, "TRIG:SOUR EXT;SLOP POS\n");           //EXT 
                    viError = AgVisa32.viPrintf(session, "SAMP:COUN 1\n");                      //每次采样次数
                    //viError = AgVisa32.viPrintf(session, "OUTP:TRIG:SLOP POS");                        
                    viError = AgVisa32.viPrintf(session, "INIT\n");

                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 释放控制
        /// </summary>
        public void ReleaseControl()
        {
            if (ComMode == 1)
            {
                mSerialPort.WriteLine("SYST:LOC");
            }
            else if (ComMode == 2)
            {
                int resourceManager = 0, viError;
                int session = 0; //session identifier 
                string strVal;

                //returns a session to the Default Resource Manager resource
                viError = AgVisa32.viOpenDefaultRM(out resourceManager);

                viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                    AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);

                viError = AgVisa32.viPrintf(session, "SYST:LOCK:REQ?\n");

                System.Text.StringBuilder idnString =
                    new System.Text.StringBuilder(100);

                viError = AgVisa32.viScanf(session, "%200s", idnString);
                strVal = idnString.ToString().Trim();

                if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                {
                    strVal = "0";
                    throw new Exception("读值失败");
                }
                else
                {
                    strVal = Convert.ToDouble(strVal).ToString();
                }

                viError = AgVisa32.viPrintf(session, "SYSTem:LOCK:RELease\n");

                viError = AgVisa32.viPrintf(session, "SYST:LOCK:REQ?\n");

                idnString = new System.Text.StringBuilder(100);

                viError = AgVisa32.viScanf(session, "%200s", idnString);
                strVal = idnString.ToString().Trim();

                AgVisa32.viClose(session);
                AgVisa32.viClose(resourceManager);

            }
        }

        /// <summary>
        /// 仪表读电压 
        /// </summary>
        /// <param name="Val">读数输出</param>
        public void ReadVolt(out double Val)
        {
            try
            {
                if (ComMode == 1)
                {
                    string strVal;
                    //mSerialPort.WriteLine("MEAS:VOLT:DC? " + Range + ",0.01");
                    //mSerialPort.WriteLine("INIT");
                    //mSerialPort.WriteLine("*TRG");
                    //mSerialPort.WriteLine("FETC?");
                    mSerialPort.WriteLine("READ?");
                    //mSerialPort.WriteLine("MEAS:VOLT:DC? 10,MAX");
                    Thread.Sleep(80);
                    //strVal = mSerialPort.ReadExisting();
                    strVal = mSerialPort.ReadLine();

                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读数出错");
                    }
                    else
                    {
                        strVal = Convert.ToDouble(strVal).ToString();
                    }
                    Val = double.Parse(strVal);
                    mSerialPort.DiscardInBuffer();
                }
                else if (ComMode == 2)
                {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier 
                    string strVal;

                    //returns a session to the Default Resource Manager resource
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);

                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                        AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);

                    viError = AgVisa32.viPrintf(session, "READ?\n");
                    //viError = AgVisa32.viPrintf(session, "FETC?\n");
                    //viError = AgVisa32.viPrintf(session, "MEAS:VOLT:DC? 10,MAX\n");

                    System.Text.StringBuilder idnString =
                        new System.Text.StringBuilder(100);

                    viError = AgVisa32.viScanf(session, "%200s", idnString);
                    strVal = idnString.ToString().Trim();

                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读值失败");
                    }
                    else
                    {
                        strVal = Convert.ToDouble(strVal).ToString();
                    }

                    Val = double.Parse(strVal);
                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
                }
                else
                {
                    Val = 0;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 请求读电压
        /// 与ReadVolt_GetResult配对使用实现读电压,用于电压和内阻测试共用延时.
        /// </summary>
        public void ReadVolt_Request(out int Session, out int ResourceManager)
        {
            try
            {
                if (ComMode == 1)
                {
                    mSerialPort.WriteLine("READ?");
                    Session = 0;
                    ResourceManager = 0;
                }
                else if (ComMode == 2)
                {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier 

                    //returns a session to the Default Resource Manager resource
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);

                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                        AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);

                    viError = AgVisa32.viPrintf(session, "READ?\n");
                    ResourceManager = resourceManager;
                    Session = session;
                }
                else
                {
                    ResourceManager = 0;
                    Session = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取电压结果
        /// </summary>
        public void ReadVolt_GetResult(int Session, int ResourceManager, out double Val)
        {
            try
            {
                if (ComMode == 1)
                {
                    string strVal;
                    strVal = mSerialPort.ReadLine();

                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读数出错");
                    }
                    else
                    {
                        strVal = Convert.ToDouble(strVal).ToString();
                    }
                    Val = double.Parse(strVal);
                    mSerialPort.DiscardInBuffer();
                }
                else if (ComMode == 2)
                {
                    int resourceManager = ResourceManager, viError;
                    int session = Session;      //session identifier 
                    string strVal;

                    System.Text.StringBuilder idnString =
                        new System.Text.StringBuilder(100);

                    viError = AgVisa32.viScanf(session, "%200s", idnString);
                    strVal = idnString.ToString().Trim();

                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读值失败");
                    }
                    else
                    {
                        strVal = Convert.ToDouble(strVal).ToString();
                    }

                    Val = double.Parse(strVal);
                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
                }
                else
                {
                    Val = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 仪表读电压数组
        /// </summary>
        /// <param name="Val"></param>
        public void FetchVolt(out double[] Val)
        {
            try
            {
                string[] mStrVal;
                double[] mVal;

                if (ComMode == 1)
                {
                    string strVal;
                    mSerialPort.WriteLine("FETC?");
                    Thread.Sleep(80);
                    strVal = mSerialPort.ReadLine();

                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读数出错");
                    }

                    if (strVal.Contains(",") == true)
                    {
                        mStrVal = strVal.Split(',');

                        mVal = new double[mStrVal.Length];
                        for (int i = 0; i < mStrVal.Length; i++)
                        {
                            mVal[i] = Convert.ToDouble(strVal);
                        }
                    }
                    else
                    {
                        mVal = new double[1];
                        mVal[0] = Convert.ToDouble(strVal);
                    }

                    Val = mVal;
                    mSerialPort.DiscardInBuffer();
                }
                else if (ComMode == 2)
                {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier 
                    string strVal;

                    //returns a session to the Default Resource Manager resource
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);

                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                        AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);

                    viError = AgVisa32.viPrintf(session, "FETC?\n");
                    //viError = AgVisa32.viPrintf(session, "MEAS:VOLT:DC? 10,MAX\n");

                    System.Text.StringBuilder idnString =
                        new System.Text.StringBuilder(100);

                    viError = AgVisa32.viScanf(session, "%200s", idnString);
                    strVal = idnString.ToString().Trim();

                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读值失败");
                    }

                    if (strVal.Contains(",") == true)
                    {
                        mStrVal = strVal.Split(',');

                        mVal = new double[mStrVal.Length];
                        for (int i = 0; i < mStrVal.Length; i++)
                        {
                            mVal[i] = Convert.ToDouble(mStrVal[i]);
                        }
                    }
                    else
                    {
                        mVal = new double[1];
                        mVal[0] = Convert.ToDouble(strVal);
                    }

                    Val = mVal;
                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
                }
                else
                {
                    Val = null;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 外部触发方式获取数据值
        /// </summary>
        /// <param name="Val">输出:外部触发得到的数据值</param>
        public void ReadValueEXT(out string Val)
        {
            int resourceManager = 0, viError;
            int session = 0; //session identifier 
            string strVal;

            //returns a session to the Default Resource Manager resource
            viError = AgVisa32.viOpenDefaultRM(out resourceManager);

            viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);

            viError = AgVisa32.viPrintf(session, "FETC?\n");
            //viError = AgVisa32.viPrintf(session, "MEAS:VOLT:DC? 10,MAX\n");

            System.Text.StringBuilder idnString =
                new System.Text.StringBuilder(2000);

            viError = AgVisa32.viScanf(session, "%2000s", idnString);   //读取长度:2000, 字符串方式
            //viError = AgVisa32.viScanf(session, "%#f", retDouble);

            strVal = idnString.ToString().Trim();

            AgVisa32.viClose(session);
            AgVisa32.viClose(resourceManager);

            Val = strVal;
        }
        /// <summary>
        /// 仪表读ID
        /// </summary>  
        public void ReadAg344xID()
        {
            try
            {

                string[] arrIDinfo;
                if (ComMode == 1)
                {
                    string strVal;
                    mSerialPort.WriteLine("*IDN?");
                    Thread.Sleep(80);
                    strVal = mSerialPort.ReadLine();
                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读表ID信息失败");
                    }
                    else
                    {
                        arrIDinfo = strVal.Split(',');
                        ag344XIDinfo.Manufacturer = arrIDinfo[0];
                        ag344XIDinfo.Model = arrIDinfo[1];
                        ag344XIDinfo.SerialNumber = arrIDinfo[2];
                        ag344XIDinfo.Vresion = arrIDinfo[3];
                    }
                    mSerialPort.DiscardInBuffer();
                }
                else if (ComMode == 2)
                {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier 
                    string strVal;
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);
                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc, AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);
                    viError = AgVisa32.viPrintf(session, "*IDN? \n");
                    //byte[] buf = Encoding.Default.GetBytes("*IDN?\n");
                    //viError = AgVisa32.viWrite(session, buf, 6, out int retcount);
                    string idnString = "";
                    viError = AgVisa32.viRead(session, out idnString, 72);
                    strVal = idnString.ToString().Trim();
                    if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
                    {
                        strVal = "0";
                        throw new Exception("读表ID信息失败");
                    }
                    else
                    {
                        arrIDinfo = strVal.Split(',');
                        ag344XIDinfo.Manufacturer = arrIDinfo[0];
                        ag344XIDinfo.Model = arrIDinfo[1];
                        ag344XIDinfo.SerialNumber = arrIDinfo[2];
                        ag344XIDinfo.Vresion = arrIDinfo[3];
                    }
                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    public struct Ag344xIDinfo
    {
        public string Manufacturer;
        public string Model;
        public string SerialNumber;
        public string Vresion;

    }
}
