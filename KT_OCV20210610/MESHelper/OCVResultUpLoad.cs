using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
   public class OCVResultUpLoad
    {
        /// <summary>
        /// 工序编码
        /// </summary>
        public string process_id { set; get; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string equipment_id { set; get; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string traycode { set; get; }
        /// <summary>
        /// 阶段 OCV1 01，OCV2 02
        /// </summary>
        public string procedure { set; get; }
        /// <summary>
        /// 剩余数据
        /// </summary>
        public List<OCVResultUpLoadData> data
        {
            set { this._data = value; }
            get { return this._data; }
        }
        private List<OCVResultUpLoadData> _data;

        public OCVResultUpLoad()
        {
            this._data = new List<OCVResultUpLoadData>();
        }
    }
}
