using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV
{
    public partial class UCTempView : UserControl
    {
        /// <summary>
        /// 温度警报事件委托
        /// </summary>
        /// <param name="index">监测器序号</param>
        /// <param name="tempValue">警报值</param>
        /// <param name="warningTime">警报事件</param>
        /// <param name="type">警报类型 1:上限报警 , 2:下限报警</param>
        public delegate void DelegateTempWarning(int index, double tempValue, DateTime warningTime, int type);
        /// <summary>
        /// 温度警报事件
        /// </summary>
        public event DelegateTempWarning OnDelegateTempWarning;
        int _TempViewNo;
        /// <summary>
        /// 监测器序号
        /// </summary>
        public int TempAdd
        {
            get
            {
                return _TempViewNo;
            }
            set
            {
                _TempViewNo = value;
            }
        }

        public int TempViewNo
        {
            set
            {
                _TempViewNo = value;
                labNo.Text = $"监测器[{value}]";
            }
        }


        /// <summary>
        /// 上限报警值,默认值50
        /// </summary>
        public double MaxWarningValue { get; set; } = 50;

        /// <summary>
        /// 安全温度
        /// </summary>
        public double SaveTemValue { get; set; }
        /// <summary>
        /// 上限报警颜色,默认红色
        /// </summary>
        public Color MaxWarningColor { get; set; } = Color.Red;
        /// <summary>
        /// 下限报警值,默认-1
        /// </summary>
        public double MinWarningValue { get; set; } = -1;
        /// <summary>
        /// 下限报警颜色,默认蓝
        /// </summary>
        public Color MinWarningColor { get; set; } = Color.Blue;
        /// <summary>
        /// 正常显示显示,默认绿
        /// </summary>
        public Color CurrentColor { get; set; } = Color.LawnGreen;
        /// <summary>
        /// 显示字体
        /// </summary>
        public Font LableFont { get; set; }

        public bool IsShield
        {
            get
            {
                switch (TempAdd)
                {
                    case 1:
                        return ClsTempSetting.TempSetting.TempDevice1.IsShield;
                    case 2:
                        return ClsTempSetting.TempSetting.TempDevice2.IsShield;
                    case 3:
                        return ClsTempSetting.TempSetting.TempDevice3.IsShield;
                    case 4:
                        return ClsTempSetting.TempSetting.TempDevice4.IsShield;
                    case 5:
                        return ClsTempSetting.TempSetting.TempDevice5.IsShield;
                    case 6:
                        return ClsTempSetting.TempSetting.TempDevice6.IsShield;
                    case 7:
                        return ClsTempSetting.TempSetting.TempDevice7.IsShield;
                    case 8:
                        return ClsTempSetting.TempSetting.TempDevice8.IsShield;
                    case 9:
                        return ClsTempSetting.TempSetting.TempDevice9.IsShield;
                }
                return false;
            }
            set
            {
                var tempSetting = ClsTempSetting.TempSetting;
                switch (TempAdd)
                {
                    case 1:
                        tempSetting.TempDevice1.IsShield = value;
                        break;
                    case 2:
                        tempSetting.TempDevice2.IsShield = value;
                        break;
                    case 3:
                        tempSetting.TempDevice3.IsShield = value;
                        break;
                    case 4:
                        tempSetting.TempDevice4.IsShield = value;
                        break;
                    case 5:
                        tempSetting.TempDevice5.IsShield = value;
                        break;
                    case 6:
                        tempSetting.TempDevice6.IsShield = value;
                        break;
                    case 7:
                        tempSetting.TempDevice7.IsShield = value;
                        break;
                    case 8:
                        tempSetting.TempDevice8.IsShield = value;
                        break;
                    case 9:
                        tempSetting.TempDevice9.IsShield = value;
                        break;
                }
                ClsTempSetting.TempSetting = tempSetting;

            }
        }

        Queue<double> tempMin = new Queue<double>();
        Queue<double> tempMax = new Queue<double>();
        Queue<double> tempSafe = new Queue<double>();
        double _ViewValue = 0;
        /// <summary>
        /// 显示的温度值
        /// </summary>
        public double ViewValue
        {
            get
            {
                return _ViewValue;
            }
            set
            {
                _ViewValue = value;
                if (tempMin.Count > 20)
                {
                    tempMin.Dequeue();
                }
                if (tempMax.Count > 20)
                {
                    tempMax.Dequeue();
                }
                if (tempSafe.Count > 20)
                {
                    tempSafe.Dequeue();
                }
                if(value<= MinWarningValue)
                {
                    tempMin.Enqueue(value);
                }
                else
                {
                    tempMin.Clear();
                }
                if (value >= MaxWarningValue)
                {
                    tempMax.Enqueue(value);
                }
                else
                {
                    tempMax.Clear();
                }
                if (value >= SaveTemValue)
                {
                    tempSafe.Enqueue(value);
                }
                else
                {
                    tempSafe.Clear();
                }
     
                if (TempViewData == null)
                {
                    TempViewData = new TempViewModel()
                    {
                        CurrentValue = value,
                        CurrentTime = DateTime.Now,
                        MaxVaue = value,
                        MaxValueTime = DateTime.Now,
                        MinValue = value,
                        MinValueTime = DateTime.Now
                    };
                }
                else
                {
                    TempViewData = new TempViewModel()
                    {
                        MaxVaue = TempViewData.MaxVaue > value ? TempViewData.MaxVaue : value,
                        MaxValueTime = TempViewData.MaxVaue > value ? TempViewData.MaxValueTime : DateTime.Now,
                        CurrentValue = value,
                        CurrentTime = DateTime.Now,
                        MinValue = TempViewData.MinValue < value ? TempViewData.MinValue : value,
                        MinValueTime = TempViewData.MinValue < value ? TempViewData.MinValueTime : DateTime.Now,
                    };
                }
            }
        }

        TempViewModel _TempViewData;
    
        private TempViewModel TempViewData
        {
            get
            {
                return _TempViewData;
            }
            set
            {


                _TempViewData = value;
                if (!this.IsHandleCreated)
                {
                    return;
                }
                if (!this.IsShield)
                {
                    this.Invoke(new EventHandler((oj, e) =>
                    {
                        labCurrentValue.Text = $"{value.CurrentValue.ToString("0.0")}℃";
                        //labCurrentValue.Text = $"最新值:[{value.CurrentTime.ToString("yyyy-MM-dd hh:mm:ss")}]--{Math.Round(value.CurrentValue, 2)}℃";
                        labMaxValue.Text = $"最大值:[{value.MaxValueTime.ToString("yyyy-MM-dd HH:mm:ss")}]--{Math.Round(value.MaxVaue, 2)}℃";
                        labMinValue.Text = $"最小值:[{value.MinValueTime.ToString("yyyy-MM-dd HH:mm:ss")}]--{Math.Round(value.MinValue, 2)}℃";
                        if (tempMin.Count() > 5)
                        {
                            this.BackColor = MaxWarningColor;
                            if (OnDelegateTempWarning != null)
                            {
                                try
                                {
                                    new System.Threading.Thread(() =>
                                    {
                                        OnDelegateTempWarning?.Invoke(_TempViewNo, value.CurrentValue, value.CurrentTime, 1);
                                    }).Start();
                                }
                                catch
                                {

                                }
                            }
                        }
                        else if (tempMax.Count() > 5)
                        {
                            this.BackColor = MinWarningColor;
                            if (OnDelegateTempWarning != null)
                            {
                                try
                                {
                                    new System.Threading.Thread(() =>
                                    {
                                        OnDelegateTempWarning?.Invoke(_TempViewNo, value.CurrentValue, value.CurrentTime, 2);
                                    }).Start();
                                }
                                catch
                                {

                                }
                            }
                        }
                        else if (tempSafe.Count() > 5)
                        {
                            this.BackColor = MaxWarningColor;
                            if (OnDelegateTempWarning != null)
                            {
                                try
                                {
                                    new System.Threading.Thread(() =>
                                    {
                                        OnDelegateTempWarning?.Invoke(_TempViewNo, value.CurrentValue, value.CurrentTime, 1);
                                    }).Start();
                                }
                                catch
                                {

                                }
                            }
                        }
                        else
                        {
                            this.BackColor = CurrentColor;
                        }
                    }));
                }
                else
                {
                    this.Invoke(new EventHandler((oj, e) =>
                    {
                        labCurrentValue.Text = $"-----";
                        labMaxValue.Text = $"当前监测器屏蔽";
                        labMinValue.Text = $"-----";
                        this.BackColor = Color.Gray;
                    }));
                }
            }

        }



        public UCTempView()
        {
            InitializeComponent();

        }

        public void ResetView()
        {
            if (LableFont != null)
            {
                labMaxValue.Font = LableFont;
                labMinValue.Font = LableFont;
                labCurrentValue.Font = LableFont;
                labNo.Font = LableFont;
            }
        }

        #region 菜单命令

        private void 屏蔽通道ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsShield = true;
        }

        private void 启用传感器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IsShield = false;
        }

        private void 显示传感器1ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 5);
        }

        private void 显示传感器1ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 7);
        }

        private void 显示传感器1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 1);
        }

        private void 显示传感器1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 2);
        }

        private void 显示传感器1ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 3);
        }

        private void 显示传感器1ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 4);
        }

        private void 显示传感器1ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 6);
        }

        private void 显示传感器1ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 8);
        }

        private void 显示传感器9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetViewNo(TempAdd, 9);
        }

        private void SetViewNo(int temAdd, int viewNo)
        {
            TempViewNo = viewNo;
            TempSetting setting = ClsTempSetting.TempSetting;
            switch (temAdd)
            {
                case 1:
                    setting.TempDevice1.Index = viewNo;
                    break;
                case 2:
                    setting.TempDevice2.Index = viewNo;
                    break;
                case 3:
                    setting.TempDevice3.Index = viewNo;
                    break;
                case 4:
                    setting.TempDevice4.Index = viewNo;
                    break;
                case 5:
                    setting.TempDevice5.Index = viewNo;
                    break;
                case 6:
                    setting.TempDevice6.Index = viewNo;
                    break;
                case 7:
                    setting.TempDevice7.Index = viewNo;
                    break;
                case 8:
                    setting.TempDevice8.Index = viewNo;
                    break;
                case 9:
                    setting.TempDevice9.Index = viewNo;
                    break;
            }
            ClsTempSetting.TempSetting = setting;
        }

        #endregion
    }



    public class TempViewModel
    {
        /// <summary>
        /// 最大值 单位为摄氏度
        /// </summary>
        public double MaxVaue { get; set; }
        /// <summary>
        /// 最大值时间
        /// </summary>
        public DateTime MaxValueTime { get; set; }
        /// <summary>
        /// 当前值 单位为摄氏度
        /// </summary>
        public double CurrentValue { get; set; }
        /// <summary>
        /// 获取当前值的时间
        /// </summary>
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// 最小值 单位为摄氏度
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// 最小值时间
        /// </summary>
        public DateTime MinValueTime { get; set; }
    }

}
