using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ClsDeviceComm;
using ClsDeviceComm.Profinet.Melsec;
using ClsDeviceComm.Profinet.Siemens;
using ClsDeviceComm.Profinet.Omron;
using ClsDeviceComm.Profinet.Panasonic;
using ClsDeviceComm.Profinet.Keyence;
using ClsDeviceComm.Core;

namespace OCV
{
    public class ClsPLCContr
    {
        //内部连接成功标识
        protected bool mConnected = false;
        //连接成功标识
        public bool Connected
        {
            get { return mConnected; }
        }

        public ClsDevUnitAddr mPlcAddr;           //单元地址
        private OperateResult connectResult = null;

        #region 驱动函数
        private MelsecMcNet PlcTcpNet = null;         //三菱二进制读写 
        //private MelsecMcAsciiNet PlcTcpNet = null;  // 三菱Ascii读写
        //private OmronFinsNet PlcTcpNet = null; //欧姆龙
        // private SiemensS7Net PlcTcpNet = null; //西门子

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Port">端口号</param>
        /// <param name="Addr">IP地址</param>
        public ClsPLCContr(int Port, string Addr)
        {
            //初始化地址
            mPlcAddr = new ClsDevUnitAddr();
            DrvInitial(Port, Addr);
        }
        //驱动初始化
        public void DrvInitial(int Port, string Addr)
        {
            connectResult = new OperateResult() { IsSuccess = false };
            PlcTcpNet = new MelsecMcNet();
            //欧姆龙
            //PlcTcpNet = new OmronFinsNet();
            //PlcTcpNet.SA1 = 192;
            //PlcTcpNet.DA2 = 0;
            //PlcTcpNet.ByteTransform.DataFormat = DataFormat.CDAB;

            ////西门子
            //PlcTcpNet = new SiemensS7Net(SiemensPLCS.S200Smart);
            PlcTcpNet.IpAddress = Addr;
            PlcTcpNet.Port = Port;
            Thread plcRead = new Thread(new ThreadStart(this.ThreadPlcDeviceStatus));
            plcRead.IsBackground = true;
            plcRead.Start();
        }


        #endregion

        //获取PLC设备状态
        private void ThreadPlcDeviceStatus()
        {
            while (true)
            {
                try
                {
                    if (connectResult.IsSuccess)
                    {
                        string register = "W100"; //开始地址
                        ushort arrLength = 32;      //数组长度
                        OperateResult<short[]> opr = PlcTcpNet.ReadInt16(register, arrLength);
                        if (opr.IsSuccess)
                        {
                            ClsPLCValue.PlcValue.Plc_AutoManual = opr.Content[0];
                           // ClsPLCValue.PlcValue.Plc_ScanFinshReply = opr.Content[1];
                            ClsPLCValue.PlcValue.Plc_RequestTest = opr.Content[3];
                            ClsPLCValue.PlcValue.Plc_TestFinshReply = opr.Content[4];
                            //ClsPLCValue.PlcValue.Plc_ResetReply = opr.Content[5
                            ClsPLCValue.PlcValue.Plc_InitReply = opr.Content[6];
                            ClsPLCValue.PlcValue.Plc_HaveTray = opr.Content[11];
                            ClsPLCValue.PlcValue.Plc_AutoStepNO = opr.Content[12];
                            ClsPLCValue.PlcValue.Plc_ResetStepNO = opr.Content[13];
                            ClsPLCValue.PlcValue.Plc_Period = opr.Content[15];
                            ClsPLCValue.PlcValue.Plc_EmergencyStop = opr.Content[16];
                      
                         
                            ClsPLCValue.PlcValue.Plc_Alarm1 = opr.Content[21];
                            ClsPLCValue.PlcValue.Plc_Alarm1 = opr.Content[22];
                        }
                        else
                        { //读取异常 
                          //MyShowPlcMsg("线程[ThreadPlcDevice]:读值失败" + connectResult.Message, 1);
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }

                        register = "W160";
                        opr = PlcTcpNet.ReadInt16(register, arrLength);
                        if (opr.IsSuccess)
                        {
                            ClsPLCValue.PlcValue.Plc_Alarm1 = opr.Content[0];
                            ClsPLCValue.PlcValue.Plc_Error= opr.Content[0];
                            ClsPLCValue.PlcValue.Plc_Error2 = opr.Content[1];
                            ClsPLCValue.PlcValue.Plc_Alarm2 = opr.Content[1];
                    
                        }
                        else
                        { //读取异常 
                          //MyShowPlcMsg("线程[ThreadPlcDevice]:读值失败" + connectResult.Message, 1);
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }
                        OperateResult<ushort[]> Uopr = PlcTcpNet.ReadUInt16("W131", 2);
                        if (opr.IsSuccess)
                        {
                            //IO

                            ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 = this.CheckBit16(Uopr.Content[0], 0);
                            ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 = this.CheckBit16(Uopr.Content[0], 1);
                           
                            ClsPLCValue.PlcValue.Plc_IO_PosCylUp2 = this.CheckBit16(Uopr.Content[0], 2);
                            ClsPLCValue.PlcValue.Plc_IO_PosCylDown2 = this.CheckBit16(Uopr.Content[0], 3);
                          
                           // ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 = this.CheckBit16(Uopr.Content[0], 4);
                           // ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 = this.CheckBit16(Uopr.Content[0], 5);

                           // ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose2 = this.CheckBit16(Uopr.Content[0], 6);
                          //  ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen2 = this.CheckBit16(Uopr.Content[0], 7);

                            ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = this.CheckBit16(Uopr.Content[0], 10);
                            ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = this.CheckBit16(Uopr.Content[0], 11);


                            ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = this.CheckBit16(Uopr.Content[0], 8);
                            ClsPLCValue.PlcValue.Plc_IO_SlowSpeedSignal = this.CheckBit16(Uopr.Content[1], 2);
                            ClsPLCValue.PlcValue.Plc_IO_TrayInSignal = this.CheckBit16(Uopr.Content[1], 3);
             
                        }
                        else
                        { //读取异常 
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }

                        OperateResult<ushort[]> Uopr1 = PlcTcpNet.ReadUInt16("W130", 1);
                        if (opr.IsSuccess)
                        {
                            //IO
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 = CheckBin16(Uopr1.Content[0], 4);//this.CheckBit16(Uopr1.Content[0], 4);
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose2 = CheckBin16(Uopr1.Content[0], 1);

                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 = CheckBin16(Uopr1.Content[0], 2);
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen2 = CheckBin16(Uopr1.Content[0], 5);

                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylCloseS1 = CheckBin16(Uopr1.Content[0], 3);//this.CheckBit16(Uopr1.Content[0], 4);
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylCloseS2 = CheckBin16(Uopr1.Content[0], 0);

                        }
                        else
                        { //读取异常 
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }

                        Uopr = PlcTcpNet.ReadUInt16("W140", 1);
                        if (opr.IsSuccess)
                        {
                            //IO

                            ClsPLCValue.PlcValue.Plc_IO_CVRun = this.CheckBit16(Uopr.Content[0], 6);
                            ClsPLCValue.PlcValue.Plc_IO_CVRunback = this.CheckBit16(Uopr.Content[0], 7);
                            ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow = this.CheckBit16(Uopr.Content[0],10);
                            ClsPLCValue.PlcValue.Plc_IO_BhOCVReq = this.CheckBit16(Uopr.Content[0], 11);                           
                        }
                        else
                        { //读取异常 
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }
                        ////心跳
                        //OperateResult result = PlcTcpNet.Write(mPlcAddr.PC_心跳信号, 0);
                        //if (!result.IsSuccess)
                        //{
                        //    if (opr.Message.Contains("连接失败"))
                        //    {
                        //        connectResult = new OperateResult() { IsSuccess = false };
                        //    }
                        //} 
                    }
                    else
                    {
                        try
                        {
                            PlcTcpNet.ConnectClose();
                            connectResult = PlcTcpNet.ConnectServer(); //连接PLC
                            if (!connectResult.IsSuccess) //若连接失败
                            {
                                ClsPLCValue.connectSuccess = false;
                                ClsPLCValue.PlcValue.Plc_AutoManual = -1;
                                ClsPLCValue.PlcValue.Plc_HaveTray = -1;
                                //ClsPLCValue.PlcValue.Plc_RequestScan = -1;
                                //ClsPLCValue.PlcValue.Plc_ScanFinshReply = -1;
                                ClsPLCValue.PlcValue.Plc_RequestTest = -1;
                                ClsPLCValue.PlcValue.Plc_TestFinshReply = -1;
                                ClsPLCValue.PlcValue.Plc_EmergencyStop = -1;
                                //ClsPLCValue.PlcValue.Plc_ResetReply = -1;
                                ClsPLCValue.PlcValue.Plc_InitReply = -1;
                                
                                ClsPLCValue.PlcValue.Plc_Alarm1 = -1;
                                ClsPLCValue.PlcValue.Plc_Alarm1 = -1;

                                ClsPLCValue.PlcValue.Plc_IO_CVRun = -1;
                                ClsPLCValue.PlcValue.Plc_IO_CVRunback = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp2 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown2= -1;

                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose2 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen2 = -1;
                                //ClsPLCValue.PlcValue.Plc_IO_BlockCylUp = -1;
                                //ClsPLCValue.PlcValue.Plc_IO_BlockCylDown = -1;
                                ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = -1;
                                ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow = -1;
                                ClsPLCValue.PlcValue.Plc_IO_BhOCVReq = -1;
                                ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = -1;
                                ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = -1;
                                ClsPLCValue.PlcValue.Plc_IO_SlowSpeedSignal = -1;
                                ClsPLCValue.PlcValue.Plc_IO_TrayInSignal = -1;
                              
                            }
                            else
                            {
                                ClsPLCValue.connectSuccess = true;
                                // MyShowPlcMsg("PLC连接成功", 1);

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    
                }
                catch (Exception )
                { 
                }
                finally
                {
                    Thread.Sleep(500);
                }
            }
        }
        private short CheckBit16(UInt16 Data, int BitNum)
        {
            UInt16[] CmpBit = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80,
                         0x0100, 0x0200,0x0400,0x0800,0x1000, 0x2000, 0x4000, 0x8000};

            if (BitNum < 0 || BitNum > 15) return 0xFF;

            if ((Data & CmpBit[BitNum]) == CmpBit[BitNum])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private short CheckBin16(UInt16 Data, int BitNum)
        {
            string strBin = Convert.ToString(Data,2).PadLeft(16, '0');
            char[] arr = strBin.ToCharArray();
            List<char> list = arr.ToList();
            list.Reverse();
            if (list[BitNum] == '1')
                return 1;
            else
                return 0;
        }
        #region 接口函数   

        public void ReadDB(string DeviceNameBlock, out short Val)
        {
            try
            {
                OperateResult<short> result = PlcTcpNet.ReadInt16(DeviceNameBlock);
                if (result.IsSuccess)
                {
                    Val = result.Content;
                }
                else
                {
                    throw new Exception($"[{DeviceNameBlock}] 读取异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PLCErr->" + ex.Message);
            }
        }
      
        public void ReadDB(string DeviceNameBlock, out ushort Val)
        {
            try
            {
                OperateResult<ushort> result = PlcTcpNet.ReadUInt16(DeviceNameBlock);
                if (result.IsSuccess)
                {
                    Val = result.Content;
                }
                else
                {
                    throw new Exception($"[{DeviceNameBlock}] 读取异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PLCErr->" + ex.Message);
            }
        }

        public void ReadDB(string DeviceNameBlock, out int Val)
        {
            try
            {
                OperateResult<int> result = PlcTcpNet.ReadInt32(DeviceNameBlock);
                if (result.IsSuccess)
                {
                    Val = result.Content;
                }
                else
                {
                    throw new Exception($"[{DeviceNameBlock}] 读取异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PLCErr->" + ex.Message);
            }
        }

        public void WriteDB(string DeviceNameBlock, ushort Val)
        {

            try
            {
                OperateResult result = PlcTcpNet.Write(DeviceNameBlock, Val);
                if (result.IsSuccess)
                {
                   
                }
                else
                {
                    throw new Exception( $"[{DeviceNameBlock}] 写入异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("PLCErr->" + ex.Message);
            }
        }

        public void WriteDB(string DeviceNameBlock, string Val)
        {
            try
            {
                OperateResult result = PlcTcpNet.Write(DeviceNameBlock, Val);
                if (result.IsSuccess)
                {

                }
                else
                {
                    throw new Exception( $"[{DeviceNameBlock}] 写入异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("PLCErr->" + ex.Message);
            }
        }

        public void WriteIntDB(string DeviceNameBlock, int Val)
        {

            try
            {
                OperateResult result = PlcTcpNet.Write(DeviceNameBlock, Val);
                if (result.IsSuccess)
                {

                }
                else
                {
                    throw new Exception( $"[{DeviceNameBlock}] 读取异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("PLCErr->" + ex.Message);
            }
        }

        //获取条码
        public string GetTrayCode()
        {
            List<char> theCode = new List<char>();
            short DevVal = 0;

            for (int i = 0; i < 10; i++)
            {
                //ReadDB(ClsDevAddr.PLC_TrayNo[i], out DevVal);

                if (DevVal != 0)
                {
                    string Hex = DevVal.ToString("x4");
                    string Hex1 = Hex.Substring(2, 2);
                    string Hex2 = Hex.Substring(0, 2);
                    int Val1 = Convert.ToInt32(Hex1, 16);
                    int Val2 = Convert.ToInt32(Hex2, 16);
                    //if (Val1 != 0 && Val2 != 0)
                    //{
                    char charVal1 = (char)Val1;
                    theCode.Add(charVal1);
                    char charVal2 = (char)Val2;
                    theCode.Add(charVal2);
                    //}
                }
            }

            string result = "";
            foreach (char item in theCode)
            {
                result += item;
            }
            return result;
        }

        //获取条码
        public string GetTrayCode(string[] startAddr, int len)
        {
            List<char> theCode = new List<char>();
            short DevVal = 0;
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    ReadDB(startAddr[i], out DevVal);

                    if (DevVal != 0)
                    {
                        string Hex = DevVal.ToString("x4");
                        string Hex1 = Hex.Substring(2, 2);
                        string Hex2 = Hex.Substring(0, 2);
                        int Val1 = Convert.ToInt32(Hex1, 16);
                        int Val2 = Convert.ToInt32(Hex2, 16);
                        //if (Val1 != 0 && Val2 != 0)
                        //{
                        char charVal1 = (char)Val1;
                        theCode.Add(charVal1);
                        char charVal2 = (char)Val2;
                        theCode.Add(charVal2);
                        //}
                    }
                }

                string result = "";
                foreach (char item in theCode)
                {
                    result += item;
                }
                result = result.Trim(new char[] { '\0' });

                if (result.Length <= len)
                {
                    return result;
                }
                else
                {
                    result = result.Substring(0, len);
                    //throw new Exception("异常:读取托盘条码位数大于设置数");
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// PC->PLC 写电池NG数据 
        /// <param name="startAddr">电池异常数据寄存器地址数组</param>
        /// <param name="Code">电池异常数据数组</param>
        public void WriteNGCode(string[] startAddr, string[] Code)
        {
            string NGCode = "";
            int index;
            ushort num;
            try
            {
                for (int i = 0; i < startAddr.Length; i++)
                {
                    index = i * 16;
                    for (int j = 0; j < 16; j++)
                    {
                        NGCode += Code[index];
                        index++;
                    }
                    //num = CheckInt10(NGCode);
                    //WriteDB(startAddr[i], num);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 读电池NG数据 
        /// </summary>
        /// <param name="startAddr">电池异常数据寄存器地址数组</param>
        /// <returns>获得NG代码</returns>
        public string[] GetNGCode(string[] startAddr)
        {
            string[] arrayData;
            int DevVal = 0;
            string[] NGCode = new string[80];
            for (int i = 0; i < 5; i++)
            {
                ReadDB(startAddr[i], out DevVal);
                arrayData = CheckInt2(DevVal);
                for (int j = 0; j < 16; j++)
                {
                    NGCode[j + 16 * i] = arrayData[15 - j];
                }

            }
            return NGCode;
        }
        /// <summary>
        /// 二进制字符串转成十进制数
        /// </summary>
        /// <param name="StrNum">1:数值 
        public static int CheckInt10(string StrNum)
        {
            try
            {
                int mum = System.Convert.ToInt32(StrNum, 2);

                return mum;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 十进制转成16位二进制数组
        /// </summary>
        /// <param name="Num">1:数值 
        public static string[] CheckInt2(int Num)
        {
            try
            {
                string strmum = System.Convert.ToString(Num, 2).PadLeft(16, '0');
                int j = 0;
                string[] Data = new string[16];
                foreach (char c in strmum)
                {
                    int mum = c;
                    Data[j] = c.ToString();
                    j++;
                }
                return Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool GetBoolValue(List<BitAddressValue> addresslist, string currAddress)
        {
            string[] arr = currAddress.Split('.');
            string address = arr[0];
            string bitAddress = arr[1];
            var query = (from p in addresslist where p.Address == address select p).FirstOrDefault();
            int mBit = 0;
            if (int.TryParse(bitAddress, out mBit) == false)
            {
                switch (bitAddress)
                {
                    case "a":
                        mBit = 10;
                        break;
                    case "b":
                        mBit = 11;
                        break;
                    case "c":
                        mBit = 12;
                        break;
                    case "d":
                        mBit = 13;
                        break;
                    case "e":
                        mBit = 14;
                        break;
                    case "f":
                        mBit = 15;
                        break;
                    default:
                        mBit = 0;
                        break;

                }

            }
            if (query != null)
            {
                try
                {
                    long mRecieveData = SignToUnsign((short)query.Value);
                    string mBinaryStr = Convert.ToString(mRecieveData, 2).PadLeft(16, '0');   //转换为2进制 
                    char[] mCharRead = mBinaryStr.ToCharArray();        //读取到的值
                    char[] mCharWrite = new char[16];                   //要写入的值
                    for (int i = 0; i < 16; i++)
                    {  
                        mCharWrite[i] = mCharRead[i];
                    }
                    char cValue = mCharWrite[15 - mBit];
                    if (cValue == '1')
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
            return false;
        }

        public long SignToUnsign(short Sign)
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


    }
}
