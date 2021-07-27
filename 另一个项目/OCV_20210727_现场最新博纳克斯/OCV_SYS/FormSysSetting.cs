using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV
{
    public partial class FormSysSetting : Form
    {
        public FormSysSetting()
        {
            InitializeComponent();
        }



        FrmSystemSetting frmSys;
        private void FormSysSetting_Load(object sender, EventArgs e)
        {
             frmSys = new FrmSystemSetting();
            frmSys.FormBorderStyle = FormBorderStyle.None;
            frmSys.TopLevel = false;
            frmSys.Dock = DockStyle.Fill;
            palSourceSys.Controls.Add(frmSys);
            frmSys.Show();


            FormUserManager frmUser = new FormUserManager();
            frmUser.FormBorderStyle = FormBorderStyle.None;
            frmUser.TopLevel = false;
            frmUser.Dock = DockStyle.Fill;
            panel2.Controls.Add(frmUser);
            frmUser.Show();
            try
            {
                SysSetting sysSetting = ClsSysSetting.SysSetting;
                txtBatCodeSavePath.Text = sysSetting.BatCodeSavePath;
                txtDeviceCode.Text = sysSetting.DeviceCode;
                txtEndDataPath.Text = sysSetting.EndDataSavePath;
                txtScanCOM.Text = sysSetting.ScanCodeCOM;
                txtNGCheckCount.Text = sysSetting.NGCheckCount.ToString();
                chkCheckFiFO.Checked = sysSetting.CheckFIFO;
                chkPassWordLogin.Checked = sysSetting.LoginWithPassWord;
                chkPP.Checked = sysSetting.PassPropcentWarning;
                txtPPV.Text = sysSetting.PassPropcent.ToString("0.00");
                chkNgCheck.Checked = sysSetting.IsNGCheck;
            }catch(Exception ex)
            {
                MessageBox.Show($"加载系统设置失败:{ex.Message}");
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SysSetting sysSetting = new SysSetting();
                sysSetting.DeviceCode = txtDeviceCode.Text;
                sysSetting.BatCodeSavePath = txtBatCodeSavePath.Text;
                sysSetting.EndDataSavePath = txtEndDataPath.Text;
                sysSetting.ScanCodeCOM = txtScanCOM.Text;
                sysSetting.NGCheckCount = int.Parse(txtNGCheckCount.Text);
                sysSetting.CheckFIFO = chkCheckFiFO.Checked;
                sysSetting.LoginWithPassWord = chkPassWordLogin.Checked;
                sysSetting.PassPropcentWarning = chkPP.Checked;
                sysSetting.PassPropcent = double.Parse( txtPPV.Text);
                sysSetting.IsNGCheck = chkNgCheck.Checked;
                ClsSysSetting.SysSetting = sysSetting;
                MessageBox.Show("保存成功!");
            }catch(Exception ex)
            {
                MessageBox.Show($"保存失败:{ex.Message}");
            }
        }

        private void txtNGCheckCount_TextChanged(object sender, EventArgs e)
        {
            try {
                int.Parse(txtNGCheckCount.Text);
            } catch(Exception ex)
            {
                MessageBox.Show("请输入数字!");
                txtNGCheckCount.Text = "0";
            }
        }
        int closeSataus = 0;
        private void FormSysSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmSys.RequeteReset)
            {
                if (closeSataus == 0)
                {
                    closeSataus++;
                    MessageBox.Show("软件将重启", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClsGlobal.bCloseFrm = true;
                    this.Close();
                    this.Owner.Close();
                    ClsGlobal.Restart = true;
                }
            }
        }

        private void btnProbeRecover_Click(object sender, EventArgs e)
        {
            FormProbeRecover formProbeRecover = new FormProbeRecover();
            formProbeRecover.ShowDialog();
        }

        private void txtPPV_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                double.Parse(txtPPV.Text);
                e.Cancel = false;
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字.");
            }
        }
    }
}
