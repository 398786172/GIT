using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
   public class OCVTrayRemoveConfirm
    {
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string traycode { set; get; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string equipment_id { set; get; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string process_id { set; get; }


    }
}
