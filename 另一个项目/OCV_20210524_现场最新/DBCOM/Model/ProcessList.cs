using System;
namespace DB_KT.Model
{

	[Serializable]
	public partial class ProcessList
	{
		public ProcessList()
		{}
		#region Model
		private int _processid;
		private string _processname;
		private string _batterytype;
		private string _statis;
		private string _selection;
		private string _matching;
		private string _processtype;
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
		public string ProcessName
		{
			set{ _processname=value;}
			get{return _processname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BatteryType
		{
			set{ _batterytype=value;}
			get{return _batterytype;}
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
		public string ProcessType
		{
			set{ _processtype=value;}
			get{return _processtype;}
		}
		#endregion Model

	}
}

