using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCV
{
    class ClsSWDef
    {
        #region 公共部分命令
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
        //设备状态
        public const byte AddrH_DevState = 0;
        public const byte AddrL_DevState = 0;

        //初始化命令
        public const byte AddrH_DevInit_CMD = 0;
        public const byte AddrL_DevInit_CMD = 1; 
        #endregion

        #region 坑梓C42改造版本切换板命令

        //切换测量电压命令
        public const byte AddrH_SW1_DMM_Volt_CMD = 0;   //切换测量电压命令
        public const byte AddrL_SW1_DMM_Volt_CMD = 7;

        public const byte AddrH_SW1_POSConnType = 0;      //正极对负极, 壳体对负极
        public const byte AddrL_SW1_POSConnType = 8;

        public const byte AddrH_SW1_CH = 0;       //通道号
        public const byte AddrL_SW1_CH = 9;


        //切换测量内阻命令
        public const byte AddrH_SW2_IR_CMD = 0; //切换测量内阻命令
        public const byte AddrL_SW2_IR_CMD = 12;

        public const byte AddrH_SW2_IRDevType = 0;   //内阻仪BT3, 内阻仪BT4
        public const byte AddrL_SW2_IRDevType = 13;

        public const byte AddrH_SW2_CH = 0;     //通道号
        public const byte AddrL_SW2_CH = 14;


        //同步切换测量命令
        public const byte AddrH_SW3_DOUBLE_DMM_Volt_CMD = 0;    //同步切换测量命令
        public const byte AddrL_SW3_DOUBLE_DMM_Volt_CMD = 17;

        public const byte AddrH_SW3_TestType = 0;     //测量类型:  (A区正极对负极电压, B区测内阻) , (A区测内阻, B区测正极对负极电压)
        public const byte AddrL_SW3_TestType = 18;

        public const byte AddrH_SW3_A_CH = 0;       //A区通道号
        public const byte AddrL_SW3_A_CH = 19;

        public const byte AddrH_SW3_B_CH = 0;      //B区通道号    值范围: (A区最后通道号+1 ~ 最后的通道号)
        public const byte AddrL_SW3_B_CH = 20;

        #endregion

        #region 宝龙版本切换板命令

        //测试信号选通命令 
        public const byte AddrH_StoD_CMD = 0;
        public const byte AddrL_StoD_CMD = 4;

        public const byte AddrH_StoD_Region = 0;
        public const byte AddrL_StoD_Region = 5;

        public const byte AddrH_StoD_TestType = 0;
        public const byte AddrL_StoD_TestType = 6;

        //正极接入命令
        public const byte AddrH_PosIn_CMD = 0;
        public const byte AddrL_PosIn_CMD = 9;

        public const byte AddrH_PosIn_Region = 0;
        public const byte AddrL_PosIn_Region = 10;

        public const byte AddrH_PosIn_TestType = 0;
        public const byte AddrL_PosIn_TestType = 11;


        //切换命令(1型)
        public const byte AddrH_SW1_CMD = 0;
        public const byte AddrL_SW1_CMD = 14;

        public const byte AddrH_SW1_SWType = 0;      //切换类型: 单1 /双 2
        public const byte AddrL_SW1_SWType = 15;

        public const byte AddrH_SW1_TestType = 0;    //测试类型: 测电压-> 1 , 测内阻-> 2 , 不测试-> 0
        public const byte AddrL_SW1_TestType = 16;

        public const byte AddrH_SW1_Channel = 0;     //通道号
        public const byte AddrL_SW1_Channel = 17;

        //切换命令(2型) 
        public const byte AddrH_SW2_CMD = 0;
        public const byte AddrL_SW2_CMD = 19;

        public const byte AddrH_SW2_SWType = 0;      //切换类型: 正负极->1， 壳体->2
        public const byte AddrL_SW2_SWType = 20;

        public const byte AddrH_SW2_Channel = 0;     //通道号
        public const byte AddrL_SW2_Channel = 21;


        //切换命令(3型)   对应接线:只测正负极电压
        public const byte AddrH_SW3_CMD = 0;
        public const byte AddrL_SW3_CMD = 23;

        public const byte AddrH_SW3_Channel = 0;     //通道号
        public const byte AddrL_SW3_Channel = 24;
        #endregion
    }
}
