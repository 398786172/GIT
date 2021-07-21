using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCV
{
    public class ClsIODef
    {
        //ON or OFF (Normal)
        public const byte DATA1_ON = 0xFF;
        public const byte DATA2_ON = 0x0;

        public const byte DATA1_OFF = 0x0;
        public const byte DATA2_OFF = 0x0;

        //功能码
        public const byte FUNC_READ_COILS = 0x01;
        public const byte FUNC_READ_DISCRETES = 0x02;
        public const byte FUNC_READ_HOLDINGREG = 0x03;
        public const byte FUNC_READ_INPUTREG = 0x04;

        public const byte FUNC_WRITE_COILS = 0x05;
        public const byte FUNC_WRITE_HOLDINGREG = 0x06;
        public const byte FUNC_WRITE_HOLDINGREGS_MULTI = 0x10;

        //定义
        //上位机
        public const ushort Addr_HOST_HeartBeat = 31;            //联机确认(心跳)
        public const ushort Addr_HOST_ReqClearAlarm = 40;        //报警复位请求
        public const ushort Addr_HOST_ReqReset = 41;             //设备复位

        public const ushort Addr_HOST_ReqPressPB = 44;           //针床压合请求
        public const ushort Addr_HOST_TestFinish = 47;           //测试完成

        public const ushort Addr_HOST_Debug = 49;                //调试状态
        public const ushort Addr_HOST_TrayClose = 50;                //托盘夹紧
        public const ushort Addr_HOST_TrayDown = 51;                //托盘下降
        public const ushort Addr_HOST_TrayPush = 52;                //托盘推入
        public const ushort Addr_HOST_PBPress = 53;            //探针压合

        public const ushort Addr_HOST_DebugGreenLight = 55;      //双色灯绿灯
        public const ushort Addr_HOST_DebugRedLight = 54;        //双色灯红灯

        public const ushort Addr_HOST_TowerGreenLight = 58;      //灯塔绿灯
        public const ushort Addr_HOST_TowerRedLight = 56;        //灯塔红灯
        public const ushort Addr_HOST_TowerOrangeLight = 57;        //灯塔橙色灯
        public const ushort Addr_HOST_Busser = 59;        //蜂鸣器

        //public const ushort Addr_HOST_ReqBattType = 41;          //压合电池类型要求
        //public const ushort Addr_HOST_PressTimes = 42;           //针床压合次数

        //public const ushort Addr_HOST_ReqOpenPB = 43;            //针床打开请求

        //public const ushort Addr_HOST_Debug = 49;                //调试状态
        //public const ushort Addr_HOST_DebugOpenOrPressPB = 46;   //调试_针床打开压合
        //public const ushort Addr_HOST_DebugGreenLight = 47;      //调试_绿灯亮
        //public const ushort Addr_HOST_DebugRedLight = 48;        //调试_红灯亮

        //针床
        //public const ushort Addr_MECHA_DEVNo = 128;              //设备号
        public const ushort Addr_MECHA_HeartBeat = 159;          //针床联机确认(心跳)
        public const ushort Addr_MECHA_DevState = 160;           //设备状态
        public const ushort Addr_MECHA_Alarm = 161;             //报警状态
        public const ushort Addr_MECHA_TrayIn = 162;             //托盘有无
        public const ushort Addr_MECHA_TrayClose = 163;            //托盘夹住
        public const ushort Addr_MECHA_TrayPush = 164;      //托盘推进
        public const ushort Addr_MECHA_PressPB = 165;     //针床压合
        public const ushort Addr_MECHA_TrayDown = 166;          //托盘下降

        public const ushort Addr_MECHA_AlarmReset = 168;        //报警清除应答
        public const ushort Addr_MECHA_DevReset = 169;         //设备复位应答
        public const ushort Addr_MECHA_IsWork = 172;          //工作应答
        public const ushort Addr_MECHA_TestFinish = 175;             //测试结束应答
        public const ushort Addr_MECHA_Debug = 177;             //调试状态应答
        public const ushort Addr_MECHA_Input1 = 179;            //输入信息1
        public const ushort Addr_MECHA_Input2 = 180;            //输入信息2
        public const ushort Addr_MECHA_Output1 = 182;            //输出信息1
        public const ushort Addr_MECHA_Output2 = 183;            //输出信息2

    }
}
