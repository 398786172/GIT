using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMultimeter
{
    public partial class UCMultimeter_COM : UserControl
    {
        public UCMultimeter_COM()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                cmbCOM.Items.Add("COM"+i);
            }
        }
    }
}
