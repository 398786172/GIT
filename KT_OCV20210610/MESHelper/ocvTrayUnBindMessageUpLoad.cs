using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
    public class OCVTrayUnBindMessageUpLoad
    {   /// <summary>
        /// 工序编码
        /// </summary>
        public string process_id { set; get; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string equipment_id { set; get; }
        /// <summary>
        /// 托盘编号
        /// </summary>
        public string tracode { set; get; }
        /// <summary>
        /// 01正常解绑；02异常解绑
        /// </summary>
        public string type { set; get; }
    }
}
