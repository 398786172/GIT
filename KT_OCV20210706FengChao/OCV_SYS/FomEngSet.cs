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
    public partial class FomEngSet : Form
    {
        public FomEngSet()
        {
            InitializeComponent();
        }
         private void btnSave_Click(object sender, EventArgs e)
        {
            double Val1, Val2;

            frmUserPwd fuserpwd = new frmUserPwd();
            ClsGlobal.Pwdflag = 3;
            if (fuserpwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                //参数写入-------------------------------------------------
                //容量上下限        
                if (double.TryParse(txtDownLMT_CAP.Text, out Val1) == false)
                {
                    MessageBox.Show   ("容量下限输入出错");
                    return;
                }
                else if (double.TryParse(txtUpLMT_CAP.Text, out Val2) == false)
                {
                    MessageBox.Show("容量上限输入出错");
                    return;
                }

                if (Val2 > Val1)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "DownLmt_CAP", Val1.ToString());
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "UpLmt_CAP", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置容量值上限小于下限");
                    return;
                }

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
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "DownLmt_V", Val1.ToString());
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "UpLmt_V", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置电压值上限小于下限");
                    return;
                }
                //ACIR上下限 
                if (double.TryParse(txtDownLMT_ACIR.Text, out Val1) == false)
                {
                    MessageBox.Show("内阻下限输入出错");
                    return;
                }
                else if (double.TryParse(txtUpLMT_ACIR.Text, out Val2) == false)
                {
                    MessageBox.Show("内阻上限输入出错");
                    return;
                }
                if (Val2 > Val1)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "DownLmt_ACIR", Val1.ToString());
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "UpLmt_ACIR", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置内阻值上限小于下限");
                    return;
                }

                //压降参数
                if (double.TryParse(txtMaxVoltDrop.Text, out Val1) == false)
                {
                    MessageBox.Show("最大压降输入出错");
                    return;
                }
                else if (double.TryParse(txtMinVoltDrop.Text, out Val2) == false)
                {
                    MessageBox.Show("最小压降输入出错");
                    return;
                }

                if (Val2 < Val1)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "MaxVoltDrop", Val1.ToString());
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "OCVSetting", "MinVoltDrop", Val2.ToString());
                }
                else
                {
                    MessageBox.Show("错误;设置压降值上限小于下限");
                    return;
                }

                ////变量加载----------------------------------------------------
                //if (ClsGlobal.SetParaInputLoc == 1)
                //{
                //    ClsGlobal.DownLmt_V = double.Parse(txtDownLMT_V.Text);
                //    ClsGlobal.UpLmt_V = double.Parse(txtUpLMT_V.Text);

                //    ClsGlobal.DownLmt_ACIR = double.Parse(txtDownLMT_ACIR.Text);
                //    ClsGlobal.UpLmt_ACIR = double.Parse(txtUpLMT_ACIR.Text);

                //    ClsGlobal.DownLmt_CAP = double.Parse(txtDownLMT_CAP.Text);
                //    ClsGlobal.UpLmt_CAP = double.Parse(txtUpLMT_CAP.Text);

                //    ClsGlobal.MaxVoltDrop = double.Parse(txtMaxVoltDrop.Text);
                //    ClsGlobal.MinVoltDrop = double.Parse(txtMinVoltDrop.Text);
                //}

                MessageBox.Show("保存成功!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

         private void FomEngSet_Load(object sender, EventArgs e)
         {    
             //电压上下限输出
             txtUpLMT_V.Text = ClsGlobal.UpLmt_V.ToString();
             txtDownLMT_V.Text = ClsGlobal.DownLmt_V.ToString();

             txtUpLMT_ACIR.Text = ClsGlobal.UpLmt_ACIR.ToString();
             txtDownLMT_ACIR.Text = ClsGlobal.DownLmt_ACIR.ToString();

             txtUpLMT_CAP.Text = ClsGlobal.UpLmt_CAP.ToString();
             txtDownLMT_CAP.Text = ClsGlobal.DownLmt_CAP.ToString();

             txtMaxVoltDrop.Text = ClsGlobal.MaxVoltDrop.ToString();
             txtMinVoltDrop.Text = ClsGlobal.MinVoltDrop.ToString();
         }
         
    }
}
