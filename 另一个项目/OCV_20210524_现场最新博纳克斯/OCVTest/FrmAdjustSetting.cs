using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OCV
{
    public partial class FrmAdjSetting : Form
    {
        double[] ArrValue = new double[ClsGlobal.TrayType];
        string mFile = "";

        List<string[]> LstStrData;              //CSV数据集

        public FrmAdjSetting()
        {
            InitializeComponent();
        }

        private void FrmAdjustSetting_Load(object sender, EventArgs e)
        {
            dgvAdjustVal.Columns.Add("No", "No.");
            dgvAdjustVal.Columns.Add("RValue", "补偿值(mΩ)");
            dataGridViewSetData();
            dgvAdjustVal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAdjustVal.Columns[0].Width = 80;
            dgvAdjustVal.AllowUserToAddRows = false;
        }

        private void dataGridViewSetData()
        {
            dgvAdjustVal.Rows.Clear();

            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                DataGridViewRow gridViewRow = new DataGridViewRow();
                gridViewRow.CreateCells(dgvAdjustVal);
                gridViewRow.Cells[0].Value = i + 1;
                gridViewRow.Cells[1].Value = INIAPI.INIGetStringValue(ClsGlobal.mIRAdjustPath, "ACIRAdjust", "CH" + (i + 1).ToString(), null);
                dgvAdjustVal.Rows.Add(gridViewRow);
            }
        }

        private void btnInsertTheSame_Click(object sender, EventArgs e)
        {
            double Val;

            try
            {
                Val = double.Parse(txtIR.Text);

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    dgvAdjustVal.Rows[i].Cells[1].Value = Val;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("出错:" + ex.Message);
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    if (dgvAdjustVal.Rows[i].Cells["RValue"].Value == null)
                    {
                        MessageBox.Show("第" + (i + 1) + "个数据是空值");
                        return;
                    }

                    if (double.TryParse(dgvAdjustVal.Rows[i].Cells["RValue"].Value.ToString(), out ArrValue[i]) == false)
                    {
                        MessageBox.Show("第" + (i + 1) + "个数据输入出错");
                        return;
                    }
                }

                for (int i = 0; i < ClsGlobal.TrayType; i++)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mIRAdjustPath, "ACIRAdjust", "CH" + (i + 1).ToString(), ArrValue[i].ToString("F2"));
                    ClsGlobal.ArrIRAdjustPara[i] = ArrValue[i].ToString();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mpath = Application.StartupPath + "\\ImportIRCompensateData";
            if (!Directory.Exists (mpath ))
            {
                Directory.CreateDirectory(mpath);
            }
            OpenFileDialog oFILE = new OpenFileDialog();
            oFILE.InitialDirectory = mpath;
            oFILE.Filter = "数据文件|*.CSV";
            oFILE.RestoreDirectory = true;
            oFILE.FilterIndex = 1;
            if (oFILE.ShowDialog() == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start(oFILE.FileName);
                mFile = oFILE.FileName;

                //导入校准用ACIR值
                if (mFile == "")
                {
                    MessageBox.Show("选择文件出错");
                    return;
                }

                ImportAdjustTrayData(mFile);

            }
        }

        private void ImportAdjustTrayData(string FilePath)
        {
            List<double> lstImportData = new List<double>();
            double Val;

            try
            {
                //读取文件数据
                LstStrData = ReadCSV(FilePath);

                string str1 = LstStrData[0][0];
                string str2 = LstStrData[0][1];

                //Check Format
                if (string.Compare(str1, "[No]") == 0 && string.Compare(str2, "[Compensate Data]") == 0)
                {

                    //加载数据
                    for (int i = 1; i < LstStrData.Count; i++)
                    {
                        if (LstStrData[i][1] == null)
                        {
                            throw new Exception("存在空数据!");
                        }
                        else
                        {
                            if (double.TryParse(LstStrData[i][1], out Val) == false)
                            {
                                throw new Exception("数据存在非数字!");
                            }
                            else
                            {
                                lstImportData.Add(Val);
                            }
                        }
                    }

                    if (lstImportData.Count != ClsGlobal.TrayType)
                    {
                        throw new Exception("输入数量不够!");
                    }

                    //加入表格
                    for (int i = 0; i < ClsGlobal.TrayType; i++)
                    {
                        dgvAdjustVal.Rows[i].Cells[1].Value = lstImportData[i];
                    }

                }
                else
                {
                    throw new Exception("文件格式出错!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入出错:" + ex.Message);

            }

        }

        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader = new StreamReader(filePathName, Encoding.UTF8);

            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine.Split(','));
                    //Debug.WriteLine(strLine);
                }
            }
            fileReader.Close();
            return ls;
        }
    }
}
