using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
    public class ResultOCVTrayRemoveConfirmData
    {
        /// <summary>
        /// 电芯条码 
        /// </summary>
        public string bar_code { set; get; }
        /// <summary>
        /// 电芯位置,X-Y X代表行号，Y代表列号
        /// </summary>
        public string location { set; get; }
        /// <summary>
        /// 容量结果
        /// </summary>
        public string capacity_result { set; get; }
        /// <summary>
        /// OCV结果
        /// </summary>
        public string OCV1_result { set; get; }
        /// <summary>
        /// OCV结果
        /// </summary>
        public string OCV2_result { set; get; }


    }
}
