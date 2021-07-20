/*
 *  PLC接口实现
 *  要使用其他PLC只需要实例化相应的plc
 * 
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Text;
using ClsDeviceComm;
using ClsDeviceComm.Profinet.Melsec;
using ClsDeviceComm.LogNet;
using System.Windows.Forms;

namespace ClsDevComm
{
    public class CLS_PLCComm_TCP : CLS_PLC_TCP, IDisposable
    {
        #region///参数
        //PC及PLC ip地址和端口
        private string mLocalIP;
        private int mLocalPort;
        private string mRemoteIP;
        private int mRemotPort;
        //实例化对应PLC类型
        private MelsecMcNet melsec_net = null;
        string Path = Application.StartupPath.ToString() + "\\PLClog";
        #endregion

        public void Dispose()
        {
            try
            {
                Close();
            }
            catch (Exception)
            {

            }
        }

        #region///建立连接

        //返回值 -1:IP地址连接不上,-2:发送命令超时,-3:读返回值失败,1:成功
        public override short Link(string RemoteIP, int RemotePort, string LocalIP, int LocalPort)
        {
            mConnected = false;
            melsec_net = new MelsecMcNet();
            melsec_net.LogNet = new LogNetDateTime(Path, GenerateMode.ByEveryDay);
            melsec_net.LogNet.SetMessageDegree(ClsMessageDegree.DEBUG);//所有等级存储

            // 连接
            melsec_net.IpAddress = RemoteIP;
            //melsec_net.IpAddress = "127.0.0.1";
            melsec_net.Port = RemotePort;
            melsec_net.ConnectClose();
            try
            {
                OperateResult connect = melsec_net.ConnectServer();
                if (connect.IsSuccess)
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.INFO, mPLCNo.ToString(), "PLC连接成功！");
                    mConnected = true;
                    return 1;
                }
                else
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.INFO, mPLCNo.ToString(), "PLC连接失败！");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                melsec_net.LogNet.RecordMessage(ClsMessageDegree.FATAL, mPLCNo.ToString(), ex.Message);
                return -1;

            }


        }

        #endregion

        #region///关闭连接

        public override short Close()
        {
            try
            {
                mConnected = false;
                melsec_net.ConnectClose();

            }
            catch (Exception ex)
            {
                melsec_net.LogNet.RecordMessage(ClsMessageDegree.FATAL, mPLCNo.ToString(), ex.Message);
                return -1;
            }

            return 1;
        }

        #endregion

        #region///PLC参数读取

        //PLC读取位寄存器值
        public override void PLC_BitReg_Read(string mAddressStr, ref bool RecieveBit, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            short mBit = 0;         //位
            string[] mStr;          //拆分地址
            ushort recvValue = 0;
            char[] mCharRead = new char[16];
            try
            {
                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 2)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false || short.TryParse(mStr[1], out mBit) == false || (mBit < 0 || mBit > 15))
                    {
                        throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                    if (mMemoryArea == "D")
                    {
                        mStr = mAddressStr.Split('.');
                        OperateResult<short> read = melsec_net.ReadInt16(mStr[0]);
                        if (read.IsSuccess)
                        {
                            long mRecieveData = SignToUnsign(read.Content);
                            string mBinaryStr = Convert.ToString(mRecieveData, 2).PadLeft(16, '0');     //转换为2进制 
                            mCharRead = mBinaryStr.ToCharArray();        //读取到的值                         
                            RecieveBit = mCharRead[mCharRead.Length - 1 - mBit] == '1' ? true : false;
                        }
                        else
                        {
                            throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                        }

                    }
                    else if (mMemoryArea == "M")
                    {

                        OperateResult<bool> read = melsec_net.ReadBool(mAddressStr);

                        if (read.IsSuccess)
                        {
                            RecieveBit = read.Content;

                        }
                        else
                        {
                            throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                        }
                    }
                }
                else
                {
                    throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }



            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                    //LR.logRecord(mPLCNo, ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }
        }

        //PLC读取字寄存器值
        public override void PLC_WordReg_Read(string mAddressStr, ref ushort RecieveValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址
            try
            {
                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }
                OperateResult<ushort> read = melsec_net.ReadUInt16(mAddressStr);
                if (read.IsSuccess)
                {
                    RecieveValue = read.Content;

                }
                else
                {
                    throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，地址为：" + mAddressStr);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }
        }

        //PLC读取有符号字寄存器值
        public override void PLC_WordReg_Read(string mAddressStr, ref short RecieveValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址
            try
            {
                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }
                OperateResult<short> read = melsec_net.ReadInt16(mAddressStr);
                if (read.IsSuccess)
                {
                    RecieveValue = read.Content;

                }
                else
                {
                    throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }
        }

        //PLC读取多字寄存器值
        public override int PLC_MulWordReg_Read(string mAddressStr, int mCount, ref ushort[] RecieveValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址
            short mReNum = 0;

            try
            {
                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }

                //  short[] mRecieveValue = new short[mCount];
                //OperateResult<ushort[]> read = melsec_net.ReadUInt16(mAddressStr, (ushort)mCount);
                OperateResult<ushort[]> read = melsec_net.ReadUInt16(mAddressStr, (ushort)mCount);
                if (read.IsSuccess)
                {
                    RecieveValue = read.Content;
                    mReNum = 1;
                }
                else
                {
                    throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        //PLC读取多有符号字寄存器值
        public override int PLC_MulWordReg_Read(string mAddressStr, int mCount, ref short[] RecieveValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址

            short mReNum = 0;

            try
            {
                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }
                OperateResult<short[]> read = melsec_net.ReadInt16(mAddressStr, (ushort)mCount);
                if (read.IsSuccess)
                {
                    RecieveValue = read.Content;
                    mReNum = 1;
                }

                else
                {
                    throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        //PLC读取字寄存器值
        public override void PLC_StringReg_Read(string mAddressStr, ushort mCount, ref string RecieveValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址
            try
            {
                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[设备" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }
                OperateResult<string> read = melsec_net.ReadString(mAddressStr, mCount);
                if (read.IsSuccess)
                {
                    RecieveValue = read.Content;

                }
                else
                {
                    throw new Exception("读设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，地址为：" + mAddressStr);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }
        }
        
      
        #endregion

        #region///PLC参数写入

        //PLC写入位寄存器值
        public override int PLC_BitReg_Write(string mAddressStr, bool mVal, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            short mBit = 0;         //位
            string[] mStr;          //拆分地址

            short mReNum = 0;

            try
            {
                #region///写入PLC位寄存器值

                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 2)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false || short.TryParse(mStr[1], out mBit) == false || (mBit < 0 || mBit > 15))
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }

                mStr = mAddressStr.Split('.');
                short mRecieveArray = 0;
                OperateResult<short> read = melsec_net.ReadInt16(mStr[0]);
                if (read.IsSuccess)
                {
                    mRecieveArray = read.Content;
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.DEBUG, mStr[0], mRecieveArray.ToString());
                }
                else
                {
                    throw new Exception("写设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，读取地址为：" + mStr[0]);
                }
                long mRecieveData = SignToUnsign(mRecieveArray);
                string mBinaryStr = Convert.ToString(mRecieveData, 2);     //转换为2进制 
                char[] mCharRead = mBinaryStr.ToCharArray();        //读取到的值
                char[] mCharWrite = new char[16];                   //要写入的值
                for (int i = 0; i < 16; i++)
                {   //初始化
                    mCharWrite[i] = '0';
                }

                for (int i = 0; i < mCharRead.Length; i++)
                {   //录入读取到的值
                    mCharWrite[16 - mCharRead.Length + i] = mCharRead[i];
                }

                char mBitWrite; //0或1

                if (mVal == true)
                {
                    mBitWrite = '1';
                }
                else
                {
                    mBitWrite = '0';
                }
                if (mCharWrite[15 - mBit] == mBitWrite)
                {   //写入值与现有值一样
                    return 1;
                }
                mCharWrite[15 - mBit] = mBitWrite;   //要写入的位的值,其他位值不改变
                mBinaryStr = new string(mCharWrite);    //需要转换的2进制值
                int mInt = Convert.ToInt32(mBinaryStr, 2);  //从2进制转换为10进制
                short[] mShort = new short[1];
                mShort[0] = UnsignToSign(mInt);     //转换成有符号数
                OperateResult Result = melsec_net.Write(mStr[0], mShort[0]);
                if (Result.IsSuccess)
                {
                    mReNum = 1;
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.DEBUG, mAddressStr, mVal.ToString());
                }
                else
                {
                    throw new Exception("写设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，地址为：" + mAddressStr);
                }
                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        //PLC写入字寄存器值
        public override int PLC_WordReg_Write(string mAddressStr, ushort mValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址

            short mReNum = 0;
            short mCount = 1;
            short[] mValue1 = new short[mCount];

            try
            {
                #region///写入PLC字寄存器值

                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }

                //mValue1[0] = (short)mValue;
                OperateResult read = melsec_net.Write(mAddressStr, (short)mValue);
                if (read.IsSuccess)
                {
                    mReNum = 1;
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.DEBUG, mAddressStr, mValue.ToString());
                }
                else
                {
                    throw new Exception("写设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }
                //
                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        //PLC写入有符号字寄存器值
        public override int PLC_WordReg_Write(string mAddressStr, short mValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址

            short mReNum = 0;
            short mCount = 1;
            short[] mValue1 = new short[mCount];

            try
            {
                #region///写入PLC字寄存器值

                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }
                OperateResult read = melsec_net.Write(mAddressStr, mValue);
                if (read.IsSuccess)
                {
                    mReNum = 1;
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.DEBUG, mAddressStr, mValue.ToString());
                }
                else
                {
                    throw new Exception("写设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        //PLC写入多字寄存器值
        public override int PLC_MulWordReg_Write(string mAddressStr, int mCount, ushort[] mValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址

            short mReNum = 0;
            short[] mValue1 = new short[mCount];

            try
            {
                #region///写入PLC值

                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }

                OperateResult read = melsec_net.Write(mAddressStr, mValue);
                if (read.IsSuccess)
                {
                    mReNum = 1;
                    // melsec_net.LogNet.RecordMessage(ClsMessageDegree.DEBUG, mAddressStr, mVal.ToString());
                }
                else
                {
                    throw new Exception("写设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }
                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        //PLC写入多有符号字寄存器值
        public override int PLC_MulWordReg_Write(string mAddressStr, int mCount, short[] mValue, string mStateStr)
        {
            string mMemoryArea;     //数据区域D/M
            string mAddress;        //地址值
            int mChannel = 0;       //通道
            string[] mStr;          //拆分地址

            short mReNum = 0;

            try
            {
                #region///写入PLC值

                mMemoryArea = mAddressStr.Substring(0, 1);
                mAddress = mAddressStr.Substring(1, mAddressStr.Length - 1);
                mStr = mAddress.Split('.');

                if (mStr.Length == 1)
                {
                    if (int.TryParse(mStr[0], out mChannel) == false)
                    {
                        throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                    }
                }
                else
                {
                    throw new Exception("[" + mStateStr + "]PLC地址设定错误:" + mAddressStr);
                }
                OperateResult read = melsec_net.Write(mAddressStr, mValue);
                if (read.IsSuccess)
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.DEBUG, mAddressStr, mValue.ToString());
                    mReNum = 1;
                }
                else
                {
                    throw new Exception("写设备" + mStateStr + "值失败:" + read.ToMessageShowString() + "，返回值为：" + read.ErrorCode);
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    melsec_net.LogNet.RecordMessage(ClsMessageDegree.ERROR, mPLCNo.ToString(), ex.Message);
                }
                if (ex.Message.Contains("连接失败") || ex.Message.Contains("远程主机强迫关闭了一个现有的连接"))
                {
                    mConnected = false;
                }
            }

            return mReNum;
        }

        #endregion

        #region///有符号转无符号
        private long SignToUnsign(short Sign)
        {
            if (Sign >= 0)
            {
                return (long)Sign;
            }
            else
            {
                return (long)(65536 + Sign);
            }
        }
        #endregion

        #region///无符号转有符号
        private short UnsignToSign(int Unsign)
        {
            if (Unsign > 32767)
            {
                return (short)(Unsign - 65536);
            }
            else
            {
                return (short)Unsign;
            }
        }
        #endregion
    }
}
