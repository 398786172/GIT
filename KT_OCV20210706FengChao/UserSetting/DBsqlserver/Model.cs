using System;

namespace Pro.Model.sql
{
    /// <summary>
    /// ProjectInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProjectInfo
    {
        public ProjectInfo()
        { }
        #region Model
        private string _ocv_type;
        private string _batterytype;
        private string _projectname;
        private decimal _uplmt_v;
        private decimal _downlmt_v;
        private decimal _uplmt_acir;
        private decimal _downlmt_acir;
        private decimal _maxvoltdrop;
        private decimal _minvoltdrop;
        private decimal _uplmt_time;
        private decimal _downlmt_time;
        private decimal _k;
        private decimal _tempbase;
        private decimal _temppara;
        private string _isolation;
        private decimal _l_dispiacement;
        private decimal _r_dispiacement;
        /// <summary>
        /// 
        /// </summary>
        public string OCV_type
        {
            set { _ocv_type = value; }
            get { return _ocv_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BatteryType
        {
            set { _batterytype = value; }
            get { return _batterytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal UpLmt_V
        {
            set { _uplmt_v = value; }
            get { return _uplmt_v; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DownLmt_V
        {
            set { _downlmt_v = value; }
            get { return _downlmt_v; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal UpLmt_ACIR
        {
            set { _uplmt_acir = value; }
            get { return _uplmt_acir; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DownLmt_ACIR
        {
            set { _downlmt_acir = value; }
            get { return _downlmt_acir; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MaxVoltDrop
        {
            set { _maxvoltdrop = value; }
            get { return _maxvoltdrop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MinVoltDrop
        {
            set { _minvoltdrop = value; }
            get { return _minvoltdrop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal UPLmt_Time
        {
            set { _uplmt_time = value; }
            get { return _uplmt_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DownLmt_Time
        {
            set { _downlmt_time = value; }
            get { return _downlmt_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal K
        {
            set { _k = value; }
            get { return _k; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TempBase
        {
            set { _tempbase = value; }
            get { return _tempbase; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TempPara
        {
            set { _temppara = value; }
            get { return _temppara; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ISOLATION
        {
            set { _isolation = value; }
            get { return _isolation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal L_Dispiacement
        {
            set { _l_dispiacement = value; }
            get { return _l_dispiacement; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal R_Dispiacement
        {
            set { _r_dispiacement = value; }
            get { return _r_dispiacement; }
        }
        #endregion Model

    }
}

