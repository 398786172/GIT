using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace DCS.Model2700_COM.ComDriver
{/// <summary>
    /// 串口驱动的封装
    /// </summary>
    [Description("串口")]
    class ComDriver:IDriver
    {
        private SerialPort serialPort;
        private List<byte> returnBytes = new List<byte>();//记录返回的信息
        private string errorMsg = null;//记录驱动在初始化、使用过程中是否出错

        public string ErrorMsg
        {
            get
            {
                if (string.IsNullOrEmpty(errorMsg))
                {
                    if (serialPort != null && serialPort.IsOpen)
                    {
                        return null;
                    }
                    else
                    {
                        return "端口未打开";
                    }
                }
                return errorMsg;
            }
        }

        public void Init(Dictionary<string,string> dic)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = dic["PortName"];
                serialPort.BaudRate = int.Parse(dic["BauRate"]);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), dic["Parity"]);
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), dic["StopBits"]);
                serialPort.DataBits = int.Parse(dic["DataBits"]);
                serialPort.ReceivedBytesThreshold = 1;
                //20190624 li 在锂威调试发现不加这两句，接收不到串口数据
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
                serialPort.Encoding = Encoding.ASCII;

                serialPort.DataReceived += (sender, e) =>
                {
                    byte[] returnBuffer = new byte[1024];
                    int readCount = serialPort.Read(returnBuffer, 0, 1024);
                    for (int i = 0; i < readCount; i++)
                    {
                        returnBytes.Add(returnBuffer[i]);
                    }
                };
                serialPort.Open();
                if (serialPort.IsOpen)
                {
                    errorMsg = null;
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }
        }
        public byte[] ReadByte()
        {
            if (returnBytes.Count == 0)
            {
                return null;
            }
            byte[] buffer = returnBytes.ToArray();
            returnBytes.Clear();
            return buffer;
        }
        public void WriteByte(byte[] buffer)
        {
            try
            {
                serialPort.DiscardOutBuffer();
                serialPort.DiscardOutBuffer();
                serialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }
        }
    }
}

