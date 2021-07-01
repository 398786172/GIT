using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV
{
    public class ChannelExpCount
    {
        #region model
        private int _id;
        private int? _channelno;
        private int? _expcount;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        public int? ExpCount
        {
            set { _expcount = value; }
            get { return _expcount; }
        }
        #endregion Model
    }
}
