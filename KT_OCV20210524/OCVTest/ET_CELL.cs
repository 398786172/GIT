using System;

namespace OCV
{
    [Serializable]
    public class ET_CELL
    {
        //Cell_ID
        public string Cell_ID;

        //Pallet_ID
        public string Pallet_ID;

        //Cell_Position
        public int Cell_Position;

        //当前测试得到的正负极电压    
        public double OCV_Now;

        //当前测试得到的壳体电压(如果有的话)
        public double OCV_Shell_Now;

        //当前测试得到的内阻
        public double ACIR_Now;

        //当前的压降
        public double VoltDrop_Now;
       
        public double K_Now;

        //上个工序OCV1或者2的电压/内阻
        public double OCV_1or2;
        //上个工序OCV1或者2的测试时间
        public string TEST_DATE;
        //OCV1 温度 
        // public double ? Temp_1;
        public double PostiveTMP;
        public double NegativeTMP;

        //电压修正值
        public double Rev_OCV;
     
        //ACIR极差值
        public double ACIR_range;
        
        //压降极差值
        public double DROP_range;

        //容量
        public double Capacity;

        //总ng状态
        public string NgState;          //NG状态  NG、OK

        //电压，内阻
        ////状态: 0.正常 1，2，3，4 NG.
        //public char NGStatus;
        ////状态: 合格   不合格  用于写数据库
        //public string NGSt;
        ////NG原因代码
        //public string NGReason;
        ////NG原因描述 用于写数据库
        //public string NGRea;
        public NgResult Test_NgResult;

        //电压，内阻
        ////状态: 0.正常 1，2，3，4 NG.
        //public char SV_NGStatus;
        ////状态: 合格   不合格  用于写数据库
        //public string SV_NGSt;
        ////NG原因代码
        //public string SV_NGReason;
        ////NG原因描述 用于写数据库
        //public string SV_NGRea;
        public NgResult SV_NgResult;

        //压降极差
        ////状态: 0.正常 1，2，3，4 NG.
        //public char DROP_NGStatus;
        ////状态: 合格   不合格  用于写数据库
        //public string DROP_NGSt;
        ////NG原因代码
        //public string DROP_NGReason;
        ////NG原因描述 用于写数据库
        //public string DROP_NGRea;
        public NgResult DROP_NgResult;
        //Acir极差
        ////状态: 0.正常 1，2，3，4 NG.
        //public char ACIR_NGStatus;
        ////状态: 合格   不合格  用于写数据库
        //public string ACIR_NGSt;
        ////NG原因代码
        //public string ACIR_NGReason;
        //NG原因描述 用于写数据库
        //public string ACIR_NGRea;

        public NgResult ACIR_NgResult;

        //历史NG原因  用于写数据库
        public string HNGSt;

        public string RESRCE;      //岗位编号
        //public string BATCH_NO;     //批号
        public string MODEL_NO;     //电池型号
        //public string PROJECT_NO;   //项目编号

        //OCV_Write_Time  
        public string OCV_Write_Time;

        //END_DATE_TIME  
        public string End_Write_Time;

    }

    public struct NgResult
    {
        public int NgType;              //NG类型 0、1
        public string NgState;          //NG状态  NG、OK
        public string NgCode;           //NG代码  00、B1、B2...
        public string NgDescribe;       //NG描述  "合格"、"小于最小压降"....
    }

    public struct SaveDataCsv
    {
        public string OCVType;
        //Cell_ID
        public string Cell_ID;

        //Pallet_ID
        public string Pallet_ID;

        //Cell_Position
        public int Cell_Position;

        //当前测试得到的正负极电压    
        public string OCV_Now;

        //当前测试得到的壳体电压(如果有的话)
        public string OCV_Shell_Now;

        //当前测试得到的内阻
        public string ACIR_Now;

        //当前的压降
        public string VoltDrop_Now;

        public string K_Now;

        //上个工序OCV1或者2的电压/内阻
        public string OCV_1or2;
       
        //OCV1 温度 
        // public double ? Temp_1;
        public string PostiveTMP;
        public string NegativeTMP;

        //电压修正值
        public string Rev_OCV;

        //ACIR极差值
        public string ACIR_range;

        //压降极差值
        public string DROP_range;

        //总ng状态
        public string NgState;          //NG状态  NG、OK
        //容量
        public string Capacity;

        public NgResult Test_NgResult;

        public NgResult SV_NgResult;

        public NgResult DROP_NgResult;
      
        public string ACIR_NGRea;

        public NgResult ACIR_NgResult;

        //历史NG原因  用于写数据库
        public string HNGSt;

        public string RESRCE;      //岗位编号
        //public string BATCH_NO;     //批号
        public string MODEL_NO;     //电池型号
        //public string PROJECT_NO;   //项目编号

        //OCV_Write_Time  
        public string OCV_Write_Time;

        //END_DATE_TIME  
        public string End_Write_Time;


    }
}
