using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCV.PLCContr.Model
{
    public class PLCInfoModel
    {
        public PLCInfoModel()
        {

        }
        #region Model
        private int _id;
        private string _controlname = " ";
        private string _controltype = " ";
        private string _pagename = " ";
        private string _subpagename = " ";
        private string _registeraddress = "W130";
        private string _valuetype = "bool";
        private string _bytename = "x0";
        private string _linkname = "x0";
        private string _memo = " ";
        private bool _writeenable = false;
        private bool _bValue = false;
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
        public string ControlName
        {
            set { _controlname = value; }
            get { return _controlname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ControlType
        {
            set { _controltype = value; }
            get { return _controltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PageName
        {
            set { _pagename = value; }
            get { return _pagename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubPageName
        {
            set { _subpagename = value; }
            get { return _subpagename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegisterAddress
        {
            set { _registeraddress = value; }
            get { return _registeraddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ValueType
        {
            set { _valuetype = value; }
            get { return _valuetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ByteName
        {
            set { _bytename = value; }
            get { return _bytename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkName
        {
            set { _linkname = value; }
            get { return _linkname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool WriteEnable
        {
            set { _writeenable = value; }
            get { return _writeenable; }
        }

        public bool BValue
        {
            get { return _bValue; }
            set { _bValue = value; }
        }
        #endregion Model
    }
}
