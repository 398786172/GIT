using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
   public class ResultOCVTrayRemoveConfirm
    {
        /// <summary>
        /// 校验成功返回码0成功，1失败 
        /// </summary>
        public string status_code { set; get; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { set; get; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string traycode { set; get; }
        /// <summary>
        /// 剩余数据
        /// </summary>
        public List<ResultOCVTrayRemoveConfirmData> data
        {
            set { this._data = value; }
            get { return this._data; }
        }
        private List<ResultOCVTrayRemoveConfirmData> _data;

        public ResultOCVTrayRemoveConfirm()
        {
            this._data = new List<ResultOCVTrayRemoveConfirmData>();
        }
    }
}
