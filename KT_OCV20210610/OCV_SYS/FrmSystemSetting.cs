using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmSystemSetting : Form
    {
        Thread TestTd_1;
        Thread TestTd_2;
        int loadFalg = 0;
        public FrmSystemSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "设置系统参数");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //OCV类型
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "OCVType", cmbOCVNum.Text);

            //DeviceNo序号
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DeviceNo", cmbDeviceNo.Text);

            //岗位条码
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DeviceCode  ", texResrce.Text);

            //TrayBattType托盘类型 
            if (rdoTray80CH.Checked == true)
            {
                ClsGlobal.TrayType = 80;
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "TrayBattType ", ClsGlobal.TrayType.ToString());
            }
            //TrayBattType托盘类型 
            if (rdoTray40CH.Checked == true)
            {
                ClsGlobal.TrayType = 40;
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "TrayBattType ", ClsGlobal.TrayType.ToString());
            }
            //ShellTestType 设备测量类型：不测壳体：0，测壳体：1
            if (rdoPNVolt.Checked == true)
            {
                ClsGlobal.mShellTestType = 0;
            }
            else if (rdoPNVoltandShell.Checked == true)
            {
                ClsGlobal.mShellTestType = 1;
            }
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "ShellTestType ", ClsGlobal.mShellTestType.ToString());



            #region 万用表

            if (rdoSerialCom.Checked == true)
            {
                ClsGlobal.DMTComMode = 1;
            }
            else if (rdoUSBCom.Checked == true)
            {
                ClsGlobal.DMTComMode = 2;
            }
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DMT_COM_Mode ", ClsGlobal.DMTComMode.ToString());
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DMT_Port ", cmbCOM_DMT.Text);
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DMT_USBAddr ", txtUSBAddr.Text.Trim());

            #endregion

            #region 条码

            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "TrayCodeLen ", (cmbTrayCodeLen.SelectedIndex + 1).ToString());
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "CellCodeLen ", (cmbCellCodeLen.SelectedIndex + 1).ToString());

            #endregion

            #region 数据库

            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_ZD", "IP", txtZDServer.Text.Trim());
            //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "Server_QT", "IP", txtQTServer.Text.Trim());

            #endregion

            #region 切换系统
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Switch_Port", cmbCOM_SW.Text);
            #endregion

            #region 内阻仪
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "RT_Port", cmbCOM_RT.Text);
            #endregion

            #region 温度
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Temp_Port", cmbCOM_Tmp.Text);
            //是否测温度
            if (chkNoTemp.Checked == true)
            {
                ClsGlobal.EN_TestTemp = 0;
            }
            else
            {
                ClsGlobal.EN_TestTemp = 1;
            }
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "EN_TestTemp", Convert.ToInt16(ClsGlobal.EN_TestTemp.ToString()).ToString());

            //延时参数
            if (chkDelayTest.Checked == false)
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "EnDelayTEMPTest", "0");
            }
            else
            {
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "EnDelayTEMPTest", "1");
            }
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "DelayTEMPTime", txtDelayTime.Text.Trim());

            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "TempTestCH", cmbTemp.Text.Trim());

            #endregion

            #region 位移传感器
            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Loca_Port", cmbCOM_Loca.Text);


            #endregion

            #region 电池品种
            if (ClsGlobal.OCVType == 2)
            {
                if (rdoFixCellStyle.Checked == true)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "GetCellStyleMethod ", "1");
                }
                else
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "GetCellStyleMethod ", "2");
                }


                if (rdoCellStyle1.Checked == true)
                {
                    ClsGlobal.CellStyle = 1;
                }
                else if (rdoCellStyle2.Checked == true)
                {
                    ClsGlobal.CellStyle = 1;
                }
                else if (rdoCellStyle3.Checked == true)
                {
                    ClsGlobal.CellStyle = 1;
                }
                else
                {
                    ClsGlobal.CellStyle = 1;
                }
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "CellStyle ", ClsGlobal.CellStyle.ToString());

                int iVal;
                if (int.TryParse(txtCodeStart.Text, out iVal) == false)
                {
                    MessageBox.Show("位数输入有误,请重新输入");
                    return;
                }
                else if (iVal < 1 || iVal > 27)
                {
                    MessageBox.Show("位数输入有误,请重新输入");
                    return;
                }
                ClsGlobal.CellStyleTrayCodeStart = iVal;
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "CellStyleCodeStart ", ClsGlobal.CellStyleTrayCodeStart.ToString());

                if (txtCellStyle1_Code.Text.Trim().Count() != 1 ||
                    txtCellStyle2_Code.Text.Trim().Count() != 1 ||
                    txtCellStyle3_Code.Text.Trim().Count() != 1)
                {
                    MessageBox.Show("有字符输入不等于1位,请重新输入");
                    return;
                }
                ClsGlobal.CellStyleCode1 = txtCellStyle1_Code.Text.Trim().ToUpper();
                ClsGlobal.CellStyleCode2 = txtCellStyle2_Code.Text.Trim().ToUpper();
                ClsGlobal.CellStyleCode3 = txtCellStyle3_Code.Text.Trim().ToUpper();

                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "CellStyleCode1 ", ClsGlobal.CellStyleCode1.ToString());
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "CellStyleCode2 ", ClsGlobal.CellStyleCode2.ToString());
                INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "CellStyleCode3 ", ClsGlobal.CellStyleCode3.ToString());
            }

            #endregion

            ClsGlobal.bCloseFrm = true;

            MessageBox.Show("请重启软件");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Environment.Exit(0);            //强制关闭
            this.Close();
            this.Owner.Close();
        }

        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            string Val1, Val2, Val3, Val4, Val5, Val6;

            //OCV号
            for (int i = 1; i < 4; i++)
            {
                cmbOCVNum.Items.Add(i);
            }
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "OCVType", null);
            cmbOCVNum.Text = Val1;

            Val5 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DeviceCode ", null);
            texResrce.Text = Val5;
            //设备序号
            cmbDeviceNo.Items.Clear();
            Val2 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DeviceNo", null);
            int iDeviceNo = Convert.ToInt16(Val2);
            int iOCVType = Convert.ToInt16(Val1);
            if (iOCVType == 1)
            {
                cmbDeviceNo.Items.Add(1);
                cmbDeviceNo.Items.Add(2);
                cmbDeviceNo.Items.Add(3);
                if (iDeviceNo == 1 || iDeviceNo == 2 || iDeviceNo == 3)
                {
                    cmbDeviceNo.Text = Val2;
                }
                else
                {
                    cmbDeviceNo.Text = "1";
                }
            }
            else if (iOCVType == 2)
            {
                cmbDeviceNo.Items.Add(4);
                cmbDeviceNo.Items.Add(5);
                cmbDeviceNo.Items.Add(6);
                if (iDeviceNo == 4 || iDeviceNo == 5 || iDeviceNo == 6)
                {
                    cmbDeviceNo.Text = Val2;
                }
                else
                {
                    cmbDeviceNo.Text = "4";
                }

            }
            else if (iOCVType == 3)
            {
                cmbDeviceNo.Items.Add(7);
                cmbDeviceNo.Items.Add(8);
                cmbDeviceNo.Items.Add(9);
                if (iDeviceNo == 7 || iDeviceNo == 8 || iDeviceNo == 9)
                {
                    cmbDeviceNo.Text = Val2;
                }
                else
                {
                    cmbDeviceNo.Text = "7";
                }
            }

            //正负极,壳体
            rdoPNVolt.Checked = true;
            rdoPNVolt.Enabled = true;
            rdoPNVoltandShell.Enabled = false;

            //ShellTestType测壳体
            Val4 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "ShellTestType", null);
            if (int.Parse(Val4) == 0)
            {
                rdoPNVolt.Checked = true;
            }
            else if (int.Parse(Val4) == 1)
            {
                rdoPNVoltandShell.Checked = true;
            }
            else
            {
                throw new Exception();
            }

            //DMT COM, RT COM, Temp COM , SW COM
            cmbCOM_DMT.Items.Add(" ");
            cmbCOM_RT.Items.Add("");
            cmbCOM_Tmp.Items.Add(" ");
            cmbCOM_SW.Items.Add(" ");
            cmbCOM_Loca.Items.Add(" ");
            for (int i = 1; i < 30; i++)
            {
                cmbCOM_DMT.Items.Add("COM" + i);
                cmbCOM_RT.Items.Add("COM" + i);
                cmbCOM_Tmp.Items.Add("COM" + i);
                cmbCOM_SW.Items.Add("COM" + i);
                cmbCOM_Loca.Items.Add("COM" + i);
            }

            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DMT_Port", null);
            cmbCOM_DMT.Text = Val1;
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Temp_Port", null);
            cmbCOM_Tmp.Text = Val1;
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Loca_Port", null);
            cmbCOM_Loca.Text = Val1;
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "RT_Port", null);
            cmbCOM_RT.Text = Val1;
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Switch_Port", null);
            cmbCOM_SW.Text = Val1;
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DMT_COM_Mode", null);

            if (int.Parse(Val1) == 1)
            {
                rdoSerialCom.Checked = true;
            }
            else if (int.Parse(Val1) == 2)
            {
                rdoUSBCom.Checked = true;
                cmbCOM_DMT.Enabled = false;
            }
            //USB地址
            txtUSBAddr.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DMT_USBAddr", null);

            //TrayBattType
            Val3 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TrayBattType", null);

            if (int.Parse(Val3) == 80)
            {
                rdoTray80CH.Checked = true;
            }

            else if (int.Parse(Val3) == 40)
            {
                rdoTray40CH.Checked = true;
            }
            else
            {
                throw new Exception("通道数出错");
            }

            //条码
            for (int i = 1; i < 30; i++)
            {
                cmbCellCodeLen.Items.Add(i);
                cmbTrayCodeLen.Items.Add(i);
            }

            int codeIndex;
            codeIndex = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TrayCodeLen", null));
            cmbTrayCodeLen.SelectedIndex = codeIndex - 1;
            codeIndex = int.Parse(INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "CellCodeLen", null));
            cmbCellCodeLen.SelectedIndex = codeIndex - 1;

            //是否测温度
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "EN_TestTemp", null);
            if (Val1 == "0")
            {
                chkNoTemp.Checked = true;
            }
            else if (Val1 == "1")
            {
                chkNoTemp.Checked = false;
            }

            //温度延迟测量
            Val1 = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "EnDelayTEMPTest", null);
            if (Val1 == "0")
            {
                chkDelayTest.Checked = false;
            }
            else if (Val1 == "1")
            {
                chkDelayTest.Checked = true;
            }

            txtDelayTime.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "DelayTEMPTime", null);

            cmbTemp.Text = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "TempTestCH", null);

            //电池品种设定
            if (ClsGlobal.OCVType == 2)
            {
                if (ClsGlobal.CellStyleGetMethod == 1)
                {
                    rdoFixCellStyle.Checked = true;
                    grpFixCellStyle.Enabled = true;
                    grpDynaCellStyle.Enabled = false;
                }
                else if (ClsGlobal.CellStyleGetMethod == 2)
                {
                    rdoDynaCellStyle.Checked = true;
                    grpFixCellStyle.Enabled = false;
                    grpDynaCellStyle.Enabled = true;
                }

                switch (ClsGlobal.CellStyle)
                {
                    case 1:
                        rdoCellStyle1.Checked = true;
                        break;
                    case 2:
                        rdoCellStyle2.Checked = true;
                        break;
                    case 3:
                        rdoCellStyle3.Checked = true;
                        break;
                    default:
                        rdoCellStyle1.Checked = true;
                        break;
                }

                txtCodeStart.Text = ClsGlobal.CellStyleTrayCodeStart.ToString();
                txtCellStyle1_Code.Text = ClsGlobal.CellStyleCode1.ToString();
                txtCellStyle2_Code.Text = ClsGlobal.CellStyleCode2.ToString();
                txtCellStyle3_Code.Text = ClsGlobal.CellStyleCode3.ToString();
            }
            loadFalg = 1;
        }

        private void rdoUSBCom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoUSBCom.Checked == true)
            {
                cmbCOM_DMT.Enabled = false;
            }
            else
            {
                cmbCOM_DMT.Enabled = true;
            }

        }

        private void rdoSerialCom_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void btnTest_DMT_Click(object sender, EventArgs e)
        {

        }

        private void TestZDConnection()
        {
            //string connectionString = "Data Source=" + txtZDServer.Text.Trim() + ";" +
            //        "Initial Catalog=" + ClsGlobal.Server_ZD_DB + ";" +
            //        "User ID=" + ClsGlobal.Server_ZD_id + ";" +
            //        "Password=" + ClsGlobal.Server_ZD_Pwd + ";" +
            //        "Integrated Security=no";

            //SqlConnection connection;
            //connection = new SqlConnection(connectionString);
            //try
            //{
            //    connection.Open();
            //    Thread.Sleep(300);
            //    if (connection.State == ConnectionState.Closed)
            //    {
            //        throw new Exception();
            //    }
            //    else
            //    {
            //        MessageBox.Show("连接成功");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("连接失败");
            //}
            //finally
            //{
            //    connection.Close();
            //}
        }

        private void TestQTConnection()
        {
            //string connectionString = "Data Source=" + txtQTServer.Text.Trim() + ";" +
            //                                "Initial Catalog=" + ClsGlobal.Server_QT_DB + ";" +
            //                                "User ID=" + ClsGlobal.Server_QT_id + ";" +
            //                                "Password=" + ClsGlobal.Server_QT_Pwd + ";" +
            //                                "Integrated Security=no";
            //SqlConnection connection;
            //connection = new SqlConnection(connectionString);
            //try
            //{
            //    connection.Open();
            //    Thread.Sleep(300);
            //    if (connection.State == ConnectionState.Closed)
            //    {
            //        throw new Exception();
            //    }
            //    else
            //    {
            //        MessageBox.Show("连接成功");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("连接失败");
            //}
            //finally
            //{
            //    connection.Close();
            //}

        }


        private void cmbOCVNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOCVNum.Text == "1")
            {
                ClsGlobal.OCVType = 1;
                // chkNoTemp.Checked = true;

                grpCellStyle.Enabled = false;
                rdoFixCellStyle.Checked = true;
                rdoDynaCellStyle.Checked = false;
                rdoCellStyle1.Checked = false;
                rdoCellStyle1.Checked = false;
                rdoCellStyle1.Checked = false;
                txtCodeStart.Text = "";
                txtCellStyle1_Code.Text = "";
                txtCellStyle2_Code.Text = "";
                txtCellStyle3_Code.Text = "";

                //重新设置序号
                cmbDeviceNo.Items.Clear();
                cmbDeviceNo.Items.Add(1);
                cmbDeviceNo.Items.Add(2);
                cmbDeviceNo.Items.Add(3);
                cmbDeviceNo.Text = "1";

            }
            else if (cmbOCVNum.Text == "2")
            {
                ClsGlobal.OCVType = 2;
                // chkNoTemp.Checked = true;

                grpCellStyle.Enabled = true;
                if (ClsGlobal.CellStyleGetMethod == 1)
                {
                    rdoFixCellStyle.Checked = true;
                }
                else if (ClsGlobal.CellStyleGetMethod == 2)
                {
                    rdoDynaCellStyle.Checked = true;
                }

                switch (ClsGlobal.CellStyle)
                {
                    case 1:
                        rdoCellStyle1.Checked = true;
                        break;
                    case 2:
                        rdoCellStyle2.Checked = true;
                        break;
                    case 3:
                        rdoCellStyle3.Checked = true;
                        break;
                    default:
                        rdoCellStyle1.Checked = true;
                        break;
                }

                txtCodeStart.Text = ClsGlobal.CellStyleTrayCodeStart.ToString();
                txtCellStyle1_Code.Text = ClsGlobal.CellStyleCode1.ToString();
                txtCellStyle2_Code.Text = ClsGlobal.CellStyleCode2.ToString();
                txtCellStyle3_Code.Text = ClsGlobal.CellStyleCode3.ToString();

                //重新设置序号
                cmbDeviceNo.Items.Clear();
                cmbDeviceNo.Items.Add(4);
                cmbDeviceNo.Items.Add(5);
                cmbDeviceNo.Items.Add(6);
                cmbDeviceNo.Text = "4";
            }
            else if (cmbOCVNum.Text == "3")
            {
                ClsGlobal.OCVType = 3;
                //chkNoTemp.Checked = false;

                grpCellStyle.Enabled = false;
                rdoFixCellStyle.Checked = true;
                rdoDynaCellStyle.Checked = false;
                rdoCellStyle1.Checked = false;
                rdoCellStyle1.Checked = false;
                rdoCellStyle1.Checked = false;
                txtCodeStart.Text = "";
                txtCellStyle1_Code.Text = "";
                txtCellStyle2_Code.Text = "";
                txtCellStyle3_Code.Text = "";

                //重新设置序号
                cmbDeviceNo.Items.Clear();
                cmbDeviceNo.Items.Add(7);
                cmbDeviceNo.Items.Add(8);
                cmbDeviceNo.Items.Add(9);
                cmbDeviceNo.Text = "7";
            }
        }

        private void rdoFixCellStyle_CheckedChanged(object sender, EventArgs e)
        {
            grpFixCellStyle.Enabled = true;
            grpDynaCellStyle.Enabled = false;
        }

        private void rdoDynaCellStyle_CheckedChanged(object sender, EventArgs e)
        {
            grpFixCellStyle.Enabled = false;
            grpDynaCellStyle.Enabled = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
              

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

    }
}
