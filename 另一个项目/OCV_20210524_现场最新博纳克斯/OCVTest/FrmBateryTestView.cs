using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV.OCVTest
{
    public partial class FrmBateryTestView : Form
    {
        public FrmBateryTestView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }



        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        Dictionary<int, BateryViewController> dictBatView = new Dictionary<int, BateryViewController>();

        public int TrayRows { get; set; } = 43;
        public int TrayColunms { get; set; } = 6;
        public int BatCount
        {
            get
            {
                return (TrayRows * TrayColunms) - 2;
            }
        }

        void InitController()
        {
            this.tableLayoutPanel1.ColumnCount = this.TrayColunms + 1;
            for (int i = 0; i <= TrayColunms; i++)
            {
                if (i == TrayColunms)
                {
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
                }
                else
                {
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                }
            }
            this.tableLayoutPanel1.RowCount = this.TrayRows + 1;
            for (int i = 0; i <= TrayRows; i++)
            {
                if (i == TrayRows)
                {
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                }
                else if (i < TrayRows)
                {
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                }

            }
            foreach (var b in ChanleMapingSetting.ListBatTestData)
            {
                BateryViewController bateryView = new BateryViewController();

                bateryView.Channle = b.TrayCodeChanle;
                dictBatView.Add(b.TrayCodeChanle, bateryView);
                bateryView.Dock = DockStyle.Fill;
                if (b.TrayCodeChanle > BatCount - 4)
                {
                    this.tableLayoutPanel1.Controls.Add(bateryView, (TrayColunms - 1) - (b.ColunmIndex - 1), (TrayRows - 1) - (b.RowIndex - 1));
                }
                else
                {
                    this.tableLayoutPanel1.Controls.Add(bateryView, (TrayColunms - 1) - (b.ColunmIndex - 1), (TrayRows - 1) - (b.RowIndex - 1));
                }
            }
            for (int i = 1; i < TrayColunms + 1; i++)
            {
                var lab = new Label();
                lab.Text = (i).ToString();
                lab.TextAlign = ContentAlignment.MiddleCenter;
                lab.Dock = DockStyle.Fill;
                lab.BorderStyle = BorderStyle.FixedSingle;
                this.tableLayoutPanel1.Controls.Add(lab, TrayColunms - i, TrayRows);
            }
            this.tableLayoutPanel1.ColumnStyles[TrayColunms].SizeType = SizeType.Absolute;
            this.tableLayoutPanel1.ColumnStyles[TrayColunms].Width = 50;

            for (int i = 1; i < TrayRows + 1; i++)
            {
                var lab = new Label();
                lab.Text = (i).ToString();
                lab.TextAlign = ContentAlignment.MiddleCenter;
                lab.Dock = DockStyle.Fill;
                lab.BorderStyle = BorderStyle.FixedSingle;
                this.tableLayoutPanel1.Controls.Add(lab, TrayColunms, TrayRows - i);
            }
            this.tableLayoutPanel1.RowStyles[TrayRows].SizeType = SizeType.Absolute;
            this.tableLayoutPanel1.RowStyles[TrayRows].Height = 25;

            var labs = new Label();
            labs.Text = "";
            labs.TextAlign = ContentAlignment.MiddleCenter;
            labs.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(labs, TrayColunms - 1, TrayRows + 1);

            BateryTitleView bateryTitle = new BateryTitleView();
            bateryTitle.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(bateryTitle, 0, 0);
            BateryTitleView bateryTitle2 = new BateryTitleView();
            bateryTitle2.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(bateryTitle2, 5, 0);
        }



        public void LogInfo(string mes)
        {
            if (IsHandleCreated)
            {
                Invoke(new EventHandler((o, e) =>
                {
                    if (rtxtLog.Text.Length > 2048)
                    {
                        rtxtLog.Text = "";
                    }
                    rtxtLog.AppendText(mes + "\r\n");
                    rtxtLog.ScrollToCaret();
                }));
            }
        }


        public void ReflashValue()
        {
            while (isRuning)
            {
                try
                {

                    foreach (var b in ChanleMapingSetting.ListBatTestData)
                    {
                        if (dictBatView[b.TrayCodeChanle] == null)
                        {
                            continue;
                        }
                        dictBatView[b.TrayCodeChanle].Channle = b.TrayCodeChanle;
                        if (ClsProcessSet.WorkingProcess != null)
                        {
                            if (string.IsNullOrEmpty(b.BatCode))
                            {
                                dictBatView[b.TrayCodeChanle].Value1 = "--";
                                dictBatView[b.TrayCodeChanle].Value2 = "--";
                                dictBatView[b.TrayCodeChanle].AllColor = Color.Gray;
                                dictBatView[b.TrayCodeChanle].Value1Color = Color.Gray;
                                dictBatView[b.TrayCodeChanle].Value2Color = Color.Gray;
                            }
                            else if (!b.BatCode.StartsWith("000"))
                            {
                                dictBatView[b.TrayCodeChanle].AllColor = SystemColors.Control;
                                dictBatView[b.TrayCodeChanle].Value1Color = SystemColors.Control;
                                dictBatView[b.TrayCodeChanle].Value2Color = SystemColors.Control;
                                dictBatView[b.TrayCodeChanle].AllColor = SystemColors.Control;
                                dictBatView[b.TrayCodeChanle].Value1 = b.OCV.ToString("0.0");
                                dictBatView[b.TrayCodeChanle].Value2 = b.ACIR.ToString("0.0");
                                if (b.OCV > ClsProcessSet.WorkingProcess.MaxV)
                                {
                                    dictBatView[b.TrayCodeChanle].Value1Color = Color.Red;
                                }
                                else if (b.OCV < ClsProcessSet.WorkingProcess.MinV)
                                {
                                    if (!ClsGlobal.IsTestRuning && ClsGlobal.IsStartTest )
                                    {
                                        dictBatView[b.TrayCodeChanle].Value1Color = Color.Blue;
                                    }
                                }
                                else
                                {
                                    dictBatView[b.TrayCodeChanle].Value1Color = SystemColors.Control;
                                }
                                if (ClsGlobal.OCVType == 2)
                                {
                                    dictBatView[b.TrayCodeChanle].Value2Color = Color.Gray;
                                    dictBatView[b.TrayCodeChanle].Value2 = "--";
                                }
                                else if (b.ACIR > ClsProcessSet.WorkingProcess.MaxIR)
                                {
                                    dictBatView[b.TrayCodeChanle].Value2Color = Color.Red;
                                }
                                else if (b.ACIR < ClsProcessSet.WorkingProcess.MinIR)
                                {
                                    if (!ClsGlobal.IsTestRuning && ClsGlobal.IsStartTest)
                                    {
                                        dictBatView[b.TrayCodeChanle].Value2Color = Color.Blue;
                                    }
                                }
                                else
                                {
                                    dictBatView[b.TrayCodeChanle].Value2Color = SystemColors.Control;
                                }
                                
                            }
                            else
                            {
                                dictBatView[b.TrayCodeChanle].Value1 = "--";
                                dictBatView[b.TrayCodeChanle].Value2 = "--";
                                dictBatView[b.TrayCodeChanle].AllColor = Color.Gray;
                                dictBatView[b.TrayCodeChanle].Value1Color = Color.Gray;
                                dictBatView[b.TrayCodeChanle].Value2Color = Color.Gray;
                            }
                        }
                        //dictBatView[b.TrayCodeChanle].Channle = b.TrayCodeChanle;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"刷新托盘界面出现错误:{ex.Message}");
                }
                System.Threading.Thread.Sleep(2 * 1000);
            }
        }
        bool isRuning = true;
        private void FrmBateryTestView_Load(object sender, EventArgs e)
        {
            InitController();
            Task taskReflashValue = new Task(ReflashValue);
            taskReflashValue.Start();
            Task taskReflashProcess = new Task(ReflashProcess);
            taskReflashProcess.Start();
            if (ClsGlobal.OCVType == 1)
            {
                chkOCV1.Checked = true;
                chkOCV2.Checked = false;
            }
            else
            {
                chkOCV1.Checked = false;
                chkOCV2.Checked = true;
            }
            if (this.Parent != null)
            {
                this.Parent.SizeChanged += new EventHandler(ResizeThis);
            }
            this.isStart = false;
        }

        private void ResizeThis(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        void ReflashProcess()
        {
            while (true)
            {
                try
                {
                    if (IsHandleCreated)
                    {
                        Invoke(new EventHandler((o, e) =>
                        {
                            label1.Text = $"当前登录账号[{ClsGlobal.UserInfo.UserCode}]]";
                            var seletedModel = ClsProcessSet.WorkingProcess;
                            if (seletedModel != null)
                            {
                                txtProcessName.Text = seletedModel.ProcessName;
                                txtMaxNGIR.Text = seletedModel.MaxIR.ToString();
                                txtMinNGIR.Text = seletedModel.MinIR.ToString();

                                txtMaxNGV.Text = seletedModel.MaxV.ToString();
                                txtMinNGV.Text = seletedModel.MinV.ToString();
                                txtWarningV.Text = seletedModel.WarningV.ToString();

                                txtStayMin.Text = seletedModel.SpanMinute.ToString();
                                txtStayTimeHour.Text = seletedModel.SpanHourt.ToString();

                            }
                            if (ClsGlobal.mIOControl.Get_M_TrayIn() == 1)
                            {
                                chkOCV1.Enabled = false;
                                chkOCV2.Enabled = false;
                            }
                            else
                            {
                                chkOCV1.Enabled = true;
                                chkOCV2.Enabled = true;
                            }
                            labCount.Text = $" 当前探针压合次数:[{(ClsProbeRecover.ProbeSet.Probes.Count == 0 ? 0 : ClsProbeRecover.ProbeSet.Probes.Max(a => a.Times))}]";
                        }));
                        System.Threading.Thread.Sleep(1 * 1000);
                    }
                }catch
                {
                    System.Threading.Thread.Sleep(1 * 1000);
                }
            }
        }

        private void FrmBateryTestView_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRuning = false;
            return;
        }


        private void chkOCV1_CheckedChanged(object sender, EventArgs e)
        {
            chkOCV2.Checked = !chkOCV1.Checked;
            if (chkOCV1.Checked)
            {
                ClsGlobal.OCVType = 1;
            }
            else
            {
                ClsGlobal.OCVType = 2;
            }
        }

        void CloseMain(Control control )
        {
            if (control.Parent == null)
            {
                var f = control as Form;
                if (f != null)
                {
                    f.Close();
                }
            }
            else {
                CloseMain(control.Parent);
            }
        }
        bool isStart = true;
        private void chkOCV2_CheckedChanged(object sender, EventArgs e)
        {
            chkOCV1.Checked = !chkOCV2.Checked;
            if (chkOCV2.Checked)
            {
                ClsGlobal.OCVType = 2;
            }
            else
            {
                ClsGlobal.OCVType = 1;
            }
            if (!isStart)
            {
                MessageBox.Show("设定完成,重启程序后生效!");
                ClsGlobal.bCloseFrm = true;
                CloseMain(this);
                isStart = false;
            }
        }
    }
}
