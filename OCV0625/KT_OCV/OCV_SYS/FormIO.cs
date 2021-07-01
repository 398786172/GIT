﻿using System;
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
        UserLantern [] arrLbl_XIO;
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
        }
        /// <summary>
        /// 初始化控件 
        /// </summary>
        /// <param name="list"></param>
        private void InitUIXIO()
        {
            this.listXModel = InfoBLL.GetModelList("LinkName like 'X%'");
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
            this.listYModel = InfoBLL.GetModelList("LinkName like 'Y%'");
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
                            string linkName =lbl.Name;
                            var query = (from p in listXModel where p.LinkName == linkName.ToLower() select p).ToList();
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
                            var query = (from p in listYModel where p.LinkName == linkName.ToLower() select p).ToList();
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

            Thread.Sleep(1000);
            func.BeginInvoke(OnComplete_readStatus, iar.AsyncState);

        }
        private bool RefreshIO()
        {
            foreach (var item in listaddress)
            {
                if (ClsPLCValue.connectSuccess == true)
                {
                    ushort result = 0;
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
                        if (ClsPLCValue.PlcValue.Plc_IO_PosCylUp1 == 1)
                        {
                            lblPosCylUp1.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblPosCylUp1.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_PosCylUp2 == 1)
                        {
                            lblPosCylUp2.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblPosCylUp2.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_PosCylDown1 == 1)
                        {
                            lblPosCylDown1.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblPosCylDown1.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_PosCylDown2 == 1)
                        {
                            lblPosCylDown2.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblPosCylDown2.BackColor = Color.LightGray;
                        }

                        //Probe 出/回
                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose1 == 1|| ClsPLCValue.PlcValue.Plc_IO_ProbeCylCloseS1==1)
                        {
                            lblProbeClose1.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblProbeClose1.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylClose2 == 1|| ClsPLCValue.PlcValue.Plc_IO_ProbeCylCloseS2 == 1)
                        {
                            lblProbeClose2.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblProbeClose2.BackColor = Color.LightGray;
                        }

                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen1 == 1)
                        {
                            lblProbeOpen1.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblProbeOpen1.BackColor = Color.LightGray;
                        }

                        if (ClsPLCValue.PlcValue.Plc_IO_ProbeCylOpen2 == 1)
                        {
                            lblProbeOpen2.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblProbeOpen2.BackColor = Color.LightGray;
                        }

                        //阻挡气缸
                        //if (ClsPLCValue.PlcValue.Plc_IO_BlockCylUp == 1)
                        //{
                        //    lblTypCylPush.BackColor = Color.LightGreen;

                        //}
                        //else
                        //{
                        //    lblTypCylPush.BackColor = Color.LightGray;

                        //}
                        //if (ClsPLCValue.PlcValue.Plc_IO_BlockCylDown == 1)
                        //{
                        //    lblTypCylBack.BackColor = Color.LightGreen;
                        //}
                        //else
                        //{
                        //    lblTypCylBack.BackColor = Color.LightGray;
                        //}
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
                        if (ClsPLCValue.PlcValue.Plc_HaveTray == 1)
                        {
                            lblTrayInPlace.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTrayInPlace.BackColor = Color.LightGray;
                        }

                        if (ClsPLCValue.PlcValue.Plc_IO_TrayForSignal == 1)
                        {
                            lblTrayForPlace.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTrayForPlace.BackColor = Color.LightGray;
                        }

                        if (ClsPLCValue.PlcValue.Plc_IO_SlowSpeedSignal == 1)
                        {
                            lblTraySlowDown.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTraySlowDown.BackColor = Color.LightGray;
                        }
                        if (ClsPLCValue.PlcValue.Plc_IO_TrayInSignal == 1)
                        {
                            lblTraybackPlace.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            lblTraybackPlace.BackColor = Color.LightGray;
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
                ushort val = ushort.Parse(txt_Wval.Text);
                ClsGlobal.mPLCContr.WriteDB(txt_Wadder.Text, val);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnSconCode_Click_1(object sender, EventArgs e)
        {
            labReadCode.Text = ClsGlobal.CodeScan.ReadCode();
        }
        private void btnPosUp_Click_1(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_顶升气缸控制, 1);
                //Action act = delegate
                //{
                //    Thread.Sleep(1000);
                //    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_顶升气缸控制, 0);
                //};
                //act.BeginInvoke(null,null);

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
                //Action act = delegate
                //{
                //    Thread.Sleep(1000);
                //    ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_顶升气缸控制, 0);
                //};
               // act.BeginInvoke(null, null);
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
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_阻挡气缸控制, 1);
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
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_阻挡气缸控制, 0);
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
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_取电针控制, 1);
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
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_取电针控制, 0);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void FormIO_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClose = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽门开关, out val);
                var value = val ^ 1;
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽门开关, (ushort)value);
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
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(ClsGlobal.mPLCContr.mPlcAddr.PC_红,out val);
                var value = val ^ 1;
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_红, (ushort)value);
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
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(ClsGlobal.mPLCContr.mPlcAddr.PC_黄, out val);
                var value = val ^ 1;
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_黄, (ushort)value);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(ClsGlobal.mPLCContr.mPlcAddr.PC_绿, out val);
                var value = val ^ 1;
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_绿, (ushort)value);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(ClsGlobal.mPLCContr.mPlcAddr.PC_蜂鸣器, out val);
                var value = val ^ 1;
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_蜂鸣器, (ushort)value);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                short val = 0;
                ClsGlobal.mPLCContr.ReadDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽蜂鸣器, out val);
                var value = val ^ 1;
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_屏蔽蜂鸣器, (ushort)value);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_M, 1);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.mPLCContr.WriteDB(ClsGlobal.mPLCContr.mPlcAddr.PC_M, 2);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
