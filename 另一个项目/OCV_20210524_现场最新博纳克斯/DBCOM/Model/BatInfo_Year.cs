using System;
namespace DB_KT.Model
{
	[Serializable]
	public partial class BatInfo_Year
	{
		public BatInfo_Year()
		{}
		#region Model
		private string _batcode;
		private DateTime _logintime;
		private string _devicecode;
		private int _processid;
		private int? _flowid;
		private string _lotid;
		private string _colcode;
		private int _batterypos;
		private string _groupcode;
		private string _gradename;
		private string _matchname;
		private int? _matchflag;
		private decimal? _capacity1;
		private decimal? _capacity2;
		private decimal? _capacity3;
		private decimal? _capacity4;
		private decimal? _capacity5;
		private decimal? _capacity6;
		private decimal? _voltage1;
		private decimal? _voltage2;
		private decimal? _voltage3;
		private decimal? _voltage4;
		private decimal? _voltage5;
		private decimal? _voltage6;
		private decimal? _current1;
		private decimal? _current2;
		private decimal? _ocv1;
		private decimal? _ocv2;
		private decimal? _ocv3;
		private decimal? _ocv4;
		private decimal? _ocv5;
        private decimal? _ocv6;
        private decimal? _inr1;
		private decimal? _inr2;
		private decimal? _inr3;
		private decimal? _inr4;
		private decimal? _inr5;
        private decimal? _inr6;
        private decimal? _delta_v1;
		private decimal? _delta_v2;
		private decimal? _k_value1;
		private decimal? _k_value2;
		private decimal? _crate;
		private decimal? _temperature1;
		private decimal? _temperature2;
		private decimal? _capontem1;
		private decimal? _capontem2;
		private string _devaddr1;
		private string _devaddr2;
		private string _devaddr3;
		private string _devaddr4;
		private string _devaddr5;
		private string _devaddr6;
		private string _devaddr7;
		private string _devaddr8;
		private string _devaddr9;
		private string _devaddr10;
		private DateTime? _flowendtime1;
		private DateTime? _flowendtime2;
		private DateTime? _flowendtime3;
		private DateTime? _flowendtime4;
		private DateTime? _flowendtime5;
		private DateTime? _flowendtime6;
		private DateTime? _flowendtime7;
		private DateTime? _flowendtime8;
		private DateTime? _flowendtime9;
		private DateTime? _flowendtime10;
		private decimal? _thickness;
		private bool _airtight;
		private string _packcode1;
		private string _packcode2;
		private long _id;
		/// <summary>
		/// 
		/// </summary>
		public string BatCode
		{
			set{ _batcode=value;}
			get{return _batcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LoginTime
		{
			set{ _logintime=value;}
			get{return _logintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceCode
		{
			set{ _devicecode=value;}
			get{return _devicecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ProcessID
		{
			set{ _processid=value;}
			get{return _processid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FlowID
		{
			set{ _flowid=value;}
			get{return _flowid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LotID
		{
			set{ _lotid=value;}
			get{return _lotid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ColCode
		{
			set{ _colcode=value;}
			get{return _colcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BatteryPos
		{
			set{ _batterypos=value;}
			get{return _batterypos;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupCode
		{
			set{ _groupcode=value;}
			get{return _groupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GradeName
		{
			set{ _gradename=value;}
			get{return _gradename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MatchName
		{
			set{ _matchname=value;}
			get{return _matchname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MatchFlag
		{
			set{ _matchflag=value;}
			get{return _matchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Capacity1
		{
			set{ _capacity1=value;}
			get{return _capacity1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Capacity2
		{
			set{ _capacity2=value;}
			get{return _capacity2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Capacity3
		{
			set{ _capacity3=value;}
			get{return _capacity3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Capacity4
		{
			set{ _capacity4=value;}
			get{return _capacity4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Capacity5
		{
			set{ _capacity5=value;}
			get{return _capacity5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Capacity6
		{
			set{ _capacity6=value;}
			get{return _capacity6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Voltage1
		{
			set{ _voltage1=value;}
			get{return _voltage1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Voltage2
		{
			set{ _voltage2=value;}
			get{return _voltage2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Voltage3
		{
			set{ _voltage3=value;}
			get{return _voltage3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Voltage4
		{
			set{ _voltage4=value;}
			get{return _voltage4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Voltage5
		{
			set{ _voltage5=value;}
			get{return _voltage5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Voltage6
		{
			set{ _voltage6=value;}
			get{return _voltage6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Current1
		{
			set{ _current1=value;}
			get{return _current1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Current2
		{
			set{ _current2=value;}
			get{return _current2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OCV1
		{
			set{ _ocv1=value;}
			get{return _ocv1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OCV2
		{
			set{ _ocv2=value;}
			get{return _ocv2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OCV3
		{
			set{ _ocv3=value;}
			get{return _ocv3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OCV4
		{
			set{ _ocv4=value;}
			get{return _ocv4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OCV5
		{
			set{ _ocv5=value;}
			get{return _ocv5;}
		}
        /// <summary>
        /// 
        /// </summary>
        public decimal? OCV6
        {
            set { _ocv6 = value; }
            get { return _ocv6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InR1
		{
			set{ _inr1=value;}
			get{return _inr1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? InR2
		{
			set{ _inr2=value;}
			get{return _inr2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? InR3
		{
			set{ _inr3=value;}
			get{return _inr3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? InR4
		{
			set{ _inr4=value;}
			get{return _inr4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? InR5
		{
			set{ _inr5=value;}
			get{return _inr5;}
		}
        public decimal? InR6
        {
            set { _inr6 = value; }
            get { return _inr6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Delta_V1
		{
			set{ _delta_v1=value;}
			get{return _delta_v1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Delta_V2
		{
			set{ _delta_v2=value;}
			get{return _delta_v2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? K_Value1
		{
			set{ _k_value1=value;}
			get{return _k_value1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? K_Value2
		{
			set{ _k_value2=value;}
			get{return _k_value2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CRate
		{
			set{ _crate=value;}
			get{return _crate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Temperature1
		{
			set{ _temperature1=value;}
			get{return _temperature1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Temperature2
		{
			set{ _temperature2=value;}
			get{return _temperature2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CapOnTem1
		{
			set{ _capontem1=value;}
			get{return _capontem1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CapOnTem2
		{
			set{ _capontem2=value;}
			get{return _capontem2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr1
		{
			set{ _devaddr1=value;}
			get{return _devaddr1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr2
		{
			set{ _devaddr2=value;}
			get{return _devaddr2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr3
		{
			set{ _devaddr3=value;}
			get{return _devaddr3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr4
		{
			set{ _devaddr4=value;}
			get{return _devaddr4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr5
		{
			set{ _devaddr5=value;}
			get{return _devaddr5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr6
		{
			set{ _devaddr6=value;}
			get{return _devaddr6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr7
		{
			set{ _devaddr7=value;}
			get{return _devaddr7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr8
		{
			set{ _devaddr8=value;}
			get{return _devaddr8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr9
		{
			set{ _devaddr9=value;}
			get{return _devaddr9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr10
		{
			set{ _devaddr10=value;}
			get{return _devaddr10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime1
		{
			set{ _flowendtime1=value;}
			get{return _flowendtime1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime2
		{
			set{ _flowendtime2=value;}
			get{return _flowendtime2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime3
		{
			set{ _flowendtime3=value;}
			get{return _flowendtime3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime4
		{
			set{ _flowendtime4=value;}
			get{return _flowendtime4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime5
		{
			set{ _flowendtime5=value;}
			get{return _flowendtime5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime6
		{
			set{ _flowendtime6=value;}
			get{return _flowendtime6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime7
		{
			set{ _flowendtime7=value;}
			get{return _flowendtime7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime8
		{
			set{ _flowendtime8=value;}
			get{return _flowendtime8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime9
		{
			set{ _flowendtime9=value;}
			get{return _flowendtime9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FlowEndTime10
		{
			set{ _flowendtime10=value;}
			get{return _flowendtime10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Thickness
		{
			set{ _thickness=value;}
			get{return _thickness;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Airtight
		{
			set{ _airtight=value;}
			get{return _airtight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PackCode1
		{
			set{ _packcode1=value;}
			get{return _packcode1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PackCode2
		{
			set{ _packcode2=value;}
			get{return _packcode2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		#endregion Model

	}
}

