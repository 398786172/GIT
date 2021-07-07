using System;
using System.Collections;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace OCV
{
    public partial class FrmBatSetting : Form
    {
        public FrmBatSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            // ClsGlobal.sql = new SqLiteHelper("OCVInfo.db");

            //创建名为UserInfo的数据表

            //ClsGlobal.sql.CreateTable("UserInfo", new string[] { "UserName", "UserPwd", "UserAuthority", "SetTime" }, new string[] { "TEXT KEY NOT NULL  UNIQUE", "TEXT", "TEXT", "TEXT" });
            //ClsGlobal.sql.InsertValues("UserInfo", new string[] { "kinte", "123qweASD", "4", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            //ClsGlobal.sql.CreateTable("UserGroup", new string[] { "Rank", "UserAuthority" }, new string[] { "TEXT KEY NOT NULL  UNIQUE", "TEXT  NOT NULL  UNIQUE" });
            ////插入两条数据

            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "一级权限", "1" });
            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "二级权限", "2" });
            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "三级权限", "3" });
            //ClsGlobal.sql.InsertValues("UserGroup", new string[] { "超级权限", "4" });
            //ClsGlobal.sql.CreateTable("SettingRecord", new string[] { "OperateValue", "Operate", "UserName", "SetTime" }, new string[] { "TEXT", "TEXT", "TEXT", "TEXT" });
            //ClsGlobal.sql.CreateTable("BatModel", new string[] { "SettingName", "KeyString", "Descciption", "R_Dispiacement", "L_Dispiacement", "SetTime" }, new string[] { "TEXT KEY NOT NULL  UNIQUE", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });
            ////ClsGlobal.sql.InsertValues("BatModel", new string[] { "A", "A444#444566#89976#", "", "0", "0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            ////ClsGlobal.sql.InsertValues("BatModel", new string[] { "B", "B1112ww#444566#89976#", "", "0", "0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            ////ClsGlobal.sql.InsertValues("BatModel", new string[] { "C", "D444#444566#89976#", "", "0", "0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            //ClsGlobal.sql.CreateTable("ENBatModel", new string[] { "ModelName", "BatModel", "SetTime" }, new string[] { "TEXT KEY NOT NULL  UNIQUE", "TEXT", "TEXT"});
            //ClsGlobal.sql.InsertValues("ENBatModel", new string[] { "Model", "","" });
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.ReadFullTable("BatModel");
            ArrayList ArrModelName = new ArrayList();
            ArrayList ArrKeyString = new ArrayList();
            ArrayList ArrDescciption = new ArrayList();
            ArrayList ArrL_Dispiacement = new ArrayList();
            ArrayList ArrR_Dispiacement = new ArrayList();
            //ArrayList ArrPosition = new ArrayList();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    ArrModelName.Add(reader["SettingName"].ToString());
                    ArrKeyString.Add(reader["KeyString"].ToString());
                    ArrDescciption.Add(reader["Descciption"].ToString());
                    ArrL_Dispiacement.Add(reader["L_Dispiacement"].ToString());
                    ArrR_Dispiacement.Add(reader["R_Dispiacement"].ToString());
                    //ArrPosition.Add(reader["Position"].ToString());
                }

            }
            string ModelName = "";
            reader = ClsGlobal.sql.ReadFullTable("ENBatModel");
            reader.Read();
            if (reader.HasRows)
            {
                ModelName = reader["BatModel"].ToString();

            }
            string describe = "";
            reader = ClsGlobal.sql.ReadFullTable("LocaContr");
            reader.Read();
            if (reader.HasRows)
            {
                int Val1;
                if (int.TryParse(reader["Position"].ToString(), out Val1))
                {
                    if (Val1 == 0)
                    {
                        rdoLoca1.Checked = true;
                        rdoLoca2.Checked = false;
                        ClsGlobal.Position = 1;
                        //describe = "左右安装";
                    }
                    else
                    {
                        rdoLoca2.Checked = true;
                        rdoLoca1.Checked = false;
                        ClsGlobal.Position = 1;
                       // describe = "同方向安装";
                    }
                }
                else
                {
                    //describe = "左右安装";
                    ClsGlobal.Position = 1;
                    ClsGlobal.sql.UpdateValues("LocaContr", new string[] { "Position", "SetTime" },
                                                      new string[] { ClsGlobal.Position.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, "LocaName",
                                                      "Loca", "=");

                    rdoLoca1.Checked = true;
                    rdoLoca2.Checked = false;

                }
            }


            //OCV测试型号加载
            // ModelName = INIAPI.INIGetStringValue(ClsGlobal.mSettingPath, "System", "ModelName", null);

            //刷新提示

            if (ArrModelName.Count > 0)
            {
                try
                {
                    for (int i = 0; i < ArrModelName.Count; i++)
                    {
                        //if (ModelName == arrSection[i])
                        //{
                        struBatModelset theBatModelset = new struBatModelset();
                        //列表项参数         
                        theBatModelset.M_SettingName = ArrModelName[i].ToString();
                        theBatModelset.M_KeyString = ArrKeyString[i].ToString();
                        theBatModelset.M_Descciption = ArrDescciption[i].ToString();

                        double Val;
                        if (double.TryParse(ArrL_Dispiacement[i].ToString(), out Val))
                        {
                            theBatModelset.M_L_Dispiacement = Val;
                        }
                        else
                        {
                            theBatModelset.M_L_Dispiacement = 0;
                            ClsGlobal.sql.UpdateValues("BatModel", new string[] { "L_Dispiacement" }, new string[] { "0" }, "SettingName", ArrModelName[i].ToString().Trim(), "=");
                        }
                        if (double.TryParse(ArrR_Dispiacement[i].ToString(), out Val))
                        {
                            theBatModelset.M_R_Dispiacement = Val;
                        }
                        else
                        {
                            theBatModelset.M_R_Dispiacement = 0;
                            ClsGlobal.sql.UpdateValues("BatModel", new string[] { "R_Dispiacement" }, new string[] { "0" }, "SettingName", ArrModelName[i].ToString().Trim(), "=");
                        }

                        //int val1;
                        //if (int.TryParse(ArrPosition[i].ToString(), out val1))
                        //{
                        //    theBatModelset.M_Position = val1;
                        //}
                        //else
                        //{
                        //    theBatModelset.M_Position = 0;
                        //    ClsGlobal.sql.UpdateValues("BatModel", new string[] { "Position" }, new string[] { "0" }, "SettingName", ArrModelName[i].ToString().Trim(), "=");
                        //}




                        //  ClsGlobal.lstBatModelset.Add(theBatModelset);
                        //break;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("载入工程设定出错: " + ex.Message);
                }
            }
            for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
            {
                cmbModelName.Items.Add(ClsGlobal.lstBatModelset[i].M_SettingName);
            }
            cmbModelName.Text = ModelName;
            //刷新提示
            string Mes;
            if (ModelName == "")
            {
                Mes = "当前设备未设置使用的类别编号";
            }
            else
            {
                Mes = "当前设备使用的类别编号为：" + ModelName;
                cmbModelName.Text = ModelName;
            }

           // txt_Info.Text += Mes + "\r\n位移传感器安装位置为：" + describe;
            //  txt_Info.Text = Mes;
        }

        private void btnSavePrjPara_Click(object sender, EventArgs e)
        {

            #region 列表方式
            string theModelName;
            string theKey;
            double theL_Dispiacement;
            double theR_Dispiacement;
            struBatModelset theBatModelset = new struBatModelset();
            //--参数

            //型号名
            theModelName = cmbModelName.Text.Trim().ToString();
            if (theModelName == "")
            {
                MessageBox.Show("设备型号类别出错", "提示");
                return;
            }

            //关键字
            theKey = txtKey.Text.ToUpper(); //大写                   
            string[] arrKey = theKey.Split(new char[] { '#' });
            if (arrKey == null || arrKey.Count() == 0)
            {
                MessageBox.Show("电池型号标识字符输入出错", "提示");
                return;
            }
            else
            {
                for (int i = 0; i < arrKey.Count(); i++)
                {
                    if (arrKey[i].Trim() == "")
                    {
                        MessageBox.Show("电池型号标识字符输入出错, 请检查内容", "提示");
                        return;
                    }
                }
            }


            if (double.TryParse(txt_L_Disp.Text, out theL_Dispiacement))
            {
                if (theL_Dispiacement <= -25)
                {
                    MessageBox.Show("位移下限出错, 请检查内容", "提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("位移下限出错, 请检查内容", "提示");
                return;
            }
            if (double.TryParse(cmbBattTypecmbBattType.Text, out theR_Dispiacement))
            {
                if (theR_Dispiacement >= 25)
                {
                    MessageBox.Show("位移上限出错, 请检查内容", "提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show("位移上限出错, 请检查内容", "提示");
                return;
            }
            if (theL_Dispiacement > theR_Dispiacement)
            {
                MessageBox.Show("位移设置出错, 位移上限小于位移下限", "提示");
                return;
            }
            frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "增加类别：" + cmbModelName.Text);
            if (frmPwd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //string describe;
            //if (rdoLoca1.Checked && !rdoLoca1.Checked)
            //{
            //    theBatModelset.M_Position = 0;

            //}
            //else
            //{
            //    theBatModelset.M_Position = 1;
            //}
            //frmPwd = new frmUserPwd(PwdType.ADVCMD, "确认增加类别：" + cmbModelName.Text);
            //if (frmPwd.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}

            //列表项参数
            theBatModelset.M_SettingName = theModelName;
            theBatModelset.M_KeyString = theKey;
            theBatModelset.M_Descciption = tXtDesc.Text;
            theBatModelset.M_L_Dispiacement = theL_Dispiacement;
            theBatModelset.M_R_Dispiacement = theR_Dispiacement;
            //check列表
            bool checkModify = false;
            int theIndex = 0;
            for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
            {
                if (theModelName == ClsGlobal.lstBatModelset[i].M_SettingName)
                {
                    //theProjSet = ClsGlobal.lstProjSet[i];
                    theIndex = i;
                    checkModify = true;
                }
            }
            if (checkModify == true)
            {
                //工程存在
                if (MessageBox.Show("是否对类别[" + theModelName + "]进行修改?", "提示", MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    //check keyString                  
                    for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
                    {
                        if (theBatModelset.M_SettingName != ClsGlobal.lstBatModelset[i].M_SettingName &&
                            theBatModelset.M_KeyString == ClsGlobal.lstBatModelset[i].M_KeyString)
                        {
                            MessageBox.Show("<标识字符>不允许与其他已建类别相同", "提示");
                            return;
                        }
                    }
                    SQLiteDataReader reader = null;
                    reader = ClsGlobal.sql.UpdateValues("BatModel", new string[] { "KeyString", "Descciption", "R_Dispiacement", "L_Dispiacement", "SetTime" },
                                                                     new string[] {  theBatModelset.M_KeyString.ToString(),theBatModelset.M_Descciption,
                                                                         theBatModelset.M_R_Dispiacement.ToString(), theBatModelset.M_L_Dispiacement.ToString(),
                                                                         DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}, "SettingName", theBatModelset.M_SettingName.Trim(), "=");

                    int count = reader.RecordsAffected;
                    if (count > 0)
                    {
                        //修改
                        ClsGlobal.lstBatModelset[theIndex] = theBatModelset;
                        MessageBox.Show("类别修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("无此类别！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
            else
            {
                //新建    
                if (MessageBox.Show("确定要新建类别并保存?", "提示", MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    //check keyString                  
                    for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
                    {
                        if (theBatModelset.M_SettingName != ClsGlobal.lstBatModelset[i].M_SettingName &&
                           theBatModelset.M_KeyString == ClsGlobal.lstBatModelset[i].M_KeyString)
                        {
                            MessageBox.Show("<标识字符>不允许与其他已建型号相同", "提示");
                            return;
                        }
                    }
                    //增加
                    SQLiteDataReader reader = null;
                    reader = ClsGlobal.sql.InsertValues("BatModel", new string[] { theBatModelset.M_SettingName, theBatModelset.M_KeyString.ToString(),
                                                                         theBatModelset.M_Descciption, theBatModelset.M_R_Dispiacement.ToString(),theBatModelset.M_L_Dispiacement.ToString(),
                                                                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                    if (reader != null)
                    {
                        ClsGlobal.lstBatModelset.Add(theBatModelset);
                        //界面
                        cmbModelName.Items.Add(theBatModelset.M_SettingName);
                        MessageBox.Show("此类别保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("已存在不允许新建!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            #endregion

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            string theModelName;
            //工程名
            theModelName = cmbModelName.Text.Trim().ToString();
            if (theModelName == "")
            {
                MessageBox.Show("设备型号输入出错");
                return;
            }

            //check列表
            int theIndex = 0;
            bool theGetItem = false;
            for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
            {
                if (theModelName == ClsGlobal.lstBatModelset[i].M_SettingName)
                {
                    theIndex = i;
                    theGetItem = true;
                    break;
                }
            }
            if (theGetItem == false)
            {
                MessageBox.Show("未找到相应的类别", "提示");
                return;
            }
            else
            {
                if (MessageBox.Show("是否要删除该类别设定?", "提示", MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {

                    frmUserPwd frmPwd = new frmUserPwd(PwdType.PROCESS, "删除类别：" + cmbModelName.Text);
                    if (frmPwd.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    SQLiteDataReader reader = null;
                    reader = ClsGlobal.sql.DeleteValuesAND("UserInfo", new string[] { "BatModel" }, new string[] { theModelName.Trim() }, new string[] { "=" });
                    int count = reader.RecordsAffected;
                    if (count > 0)
                    {
                        cmbModelName.Items.Remove(theModelName);
                        txtKey.Text = "";
                        cmbModelName.Text = "";
                        tXtDesc.Text = "";
                        txt_L_Disp.Text = "";
                        cmbBattTypecmbBattType.Text = "";
                        //删除
                        ClsGlobal.lstBatModelset.Remove(ClsGlobal.lstBatModelset[theIndex]);
                        MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //  MessageBox.Show("！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void btnSetDefaultProc_Click(object sender, EventArgs e)
        {

            string theModelName;
            //工程名
            theModelName = cmbModelName.Text.Trim().ToString();
            if (theModelName == "")
            {
                MessageBox.Show("设备型号输入出错");
                return;
            }

            //check列表
            int theIndex = 0;
            bool theGetItem = false;
            for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
            {
                if (theModelName == ClsGlobal.lstBatModelset[i].M_SettingName)
                {
                    theIndex = i;
                    theGetItem = true;
                    break;
                }
            }

            if (theGetItem == false)
            {
                MessageBox.Show("未找到相应的类别，请保存类别后再设置", "提示");
                return;
            }
            frmUserPwd frmPwd;
            if ((int)MessageBox.Show("请操作人员确认是否可切换成此类别", "密码验证提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != 1)
            {
                return;
            }
            else
            {
                frmPwd = new frmUserPwd(PwdType.USER, "操作人员切换类型成" + cmbModelName.Text);
                if (frmPwd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

            }
            if ((int)MessageBox.Show("请品管/生产主管确认是否可切换成此类别", "密码验证提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != 1)
            {
                return;
            }
            else
            {
                frmPwd = new frmUserPwd(PwdType.ADVCMD, "产管理者确认类别");
                if (frmPwd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            //if ((int)MessageBox.Show("请品管确认是否可切换成此类别", "密码验证提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != 1)
            //{

            //    return;
            //}
            //else
            //{
            //    frmPwd = new frmUserPwd(PwdType.ADVCMD, "品管确认类别");
            //    if (frmPwd.ShowDialog() == DialogResult.OK)
            //    {

            //    }
            //    else
            //    {
            //        return;
            //    }  
            //}
            string describe;
            if (rdoLoca1.Checked && !rdoLoca2.Checked)
            {
                describe = "左右安装";
                ClsGlobal.Position = 1;
            }
            else
            {
                describe = "同方向安装";
                ClsGlobal.Position = 1;
            }
            SQLiteDataReader reader = null;
            reader = ClsGlobal.sql.UpdateValues("LocaContr", new string[] { "Position", "SetTime" },
                                                      new string[] { ClsGlobal.Position.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, "LocaName",
                                                      "Loca", "=");

            int count = reader.RecordsAffected;
            if (count > 0)
            {

            }
            else
            {
                MessageBox.Show("位移传感器安装位置设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            reader = ClsGlobal.sql.UpdateValues("ENBatModel", new string[] { "BatModel", "SetTime" },
                                                             new string[] { cmbModelName.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, "ModelName",
                                                             "Model", "=");

            count = reader.RecordsAffected;
            if (count > 0)
            {
                //修改
                txt_Info.Text = "当前设备测试型号为：" + cmbModelName.Text;

              //  txt_Info.Text += "\r\n位移传感器安装位置为：" + describe;
                ClsGlobal.ModelName = cmbModelName.Text;
                MessageBox.Show("类别设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("类别设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "ModelName", cmbModelName.Text);

        }

        int Check = 0;
        private void chk_EnModel_CheckedChanged(object sender, EventArgs e)
        {

            frmUserPwd frmPwd;
            if (chk_EnModel.Checked == true)
            {
                if ((int)MessageBox.Show("请验证工艺权限", "密码验证提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != 1)
                {
                    chk_EnModel.Checked = false;
                    return;
                }
                else
                {

                    frmPwd = new frmUserPwd(PwdType.PROCESS, "启用编辑功能");
                    if (frmPwd.ShowDialog() == DialogResult.OK)
                    {
                        txtKey.ReadOnly = false;
                        tXtDesc.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSavePrjPara.Enabled = true;
                        txt_L_Disp.Enabled = true;
                        cmbBattTypecmbBattType.Enabled = true;
                        cmbModelName.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    else
                    {

                        // ClsGlobal.EnBatModel = 0;
                        Check = 1;
                        chk_EnModel.Checked = false;
                        return;
                    }
                }
                //if ((int)MessageBox.Show("请主管确认", "密码验证提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != 1)
                //{
                //    chk_EnModel.Checked = false;
                //    return;
                //}
                //else
                //{
                //    frmPwd = new frmUserPwd(PwdType.ADVCMD, "确认启用编辑功能");
                //    if (frmPwd.ShowDialog() == DialogResult.OK)
                //    {
                //        //groupBox7.Enabled = true;
                //        // chk_EnModel.Checked = true;
                //        // ClsGlobal.EnBatModel = 1;
                //       // txtKey.Enabled = true;
                //        txtKey.ReadOnly = false;
                //        tXtDesc.Enabled = true;
                //        btnDelete.Enabled = true;
                //        btnSavePrjPara.Enabled = true;
                //        txt_L_Disp.Enabled = true;
                //        txt_R_Disp.Enabled = true;
                //        cmbModelName.DropDownStyle = ComboBoxStyle.DropDown;

                //    }
                //    else
                //    {
                //        Check = 1;
                //        // ClsGlobal.EnBatModel = 0;
                //        chk_EnModel.Checked = false;
                //    }
                //    //  INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "EnBatModel ", ClsGlobal.EnBatModel.ToString());
                //}

            }
            else
            {
                // txtKey.Enabled = false;
                txtKey.ReadOnly = true;
                tXtDesc.Enabled = false;
                btnDelete.Enabled = false;
                btnSavePrjPara.Enabled = false;
                txt_L_Disp.Enabled = false;
                cmbBattTypecmbBattType.Enabled = false;
                cmbModelName.DropDownStyle = ComboBoxStyle.DropDownList;
                //if (Check == 1)
                //{
                //    Check = 0;
                //    return;
                //}
                //frmUserPwd frmPwd = new frmUserPwd(PwdType.ADVCMD);
                ////PubClass.sWinTextInfo = "用户密码确认";
                //if (frmPwd.ShowDialog() == DialogResult.OK)
                //{
                //    //groupBox7.Enabled = true;
                //    // chk_EnModel.Checked = true;
                //    //ClsGlobal.EnBatModel = 0;
                //}
                //else
                //{
                //   // ClsGlobal.EnBatModel = 1;
                //    Check = 1;
                //    chk_EnModel.Checked = true;
                //}
                //INIAPI.INIWriteValue(ClsGlobal.mSettingPath, "System", "EnBatModel ", ClsGlobal.EnBatModel.ToString());

            }

        }

        private void cmbModelName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbModelName.Text == "")
            {
                return;
            }
            string theSel = cmbModelName.Text;
            int theIndex = 0;
            bool theGetItem = false;

            //查找工程
            for (int i = 0; i < ClsGlobal.lstBatModelset.Count; i++)
            {
                if (theSel == ClsGlobal.lstBatModelset[i].M_SettingName)
                {
                    theGetItem = true;
                    theIndex = i;
                    break;
                }
            }

            if (theGetItem == false)
            {
                MessageBox.Show("未找到相应的工程", "提示");
                return;
            }
            else
            {
                //cmbModelName.Text = ClsGlobal.lstBatModelset[theIndex].M_SettingName;
                txtKey.Text = ClsGlobal.lstBatModelset[theIndex].M_KeyString;
                tXtDesc.Text = ClsGlobal.lstBatModelset[theIndex].M_Descciption;
                txt_L_Disp.Text = ClsGlobal.lstBatModelset[theIndex].M_L_Dispiacement.ToString();
                cmbBattTypecmbBattType.Text = ClsGlobal.lstBatModelset[theIndex].M_R_Dispiacement.ToString();
            }
        }

    }
}
