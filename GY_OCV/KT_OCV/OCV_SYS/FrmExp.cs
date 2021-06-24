using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV.OCV_SYS
{
    public partial class FrmExp : Form
    {
        public FrmExp(DataTable table)
        {
            InitializeComponent();
            dataGridView1.DataSource = table;
        }
    }
}
