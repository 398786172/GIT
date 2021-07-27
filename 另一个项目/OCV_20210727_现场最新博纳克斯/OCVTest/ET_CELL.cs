using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCV
{
    //电池信息
    [Serializable]
    public class ET_CELL
    {

        /// <summary>
        /// Cell_ID  电池码
        /// </summary>
        public string Cell_ID { get; set; }


        /// <summary>
        /// Pallet_ID 托盘码
        /// </summary>
        public string Pallet_ID { get; set; }


        /// <summary>
        /// Cell_Position  通道
        /// </summary>
        public int Cell_Position { get; set; }


        /// <summary>
        /// OCV_V1   （正负极电压）
        /// </summary>
        public double OCV_V1 { get; set; }


        /// <summary>
        /// OCV_V2   (壳体电压，如果有）
        /// </summary>
        public double OCV_V2 { get; set; }


        /// <summary>
        /// ACIR_R1   (一次测量)
        /// </summary>
        public double ACIR { get; set; }


        /// <summary>
        /// 设备编码
        /// </summary>
        public string CODE { get; set; }


        /// <summary>
        /// OCV1_T    （温度）
        /// </summary>
        public double TMP { get; set; }


        /// <summary>
        /// OCV_START_Time
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// OCV_Write_Time 
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// 上次OCV测试的结束时间
        /// </summary>
        public DateTime LastTest_EndTime { get; set; }


        /// <summary>
        /// K 值 = (上次OCV测试电压 - 本次OCV测试电压)/(本次OCV测试时间 - 上次OCV测试时间), 单位: mV/day
        /// </summary>
        public double KVal { get; set; }

    }
}
