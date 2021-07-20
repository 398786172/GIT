using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCS.Common;
using DCS.Common.Interface;
using System.ComponentModel;


namespace DCS.GPIBDriver
{
    /// <summary>
    /// GPIB驱动，对应VISA32
    /// </summary>
    [Description("GPIB")]
    public class GPIBDriver : IDriver
    {
        private string errorMsg;
        private int session = -1;
        private int viResult = -1;
        public string ErrorMsg
        {
            get
            {
                if (string.IsNullOrEmpty(errorMsg))
                {
                    if (session >= 0)
                    {
                        return "";
                    }
                    else
                    {
                        return "设备未打开";
                    }
                }
                else
                {
                    return errorMsg;
                }
            }
        }

        public void Init(DictionaryEx dic)
        {
            if (session > 0)
            {
                AgVisa32.viClose(session);
                session = -1;
            }
            try
            {
                int resourceManager = -1;
                viResult = AgVisa32.viOpenDefaultRM(out resourceManager);
                if (viResult >= AgVisa32.VI_SUCCESS)
                {
                    viResult = AgVisa32.viOpen(resourceManager, dic["USBDes"], AgVisa32.VI_NO_LOCK, AgVisa32.VI_TMO_IMMEDIATE, out session);
                   // AgVisa32.viPrintf(session, "CONF:VOLT:DC 10, MAX\n");
                    if (viResult < AgVisa32.VI_SUCCESS)
                    {
                        errorMsg = "打开设备错误";
                        session = -1;
                    }
                    else
                    {
                        errorMsg = "";
                    }
                }
                else
                {
                    errorMsg = "AgVisa32.viOpenDefaultRM返回值错误";
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                session = -1;
            }
        }

        private int WriteBytes(int session, byte[] data)
        {
            int outCount = -1;
            if (session > 0)
            {
                viResult = AgVisa32.viWrite(session, data, data.Length, out outCount);
            }
            return outCount;
        }
        private int ReadBytes(int session, int maxCount, out byte[] dataR)
        {
            int readCount = -1;
            dataR = new Byte[maxCount];
            viResult = AgVisa32.viRead(session, dataR, maxCount, out readCount);
            return readCount;
        }
        public void WriteByte(byte[] buffer)
        {
            WriteBytes(session, buffer);
        }
        public byte[] ReadByte()
        {

            if (session > 0)
            {
                byte[] bytes = new byte[1024];
                int readCount = ReadBytes(this.session, bytes.Length, out bytes);
                if (readCount > 0)
                {
                    byte[] readBytes = new byte[readCount];
                    Array.Copy(bytes, 0, readBytes, 0, readCount);
                    return readBytes;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
