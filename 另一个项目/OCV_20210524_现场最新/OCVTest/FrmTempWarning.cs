using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV.OCVTest
{
    public partial class FrmTempWarning : Form
    {
        public FrmTempWarning()
        {
            InitializeComponent();
        }
        ClsIOControl iOControl = ClsGlobal.mIOControl;
        bool isWarning = false;
        private void FrmTempWarning_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        public void Waring( string waringMasege) {
            if (IsHandleCreated)
            {
                Invoke(new EventHandler((o, e) =>
                {
                    richTextBox1.AppendText($"{waringMasege}\r\n");
                    if (!isWarning)
                    {
                        OpenWarning();
                    }
                }));
         

            }

        }

        void OpenWarning() {
            isWarning = true;
            iOControl.Set_TestFinish();
            System.Threading.Thread.Sleep(2*1000);
            iOControl.Set_ResetTestFinish();
            iOControl.Set_DebugIn();
            iOControl.Set_TowerRedLight_On();
            iOControl.Set_TowerOrangeLight_Off();
            iOControl.Set_TowerGreenLight_Off();
            iOControl.Set_TowerBusser_On();
        }

        void CloseWarning() {
            isWarning = false;
            iOControl.Set_TowerRedLight_Off();
            iOControl.Set_TowerOrangeLight_Off();
            iOControl.Set_TowerGreenLight_On();
            iOControl.Set_TowerBusser_Off();
            iOControl.Set_DebugOut();
        }

        private void FrmTempWarning_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            CloseWarning();
        }
    }
}
