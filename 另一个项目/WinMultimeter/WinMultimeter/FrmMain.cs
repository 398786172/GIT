using DCS.Common.Helper;
using DCS.Common.Interface;
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
using System.Threading;

namespace WinMultimeter
{
    public partial class FrmMain : Form
    {
        private IMultimeter _multInstance;
        private Thread thRead = null;
        public Action<string> ShowMsg;
        public FrmMain()
        {
            InitializeComponent();
            ShowMsg = ActShowMsgToUI;
        }

        private void 万用表选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMultChoose frm = new FrmMultChoose())
            {
                frm.ShowDialog();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (Config.Settings == null)
            {
                Config.Settings = ConfigHelper.LoadSetting();
            }
            _multInstance = CreateInstance(Config.Settings.StrMultimeterName);
            if (_multInstance == null)
            {
                MessageBox.Show("万用表加载失败！");
            }
            txtReadTime.Text = Config.Settings.ReadTime.ToString();
            txtPath.Text = Config.Settings.CSVSavePath;
        }

        private void tsmiMulteterSetting_Click(object sender, EventArgs e)
        {
            if (_multInstance != null)
            {
                _multInstance.ShowSetting();
            }
        }

        /// <summary>
        /// 获得接口类 数据集
        /// </summary>
        /// <returns></returns>
        private IMultimeter CreateInstance(string name)
        {

            try
            {
                string[] dllPathList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
                List<Type> templist = new List<Type>();
                foreach (var dllPath in dllPathList)
                {
                    Assembly asm = Assembly.LoadFile(dllPath);
                    Type[] types = asm.GetTypes(); //Mul_Model2700_COM-- Mul_Model2700_COM
                    var temp = types.Where(o => o.GetInterface("IMultimeter") != null && o.Name.Contains(name)).ToList();
                    if (temp.Count > 0)
                    {
                        Type t = temp[0];
                        return Activator.CreateInstance(t) as IMultimeter;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                lbMsg.Items.Add(ex.Message);
                return null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int stime = 0;
            if (int.TryParse(txtReadTime.Text, out stime))
            {
                Config.Settings.ReadTime = stime;
                ConfigHelper.SaveSetting(Config.Settings);
                MessageBox.Show("配置保存成功！");
            }
            else
            {
                MessageBox.Show("请输入整数！");
            }
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_multInstance == null)
                return;
            if (string.IsNullOrEmpty(Config.Settings.CSVSavePath))
            {
                ShowMsg("保存路径参数不能为空！");
                return;
            }
            StartRead();
            if (thRead.IsAlive)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private void StartRead()
        {
            if (thRead == null)
            {
                thRead = new Thread(DoRead);
                thRead.IsBackground = true;
                thRead.Start();
            }
            else
            {
                if (thRead.IsAlive == false)
                {
                    thRead = new Thread(DoRead);
                    thRead.IsBackground = true;
                    thRead.Start();
                }
            }
        }

        private void StopRead()
        {
            if (thRead == null)
                return;
            if (thRead.IsAlive)
            {
                try
                {
                    thRead.Abort();
                    thRead = null;
                }
                catch
                {
                }
            }
        }

        private void DoRead()
        {
            
            while (true)
            {
                
                double? value = _multInstance.ReadValue();
                
                if (value != null)
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    var realValue = Math.Round((double)value, 4);
                    string msg = string.Format("{0}读数:{1}", time, realValue);
                    ShowMsg(msg);
                    
                    SaveCSV(time, realValue);
                    
                }
                Thread.Sleep(Config.Settings.ReadTime);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopRead();
            if (thRead != null && thRead.IsAlive)
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        public Action<string> ActShowMsgToUI
            => (info)
             => ShowMsgToUI(info);

        private void ShowMsgToUI(string info)
        {
            Action act = delegate
              {
                  if (lbMsg.Items.Count > 30)
                  {
                      lbMsg.Items.Clear();
                  }
                  lbMsg.Items.Add(info);
              };
            if (this.InvokeRequired)
            {
                Invoke(act);
            }
            else
            {
                act();
            }
        }
        private void SaveCSV(string strTime, double value)
        {
            bool isAppend = true;
            if (File.Exists(Config.Settings.CSVSavePath))
            {
                isAppend = true;
            }
            else
            {
                isAppend = false;
            }
            
            using (StreamWriter SWR = new StreamWriter(Config.Settings.CSVSavePath, isAppend, Encoding.Default))
            {
                if (isAppend == false)
                {
                    SWR.WriteLine("读取时间,万用表数值");
                }
                SWR.WriteLine(strTime + "," + value);
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog savefile = new SaveFileDialog())
            {
                savefile.Filter = "*.csv|*.csv";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = savefile.FileName;
                }
            }
        }
    }
}
