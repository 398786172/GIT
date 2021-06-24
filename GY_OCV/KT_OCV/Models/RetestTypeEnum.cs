using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.Models
{
    public enum RetestTypeEnum
    {
        /// <summary>
        /// 开路电压
        /// </summary>
        Voltage = 0,

        /// <summary>
        /// 壳体\正极
        /// </summary>
        SPVolage = 1,

        /// <summary>
        /// 壳体\负极
        /// </summary>
        SNVolage = 2,

        /// <summary>
        /// 内阻
        /// </summary>
        ACIR = 3,

        None = 99

    }
}
