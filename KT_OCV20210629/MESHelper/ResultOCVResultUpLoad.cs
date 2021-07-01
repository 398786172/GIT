using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
    public class ResultOCVResultUpLoad
    {
        /// <summary>
        /// 校验成功返回码0成功，1失败 
        /// </summary>
        public string status_code { set; get; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { set; get; }
    }
}
