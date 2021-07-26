using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCV
{
    /// <summary>
    /// 支持4种测试类型:
    /// 测试类型1: 正负极电压+内阻
    /// 测试类型2: 正负极电压 + 负极对壳压
    /// 测试类型3: 只测试正负极电压
    /// 测试类型4: 正负极电压 + 内阻 + 负极对壳压  (20210520增) 
    /// </summary>
    class ClsSWDef
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

        //当前测试类型状态
        public const byte AddrH_DevSwStyle_State = 0;
        public const byte AddrL_DevSwStyle_State = 133;

        //测试类型使能设置
        public const byte AddrH_SetDevSwStyle_CMD = 0;          //设置测试类型
        public const byte AddrL_SetDevSwStyle_CMD = 136;

        public const byte AddrH_SetDevSwStyle_Choice = 0;
        public const byte AddrL_SetDevSwStyle_Choice = 137;

        public const byte AddrH_SetDevSwStyle_key = 0;
        public const byte AddrL_SetDevSwStyle_key = 138;

        //正极接入命令(单独调试正极接入,一般不用)
        public const byte AddrH_PosIn_CMD = 0;
        public const byte AddrL_PosIn_CMD = 9;

        public const byte AddrH_PosIn_Region = 0;
        public const byte AddrL_PosIn_Region = 10;

        public const byte AddrH_PosIn_TestType = 0;
        public const byte AddrL_PosIn_TestType = 11;


        //切换命令(1型)   对应接线:内阻和正负极电压
        public const byte AddrH_SW1_CMD = 0;
        public const byte AddrL_SW1_CMD = 14;

        public const byte AddrH_SW1_SWType = 0;      //切换类型: 单1 /双 2
        public const byte AddrL_SW1_SWType = 15;

        public const byte AddrH_SW1_TestType = 0;    //测试类型: 测电压-> 1 , 测内阻-> 2 , 不测试-> 0
        public const byte AddrL_SW1_TestType = 16;
        
        public const byte AddrH_SW1_Channel = 0;     //通道号
        public const byte AddrL_SW1_Channel = 17;

        //切换命令(2型)   对应接线:正负极电压与壳体
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

        //切换命令(4型)   对应接线:只测正负极电压
        public const byte AddrH_SW4_CMD = 0;
        public const byte AddrL_SW4_CMD = 26;

        public const byte AddrH_SW4_SWType = 0;      //切换类型: 1:正负极电压，2:内阻  3：壳体对负极电压
        public const byte AddrL_SW4_SWType = 27;

        public const byte AddrH_SW4_Channel = 0;     //通道号
        public const byte AddrL_SW4_Channel = 28;




    }
}
