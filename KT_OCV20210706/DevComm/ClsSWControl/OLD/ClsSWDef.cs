namespace ClsDevComm
{
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
        //设备状态
        public const byte AddrH_DevState = 0;
        public const byte AddrL_DevState = 0;

        //初始化命令
        public const byte AddrH_DevInit_CMD = 0;
        public const byte AddrL_DevInit_CMD = 1;

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



    }
}
