using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace OCV
{
    public class ClsTempContr
    {
        public SerialPort mSerialPort = new SerialPort();
        public double[] ArrChannelTemp = new double[88];

        Mutex mMut = new Mutex();
        Thread ThreadTestAction;
        private bool mTestFinish;
        public bool TestFinish { get { return mTestFinish; } set { mTestFinish = value; } }

        byte[] mBuffer = new Byte[48];
        int mBufferNum;
        string sTemp;

        public delegate void RefreshUIData(double[] val);
        public RefreshUIData mRefreshUIData;

        int mCHCount = ClsGlobal.TrayType;   //通道数    
        int mEnCH = ClsGlobal.EnCH;     //使用的通道   
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
            buffer[3] = 0x00;
            buffer[4] = 0x03;
            buffer[5] = 0x00;
            buffer[6] = 0x03;
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
                    if (mBufferNum > 24)
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
                    if (DataNum != 16)
                    {
                        throw new Exception("温度值数据获取数量不对");
                    }
                    if (end != 0xdd)
                    {
                        throw new Exception("温度值数据结尾出错");
                    }
                    result = new double[8];
                    for (int i = 0; i < 8; i++)
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
        public void GetCHTemp(int ch, int CHcount, out double[] val)
        {
            double[] result = new double[CHcount];
            double[] TempVal;
            try
            {
                if (ch > 8 || ch < 1)
                {
                    throw new Exception("温度通道设置错误");
                }

                GetTemp(out TempVal);
                for (int i = 0; i < CHcount; i++)
                {
                    result[i] = TempVal[ch - 1];
                }
                val = result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Restart()
        {
            byte[] buffer = new byte[8];
            double[] result;
            buffer[0] = 0x11;
            buffer[1] = 0x22;
            buffer[2] = 0x33;
            buffer[3] = 0x00;
            buffer[4] = 0xFE;      //cmd
            buffer[5] = 0x00;
            buffer[6] = 0xFE;      //255
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
                    if (mBufferNum > 8)
                    {
                        break;
                    }
                }
                if (mBuffer[0] == 0xAA && mBuffer[1] == 0xBB && mBuffer[2] == 0xCC)
                {
                    byte Addr = mBuffer[3];
                    byte cmd = mBuffer[4];
                    byte end = mBuffer[mBufferNum - 1];
                    if (end != 0xdd)
                    {
                        throw new Exception("复位数据结尾出错");
                    }
                    byte Data = mBuffer[5];
                    if (Data != 0x00)
                    {
                        throw new Exception("温控板复位失败");
                    }
                    result = new double[8];

                }
                else
                {
                    throw new Exception("温控板复位失败");
                }
                // val = result;
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

        public void InitiaCalValue()
        {
            byte[] buffer = new byte[12];
            double[] result;
            buffer[0] = 0x11;
            buffer[1] = 0x22;
            buffer[2] = 0x33;
            buffer[3] = 0x00;
            buffer[4] = 0x04;      //cmd
            buffer[5] = 0x04;
            buffer[6] = 0xFF;      //255
            buffer[7] = 0x00;
            buffer[8] = 0x00;
            buffer[9] = 0x00;
            buffer[10] = 0xFF;
            buffer[11] = 0x44;
            try
            {
                mMut.WaitOne();
                if (mSerialPort.IsOpen == false)
                {
                    mSerialPort.Open();
                }
                mBufferNum = 0;
                mSerialPort.Write(buffer, 0, 12);
                for (int i = 0; i < 30; i++)
                {
                    Thread.Sleep(20);
                    if (mBufferNum > 8)
                    {
                        break;
                    }
                }
                if (mBuffer[0] == 0xAA && mBuffer[1] == 0xBB && mBuffer[2] == 0xCC)
                {
                    byte Addr = mBuffer[3];
                    byte cmd = mBuffer[4];
                    byte end = mBuffer[mBufferNum - 1];
                    if (end != 0xdd)
                    {
                        throw new Exception("初始化校准温度数据结尾出错");
                    }
                    byte Data = mBuffer[5];
                    if (Data != 0x00)
                    {
                        throw new Exception("初始化校准温度失败");
                    }
                    result = new double[8];

                }
                else
                {
                    throw new Exception("初始化校准温度失败");
                }
                // val = result;
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
                    GetCHTemp(mEnCH, mCHCount, out ArrChannelTemp);

                }
                for (int i = 0; i < mCHCount; i++)
                {
                    ClsGlobal.G_dbl_P_TempArr[i] = ArrChannelTemp[i] + double.Parse(ClsGlobal.mTempAdjustVal[i]);
                    ClsGlobal.listETCELL[i].TMP = ClsGlobal.G_dbl_TempArr[i];
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

        ////测试流程
        //public void StartTestAction()
        //{
        //    mTestFinish = false;
        //    ThreadTestAction = new Thread(new ThreadStart(TestTempForProc));
        //    ThreadTestAction.Start();
        //}

        ////测试电池温度(流程处理使用)
        //public void TestTempForProc()
        //{
        //    try
        //    {
        //        GetTemp(out ArrChannelTemp);

        //        int Val = 0;
        //        if (ClsGlobal.TrayType == 70)
        //        {
        //            Val = 88;
        //        }
        //        else
        //        {
        //            Val = ClsGlobal.TrayType;
        //        }

        //        //温度异常检查
        //        for (int i = 0; i < Val; i++)
        //        {
        //            //温度通道映射
        //            int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp[i]);

        //            if ((ClsGlobal.TrayType == 70))
        //            {
        //                if ((i > 8 && i < 44) || (i > 52 && i < 88))
        //                {
        //                    if (ArrChannelTemp[ActualNum - 1] < 15)
        //                    {
        //                        throw new Exception("第" + (i + 1) + "通道温度校准前测试值<15度");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (ArrChannelTemp[ActualNum - 1] < 15)
        //                {
        //                    throw new Exception("第" + (i + 1) + "通道温度校准前测试值<15度");
        //                }
        //            }
        //            ClsGlobal.G_dbl_TempArr[i] = ArrChannelTemp[ActualNum - 1] +
        //                double.Parse(ClsGlobal.mTempAdjustCH[ActualNum - 1]);
        //        }

        //        //界面刷新
        //        mRefreshUIData(ClsGlobal.G_dbl_TempArr);

        //        //数据转换
        //        int Len = ClsGlobal.listETCELL.Count;                
        //        int Num = 0; 
        //        if (ClsGlobal.TrayType == 70)
        //        {
        //            for (int i = 9; i < 44; i++)
        //            {
        //                ClsGlobal.listETCELL[Num].OCV_T = ClsGlobal.G_dbl_TempArr[i];
        //                Num++;
        //            }

        //            for (int i = 53; i < 88; i++)
        //            {
        //                ClsGlobal.listETCELL[Num].OCV_T = ClsGlobal.G_dbl_TempArr[i];
        //                Num++;
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < ClsGlobal.TrayType; i++)
        //            {
        //                ClsGlobal.listETCELL[Num].OCV_T = ClsGlobal.G_dbl_TempArr[i];
        //                Num++;
        //            }
        //        }

        //        mTestFinish = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //测试温度异常   
        //        ClsGlobal.OCV_TestState = eTestState.ErrTempTest;
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        ////自检温度测试,是否有异常
        ////result: -1->不需要进行测试
        ////result: 0->正常
        ////result: 1->测试值为0,可能测试断路
        ////result: 2->测试值小于15度,测试温度过低
        //public void SelfCheckTemp(out int[] result)
        //{
        //    result = new int[88];
        //    double[] ArrTemp = new double[88];

        //    try
        //    {
        //        GetTemp(out ArrTemp);

        //        if (ClsGlobal.TrayType == 70)
        //        {
        //            for (int i = 0; i < 88; i++)
        //            {
        //                if ((i >= 9 && i < 44) || (i >= 53 && i < 88))
        //                {
        //                    if (ArrTemp[i] == 0)
        //                    {
        //                        result[i] = 1;
        //                    }
        //                    else if (Math.Abs(ArrTemp[i]) < 15)
        //                    {
        //                        result[i] = 2;
        //                    }
        //                    else
        //                    {
        //                        result[i] = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    result[i] = -1;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < ClsGlobal.TrayType; i++)
        //            {
        //                if (ArrTemp[i] == 0)
        //                {
        //                    result[i] = 1;
        //                }
        //                else if (Math.Abs(ArrTemp[i]) < 15)
        //                {
        //                    result[i] = 2;
        //                }
        //                else
        //                {
        //                    result[i] = 0;
        //                }
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }

        //}


    }
}
