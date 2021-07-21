using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace OCV
{
    public partial class FormProbeRecover : Form
    {
        public FormProbeRecover()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        ProbeModel probeModel = new ProbeModel();
        List<ProbeRecoverModel> lst = new List<ProbeRecoverModel>();
        ProbeRecoverModel selectedModel = new ProbeRecoverModel();
        private void FormProbeRecover_Load(object sender, EventArgs e)
        {
            probeModel = ClsProbeRecover.ProbeSet;
            lst = probeModel.Probes;
            dataGridView1.DataSource = lst;
            txtStopCount.Text = probeModel.StopCount.ToString();
            txtWarningCount.Text = probeModel.WarningCount.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            selectedModel = lst[e.RowIndex];
            txtTimes.Text = selectedModel.Times.ToString();

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            selectedModel.Times = int.Parse(txtTimes.Text);
            lst.ForEach(a =>
            {
                if (a.Channel == selectedModel.Channel)
                {
                    a.Times = selectedModel.Times;
                }
            });
            probeModel.Probes = lst;
            ClsProbeRecover.ProbeSet = probeModel;
            MessageBox.Show("设定成功!");
        }

        private void txtTimes_Validating(object sender, CancelEventArgs e)
        {
            if (CheckInt(txtTimes.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字.");
            }
        }

        /// <summary>
        /// true:通过验证  false:不通过验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CheckInt(string value)
        {
            try
            {
                int.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnSetCount_Click(object sender, EventArgs e)
        {
            probeModel.StopCount = int.Parse(txtStopCount.Text);
            probeModel.WarningCount = int.Parse(txtWarningCount.Text);
            ClsProbeRecover.ProbeSet = probeModel;
            MessageBox.Show("设定成功!");
        }

        private void txtWarningCount_Validating(object sender, CancelEventArgs e)
        {
            if (CheckInt(txtWarningCount.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字.");
            }
        }

        private void txtStopCount_Validating(object sender, CancelEventArgs e)
        {
            if (CheckInt(txtStopCount.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字.");
            }
        }

        private void btnSetAll_Click(object sender, EventArgs e)
        {
            lst.ForEach(a =>
            {
                a.Times = int.Parse(txtTimes.Text);
            });
            probeModel.Probes = lst;
            ClsProbeRecover.ProbeSet = probeModel;
            MessageBox.Show("设定成功!");
        }
    }

    public static class ClsProbeRecover
    {
        static string saveDir = $"{AppDomain.CurrentDomain.BaseDirectory}\\SettingFile";
        static string saveFile = "ProbeRecover.json";
        static ProbeModel _ProbeSet;
        public static ProbeModel ProbeSet
        {
            get
            {
                if (_ProbeSet == null)
                {
                    _ProbeSet = GetData();
                }
                return _ProbeSet;
            }
            set
            {
                _ProbeSet = value;
                SaveData(_ProbeSet);
            }
        }

        public static void Add()
        {
            var model = ProbeSet;
            var lst = model.Probes;
            lst.ForEach(a =>
            {
                a.Times = a.Times + 1;
            });
            model.Probes = lst;
            ProbeSet = model;
        }
        static ProbeModel GetData()
        {
            ProbeModel result = new ProbeModel();
            if (!File.Exists($"{saveDir}\\{saveFile}"))
            {

                for (int i = 1; i <= 256; i++)
                {
                    result.Probes.Add(new ProbeRecoverModel()
                    {
                        Channel = i,
                        Times = 0
                    });
                }
            }
            else
            {
                string jsonData = File.ReadAllText($"{saveDir}\\{saveFile}");
                result = JsonConvert.DeserializeObject<ProbeModel>(jsonData);
            }

            return result;
        }
        static void SaveData(ProbeModel probeModel)
        {
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            string jsonData = JsonConvert.SerializeObject(probeModel);
            File.WriteAllText($"{saveDir}\\{saveFile}", jsonData);
        }
    }

    public class ProbeModel
    {
        public int WarningCount { get; set; }
        public int StopCount { get; set; }
        public List<ProbeRecoverModel> Probes { get; set; } = new List<ProbeRecoverModel>();

    }

    public class ProbeRecoverModel
    {
        public int Channel { get; set; }
        public int Times { get; set; }
    }
}
