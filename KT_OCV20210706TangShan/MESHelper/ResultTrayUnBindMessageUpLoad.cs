using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
    public class ResultTrayUnBindMessageUpLoad
    {
        /// <summary>
        /// 状态码返回码0成功，1失败
        /// </summary>
        public string status_code { set; get; }
        /// <summary>
        /// 返回信息（错误原因等）
        /// </summary>
        public string message { set; get; }
    }
}
