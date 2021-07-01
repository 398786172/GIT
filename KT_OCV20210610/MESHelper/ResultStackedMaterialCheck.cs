using OCV.MESHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
    public class ResultStackedMaterialCheck
    {   /// <summary>
        /// 校验成功返回码0成功，1失败
        /// </summary>
        public string status_code { set; get; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string traycode { set; get; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { set; get; }
        /// <summary>
        /// 电芯位置,X-Y X代表行号，Y代表列号
        /// </summary>
        public string procedure { set; get; }
        /// <summary>
        /// 其余数据信息
        /// </summary>
        public List<Data> data
        {
            set { this._data = value; }
            get { return this._data; }
        }
        private List<Data> _data; 

        public ResultStackedMaterialCheck()
        {
            this._data = new List<Data> ();
        }
    }
}
