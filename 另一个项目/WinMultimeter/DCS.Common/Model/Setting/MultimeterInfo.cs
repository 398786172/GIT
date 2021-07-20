using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCS.Common.Model.Setting
{
   public class MultimeterInfo
    {
        /// <summary>
        /// 万用表的类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 万用表实际显示的名称
        /// </summary>
        public string  Name { get; set; }
        /// <summary>
        /// 万用表的驱动（可能包含多个）
        /// </summary>
        public string Driver { get; set; }
    }
}
