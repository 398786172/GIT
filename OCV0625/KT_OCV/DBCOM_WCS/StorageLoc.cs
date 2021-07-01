using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    /// <summary>
    /// 库位类
    /// </summary>
    public class StorageLoc
    {
        /// <summary>
        /// 库位编号
        /// </summary>
        [JsonProperty(PropertyName = "StageLoc")]
        public string LocationNum { get; set; }

        /// <summary>
        /// 工站
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 线体编号
        /// </summary>
        public int LineId { get; set; }
        /// <summary>
        /// 库位状态
        /// </summary>
        public string LocStatus { get; set; }
        /// <summary>
        /// 库位托盘
        /// </summary>
        //public string TrayNo { get; set; }
        public string TrayCode { get; set; }
        /// <summary>
        /// 构造器：根据库位编号构造库位信息
        /// </summary>
        /// <param name="_LocationNum"></param>
        public StorageLoc(string _LocationNum,string _LocStatus,string _TrayNo)
        {
            LocationNum = _LocationNum;
            LocStatus = _LocStatus;
            TrayCode = _TrayNo;
        }
    }
}
