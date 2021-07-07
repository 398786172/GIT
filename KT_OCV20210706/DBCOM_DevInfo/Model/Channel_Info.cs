using System;
namespace DevInfo.Model
{
    [Serializable]
    public partial class Channel_Info
    {
        public Channel_Info()
        { }
        #region Model
        private int _channelno;
        private int? _ocv_errcount;
        private bool _ocv_en;
        private int? _shell_errcount;
        private bool _shell_en;
        private int? _acir_errcount;
        private bool _acir_en;
        /// <summary>
        /// 
        /// </summary>
        public int ChannelNo
        {
            set { _channelno = value; }
            get { return _channelno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OCV_ErrCount
        {
            set { _ocv_errcount = value; }
            get { return _ocv_errcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool OCV_EN
        {
            set { _ocv_en = value; }
            get { return _ocv_en; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Shell_ErrCount
        {
            set { _shell_errcount = value; }
            get { return _shell_errcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Shell_EN
        {
            set { _shell_en = value; }
            get { return _shell_en; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ACIR_ErrCount
        {
            set { _acir_errcount = value; }
            get { return _acir_errcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ACIR_EN
        {
            set { _acir_en = value; }
            get { return _acir_en; }
        }
        #endregion Model

    }
}

