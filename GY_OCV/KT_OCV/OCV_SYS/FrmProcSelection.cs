using System;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmProSelection : Form
    {
        public FrmProSelection()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            frmUserPwd fuserpwd = new frmUserPwd(PwdType.USER, "默认工艺选择");
            //PubClass.sWinTextInfo = "用户密码确认";
            if (fuserpwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string mProjectPath = ClsGlobal.mSettingProjectPath + "\\" + "OCV" + ClsGlobal.OCVType + "\\Project.ini";
            INIAPI.INIWriteValue(mProjectPath, ClsGlobal.listETCELL[0].MODEL_NO, " ProName", ClsGlobal.listETCELL[0].MODEL_NO);
            INIAPI.INIWriteValue(mProjectPath, ClsGlobal.listETCELL[0].MODEL_NO, "CfgName ", cmb_Process.Text.Trim());
            ClsGlobal.MESConfigIndex = cmb_Process.Items.IndexOf(cmb_Process.Text);
            Close();
        }

        private void FrmProSelection_Load(object sender, EventArgs e)
        {
            labModel_no.Text = ClsGlobal.listETCELL[0].MODEL_NO;    //电池型号
            labProject_no.Text = ClsGlobal.listETCELL[0].PROJECT_NO;    //项目号
            labBatch_NO.Text = ClsGlobal.listETCELL[0].BATCH_NO;    //电池型号
            for (int i = 0; i < ClsGlobal.MESConfig.Count; i++)
            {
                cmb_Process.Items.Add(ClsGlobal.MESConfig[i]);
            }
            cmb_Process.Text = ClsGlobal.MESConfig[0];

        }
    }
}
