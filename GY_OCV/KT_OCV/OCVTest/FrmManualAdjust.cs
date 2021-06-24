using DB_OCV.DAL;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;


namespace OCV
{
    public partial class FrmManualAdjust : Form
    {

        #region 电压清零

        //序号
        Label[] arrLbl_VoltZeroNo;

        //电压清零显示
        public TextBox[] arrTxt_VoltZeroSampleShow;

        //电压清零执行
        Button[] arrBtn_VoltZeroAct;

        int YBase_VoltZero = 10;
        int YIntervel_VoltZero = 25;
        int XBase_VoltZero = 30;
        ToolTip mToolTip_VoltZero = new ToolTip();

        #endregion

        #region 内阻校准

        //界面------------------------
        //序号
        Label[] arrLbl_IRAdjustNo;

        //校准时内阻值显示
        public TextBox[] arrTxt_IRSampleShow;

        //基准值写入
        public TextBox[] arrTxt_IRAdjustBase;      //通常用短路块   IRAdjustBase = 0

        //测量执行
        Button[] arrBtn_IRTestAct;

        //校准执行
        Button[] arrBtn_IRAdjustAct;

        //校准参数清0
        Button[] arrBtn_IRAdjustClr;

        int YBase_IRAdjust = 10;
        int YIntervel_IRAdjust = 25;
        int XBase_IRAdjust = 30;
        ToolTip mToolTip_IRAdjust = new ToolTip();

        #endregion

        #region 内阻计量

        //序号
        Label[] arrLbl_IRMeterNo;

        //调零时内阻值显示
        public TextBox[] arrTxt_IRMeterShow;

        //计量执行
        Button[] arrBtn_IRMeterAct;
        public Label[] arrLbl_IRMeterJudge;

        int YBase_IRMeter = 10;
        int YIntervel_IRMeter = 25;
        int XBase_IRMeter = 30;
        public ToolTip mToolTip_IRMeter = new ToolTip();


        #endregion

        #region 测温

        //界面------------------------
        //序号
        Label[] arrLbl_TempNo;
        //采样值显示
        TextBox[] arrTxt_TempSampleShow;
        //基准值写入
        TextBox[] arrTxt_TempBase;

        //校准执行
        Button[] arrBtn_TempAdjustSet;
        //清空执行
        Button[] arrBtn_TempAdjustClr;

        int YBase_Temp = 10;
        int YIntervel_Temp = 25;
        int XBase_Temp = 60;
        ToolTip mToolTip_Temp = new ToolTip();   //标签

        #endregion              

        public FrmManualAdjust()
        {
            InitializeComponent();
        }

        private void FrmOCV_Load(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                int count = 0;
                #region 温度校准界面

                btnSampleTemp.Location = new Point(105, 5);
                btnSampleTemp2.Location = new Point(425, 5);
                lblBaseAdjust1.Location = new Point(165, 10);
                lblBaseAdjust2.Location = new Point(485, 10);

                j = 0;
                count = ClsGlobal.TrayType * 2;

                arrLbl_TempNo = new Label[count];
                arrTxt_TempSampleShow = new TextBox[count];
                arrTxt_TempBase = new TextBox[count];
                arrBtn_TempAdjustSet = new Button[count];
                arrBtn_TempAdjustClr = new Button[count];

                for (int i = 0; i < count; i++)
                {
                    if (i < count / 2)
                    {
                        XBase_Temp = 50;
                        j = i;
                    }
                    else
                    {
                        XBase_Temp = 370;
                        j = i - count / 2;
                    }

                    //序号
                    arrLbl_TempNo[i] = new Label();
                    arrLbl_TempNo[i].Text = (i + 1).ToString();
                    arrLbl_TempNo[i].Font = new Font("微软雅黑", (float)9);
                    arrLbl_TempNo[i].Location = new Point(XBase_Temp, YBase_Temp + YIntervel_Temp * j);
                    arrLbl_TempNo[i].AutoSize = true;
                    panelTEMP.Controls.Add(arrLbl_TempNo[i]);


                    //TxtSample
                    arrTxt_TempSampleShow[i] = new TextBox();
                    arrTxt_TempSampleShow[i].Tag = i;
                    arrTxt_TempSampleShow[i].Width = 50;
                    arrTxt_TempSampleShow[i].ReadOnly = true;
                    arrTxt_TempSampleShow[i].Font = new Font("微软雅黑", (float)9);
                    arrTxt_TempSampleShow[i].Location = new Point(XBase_Temp + 55, YBase_Temp + YIntervel_Temp * j);
                    arrTxt_TempSampleShow[i].Name = "TxtSample" + (i + 1);
                    panelTEMP.Controls.Add(arrTxt_TempSampleShow[i]);

                    //TxtAdjust
                    arrTxt_TempBase[i] = new TextBox();
                    arrTxt_TempBase[i].Tag = i;
                    arrTxt_TempBase[i].Width = 50;
                    arrTxt_TempBase[i].Font = new Font("微软雅黑", (float)9);
                    arrTxt_TempBase[i].Location = new Point(XBase_Temp + 115, YBase_Temp + YIntervel_Temp * j);
                    arrTxt_TempBase[i].Name = "TxtAdjust" + (i + 1);
                    panelTEMP.Controls.Add(arrTxt_TempBase[i]);

                    //校准按钮
                    arrBtn_TempAdjustSet[i] = new Button();
                    arrBtn_TempAdjustSet[i].Tag = i;
                    arrBtn_TempAdjustSet[i].Width = 50;
                    arrBtn_TempAdjustSet[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_TempAdjustSet[i].Location = new Point(XBase_Temp + 175, YBase_Temp + YIntervel_Temp * j);
                    arrBtn_TempAdjustSet[i].Text = "校准";
                    arrBtn_TempAdjustSet[i].Click += new EventHandler(btnSaveTempAdjust_Single_Click);
                    panelTEMP.Controls.Add(arrBtn_TempAdjustSet[i]);

                    //清空按钮
                    arrBtn_TempAdjustClr[i] = new Button();
                    arrBtn_TempAdjustClr[i].Tag = i;
                    arrBtn_TempAdjustClr[i].Width = 50;
                    arrBtn_TempAdjustClr[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_TempAdjustClr[i].Location = new Point(XBase_Temp + 235, YBase_Temp + YIntervel_Temp * j);
                    arrBtn_TempAdjustClr[i].Text = "清0";
                    arrBtn_TempAdjustClr[i].Click += new EventHandler(btnClearTempAdjust_Single_Click);
                    panelTEMP.Controls.Add(arrBtn_TempAdjustClr[i]);

                }


                #endregion

                #region 电压清零界面

                XBase_VoltZero = 30;
                lblVoltZeroVal1.Location = new Point(XBase_VoltZero + 30, 10);


                XBase_VoltZero = 370;
                lblVoltZeroVal2.Location = new Point(XBase_VoltZero + 30, 10);


                j = 0;
                count = ClsGlobal.TrayType;

                arrLbl_VoltZeroNo = new Label[count];
                arrTxt_VoltZeroSampleShow = new TextBox[count];

                arrBtn_VoltZeroAct = new Button[count];


                for (int i = 0; i < count; i++)
                {
                    if (i < count / 2)
                    {
                        XBase_VoltZero = 30;
                        j = i;
                    }
                    else
                    {
                        XBase_VoltZero = 370;
                        j = i - count / 2;
                    }

                    //序号
                    arrLbl_VoltZeroNo[i] = new Label();
                    arrLbl_VoltZeroNo[i].Text = (i + 1).ToString();
                    arrLbl_VoltZeroNo[i].Font = new Font("微软雅黑", (float)9);
                    arrLbl_VoltZeroNo[i].Location = new Point(XBase_VoltZero, YBase_VoltZero + YIntervel_VoltZero * j);
                    arrLbl_VoltZeroNo[i].AutoSize = true;
                    panelVoltZero.Controls.Add(arrLbl_VoltZeroNo[i]);

                    //TxtSample
                    arrTxt_VoltZeroSampleShow[i] = new TextBox();
                    arrTxt_VoltZeroSampleShow[i].Tag = i;
                    arrTxt_VoltZeroSampleShow[i].Width = 100;
                    arrTxt_VoltZeroSampleShow[i].ReadOnly = true;
                    arrTxt_VoltZeroSampleShow[i].Font = new Font("微软雅黑", (float)9);
                    arrTxt_VoltZeroSampleShow[i].Location = new Point(XBase_VoltZero + 30, YBase_VoltZero + YIntervel_VoltZero * j);
                    arrTxt_VoltZeroSampleShow[i].Name = "TxtSample" + (i + 1);
                    panelVoltZero.Controls.Add(arrTxt_VoltZeroSampleShow[i]);


                    //测试按钮
                    arrBtn_VoltZeroAct[i] = new Button();
                    arrBtn_VoltZeroAct[i].Tag = i;
                    arrBtn_VoltZeroAct[i].Width = 50;
                    arrBtn_VoltZeroAct[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_VoltZeroAct[i].Location = new Point(XBase_VoltZero + 145, YBase_VoltZero + YIntervel_VoltZero * j);
                    arrBtn_VoltZeroAct[i].Text = "清零";
                    arrBtn_VoltZeroAct[i].Click += new EventHandler(btnVoltZero_Single_Click);
                    panelVoltZero.Controls.Add(arrBtn_VoltZeroAct[i]);


                }

                #endregion

                #region 内阻校准 

                XBase_IRAdjust = 10;
                lblIRTestVal1.Location = new Point(XBase_IRAdjust + 40, 10);
                lblIRBaseVal1.Location = new Point(XBase_IRAdjust + 135, 10);

                XBase_IRAdjust = 390;
                lblIRTestVal2.Location = new Point(XBase_IRAdjust + 40, 10);
                lblIRBaseVal2.Location = new Point(XBase_IRAdjust + 135, 10);

                j = 0;
                count = ClsGlobal.TrayType;

                arrLbl_IRAdjustNo = new Label[count];
                arrTxt_IRSampleShow = new TextBox[count];
                arrTxt_IRAdjustBase = new TextBox[count];
                arrBtn_IRTestAct = new Button[count];
                arrBtn_IRAdjustAct = new Button[count];
                arrBtn_IRAdjustClr = new Button[count];


                for (int i = 0; i < count; i++)
                {
                    if (i < count / 2)
                    {
                        XBase_IRAdjust = 10;
                        j = i;
                    }
                    else
                    {
                        XBase_IRAdjust = 390;
                        j = i - count / 2;
                    }

                    //序号
                    arrLbl_IRAdjustNo[i] = new Label();
                    arrLbl_IRAdjustNo[i].Text = (i + 1).ToString();
                    arrLbl_IRAdjustNo[i].Font = new Font("微软雅黑", (float)9);
                    arrLbl_IRAdjustNo[i].Location = new Point(XBase_IRAdjust, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    arrLbl_IRAdjustNo[i].AutoSize = true;
                    panelIRAdjust.Controls.Add(arrLbl_IRAdjustNo[i]);

                    //TxtSample
                    arrTxt_IRSampleShow[i] = new TextBox();
                    arrTxt_IRSampleShow[i].Tag = i;
                    arrTxt_IRSampleShow[i].Width = 100;
                    arrTxt_IRSampleShow[i].ReadOnly = true;
                    arrTxt_IRSampleShow[i].Font = new Font("微软雅黑", (float)9);
                    arrTxt_IRSampleShow[i].Location = new Point(XBase_IRAdjust + 30, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    arrTxt_IRSampleShow[i].Name = "TxtSample" + (i + 1);
                    panelIRAdjust.Controls.Add(arrTxt_IRSampleShow[i]);

                    //TxtAdjust
                    arrTxt_IRAdjustBase[i] = new TextBox();
                    arrTxt_IRAdjustBase[i].Tag = i;
                    arrTxt_IRAdjustBase[i].Width = 50;
                    arrTxt_IRAdjustBase[i].Font = new Font("微软雅黑", (float)9);
                    arrTxt_IRAdjustBase[i].Location = new Point(XBase_IRAdjust + 135, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    arrTxt_IRAdjustBase[i].Name = "TxtAdjust" + (i + 1);
                    arrTxt_IRAdjustBase[i].Text = "0";
                    panelIRAdjust.Controls.Add(arrTxt_IRAdjustBase[i]);

                    //测试按钮
                    arrBtn_IRTestAct[i] = new Button();
                    arrBtn_IRTestAct[i].Tag = i;
                    arrBtn_IRTestAct[i].Width = 50;
                    arrBtn_IRTestAct[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_IRTestAct[i].Location = new Point(XBase_IRAdjust + 195, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    arrBtn_IRTestAct[i].Text = "测试";
                    arrBtn_IRTestAct[i].Click += new EventHandler(btnTestIR_Single_Click);
                    panelIRAdjust.Controls.Add(arrBtn_IRTestAct[i]);

                    //校准按钮
                    arrBtn_IRAdjustAct[i] = new Button();
                    arrBtn_IRAdjustAct[i].Tag = i;
                    arrBtn_IRAdjustAct[i].Width = 50;
                    arrBtn_IRAdjustAct[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_IRAdjustAct[i].Location = new Point(XBase_IRAdjust + 255, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    arrBtn_IRAdjustAct[i].Text = "校准";
                    arrBtn_IRAdjustAct[i].Click += new EventHandler(btnSaveIRAdjust_Single_Click);
                    panelIRAdjust.Controls.Add(arrBtn_IRAdjustAct[i]);

                    //清空按钮
                    arrBtn_IRAdjustClr[i] = new Button();
                    arrBtn_IRAdjustClr[i].Tag = i;
                    arrBtn_IRAdjustClr[i].Width = 50;
                    arrBtn_IRAdjustClr[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_IRAdjustClr[i].Location = new Point(XBase_IRAdjust + 315, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    arrBtn_IRAdjustClr[i].Text = "清0";
                    arrBtn_IRAdjustClr[i].Click += new EventHandler(btnClearIRAdjust_Single_Click);
                    panelIRAdjust.Controls.Add(arrBtn_IRAdjustClr[i]);

                }


                #endregion

                #region 内阻计量

                XBase_IRMeter = 30;
                lblIRMeterVal1.Location = new Point(XBase_IRMeter + 30, 10);

                XBase_IRMeter = 370;
                lblIRMeterVal2.Location = new Point(XBase_IRMeter + 30, 10);

                j = 0;
                count = ClsGlobal.TrayType;

                arrLbl_IRMeterNo = new Label[count];
                arrTxt_IRMeterShow = new TextBox[count];
                //arrTxt_IRAdjustBase = new TextBox[count];
                arrBtn_IRMeterAct = new Button[count];

                arrLbl_IRMeterJudge = new Label[count];


                for (int i = 0; i < count; i++)
                {
                    if (i < count / 2)
                    {
                        XBase_IRMeter = 30;
                        j = i;
                    }
                    else
                    {
                        XBase_IRMeter = 370;
                        j = i - count / 2;
                    }

                    //序号
                    arrLbl_IRMeterNo[i] = new Label();
                    arrLbl_IRMeterNo[i].Text = (i + 1).ToString();
                    arrLbl_IRMeterNo[i].Font = new Font("微软雅黑", (float)9);
                    arrLbl_IRMeterNo[i].Location = new Point(XBase_IRMeter, YBase_IRMeter + YIntervel_IRMeter * j);
                    arrLbl_IRMeterNo[i].AutoSize = true;
                    panelIRMetering.Controls.Add(arrLbl_IRMeterNo[i]);

                    //TxtSample
                    arrTxt_IRMeterShow[i] = new TextBox();
                    arrTxt_IRMeterShow[i].Tag = i;
                    arrTxt_IRMeterShow[i].Width = 100;
                    arrTxt_IRMeterShow[i].ReadOnly = true;
                    arrTxt_IRMeterShow[i].Font = new Font("微软雅黑", (float)9);
                    arrTxt_IRMeterShow[i].Location = new Point(XBase_IRMeter + 30, YBase_IRMeter + YIntervel_IRMeter * j);
                    arrTxt_IRMeterShow[i].Name = "TxtSample" + (i + 1);
                    panelIRMetering.Controls.Add(arrTxt_IRMeterShow[i]);

                    ////TxtAdjust
                    //arrTxt_IRAdjustBase[i] = new TextBox();
                    //arrTxt_IRAdjustBase[i].Tag = i;
                    //arrTxt_IRAdjustBase[i].Width = 50;
                    //arrTxt_IRAdjustBase[i].Font = new Font("微软雅黑", (float)9);
                    //arrTxt_IRAdjustBase[i].Location = new Point(XBase_IRAdjust + 85, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    //arrTxt_IRAdjustBase[i].Name = "TxtAdjust" + (i + 1);
                    //panelIRMetering.Controls.Add(arrTxt_IRAdjustBase[i]);

                    //测试按钮
                    arrBtn_IRMeterAct[i] = new Button();
                    arrBtn_IRMeterAct[i].Tag = i;
                    arrBtn_IRMeterAct[i].Width = 50;
                    arrBtn_IRMeterAct[i].Font = new Font("微软雅黑", (float)9);
                    arrBtn_IRMeterAct[i].Location = new Point(XBase_IRMeter + 145, YBase_IRMeter + YIntervel_IRMeter * j);
                    arrBtn_IRMeterAct[i].Text = "计量";
                    arrBtn_IRMeterAct[i].Click += new EventHandler(btnIRMeter_Single_Click);
                    panelIRMetering.Controls.Add(arrBtn_IRMeterAct[i]);

                    //结果判断
                    arrLbl_IRMeterJudge[i] = new Label();
                    arrLbl_IRMeterJudge[i].Text = "";
                    arrLbl_IRMeterJudge[i].Font = new Font("微软雅黑", (float)9);
                    arrLbl_IRMeterJudge[i].Location = new Point(XBase_IRMeter + 200, YBase_IRMeter + YIntervel_IRMeter * j);
                    arrLbl_IRMeterJudge[i].AutoSize = false;
                    arrLbl_IRMeterJudge[i].Width = 100;
                    arrLbl_IRMeterJudge[i].BorderStyle = BorderStyle.Fixed3D;
                    panelIRMetering.Controls.Add(arrLbl_IRMeterJudge[i]);

                    ////校准按钮
                    //arrBtn_IRAdjustAct[i] = new Button();
                    //arrBtn_IRAdjustAct[i].Tag = i;
                    //arrBtn_IRAdjustAct[i].Width = 50;
                    //arrBtn_IRAdjustAct[i].Font = new Font("微软雅黑", (float)9);
                    //arrBtn_IRAdjustAct[i].Location = new Point(XBase_IRMeter + 205, YBase_IRMeter + YIntervel_IRMeter * j);
                    //arrBtn_IRAdjustAct[i].Text = "校准";
                    //arrBtn_IRAdjustAct[i].Click += new EventHandler(btnSaveIRAdjust_Single_Click);
                    //panelIRAdjust.Controls.Add(arrBtn_IRAdjustAct[i]);

                    ////清空按钮
                    //arrBtn_IRAdjustClr[i] = new Button();
                    //arrBtn_IRAdjustClr[i].Tag = i;
                    //arrBtn_IRAdjustClr[i].Width = 50;
                    //arrBtn_IRAdjustClr[i].Font = new Font("微软雅黑", (float)9);
                    //arrBtn_IRAdjustClr[i].Location = new Point(XBase_IRAdjust + 265, YBase_IRAdjust + YIntervel_IRAdjust * j);
                    //arrBtn_IRAdjustClr[i].Text = "清0";
                    //arrBtn_IRAdjustClr[i].Click += new EventHandler(btnClearTempAdjust_Single_Click);
                    //panelIRAdjust.Controls.Add(arrBtn_IRAdjustClr[i]);

                }

                #endregion

            }
            catch (Exception ex)
            {

            }
        }

        private void FrmManualTestOCV_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        private void tim_UI_Tick(object sender, EventArgs e)
        {
            if (ClsGlobal.OCVTestContr == null)
            {
                return;
            }
            if (ClsGlobal.OCVTestContr.ManualTestFinish == true)
            {

            }
        }

        #region 电压清零处理

        //单点电压清零功能
        private void btnVoltZero_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;
            double theVoltSample;
            double theAdjusVolt;
            try
            {
                Num = (int)theBtn.Tag + 1;
                if (ClsGlobal.Switch_Count == 3)
                {
                    if (Num <= 13)
                    {
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(1, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(1, 0);
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(1, Num);   //正极对负极
                    }
                    if (Num >= 14 && Num <= 26)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(1, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(1, 0);
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(1, Num - 13);   //正极对负极
                    }
                    if (Num >= 27)
                    {
                        ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(1, 0);
                        ClsGlobal.OCVTestContr.SWControl[1].ChannelVoltSwitchContr(1, 0);
                        ClsGlobal.OCVTestContr.SWControl[2].ChannelVoltSwitchContr(1, Num - 26);   //正极对负极
                    }

                }
                else
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(1, 0);
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelVoltSwitchContr(1, Num);   //正极对负极
                }
                Thread.Sleep(50);
                ClsGlobal.OCVTestContr.DMM_Ag344X.ReadVolt(out theVoltSample);

                //计算得到校准值
                theAdjusVolt = 0 - theVoltSample * 1000;

                //值显示
                arrTxt_VoltZeroSampleShow[Num - 1].Text = (theAdjusVolt).ToString("F4");

                //更改和保存校准值
                INIAPI.INIWriteValue(ClsGlobal.mVoltAdjustPath, "VoltAdjust", "CH" + (Num), theAdjusVolt.ToString("F4"));
                //  ClsGlobal.ArrVoltAdjustPara[Num - 1] = theAdjusVolt.ToString("F4");
                lblNote_VoltZero.Text = ("[通道" + (Num) + "]:\r\n" + "完成电压校准");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //所有点电压清零
        private void btnVoltZeroAllStart_Click(object sender, EventArgs e)
        {

            try
            {
                btnVoltZeroAllStart.Text = "电压清零中...";

                ClsGlobal.OCVTestContr.StartManualVoltZero_Action(this);

                lblNote_VoltZero.Text = ("全部通道电压清零完成!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //停止所有点电压清零
        private void btnVoltZeroAllStop_Click(object sender, EventArgs e)
        {
            ClsGlobal.OCVTestContr.StopManualVoltZero_Meter();
        }

        //保存清零结果
        private void SaveVoltZeroResult_csv(out string filesAddr)
        {
            string addr1 = "\\VoltZeroFiles\\" + "VoltZero" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            string addr = Application.StartupPath + addr1;

            StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default);

            SWR.WriteLine("通道号, 清零电压(mV)");

            //写入数据            
            for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
            {
                SWR.WriteLine((Num + 1).ToString() + "," + arrTxt_VoltZeroSampleShow[Num].Text.ToString());
            }

            SWR.Close();

            filesAddr = addr1;
        }



        private void btnSaveVoltZeroResult_Click(object sender, EventArgs e)
        {
            string FilesAddr;

            SaveVoltZeroResult_csv(out FilesAddr);
            lblNote_VoltZero.Text = "已保存电压清零结果到:" + FilesAddr;
        }

        //获得电压校准值
        private string[] GetVoltAdjust(string Path)
        {
            int CountNum;
            string[] ArrAdjustVolt;
            string count = INIAPI.INIGetStringValue(Path, "VoltAdjust", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                ArrAdjustVolt = new string[CountNum];

                for (int i = 0; i < ArrAdjustVolt.Count(); i++)
                {
                    ArrAdjustVolt[i] = INIAPI.INIGetStringValue(Path, "VoltAdjust", "CH" + (i + 1).ToString(), null);
                }
                return ArrAdjustVolt;
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return null;
            }

        }

        #endregion

        #region 内阻校准处理



        //单点内阻测试功能
        private void btnTestIR_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;
            double theIRSample = 0;

            try
            {
                //切换
                Num = (int)theBtn.Tag + 1;
                if (Num <= 13)
                {
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, Num);
                    //内阻采样
                    Thread.Sleep(50);
                    ClsGlobal.OCVTestContr.HIOKI4560[0].ReadRData(out theIRSample);
                }
                if (Num >= 14 && Num <= 26)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, Num - 13);
                    //内阻采样
                    Thread.Sleep(50);
                    ClsGlobal.OCVTestContr.HIOKI4560[1].ReadRData(out theIRSample);
                }
                if (Num >= 27)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, Num - 26);
                    //内阻采样
                    Thread.Sleep(50);
                    ClsGlobal.OCVTestContr.HIOKI4560[2].ReadRData(out theIRSample);
                }
                //显示阻值
                arrTxt_IRSampleShow[Num - 1].Text = (theIRSample * 1000).ToString("F3") + "/" +
                    (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num - 1])).ToString("F3");

                if (double.Parse(ClsGlobal.mIRAdjustVal[Num - 1]) != 0)
                {
                    arrTxt_IRSampleShow[Num - 1].BackColor = Color.LightGreen;
                    mToolTip_IRAdjust.SetToolTip(arrTxt_IRSampleShow[Num - 1], "该采样值经过校准");
                }
                else
                {
                    arrTxt_IRSampleShow[Num - 1].BackColor = Color.LightGray;
                    mToolTip_IRAdjust.SetToolTip(arrTxt_IRSampleShow[Num - 1], "该采样值没有经过校准");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        //单点内阻校准功能
        private void btnSaveIRAdjust_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;
            double theBaseVal;
            double theAdjust;
            double theIRSample = 0;

            try
            {
                //得到用户输入的基准值
                Num = (int)theBtn.Tag + 1;
                if (double.TryParse(arrTxt_IRAdjustBase[Num - 1].Text, out theBaseVal) == false)
                {
                    lblNote_IRAdjust.Text = ("[通道" + (Num) + "]:\r\n" + "请先填入正确的内阻基准值，再进行校准");
                    return;
                }
                //内阻采样
                if (Num <= 13)
                {
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, Num);
                    //内阻采样
                    Thread.Sleep(100);
                    ClsGlobal.OCVTestContr.HIOKI4560[0].ReadRData(out theIRSample);
                }
                if (Num >= 14 && Num <= 26)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, Num - 13);
                    //内阻采样
                    Thread.Sleep(100);
                    ClsGlobal.OCVTestContr.HIOKI4560[1].ReadRData(out theIRSample);
                }
                if (Num >= 27)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, Num - 26);
                    //内阻采样
                    Thread.Sleep(100);
                    ClsGlobal.OCVTestContr.HIOKI4560[2].ReadRData(out theIRSample);
                }


                if (theIRSample > 100)
                {
                    lblNote_IRAdjust.Text = ("[通道" + (Num) + "]:\r\n" + "测得阻值>100mΩ,校准失败!");
                    return;
                }

                //计算得到校准值
                theAdjust = theBaseVal - theIRSample * 1000;

                //更改和保存校准值
                INIAPI.INIWriteValue(ClsGlobal.mIRAdjustPath, "ACIRAdjust", "CH" + (Num), theAdjust.ToString("F3"));
                ClsGlobal.mIRAdjustVal[Num - 1] = theAdjust.ToString();
                lblNote_IRAdjust.Text = ("[通道" + (Num) + "]:\r\n" + "完成内阻校准");

                //显示校准后的阻值
                arrTxt_IRSampleShow[Num - 1].BackColor = Color.LightGreen;
                //arrTxt_IRSampleShow[Num].Text = (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[ActualNum - 1])).ToString("F3");

                arrTxt_IRSampleShow[Num - 1].Text = (theIRSample * 1000).ToString("F3") + "/" +
                    (theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num - 1])).ToString("F3");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //单点内阻校准值清0
        private void btnClearIRAdjust_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;

            try
            {
                //实际对应通道
                Num = (int)theBtn.Tag + 1;

                //更改和清0校准值
                INIAPI.INIWriteValue(ClsGlobal.mTempAdjustPath, "ACIRAdjust", "CH" + (Num), "0");
                ClsGlobal.mIRAdjustVal[Num - 1] = "0";
                lblNote_IRAdjust.Text = ("[通道" + (Num) + "]:\r\n" + "内阻校准值已清0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void UpdateUIText(Button btn, string text)
        {
            Action act = () =>
              {
                  btn.Text = text;
              };
            if (this.InvokeRequired)
            {
                Invoke(act);
            }
            else
            {
                act();
            }
        }

        //所有点进行采样测试, 并非校准
        private void btnIRAdjustSampleAllStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnIRAdjustSampleAllStart.Text = "测试中...";

                ClsGlobal.OCVTestContr.StartManualIRAdjust_Test_Action(this, () => UpdateUIText(btnIRAdjustSampleAllStart, "所有通道测试"));

                lblNote_IRAdjust.Text = ("全部通道测试完成!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //所有点进行内阻校准
        private void btnIRAdjustAllStart_Click(object sender, EventArgs e)
        {

            try
            {
                btnIRAdjustAllStart.Text = "内阻校准中...";

                ClsGlobal.OCVTestContr.StartManualIRAdjust_Action(this, () => UpdateUIText(btnIRAdjustAllStart, "所有通道校准"));

                lblNote_IRAdjust.Text = ("全部通道内阻校准完成!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //停止所有点内阻校准
        private void btnIRAdjustAllStop_Click(object sender, EventArgs e)
        {
            ClsGlobal.OCVTestContr.StopManualIR_Adjust_Meter();
        }

        //所有点内阻校准值清0
        private void btnIRAdjustAllValClr_Click(object sender, EventArgs e)
        {
            for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
            {

                //更改和清0校准值
                INIAPI.INIWriteValue(ClsGlobal.mTempAdjustPath, "ACIRAdjust", "CH" + (Num + 1), "0");
                ClsGlobal.mIRAdjustVal[Num] = "0";

            }

            lblNote_IRAdjust.Text = ("全部通道内阻校准值已清0");
        }


        #endregion

        #region 内阻计量处理

        //单点计量功能
        private void btnIRMeter_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;
            double theMeterVal;
            double MeterErrRange;
            double theIRSample = 0;
            double theIR;


            try
            {
                Num = (int)theBtn.Tag + 1;

                //已输入计量值
                if (double.TryParse(txtIRSet_Metering.Text, out theMeterVal) == false)
                {
                    lblNote_IRMetering.Text = ("请先填入正确的计量值，再进行计量");
                    return;
                }

                if (double.TryParse(txtIRMeterErrRange.Text, out MeterErrRange) == false)
                {
                    lblNote_IRMetering.Text = ("请先填入正确的计量误差值，再进行计量");
                    return;
                }

                //内阻采样
                if (Num <= 13)
                {
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, Num);
                    //内阻采样
                    Thread.Sleep(100);
                    ClsGlobal.OCVTestContr.HIOKI4560[0].ReadRData(out theIRSample);
                }
                if (Num >= 14 && Num <= 26)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, Num - 13);
                    //内阻采样
                    Thread.Sleep(100);
                    ClsGlobal.OCVTestContr.HIOKI4560[1].ReadRData(out theIRSample);
                }
                if (Num >= 27)
                {
                    ClsGlobal.OCVTestContr.SWControl[0].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[1].ChannelAcirSwitchContr(2, 0);
                    ClsGlobal.OCVTestContr.SWControl[2].ChannelAcirSwitchContr(2, Num - 13);
                    //内阻采样
                    Thread.Sleep(100);
                    ClsGlobal.OCVTestContr.HIOKI4560[2].ReadRData(out theIRSample);
                }


                theIR = theIRSample * 1000 + double.Parse(ClsGlobal.mIRAdjustVal[Num - 1]);  //经过adjust

                //计量测试
                if (Math.Abs(theMeterVal - theIR) <= MeterErrRange)
                {
                    arrTxt_IRMeterShow[Num - 1].Text = theIR.ToString("F3");
                    arrTxt_IRMeterShow[Num - 1].BackColor = Color.LightGreen;
                    lblNote_IRMetering.Text = ("[通道" + (Num) + "]:\r\n" + "计量通过");
                }
                else
                {
                    arrTxt_IRMeterShow[Num - 1].Text = theIR.ToString("F3");
                    arrTxt_IRMeterShow[Num - 1].BackColor = Color.Red;
                    lblNote_IRMetering.Text = ("[通道" + (Num) + "]:\r\n" + "计量不通过!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //所有点进行内阻计量
        private void btnIRMeteringAllStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnIRMeteringAllStart.Text = "内阻计量中...";

                ClsGlobal.OCVTestContr.StartManualIRMetering_Action(this, () => UpdateUIText(btnIRMeteringAllStart, "所有通道计量"));

                lblNote_IRMetering.Text = ("全部通道内阻计量完成!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //停止所有点进行内阻计量
        private void btnIRMeteringAllStop_Click(object sender, EventArgs e)
        {
            ClsGlobal.OCVTestContr.StopManualIR_Adjust_Meter();
        }


        //保存清零结果
        private void SaveIRMeteringResult_csv(out string filesAddr)
        {
            string addr1 = "\\IRMeteringFiles\\" + "IRMetering" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            string addr = Application.StartupPath + addr1;

            StreamWriter SWR = new StreamWriter(addr, false, Encoding.Default);

            SWR.WriteLine("工装阻值(mΩ) = " + txtIRSet_Metering.Text.Trim() + " , 误差范围(mΩ) = " + txtIRMeterErrRange.Text.Trim());

            SWR.WriteLine("通道号, 计量内阻值(mΩ),结果");

            //写入数据            
            for (int Num = 0; Num < ClsGlobal.TrayType; Num++)
            {
                SWR.WriteLine((Num + 1).ToString() + "," + arrTxt_VoltZeroSampleShow[Num].Text.ToString() + "," +
                    arrLbl_IRMeterJudge[Num].Text.ToString());
            }

            SWR.Close();

            filesAddr = addr1;
        }



        private void btnIRMeteringSave_Click(object sender, EventArgs e)
        {
            string FilesAddr;

            SaveIRMeteringResult_csv(out FilesAddr);
            lblNote_IRMetering.Text = "已保存内阻计量结果到:" + FilesAddr;
        }


        #endregion 

        #region 温度校准处理

        //全部温度校准值清0
        public void ClearAllTempAdjust(string Path)
        {
            int CountNum;
            //正极
            string count = INIAPI.INIGetStringValue(Path, "TempAdjust_P", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                for (int i = 0; i < CountNum; i++)
                {
                    INIAPI.INIWriteValue(Path, "TempAdjust_P", "CH" + (i + 1).ToString(), "0");
                    ClsGlobal.mTempAdjustVal_P[i] = "0";
                }
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return;
            }
            //负极
            count = INIAPI.INIGetStringValue(Path, "TempAdjust_N", "chCount", null);
            if (int.TryParse(count, out CountNum) == true)
            {
                for (int i = 0; i < CountNum; i++)
                {
                    INIAPI.INIWriteValue(Path, "TempAdjust_N", "CH" + (i + 1).ToString(), "0");
                    ClsGlobal.mTempAdjustVal_N[i] = "0";
                }
            }
            else
            {
                MessageBox.Show("{0}文件不存在", Path);
                return;
            }
        }


        private void btnSaveTempAdjust_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;
            double theBaseVal;
            double theAdjust;
            double[] theTEMPSample;

            try
            {
                //得到用户输入的基准值
                Num = (int)theBtn.Tag;
                if (double.TryParse(arrTxt_TempBase[Num].Text, out theBaseVal) == false)
                {
                    lblNote_TEMP.Text = ("[通道" + (Num + 1) + "]:\r\n" + "请先填入正确的温度基准值，再进行校准");
                    return;
                }


                int ActualNum = 0;
                if (Num < 38)
                {
                    //实际对应通道, 注:Num从零开始,ActualNum从1开始
                    ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[Num]);
                    theAdjust = theBaseVal - ClsGlobal.TempContr.Anodetemperature[ActualNum - 1];
                    //更改和保存校准值
                    INIAPI.INIWriteValue(ClsGlobal.mTempAdjustPath, "TempAdjust_P", "CH" + (ActualNum), theAdjust.ToString("F1"));
                    ClsGlobal.mTempAdjustVal_P[ActualNum - 1] = theAdjust.ToString();

                }
                else
                {
                    //实际对应通道, 注:Num从零开始,ActualNum从1开始
                    ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_N[Num]);
                    theAdjust = theBaseVal - ClsGlobal.TempContr.Poletemperature[ActualNum - 1];
                    //更改和保存校准值
                    INIAPI.INIWriteValue(ClsGlobal.mTempAdjustPath, "TempAdjust_N", "CH" + (ActualNum), theAdjust.ToString("F1"));
                    ClsGlobal.mTempAdjustVal_N[ActualNum - 1] = theAdjust.ToString();
                }






                lblNote_TEMP.Text = ("[通道" + (Num + 1) + "]:\r\n" + "完成温度校准");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearTempAdjust_Single_Click(object sender, EventArgs e)
        {
            Button theBtn = (Button)sender;
            int Num;
            try
            {
                //得到对应的校准值号
                Num = (int)theBtn.Tag;
                if (Num < 38)
                {
                    //实际对应通道
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[Num]);
                    //更改和清0校准值
                    INIAPI.INIWriteValue(ClsGlobal.mTempAdjustPath, "TempAdjust_P", "CH" + (Num + 1), "0");
                    ClsGlobal.mTempAdjustVal_P[ActualNum - 1] = "0";
                    lblNote_TEMP.Text = ("[通道" + (Num + 1) + "]:\r\n" + "温度校准值已清0");
                }
                else
                {
                    //实际对应通道
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_N[Num]);
                    //更改和清0校准值
                    INIAPI.INIWriteValue(ClsGlobal.mTempAdjustPath, "TempAdjust_N", "CH" + (Num + 1), "0");
                    ClsGlobal.mTempAdjustVal_N[ActualNum - 1] = "0";
                    lblNote_TEMP.Text = ("[通道" + (Num + 1) + "]:\r\n" + "温度校准值已清0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btnSampleTemp_Click(object sender, EventArgs e)
        {
            double[] theTEMPSample;
            try
            {
                //更新界面
                int Val = 0;
                Val = ClsGlobal.TrayType;

                for (int i = 0; i < Val; i++)
                {
                    //正极
                    int ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_P[i]);//实际对应通道    
                    arrTxt_TempSampleShow[i].Text = (ClsGlobal.TempContr.Anodetemperature[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1])).ToString();

                    if (double.Parse(ClsGlobal.mTempAdjustVal_P[ActualNum - 1]) != 0)
                    {
                        arrTxt_TempSampleShow[i].BackColor = Color.LightGreen;
                        mToolTip_Temp.SetToolTip(arrTxt_TempSampleShow[i], "该采样值经过校准");
                    }
                    else
                    {
                        arrTxt_TempSampleShow[i].BackColor = Color.LightGray;
                        mToolTip_Temp.SetToolTip(arrTxt_TempSampleShow[i], "该采样值没有经过校准");
                    }

                    //负极
                    ActualNum = Convert.ToUInt16(ClsGlobal.mSwitchCHTemp_N[i]);       //实际对应通道
                    arrTxt_TempSampleShow[i + 38].Text = (ClsGlobal.TempContr.Poletemperature[ActualNum - 1] + double.Parse(ClsGlobal.mTempAdjustVal_N[ActualNum - 1])).ToString();

                    if (double.Parse(ClsGlobal.mTempAdjustVal_N[ActualNum - 1]) != 0)
                    {
                        arrTxt_TempSampleShow[i + 38].BackColor = Color.LightGreen;
                        mToolTip_Temp.SetToolTip(arrTxt_TempSampleShow[i], "该采样值经过校准");
                    }
                    else
                    {
                        arrTxt_TempSampleShow[i + 38].BackColor = Color.LightGray;
                        mToolTip_Temp.SetToolTip(arrTxt_TempSampleShow[i], "该采样值没有经过校准");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetAllTempAdjust_Click(object sender, EventArgs e)
        {
            double Res;

            try
            {
                if (double.TryParse(txtTempBase_adjust.Text, out Res) == true)
                {
                    INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "TempSetting", "TempBase", Res.ToString());
                    ClsGlobal.TempBase = Res;
                }
                else
                {
                    lblNote_TEMP.Text = ("[整体操作]:\r\n" + "请先填入正确的温度基准，再进行校准");
                    return;
                }

                SetAllTempAdjust(ClsGlobal.mTempAdjustPath);
                lblNote_TEMP.Text = ("[整体操作]:\r\n" + "全部温度校准设置完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //设置全部温度校准值
        public void SetAllTempAdjust(string Path)
        {
            double[] theTEMPSample;
            double[] ArrTempAdjust = new double[88];



            //输出校准值
            for (int i = 0; i < ClsGlobal.TrayType; i++)
            {
                //正极
                ArrTempAdjust[i] = (ClsGlobal.TempBase - ClsGlobal.TempContr.Anodetemperature[i]);        //计算方法
                INIAPI.INIWriteValue(Path, "TempAdjust_P", "CH" + (i + 1).ToString(), ArrTempAdjust[i].ToString("F1"));
                ClsGlobal.mTempAdjustVal_P[i] = ArrTempAdjust[i].ToString();

                //负极
                ArrTempAdjust[i] = (ClsGlobal.TempBase - ClsGlobal.TempContr.Poletemperature[i]);        //计算方法
                INIAPI.INIWriteValue(Path, "TempAdjust_N", "CH" + (i + 1).ToString(), ArrTempAdjust[i].ToString("F1"));
                ClsGlobal.mTempAdjustVal_N[i] = ArrTempAdjust[i].ToString();
            }

        }


        private void btnClrAllTempAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("全部校准值将被清空,是否继续?", "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                ClearAllTempAdjust(ClsGlobal.mTempAdjustPath);
                lblNote_TEMP.Text = ("[整体操作]:\r\n" + "全部温度校准值已清0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #endregion

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
