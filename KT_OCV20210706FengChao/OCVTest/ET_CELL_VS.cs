using System;

namespace OCV
{
    [Serializable]
    public class ET_CELL_VS
    {
        //Cell_ID
        public string Cell_ID;

        //Pallet_ID
        public string Pallet_ID;

        //Cell_Position
        public int Cell_Position;

        //容量
        public string Capacity;

        //休眠电压
        public string SleepVolt;

        //当前测试得到的正负极电压    
        public string OCV_Now;

        //当前测试得到的壳体电压(如果有的话)
        public string OCV_Shell_Now;

        //当前测试的内阻     
        public string ACIR_Now;

        //当前的电压对比
        public string VoltDrop_Now;

        //压降(OCV1-OCV2)
        public string VoltDrop_1_2;

        //OCV1电压/内阻
        public string OCV_1;
        public string ACIR_1;

        //OCV1 温度 岗位编号
        public string Temp_1;
        public string RESRCE_1;

        //OCV2电压/内阻
        public string OCV_2;
        public string ACIR_2;

        //电压修正值
        public string Rev_OCV;

        //读取测试时间
        public string FlowEndTime;       //分容时间
        public string TEST_DATE;
        public string TEST_DATE2;

        //K值
        public double K_Now;             //当前OCV类型下，测试的K值
        public double K_1_2, K_2_3;      //OCV1-2的K值，OCV2-3的K值

        //状态: 0.正常 1，2，3，4 NG.
        public char NGStatus;

        //状态: 合格   不合格  用于写数据库
        public string NGSt;

        //历史NG原因  用于写数据库
        public string HNGSt;

        //NG原因代码
        public string NGReason;

        //NG原因描述 用于写数据库
        public string NGRea;

        //OCV1_T    （温度）
        public double TMP;

        public string BATCH_NO;     //批号
        public string MODEL_NO;     //电池型号
        public string PROJECT_NO;   //项目编号

        public string ISOLATION;   //电池是否隔离   隔离：1    不隔离：空值


        //OCV_Write_Time  
        public string OCV_Write_Time;

        //ACIR_Write_Time  
        public string ACIR_Write_Time;

        //END_DATE_TIME  
        public string End_Write_Time;

    }
}
