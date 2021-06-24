using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace OCV
{

    //万用表Ag344x
    public class ClsDMM_Ag34970
    {
        private SerialPort mSerialPort;
        private string mUSBDesc;

        public SerialPort SerialPort
        {
            get { return mSerialPort; }
        }
        public int ComMode;     //ComMode=1: 232串口  , ComMode=2: GPIB
        public int TrigMode;    //触发模式 =1:IMM  =2:EXT
        private uint mTrigNum = 1;

        /// <summary>
        /// 万用表构造
        /// </summary>
        /// <param name="port">串口端口</param>
        /// <param name="BaudRate">波特率</param>
        /// <param name="parity">奇偶</param>
        /// <param name="dataBits">大小</param>
        /// <param name="stopBits">停止位</param>
        public ClsDMM_Ag34970(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            mSerialPort = new SerialPort();
            SetPortProperty(port, BaudRate, parity, dataBits, stopBits);
            ComMode = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SP">串口</param>
        public ClsDMM_Ag34970(SerialPort SP)
        {
            mSerialPort = SP;
            ComMode = 1;     //串口方式
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="USBDesc">GPIB地址</param>
        public ClsDMM_Ag34970(string USBDesc)
        {
            mUSBDesc = USBDesc;
            ComMode = 2;    //USB方式        
        }

        /// <summary>
        /// 设置串口
        /// </summary>
        private void SetPortProperty(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            try
            {
                // 关闭串口
                if (mSerialPort.IsOpen == true)
                {
                    mSerialPort.Close();
                }
                if ((mSerialPort != null) && (mSerialPort.IsOpen == false))
                {
                    //设置串口名 
                    mSerialPort.PortName = port.Trim();
                    //设置串口的波特率 
                    mSerialPort.BaudRate = BaudRate;
                    //设置停止位 
                    float f = stopBits;
                    if (f == 0)
                    {
                        mSerialPort.StopBits = StopBits.None;
                    }
                    else if (f == 1.5)
                    {
                        mSerialPort.StopBits = StopBits.OnePointFive;
                    }
                    else if (f == 1)
                    {
                        mSerialPort.StopBits = StopBits.One;
                    }
                    else if (f == 2)
                    {
                        mSerialPort.StopBits = StopBits.Two;
                    }
                    else
                    {
                        mSerialPort.StopBits = StopBits.One;
                    }
                    //设置数据位 
                    if ((dataBits > 4) && (dataBits < 9))
                    {
                        mSerialPort.DataBits = dataBits;
                    }
                    else
                    {
                        mSerialPort.DataBits = 8;
                    }

                    //设置奇偶校验位 
                    string s = parity.Trim();
                    if ((s.CompareTo("无") == 0) || (s.ToUpper().CompareTo("NONE") == 0))
                    {
                        mSerialPort.Parity = Parity.None;
                    }
                    else if ((s.CompareTo("奇校验") == 0) || (s.ToUpper().CompareTo("ODD") == 0))
                    {
                        mSerialPort.Parity = Parity.Odd;
                    }
                    else if ((s.CompareTo("偶校验") == 0) || (s.ToUpper().CompareTo("EVEN") == 0))
                    {
                        mSerialPort.Parity = Parity.Even;
                    }
                    else
                    {
                        mSerialPort.Parity = Parity.None;
                    }
                    //设置超时读取时间 
                    mSerialPort.ReadTimeout = 500;
                    mSerialPort.DtrEnable = true;
                    mSerialPort.Encoding = Encoding.UTF8;//2016-5-10
                    mSerialPort.Open();
                }

            }
            catch (Exception ex)//IOException)
            {
                throw ex;
            }


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
                    //mSerialPort.WriteLine("CONF:VOLT:DC 10, MAX");  //1E-6  0.000001
                    //mSerialPort.WriteLine("VOLT:DC:NPLC 10");  //1E-6  0.000001
                    //mSerialPort.WriteLine("TRIG:SOUR IMM\n");

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
                    viError = AgVisa32.viPrintf(session, "CONF:VOLT:DC (@101:214)\n");
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
                   
                    mSerialPort.WriteLine("READ?");
                    Thread.Sleep(80);
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
                    //strVal = mSerialPort.ReadLine(); 
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
                double [] mVal;

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
        /// 读取通道切换数量 
        /// </summary>
        /// <param name="Val">读数输出</param>
        public void ReadPOINTS(out int ch)
        {
            try
            {
                    int resourceManager = 0, viError;
                    int session = 0; //session identifier 
                    string strVal;

                    //returns a session to the Default Resource Manager resource
                    viError = AgVisa32.viOpenDefaultRM(out resourceManager);

                    viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                        AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);

                    viError = AgVisa32.viPrintf(session, "DATA:POINTS?\n");
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
                        strVal = Convert.ToInt16(strVal).ToString();
                    }
                    ch = int.Parse(strVal);
                    AgVisa32.viClose(session);
                    AgVisa32.viClose(resourceManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        ///  函数: 切换控制命令
        /// </summary>
        /// <param name="TestType">1 电压 2壳压</param>
        /// <param name="Channel">通道号: 1~ X , 按照板顺序</param>
        public void ChannelSwitchContr_IMM(int TestType,int Channel)
        {
            try
            {
                string strCh = "";
                if (TestType==1)
                {
                    strCh = "1" + Channel.ToString("00");
                }
                else
                {
                    strCh = "2" + Channel.ToString("00"); 
                }
             
                int resourceManager = 0, viError;
                int session = 0; //session identifier       
                viError = AgVisa32.viOpenDefaultRM(out resourceManager);
                viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                    AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);
                viError = AgVisa32.viPrintf(session, "ROUT:MON (@" + strCh + ")\n");
                viError = AgVisa32.viPrintf(session, "ROUT:MON:STATE ON\n");
                viError = AgVisa32.viPrintf(session, "ROUT:SCAN (@" + strCh + ")\n");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

   
        /// <summary>
        /// 函数: 切换控制命令
        /// </summary>
        ///</param>
        /// <param name="Channel">通道号: 1~ X , 按照板顺序</param>
        public void ChannelSwitchContr(int TestType)
        {
            try
            {
                string strCh = "";
                if (TestType == 1)
                {
                    strCh = "(@101:114)";
                }
                else
                {
                    strCh = "(@201:214)";
                }

                int resourceManager = 0, viError;
                int session = 0; //session identifier       
                viError = AgVisa32.viOpenDefaultRM(out resourceManager);
                viError = AgVisa32.viOpen(resourceManager, mUSBDesc,
                    AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);
                viError = AgVisa32.viPrintf(session, "*CLS\n");
                viError = AgVisa32.viPrintf(session, "*RST\n");
                
                viError = AgVisa32.viPrintf(session, "CONF:VOLT:DC "+ strCh + "\n");
                viError = AgVisa32.viPrintf(session, "ROUT:CHAN:DELAY 0.2 ," + strCh + "\n");
                viError = AgVisa32.viPrintf(session, "ROUT:SCAN "+ strCh + "\n");
                viError = AgVisa32.viPrintf(session, "INIT\n");
          
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }    

}
