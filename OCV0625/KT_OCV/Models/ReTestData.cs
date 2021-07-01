using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.Models
{
    public class RetestData
    {
        /// <summary>
        /// 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNo { get; set; }

        /// <summary>
        /// 复测类型
        /// </summary>
       // public RetestTypeEnum RetestType { get; set; }

        
       public List<RetestTypeEnum> RetestTypelist = new List<RetestTypeEnum>();
    }
}
