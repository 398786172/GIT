using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class OCVLog
    {
        public OCVLog()
        {
        }

        #region Model
        private string _id;
        private string _traycode;
        private string _cellcode;
        private int? _channelno;
        private string _pcname;
        private string _expcode;
        private string _describe;
        private string _exptime;
        private double? _timecut;

        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TrayCode
        {
            set { _traycode = value; }
            get { return _traycode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CellCode
        {
            set { _cellcode = value; }
            get { return _cellcode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ChannelNo
        {
            set { _channelno = value; }
            get { return _channelno; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PCName
        {
            set { _pcname = value; }
            get { return _pcname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExpCode
        {
            set { _expcode = value; }
            get { return _expcode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExpTime
        {
            set { _exptime = value; }
            get { return _exptime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? TimeCut
        {
            set { _timecut = value; }
            get { return _timecut; }
        }
        #endregion Model
    }

    public class OCVLogExpNum
    {
        public string TrayCode { get; set; }
        public int ExpCount { get; set; }
    }
}
