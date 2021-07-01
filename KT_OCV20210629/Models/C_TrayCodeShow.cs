using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.Models
{
    public class C_TrayCodeShow
    {
        public int ID { set; get; }                    //托盘序号

        public string TrayCode { set; get; }           //托盘条码
        public string BindTime { set; get; }           //绑定时间

        public int CellCount { set; get; }             //通道号

        public int StateFlag { set; get; }             //状态标志
    }
}
