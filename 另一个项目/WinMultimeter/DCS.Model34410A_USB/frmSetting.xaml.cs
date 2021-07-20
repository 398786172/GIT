using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DCS.Common;
using DCS.Common.Interface;

namespace DCS.Model34410A_USB
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class frmSetting
    {
        private DCS.Common.Interface.IMultimeter multimeter;
        private CultureInfo cultureInfo;
        public frmSetting()
        {
            InitializeComponent();
        }
    
        public frmSetting(IMultimeter multimeter):this()
        {
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
            this.cbSpeed.Items.Add(item1);
            this.cbSpeed.Items.Add(item2);
            this.cbSpeed.Items.Add(item3);
            this.cbSpeed.Items.Add(item4);
            this.cbSpeed.Items.Add(item5);

            this.multimeter = multimeter;
            this.tbErrorMsg.Text = this.multimeter.Driver.ErrorMsg;
            DictionaryEx dic = this.multimeter.GetConfig();
            this.tbAddress.Text = dic[CommonStr.USBDescription];
            this.cbSpeed.Text = dic[CommonStr.MeasureSpeed];

        }

        private void btnSaveSpeed_Click(object sender, RoutedEventArgs e)
        {
            DictionaryEx dic = new DictionaryEx();
            dic[CommonStr.USBDescription] = this.tbAddress.Text;
            dic[CommonStr.MeasureSpeed] = this.cbSpeed.Text;
            this.multimeter.SaveConfig(dic);
        }

        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            DictionaryEx dic = new DictionaryEx();
            dic[CommonStr.USBDescription] = this.tbAddress.Text;
            dic[CommonStr.MeasureSpeed] = this.cbSpeed.Text;
            this.multimeter.SaveConfig(dic);
            this.multimeter.Driver.Init(dic);
            this.tbErrorMsg.Text = this.multimeter.Driver.ErrorMsg;
        }

        private void btnReadProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = this.multimeter.GetProductInfo();
            MessageBox.Show(string.IsNullOrEmpty(product) ? "" : product);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.multimeter.Driver.ErrorMsg))
            {
                this.multimeter.WriteString(this.cbCMD.Text, false);
                //包含则移除，然后添加到第一位
                string msg = this.cbCMD.Text;
                if (this.cbCMD.Items.Contains(msg))
                {
                    this.cbCMD.Items.Remove(msg);
                }
                this.cbCMD.Items.Insert(0, msg);
                AppendMsg(msg, MsgType.Send);
            }
            else
            {
                MessageBox.Show(this.FindResource("msg_设备连接错误_请重新连接") as string);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            string msg = this.multimeter.WriteString(null, true);
            if (string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(this.FindResource("msg_无返回值") as string);
                return;
            }
            AppendMsg(msg, MsgType.Receive);
        }
        private void AppendMsg(string msg, MsgType msgType)
        {
            Run run = new Run();
            msg = string.Format("{0} [{1}]:{2}\r\n", DateTime.Now, msgType == MsgType.Send ? this.FindResource("msg_Send") : this.FindResource("msg_Receive"), msg);
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
