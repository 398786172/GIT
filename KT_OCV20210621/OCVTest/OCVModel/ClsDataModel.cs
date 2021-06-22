using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class ClsDataModel
    {

        /// <summary>
        /// 电池记录数据类
        /// </summary>
        [Serializable]
        public class C_CellData : C_CellRealData
        {
            public string Cell_ID;          //电池条码
            public string Pallet_ID;        //托盘号
            public int Cell_Position;       //电池位置

            public double Rev_OCV;          //电压修正值
            public double ACIR_Range;       //ACIR极差值
            public double OCV_Drop_Range;   //压降极差值
            public double VoltDrop_Now;     //当前的压降
            public double K_Now;            //K值
            public double Capacity;         //容量

            public string NgState;          //NG状态  NG、OK    （总ng状态）
            public NgResult Test_NgResult;  //电压，内阻
            public NgResult SV_NgResult;    //壳体电压
            public NgResult DROP_NgResult;  //压降极差
            public NgResult ACIR_NgResult;  //Acir极差

            public string End_Write_Time;   //当前测试结束时间

            public C_CellData()
            {

            }
        }

        /// <summary>
        /// 电池实时数据类
        /// </summary>
        [Serializable]
        public class C_CellRealData: C_LastTestData
        {
           
            public double OCV_Now;          //当前测试得到的正负极电压    
            public double Postive_Shell;    //当前测试得到的正极对壳体电压(如果有的话)
            public double Negative_Shell;   //当前测试得到的负极对壳体电压(如果有的话)
            public double ACIR_Now;         //当前测试得到的内阻
            public double PostiveTMP;       //正极温度
            public double NegativeTMP;      //负极温度
            public C_CellRealData()
            {

            }
        }

        /// <summary>
        /// 上个工步测试数据
        /// </summary>
        [Serializable]
        public class C_LastTestData
        {
            public double OCV_1or2;         //上个工序OCV1或者2的电压/内阻
            public string Test_Date;        //上个工序OCV1或者2的测试时间
            public string OCV_Write_Time;   //上个工序测试时间   
            public string HNGSt;             //上个工序NG原因 

            public C_LastTestData()
            {

            }
        }

        public struct NgResult
        {
            public int NgType;              //NG类型 0、1
            public string NgState;          //NG状态  NG、OK
            public string NgCode;           //NG代码  00、B1、B2...
            public string NgDescribe;       //NG描述  "合格"、"小于最小压降"....
        }
        /// <summary>
        /// 资源数据类
        /// </summary>
        [Serializable]
        public class C_Resource_Data
        {
            public string RESRCE;      //岗位编号
            public string BATCH_NO;     //批号
            public string MODEL_NO;     //电池型号
            public string PROJECT_NO;   //项目编号
            public C_Resource_Data()
            {

            }
        }
    }
}
