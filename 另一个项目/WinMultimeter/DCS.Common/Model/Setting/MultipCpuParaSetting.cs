using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCS.Common.Model.Setting
{
    public class MultipCpuParaSetting
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; } = 1;

       /// <summary>
       /// IP
       /// </summary>
        public string IP { get; set; } = "192.168.1.1";

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5101;

        /// <summary>
        /// 控制的通道数量
        /// </summary>
        public int SingleControlChannelNum { get; set; } = 2;

        public int ParallelControlChannelNum { get; set; } = 2;
        public string ReMark { get; set; }
    }
}
