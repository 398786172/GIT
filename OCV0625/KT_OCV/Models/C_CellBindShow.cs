using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.Models
{
    /// <summary>
    /// 电芯条码绑定显示类
    /// </summary>
    public class C_CellBindShow
    {
        public string TrayCode = "";                   //托盘条码
        public string BindTime = "";                   //绑定时间

        public int TrayIndex { get; set; }        //所在托盘在库位中的索引号

        public string Channel { set; get; }            //通道号
        public string CellCode { set; get; }           //电芯条码 

        public int StateFlag = 0;                      //状态标志

        public C_CellBindShow()
        {

        }

        //internal C_CellBindShow(C_CellBindShow cbs)
        public C_CellBindShow(C_CellBindShow cbs)
        {
            TrayIndex = cbs.TrayIndex;
            this.TrayCode = cbs.TrayCode;
            this.BindTime = cbs.BindTime;
            this.Channel = cbs.Channel;
            this.CellCode = cbs.CellCode;
            this.StateFlag = cbs.StateFlag;
        }
    }
}
