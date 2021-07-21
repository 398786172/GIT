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

        public string PC_手自动模式;             //无需定义，直接地址操作
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
        public string PC_气缸控制;
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
        public string PC_自动流程针床气缸控制; //新增
        public string PC_跨线式;         //新增
        public string PC_指示回零;       //新增
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
        public string PC_POS10;
        public string PC_POS11;
        public string PC_POS12;
        public string PC_POS13;

        public string PC_1POS1;
        public string PC_1POS2;
        public string PC_1POS3;
        public string PC_1POS4;
        public string PC_1POS5;
        public string PC_1POS6;
        public string PC_1POS7;
        public string PC_1POS8;
        public string PC_1POS9;
        public string PC_1POS10;
        public string PC_1POS11;
        public string PC_1POS12;
        public string PC_1POS13;
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
        public string PLC_输入IO4;
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
            PC_手自动模式 = "V100";
            PC_指示清除报警 = "V102";
            PC_应答检测请求 = "V103";
            PC_指示测定结束 = "V104";
            PC_整体复位 = "V105";
            PC_自动流程启动 = "V106";
            PC_初始化 = "V107";
            PC_指示暂停 = "V108";
            PC_报警复位 = "V102";

            PC_指示上位机有报警 = "V117";
            PC_顶升气缸控制 = "V118";
            PC_阻挡气缸控制 = "V119";
            PC_针床气缸控制 = "V120";
            PC_滚筒控制 = "V121";
            PC_允许送入信号控制 = "V122";
            PC_允许送出信号控制 = "V123";
            PC_红 = "V124";
            PC_黄 = "V125";
            PC_绿 = "V126";
            PC_蜂鸣器 = "V127";
            PC_屏蔽蜂鸣器 = "V128";
            PC_屏蔽门开关 = "V129";
            PC_滚筒M1M2 = "V130";
            PC_自动流程针床气缸控制 = "V132";
            PC_跨线式 = "V133";
            PC_指示回零 = "V140";
            PC_相对坐标启动 = "V141";
            PC_运动值 = "V142";        //（相对坐标）
            PC_速度 = "V146";
            PC_启动 = "V151";    //坐标号启动
            PC_坐标 = "V152";    //坐标号
            PC_正转 = "V153";
            PC_反转 = "V154";
            PC_加减速时间 = "V155";

            PC_POS1 = "V160";
            PC_POS2 = "V164";
            PC_POS3 = "V168";
            PC_POS4 = "V172";
            PC_POS5 = "V176";
            PC_POS6 = "V180";
            PC_POS7 = "V184";
            PC_POS8 = "V188";
            PC_POS9 = "V192";
            PC_POS10 = "V196";
            PC_POS11 = "V200";
            PC_POS12 = "V204";
            PC_POS13 = "V208";

            PC_1POS1 = "V160";
            PC_1POS2 = "V164";
            PC_1POS3 = "V168";
            PC_1POS4 = "V172";
            PC_1POS5 = "V176";
            PC_1POS6 = "V180";
            PC_1POS7 = "V184";
            PC_1POS8 = "V188";
            PC_1POS9 = "V192";
            PC_1POS10 = "V196";
            PC_1POS11 = "V200";
            PC_1POS12 = "V204";
            PC_1POS13 = "V208";

            //PLC Register
            PLC_手自动模式="V200";
            PLC_托盘到位 = "V201";
            PLC_请求测定 = "V203";          //1:有请求
            PLC_测定结束应答 ="V204";       //应答
            PLC_初始化完成 = "V207";
            PLC_急停信号="V210";            //1:无 0:有
            PLC_装载托盘有无="V211";        //托盘有无 0:无 1:有
            PLC_自动流程工步 = "V212";
            PLC_初始流程工步 = "V213";
            PLC_回零中 = "V216";
            PLC_启动应答 = "V217";
            PLC_到位 = "V220";
            单次运行时间 = "V221";
            PLC_回零完成 = "V225";
            PLC_当前坐标 = "V230";
            //
            PLC_输入IO1 = "V240";
            PLC_输入IO2 = "V241";
            PLC_输入IO3 = "V242";
            PLC_输入IO3 = "V243";

            PLC_原点 = "V240.0";
            PLC_X_alm = "V240.2";
            PLC_正限位 = "V240.4";
            PLC_负限位 = "V240.5";

            PLC_前顶升气缸伸 = "V241.0";
            PLC_前顶升气缸缩 = "V241.1";
            PLC_门禁开关 = "V242.6";

            PLC_急停 = "V243.0";
            PLC_托盘到位 = "V243.3";

            //
            PLC_报警 = "V260";

            PLC_总警报 = "V260.0";
            PLC_急停按钮 = "V260.1";
            PLC_气缸警报 = "V260.2";
            PLC_电机警报 = "V260.3";


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
