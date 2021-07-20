using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ClsDeviceComm;
using ClsDeviceComm.Profinet.Siemens;
using ClsDeviceComm.Profinet.Omron;
using ClsDeviceComm.Profinet.Panasonic;
using ClsDeviceComm.Profinet.Keyence;
using ClsDeviceComm.Core;
using HslCommunication.Profinet.Melsec;
using MelsecMcNet = ClsDeviceComm.Profinet.Melsec.MelsecMcNet;

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

        public ClsPLCAddr mPlcAddr;
        //单元地址
        //private OperateResult connectResult = null;
        private OperateResult connectResult = null;
        private const short MaxSpeed = 31000;
        public const byte mSet = 1;
        public const byte mReSet = 0;

        #region 驱动函数

        //private MelsecMcNet PlcTcpNet = null;         //三菱二进制读写 
        //private MelsecMcRNet PlcTcpNet = null;        //三菱R系列二进制读写 
        //private MelsecMcAsciiNet PlcTcpNet = null;  // 三菱Ascii读写
        //private OmronFinsNet PlcTcpNet = null;      //欧姆龙Fins
        private SiemensS7Net PlcTcpNet = null;      //西门子S7

        /// <summary>
        /// 构造函数
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
            //PlcTcpNet = new MelsecMcNet();
            // PlcTcpNet = new MelsecMcRNet();
            ////欧姆龙
            //PlcTcpNet = new OmronFinsNet();
            //PlcTcpNet.SA1 = 192;
            //PlcTcpNet.DA2 = 0;
            //PlcTcpNet.ByteTransform.DataFormat = DataFormat.CDAB;

            //西门子
            PlcTcpNet = new SiemensS7Net(SiemensPLCS.S200Smart);
            PlcTcpNet.ByteTransform.DataFormat = DataFormat.CDAB;
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
                        string register = "V200";   //开始地址
                        ushort arrLength = 40;//数组长度30
                        OperateResult<byte[]> opr = PlcTcpNet.Read(register, arrLength);
                        if (opr.IsSuccess)
                        {
                            //交互
                            ClsPLCValue.PlcValue.Plc_AutoManual = opr.Content[0];          //PLC_手自动模式状态
                            ClsPLCValue.PlcValue.Plc_ResetFinished = opr.Content[2];        //PLC复位结束
                            ClsPLCValue.PlcValue.Plc_RequestTest_A = opr.Content[3];       //PLC_请求测定
                            ClsPLCValue.PlcValue.Plc_TestFinshReply_A = opr.Content[4];    //PLC_测定结束应答
                            ClsPLCValue.PlcValue.Plc_InitReply = opr.Content[7];           //PLC_初始化完成
                            ClsPLCValue.PlcValue.Plc_EmergencyStop = opr.Content[10];      //PLC_急停信号
                            ClsPLCValue.PlcValue.Plc_HaveTray = opr.Content[11];           //PLC表示托盘有无
                            ClsPLCValue.PlcValue.Plc_AutoStepNO = opr.Content[12];         //PLC_自动流程工步
                            ClsPLCValue.PlcValue.Plc_ResetStepNO = opr.Content[13];        //PLC_初始流程工步
                            ClsPLCValue.PlcValue.Plc_IO_ZeroIng = opr.Content[16];         //X轴回零中？
                            ClsPLCValue.PlcValue.Plc_StartReply = opr.Content[17];         //PLC启动应答
                            ClsPLCValue.PlcValue.Plc_On_Position = opr.Content[20];        //PLC_到位
                            ClsPLCValue.PlcValue.Plc_Period1 = opr.Content[21];            //PLC单次运动时间1
                            ClsPLCValue.PlcValue.Plc_Period2 = opr.Content[22];            //PLC单次运动时间2
                            ClsPLCValue.PlcValue.Plc_IO_ZeroCompletion = opr.Content[25];  //X轴回零完成
                            ClsPLCValue.PlcValue.Plc_CurrentPosition1 = opr.Content[32];   //X轴位置1信息
                            ClsPLCValue.PlcValue.Plc_CurrentPosition2 = opr.Content[33];   //X轴位置2信息
                            ClsPLCValue.PlcValue.Plc_CurrentPosition3 = opr.Content[34];   //X轴位置3信息
                            ClsPLCValue.PlcValue.Plc_CurrentPosition4 = opr.Content[35];   //X轴位置3信息
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
                        register = "V260";
                        arrLength = 2;
                        OperateResult<short[]> OperateResult = PlcTcpNet.ReadInt16(register, arrLength);
                        if (opr.IsSuccess)
                        {  //alarm信息存储
                            ClsPLCValue.PlcValue.Plc_Alarm1 = OperateResult.Content[0];
                            ClsPLCValue.PlcValue.Plc_Error1 = OperateResult.Content[0];
                            ClsPLCValue.PlcValue.Plc_Alarm2 = OperateResult.Content[1];
                            ClsPLCValue.PlcValue.Plc_Error2 = OperateResult.Content[1];
                        }
                        else
                        {
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }
                        //OperateResult<ushort[]> alarm = PlcTcpNet.ReadUInt16("W160", 2);
                        //if (opr.IsSuccess)
                        //{
                        //    ClsPLCValue.PlcValue.Plc_Alarm = this.CheckBit16(alarm.Content[0], 0);
                        //}
                        //else
                        //{
                        //    //读取异常 
                        //    if (opr.Message.Contains("连接失败"))
                        //    {
                        //        connectResult = new //HslCommunication.
                        //            OperateResult()
                        //            { IsSuccess = false };
                        //    }
                        //}
                        OperateResult<Byte[]> Uopr = PlcTcpNet.Read(mPlcAddr.PLC_输入IO1, 4);
                        if (opr.IsSuccess)
                        {
                            ClsPLCValue.PlcValue.Plc_IO_HomeLimit = this.CheckBit16(Uopr.Content[0], 0);            //原点
                            ClsPLCValue.PlcValue.Plc_IO_XAr = this.CheckBit16(Uopr.Content[0], 2);                  //X轴报警
                            ClsPLCValue.PlcValue.Plc_IO_PosLimit = this.CheckBit16(Uopr.Content[0], 4);             //X轴正极限
                            ClsPLCValue.PlcValue.Plc_IO_NegLimit = this.CheckBit16(Uopr.Content[0], 5);             //X轴负极限
                            ClsPLCValue.PlcValue.Plc_IO_PosCylUp = this.CheckBit16(Uopr.Content[1], 0);             //前顶升气缸伸
                            ClsPLCValue.PlcValue.Plc_IO_PosCylDown = this.CheckBit16(Uopr.Content[1], 1);           //前顶气缸升缩
                            ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 = this.CheckBit16(Uopr.Content[1], 2);            //后顶升气缸伸
                            ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 = this.CheckBit16(Uopr.Content[1], 3);          //后顶升气缸缩                            
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose = this.CheckBit16(Uopr.Content[1], 4);        //阻挡气缸伸
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen = this.CheckBit16(Uopr.Content[1], 5);         //阻挡气缸缩
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 = this.CheckBit16(Uopr.Content[1], 6);        //探针压合伸
                            ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 = this.CheckBit16(Uopr.Content[1], 7);         //探针压合缩
                            ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = this.CheckBit16(Uopr.Content[2], 0);         //进料口托盘检测（托盘入口）
                            ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = this.CheckBit16(Uopr.Content[2], 2);             //允许出盘
                            ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = this.CheckBit16(Uopr.Content[2], 3);           //请求进盘
                            //ClsPLCValue.PlcValue.Plc_IO_YanwuUp = this.CheckBit16(Uopr.Content[1], 12);              //烟雾传感器前
                            //ClsPLCValue.PlcValue.Plc_IO_YanwuDown = this.CheckBit16(Uopr.Content[1], 13);            //烟雾传感器后
                            ClsPLCValue.PlcValue.Plc_IO_Opendoor = this.CheckBit16(Uopr.Content[2], 6);             //门开关
                            ClsPLCValue.PlcValue.Plc_IO_Stop = this.CheckBit16(Uopr.Content[3], 0);                  //急停
                            //ClsPLCValue.PlcValue.Plc_IO_Alaguntong = this.CheckBit16(Uopr.Content[2], 1);            //滚筒报警
                            ClsPLCValue.PlcValue.Plc_IO_jiansuguandian = this.CheckBit16(Uopr.Content[3], 2);        //减速关电
                            ClsPLCValue.PlcValue.Plc_IO_jiantuopandaowei = this.CheckBit16(Uopr.Content[3], 3);      //托盘到位检测
                            ClsPLCValue.PlcValue.Plc_IO_TrayOutputSignal = this.CheckBit16(Uopr.Content[3], 4);      //托盘出料检测
                            ClsPLCValue.PlcValue.Plc_IO_tuopanrudingqian = this.CheckBit16(Uopr.Content[3], 5);      //托盘入定位销检测（前）
                            ClsPLCValue.PlcValue.Plc_IO_tuopanrudinghou = this.CheckBit16(Uopr.Content[3], 6);       //托盘入定位销检测（后）
                            ClsPLCValue.PlcValue.Plc_IO_qiyajiance = this.CheckBit16(Uopr.Content[3], 7);            //气压检测
                        }
                        else
                        {
                            //读取异常 
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult()
                                { IsSuccess = false };
                            }
                        }
                        Uopr = PlcTcpNet.Read("V250", 3);
                        if (opr.IsSuccess)
                        {
                            //IO
                            ClsPLCValue.PlcValue.Plc_IO_BlockCylUp = this.CheckBit16(Uopr.Content[0], 4);   //探针压合伸
                            ClsPLCValue.PlcValue.Plc_IO_BlockCylDown = this.CheckBit16(Uopr.Content[0], 5); //探针压合缩
                            ClsPLCValue.PlcValue.Plc_IO_CVRun = this.CheckBit16(Uopr.Content[0], 6);
                            ClsPLCValue.PlcValue.Plc_IO_CVRunback = this.CheckBit16(Uopr.Content[0], 7);
                            ClsPLCValue.PlcValue.Plc_IO_M1 = this.CheckBit16(Uopr.Content[1], 0);  //M1
                            ClsPLCValue.PlcValue.Plc_IO_M2 = this.CheckBit16(Uopr.Content[1], 1);    //M2
                            ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow = this.CheckBit16(Uopr.Content[1], 2);  //允许进料
                            ClsPLCValue.PlcValue.Plc_IO_BhOCVReq = this.CheckBit16(Uopr.Content[1], 3);    //请求出料
                        }
                        else
                        { //读取异常 
                            if (opr.Message.Contains("连接失败"))
                            {
                                connectResult = new OperateResult() { IsSuccess = false };
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            //PlcTcpNet.Dispose();
                            PlcTcpNet.ConnectClose();
                            connectResult = PlcTcpNet.ConnectServer(); //连接PLC
                            if (!connectResult.IsSuccess) //若连接失败
                            {
                                ClsPLCValue.connectSuccess = false;
                                ClsPLCValue.PlcValue.Plc_AutoManual = -1;          //PLC_手自动模式状态
                                ClsPLCValue.PlcValue.Plc_ResetFinished = -1;        //PLC复位结束
                                ClsPLCValue.PlcValue.Plc_RequestTest_A = -1;       //PLC_请求测定
                                ClsPLCValue.PlcValue.Plc_TestFinshReply_A = -1;    //PLC_测定结束应答
                                ClsPLCValue.PlcValue.Plc_InitReply = -1;           //PLC_初始化完成
                                ClsPLCValue.PlcValue.Plc_EmergencyStop = -1;      //PLC_急停信号
                                ClsPLCValue.PlcValue.Plc_HaveTray = -1;           //PLC表示托盘有无
                                ClsPLCValue.PlcValue.Plc_AutoStepNO = -1;         //PLC_自动流程工步
                                ClsPLCValue.PlcValue.Plc_ResetStepNO = -1;        //PLC_初始流程工步
                                ClsPLCValue.PlcValue.Plc_IO_ZeroIng = -1;         //X轴回零中？
                                ClsPLCValue.PlcValue.Plc_StartReply = -1;         //PLC启动应答
                                ClsPLCValue.PlcValue.Plc_On_Position = -1;        //PLC_到位
                                ClsPLCValue.PlcValue.Plc_Period1 = -1;            //PLC单次运动时间1
                                ClsPLCValue.PlcValue.Plc_Period2 = -1;            //PLC单次运动时间2
                                ClsPLCValue.PlcValue.Plc_IO_ZeroCompletion = -1;  //X轴回零完成
                                ClsPLCValue.PlcValue.Plc_CurrentPosition1 = -1;   //X轴位置1信息
                                ClsPLCValue.PlcValue.Plc_CurrentPosition2 = -1;   //X轴位置2信息
                                ClsPLCValue.PlcValue.Plc_CurrentPosition3 = -1;   //X轴位置3信息
                                ClsPLCValue.PlcValue.Plc_CurrentPosition4 = -1;   //X轴位置4信息

                                ClsPLCValue.PlcValue.Plc_Alarm1 = -1;
                                ClsPLCValue.PlcValue.Plc_Error1 = -1;
                                ClsPLCValue.PlcValue.Plc_Alarm2 = -1;
                                ClsPLCValue.PlcValue.Plc_Error2 = -1;

                                ClsPLCValue.PlcValue.Plc_IO_BlockCylUp = -1;   //探针压合伸
                                ClsPLCValue.PlcValue.Plc_IO_BlockCylDown = -1; //探针压合缩
                                ClsPLCValue.PlcValue.Plc_IO_CVRun = -1;
                                ClsPLCValue.PlcValue.Plc_IO_CVRunback = -1;
                                ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow = -1;  //允许进料
                                ClsPLCValue.PlcValue.Plc_IO_BhOCVReq = -1;    //请求出料

                                ClsPLCValue.PlcValue.Plc_IO_HomeLimit = -1;            //原点
                                ClsPLCValue.PlcValue.Plc_IO_XAr = -1;                  //X轴报警
                                ClsPLCValue.PlcValue.Plc_IO_PosLimit = -1;             //X轴正极限
                                ClsPLCValue.PlcValue.Plc_IO_NegLimit = -1;             //X轴负极限
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp = -1;             //前顶升气缸伸
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown = -1;           //前顶气缸升缩
                                ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 = -1;            //后顶升气缸伸
                                ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 = -1;          //后顶升气缸缩                            
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose = -1;        //阻挡气缸伸
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen = -1;         //阻挡气缸缩
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 = -1;        //探针压合伸
                                ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 = -1;         //探针压合缩
                                ClsPLCValue.PlcValue.Plc_IO_TrayForSignal = -1;         //进料口托盘检测（托盘入口）
                                ClsPLCValue.PlcValue.Plc_IO_BhCVAllow = -1;             //允许出盘
                                ClsPLCValue.PlcValue.Plc_IO_FrCVRequest = -1;           //请求进盘
                                //ClsPLCValue.PlcValue.Plc_IO_YanwuUp = this.CheckBit16(Uopr.Content[1], 12);              //烟雾传感器前
                                //ClsPLCValue.PlcValue.Plc_IO_YanwuDown = this.CheckBit16(Uopr.Content[1], 13);            //烟雾传感器后
                                ClsPLCValue.PlcValue.Plc_IO_Opendoor = -1;             //门开关
                                ClsPLCValue.PlcValue.Plc_IO_Stop = -1;                  //急停
                                //ClsPLCValue.PlcValue.Plc_IO_Alaguntong = this.CheckBit16(Uopr.Content[2], 1);            //滚筒报警
                                ClsPLCValue.PlcValue.Plc_IO_jiansuguandian = -1;        //减速关电
                                ClsPLCValue.PlcValue.Plc_IO_jiantuopandaowei = -1;      //托盘到位检测
                                ClsPLCValue.PlcValue.Plc_IO_TrayOutputSignal = -1;      //托盘出料检测
                                ClsPLCValue.PlcValue.Plc_IO_tuopanrudingqian = -1;      //托盘入定位销检测（前）
                                ClsPLCValue.PlcValue.Plc_IO_tuopanrudinghou = -1;       //托盘入定位销检测（后）
                                ClsPLCValue.PlcValue.Plc_IO_qiyajiance = -1;            //气压检测
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
                catch (Exception)
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

        public void ReadDB(string DeviceNameBlock, out byte Val)
        {
            try
            {
                OperateResult<byte[]> result = PlcTcpNet.Read(DeviceNameBlock, 1);
                if (result.IsSuccess)
                {
                    Val = result.Content[0];
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

        public void WriteDB(string DeviceNameBlock, byte Val)
        {

            try
            {
                OperateResult result = PlcTcpNet.Write(DeviceNameBlock, Val);
                if (result.IsSuccess)
                {

                }
                else
                {
                    throw new Exception($"[{DeviceNameBlock}] 写入异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");

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
                    throw new Exception($"[{DeviceNameBlock}] 写入异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");

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
                    throw new Exception($"[{DeviceNameBlock}] 读取异常 {Environment.NewLine}Reason：{result.ToMessageShowString()}");
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
        public void Set_CylBlock_Down()
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
                PlcTcpNet.Write(ClsGlobal.mPLCContr.mPlcAddr.PC_针床气缸控制, 2);

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
                OperateResult<byte[]> read = PlcTcpNet.Read(mPlcAddr.PLC_到位, 1);
                tempVal = read.Content[0];
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
                if (num == 10 && currPos == ClsGlobal.SetPos10)
                {
                    bInPlace = true;
                }
                if (num == 11 && currPos == ClsGlobal.SetPos11)
                {
                    bInPlace = true;
                }
                if (num == 12 && currPos == ClsGlobal.SetPos12)
                {
                    bInPlace = true;
                }
                if (num == 13 && currPos == ClsGlobal.SetPos13)
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
            ushort tempVal;
            try
            {
                OperateResult<byte[]> read = PlcTcpNet.Read(mPlcAddr.PLC_输入IO1, 1);
                tempVal = read.Content[0];
                if ((this.CheckBit16(tempVal, 4) == 0))
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
            ushort tempVal;
            try
            {
                OperateResult<byte[]> read = PlcTcpNet.Read(mPlcAddr.PLC_输入IO1, 1);
                tempVal = read.Content[0];
                if ((this.CheckBit16(tempVal, 0) == 1))
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
            ushort tempVal;
            try
            {
                OperateResult<byte[]> read = PlcTcpNet.Read(mPlcAddr.PLC_输入IO1, 1);
                tempVal = read.Content[0];
                if ((this.CheckBit16(tempVal, 5) == 0))
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
                OperateResult<byte[]> read = PlcTcpNet.Read(mPlcAddr.PLC_回零中, 1);
                tempVal = read.Content[0];
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
                OperateResult<byte[]> read = PlcTcpNet.Read(mPlcAddr.PLC_回零完成, 1);
                tempVal = read.Content[0];
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
                PlcTcpNet.Write(mPlcAddr.PC_指示回零, mSet);
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
                PlcTcpNet.Write(mPlcAddr.PC_指示回零, mReSet);
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
            short i;
            int tempVal;
            if (speed < 0 || speed > MaxSpeed)
            {
                return;
            }
            i = speed;
            try
            {
                OperateResult<short[]> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度, 2);
                tempVal = (ushort)read.Content[1] +(ushort)
                          read.Content[0] * 65536;
                if (i != tempVal)
                {
                    DevMove_ChangeSpeed(i);
                    //PlcTcpNet.Write(mPlcAddr.PC_速度, i);
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
            short i;
            int tempVal;
            if (speed < 0 || speed > MaxSpeed)
            {
                return;
            }
            i = speed;
            try
            {
                OperateResult<short[]> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度, 2);
                tempVal = (ushort)read.Content[1] +
                          (ushort)read.Content[0] * 65536;
                if (i != tempVal)
                {
                    DevMove_ChangeSpeed(i);
                    //PlcTcpNet.Write(mPlcAddr.PC_速度, i);
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
        public void DevMove_ChangeSpeed(int speed)
        {
            if (speed <= 0 || speed > MaxSpeed)
            {
                return;
            }
            try
            {
                short[] tSpeed = new short[2];
                tSpeed[1] = (short)(speed & 0xffff);
                tSpeed[0] = (short)(speed / 0xffff);
                PlcTcpNet.Write(mPlcAddr.PC_速度, tSpeed);
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
        public int DevMove_CurrentSpeed()
        {
            int tempVal;
            try
            {
                OperateResult<short[]> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度, 2);
                tempVal = (ushort)read.Content[1] +
                          (ushort)read.Content[0] * 65536;
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
        public void DevMove_ChangeAccTime(int mAccTime)
        {
            if (mAccTime < 0 || mAccTime > 1000)
            {
                return;
            }
            try
            {
                short[] tAcctime = new short[2];
                tAcctime[1] = (short)(mAccTime & 0xffff);
                tAcctime[0] = (short)(mAccTime / 0xffff);
                PlcTcpNet.Write(mPlcAddr.PC_加减速时间, tAcctime);
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
        public int DevMove_CurrentAccTime()
        {
            int tempVal;
            try
            {
                OperateResult<short[]> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_加减速时间, 2);
                tempVal = (ushort)read.Content[1] + (ushort)read.Content[0] * 65536;
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
                if (pos < -1000 || pos > 300000 || num < 1 || num > 13)
                {
                    return;
                }
                short[] sPos = new short[2];
                sPos[1] = (short)(pos & 0xffff);
                sPos[0] = (short)(pos / 0xffff);
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
                    case 10:
                        PlcTcpNet.Write(mPlcAddr.PC_POS10, sPos);
                        break;
                    case 11:
                        PlcTcpNet.Write(mPlcAddr.PC_POS11, sPos);
                        break;
                    case 12:
                        PlcTcpNet.Write(mPlcAddr.PC_POS12, sPos);
                        break;
                    case 13:
                        PlcTcpNet.Write(mPlcAddr.PC_POS13, sPos);
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
                OperateResult<short[]> read = null;

                if (num < 1 || num > 13)
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
                    case 10:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS10, 2);
                        break;
                    case 11:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS11, 2);
                        break;
                    case 12:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS12, 2);
                        break;
                    case 13:
                        read = PlcTcpNet.ReadInt16(mPlcAddr.PC_1POS13, 2);
                        break;
                    default:
                        tempVal = 0;
                        break;
                }
                tempVal1 = read.Content;
                tempVal = (ushort)tempVal1[1] + (ushort)tempVal1[0] * 65536;
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
                tempVal1 = read.Content;
                tempVal = (ushort)tempVal1[0] + tempVal1[1] * 65536;
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
        public void DevMove_Inc(int speed, int position)
        {
            int i, j;
            int tempVal;

            if (speed < 0 || speed > MaxSpeed || position < -300000 || position > 300000 || position == 0)
            {
                return;
            }
            i = speed;
            try
            {
                OperateResult<short[]> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度, 2);
                tempVal = (ushort)read.Content[1] +
                                 (ushort) read.Content[0] * 65536;
                if (i != tempVal && i != 0)
                {
                    DevMove_ChangeSpeed(i);
                   //PlcTcpNet.Write(mPlcAddr.PC_速度, i);
                    Thread.Sleep(10);
                }
                j = position;
                OperateResult<short[]> read1 = PlcTcpNet.ReadInt16(mPlcAddr.PC_运动值, 2);
                tempVal = (short)read1.Content[1] +
                                 (short) read1.Content[0] * 65536;
                if (j != tempVal)
                {
                    short[] tMovePosition = new short[2];
                    tMovePosition[1] = (short)(j & 0xffff);
                    tMovePosition[0] = (short)(j / 0xffff);
                    PlcTcpNet.Write(mPlcAddr.PC_运动值, tMovePosition);
                    //PlcTcpNet.Write(mPlcAddr.PC_运动值, j);
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
        public void DevMove_AbsNO(int speed, short posNO)
        {
            int i, tempVal;
            byte j;

            if (speed < 0 || speed > MaxSpeed || posNO < 0 || posNO > 13)
            {
                return;
            }

            i = speed;
            try
            {
                // PlcTcpNet.Write(mPlcAddr.PC_启动, mReSet);
                OperateResult<short[]> read = PlcTcpNet.ReadInt16(mPlcAddr.PC_速度, 2);
                OperateResult<byte[]> read1 = PlcTcpNet.Read(mPlcAddr.PC_坐标, 1);
                tempVal = (ushort)read.Content[1] +
                              (ushort)read.Content[0] * 65536;
                if (i != tempVal && i != 0)
                {
                    DevMove_ChangeSpeed(i);
                    //PlcTcpNet.Write(mPlcAddr.PC_速度, i);
                    Thread.Sleep(10);
                }

                j =(byte) posNO;
                read1 = PlcTcpNet.Read(mPlcAddr.PC_坐标, 1);
                tempVal = read.Content[0];
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
