using System;
using System.Reflection;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
         
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            this.Text = ClsAbout.AssemblyTitle;
            this.labelProductName.Text = ClsAbout.AssemblyProduct;
            this.labelVersion.Text = ClsAbout.AssemblyVersion;
            this.labelCopyright.Text = ClsAbout.AssemblyCopyright;
            this.labelCompanyName.Text = ClsAbout.AssemblyCompany;
            this.textBoxDescription.Text = ClsAbout.AssemblyDescription;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
