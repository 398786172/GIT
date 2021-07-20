using DCS.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMultimeter
{
    public partial class FrmMultChoose : Form
    {
        public FrmMultChoose()
        {
            InitializeComponent();
            var list = GetDataHelperTypes();
            foreach (var item in list)
            {
               RadioButton chb = new RadioButton();
                chb.Text = item.Name;
                chb.Width = flwpChoose.Width;
                flwpChoose.Controls.Add(chb);

            }
         
        }

        /// <summary>
        /// 获得接口类 数据集
        /// </summary>
        /// <returns></returns>
        private List<Type> GetDataHelperTypes()
        {
     
            try
            {
                string[] dllPathList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
                List<Type> templist = new List<Type>();
                foreach (var dllPath in dllPathList)
                {
                    Assembly asm = Assembly.LoadFile(dllPath);
                    Type[] types = asm.GetTypes();
                    var temp = types.Where(o => o.GetInterface("IMultimeter") != null).ToList();
                    if (temp.Count > 0)
                    {
                        templist= templist.Union(temp).ToList();
                    }
                }
                return templist;
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = "";
            foreach (var item in flwpChoose.Controls)
            {
                if (item is RadioButton)
                {
                    RadioButton rb = (RadioButton)item;
                    if (rb.Checked)
                    {
                        name = rb.Text;
                        break;
                    }
                }
            }
            Config.Settings.StrMultimeterName = name;
            ConfigHelper.SaveSetting(Config.Settings);
            MessageBox.Show("配置保存成功！");
            this.Close();
        }

        private void FrmMultChoose_Load(object sender, EventArgs e)
        {

        }
    }
}
