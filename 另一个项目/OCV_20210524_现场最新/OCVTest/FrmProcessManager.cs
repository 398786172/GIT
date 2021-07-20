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
    public partial class FrmProcessManager : Form
    {
        public FrmProcessManager()
        {
            InitializeComponent();
        }

        ProcessModel seletedModel = new ProcessModel();


        private void FrmProcessManager_Load(object sender, EventArgs e)
        {
            try
            {
                dgvView.AutoGenerateColumns = false;
                dgvView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvView.MultiSelect = false;
                int type = ClsGlobal.OCVType - 1;

                if (type == -1)
                {
                    dgvView.DataSource = ClsProcessSet.LstProcess;
                }
                else
                {
                    dgvView.DataSource = ClsProcessSet.LstProcess.Where(a => a.Type == type).ToList();
                }
                if (ClsProcessSet.WorkingProcess != null)
                {
                    if (ClsProcessSet.LstProcess.Count == 0)
                    {
                        return;
                    }
                    int index = 0;
                    if (type == -1)
                    {
                        index = ClsProcessSet.LstProcess.FindIndex(a => a.IsCurrent);
                    }
                    else
                    {
                        index = ClsProcessSet.LstProcess.Where(a => a.Type == type).ToList().FindIndex(a => a.IsCurrent);
                    }
                    dgvView.Rows[index].Selected = true;
                    seletedModel = ClsProcessSet.WorkingProcess;
                    LoadEdit();
                }else if (dgvView.Rows.Count != 0)
                {
                    dgvView.Rows[0].Selected = true;
                    seletedModel = dgvView.SelectedRows[0].DataBoundItem as ProcessModel;
                    LoadEdit();
                }
                ///非管理员账号关闭 删除和保存功能
                if (ClsGlobal.UserInfo.UserType != UserType.Admin &&  ClsGlobal.UserInfo.UserType != UserType.Engineer)
                {
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载失败:{ex.Message}");
            }
        }



        /// <summary>
        /// 加载列表数据
        /// </summary>
        void LoadData(int type = -1)
        {
            if (type == -1)
            {
                dgvView.DataSource = ClsProcessSet.LstProcess;
            }
            else
            {
                dgvView.DataSource = ClsProcessSet.LstProcess.Where(a => a.Type == type).ToList();
            }
        }

        void LoadEdit()
        {
            if (seletedModel == null)
            {
                return;
            }

            txtMaxNGIR.Text = seletedModel.MaxIR.ToString();
            txtMinNGIR.Text = seletedModel.MinIR.ToString();

            txtMaxNGV.Text = seletedModel.MaxV.ToString();
            txtMinNGV.Text = seletedModel.MinV.ToString();
            txtWarningV.Text = seletedModel.WarningV.ToString();

            txtProcessName.Text = seletedModel.ProcessName;
            txtStayMin.Text = seletedModel.SpanMinute.ToString();
            txtStayTimeHour.Text = seletedModel.SpanHourt.ToString();

            if (seletedModel.Type == 0)
            {
                chkOCV2.Checked = false;  //确保触发事件 chkOCV1_CheckedChanged
                chkOCV1.Checked = true;
            }
            else
            {
                chkOCV2.Checked = true; //确保触发事件 chkOCV1_CheckedChanged
                chkOCV1.Checked = false;
            }
        }

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (dgvView.SelectedRows.Count != 1)
            {
                return;
            }
            seletedModel = dgvView.SelectedRows[0].DataBoundItem as ProcessModel;
            LoadEdit();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClsProcessSet.LstProcess.ForEach(A => A.IsCurrent = false)
; if (string.IsNullOrEmpty(seletedModel.ProcessName) || txtProcessName.Text != seletedModel.ProcessName)
                {
                    MessageBox.Show($"未找到工程{txtProcessName.Text};");
                    return;
                }
                if (seletedModel.IsCurrent)
                {
                    MessageBox.Show($"工程[{txtProcessName.Text}]已经设定为当前工作工程,无法删除;");
                    return;
                }
                if (MessageBox.Show($"是否删除工程{txtProcessName.Text}", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                var lst = ClsProcessSet.LstProcess;
                lst.Remove(seletedModel);
                ClsProcessSet.LstProcess = lst;
                dgvView.DataSource = null;
                int type = chkOCV1.Checked ? 0 : 1;
                LoadData(type);

                MessageBox.Show("删除成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败!:{ex.Message}");
            }
        }


        #region 输入内容验证


        private void txtProcessName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProcessName.Text))
            {
                e.Cancel = true;
                MessageBox.Show("工程名不能为空!");
            }
            else
            {
                e.Cancel = false;

            }
        }

        private void txtStayTimeHour_Validating(object sender, CancelEventArgs e)
        {
            if (CheckInt(txtStayTimeHour.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入整数");
            }
        }

        private void txtStayMin_Validating(object sender, CancelEventArgs e)
        {
            if (CheckInt(txtStayMin.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入整数");
            }
        }

        private void txtWarningV_Validating(object sender, CancelEventArgs e)
        {
            if (CheckDouble(txtWarningV.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字");
            }
        }

        private void txtMaxNGV_Validating(object sender, CancelEventArgs e)
        {
            if (CheckDouble(txtMaxNGV.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字");
            }
        }

        private void txtMinNGV_Validating(object sender, CancelEventArgs e)
        {
            if (CheckDouble(txtMinNGV.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字");
            }
        }

        private void txtMaxNGIR_Validating(object sender, CancelEventArgs e)
        {
            if (CheckDouble(txtMaxNGIR.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字");
            }
        }

        private void txtMinNGIR_Validating(object sender, CancelEventArgs e)
        {
            if (CheckDouble(txtMinNGIR.Text))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("请输入数字");
            }
        }

        bool CheckDouble(string value)
        {
            try
            {
                double.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

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
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProcessName.Text.Trim()))
                {
                    MessageBox.Show("工程名不能为空!");
                }
                int type = chkOCV1.Checked ? 0 : 1;
                if (txtProcessName.Text.Trim() == seletedModel.ProcessName && seletedModel.Type == type)
                {
                    var addModel = new ProcessModel()
                    {
                        ProcessName = txtProcessName.Text.Trim(),
                        MaxIR = double.Parse(txtMaxNGIR.Text),
                        MaxV = double.Parse(txtMaxNGV.Text),
                        MinIR = double.Parse(txtMinNGIR.Text),
                        MinV = double.Parse(txtMinNGV.Text),
                        SpanHourt = int.Parse(txtStayTimeHour.Text),
                        SpanMinute = int.Parse(txtStayMin.Text),
                        Type = chkOCV1.Checked ? 0 : 1,
                        WarningV = double.Parse(txtWarningV.Text),
                        IsCurrent = seletedModel.IsCurrent
                    };
                    var lst = ClsProcessSet.LstProcess;
                    lst.Remove(seletedModel);
                    lst.Add(addModel);
                    ClsProcessSet.LstProcess = lst;
                    MessageBox.Show("修改成功!");
                }
                else
                {
                    if (ClsProcessSet.LstProcess.Count(a => a.ProcessName == txtProcessName.Text.Trim() && a.Type == type) != 0)
                    {
                        MessageBox.Show($"已经存在[{txtProcessName.Text}]工程!");
                        return;
                    }
                    var addModel = new ProcessModel()
                    {
                        ProcessName = txtProcessName.Text.Trim(),
                        MaxIR = double.Parse(txtMaxNGIR.Text),
                        MaxV = double.Parse(txtMaxNGV.Text),
                        MinIR = double.Parse(txtMinNGIR.Text),
                        MinV = double.Parse(txtMinNGV.Text),
                        SpanHourt = int.Parse(txtStayTimeHour.Text),
                        SpanMinute = int.Parse(txtStayMin.Text),
                        Type = chkOCV1.Checked ? 0 : 1,
                        WarningV = double.Parse(txtWarningV.Text)
                    };
                    var lst = ClsProcessSet.LstProcess;
                    lst.Add(addModel);
                    ClsProcessSet.LstProcess = lst;
                    MessageBox.Show("新增成功!");
                }
                dgvView.DataSource = null;
                int type1 = chkOCV1.Checked ? 0 : 1;
                LoadData(type1);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败!{ex.Message}");
            }
        }

        private void btnSetCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvView.SelectedRows.Count != 1 || string.IsNullOrEmpty(seletedModel.ProcessName))
                {
                    MessageBox.Show("请选择一条工程纪录!");
                    return;
                }
                var lst = ClsProcessSet.LstProcess;
                lst.ForEach(a => a.IsCurrent = false);
                lst.Remove(seletedModel);
                seletedModel.IsCurrent = true;
                lst.Add(seletedModel);
                ClsProcessSet.LstProcess = lst;
                dgvView.DataSource = null;
                int type = chkOCV1.Checked ? 0 : 1;
                LoadData(type);
                MessageBox.Show("设定成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"设定失败!{ex.Message}");
            }
        }

        private void chkOCV2_CheckedChanged(object sender, EventArgs e)
        {
            chkOCV1.Checked = !chkOCV2.Checked;
            if (chkOCV1.Checked)
            {
                tlpV.Enabled = true;
                tlpIR.Enabled = true;
            }
            else
            {
                tlpIR.Enabled = false;
            }
            int type = chkOCV1.Checked ? 0 : 1;
            LoadData(type);
        }

        private void chkOCV1_CheckedChanged(object sender, EventArgs e)
        {
            chkOCV2.Checked = !chkOCV1.Checked;
            if (chkOCV1.Checked)
            {
                tlpV.Enabled = true;
                tlpIR.Enabled = true;
            }
            else
            {
                tlpIR.Enabled = false;
            }
            int type = chkOCV1.Checked ? 0 : 1;
            LoadData(type);
        }
    }
}
