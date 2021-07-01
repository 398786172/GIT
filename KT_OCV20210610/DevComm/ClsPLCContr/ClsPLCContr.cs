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

        public ClsPLCAddr mPlcAddr;           //单元地址
        private OperateResult connectResult = null;
        private const short MaxSpeed = 31000;
        public const short mSet = 1;
        public const short mReSet = 0;

        #region 驱动函数

        private MelsecMcNet PlcTcpNet = null;         //三菱二进制读写 
        //private MelsecMcAsciiNet PlcTcpNet = null;  // 三菱Ascii读写
        //private OmronFinsNet PlcTcpNet = null;      //欧姆龙Fins
        //private SiemensS7Net PlcTcpNet = null;      //西门子S7

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Port">端口号</param>
        /// <param name="Addr">IP地址</param>
        public ClsPLCContr(int Port, string Addr)
        {
            //初始化地址
            mPlcAddr = new ClsPLCAddr();

            DrvInitial(Port, Addr);
        }

        //驱动初始化
        public void DrvInitial(int Port, string Addr)
        {
            connectResult = new OperateResult() { IsSuccess = false };

            //三菱
            PlcTcpNet = new MelsecMcNet();

            ////欧姆龙
            //PlcTcpNet = new OmronFinsNet();
            //PlcTcpNet.SA1 = 192;
            //PlcTcpNet.DA2 = 0;
            PlcTcpNet.ByteTransform.DataFormat = DataFormat.CDAB;

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
                        string register = "W100";   //开始地址
                        ushort arrLength = 30;      //数组长度

                        OperateResult<short[]> opr = PlcTcpNet.ReadInt16(register, arrLength);

                        if (opr.IsSuccess)
                        {
                            //交互
                         
                            ClsPLCValue.PlcValue.Plc_AutoManual = opr.Content[0];
                            ClsPLCValue.PlcValue.Plc_HaveTray = opr.Content[11];
                           //  = opr.Content[2];   //复位
                            ClsPLCValue.PlcValue.Plc_RequestTest_A = opr.Content[3];
                            ClsPLCValue.PlcValue.Plc_TestFinshReply_A = opr.Content[4];   //PLC_测定结束应答
                            ClsPLCValue.PlcValue.Plc_IO_ZeroIng = opr.Content[16];
                            ClsPLCValue.PlcValue.Plc_IO_ZeroCompletion = opr.Content[24];
                            ClsPLCValue.PlcValue.Plc_InitReply = opr.Content[7];          //PLC_初始化完成
                            ClsPLCValue.PlcValue.Plc_EmergencyStop = opr.Content[10];     //PLC_急停信号

                            ClsPLCValue.PlcValue.Plc_AutoStepNO = opr.Content[12];
                            ClsPLCValue.PlcValue.Plc_ResetStepNO = opr.Content[13];
                        }
                        else
                        { 
                            //读取异常 
                            //MyShowPlcMsg("线程[ThreadPlcDevice]:读值失败" + connectResult.Message, 1);

                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }

                        //
                        OperateResult<ushort[]> Uopr = PlcTcpNet.ReadUInt16(mPlcAddr.PLC_输入IO1, 3);

                        if (opr.IsSuccess)
                        {
                            //IO
                            /*    ClsPLCValue.PlcValue.Plc_IO_CVRun = this.CheckBit16(Uopr.Content[0], 1);
                                ClsPLCValue.PlcValue.Plc_IO_CVRunback = this.CheckBit16(Uopr.Content[0], 2);
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp = this.CheckBit16(Uopr.Content[0], 3);
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown = this.CheckBit16(Uopr.Content[0], 4);
                                ClsPLCValue.PlcValue.Plc_IO_BlockCylUp = this.CheckBit16(Uopr.Content[0],5);
                                ClsPLCValue.PlcValue.Plc_IO_BlockCylDown = this.CheckBit16(Uopr.Content[0], 6);
                                ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = this.CheckBit16(Uopr.Content[0], 7);
                                ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow = this.CheckBit16(Uopr.Content[0], 8);
                                ClsPLCValue.PlcValue.Plc_IO_BhOCVReq = this.CheckBit16(Uopr.Content[0], 9);
                                ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = this.CheckBit16(Uopr.Content[0], 10);
                                ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = this.CheckBit16(Uopr.Content[0], 11);
                                ClsPLCValue.PlcValue.Plc_IO_SlowSpeedSignal = this.CheckBit16(Uopr.Content[0], 12);
                                ClsPLCValue.PlcValue.Plc_IO_TrayInSignal = this.CheckBit16(Uopr.Content[0], 13);
                                ClsPLCValue.PlcValue.Plc_IO_TrayTypeSignal = this.CheckBit16(Uopr.Content[0], 14);
                                ClsPLCValue.PlcValue.Plc_IO_NegLimit = this.CheckBit16(Uopr.Content[0], 15);
                                ClsPLCValue.PlcValue.Plc_IO_HomeLimit = this.CheckBit16(Uopr.Content[0], 16);

                                ClsPLCValue.PlcValue.Plc_IO_PosLimit = this.CheckBit16(Uopr.Content[1], 1);
                            
                            */
                            ClsPLCValue.PlcValue.Plc_IO_HomeLimit = this.CheckBit16(Uopr.Content[0], 0);            //原点
                            ClsPLCValue.PlcValue.Plc_IO_XAr = this.CheckBit16(Uopr.Content[0], 2);                  //X轴报警
                            ClsPLCValue.PlcValue.Plc_IO_PosLimit = this.CheckBit16(Uopr.Content[0], 4);             //X轴正极限
                            ClsPLCValue.PlcValue.Plc_IO_NegLimit = this.CheckBit16(Uopr.Content[0], 5);             //X轴负极限
                            ClsPLCValue.PlcValue.Plc_IO_PosCylUp = this.CheckBit16(Uopr.Content[1], 0);             //前顶升伸
                            ClsPLCValue.PlcValue.Plc_IO_PosCylDown = this.CheckBit16(Uopr.Content[1], 1);           //前顶升缩
                            ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 = this.CheckBit16(Uopr.Content[1], 2);            //后顶升伸
                            ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 = this.CheckBit16(Uopr.Content[1], 3);          //后顶升缩                            
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose = this.CheckBit16(Uopr.Content[1], 4);        //探针左压合伸
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen = this.CheckBit16(Uopr.Content[1], 5);         //探针左压合缩
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 = this.CheckBit16(Uopr.Content[1], 6);        //探针右压合伸
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 = this.CheckBit16(Uopr.Content[1], 7);         //探针右压合缩
                            ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = this.CheckBit16(Uopr.Content[1], 8);         //进料口托盘检测（托盘入口）
                            ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = this.CheckBit16(Uopr.Content[1], 10);             //允许出盘
                            ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = this.CheckBit16(Uopr.Content[1], 11);           //请求进盘
                            ClsPLCValue.PlcValue.Plc_IO_YanwuUp = this.CheckBit16(Uopr.Content[1], 12);              //烟雾传感器前
                            ClsPLCValue.PlcValue.Plc_IO_YanwuDown = this.CheckBit16(Uopr.Content[1], 13);            //烟雾传感器后
                            ClsPLCValue.PlcValue.Plc_IO_Opendoor = this.CheckBit16(Uopr.Content[1], 14);             //门开关
                            ClsPLCValue.PlcValue.Plc_IO_Stop = this.CheckBit16(Uopr.Content[2], 0);                  //急停
                            ClsPLCValue.PlcValue.Plc_IO_Alaguntong = this.CheckBit16(Uopr.Content[2], 1);            //滚筒报警
                            ClsPLCValue.PlcValue.Plc_IO_jiansuguandian = this.CheckBit16(Uopr.Content[2], 2);        //减速关电
                            ClsPLCValue.PlcValue.Plc_IO_jiantuopandaowei = this.CheckBit16(Uopr.Content[2], 3);      //托盘到位检测
                            ClsPLCValue.PlcValue.Plc_IO_tuopanrudingqian = this.CheckBit16(Uopr.Content[2], 5);      //托盘入定位销检测（前）
                            ClsPLCValue.PlcValue.Plc_IO_tuopanrudinghou = this.CheckBit16(Uopr.Content[2], 6);       //托盘入定位销检测（后）
                            ClsPLCValue.PlcValue.Plc_IO_qiyajiance = this.CheckBit16(Uopr.Content[2], 7);            //气压检测

                        }
                        else
                        { 
                            //读取异常 
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }

                        //心跳
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
                                ClsPLCValue.PlcValue.Plc_RequestTest_A = -1;
                                ClsPLCValue.PlcValue.Plc_TestFinshReply_A = -1;
                                ClsPLCValue.PlcValue.Plc_EmergencyStop = -1;

                                ClsPLCValue.PlcValue.Plc_IO_ZeroIng = -1;
                                ClsPLCValue.PlcValue.Plc_IO_ZeroCompletion = -1;

                                ClsPLCValue.PlcValue.Plc_IO_CVRun = -1;
                                ClsPLCValue.PlcValue.Plc_IO_CVRunback = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 = -1;
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose = -1;
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen =-1;
                                ClsPLCValue.PlcValue.Plc_IO_BlockCylUp = -1;
                                ClsPLCValue.PlcValue.Plc_IO_BlockCylDown = -1;
                                ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = -1;
                                ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow = -1;
                                ClsPLCValue.PlcValue.Plc_IO_BhOCVReq = -1;
                                ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = -1;
                                ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = -1;
                                ClsPLCValue.PlcValue.Plc_IO_SlowSpeedSignal = -1;
                                ClsPLCValue.PlcValue.Plc_IO_TrayInSignal = -1;
                                ClsPLCValue.PlcValue.Plc_IO_TrayTypeSignal = -1;
                                ClsPLCValue.PlcValue.Plc_IO_NegLimit = -1;
                                ClsPLCValue.PlcValue.Plc_IO_HomeLimit = - 1;
                                ClsPLCValue.PlcValue.Plc_IO_PosLimit = -1;
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

        //探针气缸下降
        public  void Set_CylBlock_Down()
        {
            try
            {
                PlcTcpNet.Write(ClsGlobal.mPLCContr.mPlcAddr.PC_针床气缸控制, 1);
            }
            catch (Exception ex)
            {

            }
        }

        //探针气缸上升
        public void Set_CylBlock_Up()
        {
            try
            {
                PlcTcpNet.Write(ClsGlobal.mPLCContr.mPlcAddr.PC_针床气缸控制, 0);

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 运动到位
        /// </summary>
        /// <returns>
        /// true: 到位
        /// false:未到位
        /// </returns>
        public bool GetState_MoveInPlace(short num)
        {
            short tempVal;
            bool bInPlace = false;
            int currPos;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_到位);
                tempVal = read.Content;
                //OperateResult<int> read1 = PlcTcpNet.ReadInt32(mPlcAddr.PLC_当前坐标);
                //currPos = read1.Content;
                currPos = DevMove_CurrentPos();
                if (num == 1 && currPos == ClsGlobal.SetPos1)
                {
                    bInPlace = true;
                }
                if (num == 2 && currPos == ClsGlobal.SetPos2)
                {
                    bInPlace = true;
                }
                if (num == 3 && currPos == ClsGlobal.SetPos3)
                {
                    bInPlace = true;
                }
                if (num == 4 && currPos == ClsGlobal.SetPos4)
                {
                    bInPlace = true;
                }
                if (num == 5 && currPos == ClsGlobal.SetPos5)
                {
                    bInPlace = true;
                }
                if (num == 6 && currPos == ClsGlobal.SetPos6)
                {
                    bInPlace = true;
                }
                if (num == 7 && currPos == ClsGlobal.SetPos7)
                {
                    bInPlace = true;
                }
                if (num == 8 && currPos == ClsGlobal.SetPos8)
                {
                    bInPlace = true;
                }
                if (num == 9 && currPos == ClsGlobal.SetPos9)
                {
                    bInPlace = true;
                }
                if (tempVal == 1 || bInPlace == true)
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
                
                return true;
                throw ex;
            }
        }

        #region 设备X轴控制
        /// <summary>
        /// 获取正限位信号
        /// </summary>
        /// <returns>
        /// true: 碰到正限位
        /// false:没到正限位
        /// </returns>
        public bool GetState_PosLimit()
        {
            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_输入IO1);
                tempVal = read.Content;
                if ((tempVal & 0x10) == 0x10)
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
              
                return true;
                throw ex;
            }
        }

        /// <summary>
        /// 获取原点信号
        /// </summary>
        /// <returns>
        /// true: 碰到原点
        /// false:没到原点
        /// </returns>
        public bool GetState_HomeLimit()
        {
            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_输入IO1);
                tempVal = read.Content;
                if ((tempVal & 0x01) == 0x01)
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
            
                return true;
                throw ex;
            }
        }

        /// <summary>
        /// 获取负限位信号
        /// </summary>
        /// <returns>
        /// true: 碰到负限位
        /// false:没到负限位
        /// </returns>
        public bool GetState_NegLimit()
        {
            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_输入IO1);
                tempVal = read.Content;
                if ((tempVal & 0x20) == 0x20)
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
                return true;
                throw ex;
            }
        }
        /// <summary>
        /// 回零中
        /// </summary>
        /// <returns>
        /// true: 到位
        /// false:未到位
        /// </returns>
        public bool GetState_ZeroIng()
        {

            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_回零中);
                tempVal = read.Content;
                if (tempVal == 1)
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
                return true;
                throw ex;
            }
        }

        /// <summary>
        /// 回零完成
        /// </summary>
        /// <returns>
        /// true: 到位
        /// false:未到位
        /// </returns>
        public bool GetState_ZeroCompletion()
        {

            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_回零完成);
                tempVal = read.Content;
                if (tempVal == 1)
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
                return true;
                throw ex;
            }
        }

        /// <summary>
        /// 报警复位
        /// </summary>
        public void Set_ErrorToHome()
        {
            try
            {
                PlcTcpNet.Write(mPlcAddr.PC_报警复位, mSet);
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 设备X轴回零置1
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        public void DevMove_SetHome()
        {

            try
            {
                PlcTcpNet.Write(mPlcAddr.PC_初始化, mSet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设备X轴回零清0
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        public void DevMove_ReSetHome()
        {

            try
            {
                PlcTcpNet.Write(mPlcAddr.PC_初始化, mReSet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 设备X轴正向运动
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        public void DevMove_Pos(short speed)
        {
            short i, tempVal;
            if (speed < 0 || speed > MaxSpeed)
            {
                return;
            }
            i = speed;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度);
                tempVal = read.Content;
                if (i != tempVal)
                {
                    PlcTcpNet.Write(mPlcAddr.PC_速度, i);
                    Thread.Sleep(10);
                }
                PlcTcpNet.Write(mPlcAddr.PC_正转, mSet);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设备X轴反向运动
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        public void DevMove_Neg(short speed)
        {
            short i, tempVal;
            if (speed < 0 || speed > MaxSpeed)
            {
                return;
            }
            i = speed;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度);
                tempVal = read.Content;
                if (i != tempVal)
                {
                    PlcTcpNet.Write(mPlcAddr.PC_速度, i);
                    Thread.Sleep(10);
                }
                PlcTcpNet.Write(mPlcAddr.PC_反转, mSet);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设备X轴运行中修改速度
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        public void DevMove_ChangeSpeed(short speed)
        {
            if (speed < 0 || speed > MaxSpeed)
            {
                return;
            }
            try
            {
                PlcTcpNet.Write(mPlcAddr.PC_速度, speed);
                Thread.Sleep(10);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取当前设备运行速度
        /// </summary>
        /// <returns>速度值,单位: pps</returns>
        public short DevMove_CurrentSpeed()
        {
            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度);
                tempVal = read.Content;
                return tempVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设备X轴停止运行
        /// </summary>
        public void DevMove_Stop()
        {
            try
            {
                PlcTcpNet.Write(mPlcAddr.PC_正转, mReSet);
                PlcTcpNet.Write(mPlcAddr.PC_反转, mReSet);
                PlcTcpNet.Write(mPlcAddr.PC_相对坐标启动, mReSet);
                PlcTcpNet.Write(mPlcAddr.PC_启动, mReSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加速时间设置
        /// </summary>
        /// <returns>速度值,单位: pps</returns>
        public void DevMove_ChangeAccTime(short mAccTime)
        {
            if (mAccTime < 0 || mAccTime > 1000)
            {
                return;
            }
            try
            {
                PlcTcpNet.Write(mPlcAddr.PC_加减速时间, mAccTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读取当前设备加速时间
        /// </summary>
        /// <returns>速度值,单位: pps</returns>
        public short DevMove_CurrentAccTime()
        {
            short tempVal;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_加减速时间);
                tempVal = read.Content;
                return tempVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 坐标设置
        /// </summary>
        /// <returns>速度值,单位: pps</returns>
        public void DevMove_ChangePos(int pos, short num)
        {
            try
            {
                if (pos < -1000 || pos > 300000 || num < 1 || num > 8)
                {
                    return;
                }
				short[] sPos = new short[2];
				sPos[0] = (short) (pos & 0xffff);
				sPos[1] = (short)(pos / 0xffff);

				switch (num)
                {
                    case 1:
                        //PlcTcpNet.Write(mPlcAddr.PC_1POS1, pos);
						PlcTcpNet.Write(mPlcAddr.PC_POS1, sPos);
						break;
                    case 2:
                        PlcTcpNet.Write(mPlcAddr.PC_POS2, sPos);
                        break;
                    case 3:
                        PlcTcpNet.Write(mPlcAddr.PC_POS3, sPos);
                        break;
                    case 4:
                        PlcTcpNet.Write(mPlcAddr.PC_POS4, sPos);
                        break;
                    case 5:
                        PlcTcpNet.Write(mPlcAddr.PC_POS5, sPos);
                        break;
                    case 6:
                        PlcTcpNet.Write(mPlcAddr.PC_POS6, sPos);
                        break;
                    case 7:
                        PlcTcpNet.Write(mPlcAddr.PC_POS7, sPos);
                        break;
                    case 8:
                        PlcTcpNet.Write(mPlcAddr.PC_POS8, sPos);
                        break;
                    case 9:
                        PlcTcpNet.Write(mPlcAddr.PC_POS9, sPos);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 读取设置坐标
        /// </summary>
        /// <returns>速度值,单位: pps</returns>
        public int DevMove_ReadSetPos(short num)
        {
            try
            {
                int tempVal;
				short[] tempVal1;
				//OperateResult<int> read = null;
				OperateResult<short[]> read = null;

				if (num < 1 || num > 8)
                {
                    return 0;
                }
                switch (num)
                {
                    case 1:
						read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS1, 2);
                        break;
                    case 2:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS2, 2);
                        break;
                    case 3:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS3, 2);
                        break;
                    case 4:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS4, 2);
                        break;
                    case 5:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS5, 2);
                        break;
                    case 6:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS6, 2);
                        break;
                    case 7:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS7, 2);
                        break;
                    case 8:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS8, 2);
                        break;
                    case 9:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS9, 2);
                        break;
                    default:
                        tempVal = 0;
                        break;
                }

				tempVal1 = read.Content;
				tempVal = tempVal1[0] + tempVal1[1] * 65536;
				//tempVal = read.Content;
                return tempVal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 读取坐标
        /// </summary>
        /// <returns>X轴坐标,单位: pps</returns>
        public int DevMove_CurrentPos()
        {
            int tempVal;
            short[] tempVal1;
            try
            {
                OperateResult<short[]> read = null;
                read = PlcTcpNet.ReadInt16(mPlcAddr.PLC_当前坐标, 2);


                // OperateResult<int> read = PlcTcpNet.ReadInt32(mPlcAddr.PLC_当前坐标);
                //tempVal = read.Content;
                
                tempVal1 = read.Content;
                tempVal = tempVal1[0] + tempVal1[1] * 65536;
                return tempVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int PLC_IntRead(string address)
        {
            int tempVal;
            try
            {
                OperateResult<int> read = PlcTcpNet.ReadInt32(address);
                tempVal = read.Content;
                return tempVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //PLC复位
        public void Set_PLCReset()
        {
            PlcTcpNet.Write(mPlcAddr.PC_报警复位, mSet);
            //Thread.Sleep(1000);
            //PlcContr.WriteDB(mPlcAddr.PC_报警复位, 0);
        }

        //PLC不复位
        public void ReSet_PLCReset()
        {

            PlcTcpNet.Write(mPlcAddr.PC_报警复位, mReSet);
        }
        /// <summary>
        /// 设备X轴相对坐标运行
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        /// 设置速度为0是不改变速度
        public void DevMove_Inc(short speed, short position)
        {
            short i, tempVal, j;;
             
            if (speed < 0 || speed > MaxSpeed || position < -30000 || position > 30000 || position == 0)
            {
                return;
            }

            i = speed;
            try
            {
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度);
                tempVal = read.Content;
                if (i != tempVal && i != 0)
                {
                    PlcTcpNet.Write(mPlcAddr.PC_速度, i);
                    Thread.Sleep(10);
                }

                j = position;
                read = PlcTcpNet.ReadInt16(mPlcAddr.PC_运动值);
                tempVal = read.Content;
                if (j != tempVal)
                {
                    PlcTcpNet.Write(mPlcAddr.PC_运动值, j);
                    Thread.Sleep(10);
                }
                PlcTcpNet.Write(mPlcAddr.PC_相对坐标启动, mSet);
                Thread.Sleep(200);
                PlcTcpNet.Write(mPlcAddr.PC_相对坐标启动, mReSet);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设备X轴坐标号运行
        /// </summary>
        /// <param name="speed">速度值,单位: pps</param>
        /// 设置速度为0是不改变速度
        public void DevMove_AbsNO(short speed, short posNO)
        {
            short i, tempVal,j;
            
            if (speed < 0 || speed > MaxSpeed || posNO < 0 || posNO > 8)
            {
                return;
            }

            i = speed;
            try
            {
               // PlcTcpNet.Write(mPlcAddr.PC_启动, mReSet);
                OperateResult<short> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度);
                OperateResult<short> read1 = PlcTcpNet.ReadInt16(mPlcAddr.PC_坐标);
                tempVal = read.Content;
                if (i != tempVal && i != 0)
                {
                    PlcTcpNet.Write(mPlcAddr.PC_速度, i);
                    Thread.Sleep(10);
                }

                j = posNO;
                read1 = PlcTcpNet.ReadInt16(mPlcAddr.PC_坐标);
                tempVal = read.Content;
                if (j != tempVal)
                {
                    PlcTcpNet.Write(mPlcAddr.PC_坐标, j);
                    Thread.Sleep(10);
                }
                PlcTcpNet.Write(mPlcAddr.PC_启动, mSet);
                Thread.Sleep(200);
                PlcTcpNet.Write(mPlcAddr.PC_启动, mReSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
