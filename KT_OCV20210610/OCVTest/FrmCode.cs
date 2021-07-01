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
    public partial class FrmCode : Form
    {
        public bool OK = false;
        public string TrayCode;


        public FrmCode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtScanCode.Text.Trim() == "")
            {
                MessageBox.Show("输入了空值哦");
                OK = false;
                return;
            }


            if (MessageBox.Show("请确认条码是否正确", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                OK = true;
                TrayCode = txtScanCode.Text.Trim().ToUpper();
                this.Close();
            }
            else
            {
                OK = false;
            }
            
        }
    }
}
