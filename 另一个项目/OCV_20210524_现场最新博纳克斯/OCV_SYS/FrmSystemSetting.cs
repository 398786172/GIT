using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace OCV
{
    public partial class FrmSystemSetting : Form
    {

        public bool RequeteReset =false;

        public FrmSystemSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "OCVType", cmbOCVType.Text);  //改由工程管理配置 20200828 由ajone 屏蔽

            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DeviceNo", cmbDeviceNo.Text);

            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "TrayType", ClsGlobal.TrayType.ToString());
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "ShellTestType", ClsGlobal.mShellTestType.ToString());

            //万用表连接模式            
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DMT_USBAddr", txtUSBAddr.Text.Trim());
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Port", cmbmultimeterCOM_RT.Text.Trim());
            if (rdoVoltSpeedSlow.Checked)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Speed", "1");
            }
            if (rdoVoltSpeedMid.Checked)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Speed", "2");
            }
            if (rdoUSBConnection.Checked)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Multimeter_Connection_Type", "1");
            }
            if (rdoCOMConnection.Checked)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Multimeter_Connection_Type", "2");
            }

            //数据库连接
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_ZD", "IP", txtZDServer.Text.Trim());
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_QT", "IP", txtServer.Text.Trim());
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_QT", "DB", txtDBName.Text.Trim());
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_QT", "IP", txtDBUser.Text.Trim());
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_QT", "Pwd", txtDBPwd.Text.Trim());

            //TrayType托盘类型 
            if (rdoTray256CH.Checked == true)
            {
                ClsGlobal.TrayType = 256;
            }



            //内阻仪串口
            //cmbCOM_RT.Items
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "RT_Port", cmbCOM_RT.Text);

            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "SW_Port", cmbCOM_SW.Text);

            if (rdoIRSpeedLow.Checked == true)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "RT_Speed", "1");
            }
            else
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "RT_Speed", "2");
            }

            MessageBox.Show("保存成功!");
            RequeteReset = true;
            //ClsGlobal.bCloseFrm = true;
        }

        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            string Val1, Val2, Val3;

            #region 改由工程管理配置 20200828 由ajone 屏蔽
            //OCV类型           
            //for (int i = 1; i < 3; i++)
            //{
            //    cmbOCVType.Items.Add(i);
            //}
            //Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "OCVType", null);
            //cmbOCVType.Text = Val1;
            //if (Val1.Trim() == "1" )
            //{
            //    rdoACIROCV.Checked = true;
            //    rdoOCV.Checked = false;
            //    rdoOCV.Enabled = false;
            //}
            //else if (Val1.Trim() == "2")
            //{
            //    rdoOCV.Checked = true;
            //    rdoACIROCV.Checked = false;
            //    rdoACIROCV.Enabled = false;
            //}
            #endregion



            //设备序号
            for (int i = 1; i < 2; i++)
            {
                cmbDeviceNo.Items.Add(i);
            }
            Val2 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DeviceNo", null);
            cmbDeviceNo.Text = Val2;

            ////ShellTestType测壳体
            //Val4 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "ShellTestType", null);
            //if (int.Parse(Val4) == 0)
            //{
            //    rdoOCV.Checked = true;
            //}
            //else if (int.Parse(Val4) == 1)
            //{
            //    rdoACIROCV.Checked = true;
            //}
            //else
            //{
            //    throw new Exception();
            //}

            //内阻仪
            cmbCOM_RT.Items.Add("");
            for (int i = 1; i < 30; i++)
            {
                cmbCOM_RT.Items.Add("COM" + i);
            }
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "RT_Port", null);
            cmbCOM_RT.Text = Val1;

            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "RT_Speed", null);
            if (int.Parse(Val1) == 1)
            {
                rdoIRSpeedLow.Checked = true;
            }
            else
            {
                rdoIRSpeedMid.Checked = true;
            }
            //万用表
            cmbmultimeterCOM_RT.Items.Add("");
            for (int i = 1; i < 30; i++)
            {
                cmbmultimeterCOM_RT.Items.Add("COM" + i);
            }
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Port", null);
            cmbmultimeterCOM_RT.Text = Val1;

            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "MultimeterCOM_RT_Speed", null);
            if (int.Parse(Val1) == 1)
            {
                rdoVoltSpeedSlow.Checked = true;
            }
            else
            {
                rdoVoltSpeedMid.Checked = true;
            }
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Multimeter_Connection_Type", null);
            if (int.Parse(Val1) == 1)
            {
                rdoUSBConnection.Checked = true;
            }
            else
            {
                rdoCOMConnection.Checked = true;
            }
            //切换系统
            cmbCOM_SW.Items.Add("");
            for (int i = 1; i < 30; i++)
            {
                cmbCOM_SW.Items.Add("COM" + i);
            }
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "SW_Port", null);
            cmbCOM_SW.Text = Val1;

            //USB地址
            txtUSBAddr.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DMT_USBAddr", null);

            //COM地址
        
            //TrayType
            Val3 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TrayType", null);
                        
            if (int.Parse(Val3) == 256)
            {
                rdoTray256CH.Checked = true;
            }
            else
            {
                throw new Exception("通道数出错");
            }

            //电池类型
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "BatteryType", null);
            if (Val1 == "18650")
            {
                //rdo18650.Checked = true;
            }
            else if (Val1 == "21700")
            {
                //rdo21700.Checked = true;
            }
            else
            {
                throw new Exception("通道数出错");
            }
            


        }







        #region 改由工程管理配置 20200828 由ajone 屏蔽
        //private void cmbOCVNum_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbOCVType.Text == "1")
        //    {
        //        rdoACIROCV.Checked = true;
        //        rdoACIROCV.Enabled = true;
        //        rdoOCV.Enabled = false;                
        //    }
        //    else if (cmbOCVType.Text == "2")
        //    {
        //        rdoOCV.Checked = true;
        //        rdoOCV.Enabled = true;
        //        rdoACIROCV.Enabled = false;
        //    }
        //}
        #endregion
        private void rdoPNVolt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoPNVoltandShell_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoTray70CH_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmSystemSetting_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void FrmSystemSetting_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void rdoUSBConnection_CheckedChanged(object sender, EventArgs e)
        {
            cmbmultimeterCOM_RT.Enabled = false;
            rdoVoltSpeedMid.Enabled = false;
            rdoVoltSpeedSlow.Enabled = false;
        }

        private void rdoCOMConnection_CheckedChanged(object sender, EventArgs e)
        {
            txtUSBAddr.Enabled = false;
        }
    }
}
