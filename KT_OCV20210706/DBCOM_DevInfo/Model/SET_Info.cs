using System;
namespace DevInfo.Model
{
    [Serializable]
    public partial class SET_Info
    {
        public SET_Info()
        { }
        #region Model
        private string _setname;
        private bool _ocv_seten;
        private int? _ocv_testtimes;
        private decimal? _ocv_ucl;
        private decimal? _ocv_lcl;
        private bool _shell_seten;
        private int? _shell_testtimes;
        private decimal? _shell_ucl;
        private decimal? _shell_lcl;
        private bool _acir_seten;
        private int? _acir_testtimes;
        private decimal? _acir_ucl;
        private decimal? _acir_lcl;
        /// <summary>
        /// 
        /// </summary>
        public string SetName
        {
            set { _setname = value; }
            get { return _setname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool OCV_SetEN
        {
            set { _ocv_seten = value; }
            get { return _ocv_seten; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OCV_TestTimes
        {
            set { _ocv_testtimes = value; }
            get { return _ocv_testtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OCV_UCL
        {
            set { _ocv_ucl = value; }
            get { return _ocv_ucl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OCV_LCL
        {
            set { _ocv_lcl = value; }
            get { return _ocv_lcl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Shell_SetEN
        {
            set { _shell_seten = value; }
            get { return _shell_seten; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Shell_TestTimes
        {
            set { _shell_testtimes = value; }
            get { return _shell_testtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Shell_UCL
        {
            set { _shell_ucl = value; }
            get { return _shell_ucl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Shell_LCL
        {
            set { _shell_lcl = value; }
            get { return _shell_lcl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ACIR_SetEN
        {
            set { _acir_seten = value; }
            get { return _acir_seten; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ACIR_TestTimes
        {
            set { _acir_testtimes = value; }
            get { return _acir_testtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ACIR_UCL
        {
            set { _acir_ucl = value; }
            get { return _acir_ucl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ACIR_LCL
        {
            set { _acir_lcl = value; }
            get { return _acir_lcl; }
        }
        #endregion Model

    }
}