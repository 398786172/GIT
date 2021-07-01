using System;
using System.Collections.Generic;


namespace OCV
{
    /// <summary>
    /// 单元地址
    /// 该设备分为A,B两个单元
    /// </summary>
    public class ClsDevUnitAddr
    {
        #region 定义

        public string PC_手自动模式;
        public string PC_指示清除报警;
        public string PC_应答扫码请求;
        public string PC_扫码完成;
        public string PC_应答检测请求;      //测试准备开始
        public string PC_指示测定结束;          //1:搬送 2:再测定
        public string PC_指示上位机有报警;

        public string PC_整体复位;
        public string PC_自动流程启动;
        public string PC_初始化;
        
        ////public string PLC_复位应答;
        //public string PLC_手自动模式;
        //public string PLC_请求扫码;           //1:有更新
        //public string PLC_应答扫码完成;           //1:有更新
        //public string PLC_请求测定;            //1:有请求
        //public string PLC_测定结束应答;        //应答
        //public string PLC_装载托盘有无;        //托盘有无 0:无 1:有
        //public string PLC_急停信号;        // 1:无 0:有

        //public string PLC_整体复位完成;        

        //public string PLC_初始化完成;       

        public string PC_顶升气缸控制;
        public string PC_针床气缸控制;
        public string PC_阻挡气缸控制;
        public string PC_滚筒控制;
        public string PC_允许送入信号控制;
        public string PC_允许送出信号控制;
        public string PC_取电针控制;
        public string PC_红;
        public string PC_黄;
        public string PC_绿;
        public string PC_蜂鸣器;
        public string PC_屏蔽蜂鸣器;
        public string PC_屏蔽门开关;
        public string PC_M;
        //PLC TRAY CODE
        public string[] PLC_TrayNo;

        #endregion

        public ClsDevUnitAddr(string unit)
        {
            if (unit.Trim() == "A")
            {
                InitA();
            }
        }
        public ClsDevUnitAddr()
        {
             InitA();
            
        }
        public void InitA()
        {
            // //PC----------------------------------
            // //PC control
            PC_手自动模式 = "W00";
            PC_指示清除报警 = "W02";
            //PC_应答扫码请求 = "D15200";
            //PC_扫码完成 = "D15201";
            PC_应答检测请求 = "W03";
            PC_指示测定结束 = "W04";
            PC_整体复位 = "W05";
            PC_自动流程启动 = "W06";
            PC_初始化 = "W07";

            //PLC_手自动模式 = "W100";

            //PLC_装载托盘有无 = "W111";        //托盘有无 0:无 1:有

            //PLC_请求扫码 = "D15100";           //1:有更新
            //PLC_应答扫码完成 = "D15101";           //1:有更新
            //PLC_请求测定 = "D15102";            //1:有请求
            //PLC_测定结束应答 = "D15103";        //应答
            //PLC_急停信号 =  "D15301";
            PC_指示上位机有报警 = "W11";
            PC_顶升气缸控制 = "W12";
            PC_针床气缸控制 = "W13";
            //PC_阻挡气缸控制 = "D15301";
            PC_滚筒控制 = "W14";
            PC_允许送入信号控制 = "W15";
            PC_允许送出信号控制 = "W16";
            //PC_取电针控制 = "D15301";
            PC_红 = "W17";
            PC_黄 = "W18";
            PC_绿 = "W19";
            PC_蜂鸣器 = "W20";
            PC_屏蔽蜂鸣器 = "W21";
            PC_屏蔽门开关 = "W22";
            PC_M = "W23";
        }
    }

    public class AlarmInfo
    {
        public int[] arrValue = new int[16];

        public Dictionary<int, string> dictAlarm = new Dictionary<int, string>  {
        { 0, "E00总警报" }, 
        { 1, "E01急停按下" },
        { 2, "E02气缸警报" }, 
        { 3, "" },
        { 4, "" },
        { 5, "" }, 
        { 6, "" },  
        { 7, "E07气源压力不足" },
        { 8, "E08安全门打开" },
        { 9, "E09流道有电池请手动排出" },
        { 10, "E0A顶升时定位销未到位" }, 
        { 11, "E0B顶升气缸上异常" },
        { 12, "E0C顶升气缸下异常" },
        { 13, "E0D探针压缩气缸伸异常" },
        { 14, "E0E探针压缩气缸缩异常" },
        { 15, "" },
        };

        public void GetAlarmInfo(short Val)
        {
            arrValue[0]= CheckBit16((ushort)Val, 0);
            arrValue[1] = CheckBit16((ushort)Val, 1);
            arrValue[2] = CheckBit16((ushort)Val, 2);
            arrValue[3] = CheckBit16((ushort)Val, 3);
            arrValue[4] = CheckBit16((ushort)Val, 4);
            arrValue[5] = CheckBit16((ushort)Val, 5);
            arrValue[6] = CheckBit16((ushort)Val, 6);
            arrValue[7] = CheckBit16((ushort)Val, 7);
            arrValue[8] = CheckBit16((ushort)Val, 8);
            arrValue[9] = CheckBit16((ushort)Val, 9);
            arrValue[10] = CheckBit16((ushort)Val, 10);
            arrValue[11] = CheckBit16((ushort)Val, 11);
            arrValue[12] = CheckBit16((ushort)Val, 12);
            arrValue[13] = CheckBit16((ushort)Val, 13);
            arrValue[14] = CheckBit16((ushort)Val, 14);
            arrValue[15] = CheckBit16((ushort)Val, 15);
           
        }

        private int CheckBit16(UInt16 Data, int BitNum)
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
    }


}
