using System;
using System.IO.Ports;
using System.Runtime.Serialization;
using System.Threading;

namespace OCV
{
    public class ClsLocaRange
    {
        public SerialPort mSerialPort = new SerialPort();
        public double[] ArrChannelTemp = new double[88];
        Mutex MT = new Mutex();
        // Mutex mMut = new Mutex();
        Thread ThreadTestAction;
        private bool mTestFinish;
        public bool TestFinish { get { return mTestFinish; } set { mTestFinish = value; } }

        byte[] mBuffer = new Byte[48];
        int mBufferNum;

        public delegate void RefreshUIData(double[] val);
        public RefreshUIData mRefreshUIData;

        // int CHCount = ClsGlobal.TrayType;   //通道数    

        public ClsLocaRange(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            SetPortProperty(port, BaudRate, parity, dataBits, stopBits);
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SP_DataReceived);
        }

        public ClsLocaRange(SerialPort SP)
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

        //获取位置信息
        public bool GetData(ushort DevAddr, out double Value)
        {
            try
            {
                double Val = -1000;
                int unit = 0;
                byte[] mBufbytes = new byte[9];
                byte[] mBufValue = new byte[2];
                byte[] mBufunit = new byte[2];
                if (mSerialPort.IsOpen == false)
                {
                    mSerialPort.Open();
                }
                ReadRegister(DevAddr, 0x04, 0x0000, 2, out mBufbytes);
                if (mBuffer[0] == DevAddr && mBuffer[1] == 0x04)
                {
                    byte DataNum = mBuffer[2];
                    if (DataNum != 4)
                    {
                        throw new Exception("获取位移数据数量不对");
                    }
                    Array.Copy(mBufbytes, 0, mBufValue, 0, 2);
                    Array.Copy(mBufbytes, 2, mBufunit, 0, 2);

                    unit = Convert.ToInt16(byteToHexStr(mBufunit), 16);
                    if (unit == 1)
                    {
                        Val = Convert.ToInt16(byteToHexStr(mBufValue), 16) / 10.0;

                    }
                    else if (unit == 2)
                    {
                        Val = Convert.ToInt16(byteToHexStr(mBufValue), 16) / 100.0;
                    }
                    else if (unit == 3)
                    {
                        Val = Convert.ToInt16(byteToHexStr(mBufValue), 16) / 1000.0;
                    }
                    else
                    {
                        Val = -1000;
                        //  MessageBox.Show("获取位移数据单位异常");
                        throw new Exception("获取位移数据单位异常");
                    }
                }
                else
                {
                    Val = -1000;
                    // MessageBox.Show("获取位移数据出错");
                    throw new Exception("获取位移数据出错");
                }
                Value = Val;
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                Value = -1000;
                //return false;
                throw ex;
            }
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        //写寄存器
        public void WriteSingleRegister(ushort DevAddr, ushort Cmd, ushort Addr, short val)
        {
            byte[] arrDevAddr = BitConverter.GetBytes(DevAddr);
            byte[] arrCmd = BitConverter.GetBytes(Cmd);
            byte[] arrAddr = BitConverter.GetBytes(Addr);
            byte[] arrVal = BitConverter.GetBytes(val);

            byte[] sendBuf = new byte[9];
            int ErrId = 0;

            try
            {
                sendBuf[0] = arrDevAddr[0];
                sendBuf[1] = arrCmd[0];
                sendBuf[2] = arrAddr[1];       //地址
                sendBuf[3] = arrAddr[0];
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x01;
                sendBuf[6] = 0x02;
                sendBuf[7] = arrVal[1];        //数值          
                sendBuf[8] = arrVal[0];
                MT.WaitOne();
                ErrId = ModBus_Write(ref sendBuf);
                MT.ReleaseMutex();
                if (ErrId != 0) throw new CustomException("通讯出错", 0, ErrId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //读寄存器
        public void ReadRegister(ushort DevAddr, ushort Cmd, ushort Addr, int RegNum, out byte[] val)
        {
            byte[] arrDevAddr = BitConverter.GetBytes(DevAddr);
            byte[] arrCmd = BitConverter.GetBytes(Cmd);
            byte[] arrAddr = BitConverter.GetBytes(Addr);
            byte[] arrNum = BitConverter.GetBytes(RegNum);
            byte[] sendBuf = new byte[6];
            int ErrId = 0;
            try
            {
                sendBuf[0] = arrDevAddr[0];
                sendBuf[1] = arrCmd[0];
                sendBuf[2] = arrAddr[1];       //地址
                sendBuf[3] = arrAddr[0];
                sendBuf[4] = arrNum[1];         //寄存器数量
                sendBuf[5] = arrNum[0];
                MT.WaitOne();
                ErrId = ModBus_Read(ref sendBuf, out val);
                MT.ReleaseMutex();
                if (ErrId != 0) throw new CustomException("通讯出错", 0, ErrId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ModBus函数

        /// <summary>
        /// MODBUS读取通讯函数
        /// </summary>
        /// <param name="sendBuf">发送字节数组</param> 
        /// <param name="receiveBuf">接收字节数组</param> 
        /// <returns>OK->0; NG->1,2,...</returns> 
        private int ModBus_Read(ref byte[] sendBuf, out byte[] receiveBuf)
        {
            int i;
            byte[] arrSendBuf;
            byte sendLen;
            byte[] recBuf;
            byte[] CRCNum;
            byte[] CRCResult;
            int recLengh = 0;
            int delayTimes;
            int tryTimes = 3;
            int ErrId = 0;
            byte[] CRCN = new byte[9];
            receiveBuf = null;
        tryTimes:
            try
            {
                #region //处理  
                CRCN[0] = 0x01;
                CRCN[1] = 0x04;
                CRCN[2] = 0x04;
                CRCN[3] = 0x11;
                CRCN[4] = 0x94;
                CRCN[5] = 0x00;
                CRCN[6] = 0x03;
                CRCN[7] = 0xff;
                CRCN[8] = 0x55;
                recLengh = CRCN[2];
                CRCResult = ClsCRC.CountCRC(CRCN, 0, recLengh + 3, true);
                if ((CRCResult[0] != CRCN[recLengh + 3]) || (CRCResult[1] != CRCN[recLengh + 4]))
                {
                    ErrId = 5;                  //CRC校验出错
                    throw new Exception();
                }
                if (mSerialPort.IsOpen == true)
                {
                    sendLen = (byte)(sendBuf.Length + 2);  //发送总长度
                    arrSendBuf = new byte[sendLen];     //发送的数组
                    sendBuf.CopyTo(arrSendBuf, 0);
                    CRCNum = ClsCRC.CountCRC(sendBuf, true);  //CRC计算
                    CRCNum.CopyTo(arrSendBuf, sendBuf.Length);
                    mBufferNum = 0;                   //复位接收偏移量
                    mSerialPort.Write(arrSendBuf, 0, sendLen); //写串口
                }
                else
                {
                    ErrId = 3;                  //COM口没打开
                    throw new Exception();
                }
                //接收头部3个字节
                ErrId = 1;                      //接收超时
                delayTimes = 3;
                do
                {
                    Thread.Sleep(20);
                    //Delay(10);
                    i = mBufferNum;
                    if (i >= 3)
                    {
                        ErrId = 0;
                        break;
                    }
                    delayTimes--;
                } while (delayTimes > 0);
                if (ErrId == 1)
                {
                    throw new Exception();
                }
                //检查接收字头
                for (i = 0; i < 2; i++)
                {
                    if (mBuffer[i] != sendBuf[i])
                    {
                        ErrId = 2;               //接收字节出错
                        throw new Exception();
                    }
                }
                //确认接收字节数
                recLengh = mBuffer[2];
                //接收剩余数据
                int tempNum = recLengh + 2;                   //加校验码2字节
                //接收所有数据
                ErrId = 1;
                delayTimes = 3;
                do
                {
                    Thread.Sleep(10);
                    //Delay(100);
                    i = mBufferNum - 3;
                    if (i >= tempNum)
                    {
                        ErrId = 0;
                        break;
                    }
                    delayTimes--;
                } while (delayTimes > 0);
                if (ErrId == 1)
                {
                    throw new Exception();
                }
                //计算CRC
                CRCResult = ClsCRC.CountCRC(mBuffer, 0, recLengh + 3, true);
                if ((CRCResult[0] != mBuffer[recLengh + 3]) || (CRCResult[1] != mBuffer[recLengh + 4]))
                {
                    ErrId = 5;                  //CRC校验出错
                    throw new Exception();
                }
                //获取接收字节
                recBuf = new byte[recLengh];
                for (i = 0; i < recLengh; i++)
                {
                    recBuf[i] = mBuffer[i + 3];
                }

                receiveBuf = recBuf;

                #endregion

            }
            catch
            {
                switch (ErrId)
                {
                    case 1:
                        tryTimes--;
                        if (tryTimes > 0)
                        {
                            goto tryTimes;   //超时错误
                        }
                        else
                        {
                            return ErrId;
                            //throw;
                        }
                    case 2:
                        tryTimes--;
                        if (tryTimes > 0)
                        {
                            goto tryTimes;   //接收错误
                        }
                        else
                        {
                            return ErrId;
                        }
                    case 3: return ErrId;   //COM口没打开
                    case 4: return ErrId;
                    case 5: return ErrId;   //CRC校验出错
                }
                ErrId = 55;
                throw;
            }

            return 0;
        }

        /// <summary>
        /// MODBUS写操作通讯函数
        /// </summary>
        /// <param name="sendBuf">发送字节数组</param> 
        /// <returns>OK->0; NG->not 0</returns> 
        private int ModBus_Write(ref byte[] sendBuf)
        {
            int i;
            byte[] arrSendBuf;
            byte sendLen;
            byte[] CRCNum;
            int delayTimes;
            int tryTimes = 3;
            int ErrId = 0;

        tryTimes:
            try
            {
                #region //处理
                if (mSerialPort.IsOpen == true)
                {
                    sendLen = (byte)(sendBuf.Length + 2);  //发送总长度
                    arrSendBuf = new byte[sendLen];     //发送的数组
                    sendBuf.CopyTo(arrSendBuf, 0);
                    CRCNum = ClsCRC.CountCRC(sendBuf, true);  //CRC计算
                    CRCNum.CopyTo(arrSendBuf, sendBuf.Length);
                    mBufferNum = 0;                   //复位接收偏移量
                    mSerialPort.Write(arrSendBuf, 0, sendLen); //写串口
                }
                else
                {
                    ErrId = 3;                  //COM口没打开
                    throw new Exception();
                }
                //接收所有字节
                ErrId = 1;
                delayTimes = 3;
                do
                {
                    Thread.Sleep(10);
                    //Delay(10);
                    i = mBufferNum;
                    if (i >= 8)
                    {
                        ErrId = 0;
                        break;
                    }
                    delayTimes--;
                } while (delayTimes > 0);
                if (ErrId == 1)
                {
                    throw new Exception();       //接收超时
                }
                //检查接收字节
                for (i = 0; i < 6; i++)
                {
                    if (mBuffer[i] != sendBuf[i])
                    {
                        ErrId = 2;               //接收字节出错
                        throw new Exception();
                    }
                }

                #endregion

            }
            catch
            {
                switch (ErrId)
                {
                    case 1:
                        tryTimes--;
                        if (tryTimes > 0)
                        {
                            goto tryTimes;                  //超时错误
                        }
                        else
                        {
                            return ErrId;
                        }
                    case 2:
                        tryTimes--;
                        if (tryTimes > 0)
                        {
                            goto tryTimes;                  //接收错误
                        }
                        else
                        {
                            return ErrId;
                        }
                    case 3: return ErrId;                   //COM口没打开
                }
                ErrId = 55;
                throw;
            }

            return 0;
        }

        #endregion ModBus函数
        //CRC计算类
        class ClsCRC
        {
            //双表(高低字节)查询
            public static byte[] CountCRC(byte[] btArray, int start, int lengh, bool IsLSB)
            {
                int index;
                int crc_Low = 0xFF;
                int crc_High = 0xFF;
                for (int i = start; i < lengh; i++)
                {
                    index = crc_High ^ (char)btArray[i];
                    crc_High = crc_Low ^ aucCRCHi[index];
                    crc_Low = (byte)aucCRCLo[index];
                }
                if (IsLSB == true)
                {
                    return new byte[2] { (byte)crc_High, (byte)crc_Low };
                }
                else
                {
                    return new byte[2] { (byte)crc_Low, (byte)crc_High };
                }
            }

            //双表(高低字节)查询2
            public static byte[] CountCRC(byte[] btArray, bool IsHighBefore)
            {
                int index;
                int crc_Low = 0xFF;
                int crc_High = 0xFF;
                for (int i = 0; i < btArray.Length; i++)
                {
                    index = crc_High ^ (char)btArray[i];
                    crc_High = crc_Low ^ aucCRCHi[index];
                    crc_Low = (byte)aucCRCLo[index];
                }
                if (IsHighBefore == true)
                {
                    return new byte[2] { (byte)crc_High, (byte)crc_Low };
                }
                else
                {
                    return new byte[2] { (byte)crc_Low, (byte)crc_High };
                }
            }

            //高字节校验码表
            static private readonly byte[] aucCRCHi = {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40

            };
            //低字节校验码表
            static private readonly byte[] aucCRCLo = {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
            0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
            0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
            0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
            0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
            0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
            0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
            0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
            0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
            0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
            0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
            0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
            0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
            0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
            0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
            0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
            0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
            0x41, 0x81, 0x80, 0x40
        };



        }

        [Serializable]
        public class CustomException : ApplicationException
        {
            int type = 0;
            int value = 0;

            public int Value
            {
                get
                {
                    return value;
                }
            }

            public int Type
            {
                get
                {
                    return type;
                }
            }
            //构造函数1
            public CustomException()
                : base()
            {
            }
            //构造函数2
            public CustomException(string message)
                : base(message)
            {
            }
            //构造函数3
            public CustomException(string message, Exception inner)
                : base(message, inner)
            {
            }
            //构造函数4,添加自定义数据
            public CustomException(string message, int type, int value)
                : base(message)
            {
                this.type = type;
                this.value = value;
            }
            //序列化
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
                info.AddValue("Type", type);
                info.AddValue("Value", value);
            }

            //反序列化
            public CustomException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                type = info.GetInt32("Type");
                value = info.GetInt32("Value");
            }
            //重写message
            public override string Message
            {
                get
                {
                    string s = string.Format("Type:{0},Value:{1}", type, value);
                    return base.Message + Environment.NewLine + s;
                }
            }


        }
  
    }
}
