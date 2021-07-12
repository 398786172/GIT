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
                    this.dgvManualTest.Rows[i].Cells[4].Value = "";
                    this.dgvManualTest.Rows[i].Cells[5].Value = "";
                }
                //this.mForm.dgvTest.Rows.Clear();


                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    dgvManualTest.Rows[i].Cells[4].Value = ClsGlobal.TempContr.Anodetemperature[i] + double.Parse(ClsGlobal.mTempAdjustVal_P[i]);
                    dgvManualTest.Rows[i].Cells[5].Value = ClsGlobal.TempContr.Poletemperature[i] + double.Parse(ClsGlobal.mTempAdjustVal_N[i]);
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
                string addr = _sFilePath+ "DebugTest" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
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
                btnTestMultiIR_BT4560.Text = "测BT3562内阻";
            }
            if (ClsGlobal.ManualMessInfo !="" )
            {
                mRefreshTextAlarmA(ClsGlobal.ManualMessInfo);
                ClsGlobal.ManualMessInfo = "";
            }
           
        }

        private void btnTestMultiVolt_PosNeg_Click(object sender, EventArgs e)
        {
            try
            {
                grpbxTestManual.Enabled = false;
                btnTestMultiVolt_PosNeg.Text = "测电压中...";
                ClsGlobal.OCVTestContr.StartManualTestVolt_PosNeg_Action(this);
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
                ClsGlobal.OCVTestContr.StartManualTestVolt_ShellNeg_Action(this);
              
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
                ClsGlobal.OCVTestContr.HIOKI365X.InitControl_IMM(2);
               
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
            int resCH;

            if (int.TryParse(txtChannel.Text, out resCH) == true)
            {
                ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(1, resCH);//正极对负极   
                //ClsGlobal.OCVTestContr.SWControl.ChannelVoltSwitchContr(1, resCH);//正极对负极       
            }
            else
            {
                MessageBox.Show("通道输入出错");
            }
        }

        private void btnStopSW_PosNeg_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(1, 0);
                //ClsGlobal.OCVTestContr.SWControl.ChannelVoltSwitchContr(1, 0);
            }
            catch (Exception)
            {
            }  
        }

        //壳体对负极测量
        private void btnSWCH_ShellNeg_Click(object sender, EventArgs e)
        {
            int resCH;
           
            if (int.TryParse(txtChannel.Text, out resCH) == true)
            {
                ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(3, resCH);//壳体对负极
                //ClsGlobal.OCVTestContr.SWControl.ChannelVoltSwitchContr(2, resCH);//壳体对负极
            }
            else
            {
                MessageBox.Show("通道输入出错");
            }
        }

        private void btnStopSW_ShellNeg_Click(object sender, EventArgs e)
        {
 
            try
            {
                ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(3, 0);
                // ClsGlobal.OCVTestContr.SWControl.ChannelVoltSwitchContr(2, 0);
            }
            catch (Exception)
            {
            }
        }

        private void btnSWCH_IRBT4_Click(object sender, EventArgs e)
        {
            int resCH;
            if (int.TryParse(txtChannel.Text, out resCH) == true)
            {
                //ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(2, resCH);
                ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(2, resCH);
            }
            else
            {
                MessageBox.Show("通道输入出错");
            }
        }

        private void btnStopSW_IRBT4_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGlobal.OCVTestContr.SWControl.ChannelVoltIRShellNegSwitchContr(2, 0); //内阻测试
                //ClsGlobal.OCVTestContr.SWControl.ChannelAcirSwitchContr(2, 0);
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
            try
            {
                int resCH;
                if (int.TryParse(txtChannel.Text, out resCH) == true)
                {
                    double Val;
                    ClsGlobal.OCVTestContr.HIOKI365X.ReadData(out Val);
                    txtACIR.Text = (Val * 1000).ToString("f2");
                }
                else
                {
                    MessageBox.Show("通道输入出错");
                }
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
               
                ClsGlobal.OCVTestContr.HIOKI365X.InitControl_IMM(2);
            }
            catch
            {
                MessageBox.Show("内阻仪初始化失败");
            }    
        }

        //刷新报警界面
        private void mRefreshTextAlarmA(string info)
        {
            txtInfoA.Text += System.DateTime.Now.ToString("HH:mm:ss") + ":  " + info + "\r\n";
            txtInfoA.Select(txtInfoA.TextLength, 0);
            txtInfoA.ScrollToCaret();
        }

    }
}
