using System;
using System.Collections.Generic;


namespace OCV
{
    /// <summary>
    /// 单元地址
    /// 该设备分为A,B两个单元
    /// </summary>
    public class ClsPLCAddr
    {
        #region 定义

        public string PC_应答扫码请求;    //取消
        public string PC_扫码完成;        //取消

        public string PC_手自动模式;             //——无需定义，直接地址操作
        public string PC_指示清除报警;
        public string PC_报警复位;              //功能与PC_指示清除报警相同
        public string PC_应答检测请求;          //测试准备开始
        public string PC_指示测定结束;          //1:搬送 2:再测定
        public string PC_整体复位;              //--无需定义，直接地址操作
        public string PC_自动流程启动;  //新增
        public string PC_初始化;
        public string PC_指示暂停;      //新增

        public string PC_指示上位机有报警;
        public string PC_顶升气缸控制;
        public string PC_针床气缸控制;
        public string PC_滚筒控制;
        public string PC_允许送入信号控制;
        public string PC_允许送出信号控制;
        public string PC_红;
        public string PC_黄;
        public string PC_绿;
        public string PC_蜂鸣器;
        public string PC_屏蔽蜂鸣器;     //新增
        public string PC_屏蔽门开关;     //新增
        public string PC_滚筒M1M2;       //新增
        public string PC_伺服使能;       //新增

        public string PC_阻挡气缸控制;   //取消
        public string PC_取电针控制;     //取消

        //PC----------------------------------
        //PC control
        public string PC_相对坐标启动;
        public string PC_运动值;     //（相对坐标）
        public string PC_速度;
        public string PC_启动;       //坐标号启动
        public string PC_坐标;       //坐标号
        public string PC_正转;
        public string PC_反转;
        public string PC_加减速时间;

        public string PC_POS1;
        public string PC_POS2;
        public string PC_POS3;
        public string PC_POS4;
        public string PC_POS5;
        public string PC_POS6;
        public string PC_POS7;
        public string PC_POS8;
        public string PC_POS9;

        public string PC_1POS1;
        public string PC_1POS2;
        public string PC_1POS3;
        public string PC_1POS4;
        public string PC_1POS5;
        public string PC_1POS6;
        public string PC_1POS7;
        public string PC_1POS8;
        public string PC_1POS9;
        //
        public string PLC_手自动模式;
        public string PLC_托盘到位;            //PLC表示托盘有无
        public string PLC_请求测定;            //1:有请求
        public string PLC_测定结束应答;        //应答

        public string PLC_初始化完成;          //新增

        public string PLC_急停信号;            //1:无 0:有
        public string PLC_装载托盘有无;        //托盘有无 0:无 1:有

        public string PLC_自动流程工步;        //新增
        public string PLC_初始流程工步;        //新增

        public string PLC_回零中;
        public string PLC_启动应答;
        public string PLC_到位;
        public string 单次运行时间;            //新增
        public string PLC_回零完成;
        public string PLC_心跳信号;
        public string PLC_当前坐标;

        //
        public string PLC_输入IO1;
        public string PLC_原点;
        public string PLC_X_alm;  //X轴报警
        public string PLC_正限位;
        public string PLC_负限位;

        public string PLC_输入IO2;
        public string PLC_前顶升气缸伸;
        public string PLC_前顶升气缸缩;
        public string PLC_门禁开关;

        public string PLC_输入IO3;
        public string PLC_启动;
        public string PLC_急停;

        //
        public string PLC_报警;
        public string PLC_总警报;
        public string PLC_急停按钮;
        public string PLC_气缸警报;
        public string PLC_电机警报;


        //PLC TRAY CODE
        public string[] PLC_TrayNo;

        #endregion

        public ClsPLCAddr(string unit)
        {
            if (unit.Trim() == "A")
            {
                InitA();
            }
        }
        public ClsPLCAddr()
        {
            InitA();

        }

        public void InitA()
        {
            //PC control
            PC_手自动模式 = "W000";
            PC_指示清除报警 = "W002";
            PC_应答检测请求 = "W003";
            PC_指示测定结束 = "W004";
            PC_整体复位 = "W005";
            PC_自动流程启动 = "W006";
            PC_初始化 = "W007";

            PC_报警复位 = "W002";

            PC_指示上位机有报警 = "W011";
            PC_顶升气缸控制 = "W012";
            PC_针床气缸控制 = "W013";
            PC_滚筒控制 = "W014";
            PC_允许送入信号控制 = "W015";
            PC_允许送出信号控制 = "W016";
            PC_红 = "W017";
            PC_黄 = "W018";
            PC_绿 = "W019";
            PC_蜂鸣器 = "W01A";
            PC_屏蔽蜂鸣器 = "W01B";
            PC_屏蔽门开关 = "W01C";
            PC_滚筒M1M2 = "W01D";
            PC_伺服使能 = "W01E";

            //PC_阻挡气缸控制 = "W024";
            // PC_取电针控制 = "D15301";

            PC_相对坐标启动 = "W030";
            PC_运动值 = "W031";     //（相对坐标）
            PC_速度 = "W032";
            PC_启动 = "W033";    //坐标号启动
            PC_坐标 = "W034";    //坐标号
            PC_正转 = "W035";
            PC_反转 = "W036";
            PC_加减速时间 = "W037";

            PC_POS1 = "W40";
            PC_POS2 = "W42";
            PC_POS3 = "W44";
            PC_POS4 = "W46";
            PC_POS5 = "W48";
            PC_POS6 = "W4A";
            PC_POS7 = "W4C";
            PC_POS8 = "W4E";
            PC_POS9 = "W50";

            PC_1POS1 = "D4000";
            PC_1POS2 = "D4002";
            PC_1POS3 = "D4004";
            PC_1POS4 = "D4006";
            PC_1POS5 = "D4008";
            PC_1POS6 = "D4010";
            PC_1POS7 = "D4012";
            PC_1POS8 = "D4014";
            PC_1POS9 = "D4016";

            //PLC Register
            PLC_初始化完成 = "W107";
            PLC_自动流程工步 = "W10C";
            PLC_初始流程工步 = "W10D";
            单次运行时间 = "W115";
            PLC_手自动模式 = "W100";
            PLC_装载托盘有无 = "W101";        //托盘有无 0:无 1:有
            PLC_请求测定 = "W103";            //1:有请求
            PLC_测定结束应答 = "W104";        //应答
            PLC_急停信号 = "W10A";
            PLC_回零中 = "W110";
            PLC_启动应答 = "W111";
            PLC_到位 = "W114";
            PLC_回零完成 = "W118";
            PLC_心跳信号 = "W11C";
            PLC_当前坐标 = "W120";
            //
            PLC_输入IO1 = "W130";
            PLC_输入IO2 = "W131";
            PLC_输入IO3 = "W132";

            PLC_原点 = "W130.0";
            PLC_X_alm = "W130.2";
            PLC_正限位 = "W130.4";
            PLC_负限位 = "W130.5";

            PLC_前顶升气缸伸 = "W131.0";
            PLC_前顶升气缸缩 = "W131.1";
            PLC_门禁开关 = "W131.e";

            PLC_急停 = "W132.0";
            PLC_托盘到位 = "W132.3";

            //
            PLC_报警 = "W160";

            PLC_总警报 = "W160.0";
            PLC_急停按钮 = "W160.1";
            PLC_气缸警报 = "W160.2";
            PLC_电机警报 = "W160.3";


        }
    }

    public class AlarmInfo
    {
        public int[] arrValue = new int[16];

        public Dictionary<int, string> dictAlarm = new Dictionary<int, string>  {
        { 0, "E00紧急停止" },        { 1, "E01气圧异常" },
        { 2, "E02PC非常停止" },        { 3, "E03循环停止超时" },
        { 4, "E04搬送CV超时" },        { 5, "E05Tray升降超时" },
        { 6, "E06挡块超时" },        { 7, "E07Probe超时" },
        { 8, "E08机种切换1超时" },        { 9, "E09机种切换2超时" },
        { 10, "E10扫码汽缸超时" },        { 11, "E11PC应答异常" },
        { 12, "E12扫码读取异常" },        { 13, "E13測定完了等待超时" },
        { 14, "E14烟雾检测异常" },        { 15, "E15" },
        };

        public int E00紧急停止;
        public int E01气圧异常;
        public int E02PC非常停止;
        public int E03循环停止超时;
        public int E04搬送CV超时;
        public int E05Tray升降超时;
        public int E06挡块超时;
        public int E07Probe超时;
        public int E08机种切换1超时;
        public int E09机种切换2超时;

        public int E10扫码汽缸超时;
        public int E11PC应答异常;
        public int E12扫码读取异常;
        public int E13測定完了等待超时;
        public int E14烟雾检测异常;
        public int E15;


        public void GetAlarmInfo(short Val)
        {
            E00紧急停止 = CheckBit16((ushort)Val, 0);
            arrValue[0] = E00紧急停止;
            E01气圧异常 = CheckBit16((ushort)Val, 1);
            arrValue[1] = E01气圧异常;
            E02PC非常停止 = CheckBit16((ushort)Val, 2);
            arrValue[2] = E02PC非常停止;
            E03循环停止超时 = CheckBit16((ushort)Val, 3);
            arrValue[3] = E03循环停止超时;
            E04搬送CV超时 = CheckBit16((ushort)Val, 4);
            arrValue[4] = E04搬送CV超时;
            E05Tray升降超时 = CheckBit16((ushort)Val, 5);
            arrValue[5] = E05Tray升降超时;
            E06挡块超时 = CheckBit16((ushort)Val, 6);
            arrValue[6] = E06挡块超时;
            E07Probe超时 = CheckBit16((ushort)Val, 7);
            arrValue[7] = E07Probe超时;
            E08机种切换1超时 = CheckBit16((ushort)Val, 8);
            arrValue[8] = E08机种切换1超时;
            E09机种切换2超时 = CheckBit16((ushort)Val, 9);
            arrValue[9] = E09机种切换2超时;
            E10扫码汽缸超时 = CheckBit16((ushort)Val, 10);
            arrValue[10] = E10扫码汽缸超时;
            E11PC应答异常 = CheckBit16((ushort)Val, 11);
            arrValue[11] = E11PC应答异常;
            E12扫码读取异常 = CheckBit16((ushort)Val, 12);
            arrValue[12] = E12扫码读取异常;
            E13測定完了等待超时 = CheckBit16((ushort)Val, 13);
            arrValue[13] = E13測定完了等待超时;
            E14烟雾检测异常 = CheckBit16((ushort)Val, 14);
            arrValue[14] = E14烟雾检测异常;
            E15 = CheckBit16((ushort)Val, 15);
            arrValue[15] = E15;
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
