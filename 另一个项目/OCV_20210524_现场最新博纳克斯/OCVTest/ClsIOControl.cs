﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Runtime.Serialization;

namespace OCV
{

    //IO控制
    public class ClsIOControl
    {
        //串口参数
        public SerialPort SPort;                                //对应串口 
        static byte[] m_TempBuffer = new byte[10240];           //接收数据缓存
        static int m_RecOffset = 0;                             //接收字节数

        Mutex CommMutex = new Mutex();

        //站号
        byte DevAddr;

        //构造函数
        public ClsIOControl(SerialPort SP, byte Addr)
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
                throw new Exception("打开串口失败", ex);
            }
            this.SPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialGetData);
            DevAddr = Addr;
        }

        // 委托函数
        private void SerialGetData(object sender, SerialDataReceivedEventArgs e)
        {
            int i;
            i = SPort.BytesToRead;
            SPort.Read(m_TempBuffer, m_RecOffset, i);
            m_RecOffset += i;
        }


        //接口函数-------------------------------------------------

        //输出函数---------------------------------
        //联机确认(心跳)
        public void Set_HeartBeat(uint val)
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_HeartBeat, (short)val);
        }

        //报警复位请求
        public void Set_ReqClearAlarm(uint val)
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_ReqClearAlarm, (short)val);
        }

        //设备复位请求
        public void Set_ReqReset(uint val)
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_ReqReset, (short)val);
        }

        //针床压合请求
        public void Set_ReqPressPB(uint val)
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_ReqPressPB, (short)val);
        }

        //针床打开请求
        public void Set_ReqOpenPB(uint val)
        {
            //WriteSingleRegister(ClsIODef.Addr_HOST_ReqOpenPB, (short)val);
        }

        /// <summary>
        /// 压合电池类型要求
        /// </summary>
        /// <param name="val">1: 18650 , 2: 21700 </param>
        public void Set_ReqBattType(uint val)
        {
            //WriteSingleRegister(ClsIODef.Addr_HOST_ReqBattType, (short)val);
        }

        /// <summary>
        /// 针床压合次数
        /// </summary>
        /// <param name="val"> 1~3</param>
        public void Set_PressTimes(uint val)
        {
            //WriteSingleRegister(ClsIODef.Addr_HOST_PressTimes, (short)val);
        }

        //进入调试状态
        public void Set_DebugIn()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_Debug, 1); //wjp,2020/7/7  启用
        }

        //离开调试状态
        public void Set_DebugOut()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_Debug, 0); //wjp,2020/7/7 启用
        }

        //针床打开 地址47
        public void Set_TestFinish()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TestFinish, 1);
            //WriteSingleRegister(ClsIODef.Addr_HOST_ReqPressPB, 0);
        }

        public void Set_ResetTestFinish()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TestFinish, 0);
            //WriteSingleRegister(ClsIODef.Addr_HOST_ReqPressPB, 0);
        }


        //针床压合  地址44
        public void Set_DebugPressPB()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_ReqPressPB, 1);
        }

        public void Set_DebugUnPressPB()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_ReqPressPB, 0);
        }

        //探针打开 地址53 wjp,2020/7/7
        public void Set_PBOpen()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_PBPress, 0);
        }

        //探针压合  地址53  wjp,2020/7/7
        public void Set_PBPress()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_PBPress, 1);
        }

        //托盘夹紧  wjp,2020/7/7
        public void Set_TrayClose()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TrayClose, 1);
        }
        //托盘夹紧  wjp,2020/7/7
        public void Set_TrayOpen()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TrayClose, 0);
        }
        //托盘上升  wjp,2020/7/7
        public void Set_TrayUp()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TrayDown, 0);
        }
        //托盘下降  wjp,2020/7/7
        public void Set_TrayDown()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TrayDown, 1);
        }
        //托盘推入  wjp,2020/7/7
        public void Set_TrayIn()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TrayPush, 1);
        }
        //托盘取出  wjp,2020/7/7
        public void Set_TrayOut()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TrayPush, 0);
        }

        //绿灯开
        public void Set_DebugGreenLight_On()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_DebugGreenLight, 1);
        }

        //绿灯闭
        public void Set_DebugGreenLight_Off()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_DebugGreenLight, 0);
        }

        //红灯开
        public void Set_DebugRedLight_On()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_DebugRedLight, 1);
        }

        //红灯关
        public void Set_DebugRedLight_Off()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_DebugRedLight, 0);
        }

        //灯塔红灯开  wjp,2020/7/7
        public void Set_TowerRedLight_On()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TowerRedLight, 1);
        }

        //灯塔红灯关 wjp,2020/7/7
        public void Set_TowerRedLight_Off()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TowerRedLight, 0);
        }

        //灯塔橙灯开  wjp,2020/7/7
        public void Set_TowerOrangeLight_On()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TowerOrangeLight, 1);
        }

        //灯塔橙灯关 wjp,2020/7/7
        public void Set_TowerOrangeLight_Off()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TowerOrangeLight, 0);
        }


        //灯塔绿灯开  wjp,2020/7/7
        public void Set_TowerGreenLight_On()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TowerGreenLight, 1);
        }

        //灯塔绿灯关 wjp,2020/7/7
        public void Set_TowerGreenLight_Off()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_TowerGreenLight, 0);
        }
        //
        //蜂鸣器开  wjp,2020/7/7
        public void Set_TowerBusser_On()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_Busser, 1);
        }

        //蜂鸣器关 wjp,2020/7/7
        public void Set_TowerBusser_Off()
        {
            WriteSingleRegister(ClsIODef.Addr_HOST_Busser, 0);
        }

        //输入函数----------------------------------
        public UInt16 Get_M_DEVNo()
        {
            byte[] arrVal;
            ////ReadRegister(ClsIODef.Addr_MECHA_DEVNo, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_MECHA_IsWork()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_IsWork, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }




        public UInt16 Get_M_HeartBeat()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_HeartBeat, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }


        public UInt16 Get_M_DevState()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_DevState, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }

        public UInt16 Get_M_Alarm1()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_Alarm, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }

        public UInt16 Get_M_TrayIn()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_TrayIn, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }

        public UInt16 Get_M_PBPress()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_PBPress, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_MECHA_PressPB()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_PressPB, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);

        }


        public UInt16 Get_M_PressBattType()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_PressBattType, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);

            return 0;
        }

        public UInt16 Get_M_EchoClearAlarm()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_EchoClearAlarm, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_EchoReset()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_EchoReset, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_EchoPressPB()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_EchoPressPB, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_MECHA_TrayClose()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_TrayClose, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }


        public UInt16 Get_IOResetCompleted()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_TestFinish, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);

        }



        public UInt16 Get_M_EchoOpenPB()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_EchoOpenPB, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_EchoDebug()
        {
            byte[] arrVal;
            //ReadRegister(ClsIODef.Addr_MECHA_EchoDebug, 1, out arrVal);
            //return (UInt16)((arrVal[0] << 8) + arrVal[1]);
            return 0;
        }

        public UInt16 Get_M_Input1()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_Input1, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }

        public UInt16 Get_M_Input2()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_Input2, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }

        public UInt16 Get_M_Output1()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_Output1, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }

        public UInt16 Get_M_Output2()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_Output2, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }
        //wjp,2020/7/8 调试状态
        public UInt16 Get_Mode_Output()
        {
            byte[] arrVal;
            ReadRegister(ClsIODef.Addr_MECHA_Debug, 1, out arrVal);
            return (UInt16)((arrVal[0] << 8) + arrVal[1]);
        }



        /// <summary>
        /// 函数: 切换控制命令
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

                CommMutex.WaitOne();
                ErrId = ModBus_Write(ref sendBuf);

                if (ErrId != 0) throw new CustomException("切换系统通讯出错", 0, ErrId);

                CommMutex.ReleaseMutex();


            }
            catch (Exception ex)
            {
                CommMutex.ReleaseMutex();
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
            byte[] sendBuf = new byte[13];
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

                CommMutex.WaitOne();
                ErrId = ModBus_Write(ref sendBuf);

                if (ErrId != 0) throw new CustomException("切换系统通讯出错", 0, ErrId);

                CommMutex.ReleaseMutex();


            }
            catch (Exception ex)
            {

                CommMutex.ReleaseMutex();

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
            //bool flag = false;
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

                CommMutex.WaitOne();
                ErrId = ModBus_Write(ref sendBuf);

                if (ErrId != 0) throw new CustomException("切换系统通讯出错", 0, ErrId);

                CommMutex.ReleaseMutex();


            }
            catch (Exception ex)
            {

                CommMutex.ReleaseMutex();

                throw ex;
            }

        }

        /// <summary>
        /// 函数: 切换控制命令,正负极(单测OCV)
        /// </summary>
        /// <param name="Channel"> 1~256 -> 相应的通道, 0 -> 断开所有通道 </param>
        public void ChanndlVoltSwitchContr(int Channel, out int eID)
        {
            byte[] sendBuf = new byte[11];
            int ErrId = -1;
            eID = ErrId;
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

                CommMutex.WaitOne();
                ErrId = ModBus_Write(ref sendBuf);
                eID = ErrId;
                if (ErrId != 0) throw new CustomException("切换系统通讯出错", 0, ErrId);

                CommMutex.ReleaseMutex();


            }
            catch (Exception ex)
            {
                CommMutex.ReleaseMutex();
                //throw ex;
            }

        }


        //驱动函数--------------------------------------------------

        //写寄存器
        public void WriteSingleRegister(ushort Addr, short val)
        {
            byte[] arrAddr = BitConverter.GetBytes(Addr);
            byte[] arrVal = BitConverter.GetBytes(val);
            //Array.Reverse();

            byte[] sendBuf = new byte[9];
            int ErrId = 0;

            int errTimes = 0;
            //bool flag = false;
            while (true)
            {
                try
                {
                    sendBuf[0] = DevAddr;
                    sendBuf[1] = ClsIODef.FUNC_WRITE_HOLDINGREGS_MULTI;
                    sendBuf[2] = arrAddr[1];       //地址
                    sendBuf[3] = arrAddr[0];
                    sendBuf[4] = 0x00;
                    sendBuf[5] = 0x01;
                    sendBuf[6] = 0x02;
                    sendBuf[7] = arrVal[1];        //数值          
                    sendBuf[8] = arrVal[0];

                    //MT.WaitOne();
                    CommMutex.WaitOne();
                    ErrId = ModBus_Write(ref sendBuf);
                    //MT.ReleaseMutex();

                    if (ErrId != 0)
                    {
                        throw new CustomException("通讯出错", 0, ErrId);
                    }
                    CommMutex.ReleaseMutex();
                    if (ErrId == 0)
                    {
                        break;
                    }


                }
                catch (Exception ex)
                {

                    CommMutex.ReleaseMutex();
                    if (errTimes >= 3)
                    {
                        LogHelper.LogErr($"[{DateTime.Now}]-IO通信出错,错误Id[{ErrId}]", "IOErroLog");
                        throw ex;
                    }
                    else
                    {
                        errTimes++;
                    }
                    System.Threading.Thread.Sleep(1 * 1000);
                }
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
            int errTimes = 0;
            while (true)
            {
                try
                {
                    sendBuf[0] = DevAddr;
                    sendBuf[1] = ClsIODef.FUNC_READ_HOLDINGREG;
                    sendBuf[2] = arrAddr[1];       //地址
                    sendBuf[3] = arrAddr[0];
                    sendBuf[4] = arrNum[1];         //寄存器数量
                    sendBuf[5] = arrNum[0];
                    CommMutex.WaitOne();
                    //MT.WaitOne();
                    ErrId = ModBus_Read(ref sendBuf, out val);
                    //MT.ReleaseMutex(); 
                    if (ErrId != 0)
                    {
                        throw new CustomException("通讯出错", 0, ErrId);
                    }
                    CommMutex.ReleaseMutex();
                    if (ErrId == 0)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    CommMutex.ReleaseMutex();
                    if (errTimes >= 3)
                    {
                        LogHelper.LogErr($"[{DateTime.Now}]-IO通信出错,错误Id[{ErrId}]", "IOErroLog");
                        throw ex;
                    }
                    else
                    {
                        errTimes++;
                    }
                    System.Threading.Thread.Sleep(1 * 1000);
                }
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
                    CRCNum = CountCRC(sendBuf, true);  //CRC计算
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
                    Thread.Sleep(30);
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
                CRCResult = CountCRC(m_TempBuffer, 0, recLengh + 3, true);
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
                    case 1:
                        tryTimes--;
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
                    case 2:
                        tryTimes--;
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
                    CRCNum = CountCRC(sendBuf, true);  //CRC计算
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
                    Thread.Sleep(40);
                    i = SPort.BytesToRead;
                    SPort.Read(m_TempBuffer, m_RecOffset, i);
                    //m_RecOffset += i;

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
                    case 1:
                        tryTimes--;
                        if (tryTimes > 0)
                        {
                            Thread.Sleep(5);
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

        #endregion ModBus函数
    }

    ////CRC计算类
    //class ClsCRC
    //{
    //    //双表(高低字节)查询
    //    public static byte[] CountCRC(byte[] btArray, int start, int lengh, bool IsLSB)
    //    {
    //        int index;
    //        int crc_Low = 0xFF;
    //        int crc_High = 0xFF;
    //        for (int i = start; i < lengh; i++)
    //        {
    //            index = crc_High ^ (char)btArray[i];
    //            crc_High = crc_Low ^ aucCRCHi[index];
    //            crc_Low = (byte)aucCRCLo[index];
    //        }
    //        if (IsLSB == true)
    //        {
    //            return new byte[2] { (byte)crc_High, (byte)crc_Low };
    //        }
    //        else
    //        {
    //            return new byte[2] { (byte)crc_Low, (byte)crc_High };
    //        }
    //    }

    //    //双表(高低字节)查询2
    //    public static byte[] CountCRC(byte[] btArray, bool IsHighBefore)
    //    {
    //        int index;
    //        int crc_Low = 0xFF;
    //        int crc_High = 0xFF;
    //        for (int i = 0; i < btArray.Length; i++)
    //        {
    //            index = crc_High ^ (char)btArray[i];
    //            crc_High = crc_Low ^ aucCRCHi[index];
    //            crc_Low = (byte)aucCRCLo[index];
    //        }
    //        if (IsHighBefore == true)
    //        {
    //            return new byte[2] { (byte)crc_High, (byte)crc_Low };
    //        }
    //        else
    //        {
    //            return new byte[2] { (byte)crc_Low, (byte)crc_High };
    //        }
    //    }

    //    //高字节校验码表
    //    static private readonly byte[] aucCRCHi = {
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    //        0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    //        0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    //        0x00, 0xC1, 0x81, 0x40

    //        };
    //    //低字节校验码表
    //    static private readonly byte[] aucCRCLo = {
    //        0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
    //        0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
    //        0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
    //        0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
    //        0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
    //        0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
    //        0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
    //        0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 
    //        0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
    //        0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
    //        0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
    //        0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
    //        0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 
    //        0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
    //        0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
    //        0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
    //        0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
    //        0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
    //        0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
    //        0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
    //        0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
    //        0x41, 0x81, 0x80, 0x40
    //    };



    //}


}
