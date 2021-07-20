using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCS.Common;
using DCS.Common.Interface;
using System.Windows.Controls;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.IO.Ports;

namespace DCS.Mode344X_COM
{
    [MultimeterDescription("Model 344X", "串口通讯")]
    public class Mul_Model344X_COM : IMultimeter
    {
        private DictionaryEx dicEx;
        private IDriver driver = new ComDriver.ComDriver();
        public IDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }
        public Mul_Model344X_COM()
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
                MessageBox.Show(this.GetType().Name + "配置文件不存在,请检查路径：\r\n" + configFile);
                Environment.Exit(-1);
                return null;
            }
            XDocument doc = XDocument.Load(configFile);
            foreach (var element in doc.Element(this.GetType().Name).Elements())
            {
                dic[element.Name.ToString()] = element.Value;
            }
            return dic;
        }

        public string GetProductInfo()
        {
            return "344X COM";
        }

        public void Init(Dictionary<string, object> dic)
        {
            System.Windows.Controls.MenuItem menuItem = dic[CommonStr.MenuItem] as System.Windows.Controls.MenuItem;
            var subItem = new MenuItem();
            subItem.Header = "344X COM设置";
            subItem.Click += (sender, e) =>
              {
                  frmSetting frm = new frmSetting(this);
                  frm.ShowDialog();
              };
            //插入菜单到倒数第二的位置
            menuItem.Items.Insert(menuItem.Items.Count - 1, subItem);
           string portName = dicEx["PortName"].ToString();
            int baudRate = int.Parse(dicEx["BauRate"].ToString());
            //Parity parity = (Parity)Enum.Parse(typeof(Parity), dicEx["Parity"].ToString());
            //StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), dicEx["StopBits"].ToString());
            //int dataBits = int.Parse(dicEx["DataBits"].ToString());
            SetPortProperty(portName,baudRate,"无",8,1);
            //读取配置，初始化
            // this.Driver.Init(dicEx);
        }

        public void Reset()
        {
            DCS.ComDriver.ComDriver comDriver = (DCS.ComDriver.ComDriver)driver;
            var mSerialPort = comDriver.GetInstance();
            mSerialPort.WriteLine("SYST:REM");
            mSerialPort.WriteLine("*CLS");
            mSerialPort.WriteLine("*RST");
            mSerialPort.WriteLine("CONF:VOLT:DC 10, MAX");  //1E-6  0.000001
            mSerialPort.WriteLine("VOLT:DC:NPLC 10");  //1E-6  0.000001
            mSerialPort.WriteLine("TRIG:SOUR IMM\n");
        }

        public void SaveConfig(DictionaryEx dic)
        {
            dicEx = dic;//20190510 li 更新配置信息
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

        public bool SetCurrentFunction()
        {
            return true;
        }

        public bool SetVoltageFunction()
        {
            return true;
        }
        /// <summary>
        /// 串口的驱动只实现了WriteByte和ReadByte方法
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="needReadResponse"></param>
        /// <returns></returns>
        public string WriteString(string cmd = "", bool needReadResponse = false)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd + "\r\n"));
                //this.Driver.WriteByte(Encoding.ASCII.GetBytes(cmd + "\n"));
            }
            if (needReadResponse)
            {
                Thread.Sleep(1000);
                byte[] result = this.Driver.ReadByte();
                if (result == null)
                {
                    return null;
                }
                return Encoding.ASCII.GetString(result);
            }
            else
            {
                return null;
            }
        }

        public double? ReadValue()
        {
            DCS.ComDriver.ComDriver comDriver = (DCS.ComDriver.ComDriver)driver;
            var mSerialPort = comDriver.GetInstance();
         

            string strVal;
            mSerialPort.WriteLine("READ?");
            Thread.Sleep(80);
            strVal = mSerialPort.ReadLine();

            if (strVal.Trim().Length <= 0 || strVal == null || strVal == "")
            {
                strVal = "0";
                return null;
            }
            else
            {
                strVal = Convert.ToDouble(strVal).ToString();
            }
            double Val = double.Parse(strVal);
            mSerialPort.DiscardInBuffer();
            return Val;
        }

        /// 设置串口
        /// </summary>
        private void SetPortProperty(string port, int BaudRate, string parity, int dataBits, float stopBits)
        {
            try
            {
                DCS.ComDriver.ComDriver comDriver = (DCS.ComDriver.ComDriver)driver;
                var mSerialPort = comDriver.GetInstance();
                mSerialPort = new SerialPort();
                comDriver.SetSerialPort(mSerialPort);
                // 关闭串口
                if (mSerialPort.IsOpen == true)
                {
                    mSerialPort.Close();
                }
                if ((mSerialPort != null) && (mSerialPort.IsOpen == false))
                {
                    //设置串口名 
                    mSerialPort.PortName = "COM17";
                    //设置串口的波特率 
                    mSerialPort.BaudRate = BaudRate;
                    //设置停止位 
                    float f = stopBits;
                    if (f == 0)
                    {
                        mSerialPort.StopBits = StopBits.None;
                    }
                    else if (f == 1.5)
                    {
                        mSerialPort.StopBits = StopBits.OnePointFive;
                    }
                    else if (f == 1)
                    {
                        mSerialPort.StopBits = StopBits.One;
                    }
                    else if (f == 2)
                    {
                        mSerialPort.StopBits = StopBits.Two;
                    }
                    else
                    {
                        mSerialPort.StopBits = StopBits.One;
                    }
                    //设置数据位 
                    if ((dataBits > 4) && (dataBits < 9))
                    {
                        mSerialPort.DataBits = dataBits;
                    }
                    else
                    {
                        mSerialPort.DataBits = 8;
                    }

                    //设置奇偶校验位 
                    string s = parity.Trim();
                    if ((s.CompareTo("无") == 0) || (s.ToUpper().CompareTo("NONE") == 0))
                    {
                        mSerialPort.Parity = Parity.None;
                    }
                    else if ((s.CompareTo("奇校验") == 0) || (s.ToUpper().CompareTo("ODD") == 0))
                    {
                        mSerialPort.Parity = Parity.Odd;
                    }
                    else if ((s.CompareTo("偶校验") == 0) || (s.ToUpper().CompareTo("EVEN") == 0))
                    {
                        mSerialPort.Parity = Parity.Even;
                    }
                    else
                    {
                        mSerialPort.Parity = Parity.None;
                    }
                    //设置超时读取时间 
                    mSerialPort.ReadTimeout = 5000;
                    mSerialPort.DtrEnable = true;
                    mSerialPort.Encoding = Encoding.UTF8;//2016-5-10
                    mSerialPort.Open();
                    mSerialPort.WriteLine("READ?");
                    Thread.Sleep(80);
                  var  strVal = mSerialPort.ReadLine();
                }

            }
            catch (Exception ex)//IOException)
            {
                throw ex;
            }


        }
    }
}
