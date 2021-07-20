using System;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmAdjustSetting : Form
    {
        double[] ArrValue = new double[ClsGlobal.TrayType];

        public FrmAdjustSetting()
        {
            InitializeComponent();
        }

        private void dataGridViewClear()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                DataGridViewRow gridViewRow = new DataGridViewRow();
                gridViewRow.CreateCells(dataGridView1);
                gridViewRow.Cells[0].Value = i + 1;
                gridViewRow.Cells[1].Value = "";
                dataGridView1.Rows.Add(gridViewRow);
            }
        }

        private void FrmAdjustSetting_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("No", "No.");
            dataGridView1.Columns.Add("RValue", "真实内阻值(mΩ)");
            dataGridViewClear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.AllowUserToAddRows = false;
            txtIR.Text = "0";
        }

        private void btnConfirmSetting_Click(object sender, EventArgs e)
        {
            try
            {
                string AdjustPara = "";
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (dataGridView1.Rows[i].Cells["RValue"].Value == null)
                    {
                        MessageBox.Show("第" + (i + 1) + "个数据是空值");
                        return;
                    }

                    if (double.TryParse(dataGridView1.Rows[i].Cells["RValue"].Value.ToString(), out ArrValue[i]) == false)
                    {
                        MessageBox.Show("第" + (i + 1) + "个数据输入出错");
                        return;
                    }

                }
                if (rdoUnit_A.Checked == true)
                {
                    AdjustPara = "A";
                }
                else
                {
                    AdjustPara = "B";
                }
                if (MessageBox.Show("1.请确认要校准的工位是" + AdjustPara + "单元?" + "\r\n" + "2.该组数值是校准托盘的内阻真实值?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    System.Windows.Forms.DialogResult.Yes)
                {

                    ClsGlobal.AdjustPara = AdjustPara;
                    ClsGlobal.ArrIRTrueVal = ArrValue;
                    ClsGlobal.IRTrueValSetFlag = true;
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                for (int i = 0; i < ArrValue.Length; i++)
                {
                    ArrValue[i] = 0;
                }
                ClsGlobal.IRTrueValSetFlag = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double Val;

            try
            {
                Val = double.Parse(txtIR.Text);

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    dataGridView1.Rows[i].Cells[1].Value = Val;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错:" + ex.Message);
            }


        }
    }
}
