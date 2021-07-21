using System;
namespace DB_KT.Model
{
	[Serializable]
	public partial class BatInfo
	{
		public BatInfo()
		{}
		#region Model
		private string _batcode;
		private DateTime _logintime;
		private string _devicecode;
		private int? _processid;
		private int? _flowid;
		private int? _flowtype;
		private string _lotid;
		private int? _workstation;
		private string _colcode;
		private int? _batterypos;
		private DateTime? _lastflowendtime;
		private string _devaddr;
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
		public int? ProcessID
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
		public int? FlowType
		{
			set{ _flowtype=value;}
			get{return _flowtype;}
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
		public int? WorkStation
		{
			set{ _workstation=value;}
			get{return _workstation;}
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
		public int? BatteryPos
		{
			set{ _batterypos=value;}
			get{return _batterypos;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastFlowEndTime
		{
			set{ _lastflowendtime=value;}
			get{return _lastflowendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DevAddr
		{
			set{ _devaddr=value;}
			get{return _devaddr;}
		}
		#endregion Model

	}
}

