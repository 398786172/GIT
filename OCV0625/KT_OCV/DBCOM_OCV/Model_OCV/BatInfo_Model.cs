using System;

namespace DB_OCV.Model
{
    /// <summary>
    /// qt_db_ocv1:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BatInfo_Model
    {
        public BatInfo_Model()
        { }
        #region Model
        private string _eqp_id;
        private string _pc_id;
        private string _operation;
        private double? _is_trans;
        private string _tray_id;
        private string _cell_id;
        private int? _battery_pos;
        private string _model_no;
        private string _batch_no;
        private string _total_ng_state;
        private double? _ocv_voltage;
        private double? _acir;
        private string _test_ng_code;
        private string _test_result;
        private string _test_result_desc;

        private double? _postiveshell_voltage;
        private string _postivesv_ng_code;
        private string _postivesv_result;
        private string _postivesv_result_desc;

        private double? _shell_voltage;
        private string _sv_ng_code;
        private string _sv_result;
        private string _sv_result_desc;


        private double? _postive_temp;
        private double? _negative_temp;
        private double? _k;
        private double? _v_drop;
        private double? _v_drop_range;
        private string _v_drop_range_code;
        private string _v_drop_result;
        private string _v_drop_result_desc;
        private double? _acir_range;
        private string _r_range_ng_code;
        private string _r_range_result;
        private string _r_range_result_desc;
        private double? _rev_ocv;
        private double? _capacity;
        private string _end_date_time;

        private string _testMode = "自动";
        /// <summary>
        /// 
        /// </summary>
        public string Eqp_ID
        {
            set { _eqp_id = value; }
            get { return _eqp_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PC_ID
        {
            set { _pc_id = value; }
            get { return _pc_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OPERATION
        {
            set { _operation = value; }
            get { return _operation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? IS_TRANS
        {
            set { _is_trans = value; }
            get { return _is_trans; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRAY_ID
        {
            set { _tray_id = value; }
            get { return _tray_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CELL_ID
        {
            set { _cell_id = value; }
            get { return _cell_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BATTERY_POS
        {
            set { _battery_pos = value; }
            get { return _battery_pos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MODEL_NO
        {
            set { _model_no = value; }
            get { return _model_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BATCH_NO
        {
            set { _batch_no = value; }
            get { return _batch_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TOTAL_NG_STATE
        {

            set { _total_ng_state = value; }
            get { return _total_ng_state; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? OCV_VOLTAGE
        {
            set { _ocv_voltage = value; }
            get { return _ocv_voltage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? ACIR
        {
            set { _acir = value; }
            get { return _acir; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TEST_NG_CODE
        {
            set { _test_ng_code = value; }
            get { return _test_ng_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TEST_RESULT
        {
            set { _test_result = value; }
            get { return _test_result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TEST_RESULT_DESC
        {
            set { _test_result_desc = value; }
            get { return _test_result_desc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? PostiveSHELL_VOLTAGE
        {
            set { _postiveshell_voltage = value; }
            get { return _postiveshell_voltage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostiveSV_NG_CODE
        {
            set { _postivesv_ng_code = value; }
            get { return _postivesv_ng_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostiveSV_RESULT
        {
            set { _postivesv_result = value; }
            get { return _postivesv_result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostiveSV_RESULT_DESC
        {
            set { _postivesv_result_desc = value; }
            get { return _postivesv_result_desc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? SHELL_VOLTAGE
        {
            set { _shell_voltage = value; }
            get { return _shell_voltage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SV_NG_CODE
        {
            set { _sv_ng_code = value; }
            get { return _sv_ng_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SV_RESULT
        {
            set { _sv_result = value; }
            get { return _sv_result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SV_RESULT_DESC
        {
            set { _sv_result_desc = value; }
            get { return _sv_result_desc; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double? POSTIVE_TEMP
        {
            set { _postive_temp = value; }
            get { return _postive_temp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? NEGATIVE_TEMP
        {
            set { _negative_temp = value; }
            get { return _negative_temp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? K
        {
            set { _k = value; }
            get { return _k; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? V_DROP
        {
            set { _v_drop = value; }
            get { return _v_drop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? V_DROP_RANGE
        {
            set { _v_drop_range = value; }
            get { return _v_drop_range; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string V_DROP_RANGE_CODE
        {
            set { _v_drop_range_code = value; }
            get { return _v_drop_range_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string V_DROP_RESULT
        {
            set { _v_drop_result = value; }
            get { return _v_drop_result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string V_DROP_RESULT_DESC
        {
            set { _v_drop_result_desc = value; }
            get { return _v_drop_result_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? ACIR_RANGE
        {
            set { _acir_range = value; }
            get { return _acir_range; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string R_RANGE_NG_CODE
        {
            set { _r_range_ng_code = value; }
            get { return _r_range_ng_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string R_RANGE_RESULT
        {
            set { _r_range_result = value; }
            get { return _r_range_result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string R_RANGE_RESULT_DESC
        {
            set { _r_range_result_desc = value; }
            get { return _r_range_result_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? REV_OCV
        {
            set { _rev_ocv = value; }
            get { return _rev_ocv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? CAPACITY
        {
            set { _capacity = value; }
            get { return _capacity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string END_DATE_TIME
        {
            set { _end_date_time = value; }
            get { return _end_date_time; }
        }

        /// <summary>
        ///set testmode value  
        /// </summary>
        public string TestMode
        {
            set { _testMode = value; }
            get { return _testMode; }
        }
        #endregion Model

    }
}


