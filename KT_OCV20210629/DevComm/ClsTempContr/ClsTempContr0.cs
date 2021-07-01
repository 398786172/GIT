using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports ;
using System.Threading;
using System.Windows.Forms;

namespace OCV
{

    //温度控制
    public class ClsTempContr
    {
        public SerialPort mSerialPort = new SerialPort();
        public double[] ArrChannelTemp = new double[88];

        Mutex mMut = new Mutex();
        Thread ThreadTestAction;
        private bool mTestFinish;
        public bool TestFinish { get { return mTestFinish; } }  //set { mTestFinish = value; }

        byte[] mBuffer = new Byte[1000];
        int mBufferNum;
        string sTemp;

        public delegate void RefreshUIData(double[] val);
        public RefreshUIData mRefreshUIData;

        public ClsTempContr(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            SetPortProperty(port, BaudRate, parity, dataBits, stopBits);
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SP_DataReceived);
        }

        public ClsTempContr(SerialPort SP)
        {
            mSerialPort = SP;
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SP_DataReceived);
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
                    mSerialPort.ReadBufferSize = 300;
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
                    string s = parity.Trim().ToLower();
                    if ((s.CompareTo("无") == 0) || (s.CompareTo("none") == 0))
                    {
                        mSerialPort.Parity = Parity.None;
                    }
                    else if ((s.CompareTo("奇校验") == 0) || (s.CompareTo("odd") == 0))
                    {
                        mSerialPort.Parity = Parity.Odd;
                    }
                    else if ((s.CompareTo("偶校验") == 0) || (s.CompareTo("even") == 0))
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
                    //mSerialPort.Encoding = Encoding.UTF8;//2016-5-10

                    mSerialPort.Open();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("串口初始化出错:" + ex.Message);
            }
        }
        
        private void m_SP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int Val = mSerialPort.BytesToRead;
            mSerialPort.Read(mBuffer, mBufferNum, Val);
            mBufferNum = mBufferNum + Val;
        }
        
        //获取温度值
        public void GetTemp(out double[] val)
        {
            byte[] buffer = new byte[8];
            double[] result;

            buffer[0] = 0x11;
            buffer[1] = 0x22;
            buffer[2] = 0x33;
            buffer[3] = 0x01;
            buffer[4] = 0x04;
            buffer[5] = 0x00;
            buffer[6] = 0x05;
            buffer[7] = 0x44;

            try
            {
                mMut.WaitOne();
                if (mSerialPort.IsOpen == false)
                {
                    mSerialPort.Open();
                }

                mBufferNum = 0;
                mSerialPort.Write(buffer, 0, 8);

                for (int i = 0; i < 30; i++)
                {
                    Thread.Sleep(20);
                    if (mBufferNum > 183)
                    {
                        break;
                    }
                }

                if (mBuffer[0] == 0xAA && mBuffer[1] == 0xBB && mBuffer[2] == 0xCC)
                {
                    byte Addr = mBuffer[3];
                    byte cmd = mBuffer[4];
                    byte end = mBuffer[mBufferNum - 1];
                    byte DataNum = mBuffer[5];
                    if (DataNum != 176)
                    {
                        throw new Exception("温度值数据获取数量不对");
                    }
                    if (end != 0xdd)
                    {
                        throw new Exception("温度值数据结尾出错");
                    }
                    result = new double[88];
                    for (int i = 0; i < 88; i++)
                    {
                        result[i] = ((double)mBuffer[i * 2 + 6] + (double)mBuffer[i * 2 + 7] * 256) / 10;
                    }
                }
                else
                {
                    throw new Exception("获取温度值数据出错");
                }
                val = result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mMut.ReleaseMutex();
            }

        }

        //public void GetCHTemp(int CHcount, out double[] val)
        //{
        //    double[] result = new double[CHcount];
        //    double[] TempVal;
        //    try
        //    {
        //        GetTemp(out TempVal);
        //        for (int i = 0; i < CHcount; i++)
        //        {
        //            result[i] = TempVal[i];
        //        }
        //        val = result;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
