using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
    public class Data
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
        /// 是否Marking标记
        /// </summary>
        public bool marking { set; get; }
        /// <summary>
        /// 是否Marking标记
        /// </summary>
        public string marking_memo { set; get; }
        /// <summary>
        /// 是否marking拦截 0拦截 1不拦截
        /// </summary>
        public string is_hold { set; get; }
        /// <summary>
        /// 容量结果 OK 01，复测 02，NG 03
        /// </summary>
        public string capacity_result { set; get; }
    }
}
