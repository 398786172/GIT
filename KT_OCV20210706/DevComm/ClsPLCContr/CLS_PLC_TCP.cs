
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace  ClsDevComm

{
    public class CLS_PLC_TCP
    {
        #region//保护变量

        

        //内部连接成功标识
        protected bool mConnected = false;

        //PLC序号
        protected int mPLCNo = 0;

        #endregion

        #region//公共变量

        public bool PLC_LinkFlag = false;
        public bool PLC_ReadFlag = false;
        public bool PLC_WriteFlag = false;

        #endregion

        #region//公共属性

        //连接成功标识
        public bool Connected
        {
            get { return mConnected; }
        }

        //PLC序号
        public int PLCNo
        {
            set { mPLCNo = value; }
            get { return mPLCNo; }
        }

        #endregion

        //构造函数
        public CLS_PLC_TCP()
        {

        }

        #region///建立连接

        //返回值 -1:IP地址连接不上,-2:发送命令超时,-3:读返回值失败,1:成功
        public virtual short Link(string RemoteIP, int RemotePort)
        {
            return 0;
        }

        //返回值 -1:IP地址连接不上,-2:发送命令超时,-3:读返回值失败,1:成功
        public virtual short Link(string RemoteIP, int RemotePort, string LocalIP)
        {
            return 0;
        }

        //返回值 -1:IP地址连接不上,-2:发送命令超时,-3:读返回值失败,1:成功
        public virtual short Link(string RemoteIP, int RemotePort, string LocalIP, int LocalPort = 0)
        {
            return 0;
        }

        #endregion

        #region///关闭连接

        public virtual short Close()
        {
            return 0;
        }

        #endregion

        #region///PLC参数读取

        //PLC读取位寄存器值
        public virtual void PLC_BitReg_Read(string mAddressStr, ref bool RecieveBit, string mStateStr = "")
        {
            
        }

        //PLC读取字寄存器值
        public virtual void PLC_WordReg_Read(string mAddressStr, ref ushort RecieveValue, string mStateStr = "")
        {
            
        }

        //PLC读取有符号字寄存器值
        public virtual void PLC_WordReg_Read(string mAddressStr, ref short RecieveValue, string mStateStr = "")
        {

        }

        //PLC读取多字寄存器值
        public virtual int PLC_MulWordReg_Read(string mAddressStr, int mCount, ref ushort[] RecieveValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC读取多有符号字寄存器值
        public virtual int PLC_MulWordReg_Read(string mAddressStr, int mCount, ref short[] RecieveValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC读取双字寄存器值
        public virtual void PLC_DoubleWordReg_Read(string mAddressStr, ref uint RecieveValue, string mStateStr = "")
        {

        }

        //PLC读取多双字寄存器值
        public virtual int PLC_MulDoubleWordReg_Read(string mAddressStr, int mCount, ref uint[] RecieveValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC读取多有符号双字寄存器值
        public virtual int PLC_MulSDoubleWordReg_Read(string mAddressStr, int mCount, ref int[] RecieveValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC读字符串
        public virtual void PLC_StringReg_Read(string mAddressStr, ushort mCount, ref string RecieveValue, string mStateStr = "")
        {
       
        }

        #endregion

        #region///PLC参数写入

        //PLC写入位寄存器值
        public virtual int PLC_BitReg_Write(string mAddressStr, bool mVal, string mStateStr = "")
        {
            return 0;
        }

        //PLC写入字寄存器值
        public virtual int PLC_WordReg_Write(string mAddressStr, ushort mValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC写入有符号字寄存器值
        public virtual int PLC_WordReg_Write(string mAddressStr, short mValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC写入多字寄存器值
        public virtual int PLC_MulWordReg_Write(string mAddressStr, int mCount, ushort[] mValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC写入多有符号字寄存器值
        public virtual int PLC_MulWordReg_Write(string mAddressStr, int mCount, short[] mValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC写入双字寄存器值
        public virtual int PLC_DoubleWordReg_Write(string mAddressStr, uint mValue, string mStateStr = "")
        {
            return 0;
        }

        //PLC写入多双字寄存器值
        public virtual int PLC_MulDoubleWordReg_Write(string mAddressStr, int mCount, uint[] mValue, string mStateStr = "")
        {
            return 0;
        }

        #endregion

        #region///错误信息

        public virtual string ErrMessage(int ErrCode)
        {
            switch (ErrCode)
            {
                case 1:
                    return "成功";
                case -1:
                    return "连接失败";
                case -2:
                    return "发送命令超时";
                case -3:
                    return "发送指令给PLC后从PLC读取的返回值是异常值";
                case -4:
                    return "数组个数与设定写入个数不一致";
                case -5:
                    return "位超出范围(值必须在0~15之间)";
                case -6:
                    return "PLC地址超出范围";
                default:
                    return "未找到该错误代码";
            }
        }

        #endregion
    }
}
