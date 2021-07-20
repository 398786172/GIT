using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using OCV.PLCContr.Model;
using OCV.PLCContr.BLL;
using DevComponents.DotNetBar;
using ClsDeviceComm.Controls;

namespace OCV
{
    public partial class FormIO : CCSkinMain
    {
        private List<PLCInfoModel> listXModel = new List<PLCInfoModel>();
        private List<PLCInfoModel> listYModel = new List<PLCInfoModel>();
        List<BitAddressValue> listaddress = new List<BitAddressValue>();
        PLCInfoBLL InfoBLL = new PLCInfoBLL();

        //界面------------------------
        //IO显示
        UserLantern[] arrLbl_XIO;
        UserLantern[] arrLbl_YIO;
        //IO名称
        Label[] arrLbl_XName;
        Label[] arrLbl_YName;
        int YBase_Temp = 10;
        int YIntervel_Temp = 22;
        int XBase_Temp = 10;

        private bool isClose;

        public FormIO()
        {
            InitializeComponent();
        }

        private void FormIO_Load(object sender, EventArgs e)
        {
            InitUIXIO();
            InitUIYIO();
            InitIOAddress();
            Thread Thread_RefreshState = new Thread(new ThreadStart(RefreshState));
            Thread_RefreshState.IsBackground = true;
            Thread_RefreshState.Start();
            Func<bool> myFuc = () =>
            {

                if (isClose == false)
                {
                    Thread.Sleep(1000);
                    return RefreshIO();
                }
                else
                {
                    return false;
                }

            };
            myFuc.BeginInvoke(OnComplete_readStatus, myFuc);

            StartShowTextValue();

            StartShowULanValue();
        }

        private void FormIO_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClose = true;
        }

        /// <summary>
        /// 初始化控件 
        /// </summary>
        /// <param name="list"></param>
        private void InitUIXIO()
        {
            this.listXModel = InfoBLL.GetModelList("LinkName like 'I%'");

            if (this.listXModel == null)
                return;

            if (this.listXModel.Count <= 0)
            {
                return;
            }

            arrLbl_XIO = new UserLantern[this.listXModel.Count];
            arrLbl_XName = new Label[this.listXModel.Count];

            for (int i = 0; i < this.listXModel.Count; i++)
            {
                int j = i / 16;
                int k = i % 16;

                //显示
                this.arrLbl_XIO[i] = new UserLantern();
                this.arrLbl_XIO[i].BackColor = System.Drawing.Color.Transparent;
                this.arrLbl_XIO[i].LanternBackground = System.Drawing.Color.Gray;
                this.arrLbl_XIO[i].Location = new Point(XBase_Temp + 170 * j, YBase_Temp + YIntervel_Temp * k);
                this.arrLbl_XIO[i].Name = this.listXModel[i].LinkName.ToUpper();
                this.arrLbl_XIO[i].Size = new Size(20, 16);
                this.stpIOX.BackColor = Color.White;
                this.stpIOX.Controls.Add(this.arrLbl_XIO[i]);

                this.arrLbl_XName[i] = new Label();
                this.arrLbl_XName[i].AutoSize = true;
                this.arrLbl_XName[i].BackColor = Color.Transparent;
                this.arrLbl_XName[i].Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
                this.arrLbl_XName[i].ForeColor = Color.Black;
                this.arrLbl_XName[i].Location = new Point(50 + 226 * j, 18 + 31 * k);
                //this.arrLbl_XName[i].Margin = new System.Windows.Forms.Padding(2);
                this.arrLbl_XName[i].Name = this.listXModel[i].ControlName;
                this.arrLbl_XName[i].Size = new Size(112, 18);
                this.arrLbl_XName[i].TabIndex = 81;
                this.arrLbl_XName[i].Text = this.listXModel[i].LinkName.ToUpper() + "  " + this.listXModel[i].Memo;
                this.stpIOX.Controls.Add(this.arrLbl_XName[i]);

            }
        }

        private void InitUIYIO()
        {
            this.listYModel = InfoBLL.GetModelList("LinkName like 'Q%'");

            if (this.listYModel == null)
                return;
            if (this.listYModel.Count <= 0)
            {
                return;
            }

            arrLbl_YIO = new UserLantern[this.listYModel.Count];
            arrLbl_YName = new Label[this.listYModel.Count];

            for (int i = 0; i < this.listYModel.Count; i++)
            {
                int j = i / 16;
                int k = i % 16;
                //显示
                this.arrLbl_YIO[i] = new UserLantern();
                this.arrLbl_YIO[i].BackColor = System.Drawing.Color.Transparent;
                this.arrLbl_YIO[i].LanternBackground = System.Drawing.Color.Gray;
                this.arrLbl_YIO[i].Location = new Point(XBase_Temp + 170 * j, YBase_Temp + YIntervel_Temp * k);
                this.arrLbl_YIO[i].Name = this.listYModel[i].LinkName.ToUpper();
                this.arrLbl_YIO[i].Size = new Size(20, 16);
                this.stpIOY.BackColor = Color.White;
                this.stpIOY.Controls.Add(this.arrLbl_YIO[i]);

                this.arrLbl_YName[i] = new Label();
                this.arrLbl_YName[i].AutoSize = true;
                this.arrLbl_YName[i].BackColor = Color.Transparent;
                this.arrLbl_YName[i].Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
                this.arrLbl_YName[i].ForeColor = Color.Black;
                this.arrLbl_YName[i].Location = new Point(50 + 226 * j, 18 + 31 * k);
                //this.arrLbl_XName[i].Margin = new System.Windows.Forms.Padding(2);
                this.arrLbl_YName[i].Name = this.listYModel[i].ControlName;
                this.arrLbl_YName[i].Size = new Size(112, 18);
                this.arrLbl_YName[i].TabIndex = 81;
                this.arrLbl_YName[i].Text = this.listYModel[i].LinkName.ToUpper() + "   " + this.listYModel[i].Memo;
                this.stpIOY.Controls.Add(this.arrLbl_YName[i]);

            }
        }

        private void InitIOAddress()
        {
            foreach (var item in listXModel)
            {
                string address = item.RegisterAddress.Split('.')[0];
                var query = from p in listaddress where p.Address == address select p;
                if (query.Count() == 0)
                {
                    listaddress.Add(new BitAddressValue() { Address = address, Value = 0 });
                }
            }
            foreach (var item in listYModel)
            {
                string address = item.RegisterAddress.Split('.')[0];
                var query = from p in listaddress where p.Address == address select p;
                if (query.Count() == 0)
                {
                    listaddress.Add(new BitAddressValue() { Address = address, Value = 0 });
                }
            }
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

                bool reslut = func.EndInvoke(iar);

                Action act = delegate
                {
                    foreach (var item in this.stpIOX.Controls)
                    {
                        if (item is UserLantern)
                        {
                            UserLantern lbl = item as UserLantern;
                            string linkName = lbl.Name;
                            var query = (from p in listXModel where p.LinkName == linkName.ToUpper() select p).ToList();
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

                    foreach (var item in stpIOY.Controls)
                    {
                        if (item is UserLantern)
                        {
                            UserLantern lbl = item as UserLantern;
                            string linkName = lbl.Name;
                            var query = (from p in listYModel where p.LinkName == linkName.ToUpper() select p).ToList();
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

        private bool RefreshIO()
        {
            foreach (var item in listaddress)
            {
                if (ClsPLCValue.connectSuccess == true)
                {
                    byte result = 0;

                    ClsGlobal.mPLCContr.ReadDB(item.Address, out result);

                    item.Value = result;
                }
            }

            foreach (var item in listXModel)
            {
                item.BValue = ClsGlobal.mPLCContr.GetBoolValue(listaddress, item.RegisterAddress);
            }

            foreach (var item in listYModel)
            {
                item.BValue = ClsGlobal.mPLCContr.GetBoolValue(listaddress, item.RegisterAddress);
            }

            return true;
        }

        #region 机械控制

        private void RefreshState()
        {
            while (true)
            {
                try
                {
                    if (this.IsHandleCreated == true)
                    {
                        //输送CV正转

                        if (ClsPLCValue.PlcValue.Plc_IO_CVRun == 1)
                        {
                            lblCVRun.BackColor = Color.LightGreen;
                            lblCVStop.BackColor = Color.LightGray;
                        }
                        else
                        {
                            lblCVRun.BackColor = Color.LightGray;
                            lblCVStop.BackColor = Color.LightGreen;
                        }
                        //输送CV反转

                        if (ClsPLCValue.PlcValue.Plc_IO_CVRunback == 1)
                        {
                            lblRunback.BackColor = Color.LightGreen;
                            lblStopBack.BackColor = Color.LightGray;
                        }
                        else
                        {
                            lblRunback.BackColor = Color.LightGray;
                            lblStopBack.BackColor = Color.LightGreen;
                        }

                        //托盘定位
                        if (ClsPLCValue.PlcValue.Plc_IO_PosCylUp == 1)
                        {
                            lblPosCylUp.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblPosCylUp.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_PosCylDown == 1)
                        {
                            lblPosCylDown.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblPosCylDown.BackColor = Color.LightGray;
                        }
                        //Probe 出/回
                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 == 1)
                        {
                            lblProbeClose.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblProbeClose.BackColor = Color.LightGray;
                        }

                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 == 1)
                        {
                            lblProbeOpen.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblProbeOpen.BackColor = Color.LightGray;
                        }
                        //M1-M2
                        if (ClsPLCValue.PlcValue.Plc_IO_M1== 1)
                        {
                            lblTypCylPush.BackColor = Color.LightGreen;

                        }
                        else
                        {
                            lblTypCylPush.BackColor = Color.LightGray;

                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_M2 == 1)
                        {
                            lblTypCylBack.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTypCylBack.BackColor = Color.LightGray;
                        }
                        //前段输送对接
                        if (ClsPLCValue.PlcValue.Plc_IO_FrCVRequest == 1)
                        {
                            lblFrCVReq.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblFrCVReq.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_FrOCVAllow == 1)
                        {
                            lblFrOCVAllow.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblFrOCVAllow.BackColor = Color.LightGray;
                        }
                        //后段输送对接
                        if (ClsPLCValue.PlcValue.Plc_IO_BhCVAllow == 1)
                        {
                            lblBhCVAllow.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblBhCVAllow.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_BhOCVReq == 1)
                        {
                            lblBhOCVReq.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblBhOCVReq.BackColor = Color.LightGray;
                        }
                        //装载托盘有无
                        if (ClsPLCValue.PlcValue.Plc_IO_TrayForSignal == 1)
                        {
                            lblTrayForPlace.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTrayForPlace.BackColor = Color.LightGray;
                        }

                        if (ClsPLCValue.PlcValue.Plc_HaveTray == 1)
                        {
                            lblTrayInPlace.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTrayInPlace.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_jiantuopandaowei == 1)
                        {
                            lblTraybackPlace.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTraybackPlace.BackColor = Color.LightGray;
                        }
                        //阻挡气缸
                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose == 1)
                        {
                            lblBlockUp.BackColor = Color.LightGreen;
                            lblBlockDown.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen == 1)
                        {
                            lblBlockDown.BackColor = Color.LightGreen;
                            lblBlockUp.BackColor = Color.LightGray;
                        }

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion

        private void btn_Read_Click_1(object sender, EventArgs e)
        {

            try
            {
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(txt_Radder.Text, out val);
                txt_Rval.Text = val.ToString();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Write_Click_1(object sender, EventArgs e)
        {
            try
            {
                //short val = short.Parse(txt_Wval.Text);
                byte val =byte.Parse(txt_Wval.Text);
                //int val = int.Parse(txt_Wval.Text);
                //uint val = uint.Parse(txt_Wval.Text);
                ClsGlobal.mPLCContr.WriteDB(txt_Wadder.Text, val);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSconCode_Click_1(object sender, EventArgs e)
        {
            if (ClsGlobal.CodeScanMode == 1)
            {
                labReadCode.Text = null;
                labReadCode.Text = ClsGlobal.CodeScan.SocketReadCode();
            }
            else
            {
                labReadCode.Text = ClsGlobal.CodeScan.ReadCode();
            }
        }

        private void btnPosUp_Click_1(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_顶升气缸控制, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnPosDown_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_顶升气缸控制, 2);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnProveClose_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_针床气缸控制, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnProveOpen_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_针床气缸控制, 2);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTypCylPush_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_滚筒M1M2, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTypCylBack_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_滚筒M1M2, 2);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCVRun_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_滚筒控制, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCVStop_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_滚筒控制, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnRunback_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_滚筒控制, 2);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnStopBack_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_滚筒控制, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Allowopen_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_允许送入信号控制, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Allowclose_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_允许送入信号控制, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Reqopen_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_允许送出信号控制, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Reqclose_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_允许送出信号控制, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnPowerOn_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽蜂鸣器, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽蜂鸣器, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetSpeed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetSpeed.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetSpeed.Text = "";
                    return;
                }
                short speed =(short) val;
                ClsGlobal.mPLCContr.DevMove_ChangeSpeed(speed);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "MoveSpeed", speed.ToString());
                ClsGlobal.MoveSpeed = speed;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnSetAccTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetAccTime.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetAccTime.Text = "";
                    return;
                }
                short AccTime = (short)val;
                ClsGlobal.mPLCContr.DevMove_ChangeAccTime(AccTime);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "AccTime", AccTime.ToString());
                ClsGlobal.AccTime = AccTime;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos1.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos1.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 1);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos1Value", SetPos.ToString());
                ClsGlobal.SetPos1 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos2.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos2.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 2);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos2Value", SetPos.ToString());
                ClsGlobal.SetPos2 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos3.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos3.Text = "";
                    return;
                }
                //ushort SetPos =(ushort) val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 3);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos3Value", SetPos.ToString());
                ClsGlobal.SetPos3 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos4.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos4.Text = "";
                    return;
                }
                //ushort SetPos =(ushort) val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 4);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos4Value", SetPos.ToString());
                ClsGlobal.SetPos4 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos5.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos5.Text = "";
                    return;
                }
                //ushort SetPos =(ushort) val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 5);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos5Value", SetPos.ToString());
                ClsGlobal.SetPos5 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos6.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos6.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 6);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos6Value", SetPos.ToString());
                ClsGlobal.SetPos6 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos7_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos7.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos7.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 7);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos7Value", SetPos.ToString());
                ClsGlobal.SetPos7 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos8.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos8.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 8);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos8Value", SetPos.ToString());
                ClsGlobal.SetPos8 = SetPos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos9_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos9.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos9.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 9);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos9Value", SetPos.ToString());
                ClsGlobal.SetPos9 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetHome_Click(object sender, EventArgs e)
        {
            ClsGlobal.mPLCContr.DevMove_SetHome();
            Thread.Sleep(100);
            if (ClsGlobal.mPLCContr.GetState_ZeroIng() == false)
            {
                Thread.Sleep(100);
                if (ClsGlobal.mPLCContr.GetState_ZeroIng() == false && ClsGlobal.mPLCContr.GetState_ZeroCompletion() == false)
                {
                    MessageBox.Show("回零未启动");
                }
            }
            ClsGlobal.mPLCContr.DevMove_ReSetHome();
        }

        private void btnResetAlm_Click(object sender, EventArgs e)
        {
            ClsGlobal.mPLCContr.Set_PLCReset();
            Thread.Sleep(1000);
            ClsGlobal.mPLCContr.ReSet_PLCReset();
        }

        #region 读取PLC数据

        /// <summary>
        /// 
        /// </summary>
        private void StartShowTextValue()
        {
            var myFunc = FuncGetTextValue();

            myFunc.BeginInvoke(OnComplete_TextValue, myFunc);
        }

        private Func<Dictionary<TextBox, int>> FuncGetTextValue()
            => ()
            => ReadTextValue();

        private void OnComplete_TextValue(IAsyncResult iar)
        {
            try
            {
                Func<Dictionary<TextBox, int>> myFunc = (Func<Dictionary<TextBox, int>>)iar.AsyncState;
                Dictionary<TextBox, int> resultDic = myFunc.EndInvoke(iar);

                foreach (var item in resultDic.Keys)
                {
                    Action act = delegate
                    {
                        item.Text = resultDic[item].ToString();
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

                Thread.Sleep(5000);

                myFunc.BeginInvoke(OnComplete_TextValue, myFunc);
            }
            catch
            {

            }
        }

        /// <summary>
        ///从plc 读取所需要的text 数据
        /// </summary>
        /// <returns></returns>
        private Dictionary<TextBox, int> ReadTextValue()
        {
            Dictionary<TextBox, int> dic = new Dictionary<TextBox, int>();

            try
            {
                var CurrSpeed = ClsGlobal.mPLCContr.DevMove_CurrentSpeed();
                dic.Add(txtCurrSpeed, CurrSpeed);

                var CurrAccTime = ClsGlobal.mPLCContr.DevMove_CurrentAccTime();
                dic.Add(txtCurrAccTime, CurrAccTime);

                var CurrPos = ClsGlobal.mPLCContr.DevMove_CurrentPos();
                dic.Add(txtCurrPos, CurrPos);

                var CurrPos1 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(1);
                dic.Add(txtCurrPos1, CurrPos1);

                var CurrPos2 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(2);
                dic.Add(txtCurrPos2, CurrPos2);

                var CurrPos3 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(3);
                dic.Add(txtCurrPos3, CurrPos3);

                var CurrPos4 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(4);
                dic.Add(txtCurrPos4, CurrPos4);

                var CurrPos5 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(5);
                dic.Add(txtCurrPos5, CurrPos5);

                var CurrPos6 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(6);
                dic.Add(txtCurrPos6, CurrPos6);

                var CurrPos7 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(7);
                dic.Add(txtCurrPos7, CurrPos7);

                var CurrPos8 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(8);
                dic.Add(txtCurrPos8, CurrPos8);

                var CurrPos9 = ClsGlobal.mPLCContr.DevMove_ReadSetPos(9);
                dic.Add(txtCurrPos9, CurrPos9);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartShowULanValue()
        {
            var myFunc = FuncGetULanValue();

            myFunc.BeginInvoke(OnComplete_ULanValue, myFunc);
        }

        private Func<Dictionary<UserLantern, Color>> FuncGetULanValue()
           => ()
           => ReadULanValue();

        private void OnComplete_ULanValue(IAsyncResult iar)
        {
            try
            {
                Func<Dictionary<UserLantern, Color>> myFunc = (Func<Dictionary<UserLantern, Color>>)iar.AsyncState;
                Dictionary<UserLantern, Color> resultDic = myFunc.EndInvoke(iar);
                foreach (var item in resultDic.Keys)
                {
                    Action act = delegate
                    {
                        item.LanternBackground = resultDic[item];
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
                Thread.Sleep(5000);
                myFunc.BeginInvoke(OnComplete_ULanValue, myFunc);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<UserLantern, Color> ReadULanValue()
        {
            Dictionary<UserLantern, Color> dic = new Dictionary<UserLantern, Color>();
            var CurrSpeed = ClsGlobal.mPLCContr.DevMove_CurrentSpeed();

            //回零完成
            var HomeFinish = ClsGlobal.mPLCContr.GetState_ZeroCompletion() ? Color.Lime : Color.LightGray;
            dic.Add(lblHomeFinish, HomeFinish);

            //回零中
            var HomeIng = ClsGlobal.mPLCContr.GetState_ZeroIng() ? Color.Lime : Color.LightGray;
            dic.Add(lblHomeIng, HomeIng);

            //正限位
            var PosLimit = ClsGlobal.mPLCContr.GetState_PosLimit() ? Color.Lime:Color.Red;
            dic.Add(lblPosLimit, PosLimit);

            //原点
            var Home = ClsGlobal.mPLCContr.GetState_HomeLimit() ? Color.Lime : Color.LightGray;
            dic.Add(lblHome, Home);

            //负限位
            var NegLimit = ClsGlobal.mPLCContr.GetState_NegLimit() ? Color.Lime : Color.Red;
            dic.Add(lblNegLimit, NegLimit);
            return dic;
        }

        #endregion read plc data and status

        private void btnMoveStartInc_Click(object sender, EventArgs e)
        {
            int val;
            if (int.TryParse(txtPosInc.Text, out val) == false)
            {
                MessageBox.Show("输入的不是数字");
                txtPosInc.Text = "";
                return;
            }
            int Pos = val;
            ClsGlobal.mPLCContr.DevMove_Inc(0, Pos);
        }

        private void btnMoveStartNO_Click(object sender, EventArgs e)
        {
            int val;
            if (int.TryParse(txtPosNO.Text, out val) == false)
            {
                MessageBox.Show("输入的不是数字");
                txtPosNO.Text = "";
                return;
            }
            if (val > 13 || val < 1)
            {
                MessageBox.Show("输入数字只能在1~13");
                txtPosNO.Text = "";
                return;
            }
            if (ClsGlobal.mPLCContr.GetState_ZeroCompletion() == false)
            {
                MessageBox.Show("设备未回原");
                txtPosNO.Text = "";
                return;
            }

            short num = (short)val;
            ClsGlobal.mPLCContr.DevMove_AbsNO(0, num);
        }

        private void btn_jogpos_MouseDown(object sender, MouseEventArgs e)
        {
            int val;
            if (int.TryParse(txtSetSpeed.Text, out val) == true)
            {
                if (val < 0 || val > 32000)
                {
                    MessageBox.Show("输入数据范围出错");
                    txtSetSpeed.Text = "";
                    return;
                }
            }
            else
            {
                MessageBox.Show("输入的不是数字");
                txtSetSpeed.Text = "";
                return;
            }

            short speed = (short)val;
            ClsGlobal.mPLCContr.DevMove_Pos(speed);
            btn_jogpos.BackColor = Color.Green;
        }

        private void btn_jogpos_MouseUp(object sender, MouseEventArgs e)
        {
            ClsGlobal.mPLCContr.DevMove_Stop();
            btn_jogpos.BackColor = Color.SteelBlue;
        }

        private void btn_jogneg_MouseDown(object sender, MouseEventArgs e)
        {
            int val;
            if (int.TryParse(txtSetSpeed.Text, out val) == true)
            {
                if (val < 0 || val > 32000)
                {
                    MessageBox.Show("输入数据范围出错");
                    txtSetSpeed.Text = "";
                    return;
                }
            }
            else
            {
                MessageBox.Show("输入的不是数字");
                txtSetSpeed.Text = "";
                return;
            }

            short speed = (short)val;
            ClsGlobal.mPLCContr.DevMove_Neg(speed);
            btn_jogneg.BackColor = Color.Green;
        }

        private void btn_jogneg_MouseUp(object sender, MouseEventArgs e)
        {
            ClsGlobal.mPLCContr.DevMove_Stop();
            btn_jogneg.BackColor = Color.SteelBlue;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽门开关, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽门开关, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_伺服使能, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_伺服使能, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private bool Red_Checked_State = false;
        private bool Yellow_Checked_State = false;
        private bool Green_Checked_State = false;
        private bool Buzzer_Checked_State = false;
        private void chkRed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Red_Checked_State)
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_红, 1);
                    Red_Checked_State = true;
                }
                else
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_红, 0);
                    Red_Checked_State = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void chkYellow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Yellow_Checked_State)
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_黄, 1);
                    Yellow_Checked_State = true;
                }
                else
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_黄, 0);
                    Yellow_Checked_State = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void chkGreen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Green_Checked_State)
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_绿, 1);
                    Green_Checked_State = true;
                }
                else
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_绿, 0);
                    Green_Checked_State = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void chkBuzzer_CheckedChanged(object sender, EventArgs e)
        {
            try
            { if (!Buzzer_Checked_State)
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_蜂鸣器, 1);
                    Buzzer_Checked_State = true;
                }
                else
                {
                    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_蜂鸣器, 0);
                    Buzzer_Checked_State = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          ClsGlobal. mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_指示上位机有报警, 1);
        }

        private void btnSetPos10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos10.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos10.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 10);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos10Value", SetPos.ToString());
                ClsGlobal.SetPos10 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos11_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos11.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos11.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 11);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos11Value", SetPos.ToString());
                ClsGlobal.SetPos11 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos12_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos12.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos12.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 12);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos12Value", SetPos.ToString());
                ClsGlobal.SetPos12 = SetPos;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPos13_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int val;
                if (int.TryParse(txtSetPos13.Text, out val) == false)
                {
                    MessageBox.Show("输入的不是数字");
                    txtSetPos13.Text = "";
                    return;
                }
                //ushort SetPos = (ushort)val;
                int SetPos = val;
                ClsGlobal.mPLCContr.DevMove_ChangePos(SetPos, 13);
                Helper.UpdateXmlNode(ClsGlobal.xmlPath, "Pos13Value", SetPos.ToString());
                ClsGlobal.SetPos13 = SetPos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBlockUp_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_阻挡气缸控制, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnBlockDown_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_阻挡气缸控制, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

    }
}
