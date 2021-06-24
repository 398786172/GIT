using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrayLogin.Model;
using TrayLogin.Business;
using DevComponents.DotNetBar;
using Cls_PLC_TCP;
using System.Threading;
using ClsDeviceComm.Controls;

namespace TrayLogin
{
    public partial class FrmPLCIOInfo : Form
    {
        private List<ClsPLCInfo> _listAddr = new List<M_PLCInfo>();
        private List<M_PLCInfo> _listValid = new List<M_PLCInfo>();
        PLCInfoBLL _bll = new PLCInfoBLL();
        private bool isClose = false;
        public FrmPLCIOInfo()
        {
            InitializeComponent();
            GetAddressList();
            this.Opacity = 0.98;

            //Action act = delegate
            //  {
            //      if (isClose == false)
            //      {
            //          Thread.Sleep(1000);
            //         ReadStatus();
            //      }

            //  };
            //act.BeginInvoke(OnComplete_readStatus, act);
            Func<bool> myFuc = () =>
            {

                if (isClose == false)
                {
                    Thread.Sleep(1000);
                    return ReadStatus();
                }
                else
                {
                    return false;
                }

            };
            myFuc.BeginInvoke(OnComplete_readStatus, myFuc);
        }

        /// <summary>
        /// 读状态完成时回调
        /// </summary>
        /// <param name="iar"></param>
        private void OnComplete_readStatus(IAsyncResult iar)
        {
            Func<bool> func = (Func<bool>)iar.AsyncState;
            try
            {
               
                bool reslut= func.EndInvoke(iar);

                Action act = delegate
                {
                    foreach (var item in tabControlPanel2.Controls)
                    {
                        if (item is HslCommunication.Controls.UserLantern)
                        {
                            HslCommunication.Controls.UserLantern lbl = item as HslCommunication.Controls.UserLantern;
                            string[] arr = lbl.Name.Split('_');
                            if (arr.Length < 2)
                                continue;
                            string linkName = arr[1];
                            var query = (from p in _listValid where p.LinkName == linkName select p).ToList();
                            if (query.Count > 0)
                            {
                                if (query.First().BValue == true)
                                {
                                    lbl.LanternBackground = Color.LawnGreen;
                                }
                                else
                                {
                                    lbl.LanternBackground = Color.Gray;
                                }

                            }
                        }
                    }
                    foreach (var item in tabControlPanel1.Controls)
                    {
                        if (item is HslCommunication.Controls.UserLantern)
                        {
                            HslCommunication.Controls.UserLantern lbl = item as HslCommunication.Controls.UserLantern;
                            string[] arr = lbl.Name.Split('_');
                            if (arr.Length < 2)
                                continue;
                            string linkName = arr[1];
                            var query = (from p in _listValid where p.LinkName == linkName select p).ToList();
                            if (query.Count > 0)
                            {
                                if (query.First().BValue == true)
                                {
                                    lbl.LanternBackground = Color.LawnGreen;
                                }
                                else
                                {
                                    lbl.LanternBackground = Color.Gray;
                                }
                            }
                        }
                    }
                };
                if (this.InvokeRequired)
                {
                    Invoke(act);
                }
                else
                {
                    act();
                }
            }
            catch (Exception ex)
            {
            }

         
            func.BeginInvoke(OnComplete_readStatus, iar.AsyncState);

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;

            }

        }

        private void tabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 得到地址
        /// </summary>
        /// <returns></returns>
        private List<M_PLCInfo> GetAddressList()
        {
            var list = _bll.GetModelList("PageName like '%FrmPLCIOInfo%'");
            InitUIName(list);
            return list;
        }

        /// <summary>
        /// 初始化控件 text
        /// </summary>
        /// <param name="list"></param>
        private void InitUIName(List<M_PLCInfo> list)
        {
            if (list == null)
                return;
            foreach (var item in tabControlPanel2.Controls)
            {
                if (item is LabelX)
                {
                    LabelX lbl = item as LabelX;
                    string[] arr = lbl.Name.Split('_');
                    if (arr.Length < 2)
                        continue;
                    string linkName = arr[1];
                    var query = (from p in list where p.LinkName == linkName select p).ToList();
                    lbl.Text = "";
                    if (query.Count > 0)
                    {
                        _listValid.Add(query.First());

                        lbl.Text = linkName + " " + query.First().Memo;
                    }
                }
            }
            foreach (var item in tabControlPanel1.Controls)
            {
                if (item is LabelX)
                {
                    LabelX lbl = item as LabelX;
                    string[] arr = lbl.Name.Split('_');
                    if (arr.Length < 2)
                        continue;
                    string linkName = arr[1];
                    var query = (from p in list where p.LinkName == linkName select p).ToList();
                    lbl.Text = "";
                    if (query.Count > 0)
                    {
                        _listValid.Add(query.First());
                        lbl.Text = linkName + " " + query.First().Memo;
                    }
                }
            }
        }


        private bool ReadStatus()
        {
            if (CLS_PLCComm_TCP.PLCInstance.Connected == false)
            {
                CLS_PLCComm_TCP.PLCInstance.ReLink();
            }
            List<BitAddressValue> addresslist = new List<BitAddressValue>();
            foreach (var item in _listValid)
            {
                string address = item.RegisterAddress.Split('.')[0];
                var query = from p in addresslist where p.Address == address select p;
                if (query.Count() == 0)
                {
                    addresslist.Add(new BitAddressValue() { Address = address, Value = 0 });
                }
            }
            foreach (var item in addresslist)
            {
                if (CLS_PLCComm_TCP.PLCInstance.Connected == true)
                {
                    ushort result = 0;
                    CLS_PLCComm_TCP.PLCInstance.PLC_WordReg_Read(item.Address, ref result, item.Address);
                    item.Value = result;
                }
            }

            foreach (var item in _listValid)
            {
                item.BValue = GetBoolValue(addresslist, item.RegisterAddress);
            }


            //foreach (var item in _listValid)
            //{
            //    if (item.ValueType == "bool")
            //    {
            //        bool result = false;
            //        if (string.IsNullOrEmpty(item.ByteName))
            //            continue;

            //        if (CLS_PLCComm_TCP.PLCInstance.Connected == true)
            //        {
            //            CLS_PLCComm_TCP.PLCInstance.PLC_BitReg_Read(item.ByteName, ref result, item.RegisterAddress);
            //            item.BValue = result;
            //        }
            //        else
            //        {
            //            item.BValue = false;
            //        }
            //    }
            //}
            return true;
        }


        private bool GetBoolValue(List<BitAddressValue> addresslist, string currAddress)
        {
            string[] arr = currAddress.Split('.');
            string address = arr[0];
            string bitAddress = arr[1];
            var query = (from p in addresslist where p.Address == address select p).FirstOrDefault();
            int mBit = 0;
            if (int.TryParse(bitAddress, out mBit) == false)
            {
                switch (bitAddress)
                {
                    case "a":
                        mBit = 10;
                        break;
                    case "b":
                        mBit = 11;
                        break;
                    case "c":
                        mBit = 12;
                        break;
                    case "d":
                        mBit = 13;
                        break;
                    case "e":
                        mBit = 14;
                        break;
                    case "f":
                        mBit = 15;
                        break;
                    default:
                        mBit = 0;
                        break;

                }

            }
            if (query != null)
            {
                try
                {
                    long mRecieveData = SignToUnsign((short)query.Value);
                   // string mBinaryStrx = Convert.ToString(mRecieveData, 2);
                    string mBinaryStr = Convert.ToString(mRecieveData, 2).PadLeft(16, '0');   //转换为2进制 
                    char[] mCharRead = mBinaryStr.ToCharArray();        //读取到的值
                    char[] mCharWrite = new char[16];                   //要写入的值
                    for (int i = 0; i < 16; i++)
                    {   //初始化
                        mCharWrite[i] = mCharRead[i];
                    }
                    char cValue = mCharWrite[15 - mBit];
                    if (cValue == '1')
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return false;
        }
        private long SignToUnsign(short Sign)
        {
            if (Sign >= 0)
            {
                return (long)Sign;
            }
            else
            {
                return (long)(65536 + Sign);
            }
        }

        private void FrmPLCIOInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClose = true;
        }
    }


}
