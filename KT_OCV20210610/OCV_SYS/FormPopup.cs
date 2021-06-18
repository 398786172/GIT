using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace OCV
{ 
    /// <summary>
    /// 一个用于消息弹出显示的类
    /// </summary>
    public partial class FormPopup : Form
    {
        //用于维护所有的弹出消息窗口
        private static List<FormPopup> FormsPopup = new List<FormPopup>();
        /// <summary>
        /// 最小的时间片段
        /// </summary>
        private static int TimeFragment { get; set; } = 20;

        /// <summary>
        /// 新增一个显示的弹出窗口
        /// </summary>
        /// <param name="form"></param>
        private static void AddNewForm(FormPopup form)
        {
            try
            {
                foreach (var m in FormsPopup)
                {
                    m.LocationUpMove();
                }
                FormsPopup.Add(form);
            }
            catch(Exception ex)
            {
                Console.WriteLine( GetExceptionMessage( ex ) );
            }
        }
        /// <summary>
        /// 重置所有弹出窗口的位置
        /// </summary>
        private static void ResetLocation()
        {
            try
            {
                for (int i = 0; i < FormsPopup.Count; i++)
                {
                    FormsPopup[i].LocationUpMove(FormsPopup.Count - 1 - i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( GetExceptionMessage( ex ) );
            }
        }


        /// <summary>
        /// 实例化一个窗口信息弹出的对象
        /// </summary>
        public FormPopup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 实例化一个窗口信息弹出的对象
        /// </summary>
        /// <param name="infotext">需要显示的文本</param>
        public FormPopup(string infotext)
        {
            InitializeComponent();
            InfoText = infotext;
        }
        /// <summary>
        /// 实例化一个窗口信息弹出的对象
        /// </summary>
        /// <param name="infotext">需要显示的文本</param>
        /// <param name="infocolor">文本的颜色</param>
        public FormPopup(string infotext,Color infocolor)
        {
            InitializeComponent();
            InfoText = infotext;
            InfoColor = infocolor;
        }
        /// <summary>
        /// 实例化一个窗口信息弹出的对象
        /// </summary>
        /// <param name="infotext">需要显示的文本</param>
        /// <param name="infocolor">文本的颜色</param>
        /// <param name="existTime">指定窗口多少时间后消失，单位毫秒</param>
        public FormPopup(string infotext, Color infocolor,int existTime)
        {
            InitializeComponent();
            InfoText = infotext;
            InfoColor = infocolor;
            InfoExistTime = existTime;
        }
        
        private string InfoText { get; set; } = "This is a test message";
        private Color InfoColor { get; set; } = Color.DimGray;
        private int InfoExistTime { get; set; } = -1;

        private System.Windows.Forms.Timer time = null;

        //下面是可用的常量,按照不合的动画结果声明本身须要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记该标记
        private const int AW_CENTER = 0x0010;//若应用了AW_HIDE标记,则使窗口向内重叠;不然向外扩大
        private const int AW_HIDE = 0x10000;//隐蔽窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口,在应用了AW_HIDE标记后不要应用这个标记
        private const int AW_SLIDE = 0x40000;//应用滑动类型动画结果,默认为迁移转变动画类型,当应用AW_CENTER标记时,这个标记就被忽视
        private const int AW_BLEND = 0x80000;//应用淡入淡出结果

        private void FormPopup_Load(object sender, EventArgs e)
        {
            label1.Text = InfoText;
            label1.ForeColor = InfoColor;
            label2.Text = "关闭";

            AddNewForm(this);
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            //int x = Screen.PrimaryScreen.WorkingArea.Width/ 2 - this.Width/2;
            //int y = Screen.PrimaryScreen.WorkingArea.Height/2 ;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_VER_NEGATIVE);
            TopMost = true;

            if (InfoExistTime > 100)
            {
                time = new Timer( );
                time.Interval = InfoExistTime;
                time.Tick += delegate
                {
                    if (IsHandleCreated)
                    {
                        time.Dispose();
                        AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
                        Close();
                    }
                };
                time.Start();
            }
        }
        /// <summary>
        /// 窗体的位置进行向上调整
        /// </summary>
        public void LocationUpMove()
        {
            this.Location = new Point(this.Location.X, this.Location.Y - Height);
        }
        /// <summary>
        /// 窗体的位置进行向上调整
        /// </summary>
        public void LocationUpMove(int index)
        {
            this.Location = new Point(this.Location.X, 
                Screen.PrimaryScreen.WorkingArea.Bottom - this.Height - index * this.Height);
        }

        private void FormPopup_Closing(object sender, FormClosingEventArgs e)
        {
            //AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
            try
            {
                time.Enabled = false;
                FormsPopup.Remove(this);
                ResetLocation();
            }
            catch (Exception ex)
            {
                Console.WriteLine( GetExceptionMessage( ex ) );
            }
        }


        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private void FormPopup_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //绘制标题
            g.FillRectangle(Brushes.Black, new Rectangle(0, 0, Width - 1, 30));
            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
            };
            g.DrawString("消息提示：", label2.Font, Brushes.White , new Rectangle(5, 0, Width - 1, 30), sf);


            g.DrawRectangle(Pens.White, 0, 0, Width - 1, Height - 1);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //关闭
            Close();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            //label2.BackColor = Color.Tomato;
            BeginBackcolorAnimation(label2, Color.Tomato, 100);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            //label2.BackColor = Color.MistyRose;
            BeginBackcolorAnimation(label2, Color.MistyRose, 100);
        }
        /// <summary>
        /// 获取一个异常的完整错误信息 ->
        /// Gets the complete error message for an exception
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <returns>完整的字符串数据</returns>
        /// <remarks>获取异常的完整信息</remarks>
        /// <exception cref="NullReferenceException">ex不能为空</exception>
       
        public static string GetExceptionMessage(Exception ex)
        {
            return "错误信息：" + ex.Message + Environment.NewLine +
               "错误堆栈：" + ex.StackTrace + Environment.NewLine +
               "错误方法：" + ex.TargetSite;
        }
        /// <summary>
        /// 调整控件背景色，采用了线性的颜色插补方式，实现了控件的背景色渐变，需要指定控件，颜色，以及渐变的时间
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="color">设置的颜色</param>
        /// <param name="time">时间</param>
        public static void BeginBackcolorAnimation(Control control, Color color, int time)
        {
            if (control.BackColor != color)
            {
                Func<Control, Color> getcolor = m => m.BackColor;
                Action<Control, Color> setcolor = (m, n) => m.BackColor = n;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolColorAnimation),
                    new object[] { control, color, time, getcolor, setcolor });
            }
        }
        private static void ThreadPoolColorAnimation(object obj)
        {
            object[] objs = obj as object[];
            Control control = objs[0] as Control;

            Color color = (Color)objs[1];
            int time = (int)objs[2];
            Func<Control, Color> getColor = (Func<Control, Color>)objs[3];
            Action<Control, Color> setcolor = (Action<Control, Color>)objs[4];
            int count = (time + TimeFragment - 1) / TimeFragment;
            Color color_old = getColor(control);

            try
            {
                for (int i = 0; i < count; i++)
                {
                    control.Invoke(new Action(() =>
                    {
                        setcolor(control, Color.FromArgb(
                            GetValue(color_old.R, color.R, i, count),
                            GetValue(color_old.G, color.G, i, count),
                            GetValue(color_old.B, color.B, i, count)));
                    }));
                    Thread.Sleep(TimeFragment);
                }
                control?.Invoke(new Action(() =>
                {
                    setcolor(control, color);
                }));
            }
            catch
            {

            }
        }
        private static byte GetValue(byte Start, byte End, int i, int count)
        {
            if (Start == End) return Start;
            return (byte)((End - Start) * i / count + Start);
        }
        /// <summary>
        /// 获取一个异常的完整错误信息，和额外的字符串描述信息 ->
        /// Gets the complete error message for an exception, and additional string description information
        /// </summary>
        /// <param name="extraMsg">额外的信息</param>
        /// <param name="ex">异常对象</param>
        /// <returns>完整的字符串数据</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string GetExceptionMessage(string extraMsg, Exception ex)
        {
            if (string.IsNullOrEmpty(extraMsg))
            {
                return GetExceptionMessage(ex);
            }
            else
            {
                return extraMsg + Environment.NewLine + GetExceptionMessage(ex);
            }
        }
    }
}
