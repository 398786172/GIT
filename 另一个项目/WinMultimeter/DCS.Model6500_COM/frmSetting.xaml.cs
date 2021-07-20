using DCS.Common;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;


namespace DCS.Model6500_COM
{
    /// <summary>
    /// frmSetting.xaml 的交互逻辑
    /// </summary>
    public partial class frmSetting
    {
        private DCS.Common.Interface.IMultimeter multimeter;
        DictionaryEx dicConfig;
        private CultureInfo cultureInfo;
        private frmSetting()
        {
            InitializeComponent();
        }
        public frmSetting(DCS.Common.Interface.IMultimeter multimeter) : this()
        {
            #region 语言
            //获取语言
            cultureInfo = this.Dispatcher.Thread.CurrentCulture;
            //设置语言
            string name = cultureInfo.Name;
            string resourceName = string.Format("pack://application:,,,/{0};component/Language/Language.{1}.xaml", this.GetType().Namespace, name);
            var languageResources = this.Resources.MergedDictionaries.Where(x => x.Source!=null&&x.Source.ToString().Contains("Language")).ToList();
            foreach (var r in languageResources)
            {
                this.Resources.MergedDictionaries.Remove(r);
            }
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(resourceName, UriKind.Absolute) });
            #endregion

            //万用表速率
            string item1 = this.FindResource("ComboxItem_最快") as string;
            string item2 = this.FindResource("ComboxItem_快") as string;
            string item3 = this.FindResource("ComboxItem_中") as string;
            string item4 = this.FindResource("ComboxItem_慢") as string;
            string item5 = this.FindResource("ComboxItem_最慢") as string;
            this.cbSpeed.Items.Add(item1);
            this.cbSpeed.Items.Add(item2);
            this.cbSpeed.Items.Add(item3);
            this.cbSpeed.Items.Add(item4);
            this.cbSpeed.Items.Add(item5);
            

            this.multimeter = multimeter;
            //读取配置
            dicConfig = multimeter.GetConfig();
            //将配置显示在界面上
            this.cbPortName.Text = dicConfig["PortName"];
            this.cbBauRate.Text = dicConfig["BauRate"];
            this.cbParity.Text = dicConfig["Parity"];
            this.cbDataBits.Text = dicConfig["DataBits"];
            this.cbStopBits.Text = dicConfig["StopBits"];
            //加入单位，标识万用表返回的值是V还是mV
            this.cbUnit.Text = dicConfig.ContainsKey("Unit") ? dicConfig["Unit"] : "V/A";
            int speed;
            if (dicConfig.ContainsKey("Speed") && int.TryParse(dicConfig["Speed"], out speed))
            {

            }
            else
            {
                speed = 0;
            }
            this.cbSpeed.SelectedIndex = speed;
            //显示错误信息
            this.lbErrorMsg.Content = multimeter.Driver.ErrorMsg;
            this.cbPortName.ItemsSource = System.IO.Ports.SerialPort.GetPortNames();
        }

        private void btnRefreshSerialPort_Click(object sender, RoutedEventArgs e)
        {
            this.cbPortName.ItemsSource = System.IO.Ports.SerialPort.GetPortNames();
        }

        private void btnReadProductModel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(multimeter.GetProductInfo());
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            multimeter.Reset();
        }

        private void SaveSetting()
        {
            dicConfig["PortName"] = this.cbPortName.Text;
            dicConfig["BauRate"] = this.cbBauRate.Text;
            dicConfig["Parity"] = this.cbParity.Text;
            dicConfig["DataBits"] = this.cbDataBits.Text;
            dicConfig["StopBits"] = this.cbStopBits.Text;
            dicConfig["Speed"] = this.cbSpeed.SelectedIndex.ToString();
            dicConfig["Unit"] = string.IsNullOrEmpty(this.cbUnit.Text) ? "V/A" : this.cbUnit.Text;
            this.multimeter.SaveConfig(dicConfig);
        }
        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            SaveSetting();
            this.multimeter.Driver.Init(dicConfig);
            this.lbErrorMsg.Content = multimeter.Driver.ErrorMsg;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string cmd = this.tbCmd.Text;
            if (string.IsNullOrEmpty(cmd))
                return;
            string result=  this.multimeter.WriteString(cmd, true);
            ShowMsg(result, MsgType.Send);
            this.tbCmd.Clear();
        }
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            double? result = this.multimeter.ReadValue();
            if (result==null)
            {
                MessageBox.Show("返回值为空");
            }
            else
            {
                ShowMsg(result.ToString(), MsgType.Receive);
            }
        }
        private void ShowMsg(string msg,MsgType msgType)
        {
            Run run = new Run();
            msg = string.Format("{0} [{1}]:{2}\r\n", DateTime.Now, msgType == MsgType.Send ? "发送" : "接收", msg);
            run.Text = msg;
            this.paragraph.Inlines.Add(run);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSetting();
        }

        private void btnSetVoltage_Click(object sender, RoutedEventArgs e)
        {
            multimeter.SetVoltageFunction();
        }

        private void btnSetCurrent_Click(object sender, RoutedEventArgs e)
        {
            multimeter.SetCurrentFunction();
        }
    }
    public enum MsgType
    {
        Send,
        Receive
    }
}
