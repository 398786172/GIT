using OCV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV.OCV_SYS
{
    public partial class FrmManualInput : Form
    {

        /// <summary>
        /// 托盘号正则表达式
        /// </summary>
        public string TrayCodeRegEx
        {
            set { this.trayCodeRegEx = value; }
        }

        /// <summary>
        /// 电芯条码正则表达式
        /// </summary>
        public string CellCodeRegEx
        {
            set { this.cellCodeRegEx = value; }
        }
        private string cellCodeRegEx = "";
        private string trayCodeRegEx = "";
        private int cellCountInTray = 16;
        private string trayCodeIn = "";
        //电芯条码绑定显示
        private List<C_CellBindShow> lstCellBindShow = new List<C_CellBindShow>();
        //已绑定托盘条码显示
        private List<C_TrayCodeShow> lstTrayCodeShow = new List<C_TrayCodeShow>();
        private string codeDataFile = Environment.CurrentDirectory + @"\CodeFile\CodeData.db";
        public FrmManualInput()
        {
            InitializeComponent();
            InitCellsInfo();
            InitTrayCodeList();
        }

        private void InitCellsInfo()
        {
            //遍历通道
            for (int j = 0; j < this.cellCountInTray; j++)
            {
                C_CellBindShow cbs = new C_CellBindShow();

                cbs.BindTime = "";
                cbs.TrayCode = "";
                cbs.Channel = (j + 1).ToString();
                cbs.CellCode = "";
                cbs.TrayIndex = j + 1;
                this.lstCellBindShow.Add(cbs);
            }

            //
            this.dgvCellCodeBind.DataSource = this.lstCellBindShow;

            //列名称
            this.dgvCellCodeBind.Columns["Channel"].HeaderText = "通道号";
            this.dgvCellCodeBind.Columns["CellCode"].HeaderText = "电芯条码";

            //列宽度
            this.dgvCellCodeBind.Columns["CellCode"].Width = 250;

            //列只读使能
            this.dgvCellCodeBind.Columns["Channel"].ReadOnly = true;
            //遍历列
            for (int i = 0; i < this.dgvCellCodeBind.ColumnCount; i++)
            {
                if (this.dgvCellCodeBind.Columns[i].ReadOnly == true)
                {
                    this.dgvCellCodeBind.Columns[i].DefaultCellStyle.BackColor = Color.Silver;
                }
            }

            //
            this.dgvCellCodeBind.Refresh();
        }

        private void InitTrayCodeList()
        {
            //加载已有托盘号
            this.lstTrayCodeShow = this.LoadTrayCode(DateTime.Now.AddDays(-2));

            this.dgvTrayCodeShow.DataSource = new BindingList<C_TrayCodeShow>(this.lstTrayCodeShow);

            //列名称
            this.dgvTrayCodeShow.Columns["TrayCode"].HeaderText = "托盘号";
            this.dgvTrayCodeShow.Columns["BindTime"].HeaderText = "绑定时间";

            //列宽度
            this.dgvTrayCodeShow.Columns["ID"].Width = 75;
            this.dgvTrayCodeShow.Columns["TrayCode"].Width = 150;
            this.dgvTrayCodeShow.Columns["BindTime"].Width = 150;

            //列显示
            this.dgvTrayCodeShow.Columns["CellCount"].Visible = false;
            this.dgvTrayCodeShow.Columns["StateFlag"].Visible = false;

            //列只读使能
            this.dgvTrayCodeShow.Columns["ID"].ReadOnly = true;
            this.dgvTrayCodeShow.Columns["TrayCode"].ReadOnly = true;
            this.dgvTrayCodeShow.Columns["BindTime"].ReadOnly = true;

            //遍历列
            for (int i = 0; i < this.dgvTrayCodeShow.ColumnCount; i++)
            {
                if (this.dgvTrayCodeShow.Columns[i].ReadOnly == true)
                {
                    this.dgvTrayCodeShow.Columns[i].DefaultCellStyle.BackColor = Color.Silver;
                }
            }

            //
            this.dgvTrayCodeShow.Refresh();
        }

        /// <summary>
        /// 加载托盘条码
        /// </summary>
        /// <param name="dtStart"></param>
        /// <returns></returns>
        private List<C_TrayCodeShow> LoadTrayCode(DateTime dtStart)
        {
            List<C_TrayCodeShow> lstTCS = new List<C_TrayCodeShow>();

            //保存条码绑定关系

            string sqlStr = "select distinct TrayCode,BindTime from CellBind where "
                          + "BindTime > '" + dtStart.ToString("yyyy-MM-dd") + "' order by ID desc;";

            DataSet ds = ClsGlobal.sqlCodeData.ExecuteDataSet(sqlStr, null);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    C_TrayCodeShow tcs = new C_TrayCodeShow();

                    tcs.ID = i + 1;
                    tcs.TrayCode = dt.Rows[i]["TrayCode"].ToString();

                    tcs.BindTime = dt.Rows[i]["BindTime"].ToString();
                    DateTime dtBindTime = DateTime.Parse(tcs.BindTime);
                    tcs.BindTime = dtBindTime.ToString("yyyy-MM-dd HH:mm:ss");

                    lstTCS.Add(tcs);
                }
            }

            return lstTCS;
        }

        /// <summary>
        /// 托盘号合法性检查（正则）
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        private bool TrayCodeRegexCheck(string trayCode)
        {
            bool flag = false;

            Regex r = new Regex(this.trayCodeRegEx);

            flag = r.IsMatch(trayCode);

            return flag;
        }

        /// <summary>
        /// 电芯条码合法性检查（正则）
        /// </summary>
        /// <param name="cellCode"></param>
        /// <returns></returns>
        private bool CellCodeRegexCheck(string cellCode)
        {
            bool flag = false;

            Regex r = new Regex(this.cellCodeRegEx);

            flag = r.IsMatch(cellCode);

            return flag;
        }


        /// <summary>
        /// 电芯条码重码检查
        /// </summary>
        /// <param name="cellCode"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool CellCodeReuseCheck(string cellCode, int rowIndex)
        {
            bool flag = false;

            //遍历设备
            for (int i = 0; i < this.lstCellBindShow.Count; i++)
            {
                //
                if (i != rowIndex && this.lstCellBindShow[i].CellCode == cellCode)
                {
                    flag = true;
                }
            }

            return flag;
        }

        private void tbTrayCodeIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.trayCodeIn = this.tbTrayCodeIn.Text.ToUpper();
                this.tbTrayCodeIn.Text = trayCodeIn;

                if (trayCodeIn != "")
                {
                    //托盘条码合法性判断
                    if (this.TrayCodeRegexCheck(trayCodeIn) == false)
                    {
                        MessageBox.Show("对不起，输入的托盘号格式非法！");

                        return;
                    }

                    //网络模式，从服务器获取电芯条码，并保存


                    //加载电芯条码
                    this.lstCellBindShow = this.LoadCellCode(trayCodeIn);

                    this.dgvCellCodeBind.DataSource = this.lstCellBindShow;
                    this.dgvCellCodeBind.Refresh();
                }

                this.dgvCellCodeBind.Focus();
                this.dgvCellCodeBind.Rows[0].Cells["CellCode"].Selected = true;
            }
        }

        /// <summary>
        /// 加载电芯条码
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        private List<C_CellBindShow> LoadCellCode(string trayCode)
        {
            string bTime = "";
            List<C_CellBindShow> lstLCC = this.LoadCellCode(trayCode, ref bTime);

            this.tbBindTime.Text = bTime;

            return lstLCC;
        }
        private List<C_CellBindShow> LoadCellCode(string trayCode, ref string bTime)
        {
            List<C_CellBindShow> lstCBS = this.lstCellBindShow.Select(a => new C_CellBindShow(a)).ToList();

            //清除原有信息
            for (int i = 0; i < lstCBS.Count; i++)
            {
                lstCBS[i].TrayCode = "";
                lstCBS[i].BindTime = "";
                lstCBS[i].CellCode = "";
                lstCBS[i].StateFlag = 0;
            }

            //保存条码绑定关系

            string sqlStr = "select * from CellBind where "
                          + "TrayCode = '" + trayCode + "' order by Channel;";
            var ds = ClsGlobal.sqlCodeData.ExecuteDataSet(sqlStr, null);
            var dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //若读取到的数量多于托盘中电芯数量，则跳出循环
                    if (i >= this.cellCountInTray)
                    {
                        break;
                    }
                    string chanStr = dt.Rows[i]["Channel"].ToString();
                    C_CellBindShow cbs = lstCBS.First(a => a.Channel == chanStr);
                    cbs.TrayCode = trayCode;
                    cbs.BindTime = dt.Rows[i]["BindTime"].ToString();
                    DateTime dtBindTime = DateTime.Parse(cbs.BindTime);
                    cbs.BindTime = dtBindTime.ToString("yyyy-MM-dd HH:mm:ss");
                    cbs.CellCode = dt.Rows[i]["CellCode"].ToString();
                    cbs.StateFlag = int.Parse(dt.Rows[i]["StateFlag"].ToString());
                    if (bTime == "" && cbs.BindTime != "")
                    {
                        bTime = cbs.BindTime;
                    }
                }
            }

            return lstCBS;
        }

        private void ClearCellCode()
        {
            //
            this.trayCodeIn = "";

            //
            this.tbTrayCodeIn.Text = "";
            this.tbBindTime.Text = "";

            //遍历通道
            for (int j = 0; j < this.lstCellBindShow.Count; j++)
            {
                C_CellBindShow cbs = this.lstCellBindShow[j];

                cbs.BindTime = "";
                cbs.TrayCode = "";
                cbs.CellCode = "";
                cbs.StateFlag = 0;
            }

            this.dgvCellCodeBind.Refresh();
        }


        /// <summary>
        /// 保存电芯条码
        /// </summary>
        private bool SaveCellCode()
        {
            bool flag = false;

            if (this.trayCodeIn == "")
            {
                //
                MessageBox.Show("对不起，托盘号不能为空！");

                return flag;
            }

            try
            {
                string bindTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                List<string> lstSqlStr = new List<string>();
                string sqlStr = "";
                string insertSql = "";
                //遍历通道
                for (int j = 0; j < this.lstCellBindShow.Count; j++)
                {
                    C_CellBindShow cbs = this.lstCellBindShow[j];

                    cbs.BindTime = bindTime;
                    cbs.TrayCode = this.trayCodeIn;

                    //
                    if (cbs.CellCode != "")
                    {
                        //
                        insertSql += "insert into CellBind(TrayCode, BindTime, Channel, CellCode) Values("
                               + " '" + cbs.TrayCode + "'"
                               + ",'" + cbs.BindTime + "'"
                               + ", " + cbs.Channel
                               + ",'" + cbs.CellCode.ToUpper() + "');";
                    }
                }

                //保存条码绑定关系


                //清空原有条码
                sqlStr = "delete from CellBind where TrayCode = '" + this.trayCodeIn + "';";
                ClsGlobal.sqlCodeData.ExecuteQuery(sqlStr);

                //保存电芯条码
                // flag = dh.UpdatePara(lstSqlStr);
                int count = ClsGlobal.sqlCodeData.ExecuteNonQuery(insertSql, null);
                if (count > 1)
                {
                    flag = true;
                    //绑定时间
                    this.tbBindTime.Text = bindTime;

                    //刷新托盘列表
                    this.InitTrayCodeList();
                    //
                    MessageBox.Show("保存电芯条码成功!");
                }
                else
                {
                    //
                    MessageBox.Show("保存电芯条码失败!");
                }
            }
            catch (Exception exp)
            {
                //
            }

            return flag;
        }
        private void dgvCellCodeBind_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //行、列合法
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //若为电芯条码列
                if (this.dgvCellCodeBind.Columns[e.ColumnIndex].Name == "CellCode")
                {
                    int rowIndex = e.RowIndex;
                    string cellCode = "";

                    if (this.dgvCellCodeBind.Rows[rowIndex].Cells["CellCode"].Value != null)
                    {
                        cellCode = this.dgvCellCodeBind.Rows[rowIndex].Cells["CellCode"].Value.ToString().ToUpper();

                        //
                        if (cellCode != "")
                        {
                            //电芯条码合法性判断
                            if (CellCodeRegexCheck(cellCode) == false)
                            {
                                MessageBox.Show("对不起，输入的电芯条码格式非法！");

                                this.tbFocus3.Focus();

                                return;
                            }

                            //电芯条码重码检查
                            if (CellCodeReuseCheck(cellCode, rowIndex) == true)
                            {
                                this.dgvCellCodeBind.Rows[rowIndex].Cells["cellCode"].Value = "";

                                MessageBox.Show("对不起，输入的电芯条码已经绑定到别的库位！");

                                this.tbFocus3.Focus();

                                return;
                            }

                            //若为最后一个电芯，则自动保存
                            if (rowIndex == this.dgvCellCodeBind.RowCount - 1)
                            {
                                this.SaveCellCode();
                            }
                        }
                    }
                }
            }
        }

        private void dgvTrayCodeShow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //行、列合法
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //
                if (this.tbTrayCodeIn.Text != "")
                {
                    var ret = MessageBox.Show("正在扫描电芯条码未保存，确定放弃保存吗？", "警告", MessageBoxButtons.YesNo);

                    if (ret == DialogResult.No)
                    {
                        return;
                    }
                }

                this.trayCodeIn = this.dgvTrayCodeShow.Rows[e.RowIndex].Cells["TrayCode"].Value.ToString();
                this.tbTrayCodeIn.Text = this.trayCodeIn;

                //加载电芯条码和绑定时间
                string bindTime = "";
                this.lstCellBindShow = this.LoadCellCode(this.trayCodeIn, ref bindTime);
                this.tbBindTime.Text = bindTime;

                this.dgvCellCodeBind.DataSource = this.lstCellBindShow;
                this.dgvCellCodeBind.Refresh();

                this.dgvCellCodeBind.Focus();
                this.dgvCellCodeBind.Rows[0].Cells["CellCode"].Selected = true;
            }
        }

        private void dgvCellCodeBind_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            #region 条码列大写
            if (dgvCellCodeBind.CurrentCell.ColumnIndex == 2)
            {//列索引
                DataGridView dgv = (DataGridView)sender;
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    DataGridViewTextBoxEditingControl editiongControl = (DataGridViewTextBoxEditingControl)e.Control;
                    if (dgv.CurrentCell.OwningColumn.Name == "CellCode")
                    {
                        editiongControl.CharacterCasing = CharacterCasing.Upper;
                    }
                    else
                    {
                        editiongControl.CharacterCasing = CharacterCasing.Normal;
                    }
                }
            }
            #endregion
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.trayCodeIn = this.tbTrayCodeIn.Text;

            //托盘条码合法性判断
            if (this.TrayCodeRegexCheck(this.trayCodeIn) == false)
            {
                MessageBox.Show("对不起，输入的托盘号格式非法！");

                return;
            }

            this.SaveCellCode();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            this.ClearCellCode();

            //
            MessageBox.Show("清空条码成功!");
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            //
            this.trayCodeIn = this.tbTrayCodeIn.Text;

            //托盘条码合法性判断
            if (this.TrayCodeRegexCheck(this.trayCodeIn) == false)
            {
                MessageBox.Show("对不起，输入的托盘号格式非法！");

                return;
            }

            //
            if (this.trayCodeIn == "")
            {
                //
                MessageBox.Show("对不起，输入的托盘号不能为空！");

                return;
            }

            //保存条码绑定关系


            //清空原有条码
            string sqlStr = "delete from CellBind where TrayCode = '" + this.trayCodeIn + "';";

            int num = ClsGlobal.sqlCodeData.ExecuteNonQuery(sqlStr, null);

            if (num > 0)
            {
                //清除电池条码
                this.ClearCellCode();

                //刷新托盘列表
                this.InitTrayCodeList();

                MessageBox.Show("提示，输入的托盘绑定信息删除成功！");
            }
        }

        private void FrmManualInput_Load(object sender, EventArgs e)
        {

        }
    }
}
