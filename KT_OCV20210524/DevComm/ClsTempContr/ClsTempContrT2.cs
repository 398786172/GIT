using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Runtime.Serialization;

namespace OCV
{

    //切换控制
    public class ClsTempContrT2
    {
        //串口参数
        public SerialPort SPort;                                //对应串口 
        private  byte[] m_TempBuffer = new byte[10240];           //接收数据缓存
        private  int m_RecOffset = 0;                             //接收字节数
        private Thread ThreadReadData;     //周期扫描读取温控板数据回来


        /// <summary>
        /// 温度板状态
        /// </summary>
        private short wal = 0;
        public short Wal

        { get { return Wal; } }  

        /// <summary>
        /// 温度板状态
        /// </summary>
        private short tempboardstate = 0;
        public short Tempboardstate { get { return tempboardstate; } }

        /// <summary>
        /// 正极通道温度断线标志
        /// </summary>
        private bool[] anodetemplinecontact = new bool[38];
        public bool[] Anodetemplinecontact { get { return anodetemplinecontact; } }

        /// <summary>
        /// 负极通道温度断线标志
        /// </summary>
        private bool[] poletemplinecontact = new bool[38];
        public bool[] Poletemplinecontact { get { return poletemplinecontact; } }


        /// <summary>
        /// 正极通道温度
        /// </summary>
        private double[] anodetemperature = new double[38];
        public double[] Anodetemperature { get { return anodetemperature; } }

        /// <summary>
        /// 负极通道温度
        /// </summary>
        private double[] poletemperature = new double[38];
        public double[] Poletemperature { get { return poletemperature; } }


        Mutex CommMutex = new Mutex();
        //站号
        byte DevAddr;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="SP">端口</param>
        /// <param name="Addr">站号</param>
        //构造函数
        public ClsTempContrT2(SerialPort SP, byte Addr)
        {
            try
            {
                SPort = SP;
                SPort.Close();
                if (SPort.IsOpen == false)
                {
                    SPort.Open();
                }
                ThreadReadData = new Thread(new ThreadStart(ReadData));
                ThreadReadData.IsBackground = true;
                ThreadReadData.Start();
            }
            catch (Exception ex)
            {
                throw new Exception("温度板打开串口失败", ex);
            }
            this.SPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialGetData);
            DevAddr = Addr;
        }
        public ClsTempContrT2(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            SetPortProperty(port, BaudRate, parity, dataBits, stopBits);
            SPort.DataReceived += new SerialDataReceivedEventHandler(SerialGetData);
        }

        //public ClsTempContrT2(SerialPort SP, byte Addr)
        //{
        //    SPort = SP;
        //    SPort.DataReceived += new SerialDataReceivedEventHandler(SerialGetData);
        //}

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
                    SPort.ReadBufferSize = 300;
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
                    string s = parity.Trim().ToLower();
                    if ((s.CompareTo("无") == 0) || (s.CompareTo("none") == 0))
                    {
                        SPort.Parity = Parity.None;
                    }
                    else if ((s.CompareTo("奇校验") == 0) || (s.CompareTo("odd") == 0))
                    {
                        SPort.Parity = Parity.Odd;
                    }
                    else if ((s.CompareTo("偶校验") == 0) || (s.CompareTo("even") == 0))
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
            catch (Exception ex)
            {
                throw new Exception("串口初始化出错:" + ex.Message);
            }
        }
        // 委托函数
        private void SerialGetData(object sender, SerialDataReceivedEventArgs e)
        {
            int i;
            i = SPort.BytesToRead;
            SPort.Read(m_TempBuffer, m_RecOffset, i);
            m_RecOffset += i;
        }


        private void ReadData()
        {
            byte[] buf;

            while (true)
            {
                try
                {
                    this.ReadRegister(13, 76, out buf);
                    int position = 0;
                    if (buf !=null)
                    {
                        if (buf.Length == 152)
                        {
                            //wal = (short)((buf[position+1] + buf[position] * 256)); position += 2;

                            //// wal = BitConverter.ToInt16(buf, position); position += 2;

                            ////tempboardstate = BitConverter.ToInt16(buf, position); 

                            // tempboardstate = (short)((buf[position + 1] + buf[position] * 256)); position += 2;
                            // position += 8;

                            ////解析温度断线标志
                            //for (int i = 0; i < 3; i++)
                            //{
                            //   short val= (short)((buf[position + 1] + buf[position ] * 256)); position += 2;
                            //    bool[] flag = IntTo16BoolArray(val); 
                            //    for (int j = 0; j < flag.Length; j++)
                            //    {
                            //        int index = i * j;
                            //        if (index >= 38)
                            //        {
                            //            break;
                            //        }
                            //        anodetemplinecontact[index] = flag[j];
                            //    }
                            //}
                            //for (int i = 0; i < 3; i++)
                            //{
                            //    short val = (short)((buf[position + 1] + buf[position] * 256)); position += 2;
                            //    bool[] flag = IntTo16BoolArray(val);
                            //    for (int j = 0; j < flag.Length; j++)
                            //    {
                            //        int index = i * j;
                            //        if (index >= 38)
                            //        {
                            //            break;
                            //        }
                            //        poletemplinecontact[index] = flag[j];
                            //    }
                            //}

                            //position += 2;

                            //解析正极温度
                            for (int i = 0; i < 38; i++)
                            {

                                anodetemperature[i] = ((double)buf[position + 1] + (double)buf[position] * 256) / 100; position += 2;
                                // anodetemperature[i] = (BitConverter.ToDouble(buf, position)) / 100; ; position += 2;
                            }
                            //解析负极温度
                            for (int i = 0; i < 38; i++)
                            {
                                poletemperature[i] = ((double)buf[position + 1] + (double)buf[position] * 256) / 100; position += 2;
                            }
                        }
                    }
                    
                }
                 catch (Exception  ex )
                {

                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// 整型转16位布尔数组
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        private bool[] IntTo16BoolArray(int inputNum)
        {
            bool[] boolArray = new bool[16];
            for (int i = 0; i < boolArray.Length; i++)
            {
                if (inputNum % 2 == 1)
                    boolArray[i] = true;
                else
                    boolArray[i] = false;
                inputNum = inputNum / 2;
            }
            return boolArray;
        }

        //public void GetTemp(out double[] val)
        //{
        //    byte[] TEMPval = new byte[176];
        //    val = new double[76];
        //    try
        //    {
        //        this.ReadRegister(0, 88, out TEMPval);
        //        for (int i = 0; i < 76; i++)
        //        {
        //            val[i] = ((double)TEMPval[i * 2] + (double)TEMPval[i * 2 + 1] * 256) / 10;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //写寄存器
        public void WriteSingleRegister(ushort Addr, short val)
        {
            byte[] arrAddr = BitConverter.GetBytes(Addr);
            byte[] arrVal = BitConverter.GetBytes(val);
            byte[] sendBuf = new byte[9];
            int ErrId = 0;
            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = 0x10;
                sendBuf[2] = arrAddr[1];       //地址
                sendBuf[3] = arrAddr[0];
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x01;
                sendBuf[6] = 0x02;

                sendBuf[7] = arrVal[1];        //数值          
                sendBuf[8] = arrVal[0];

                CommMutex.WaitOne();
                ErrId = ModBus_Write(ref sendBuf);

                if (ErrId != 0) //throw new CustomException("通讯出错", 0, ErrId);

                CommMutex.ReleaseMutex();
            }
            catch (Exception ex)
            {
                CommMutex.ReleaseMutex();
                throw ex;
            }
        }
        //读寄存器
        public void ReadRegister(ushort Addr, int RegNum, out byte[] val)
        {
            byte[] arrAddr = BitConverter.GetBytes(Addr);
            byte[] arrNum = BitConverter.GetBytes(RegNum);
            //Array.Reverse();

            byte[] sendBuf = new byte[6];
            int ErrId = 0;


            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = 0x03;
                sendBuf[2] = arrAddr[1];       //地址
                sendBuf[3] = arrAddr[0];
                sendBuf[4] = arrNum[1];         //寄存器数量
                sendBuf[5] = arrNum[0];

                CommMutex.WaitOne();
                ErrId = ModBus_Read(ref sendBuf, out val);
                //0 3 0 13  0 76  212 45
                if (ErrId != 0) //throw new CustomException("通讯出错", 0, ErrId);

                CommMutex.ReleaseMutex();


            }
            catch (Exception ex)
            {

                CommMutex.ReleaseMutex();

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

            receiveBuf = null;
        tryTimes:
            try
            {
                #region //处理

                if (SPort.IsOpen == true)
                {
                    sendLen = (byte)(sendBuf.Length + 2);  //发送总长度
                    arrSendBuf = new byte[sendLen];     //发送的数组
                    sendBuf.CopyTo(arrSendBuf, 0);
                    CRCNum = ClsCRC16.CountCRC(sendBuf, true);  //CRC计算
                    CRCNum.CopyTo(arrSendBuf, sendBuf.Length);
                    m_RecOffset = 0;                   //复位接收偏移量
                    SPort.Write(arrSendBuf, 0, sendLen); //写串口
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
                    Thread.Sleep(10);
                    //Delay(10);
                    i = m_RecOffset;
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
                    if (m_TempBuffer[i] != sendBuf[i])
                    {
                        ErrId = 2;               //接收字节出错
                        throw new Exception();
                    }
                }
                //确认接收字节数
                recLengh = m_TempBuffer[2];
                //接收剩余数据
                int tempNum = recLengh + 2;                   //加校验码2字节
                //接收所有数据
                ErrId = 1;
                delayTimes = 3;
                do
                {
                    Thread.Sleep(10);
                    //Delay(100);
                    i = m_RecOffset - 3;
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
                CRCResult = ClsCRC16.CountCRC(m_TempBuffer, 0, recLengh + 3, true);
                if ((CRCResult[0] != m_TempBuffer[recLengh + 3]) || (CRCResult[1] != m_TempBuffer[recLengh + 4]))
                {
                    ErrId = 5;                  //CRC校验出错
                    throw new Exception();
                }
                //获取接收字节
                recBuf = new byte[recLengh];
                for (i = 0; i < recLengh; i++)
                {
                    recBuf[i] = m_TempBuffer[i + 3];
                }

                receiveBuf = recBuf;

                #endregion

            }
            catch
            {
                switch (ErrId)
                {
                    case 1: tryTimes--;
                        if (tryTimes > 0)
                        {
                            Thread.Sleep(10);
                            goto tryTimes;   //超时错误
                        }
                        else
                        {
                            return ErrId;
                            //throw;
                        }
                    case 2: tryTimes--;
                        if (tryTimes > 0)
                        {
                            Thread.Sleep(10);
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
                if (SPort.IsOpen == true)
                {
                    sendLen = (byte)(sendBuf.Length + 2);  //发送总长度
                    arrSendBuf = new byte[sendLen];     //发送的数组
                    sendBuf.CopyTo(arrSendBuf, 0);
                    CRCNum = ClsCRC16.CountCRC(sendBuf, true);  //CRC计算
                    CRCNum.CopyTo(arrSendBuf, sendBuf.Length);
                    m_RecOffset = 0;                   //复位接收偏移量
                    SPort.Write(arrSendBuf, 0, sendLen); //写串口
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
                    i = m_RecOffset;
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
                for (i = 0; i < 2; i++)
                {
                    if (m_TempBuffer[i] != sendBuf[i])
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
                    case 1: tryTimes--;
                        if (tryTimes > 0)
                        {
                            Thread.Sleep(10);
                            goto tryTimes;                  //超时错误
                        }
                        else
                        {
                            return ErrId;
                        }
                    case 2: tryTimes--;
                        if (tryTimes > 0)
                        {
                            Thread.Sleep(10);
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
    }
  
    //CRC计算类
    class ClsCRC16
    {
        //双表(高低字节)查询
        public static byte[] CountCRC(byte[] btArray, int start, int lengh, bool IsHighBefore)
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
            if (IsHighBefore == true)
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

}
