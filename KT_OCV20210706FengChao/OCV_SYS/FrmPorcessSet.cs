using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using OCV.OCVLogs;

namespace OCV
{
    public partial class FormPorcessSet : Form
    {
        public FormPorcessSet()
        {
            InitializeComponent();
        }
        //bool flag;
        bool Working = false;
        private void FormPorcessSet_Load(object sender, EventArgs e)
        {
            //创建导出数据文件夹
            Working = false;
            if (!Directory.Exists(ClsGlobal.mSettingProjectPath))
            {
                Directory.CreateDirectory(ClsGlobal.mSettingProjectPath);
            }
            //创建OCV型号数据文件夹      
            for (int i = 3; i < 5; i++)
            {
                string _sOCVProjectPath = "OCV" + i;
                _sOCVProjectPath = ClsGlobal.mSettingProjectPath + "\\" + _sOCVProjectPath;
                if (!Directory.Exists(_sOCVProjectPath))
                {
                    Directory.CreateDirectory(_sOCVProjectPath);
                }
            }
            cmbOCVType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBattType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConfigName.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBox6.Enabled = false;
            btnDelBattType.Enabled = false;
            btnDelConfig.Enabled = false;
            txt_Info.ReadOnly = true;

            btnSave.Enabled = false;

            cmbOCVType.Text = "OCV" + ClsGlobal.OCVType;

            int SetPara = 1;
            if (SetPara == 3)
            {
                chkEnLocalProcess.Checked = false;
            }
            else
            {
                chkEnLocalProcess.Checked = true;
                if (cmbOCVType.Text == "OCV1")
                {
                    groupBox8.Enabled = false;
                    groupBox3.Enabled = false;
                }
                groupBox1.Enabled = true;
                groupBox13.Enabled = true;
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //输入错误判断
            if (ClsGlobal.IsAWorking == true )
            {
                Working = true;
            }

            if (Working == true)
            {
                MessageBox.Show("有工位工作中不能修改参数，请停止后再操作！");
                return;
            }

            frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "保存工艺");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (cmbBattType.Text.Trim() == "")
            {
                MessageBox.Show("请选择或输入电池型号");
                return;
            }
            else if (cmbBattType.Text.Trim() == "Project")
            {
                MessageBox.Show("电池型号错误：不能命名为Project");
                return;
            }
            if (cmbConfigName.Text.Trim() == "")
            {
                MessageBox.Show("请选择或输入工艺名");
                return;
            }

            //写入配置
            string strOCVType = cmbOCVType.Text.Trim();
            string strBattType = cmbBattType.Text.Trim();
            string strConfigName = cmbConfigName.Text.Trim();

            string BattTypePath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\" + strBattType + ".ini";

            double Val1;
            double Val2;
            double Val3;
            //参数写入------------------------------------------------ -  
            //电压上下限
            if (double.TryParse(txtDownLMT_V.Text, out Val1) == false)
            {
                MessageBox.Show("电压下限输入出错");
                return;
            }
            else if (double.TryParse(txtUpLMT_V.Text, out Val2) == false)
            {
                MessageBox.Show("电压上限输入出错");
                return;
            }

            if (Val2 > Val1)
            {
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLmt_V", Val1.ToString());
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLmt_V", Val2.ToString());
            }
            else
            {
                MessageBox.Show("错误;设置电压值上限小于下限");
                return;
            }
            if (cmbOCVType.Text == "OCV2"|| cmbOCVType.Text == "OCV3")
            {
                //壳压上下限
                if (double.TryParse(txtDownLMT_SV.Text, out Val1) == false)
                {
                    MessageBox.Show("壳压下限输入出错");
                    return;
                }
                else if (double.TryParse(txtUpLMT_SV.Text, out Val2) == false)
                {
                    MessageBox.Show("壳压上限输入出错");
                    return;
                }

                if (Val2 > Val1)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLmt_SV", Val1.ToString());
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLmt_SV", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置壳压值上限小于下限");
                    return;
                }

                //ACIR上下限 
                if (double.TryParse(txtDownLMT_ACIR.Text, out Val1) == false)
                {
                    MessageBox.Show("ACIR下限输入出错");
                    return;
                }
                else if (double.TryParse(txtUpLMT_ACIR.Text, out Val2) == false)
                {
                    MessageBox.Show("ACIR上限输入出错");
                    return;
                }
                if (Val2 > Val1)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLmt_ACIR", Val1.ToString());
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLmt_ACIR", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置ACIR上限小于下限");
                    return;
                }

                //ACIR 极差上下限 
                if (double.TryParse(txtDownLMT_ACIRrange.Text, out Val1) == false)
                {
                    MessageBox.Show("ACIR极差下限输入出错");
                    return;
                }
                else if (double.TryParse(txtUpLMT_ACIRrange.Text, out Val2) == false)
                {
                    MessageBox.Show("ACIR极差上限输入出错");
                    return;
                }
                if (Val2 > Val1)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLMT_ACIRrange", Val1.ToString());
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLMT_ACIRrange", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置ACIR极差值上限小于下限");
                    return;
                }

                if (rdb_ACIR_Median.Checked == true)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "ACIR_MedianOrMin", "Y");
                }
                else if (rdb_ACIR_Min.Checked == true)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "ACIR_MedianOrMin", "N");
                }
                else
                {
                    MessageBox.Show("错误;未选择ACIR极差计算参数");
                    return;
                }
            }         
            if (cmbOCVType.Text == "OCV3")
            {
                //压降参数
                if (double.TryParse(txtMaxVoltDrop.Text, out Val1) == false)
                {
                    MessageBox.Show("最大压降输入出错");
                    return;
                }
                else if (double.TryParse(txtVoltDrop.Text, out Val3) == false)
                {
                    MessageBox.Show("压降特定值输入出错");
                    return;
                }
                else if (double.TryParse(txtMinVoltDrop.Text, out Val2) == false)
                {
                    MessageBox.Show("最小压降输入出错");
                    return;
                }

                if (Val2 < Val1 && Val2< Val3 && Val3 < Val1)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "MaxVoltDrop", Val1.ToString());
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "VoltDrop", Val3.ToString());
                    
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "MinVoltDrop", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置压降值上限小于下限或特定值不在上下限范围内");
                    return;
                }
                if (chb_ENVoltDrop.Checked == true)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "ENVoltDrop", "Y");
                }
                else
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "ENVoltDrop", "N");
                }
                //if (double.TryParse(txt_k.Text, out Val1) == false)
                //{
                //    MessageBox.Show("K值输入出错");
                //    return;
                //}
                //INIAPI.INIWriteValue(BattTypePath, cmbConfigName.Text.Trim(), "K", Val1.ToString());
                //压降极差上下限 
                if (double.TryParse(txtDownLMT_DropRange.Text, out Val1) == false)
                {
                    MessageBox.Show("压降极差下限输入出错");
                    return;
                }
                else if (double.TryParse(txtUpLMT_DropRange.Text, out Val2) == false)
                {
                    MessageBox.Show("压降极差上限输入出错");
                    return;
                }
                if (Val2 > Val1)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLMT_DropRange", Val1.ToString());
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLMT_DropRange", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置压降极差值上限小于下限");
                    return;
                }
                if (rdb_Drop_Median.Checked == true)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "Drop_MedianOrMin", "Y");
                }
                else if (rdb_Drop_Min.Checked == true)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "Drop_MedianOrMin", "N");
                }
                else
                {
                    MessageBox.Show("错误;未选择压降极差值计算参数");
                    return;
                }

                if (ckB_IS_Enable_DropRange.Checked == true)
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "IS_Enable_DropRange", "Y");
                }
                else
                {
                    INIAPI.INIWriteValue(BattTypePath, strConfigName, "IS_Enable_DropRange", "N");
                }

            }
            else
            {
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "MaxVoltDrop", "99999");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "MinVoltDrop", "-100");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLMT_DropRange", "99999");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLMT_DropRange", "-100");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "Drop_MedianOrMin", "N");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "IS_Enable_DropRange", "N");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "ENVoltDrop", "N");
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "K", "-100");
            }

            ////时间参数
            //if (double.TryParse(txtUpLMT_time.Text, out Val1) == false)
            //{
            //    MessageBox.Show("最大时间输入出错");
            //    return;
            //}
            //else if (double.TryParse(txtDownLMT_time.Text, out Val2) == false)
            //{
            //    MessageBox.Show("最小时间输入出错");
            //    return;
            //}

            //if (Val2 < Val1)
            //{
            //    INIAPI.INIWriteValue(BattTypePath, strConfigName, "UpLMT_time", txtUpLMT_time.Text);
            //    INIAPI.INIWriteValue(BattTypePath, strConfigName, "DownLmt_time", txtDownLMT_time.Text);
            //}
            //else
            //{
            //    MessageBox.Show("错误;设置时间上限小于下限");
            //    return;
            //}
            //2019 0227 新增温度工艺
            //温度基准值
            if (double.TryParse(txtTempBase.Text, out Val1) == false)
            {
                MessageBox.Show("温度基准值输入出错");
                return;
            }
            else if (double.TryParse(txtTempPara.Text, out Val2) == false)
            {
                MessageBox.Show("温度系数输入出错");
                return;
            }
            INIAPI.INIWriteValue(BattTypePath, strConfigName, "TempBase", Val1.ToString());
            INIAPI.INIWriteValue(BattTypePath, strConfigName, "TempParaModify", Val2.ToString());


            if (ckB_IS_Enable_ACIRRange.Checked == true)
            {
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "IS_Enable_ACIRRange", "Y");
            }
            else
            {
                INIAPI.INIWriteValue(BattTypePath, strConfigName, "IS_Enable_ACIRRange", "N");
            }
            //刷新列表
            //-----------------------------------------------------------
            string[] arrFiles;
            cmbBattType.Items.Clear();
            arrFiles = Directory.GetFiles(ClsGlobal.mSettingProjectPath + "\\" + strOCVType, "*.ini");
            List<String> list = new List<string>();
            for (int i = 0; i < arrFiles.Length; i++)
            {

                string[] arr = Path.GetFileName(arrFiles[i]).Split('.');
                list.Add(Path.GetFileName(arr[0]));
                if (list[i] != "Project")
                {
                    cmbBattType.Items.Add(list[i]);
                }
            }

            cmbBattType.Text = strBattType;

            string[] arrSection;
            cmbConfigName.Items.Clear();
            arrSection = INIAPI.INIGetAllSectionNames(BattTypePath);
            for (int j = 0; j < arrSection.Length; j++)
            {
                cmbConfigName.Items.Add(arrSection[j]);
            }
            string mCfgName = INIAPI.INIGetStringValue(BattTypePath, strBattType, "CfgName", null);
            if (mCfgName == null || mCfgName == "")
            {
                cmbConfigName.Text = arrSection[0];
            }
            else
            {
                bool exists = (arrSection).Contains(mCfgName);
                if (exists)
                // 存在
                {
                    cmbConfigName.Text = mCfgName;
                }
                else
                {
                    cmbConfigName.Text = arrSection[0];
                }
            }

            string mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + cmbOCVType.Text.Trim() + "\\Project.ini";
            INIAPI.INIWriteValue(mProjectPath, cmbBattType.Text.Trim(), "BattType", cmbBattType.Text.Trim());
            INIAPI.INIWriteValue(mProjectPath, cmbBattType.Text.Trim(), "CfgName", cmbConfigName.Text.Trim());
            INIAPI.INIWriteValue(mProjectPath, "默认型号", "BattType ", cmbBattType.Text.Trim());
            txt_Info.Text = "默认工艺" + "\r\n";
            txt_Info.Text += "OCV号 :" + cmbOCVType.Text.Trim() + "\r\n";
            txt_Info.Text += "电池型号 :" + cmbBattType.Text.Trim() + "\r\n";
            txt_Info.Text += "工艺配置 :" + cmbConfigName.Text.Trim() + "\r\n";

            MessageBox.Show("配置保存成功！");
        }

        private void btn_DeleteBattType_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true )
            {
                Working = true;
            }

            if (Working == true)
            {
                MessageBox.Show("有工位工作中不能修改参数，请停止后再操作！");
                return;
            }
            frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "删除工艺" + cmbBattType.Text.Trim());
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            //删除BattType文件和相应的工艺配置
            string strOCVType = cmbOCVType.Text.Trim();
            string strBattType = cmbBattType.Text.Trim();

            string BattTypePath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\" + strBattType + ".ini";
            string ProjectPath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\Project.ini";
            if (File.Exists(BattTypePath))//判断文件是不是存在
            {
                //删除Project中的相应项
                INIAPI.INIDeleteSection(ProjectPath, strBattType);

                //删除BattType文件
                File.Delete(BattTypePath);
                //int n = this.cmbBattType.SelectedIndex;
                this.cmbBattType.Items.Remove(strBattType);
                if (cmbBattType.Items != null)
                {
                    cmbBattType.Text = cmbBattType.Items[0].ToString();
                }

                cmbConfigName.Text = "";
                MessageBox.Show("删除工程成功！");
            }
        }

        private void cmb_BattType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空configDetail
            txtUpLMT_V.Text = "";
            txtDownLMT_V.Text = "";

            txtUpLMT_SV.Text = "";
            txtDownLMT_SV.Text = "";

            txtUpLMT_ACIR.Text = "";
            txtDownLMT_ACIR.Text = "";

            txtUpLMT_ACIRrange.Text = "";
            txtDownLMT_ACIRrange.Text = "";

            txtUpLMT_time.Text = "";
            txtDownLMT_time.Text = "";

            txtMaxVoltDrop.Text = "";
            txtVoltDrop.Text = "";
            txtMinVoltDrop.Text = "";

            txtUpLMT_DropRange.Text = "";
            txtDownLMT_DropRange.Text = "";

            txt_k.Text = "";
            lblNote.Text = "";
            //txtTempBase.Text = "";
            //txtTempPara.Text = "";
            ckB_IS_Enable_ACIRRange.Checked = false;
            ckB_IS_Enable_DropRange.Checked = false;
            chb_ENVoltDrop.Checked = false;

            string strOCVType = cmbOCVType.Text.Trim();
            string strBattType = cmbBattType.Text.Trim();
            string strConfigName = cmbConfigName.Text.Trim();
            string mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\Project.ini";
            string BattTypePath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\" + strBattType + ".ini";

            //刷新工艺配置
            bool ExistDefaultCfgDetail = false;
            string[] arrSection;
            cmbConfigName.Items.Clear();
            arrSection = INIAPI.INIGetAllSectionNames(BattTypePath);
            string DefaultCfgName = INIAPI.INIGetStringValue(mProjectPath, strBattType, "CfgName", null);
            for (int j = 0; j < arrSection.Length; j++)
            {
                cmbConfigName.Items.Add(arrSection[j]);
                if (DefaultCfgName == arrSection[j])
                {
                    ExistDefaultCfgDetail = true;
                }
            }

            if (ExistDefaultCfgDetail == true)
            {
                cmbConfigName.Text = DefaultCfgName;
            }
            else
            {
                cmbConfigName.Text = "001";
            }

            //刷新默认工艺
            txt_Info.Text = "默认工艺" + "\r\n";
            txt_Info.Text += "OCV号 :" + strOCVType + "\r\n";
            txt_Info.Text += "电池型号 :" + strBattType + "\r\n";
            //txt_Info.Text += "工艺配置 :" + DefaultCfgName + "\r\n";

            if (ExistDefaultCfgDetail == false)
            {
                lblNote.Text = "此型号的电池未设置要使用的工艺，请设置";
                txt_Info.Text += "工艺配置 :" + "" + "\r\n";
            }
            else if (DefaultCfgName == null)
            {
                lblNote.Text = "此型号的电池未设置要使用的工艺，请设置";
                txt_Info.Text += "工艺配置 :" + "" + "\r\n";
            }
            else
            {
                txt_Info.Text += "工艺配置 :" + DefaultCfgName + "\r\n";
            }
        }

        private void cmb_Config_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strOCVType = cmbOCVType.Text.Trim();
            string strBattType = cmbBattType.Text.Trim();
            string strConfigName = cmbConfigName.Text.Trim();

            string BattTypePath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\" + strBattType + ".ini";

            txtUpLMT_V.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "UpLmt_V", null);
            txtDownLMT_V.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "DownLmt_V", null);

            if (cmbOCVType.Text == "OCV2" || cmbOCVType.Text == "OCV3")
            {
                txtUpLMT_SV.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "UpLmt_SV", null);
                txtDownLMT_SV.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "DownLmt_SV", null);

                txtUpLMT_ACIR.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "UpLmt_ACIR", null);
                txtDownLMT_ACIR.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "DownLmt_ACIR", null);

                txtUpLMT_ACIRrange.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "UpLMT_ACIRrange", null);
                txtDownLMT_ACIRrange.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "DownLMT_ACIRrange", null);

            }

            string ACIR_MedianOrMin= INIAPI.INIGetStringValue(BattTypePath, strConfigName, "ACIR_MedianOrMin", null);
            if (ACIR_MedianOrMin=="Y")
            {
                rdb_ACIR_Median.Checked = true;
            }
            else
            {
                rdb_ACIR_Min.Checked = true;
            }

            string IS_Enable_ACIRRange = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "IS_Enable_ACIRRange", null);
            if (IS_Enable_ACIRRange == "Y")
            {
                ckB_IS_Enable_ACIRRange.Checked = true;
            }
            else
            {
                ckB_IS_Enable_ACIRRange.Checked = false;
            }

            //txtUpLMT_time.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "UpLMT_time", null);
            //txtDownLMT_time.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "DownLmt_time", null);
            txtTempBase.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "TempBase", null);
            txtTempPara.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "TempParaModify", null);

            if (cmbOCVType.Text == "OCV3")
            {
                txtMaxVoltDrop.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "MaxVoltDrop", null);
                txtVoltDrop.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "VoltDrop", null);
                txtMinVoltDrop.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "MinVoltDrop", null);
                //txt_k.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "K", null);
                string IS_ENVoltDrop = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "ENVoltDrop", null);
                if (IS_ENVoltDrop == "Y")
                {
                    chb_ENVoltDrop.Checked = true;
                }
                else
                {
                    chb_ENVoltDrop.Checked = false;
                }
               
                txtUpLMT_DropRange.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "UpLMT_DropRange", null);
                txtDownLMT_DropRange.Text = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "DownLMT_DropRange", null);

                string IS_Enable_DropRange = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "IS_Enable_DropRange", null);
                if (IS_Enable_DropRange == "Y")
                {
                    ckB_IS_Enable_DropRange.Checked = true;
                }
                else
                {
                    ckB_IS_Enable_DropRange.Checked = false;
                }

                string Drop_MedianOrMin = INIAPI.INIGetStringValue(BattTypePath, strConfigName, "Drop_MedianOrMin", null);
                if (Drop_MedianOrMin == "Y")
                {
                    rdb_Drop_Median.Checked = true;
                }
                else
                {
                    rdb_Drop_Min.Checked = true;
                }  
            }
            else
            {
                txtMaxVoltDrop.Text = "";
                txtMinVoltDrop.Text = "";
                txtUpLMT_DropRange.Text = "";
                txtDownLMT_DropRange.Text = "";
                txtVoltDrop.Text = "";
                //txt_k.Text = "";
            }

        }

        private void SetDefaultProc_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true )
            {
                Working = true;
            }

            if (Working == true)
            {
                MessageBox.Show("有工位工作中不能修改参数，请停止后再操作！");
                return;
            }

            if (cmbConfigName.Text == "")
            {
                MessageBox.Show("工艺不能为空！");
                return;
            }

            frmUserPwd frmPwd = new frmUserPwd(PwdType.USER, "设置默认工艺");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + cmbOCVType.Text.Trim() + "\\Project.ini";
            INIAPI.INIWriteValue(mProjectPath, cmbBattType.Text.Trim(), "BattType", cmbBattType.Text.Trim());
            INIAPI.INIWriteValue(mProjectPath, cmbBattType.Text.Trim(), "CfgName", cmbConfigName.Text.Trim());
            INIAPI.INIWriteValue(mProjectPath, "默认型号", "BattType ", cmbBattType.Text.Trim());
            txt_Info.Text = "默认工艺" + "\r\n";
            txt_Info.Text += "OCV号 :" + cmbOCVType.Text.Trim() + "\r\n";
            txt_Info.Text += "电池型号 :" + cmbBattType.Text.Trim() + "\r\n";


            txt_Info.Text += "工艺配置 :" + cmbConfigName.Text.Trim() + "\r\n";
            MessageBox.Show("设置默认成功！");
            //MessageBox.Show("设置默认工艺成功,请点击【保存】按钮保存参数！");          
        }

        private void DelConfig_Click(object sender, EventArgs e)
        {
            if (ClsGlobal.IsAWorking == true )
            {
                Working = true;
            }

            if (Working == true)
            {
                MessageBox.Show("有工位工作中不能修改参数，请停止后再操作！");
                return;
            }
            frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "删除工艺");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string strOCVType = cmbOCVType.Text.Trim();
            string strBattType = cmbBattType.Text.Trim();
            string strConfigName = cmbConfigName.Text.Trim();
            string BattTypePath = ClsGlobal.mSettingProjectPath + "\\" + strOCVType + "\\" + strBattType + ".ini";
            string ProjectPath = ClsGlobal.mSettingProjectPath + "\\" + strConfigName + "\\Project.ini";
            //删除工艺配置
            INIAPI.INIDeleteSection(BattTypePath, strConfigName);
            //更新下拉项
            cmbConfigName.Items.Remove(strConfigName);
            if (cmbConfigName.Items.Count != 0)
            {
                cmbConfigName.Text = cmbConfigName.Items[0].ToString();
            }
            //判断是否是默认工艺配置
            string theDefaultConfig = INIAPI.INIGetStringValue(ProjectPath, strBattType, "CfgName", null);
            if (theDefaultConfig == strConfigName)
            {
                INIAPI.INIWriteValue(ProjectPath, strBattType, "CfgName", "");   //默认CfgName清空

                txt_Info.Text = "默认工艺" + "\r\n";
                txt_Info.Text += "OCV号 :" + cmbOCVType.Text.Trim() + "\r\n";
                txt_Info.Text += "电池型号 :" + cmbBattType.Text.Trim() + "\r\n";
                txt_Info.Text += "工艺配置 :" + "" + "\r\n";

                MessageBox.Show("请重新设置默认工艺配置");
            }



            MessageBox.Show("删除工艺成功！");
        }

        private void cmb_OCVType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            try
            {
                string[] mFiles;
                mFiles = Directory.GetFiles(ClsGlobal.mSettingProjectPath + "\\" + cmbOCVType.Text.Trim(), "*.ini");
                List<String> lstBattType = new List<string>();
                cmbConfigName.Items.Clear();
                cmbBattType.Items.Clear();

                for (int i = 0; i < mFiles.Length; i++)
                {
                    string[] arr = Path.GetFileName(mFiles[i]).Split('.');
                    lstBattType.Add(arr[0]);
                    //flag = true;
                    if (lstBattType[i] != "Project")
                    {
                        cmbBattType.Items.Add(lstBattType[i]);
                    }
                }

                if (lstBattType.Count <= 1)
                {
                    cmbBattType.Text = "";
                    cmbConfigName.Text = "";
                    txt_Info.Text = "";

                    txtUpLMT_V.Text = "";
                    txtDownLMT_V.Text = "";

                    txtUpLMT_SV.Text = "";
                    txtDownLMT_SV.Text = "";

                    txtUpLMT_ACIR.Text = "";
                    txtDownLMT_ACIR.Text = "";

                    txtUpLMT_ACIRrange.Text = "";
                    txtDownLMT_ACIRrange.Text = "";

                    txtUpLMT_time.Text = "";
                    txtDownLMT_time.Text = "";

                    txtMaxVoltDrop.Text = "";
                    txtVoltDrop.Text = "";
                    txtMinVoltDrop.Text = "";

                    txtUpLMT_DropRange.Text = "";
                    txtDownLMT_DropRange.Text = "";

                    txt_k.Text = "";
                    lblNote.Text = "";
                    //txtTempBase.Text = "";
                    //txtTempPara.Text = "";
                    ckB_IS_Enable_ACIRRange.Checked = false;
                    ckB_IS_Enable_DropRange.Checked = false;
                    chb_ENVoltDrop.Checked = false;
                }
                else
                {
                    string mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + cmbOCVType.Text + "\\Project.ini";
                    string[] arrConfigName;
                    string mCfgName = "";
                    var index = 0;
                    bool exists = (lstBattType).Contains("Project");
                    if (exists)
                    // 存在
                    {
                        index = lstBattType.ToList().IndexOf("Project");
                        arrConfigName = INIAPI.INIGetAllSectionNames(mFiles[index]);
                        bool mExists = (arrConfigName).Contains("默认型号");
                        if (mExists)
                        // 存在
                        {
                            //string mBattType = INIAPI.INIGetStringValue(mProjectPath, "默认型号", "mProName", null);
                            string mBattType = INIAPI.INIGetStringValue(mProjectPath, "默认型号", "BattType", null);
                            cmbBattType.Text = mBattType;
                            bool exist = (arrConfigName).Contains(mBattType);
                            if (exist)
                            // 存在
                            {
                                //mCfgName= INIAPI.INIGetStringValue(mProjectPath, mBattType, "Condition", null);
                                mCfgName = INIAPI.INIGetStringValue(mProjectPath, mBattType, "CfgName", null);
                            }
                            // 不存在
                            else
                            {
                                mCfgName = "";
                            }
                            //index = lstProcName.ToList().IndexOf(mBattType);
                            index = lstBattType.IndexOf(mBattType);
                            arrConfigName = INIAPI.INIGetAllSectionNames(mFiles[index]);
                        }
                        else
                        // 不存在
                        {
                            cmbBattType.Text = lstBattType[0];
                            index = 0;
                            //INIAPI.INIWriteValue(mProjectPath, "默认型号", "mProName", cmb_BattType.Text.Trim());
                            INIAPI.INIWriteValue(mProjectPath, "默认型号", "BattType", cmbBattType.Text.Trim());
                            arrConfigName = INIAPI.INIGetAllSectionNames(mFiles[0]);
                        }
                    }
                    else
                    // 不存在
                    {
                        cmbBattType.Text = lstBattType[0];
                        index = 0;
                        //INIAPI.INIWriteValue(mProjectPath, "默认型号", "mProName ", cmb_BattType.Text.Trim());                        
                        INIAPI.INIWriteValue(mProjectPath, "默认型号", "BattType ", cmbBattType.Text.Trim());
                        arrConfigName = INIAPI.INIGetAllSectionNames(mFiles[0]);
                    }

                    //删除
                    //for (int j = 0; j < arrConfigName.Length; j++)
                    //{
                    //    cmbConfigName.Items.Add(arrConfigName[j]);

                    //}

                    if (mCfgName != "")
                    {
                        bool Exist = (arrConfigName).Contains(mCfgName);
                        if (Exist)
                        // 存在
                        {
                            cmbConfigName.Text = mCfgName;
                        }
                        else
                        // 不存在
                        {
                            cmbConfigName.Text = "";
                            //cmbConfigName.Text = (arrConfigName != null && arrConfigName.Length >= 1) ? arrConfigName[0] : "";

                        }
                    }
                    else
                    {
                        cmbConfigName.Text = "";
                        //cmbConfigName.Text = (arrConfigName != null && arrConfigName.Length >= 1) ? arrConfigName[0] : "";
                    }

                }
                if (chkEnLocalProcess.Checked == true)
                {
                    if (cmbOCVType.Text == "OCV1" )
                    {
                        groupBox8.Visible = false;
                        groupBox3.Visible = false;
                        groupBox17.Visible = false;
                        groupBox12.Visible = false;
                        groupBox5.Visible = false;
                        groupBox4.Visible = false;
                        groupBox10.Visible = false;
                    }
                    else if (cmbOCVType.Text == "OCV2")
                    {
                        groupBox12.Visible = true;
                        groupBox5.Visible = true;
                        groupBox4.Visible = true;


                        groupBox8.Visible = false;
                        groupBox3.Visible = false;
                        groupBox17.Visible = false;
                        groupBox10.Visible = true;
                        ckB_IS_Enable_DropRange.Visible = false;
                        chb_ENVoltDrop.Visible = false;
                    }
                    else if (cmbOCVType.Text == "OCV3")
                    {
                   
                        groupBox4.Visible = true;

                        groupBox8.Visible = true;
                        groupBox3.Visible = true;
                        groupBox17.Visible = true;
                        groupBox17.Visible = true;
                        groupBox12.Visible = true;
                        groupBox5.Visible = true;
                        groupBox10.Visible = true;
                        ckB_IS_Enable_DropRange.Visible = true;
                        chb_ENVoltDrop.Visible = true;

                    }
                }
            }
            catch (Exception EX)
            {

               
            }

        }

        private void chkEnProjectSet_MouseClick(object sender, MouseEventArgs e)
        {
            frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "启用本地工艺");
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {

                if (chkEnLocalProcess.Checked == true)
                {
                    chkEnLocalProcess.Checked = false;
                }
                else
                {
                    chkEnLocalProcess.Checked = true;
                }
                return;
            }
            if (chkEnLocalProcess.Checked == true)
            {

                if (cmbOCVType.Text == "OCV1")
                {
                    groupBox8.Enabled = false;
                    groupBox7.Enabled = true;
                    groupBox3.Enabled = false;
                }
                //groupBox6.Enabled = true;
           
                groupBox1.Enabled = true;
          
                groupBox4.Enabled = true;

                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { "1" }, "SetType", "ProjectSetType", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    ClsGlobal.ProjectSetType = 1;  //3 设置类型:本地
                    ClsLogs.INIlogNet.WriteInfo("工艺设置", "设置启用本地工艺:" + "1" + "成功");
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("工艺设置", "设置启用本地工艺:" + "1" + "失败");
                    MessageBox.Show("设置是否启用本地工艺发生异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                //groupBox6.Enabled = false;
                groupBox8.Enabled = false;
                groupBox3.Enabled = false;
                groupBox1.Enabled = false;
              
                groupBox4.Enabled = false;
               
                // INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "ProjectSetType", "2");
                //ClsGlobal.ProjectSetType = 3;   //设置类型:MES
                SQLiteDataReader reader = null;
                reader = ClsGlobal.sql.UpdateValues("TestSeting", new string[] { "Value" }, new string[] { "3" }, "SetType", "ProjectSetType", "=");
                int count = reader.RecordsAffected;
                if (count > 0)
                {
                    ClsGlobal.ProjectSetType = 3;  //3 设置类型:本地
                    ClsLogs.INIlogNet.WriteInfo("工艺设置", "设置启用本地工艺:" + "3" + "成功");
                    MessageBox.Show("设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ClsLogs.INIlogNet.WriteFatal("工艺设置", "设置启用本地工艺:" + "3" + "失败");
                    MessageBox.Show("设置是否启用本地工艺发生异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }


        private void chkEnEditProc_MouseClick(object sender, MouseEventArgs e)
        {
            //frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS);     
            //if (frmPwd.ShowDialog() != DialogResult.OK)
            //{
            //    if (chkEnEditProc.Checked == true)
            //    {
            //        chkEnEditProc.Checked = false;
            //    }
            //    else
            //    {
            //        chkEnEditProc.Checked = true;
            //    }
            //    return;
            //}
            //if (chkEnEditProc.Checked == true)
            //{

            //    groupBox6.Enabled = true;
            //    btnDelBattType.Enabled = true;
            //    btnDelConfig.Enabled = true;
            //    cmbBattType.DropDownStyle = ComboBoxStyle.DropDown;
            //    cmbConfigName.DropDownStyle = ComboBoxStyle.DropDown;

            //}
            //else
            //{
            //    groupBox6.Enabled = false;
            //    btnDelBattType.Enabled = false;
            //    btnDelConfig.Enabled = false;
            //    cmbBattType.DropDownStyle = ComboBoxStyle.DropDownList;
            //    cmbConfigName.DropDownStyle = ComboBoxStyle.DropDownList;
            //}
        }

        private void FormPorcessSet_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void chkEnEditProc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnEditProc.Checked == true)
            {
                frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "启用工艺编辑");
                if (frmPwd.ShowDialog() != DialogResult.OK)
                {
                    chkEnEditProc.Checked = false;
                    return;
                }
                else
                {
                    groupBox6.Enabled = true;
                    btnDelBattType.Enabled = true;
                    btnDelConfig.Enabled = true;
                    btnSave.Enabled = true;
                    cmbBattType.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbConfigName.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
            else
            {
                groupBox6.Enabled = false;
                btnDelBattType.Enabled = false;
                btnDelConfig.Enabled = false;
                btnSave.Enabled = false;
                cmbBattType.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbConfigName.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            //frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS);
            //if (frmPwd.ShowDialog() != DialogResult.OK)
            //{
            //    if (chkEnEditProc.Checked == true)
            //    {
            //        chkEnEditProc.Checked = false;
            //    }
            //    else
            //    {
            //        chkEnEditProc.Checked = true;
            //    }
            //    return;
            //}
            //if (chkEnEditProc.Checked == true)
            //{

            //    groupBox6.Enabled = true;
            //    btnDelBattType.Enabled = true;
            //    btnDelConfig.Enabled = true;
            //    cmbBattType.DropDownStyle = ComboBoxStyle.DropDown;
            //    cmbConfigName.DropDownStyle = ComboBoxStyle.DropDown;

            //}
            //else
            //{
            //    groupBox6.Enabled = false;
            //    btnDelBattType.Enabled = false;
            //    btnDelConfig.Enabled = false;
            //    cmbBattType.DropDownStyle = ComboBoxStyle.DropDownList;
            //    cmbConfigName.DropDownStyle = ComboBoxStyle.DropDownList;
            //}
        }

        private void cmbBattType_TextChanged(object sender, EventArgs e)
        {
            //清空configDetail
            txtUpLMT_V.Text = "";
            txtDownLMT_V.Text = "";

            txtUpLMT_SV.Text = "";
            txtDownLMT_SV.Text = "";

            txtUpLMT_ACIR.Text = "";
            txtDownLMT_ACIR.Text = "";

            txtUpLMT_ACIRrange.Text = "";
            txtDownLMT_ACIRrange.Text = "";

            txtUpLMT_time.Text = "";
            txtDownLMT_time.Text = "";

            txtMaxVoltDrop.Text = "";
            txtVoltDrop.Text = "";
            txtMinVoltDrop.Text = "";

            txtUpLMT_DropRange.Text = "";
            txtDownLMT_DropRange.Text = "";

            txt_k.Text = "";
            lblNote.Text = "";
            
            //txtTempBase.Text = "";
            //txtTempPara.Text = "";
            ckB_IS_Enable_ACIRRange.Checked = false;
            ckB_IS_Enable_DropRange.Checked = false;
            chb_ENVoltDrop.Checked = false;
            cmbConfigName.Text = "001";
        }

        private void cmbConfigName_TextChanged(object sender, EventArgs e)
        {
            //清空configDetail
            txtUpLMT_V.Text = "";
            txtDownLMT_V.Text = "";

            txtUpLMT_SV.Text = "";
            txtDownLMT_SV.Text = "";

            txtUpLMT_ACIR.Text = "";
            txtDownLMT_ACIR.Text = "";

            txtUpLMT_ACIRrange.Text = "";
            txtDownLMT_ACIRrange.Text = "";

            txtUpLMT_time.Text = "";
            txtDownLMT_time.Text = "";

            txtMaxVoltDrop.Text = "";
            txtVoltDrop.Text = "";
            txtMinVoltDrop.Text = "";

            txtUpLMT_DropRange.Text = "";
            txtDownLMT_DropRange.Text = "";

            txt_k.Text = "";
            lblNote.Text = "";
            //txtTempBase.Text = "";
            //txtTempPara.Text = "";
            ckB_IS_Enable_ACIRRange.Checked = false;
            ckB_IS_Enable_DropRange.Checked = false;
            chb_ENVoltDrop.Checked = false; 
          
        }
    }
}
