using OCV.OCVTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV
{
    public partial class FormTempView : Form
    {
        public FormTempView()
        {
            InitializeComponent();
        }
        public System.Windows.Forms.TabPage tabPage2;
        ClsTempControl tempControl;
        ClsIOControl iOControl;
        FrmTempWarning frmWarning;

        Dictionary<int, UCTempView> dicTempView;
        private void FormTempView_Load(object sender, EventArgs e)
        {
            tempControl = ClsGlobal.BuildClsTempControl();
            InitControll();
            Task taskReflashTemp = new Task(ReflashView);
            taskReflashTemp.Start();
            LoadTempSetting();
        }

        void InitControll()
        {

            dicTempView = new Dictionary<int, UCTempView>();
            ucTempView1.TempViewNo = 3;
            ucTempView2.TempViewNo = 6;
            ucTempView3.TempViewNo = 9;
            ucTempView4.TempViewNo = 2;
            ucTempView5.TempViewNo = 5;
            ucTempView6.TempViewNo = 8;
            ucTempView7.TempViewNo = 1;
            ucTempView8.TempViewNo = 4;
            ucTempView9.TempViewNo = 7;
            ucTempView1.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView2.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView3.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView4.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView5.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView6.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView7.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView8.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            ucTempView9.OnDelegateTempWarning += new UCTempView.DelegateTempWarning(FormTempView_OnDelegateTempWarning);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice1.ViewIndex, ucTempView1);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice2.ViewIndex, ucTempView2);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice3.ViewIndex, ucTempView3);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice4.ViewIndex, ucTempView4);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice5.ViewIndex, ucTempView5);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice6.ViewIndex, ucTempView6);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice7.ViewIndex, ucTempView7);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice8.ViewIndex, ucTempView8);
            dicTempView.Add(ClsTempSetting.TempSetting.TempDevice9.ViewIndex, ucTempView9);

            ReflashTempSetting();
            if (ClsGlobal.UserInfo.UserType != UserType.Admin && ClsGlobal.UserInfo.UserType != UserType.Engineer)
            {
                this.tabControl1.TabPages.Remove(tabPage2);
            }
        }

        bool isWarning = false;
        private void FormTempView_OnDelegateTempWarning(int index, double tempValue, DateTime warningTime, int type)
        {
            try
            {
                if (dicTempView[index].IsShield)
                {
                    return;
                }
                if (frmWarning == null || frmWarning.DialogResult == DialogResult.Cancel)
                {
                    frmWarning = new FrmTempWarning();
                }
                string strType = type == 1 ? "温度超上限" : "温度超下限";
                string mess = $"[{warningTime.ToString()}]--温度传感器[{index}]测量温度值[{tempValue}],{strType}!";
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler((oj, e) =>
                    {
                        frmWarning.Waring(mess);
                        frmWarning.Show();
                        isWarning = true;
                    }));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TempSpanWarning(double tempValue, DateTime warningTime)
        {
            try
            {

                if (frmWarning == null || frmWarning.DialogResult == DialogResult.Cancel)
                {
                    frmWarning = new FrmTempWarning();
                }

                string strType = $"温度差值达到{tempValue}";
                string mess = $"[{warningTime.ToString()}]-{strType},触发警报!";
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler((oj, e) =>
                    {
                        frmWarning.Waring(mess);
                        frmWarning.Show();
                        isWarning = true;
                    }));
                }
            }
            catch (Exception ex)
            {

            }
        }


        void ReflashTempSetting()
        {
            for (var i = 1; i <= 9; i++)
            {
                dicTempView[i].Enabled = true;
                dicTempView[i].MaxWarningValue = ClsTempSetting.TempSetting.MaxWarningTemp;
                dicTempView[i].MinWarningValue = ClsTempSetting.TempSetting.MinWarningTemp;
                dicTempView[i].SaveTemValue = ClsTempSetting.TempSetting.SafeTemp;
            }
        }

        Queue<double> queTempSpan = new Queue<double>();
        void ReflashView()
        {
            while (true)
            {
                for (int i = 1; i <= 9; i++)
                {
                    dicTempView[i].ViewValue = tempControl.GetTemp(i);
                }
                var tempSpan = tempControl.GetNowTempSpan();
                if (this.IsHandleCreated)
                {
                    Invoke(new EventHandler((o, e) =>
                    {
                        labSpan.Text = $"{Math.Round(tempSpan, 1)}℃";
                        labAVGTemp.Text = $"{Math.Round(tempControl.GetAvgNowTem(), 1)}℃";
                        labSaveTemp.Text = $"{Math.Round(ClsTempSetting.TempSetting.SafeTemp, 1)}℃";
                        labMaxTemp.Text = $"{Math.Round(ClsTempSetting.TempSetting.MaxWarningTemp, 1)}℃";
                        labMinTemp.Text = $"{Math.Round(ClsTempSetting.TempSetting.MinWarningTemp, 1)}℃";
                        labWarningSpan.Text = $"{Math.Round(ClsTempSetting.TempSetting.TempSpan, 1)}℃";
                    }));

                }
                if (queTempSpan.Count >= 20)
                {
                    queTempSpan.Dequeue();
                }
                queTempSpan.Enqueue(tempSpan);
                if (queTempSpan.Count(a => a > ClsTempSetting.TempSetting.TempSpan) > 15)
                {
                    TempSpanWarning(queTempSpan.Average(), DateTime.Now);
                }


                System.Threading.Thread.Sleep(1000);
            }
        }



        void LoadTempSetting()
        {
            var temSetting = ClsTempSetting.TempSetting;

            txtSaveTemp.Text = temSetting.SafeTemp.ToString();
            txtMaxWarningTemp.Text = temSetting.MaxWarningTemp.ToString();
            txtMinWarningTemp.Text = temSetting.MinWarningTemp.ToString();
            txtTempSpan.Text = temSetting.TempSpan.ToString();

            txtK1.Text = temSetting.TempDCRExpression.K1.ToString();
            txtK2.Text = temSetting.TempDCRExpression.K2.ToString();
            txtK3.Text = temSetting.TempDCRExpression.K3.ToString();
            txtTemY.Text = temSetting.TempDCRExpression.TemY.ToString();

            txtTempDevice1.Text = temSetting.TempDevice1.AdjuctValue.ToString();
            txtTempDevice2.Text = temSetting.TempDevice2.AdjuctValue.ToString();
            txtTempDevice3.Text = temSetting.TempDevice3.AdjuctValue.ToString();
            txtTempDevice4.Text = temSetting.TempDevice4.AdjuctValue.ToString();
            txtTempDevice5.Text = temSetting.TempDevice5.AdjuctValue.ToString();
            txtTempDevice6.Text = temSetting.TempDevice6.AdjuctValue.ToString();
            txtTempDevice7.Text = temSetting.TempDevice7.AdjuctValue.ToString();
            txtTempDevice8.Text = temSetting.TempDevice8.AdjuctValue.ToString();
            txtTempDevice9.Text = temSetting.TempDevice9.AdjuctValue.ToString();

            txtViewTemp1.Text = temSetting.TempDevice1.ViewIndex.ToString();
            txtViewTemp2.Text = temSetting.TempDevice2.ViewIndex.ToString();
            txtViewTemp3.Text = temSetting.TempDevice3.ViewIndex.ToString();
            txtViewTemp4.Text = temSetting.TempDevice4.ViewIndex.ToString();
            txtViewTemp5.Text = temSetting.TempDevice5.ViewIndex.ToString();
            txtViewTemp6.Text = temSetting.TempDevice6.ViewIndex.ToString();
            txtViewTemp7.Text = temSetting.TempDevice7.ViewIndex.ToString();
            txtViewTemp8.Text = temSetting.TempDevice8.ViewIndex.ToString();
            txtViewTemp9.Text = temSetting.TempDevice9.ViewIndex.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            lst.Add(txtViewTemp1.Text);
            lst.Add(txtViewTemp2.Text);
            lst.Add(txtViewTemp3.Text);
            lst.Add(txtViewTemp4.Text);
            lst.Add(txtViewTemp5.Text);
            lst.Add(txtViewTemp6.Text);
            lst.Add(txtViewTemp7.Text);
            lst.Add(txtViewTemp8.Text);
            lst.Add(txtViewTemp9.Text);
            if (lst.Distinct().Count() != 9 || lst.Max(a => int.Parse(a)) != 9 || lst.Min(a => int.Parse(a) != 1))
            {
                MessageBox.Show("输入的显示序号只能是1-9,且不能重复!");
                return;
            }
            try
            {
                TempSetting tempSetting = ClsTempSetting.TempSetting;

                tempSetting.MaxWarningTemp = float.Parse(txtMaxWarningTemp.Text);
                tempSetting.MinWarningTemp = float.Parse(txtMinWarningTemp.Text);
                tempSetting.SafeTemp = float.Parse(txtSaveTemp.Text);
                tempSetting.TempSpan = float.Parse(txtTempSpan.Text);

                tempSetting.TempDCRExpression.K1 = float.Parse(txtK1.Text);
                tempSetting.TempDCRExpression.K2 = float.Parse(txtK2.Text);
                tempSetting.TempDCRExpression.K3 = float.Parse(txtK3.Text);
                tempSetting.TempDCRExpression.TemY = float.Parse(txtTemY.Text);

                tempSetting.TempDevice1.AdjuctValue = double.Parse(txtTempDevice1.Text);
                tempSetting.TempDevice2.AdjuctValue = double.Parse(txtTempDevice2.Text);
                tempSetting.TempDevice3.AdjuctValue = double.Parse(txtTempDevice3.Text);
                tempSetting.TempDevice4.AdjuctValue = double.Parse(txtTempDevice4.Text);
                tempSetting.TempDevice5.AdjuctValue = double.Parse(txtTempDevice5.Text);
                tempSetting.TempDevice6.AdjuctValue = double.Parse(txtTempDevice6.Text);
                tempSetting.TempDevice7.AdjuctValue = double.Parse(txtTempDevice7.Text);
                tempSetting.TempDevice8.AdjuctValue = double.Parse(txtTempDevice8.Text);
                tempSetting.TempDevice9.AdjuctValue = double.Parse(txtTempDevice9.Text);


                tempSetting.TempDevice1.ViewIndex = int.Parse(txtViewTemp1.Text);
                tempSetting.TempDevice2.ViewIndex = int.Parse(txtViewTemp2.Text);
                tempSetting.TempDevice3.ViewIndex = int.Parse(txtViewTemp3.Text);
                tempSetting.TempDevice4.ViewIndex = int.Parse(txtViewTemp4.Text);
                tempSetting.TempDevice5.ViewIndex = int.Parse(txtViewTemp5.Text);
                tempSetting.TempDevice6.ViewIndex = int.Parse(txtViewTemp6.Text);
                tempSetting.TempDevice7.ViewIndex = int.Parse(txtViewTemp7.Text);
                tempSetting.TempDevice8.ViewIndex = int.Parse(txtViewTemp8.Text);
                tempSetting.TempDevice9.ViewIndex = int.Parse(txtViewTemp9.Text);

                ClsTempSetting.TempSetting = tempSetting;
                ReflashTempSetting();
                MessageBox.Show("保存成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败:{ex.Message}");
            }
        }


        bool CheckValue(string value)
        {
            bool result = false;
            try
            {
                float.Parse(value);
                result = true;
            }
            catch
            {
                MessageBox.Show("请输入数字!");
                result = false;
            }
            return result;
        }

        private void txtSaveTemp_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtSaveTemp.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtMaxWarningTemp_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtMaxWarningTemp.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtMinWarningTemp_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtMinWarningTemp.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtTempSpan_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtTempSpan.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtK1_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtK1.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtK2_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtK2.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtK3_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtK3.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtTemY_Validating(object sender, CancelEventArgs e)
        {
            if (CheckValue(txtTemY.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtTempDevice1_Validating(object sender, CancelEventArgs e)
        {
            var oj = sender as TextBox;
            if (oj != null)
            {
                if (CheckValue(oj.Text))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }


}
