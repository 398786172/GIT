using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmRunMode : Form
    {
        public FrmRunMode()
        {
            InitializeComponent();
        }

        private void FrmRunMode_Load(object sender, EventArgs e)
        {
            string Val = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "Run_Mode", null);

            if (int.Parse(Val) == (int)eRunMode.NormalTest)
            {
                ClsGlobal.OCV_RunMode = eRunMode.NormalTest;
                rdoRunMode1.Checked = true;
            }
            //else if (int.Parse(Val) == (int)eRunMode.GoAhead )
            //{
            //    ClsGlobal.OCV_RunMode = eRunMode.GoAhead;
            //    rdoRunMode2.Checked = true;
            //}
            else if (int.Parse(Val) == (int)eRunMode.OfflineTest)
            {
                ClsGlobal.OCV_RunMode = eRunMode.OfflineTest;
                rdoRunMode3.Checked = true;
            }
            else if (int.Parse(Val) == (int)eRunMode.ACIRAdjust)
            {
                ClsGlobal.OCV_RunMode = eRunMode.ACIRAdjust;
                rdoRunMode4.Checked = true;
            }
            else
            {
                throw new Exception();
            }
        }

        private void btnSaveRunMode_Click(object sender, EventArgs e)
        {

            //PubClass.sWinTextInfo = "用户密码确认";
            int Val = 0;

            if (rdoRunMode1.Checked == true)
            {
                ClsGlobal.OCV_RunMode = eRunMode.NormalTest;
                Val = (int)eRunMode.NormalTest;
            }
            //else if (rdoRunMode2.Checked == true)
            //{
            //    ClsGlobal.OCV_RunMode = eRunMode.GoAhead;
            //    Val = (int)eRunMode.GoAhead;
            //}
            else if (rdoRunMode3.Checked == true)
            {
                ClsGlobal.OCV_RunMode = eRunMode.OfflineTest;
                Val = (int)eRunMode.OfflineTest;
            }
            else if (rdoRunMode4.Checked == true)
            {
                ClsGlobal.OCV_RunMode = eRunMode.ACIRAdjust;
                Val = (int)eRunMode.ACIRAdjust;
            }


            INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "Run_Mode ", Val.ToString());

            ClsGlobal.bCloseFrm = true;

            MessageBox.Show("软件将重启","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
            this.Owner.Close();
            
        }
    }
}
