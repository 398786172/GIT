using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WUC_LayerChannel;

namespace OCV
{
    public partial class FrmBatChoice : Form
    {
        UC_LayerChannel uc1;
        InitialSet mySet;
        ClsOCVModel myClsOCV;

        public FrmBatChoice(InitialSet set,ClsOCVModel clsOCV)
        {
            //this.Show();
            mySet = set;
            myClsOCV = clsOCV;

            int batCol = Convert.ToUInt16(mySet.TrayCol);
            int batRow = Convert.ToUInt16(mySet.TrayRow);
           
            uc1 = new UC_LayerChannel(batRow, batCol,1,1);

            InitializeComponent();
        }

        private void BatChoice_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(uc1);

            uc1.SelectMode = 0;
            uc1.Dock = DockStyle.Fill;
            uc1.UC_Resize();

            int row;
            int col;

            //初始化显示已选中的通道
            for (int i = 0; i < uc1.CellCol * uc1.CellRow; i++)
            {
                if (myClsOCV.LocalTestCH[i] == "1")
                {
                    //col = i / uc1.CellRow ;
                    //row = i % uc1.CellRow;
                    col = i % uc1.CellRow;
                    row = i / uc1.CellRow;

                    //uc1.setLblValue((col + 1).ToString() + "-" +(row + 1).ToString(), row * uc1.CellCol + col);
                    uc1.setCellSelectFlag(row * uc1.CellRow + col, 1);
                }
            }

            uc1.UC_Resize();

            for (int i = 0; i < uc1.CellCount; i++)
            {
                //uc1.setLblValue((i % uc1.CellCol + 01) + "-" + (i / uc1.CellCol + 01), i);
                //this.uc1.setLblValue((i / uc1.CellCol + 01) + "-" + (i % uc1.CellCol + 01), i);
                this.uc1.setLblValue((i / this.uc1.CellRow + 01) + "-" + (i % this.uc1.CellRow + 01), i);
            }

            this.rbSelectSingle.Checked = true;
            this.rbSelectSingle.BackColor = Color.Red;
        }

        private void rbSelectSingle_MouseUp(object sender, MouseEventArgs e)
        {
            uc1.SelectMode = 0;

            this.rbSelectSingle.BackColor = Color.Red;
            this.rbSelectMulti.BackColor = Color.WhiteSmoke;
        }

        private void rbSelectMulti_MouseUp(object sender, MouseEventArgs e)
        {
            uc1.SelectMode = 1;

            this.rbSelectSingle.BackColor = Color.WhiteSmoke;
            this.rbSelectMulti.BackColor = Color.Red;
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            manuSelect();

            this.Close();           
        }

        private void manuSelect()
        {
            //for (int i = 0; i < uc1.CellRow; i++)
            //{
            //    for (int j = 0; j < uc1.CellCol; j++)
            //    {
            //        if (uc1.CellSelectFlag(i * uc1.CellCol + j) == 1)//电池选中标志位
            //        {                   
            //            FrmOCVMain.SelectFlag[i * uc1.CellCol + j] = 1;
            //        }
            //        else
            //        {
            //            FrmOCVMain.SelectFlag[i * uc1.CellCol + j] = 0;
            //        }
            //    }
            //}
            //2014.07.11 行列顺序互换,通道序号按照托盘序号
          
            //2017-02-09 解决电池位置选择有误的BUG
            for (int i = 0; i < uc1.CellCol ; i++)
            {
                for (int j = 0; j < uc1.CellRow ; j++)
                {
                    if (uc1.CellSelectFlag(i * uc1.CellRow + j) != 0)//电池选中标志位
                    {
                        FrmOCVMain.SelectFlag[i * uc1.CellRow + j] = 1;

                        myClsOCV.LocalTestCH[i * uc1.CellRow + j] = "1";         
                    }
                    else
                    {
                        FrmOCVMain.SelectFlag[i * uc1.CellRow + j] = 0;

                        myClsOCV.LocalTestCH[i * uc1.CellRow + j] = "0";
                    }

                    mySet.ContentWriter("CH", "CH" + (i * uc1.CellRow + j + 1).ToString()
                            , myClsOCV.LocalTestCH[i * uc1.CellRow + j], myClsOCV.selCHPath);
                }
            }
        }

        private void btNG_Click(object sender, EventArgs e)
        {
            string ng;

            //2014.07.11 行列顺序互换,通道序号按照托盘序号
            for (int i = 0; i < uc1.CellCol; i++)
            {
                for (int j = 0; j < uc1.CellRow; j++)
                {
                    ng = Convert.ToString(mySet.NG(i * uc1.CellRow + j));
                    uc1.setLblValue(ng, i * uc1.CellRow + j);
                }
            }
            
        }

        private void btNGClear_Click(object sender, EventArgs e)
        {
            mySet.NG();//先清零，再重读

            string ng;

            //2014.07.11 行列顺序互换,通道序号按照托盘序号
            for (int i = 0; i < uc1.CellCol; i++)
            {
                for (int j = 0; j < uc1.CellRow; j++)
                {
                    ng = Convert.ToString(mySet.NG(i * uc1.CellRow + j));
                    uc1.setLblValue(ng, i * uc1.CellRow + j);
                }
            }

            //for (int i = 0; i < uc1.CellRow; i++)
            //{
            //    for (int j = 0; j < uc1.CellCol; j++)
            //    {
            //        ng = Convert.ToString(iS.NG(i * uc1.CellCol + j));
            //        uc1.setLblValue(ng, i * uc1.CellCol + j);
            //    }
            //}
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < uc1.CellCount; i++)
            //{
            //    FrmOCVMain.SelectFlag[i] = 1;
            //}

            this.Close();
        }
    }
}
