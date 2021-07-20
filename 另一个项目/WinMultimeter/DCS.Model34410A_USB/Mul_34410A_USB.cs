using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCS.Common;
using DCS.Common.Interface;
using DCS.GPIBDriver;
using System.ComponentModel;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace DCS.Model34410A_USB
{
   
    
    [MultimeterDescription("34410A","USB/GPIB/TCP")]
    public class Mul_34410A_USB : IMultimeter
    {
        private IDriver driver = new GPIBDriver.GPIBDriver();
        private DictionaryEx dicEx;
        public IDriver Driver
        {
            get { return driver; }
        }
        public Mul_34410A_USB()
        {
            dicEx = this.GetConfig();
        }
        public DictionaryEx GetConfig()
        {
            DictionaryEx dic = new DictionaryEx();
            //读取配置，初始化
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + this.GetType().Name + ".xml");
            if (!File.Exists(configFile))
            {
                System.Windows.Forms.MessageBox.Show(this.GetType().Name + "配置文件不存在,请检查路径：\r\n" + configFile);
                Environment.Exit(-1);
                return null;
            }
            XDocument doc = XDocument.Load(configFile);
            dic[CommonStr.USBDescription] = doc.Element(this.GetType().Name).Element(CommonStr.USBDescription).Value;
            dic[CommonStr.MeasureSpeed] = doc.Element(this.GetType().Name).Element(CommonStr.MeasureSpeed).Value;
            return dic;
        }
        public void SaveConfig(DictionaryEx dic)
        {
            dicEx = dic;
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + this.GetType().Name + ".xml");
            if (File.Exists(configFile))
            {
                File.Delete(configFile);
            }
            string className = this.GetType().Name;
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement(className));
            XElement xElement = doc.Element(className);
            foreach (var key in dic.Keys)
            {
                XElement childElement = new XElement(key);
                childElement.Value = dic[key];
                xElement.Add(childElement);
            }
            doc.Save(configFile);
        }

        public string GetProductInfo()
        {
            return this.WriteString("*IDN?", true);
        }

        public void Init(Dictionary<string, object> dic)
        {
          
            //读取配置，初始化

            this.Driver.Init(dicEx);
        }
        public void ShowSetting()
        {
            frmSetting frm = new frmSetting(this);
            frm.ShowDialog();
        }
        public void Reset()
        {
            this.WriteString("*RST", false);
            System.Threading.Thread.Sleep(100);
            this.WriteString("*CLS", false);
            this.WriteString(":CONF:VOLT:DC 10,MAX");
        }

        public bool SetCurrentFunction()
        {
            try
            {
                this.WriteString(":CONF:CURR:DC 10, MAX");
                string speed = dicEx[CommonStr.MeasureSpeed];
                string cmd = "";
                if (speed == "最快")
                {
                    cmd = "CURR:DC:NPLC 0.02";
                }
                else if (speed == "快")
                {
                    cmd = "CURR:DC:NPLC 0.2";
                }
                else if (speed == "中")
                {
                    cmd = "CURR:DC:NPLC 1";
                }
                else if (speed == "慢")
                {
                    cmd = "CURR:DC:NPLC 10";
                }
                else if (speed == "最慢")
                {
                    cmd = "CURR:DC:NPLC 100";
                }
                else//默认为最快
                {
                    cmd = "CURR:DC:NPLC 0.02";
                }
                this.WriteString(cmd, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetVoltageFunction()
        {
            try
            {
                this.WriteString(":CONF:VOLT:DC 10,MAX");  //CONF:VOLT:DC 10, MAX\n
                string speed = dicEx[CommonStr.MeasureSpeed];
                string cmd = "";
                if (speed == "最快")
                {
                    cmd = "VOLT:DC:NPLC 0.02";
                }
                else if (speed == "快")
                {
                    cmd = "VOLT:DC:NPLC 0.2";
                }
                else if (speed == "中")
                {
                    cmd = "VOLT:DC:NPLC 1";
                }
                else if (speed == "慢")
                {
                    cmd = "VOLT:DC:NPLC 10";
                }
                else if (speed == "最慢")
                {
                    cmd = "VOLT:DC:NPLC 100";
                }
                else//默认为最快
                {
                    cmd = "VOLT:DC:NPLC 0.02";
                }
                this.WriteString(cmd);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string WriteString(string cmd = "", bool needReadResponse = false)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd));
            }
            if (needReadResponse)
            {
                var returnBytes = this.Driver.ReadByte();
                if (returnBytes == null)
                {
                    return null;
                }
                return Encoding.ASCII.GetString(returnBytes);
            }
            else
            {
                return null;
            }
        }

        public double? ReadValue()
        {
            string response = WriteString(":READ?", true);
            double v;
            if (!string.IsNullOrEmpty(response) && double.TryParse(response, out v))
            {
                return (v * 1000);//将V转换为mv
            }
            else
            {
                return null;
            }
        }
    }
}
