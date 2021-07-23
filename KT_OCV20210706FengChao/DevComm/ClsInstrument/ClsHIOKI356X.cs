using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace OCV
{
    //内阻仪HIOKI365X控制
    public class ClsHIOKI365X
    {
        private SerialPort SPort = new SerialPort();
        static byte[] m_TempBuffer = new byte[10240];           //接收数据缓存
        static int m_RecOffset = 0;                             //接收字节数

        private int mSampleRateType;                            //采样速率: SLOW->1 MED->2, FAST->3   20180402

        public SerialPort SerialPort
        {
            get { return SPort; }
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        public ClsHIOKI365X(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            SetPortProperty(port, BaudRate, parity, dataBits, stopBits);
            mSampleRateType = 2;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">元素：COMx (x=1,2,...)</param>
        public ClsHIOKI365X(string port)
        {
            // 关闭串口
            if (SPort.IsOpen == true)
            {
                SPort.Close();
            }
            if ((SPort != null) && (SPort.IsOpen == false))
            {
                //设置串口名 
                SPort = new SerialPort(ClsGlobal.RT_Port, 9600, Parity.None, 8, StopBits.One);
                SPort.ReadTimeout = 500;
                SPort.DtrEnable = true;
                SPort.RtsEnable = true;
                //RTCom.Encoding = Encoding.UTF8;
                try
                {
                    SPort.Open();

                }
                catch (IOException ex)
                {
                    throw new Exception("表BT3562的COM口错误" + ex.Message);
                }
            }

        }
        public ClsHIOKI365X(SerialPort SP)
        {
            SPort = SP;
            mSampleRateType = 2;
        }

        /// <summary>
        /// 设置串口
        /// </summary>
        private void SetPortProperty(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            try
            {
                // 关闭串口
                if (SPort.IsOpen == true)
                {
                    SPort.Close();
                }
                if ((SPort != null) && (SPort.IsOpen == false))
                {
                    //设置串口名 
                    SPort.PortName = port.Trim();
                    //设置串口的波特率 
                    SPort.BaudRate = BaudRate;
                    //设置停止位 
                    float f = stopBits;
                    if (f == 0)
                    {
                        SPort.StopBits = StopBits.None;
                    }
                    else if (f == 1.5)
                    {
                        SPort.StopBits = StopBits.OnePointFive;
                    }
                    else if (f == 1)
                    {
                        SPort.StopBits = StopBits.One;
                    }
                    else if (f == 2)
                    {
                        SPort.StopBits = StopBits.Two;
                    }
                    else
                    {
                        SPort.StopBits = StopBits.One;
                    }
                    //设置数据位 
                    if ((dataBits > 4) && (dataBits < 9))
                    {
                        SPort.DataBits = dataBits;
                    }
                    else
                    {
                        SPort.DataBits = 8;
                    }

                    //设置奇偶校验位 
                    string s = parity.ToUpper().Trim();
                    if ((s.CompareTo("无") == 0) || (s.CompareTo("NONE") == 0))
                    {
                        SPort.Parity = Parity.None;
                    }
                    else if ((s.CompareTo("奇校验") == 0) || (s.CompareTo("ODD") == 0))
                    {
                        SPort.Parity = Parity.Odd;
                    }
                    else if ((s.CompareTo("偶校验") == 0) || (s.CompareTo("EVEN") == 0))
                    {
                        SPort.Parity = Parity.Even;
                    }
                    else
                    {
                        SPort.Parity = Parity.None;
                    }
                    //设置超时读取时间 
                    SPort.ReadTimeout = 500;

                    SPort.DtrEnable = true;

                    SPort.Open();

                }

            }
            catch (IOException)
            {
                throw new Exception("MMT_Ag34401串口初始化出错");
            }


        }

        /// <summary>
        /// 仪表重启
        /// </summary>
        public void Restart()
        {
            SPort.WriteLine("*RST");
        }

        /// <summary>
        /// 内阻仪初始化(内部触发方式)
        /// 量程:300毫欧 -3毫欧
        /// 修改 20180402
        /// </summary>
        /// <param name="SampleRateType">采样速率: SLOW->1 MED->2, FAST->3</param>
        public void InitControl_IMM(int SampleRateType)
        {
            //mSerialPort.WriteLine("SYST:REM");

            mSampleRateType = SampleRateType;

            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }
            SPort.WriteLine("*RST");
            SPort.WriteLine("*CLS");
            SPort.WriteLine(":RES:RANG 3E-3");
            SPort.WriteLine(":TRIG:SOUR IMM");    //触发方式：内部触发
            //SPort.WriteLine(":RES:RANG 300E-3");    //量程:300毫欧
            //SPort.WriteLine(":TRIG:SOUR IMM");
            //SPort.WriteLine(":AUT ON");
            SPort.WriteLine(":AUT OFF"); //不自动量程
            SPort.WriteLine(":INIT:CONT OFF");     //Read命令连续      
            //SPort.WriteLine(":INIT:CONT OFF");     //Read命令连续        
            SPort.WriteLine(":FUNC RES");         //只测试内阻,不测电压      

            switch (mSampleRateType)             //选择采样速率  20180402
            {
                case 1:
                    SPort.WriteLine("SAMP:RATE SLOW");  //慢   
                    break;
                case 2:
                    SPort.WriteLine("SAMP:RATE MED");   //中  
                    break;
                case 3:
                    SPort.WriteLine("SAMP:RATE FAST");  //快   
                    break;
                default:
                    SPort.WriteLine("SAMP:RATE MED");
                    break;
            }
        }

        /// <summary>
        /// 内阻仪初始化(内部触发方式)
        /// 量程:300毫欧
        /// </summary>
        public void InitControl_IMM_BigRange(int SampleRateType)
        {
            InitControl_IMM(SampleRateType);    //此设备只用量程:300毫欧  20180402
        }

        /// <summary>
        /// 内阻仪初始化(外部触发方式)
        /// 量程:3毫欧
        /// </summary>
        public void InitControl_EXT()
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }
            SPort.WriteLine("*CLS");
            SPort.WriteLine(":AUT OFF");
            SPort.WriteLine(":RES:RANG 3E-3");
            SPort.WriteLine(":TRIG:SOUR EXT");
            SPort.WriteLine(":INIT:CONT ON");

            SPort.WriteLine("SAMP:RATE SLOW");
            SPort.WriteLine(":MEMory:STAT ON");
        }

        /// <summary>
        /// 内阻仪初始化(外部触发方式)
        /// 量程:300毫欧
        /// </summary>
        public void InitControl_EXT_BigRange()
        {
            if (SPort.IsOpen == false)
            {
                SPort.Open();
            }
            SPort.WriteLine("*CLS");
            SPort.WriteLine(":RES:RANG 300E-3");
            SPort.WriteLine(":TRIG:SOUR EXT");
            SPort.WriteLine(":INIT:CONT ON");
            SPort.WriteLine("SAMP:RATE SLOW");
            SPort.WriteLine(":MEMory:STAT ON");
        }

        /// <summary>
        /// 读取多次测量储存数据
        /// </summary>
        /// <param name="Val">数据字符串</param>
        public void ReadMultiData(out string Val)
        {
            try
            {
                string strVal = "";
                SPort.WriteLine(":MEM:DATA?");
                Thread.Sleep(2500);         //串口读取时间

                strVal = SPort.ReadExisting();
                Val = strVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读内阻值  20180402
        /// </summary>
        /// <param name="IMPVal">内阻值（Ω）</param>
        public void ReadData(out double IMPVal)
        {
            try
            {
                string strVal;
                string[] arrStrVal = new string[2];
                //SPort.WriteLine(":FETCh?");
                SPort.WriteLine("*TRG");
                SPort.WriteLine(":READ?");
                switch (mSampleRateType)                //根据采样速率进行延时 20180402
                {
                    case 1:
                        Thread.Sleep(165);
                        break;
                    case 2:
                        Thread.Sleep(70);
                        break;
                    case 3:
                        Thread.Sleep(30);
                        break;
                    default:
                        Thread.Sleep(90);
                        break;
                }

                strVal = SPort.ReadLine();
                //arrStrVal = strVal.Split(',');
                //IMPVal = double.Parse(arrStrVal[0]);
                //double VOLTVal = double.Parse(arrStrVal[1]);
                double tmp1;
                if (double.TryParse(strVal, out tmp1) == true)
                {
                    IMPVal = tmp1;
                }
                else
                {
                    IMPVal = 1000000;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读内阻值  
        /// 与ReadACIR_GetResult配对使用实现读电压,用于电压和内阻测试共用延时.
        /// </summary>
        public void ReadACIR_Request()
        {
            try
            {
                SPort.WriteLine(":READ?");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取内阻值结果
        /// </summary>
        /// <param name="IMPVal"内阻值（Ω）></param>
        public void ReadACIR_GetResult(out double IMPVal)
        {
            try
            {
                string strVal;
                strVal = SPort.ReadLine();

                double tmp1;
                if (double.TryParse(strVal, out tmp1) == true)
                {
                    IMPVal = tmp1;
                }
                else
                {
                    IMPVal = 1000000;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 清除储存数据
        /// </summary>
        public void ClearData()
        {
            try
            {
                SPort.WriteLine(":MEM:ClEA");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 接收事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialGetData(object sender, SerialDataReceivedEventArgs e)
        {
            int i;
            i = SPort.BytesToRead;
            SPort.Read(m_TempBuffer, m_RecOffset, i);
            m_RecOffset += i;
        }

        /// <summary>
        /// 释放控制
        /// </summary>
        public void ReleaseControl()
        {
            SPort.WriteLine("SYST:LOC");
        }

        /// <summary>
        /// 内阻量程
        /// </summary>
        public void ResRange(string Range)
        {
            SPort.WriteLine("RES:RANG " + Range);
        }

        /// <summary>
        /// 电压量程
        /// </summary>
        public void VoltRange(string Range)
        {
            SPort.WriteLine("VOLT:RANG " + Range);
        }

        /// <summary>
        /// 自动量程
        public void AutoRange()
        {
            SPort.WriteLine(":AUT ON");
        }

        /// <summary>
        /// 仪表读ID
        /// </summary>  
        public void ReadHK356xID(out string ID)
        {
            try
            {
                if (SPort.IsOpen == false)
                {
                    SPort.Open();
                }
                SPort.WriteLine(":*IDN?");
                Thread.Sleep(80);
                string strVal = SPort.ReadLine();
                ID = strVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
