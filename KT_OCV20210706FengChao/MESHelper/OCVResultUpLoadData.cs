using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.MESHelper
{
   public class OCVResultUpLoadData
    {

        /// <summary>
        /// 电芯条码
        /// </summary>
        public string bar_code { set; get; }
        /// <summary>
        /// 判定结果 01 OK 03 NG
        /// </summary>
        public string result { set; get; }         
       /// <summary>
       /// 剩余数据
       /// </summary>
        public List<OCVResultUpLoadData2> data2
        {
            set { this._data2 = value; }
            get { return this._data2; }
        }
        private List<OCVResultUpLoadData2> _data2;

        public OCVResultUpLoadData()
        {
            this._data2 = new List<OCVResultUpLoadData2>();
        }
    }
}
