using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DCS.Common;
using DCS.Common.Interface;

namespace DCS.HIOKI_DM7276_LAN
{
    /// <summary>
    /// frmSetting.xaml 的交互逻辑
    /// </summary>
    public partial class frmSetting
    {
        private IMultimeter multimeter;
        private System.Globalization.CultureInfo cultureInfo;
        private DictionaryEx dicConfig;
        private frmSetting()
        {
            InitializeComponent();            
        }
        public frmSetting(IMultimeter multimeter):this()
        {
            this.multimeter = multimeter;

            #region 语言
            //获取语言
            cultureInfo = this.Dispatcher.Thread.CurrentCulture;
            //设置语言
            string name = cultureInfo.Name;
            string resourceName = string.Format("pack://application:,,,/{0};component/Language/Language.{1}.xaml", this.GetType().Namespace, name);
            var languageResources = this.Resources.MergedDictionaries.Where(x => x.Source != null && x.Source.ToString().Contains("Language")).ToList();
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
            this.cbSpeed.Items.Clear();
            this.cbSpeed.Items.Add(item1);
            this.cbSpeed.Items.Add(item2);
            this.cbSpeed.Items.Add(item3);
            this.cbSpeed.Items.Add(item4);
            this.cbSpeed.Items.Add(item5);


            //读取配置
            dicConfig = multimeter.GetConfig();
            //将配置显示在界面上
            this.cbIP.Text = dicConfig["IP"];
            this.cbPort.Text = dicConfig["Port"];
           
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
            this.tbErrorMsg.Text = multimeter.Driver.ErrorMsg;
            
        }
        private void btnReadProductModel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.multimeter.GetProductInfo());
        }

        private void btnSetVoltage_Click(object sender, RoutedEventArgs e)
        {
            multimeter.SetVoltageFunction();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            multimeter.Reset();
        }

        private void SaveSetting()
        {
            dicConfig["IP"] = this.cbIP.Text;
            dicConfig["Port"] = this.cbPort.Text;
            dicConfig["Speed"] = this.cbSpeed.SelectedIndex.ToString();
            this.multimeter.SaveConfig(dicConfig);
        }
        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            SaveSetting();
            this.multimeter.Driver.Init(dicConfig);
            this.tbErrorMsg.Text = multimeter.Driver.ErrorMsg;
        }

        private void btnReadValue_Click(object sender, RoutedEventArgs e)
        {
            var value = this.multimeter.ReadValue();
            if (value.HasValue)
            {
                MessageBox.Show(value.Value.ToString());
            }
            else
            {
                MessageBox.Show("");
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string cmd = this.tbCmd.Text;
            if (string.IsNullOrEmpty(cmd))
                return;
            this.multimeter.WriteString(cmd, false);
            ShowMsg(cmd, MsgType.Send);
            this.tbCmd.Clear();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            byte[] returnByte = this.multimeter.Driver.ReadByte();
            if (returnByte == null || returnByte.Length == 0)
            {
                MessageBox.Show("返回值为空");
            }
            else
            {
                ShowMsg(Encoding.ASCII.GetString(returnByte), MsgType.Receive);
            }
        }

        private void ShowMsg(string msg, MsgType msgType)
        {
            Run run = new Run();
            msg = string.Format("{0} [{1}]:{2}\r\n", DateTime.Now, msgType == MsgType.Send ? "发送" : "接收", msg);
            run.Text = msg;
            this.paragraph.Inlines.Add(run);
        }
    }
    public enum MsgType
    {
        Send,
        Receive
    }
}
