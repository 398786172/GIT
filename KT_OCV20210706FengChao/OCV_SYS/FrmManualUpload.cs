using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OCV
{
    public partial class FrmManualUpload : Form
    {
        FileInfo[] mFilesInfo;
        string mSingleFile = "";

        int SelectFilesType = 0;    //选择文件夹:1  单文件:2

        //CSV数据集
        List<string[]> LstStrData;
        
        //数据库电池数据
        public static List<ET_CELL> mlistETCELL;  

        public FrmManualUpload()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LstStrData = new List<string[]>();
            rdoFolder.Checked = true;
            SelectFilesType = 2;
            rdoSingleFile.Checked = true;
            //groupBox1.Enabled = true;
            //groupBox2.Enabled = false;            
        }

        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog P_File_Folder = new FolderBrowserDialog();
            if (P_File_Folder.ShowDialog() == DialogResult.OK)
            {
                //var files = Directory.GetFiles(P_File_Folder.SelectedPath, "*.csv");

                //foreach (var file in files)
                //{
                //    MessageBox.Show(file);
                //}

                DirectoryInfo folder = new DirectoryInfo(P_File_Folder.SelectedPath);
                mFilesInfo = folder.GetFiles("*.csv");
                txtFolder.Text = P_File_Folder.SelectedPath;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LstStrData = ReadCSV(mFilesInfo[0].FullName);
        
        }

        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader = new StreamReader(filePathName,Encoding.UTF8);
            
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

        private void btnDataUpLoad_Click(object sender, EventArgs e)
        {
            if (SelectFilesType == 1)
            {
                if (mFilesInfo == null)
                {
                    MessageBox.Show("选择文件出错");
                    return;
                }

                foreach (FileInfo F_Info in mFilesInfo)
                {
                    UploadFileData(F_Info.FullName,1);
                }
            }
            else if (SelectFilesType == 2)
            {
                if (mSingleFile == "")
                {
                    MessageBox.Show("选择文件出错");
                    return;
                }

                UploadFileData(mSingleFile,1);
            }
        }

        /// <summary>
        /// 手动上传
        /// </summary>
        /// <param name="FilePath">文件地址</param>
        /// <param name="theType">1: 中鼎  2:Kinte</param>
        private void UploadFileData( string FilePath, int theType =1)
        {
            string TrayCode = "";
            string BattType = "";
            int BattNum = 0;
            string StartTime;
            string EndTime;
            double intervalTime;

            try
            {
                //读取文件数据
                LstStrData = ReadCSV(FilePath);
                string FileName= System.IO.Path.GetFileNameWithoutExtension(FilePath);
                string[] arrstr = FileName.Split('_');
                if (arrstr.Length!=3)
                {
                    throw new Exception(FilePath + "CSV文件名异常: 格式错误");
                }
                int OCVType = 0;
                if (arrstr[0]=="OCV3")
                {
                    OCVType = 3;
                }
                else if (arrstr[0] == "OCV4")
                {
                    OCVType = 4;
                }
                else
                {
                    throw new Exception(arrstr[0] + "获取的文件名中的OCV测试类型异常");
                }

                string str1 = LstStrData[0][0];
                string str2 = LstStrData[1][0];
                string str3 = LstStrData[7][1];

                //Check Format
                if (string.Compare(str1, "[OCVResult]") == 0 && string.Compare(str2, "[ProcessInfo]") == 0)
                {
                    //不是有效数据
                    if (str3.Contains("ID") == true && str3.Length < 6)
                    {
                        throw new Exception(FilePath + "CSV数据出错: 没有有效的电池信息");
                    }
                    else
                    {
                        //TrayCode
                        string[] ArrStrT1 = LstStrData[2][0].Split('=');
                        if (ArrStrT1.Length == 2)
                        {
                            TrayCode = ArrStrT1[1];
                        }
                        else
                        {
                            throw new Exception(FilePath + "CSV数据出错: 找不到托盘号");
                        }
                       
                        ArrStrT1 = LstStrData[3][0].Split('=');
                        if (ArrStrT1.Length == 2)
                        {
                            StartTime = ArrStrT1[1];
                        }
                        else
                        {
                            throw new Exception(FilePath + "CSV数据出错: 找不到Start");
                        }
                        //EndTime
                        ArrStrT1 = LstStrData[4][0].Split('=');
                        if (ArrStrT1.Length == 2)
                        {
                            EndTime = ArrStrT1[1];
                        }
                        else
                        {
                            throw new Exception(FilePath + "CSV数据出错: 找不到End");
                        }
                       
                        DateTime DTStart = DateTime.Parse(StartTime);
                        DateTime DTEnd = DateTime.Parse(EndTime);
                      
                        BattNum = ClsGlobal.TrayType;
                        int sec = (int)((DTEnd - DTStart).TotalMinutes * 60 + (DTEnd - DTStart).TotalSeconds);
                        intervalTime = (double)sec / (BattNum - 1);

                        //Add to listETCELL
                        mlistETCELL = new List<ET_CELL>();
                        for (int i = 7; i < LstStrData.Count; i++)
                        {
                            ET_CELL CellInfo = new ET_CELL();
                            CellInfo.Pallet_ID = TrayCode;
                            CellInfo.Cell_Position = int.Parse(LstStrData[i][0]);
                            CellInfo.Cell_ID = LstStrData[i][1];
                            //CellInfo.OCV_Now = LstStrData[i][2];
                            //CellInfo.NGReason = LstStrData[i][3];
                          

                            if (LstStrData[i][4].Trim() != "")
                            {
                                //CellInfo.OCV_Shell_Now = LstStrData[i][4];
                              
                            }
                            if (LstStrData[i][6].Trim() != "")
                            {
                                //CellInfo.TMP = double.Parse(LstStrData[i][6]);
                             
                            } 
                            CellInfo.End_Write_Time = DTEnd.ToString();
                            mlistETCELL.Add(CellInfo);
                        }
                    
                        if (theType == 1)       //比亚迪
                        {
                            //try
                            //{
                            //    int iResult = 0;

                            //    iResult = ClsGlobal.mDBCOM_OCV_QT.InsertOCVData_SVS(OCVType, ClsGlobal.listETCELL);
                              
                            //    string msn = "托盘[" + mlistETCELL[0].Pallet_ID + "]的测试数据保存到比亚迪数据库成功!";
                            //    this.Invoke(new EventHandler(delegate
                            //    {
                            //        txtMsn.Text += msn + Environment.NewLine;
                            //        txtMsn.Select(txtMsn.TextLength, 0);
                            //        txtMsn.ScrollToCaret();
                            //    }));
                            //}
                            //catch (Exception ex)
                            //{
                            //    throw ex;
                            //}
                        }
                        else if (theType == 2)      //擎天
                        {
                            //Upload Data
                            int iResult = 0;
                            try
                            {
                                //TO QT SERVER
                                iResult = ClsGlobal.mDBCOM_OCV_QT.InsertOCVACIRData(OCVType, ClsGlobal.listETCELL,1);
                               
                                string msn = "托盘[" + mlistETCELL[0].Pallet_ID + "]的测试数据保存到擎天数据库成功!";
                                this.Invoke(new EventHandler(delegate
                                {
                                    txtMsn.Text += msn + Environment.NewLine;
                                    txtMsn.Select(txtMsn.TextLength, 0);
                                    txtMsn.ScrollToCaret();
                                }));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new EventHandler(delegate
                {
                    txtMsn.Text += ex.Message + Environment.NewLine;
                }));                
                return;
            }

        }




        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFILE = new OpenFileDialog();
            //oFILE.InitialDirectory = Application.StartupPath + "\\log";
            oFILE.Filter = "文本文件|*.csv|所有文件|*.*";
            oFILE.RestoreDirectory = true;
            oFILE.FilterIndex = 1;
            if (oFILE.ShowDialog() == DialogResult.OK)
            {
                mSingleFile = oFILE.FileName;
                txtFile.Text = mSingleFile;
            }            
        }

        private void rdoFolder_CheckedChanged(object sender, EventArgs e)
        {
            SelectFilesType = 1;
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            txtFile.Text = "";
        }

        private void rdoSingleFile_CheckedChanged(object sender, EventArgs e)
        {
            SelectFilesType = 2;
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            txtFolder.Text = "";
        }

        private void btnDataToKinte_Click(object sender, EventArgs e)
        {
            if (SelectFilesType == 1)
            {
                if (mFilesInfo == null)
                {
                    MessageBox.Show("选择文件出错");
                    return;
                }

                foreach (FileInfo F_Info in mFilesInfo)
                {
                    UploadFileData(F_Info.FullName, 2);     //Kinte
                    
                }
            }
            else if (SelectFilesType == 2)
            {
                if (mSingleFile == "")
                {
                    MessageBox.Show("选择文件出错");
                    return;
                }

                UploadFileData(mSingleFile, 2);
            }


        }

    }
}
