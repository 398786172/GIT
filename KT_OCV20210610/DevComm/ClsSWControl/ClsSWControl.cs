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
    public class ClsSWControl
    {
        //串口参数
        public SerialPort SPort;                                //对应串口 
        static byte[] m_TempBuffer = new byte[10240];           //接收数据缓存
        static int m_RecOffset = 0;                             //接收字节数

        //站号
        byte DevAddr;
        int DevSWtype;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SP">端口</param>
        /// <param name="Addr">站号</param>
        /// <param name="SWtype">切换板类型  1 宝龙版本  2.C42版本</param>
        //构造函数
        public ClsSWControl(SerialPort SP, byte Addr,int SWtype)
        {
            try
            {
               
                SPort = SP;
                SPort.Close();
                if (SPort.IsOpen == false)
                {
                    SPort.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("打开通道切换系统串口失败", ex);
            }
            this.SPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialGetData);
            DevAddr = Addr;
            DevSWtype = SWtype;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SP">端口</param>
        /// <param name="Addr">站号</param>
        /// <param name="SWtype">切换板类型  1 宝龙版本  2.C42版本</param>
        //构造函数
        public ClsSWControl(string Switch_Port, byte Addr, int SWtype)
        {
            try
            {
                //切换控制初始化
                try
                {
                    SPort = new SerialPort(Switch_Port, 115200, Parity.None, 8, StopBits.One);
                    //SwitchCom.ReadTimeout = 500;    //设置超时读取时间 
                    //SwitchCom.DtrEnable = true;

                    SPort.Close();
                    if (SPort.IsOpen == false)
                    {
                        SPort.Open();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("万用表串口初始化失败");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("打开通道切换系统串口失败", ex);
            }
            this.SPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialGetData);
            DevAddr = Addr;
            DevSWtype = SWtype;
        }
        // 委托函数
        private void SerialGetData(object sender, SerialDataReceivedEventArgs e)
        {
            int i;
            i = SPort.BytesToRead;
            SPort.Read(m_TempBuffer, m_RecOffset, i);
            m_RecOffset += i;
        }

        /// <summary>
        /// 切换测量电压  
        /// </summary>
        /// <param name="POSConnType">正极连接类型:  正极对负极->1, 壳体对负极->2</param>
        /// <param name="Channel">通道号</param>
        public void ChannelVoltSwitch(int POSConnType, int Channel)
        {
            try
            {
                switch (DevSWtype)
                {
                    case 1:
                        this.ChanndlVoltShellSwitchContr(POSConnType, Channel);
                        break;
                    case 2:
                        this.ChannelVoltSwitchContr(POSConnType, Channel);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region 坑梓C42改造版本切换板函数

        /// <summary>
        /// 切换测量电压
        /// </summary>
        /// <param name="POSConnType">正极连接类型:  正极对负极->1, 壳体对负极->2</param>
        /// <param name="Channel">通道号</param>
        public void ChannelVoltSwitchContr(int POSConnType, int Channel)
        {
            byte[] sendBuf = new byte[15];
            int ErrId = 0;

            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_SW1_DMM_Volt_CMD;
                sendBuf[3] = ClsSWDef.AddrL_SW1_DMM_Volt_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x03;
                sendBuf[6] = 0x06;

                sendBuf[7] = 0;     //触发标识
                sendBuf[8] = 1;

                sendBuf[9] = 0;      //正极连接类型
                sendBuf[10] = (byte)POSConnType;

                sendBuf[11] = (byte)(Channel >> 8);     //通道号
                sendBuf[12] = (byte)Channel;

                ErrId = ModBus_Write(ref sendBuf);
                if (ErrId != 0) throw new CustomException("切换箱通讯出错", 0, ErrId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 切换测量内阻命令
        /// </summary>
        /// <param name="IRDevType">内阻仪类型: 内阻仪BT3 -> 1, 内阻仪BT4 -> 2</param>
        /// <param name="Channel">通道号</param>
        public void ChannelAcirSwitchContr(int IRDevType, int Channel)
        {
            byte[] sendBuf = new byte[15];
            int ErrId = 0;

            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_SW2_IR_CMD;
                sendBuf[3] = ClsSWDef.AddrL_SW2_IR_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x03;
                sendBuf[6] = 0x06;

                sendBuf[7] = 0;
                sendBuf[8] = 1;

                sendBuf[9] = 0;
                sendBuf[10] = (byte)IRDevType;

                sendBuf[11] = (byte)(Channel >> 8);
                sendBuf[12] = (byte)Channel;

                ErrId = ModBus_Write(ref sendBuf);
                if (ErrId != 0) throw new CustomException("切换箱通讯出错", 0, ErrId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 同步切换测量命令
        /// </summary>
        /// <param name="TestType">测量类型:  (A区正极对负极电压, B区测内阻) , (A区测内阻, B区测正极对负极电压)</param>
        /// <param name="Channel_A">A区通道号</param>
        /// <param name="Channel_B">B区通道号  值范围定义:(A区最后通道号 + 1 ~ 托盘最后的通道号) </param>
        public void ChannelSYNVoltIRSwitchContr(int TestType, int Channel_A, int Channel_B)
        {
            byte[] sendBuf = new byte[15];
            int ErrId = 0;

            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_SW3_DOUBLE_DMM_Volt_CMD;
                sendBuf[3] = ClsSWDef.AddrL_SW3_DOUBLE_DMM_Volt_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x04;
                sendBuf[6] = 0x08;

                sendBuf[7] = 0;                  //命令触发标识
                sendBuf[8] = 1;

                sendBuf[9] = 0;                  //测量类型
                sendBuf[10] = (byte)TestType;

                sendBuf[11] = 0;                 //A区通道号
                sendBuf[12] = (byte)Channel_A;

                sendBuf[13] = 0;                 //B区通道号 
                sendBuf[14] = (byte)Channel_B;

                ErrId = ModBus_Write(ref sendBuf);
                if (ErrId != 0) throw new CustomException("切换箱通讯出错", 0, ErrId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion

        #region 适用于宝龙切换板函数

        /// <summary>
        /// 函数: 切换控制命令  适用于宝龙切换板
        /// </summary>
        /// <param name="SWType">切换类型:  单->1  双->2</param>
        /// <param name="TestType">   
        /// 测试类型: 
        //  ChNum通道测电压, ChNum+16通道测内阻,->1
        //  ChNum通道测内阻, ChNum+16通道测电压 -> 2
        //  不测试 -> 0 
        ///</param>
        /// <param name="Channel">通道号: 1~ X , 按照板顺序</param>
        public void ChannelVoltIRSwitchContr(int SWType, int TestType, int Channel)
        {
            byte[] sendBuf = new byte[15];
            int ErrId = 0;
            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_SW1_CMD;
                sendBuf[3] = ClsSWDef.AddrL_SW1_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x04;
                sendBuf[6] = 0x08;

                sendBuf[7] = 0;                 //命令触发标识
                sendBuf[8] = 1;
                sendBuf[9] = 0;                 //切换类型: 单双
                sendBuf[10] = (byte)SWType;
                sendBuf[11] = 0;                 //测试类型: 1V2R or 1R2V
                sendBuf[12] = (byte)TestType;
                sendBuf[13] = (byte)(Channel >> 8);    //通道号    
                sendBuf[14] = (byte)Channel;

                ErrId = ModBus_Write(ref sendBuf);

                if (ErrId != 0) throw new CustomException("切换箱通讯出错", 0, ErrId);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
            
        /// <summary>
        /// 函数: 切换控制命令,正负极(单测OCV)
        /// </summary>
        /// <param name="Channel"> 1~256 -> 相应的通道, 0 -> 断开所有通道 </param>
        public void ChanndlVoltSwitchContr(int Channel)
        {
            byte[] sendBuf = new byte[11];
            int ErrId = 0;
            //bool flag = false;
            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_SW3_CMD;
                sendBuf[3] = ClsSWDef.AddrL_SW3_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x02;
                sendBuf[6] = 0x04;

                sendBuf[7] = 0;
                sendBuf[8] = 1;                         //命令触发标识

                sendBuf[9] = (byte)(Channel >> 8);      //通道号
                sendBuf[10] = (byte)Channel;

                ErrId = ModBus_Write(ref sendBuf);

                if (ErrId != 0) throw new CustomException("切换系统通讯出错", 0, ErrId);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 函数: 切换控制命令,正负极和壳体
        /// </summary>
        /// <param name="SWType">正负极->1， 壳体->2</param>
        /// <param name="Channel">通道号</param>
        public void ChanndlVoltShellSwitchContr(int SWType, int Channel)
        {
            byte[] sendBuf = new byte[15];
            int ErrId = 0;

            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_SW2_CMD;
                sendBuf[3] = ClsSWDef.AddrL_SW2_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x03;
                sendBuf[6] = 0x06;

                sendBuf[7] = 0;
                sendBuf[8] = 1;

                sendBuf[9] = 0;
                sendBuf[10] = (byte)SWType;

                sendBuf[11] = (byte)(Channel >> 8);
                sendBuf[12] = (byte)Channel;

                ErrId = ModBus_Write(ref sendBuf);
                if (ErrId != 0) throw new CustomException("切换箱通讯出错", 0, ErrId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    
        /// <summary>
        /// 函数: 切换正极接入
        /// </summary>
        /// <param name="Region">区域: A单元,B单元</param>
        /// <param name="Mode">开路->0 ,正极信号接入-> 1 </param>
        public void PosProbeSwitchContr(int Region, int Mode)
        {
            byte[] sendBuf = new byte[13];
            int ErrId = 0;
            try
            {
                sendBuf[0] = DevAddr;
                sendBuf[1] = ClsSWDef.FUNC_WRITE_HOLDINGREGS_MULTI;
                sendBuf[2] = ClsSWDef.AddrH_PosIn_CMD;
                sendBuf[3] = ClsSWDef.AddrL_PosIn_CMD;
                sendBuf[4] = 0x00;
                sendBuf[5] = 0x03;
                sendBuf[6] = 0x06;

                sendBuf[7] = 0;                  //命令触发标识
                sendBuf[8] = 1;

                sendBuf[9] = 0;                  //区域A/B
                sendBuf[10] = (byte)Region;

                sendBuf[11] = 0;                 //开路->0 ,正极信号接入-> 1 
                sendBuf[12] = (byte)Mode;

                ErrId = ModBus_Write(ref sendBuf);
                if (ErrId != 0) throw new CustomException("切换箱通讯出错", 0, ErrId);
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
                    CRCNum = ClsCRC.CountCRC(sendBuf, true);  //CRC计算
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
                CRCResult = ClsCRC.CountCRC(m_TempBuffer, 0, recLengh + 3, true);
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
                    CRCNum = ClsCRC.CountCRC(sendBuf, true);  //CRC计算
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
                for (i = 0; i < 6; i++)
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
        #endregion
    }



    //CRC计算类
    class ClsCRC
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
