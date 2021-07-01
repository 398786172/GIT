using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace OCV
{
    /// <summary>
    /// 读一维/二维条形码类__阅读器Vuquest 3310g
    /// </summary>
    public class ClsCodeScan
    {
        //private const int SERIAL_PORT_COUNT = 1;
        //private const int RECV_DATA_MAX = 10240;
        //private const bool binaryDataMode = false;
        private SerialPort mSerialPort = new SerialPort();

        //构造函数
        public ClsCodeScan()
        {
            string[] str = SerialPort.GetPortNames();
            if (str == null || str.Length == 0)
            { 
                return;
            }
        }        

        /// <summary>
        /// 设置串口
        /// </summary>
        public bool SetPortProperty(string port, int BaudRate, string parity, int dataBits, float stopBits)
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
                if ((s.CompareTo("无") == 0) || (s.CompareTo("None") == 0) || (s.CompareTo("None") == 0))
                {
                    mSerialPort.Parity = Parity.None;
                }
                else if ((s.CompareTo("奇校验") == 0) || (s.CompareTo("Odd")==0) || (s.CompareTo("odd") == 0))
                {
                    mSerialPort.Parity = Parity.Odd;
                }
                else if ((s.CompareTo("偶校验") == 0) || (s.CompareTo("Even") == 0) || (s.CompareTo("even") == 0))
                {
                    mSerialPort.Parity = Parity.Even;
                }
                else
                {
                    mSerialPort.Parity = Parity.None;
                }
                //设置超时读取时间 
                mSerialPort.ReadTimeout = 500;
                //打开串口 
                try
                {
                    mSerialPort.Open();
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            }
            else
                return false;

        }

        /// <summary>
        /// 设置串口
        /// </summary>
        public bool SetPortProperty(string port, int BaudRate, Parity Parity, int dataBits, StopBits StopBits)
        {
            mSerialPort.Dispose();

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
                mSerialPort.StopBits = StopBits;                
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
                mSerialPort.Parity = Parity;
                
                //设置超时读取时间 
                mSerialPort.ReadTimeout = 500;
                //打开串口 
                try
                {
                    mSerialPort.Open();
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            }
            else
                return false;

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {            
            if (mSerialPort.IsOpen == true)
            {
                mSerialPort.DiscardInBuffer();
                mSerialPort.Close();
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            if (mSerialPort.IsOpen == false)
            {
                mSerialPort.Open();
            }
            mSerialPort.DiscardInBuffer();
        }

        /// <summary>
        /// 读取条码方法
        /// </summary>
        /// <returns></returns>
        public string ReadCode()
        {
            byte[] buffer = new byte[3];
            buffer[0] = 0x16;
            buffer[1] = 0x54;
            buffer[2] = 0x0D;
 
            if (this.mSerialPort.IsOpen)
            {
                try
                {
                    mSerialPort.DiscardInBuffer();
                    mSerialPort.Write(buffer, 0, 3);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(mSerialPort.PortName + "\r\n" + ex.Message);    // disappeared
                    return "ERROR";
                }
            }
            else
            {
                return "NG";
            }

            if (mSerialPort.IsOpen == false)
            {
                return "NG";
            }

            Thread.Sleep(1500);
            string strBar = mSerialPort.ReadExisting();
            if (strBar == "" || strBar == null)
            {
                buffer[0] = 0x16;
                buffer[1] = 0x55;
                buffer[2] = 0x0D;
                mSerialPort.Write(buffer, 0, 3);
                return "NG";
            }
            else
            {
                return strBar;
            }

        }
    }


}
