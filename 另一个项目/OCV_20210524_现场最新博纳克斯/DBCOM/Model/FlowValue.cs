using System;
namespace DB_KT.Model
{

	[Serializable]
	public partial class FlowValue
	{
		public FlowValue()
		{}
		#region Model
		private int _processid;
		private int _flowid;
		private string _flowname;
		private int? _flowtype;
		private string _flowdata;
		private int? _station;
		private string _flowstarttime;
		private string _statis;
		private string _selection;
		private string _matching;
		private string _capacoeffi;
		private string _plcsetstr;
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
		public int FlowID
		{
			set{ _flowid=value;}
			get{return _flowid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FlowName
		{
			set{ _flowname=value;}
			get{return _flowname;}
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
		public string FlowData
		{
			set{ _flowdata=value;}
			get{return _flowdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Station
		{
			set{ _station=value;}
			get{return _station;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FlowStartTime
		{
			set{ _flowstarttime=value;}
			get{return _flowstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Statis
		{
			set{ _statis=value;}
			get{return _statis;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Selection
		{
			set{ _selection=value;}
			get{return _selection;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Matching
		{
			set{ _matching=value;}
			get{return _matching;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CapaCoeffi
		{
			set{ _capacoeffi=value;}
			get{return _capacoeffi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PLCSETStr
		{
			set{ _plcsetstr=value;}
			get{return _plcsetstr;}
		}
		#endregion Model

	}
}

