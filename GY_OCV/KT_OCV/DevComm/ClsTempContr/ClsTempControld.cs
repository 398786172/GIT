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
    /// <summary>
    /// 温度控制
    /// 通过温度控制板 
    /// </summary>
    class ClsTempContrl
    {
        public SerialPort mSerialPort = new SerialPort();
        public double[] ArrChannelTemp = new double[88];

        Mutex mMut = new Mutex();
        Thread ThreadTestAction;
        private bool mTestFinish;
        public bool TestFinish { get { return mTestFinish; } set { mTestFinish = value; } }

        byte[] mBuffer = new Byte[1000];
        int mBufferNum;

        public delegate void RefreshUIData(double[] val);
        public RefreshUIData mRefreshUIData;

        int CHCount = ClsGlobal.TrayType;   //通道数

        //构造函数
        public ClsTempContrl(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            SetPortProperty(port, BaudRate, parity, dataBits, stopBits);
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SP_DataReceived);
        }

        public ClsTempContrl(SerialPort SP)
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

        //测试流程
        public void StartTestAction()
        {
            mTestFinish = false;
            ThreadTestAction = new Thread(new ThreadStart(TestTemp));
            ThreadTestAction.Start();
        }

        //测试电池温度流程
        public void TestTemp()
        {
            try
            {
                //调试开关 温度
                int EN_TEMP = 1;
                //调试开关

                if (EN_TEMP == 1)
                {
                    GetTemp(out ArrChannelTemp);
                }
                
                int Val = 0;
                Val = ClsGlobal.TrayType;

                for (int i = 0; i < Val; i++)
                {
                    ClsGlobal.G_dbl_TempArr[i] = ArrChannelTemp[i] + double.Parse(ClsGlobal.TempAdjustCH[i]);
                }                                            

                int Len = ClsGlobal.listETCELL.Count;
                int Num = 0;
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    ClsGlobal.listETCELL[Num].TMP = ClsGlobal.G_dbl_TempArr[i];
                    Num++;
                }

                //界面刷新
                mRefreshUIData(ClsGlobal.G_dbl_TempArr);

                mTestFinish = true;
            }
            catch (Exception ex)
            {
                //测试温度异常   
                ClsGlobal.OCV_TestState = eTestState.ErrTempTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }

    /// <summary>
    /// 温度控制
    /// 通过PLC 
    /// </summary>
    class ClsTempContrl_PLC
    {
        public double[] ArrChannelTemp = new double[ClsGlobal.TrayType];

        Mutex mMut = new Mutex();
        Thread ThreadTestAction;
        private bool mTestFinish;
        public bool TestFinish { get { return mTestFinish; } set { mTestFinish = value; } }
        
        public delegate void RefreshUIData(double[] val);
        public RefreshUIData mRefreshUIData;

        private ClsPLCContr mPlcContr;

        int CHCount = ClsGlobal.TrayType;   //通道数

        public ClsTempContrl_PLC(ClsPLCContr plcContr)
        {
            mPlcContr = plcContr;
        }

        //获取温度值
        public void GetTemp(out double[] val)
        {
            short Temp;
            double res;
            val = new double[CHCount];
            try
            {
                //mPlcContr.ReadDB(ClsDevAddr.PLC_测定温度, out Temp);
                //res = Temp * 0.1;

                //for (int i = 0; i < ClsGlobal.TrayType; i++)
                //{
                //    val[i] = res;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        //测试流程
        public void StartTestAction()
        {
            mTestFinish = false;
            ThreadTestAction = new Thread(new ThreadStart(TestTemp));
            ThreadTestAction.Start();
        }

        //测试电池温度流程
        public void TestTemp()
        {
            try
            {
                //获得温度
                GetTemp(out ArrChannelTemp);

                //载入数据
                int Num = 0;
                int Len = ClsGlobal.listETCELL.Count;

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    ClsGlobal.G_dbl_TempArr[i] = ArrChannelTemp[i] + double.Parse(ClsGlobal.TempAdjustCH[i]);

                    ClsGlobal.listETCELL[Num].TMP = ClsGlobal.G_dbl_TempArr[i];

                    Num++;
                }
                mRefreshUIData(ClsGlobal.G_dbl_TempArr);
                mTestFinish = true;
            }
            catch (Exception ex)
            {
                //测试温度异常   
                ClsGlobal.OCV_TestState = eTestState.ErrTempTest;
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}
