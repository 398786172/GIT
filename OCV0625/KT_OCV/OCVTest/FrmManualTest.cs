using DB_OCV.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;


namespace OCV
{
    public partial class FrmManualTest : Form
    {

        #region 测试

        public FrmManualTest()
        {
            InitializeComponent();
        }

        private void FrmOCV_Load(object sender, EventArgs e)
        {
            try
            {
                #region 界面初始化
                if (ClsGlobal.OCVType == 1)
                {
                    groupBox6.Visible = false;
                    groupBox8.Visible = false;
                    grpBT4560.Visible = false;

                }
                else
                {
                    groupBox6.Visible = true;
                    groupBox8.Visible = true;
                    grpBT4560.Visible = true;

                }

                dgvManualTest.Rows.Add(ClsGlobal.TrayType);
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    dgvManualTest.Rows[i].Cells[0].Value = i + 1;
                }
                grpbxTestManual.Enabled = true;

                #endregion

            }
            catch (Exception ex)
            {

            }
        }

        private void FrmManualTestOCV_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ClearDataGrid()
        {
            dgvManualTest.Rows.Clear();
            dgvManualTest.Rows.Add(ClsGlobal.TrayType);
            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                dgvManualTest.Rows[i].Cells[0].Value = i + 1;
            }
        }

        #endregion

        #region 多通道手动测试
        #endregion
        /// <summary>
        /// 多通道温度测试,采用温度控制板
        /// </summary>
        private void MultiChannelTempTest_TempBoard()
        {
            double[] tempVal;
            try
            {

                //界面处理->表格数据清空
                int Val = ClsGlobal.TrayType;

                for (int i = 0; i < Val; i++)
                {
                    this.dgvManualTest.Rows[i].Cells["TempData"].Value = "";
                    this.dgvManualTest.Rows[i].Cells["TempData2"].Value = "";
                }
                //this.mForm.dgvTest.Rows.Clear();


                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    dgvManualTest.Rows[i].Cells["TempData"].Value = ClsGlobal.TempContr.Anodetemperature[i] + double.Parse(ClsGlobal.mTempAdjustVal_P[i]);
                    dgvManualTest.Rows[i].Cells["TempData2"].Value = ClsGlobal.TempContr.Poletemperature[i] + double.Parse(ClsGlobal.mTempAdjustVal_N[i]);
                }
                dgvManualTest.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取温度值失败");
            }
        }

        private void SaveTest_csv()
        {
            try
            {

                string _sFilePath = Application.StartupPath + "\\TestFiles\\";
                if (!Directory.Exists(_sFilePath))
                {
                    Directory.CreateDirectory(_sFilePath);
                }
                string addr = _sFilePath + "DebugTest" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                using (StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default))
                {
                    SWR.WriteLine("通道号, 正/负电压(mV), 壳体/负电压(mV),内阻(mΩ),正极温度(°C)，负极温度(°C)");

                    for (int i = 0; i <= dgvManualTest.RowCount - 2; i++)
                    {
                        if (dgvManualTest[1, i].Value == null)
                        {
                            dgvManualTest[1, i].Value = "";
                        }
                        if (dgvManualTest[2, i].Value == null)
                        {
                            dgvManualTest[2, i].Value = "";
                        }
                        if (dgvManualTest[3, i].Value == null)
                        {
                            dgvManualTest[3, i].Value = "";
                        }
                        if (dgvManualTest[4, i].Value == null)
                        {
                            dgvManualTest[4, i].Value = "";
                        }
                        if (dgvManualTest[5, i].Value == null)
                        {
                            dgvManualTest[5, i].Value = "";
                        }
                        SWR.WriteLine(dgvManualTest[0, i].Value.ToString() + "," +
                        dgvManualTest[1, i].Value.ToString() + "," +
                        dgvManualTest[2, i].Value.ToString() + "," +
                        dgvManualTest[3, i].Value.ToString() + "," +
                        dgvManualTest[4, i].Value.ToString() + "," +
                        dgvManualTest[5, i].Value.ToString());
                    }

                    SWR.Close();
                }
                MessageBox.Show("数据保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据保存失败");
            }
        }

        private void tim_UI_Tick(object sender, EventArgs e)
        {
            if (ClsGlobal.OCVTestContr == null)
            {
                return;
            }
            if (ClsGlobal.OCVTestContr.ManualTestFinish == true)
            {
                grpbxTestManual.Enabled = true;
                btnTestMultiVolt_PosNeg.Text = "测正负极电压";
                btnTestMultiVolt_ShellNeg.Text = "测壳体负极电压";
                btnTestMultiIR_BT4560.Text = "测BT4560内阻";
            }

        }

        private void UpdateUIText(Button btn, string text)
        {
            Action act = delegate
            {
                btn.Text = text;
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

        private void btnTestMultiVolt_PosNeg_Click(object sender, EventArgs e)
        {
            
            try
            {
                grpbxTestManual.Enabled = false;
                btnTestMultiVolt_PosNeg.Text = "测电压中...";
                ClsGlobal.OCVTestContr.StartManualTestVolt_PosNeg_Action(this,()=> { UpdateUIText(btnTestMultiVolt_PosNeg, "测正负极电压"); });
            }
            catch
            {
                MessageBox.Show("读取电压值失败");
            }
        }

        private void btnTestMultiVolt_ShellNeg_Click(object sender, EventArgs e)
        {
            try
            {
                grpbxTestManual.Enabled = false;
                btnTestMultiVolt_ShellNeg.Text = "测壳体电压中...";
                ClsGlobal.OCVTestContr.StartManualTestVolt_ShellNeg_Action(this, 2);

            }
            catch
            {
                MessageBox.Show("读取壳体电压值失败");
            }
        }

        private void btnTestMultiIR_BT4560_Click(object sender, EventArgs e)
        {
            try
            {
                grpbxTestManual.Enabled = false;
                btnTestMultiIR_BT4560.Text = "测ACIR中...";
                ClsGlobal.OCVTestContr.StartManualTestIR_BT4560_Action(this);
            }
            catch
            {
                MessageBox.Show("读取ACIR值失败");
            }
        }


        private void btnDmm_Init_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.DMM_Ag344X.InitControl_IMM();
            }
            catch
            {
                MessageBox.Show("万用表初始化失败");
            }
        }

        private void btnIRBT4_Init_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.HIOKI4560[0].InitControl_IMM();
                ClsGlobal.OCVTestContr.HIOKI4560[1].InitControl_IMM();
            }
            catch
            {
                MessageBox.Show("内阻仪初始化失败");
            }
        }

        private void btnStartTempTest_Click_1(object sender, EventArgs e)
        {
            MultiChannelTempTest_TempBoard();
            //MultiChannelTempTest();
        }

        private void btnSaveTest_Click_1(object sender, EventArgs e)
        {
            SaveTest_csv();

        }

        private void btnSUB_V_Click(object sender, EventArgs e)
        {
            int Num;

            if (int.TryParse(txtChannel.Text, out Num) == true && Num > 1)
            {
                txtChannel.Text = (Num - 1).ToString();
            }
            else
            {
                txtChannel.Text = "1";
            }
        }

        private void btnAdd_V_Click(object sender, EventArgs e)
        {
            int Num;

            if (int.TryParse(txtChannel.Text, out Num) == true && Num > 0)
            {
                txtChannel.Text = (Num + 1).ToString();
            }
            else
            {
                txtChannel.Text = "1";
            }
        }

        private void btnSWCH_PosNeg_Click(object sender, EventArgs e)
        {
            try
            {
                int resCH;

                if (int.TryParse(txtChannel.Text, out resCH) == true)
                {
                    if (ClsGlobal.OCVType == 1)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitch(1, resCH);
                    }
                    else
                    {
                        if (resCH <= 13)
                        {
                            ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitch(1, 0);
                            ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitch(1, 0);
                            ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitch(1, resCH);   //正极对负极
                        }
                        if (resCH >= 14 && resCH <= 26)
                        {
                            ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitch(1, 0);
                            ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitch(1, 0);
                            ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitch(1, resCH - 13);   //正极对负极
                        }
                        if (resCH >= 27)
                        {
                            ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitch(1, 0);
                            ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitch(1, 0);
                            ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitch(1, resCH - 26);   //正极对负极
                        }
                    }


                }
                else
                {
                    MessageBox.Show("通道输入出错");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("通道输入出错" + ex.Message);
            }

        }

        private void btnStopSW_PosNeg_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClsGlobal.OCVType == 1)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitch(1, 0);
                }
                else
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitch(1, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitch(1, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitch(1, 0);
                }

            }
            catch (Exception)
            {
            }
        }

        //壳体对负极测量
        private void btnSWCH_ShellNeg_Click(object sender, EventArgs e)
        {
            try
            {
                int resCH;

                if (int.TryParse(txtChannel.Text, out resCH) == true)
                {
                    if (resCH <= 13)
                    {
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(2, resCH);   //壳体对负极
                    }
                    if (resCH >= 14 && resCH <= 26)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(2, resCH - 13);   //壳体对负极
                    }
                    if (resCH >= 27)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(2, 0);   //壳体对负极
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(2, 0);   //壳体对负极
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(2, resCH - 26);
                    }
                }
                else
                {
                    MessageBox.Show("通道输入出错");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("通道输入出错" + ex.Message);
            }
        }

        private void btnStopSW_ShellNeg_Click(object sender, EventArgs e)
        {

            try
            {
                ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(2, 0);
                ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(2, 0);
                ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(2, 0);
            }
            catch (Exception)
            {
            }
        }

        private void btnSWCH_IRBT4_Click(object sender, EventArgs e)
        {
            try
            {
                int resCH;
                if (int.TryParse(txtChannel.Text, out resCH) == true)
                {
                    if (resCH <= 13)
                    {
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, resCH);   //壳体对负极
                    }
                    if (resCH >= 14 && resCH <= 26)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, resCH - 13);   //壳体对负极
                    }
                    if (resCH >= 27)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);   //壳体对负极
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);   //壳体对负极
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, resCH - 26);
                    }
                }
                else
                {
                    MessageBox.Show("通道输入出错");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("通道输入出错" + ex.Message);
            }
        }

        private void btnStopSW_IRBT4_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
            }
            catch (Exception)
            {
            }
        }

        private void btnTestVolt_Click(object sender, EventArgs e)
        {
            try
            {
                double Val;
                //OCVTestContr.mDmm.InitControl_IMM();
                ClsGlobal.OCVTestContr.DMM_Ag344X.ReadVolt(out Val);
                txtVolt.Text = (Val * 1000).ToString("f2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.Message.ToString());
            }
        }

        private void btnInit_TestVolt_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.DMM_Ag344X.InitControl_IMM();
            }
            catch
            {
                MessageBox.Show("初始化失败");
            }
        }

        private void btnTestIRBT4_Click(object sender, EventArgs e)
        {
            btnTestIRBT4.Enabled = false;
            try
            {
                int resCH;
                if (int.TryParse(txtChannel.Text, out resCH) == true)
                {
                    double Val = 0;
                    if (resCH <= 13)
                    {

                        ClsGlobal.OCVTestContr.HIOKI4560[0].ReadRData(out Val);
                        txtACIR.Text = (Val * 1000).ToString("f7");
                    }
                    if (resCH >= 14 && resCH <= 26)
                    {
                        ClsGlobal.OCVTestContr.HIOKI4560[1].ReadRData(out Val);
                        txtACIR.Text = (Val * 1000).ToString("f7");
                    }
                    if (resCH >= 27)
                    {
                        ClsGlobal.OCVTestContr.HIOKI4560[2].ReadRData(out Val);
                        txtACIR.Text = (Val * 1000).ToString("f7");
                    }
                    double maxValue = 0;
                    double minValue = 0;
                    Val = Val * 1000;
                    if (double.TryParse(txtMax.Text, out maxValue) == false)
                    {
                        maxValue = -99999;
                    }
                    if (double.TryParse(txtMin.Text, out minValue) == false)
                    {
                        minValue = 99999;
                    }
                    if (Val > maxValue)
                    {
                        maxValue = Val;
                        txtMax.Text = Val.ToString();
                    }
                    if (Val < minValue)
                    {
                        minValue = Val;
                        txtMin.Text = Val.ToString();
                    }
                    double result = maxValue - minValue;
                    txtResult.Text = result.ToString();
                    if (result >= 0.04)
                    {
                        string txt = "{0}-{1}={2}";
                        string msg = string.Format(txt, maxValue, minValue, result);
                        listBox1.Items.Add(msg);
                    }

                }
                else
                {
                    MessageBox.Show("通道输入出错");
                }
                btnTestIRBT4.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.Message.ToString());
            }
        }

        private void btnInit_TestIRBT4_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.HIOKI4560[0].InitControl_IMM();
                ClsGlobal.OCVTestContr.HIOKI4560[1].InitControl_IMM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("内阻仪初始化失败：" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int resCH;

                if (int.TryParse(txtChannel.Text, out resCH) == true)
                {
                    if (resCH <= 13)
                    {
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(3, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(3, 0);
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(3, resCH);   //壳体对负极
                    }
                    if (resCH >= 14 && resCH <= 26)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(3, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(3, 0);
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(3, resCH - 13);   //壳体对负极
                    }
                    if (resCH >= 27)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(3, 0);   //壳体对负极
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(3, 0);   //壳体对负极
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(3, resCH - 26);
                    }
                }
                else
                {
                    MessageBox.Show("通道输入出错");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("通道输入出错" + ex.Message);
            }
        }

        private void btnPostiveSVTest_Click(object sender, EventArgs e)
        {
            try
            {
                grpbxTestManual.Enabled = false;
                btnTestMultiVolt_ShellNeg.Text = "测壳体电压中...";
                ClsGlobal.OCVTestContr.StartManualTestVolt_ShellNeg_Action(this, 3);

            }
            catch
            {
                MessageBox.Show("读取壳体电压值失败");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.csv|*.csv";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                if (SaveData(path))
                {
                    MessageBox.Show("保存成功！");
                }
            }
        }

        public bool SaveData(string path)
        {
            try
            {
                using (StreamWriter SWR = new StreamWriter(path, false, Encoding.Default))
                {
                    SWR.WriteLine("通道号,开路电压(V),壳体/正极电压(V),壳体/负极电压(V),ACIR(mΩ)),正极温度(℃),负极温度(℃)");
                    int row = dgvManualTest.Rows.Count;
                    for (int i = 0; i < row - 1; i++)
                    {
                        string Number = dgvManualTest.Rows[i].Cells["Number"].Value.ToString();
                        string Volt_PosNeg = dgvManualTest.Rows[i].Cells["Volt_PosNeg"].Value.ToString();
                        string Volt_ShellNeg2 = 0.ToString(); //dgvManualTest.Rows[i].Cells["Volt_ShellNeg2"].Value.ToString();
                        string Volt_ShellNeg = dgvManualTest.Rows[i].Cells["Volt_ShellNeg"].Value.ToString();
                        string ACIR_BT4 = dgvManualTest.Rows[i].Cells["ACIR_BT4"].Value.ToString();
                        string TempData = 0.ToString();//dgvManualTest.Rows[i].Cells["TempData"].Value.ToString();
                        string TempData2 = 0.ToString();//dgvManualTest.Rows[i].Cells["TempData2"].Value.ToString();
                        string txt = "{0},{1},{2},{3},{4},{5},{6}";
                        string info = string.Format(txt, Number, Volt_PosNeg, Volt_ShellNeg2, Volt_ShellNeg, ACIR_BT4, TempData, TempData2);
                        SWR.WriteLine(info);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("save error:" + ex.Message);
                return false;
            }
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                ClsGlobal.OCVTestContr.SetForm(this);
                string path = folderBrowserDialog1.SelectedPath;
                try
                {
                    grpbxTestManual.Enabled = false;
                    ClsGlobal.OCVTestContr.CreateData(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("save error:" + ex.Message);
                }

            }
        }

        public void SetEnable(bool value)
        {
            Action act = delegate
              {
                  grpbxTestManual.Enabled = value;
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

        public void SetNum(int num)
        {
            Action act = delegate
              {
                  lblNum.Text = num.ToString();
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

        public void ClearDgv(string name)
        {

            Action act = delegate
              {
                  //界面处理->表格数据清空
                  int Val = ClsGlobal.TrayType;
                  if (dgvManualTest.Rows.Count == 0)
                      return;
                  for (int i = 0; i < Val; i++)
                  {
                      dgvManualTest.Rows[i].Cells[name].Value = "";

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (btnTestIRBT4.Enabled == true)
                btnTestIRBT4_Click(null, null);
            timer1.Enabled = true;
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
