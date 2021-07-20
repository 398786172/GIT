﻿namespace OCV
{
    partial class FormPorcessSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPorcessSet));
            this.btnSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbConfigName = new System.Windows.Forms.ComboBox();
            this.btnDelBattType = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.chb_ENVoltDrop = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ckB_IS_Enable_ACIRRange = new System.Windows.Forms.CheckBox();
            this.ckB_IS_Enable_DropRange = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdb_ACIR_Min = new System.Windows.Forms.RadioButton();
            this.rdb_ACIR_Median = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDownLMT_ACIRrange = new System.Windows.Forms.TextBox();
            this.txtUpLMT_ACIRrange = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.rdb_Drop_Min = new System.Windows.Forms.RadioButton();
            this.rdb_Drop_Median = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDownLMT_DropRange = new System.Windows.Forms.TextBox();
            this.txtUpLMT_DropRange = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.txtDownLMT_ACIR = new System.Windows.Forms.TextBox();
            this.txtUpLMT_ACIR = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUpLMT_SV = new System.Windows.Forms.TextBox();
            this.txtDownLMT_SV = new System.Windows.Forms.TextBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTempPara = new System.Windows.Forms.TextBox();
            this.txtTempBase = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUpLMT_V = new System.Windows.Forms.TextBox();
            this.txtDownLMT_V = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtUpLMT_time = new System.Windows.Forms.TextBox();
            this.txtDownLMT_time = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtVoltDrop = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtMinVoltDrop = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtMaxVoltDrop = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_k = new System.Windows.Forms.TextBox();
            this.chkEnEditProc = new System.Windows.Forms.CheckBox();
            this.chkEnLocalProcess = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOCVType = new System.Windows.Forms.ComboBox();
            this.cmbBattType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Info = new System.Windows.Forms.TextBox();
            this.btnSetDefaultProc = new System.Windows.Forms.Button();
            this.btnDelConfig = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox6.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(119, 287);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 32);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label10.Location = new System.Drawing.Point(7, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "工艺配置";
            // 
            // cmbConfigName
            // 
            this.cmbConfigName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cmbConfigName.FormattingEnabled = true;
            this.cmbConfigName.Location = new System.Drawing.Point(83, 122);
            this.cmbConfigName.Name = "cmbConfigName";
            this.cmbConfigName.Size = new System.Drawing.Size(115, 28);
            this.cmbConfigName.TabIndex = 0;
            this.cmbConfigName.Visible = false;
            this.cmbConfigName.SelectedIndexChanged += new System.EventHandler(this.cmb_Config_SelectedIndexChanged);
            this.cmbConfigName.TextChanged += new System.EventHandler(this.cmbConfigName_TextChanged);
            // 
            // btnDelBattType
            // 
            this.btnDelBattType.Location = new System.Drawing.Point(11, 288);
            this.btnDelBattType.Name = "btnDelBattType";
            this.btnDelBattType.Size = new System.Drawing.Size(89, 31);
            this.btnDelBattType.TabIndex = 22;
            this.btnDelBattType.Text = "删除型号";
            this.btnDelBattType.UseVisualStyleBackColor = true;
            this.btnDelBattType.Click += new System.EventHandler(this.btn_DeleteBattType_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox10);
            this.groupBox6.Controls.Add(this.groupBox5);
            this.groupBox6.Controls.Add(this.groupBox17);
            this.groupBox6.Controls.Add(this.groupBox12);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.groupBox13);
            this.groupBox6.Controls.Add(this.groupBox1);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Enabled = false;
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.groupBox6.Location = new System.Drawing.Point(12, 30);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(535, 536);
            this.groupBox6.TabIndex = 181;
            this.groupBox6.TabStop = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.chb_ENVoltDrop);
            this.groupBox10.Controls.Add(this.label16);
            this.groupBox10.Controls.Add(this.ckB_IS_Enable_ACIRRange);
            this.groupBox10.Controls.Add(this.ckB_IS_Enable_DropRange);
            this.groupBox10.Location = new System.Drawing.Point(248, 384);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(267, 132);
            this.groupBox10.TabIndex = 198;
            this.groupBox10.TabStop = false;
            // 
            // chb_ENVoltDrop
            // 
            this.chb_ENVoltDrop.AutoSize = true;
            this.chb_ENVoltDrop.Location = new System.Drawing.Point(15, 85);
            this.chb_ENVoltDrop.Name = "chb_ENVoltDrop";
            this.chb_ENVoltDrop.Size = new System.Drawing.Size(118, 24);
            this.chb_ENVoltDrop.TabIndex = 185;
            this.chb_ENVoltDrop.Text = "是否计算压降";
            this.chb_ENVoltDrop.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(8, 109);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(199, 20);
            this.label16.TabIndex = 184;
            this.label16.Text = "【附注】是：选中  否：不选择";
            // 
            // ckB_IS_Enable_ACIRRange
            // 
            this.ckB_IS_Enable_ACIRRange.AutoSize = true;
            this.ckB_IS_Enable_ACIRRange.Location = new System.Drawing.Point(15, 25);
            this.ckB_IS_Enable_ACIRRange.Name = "ckB_IS_Enable_ACIRRange";
            this.ckB_IS_Enable_ACIRRange.Size = new System.Drawing.Size(183, 24);
            this.ckB_IS_Enable_ACIRRange.TabIndex = 181;
            this.ckB_IS_Enable_ACIRRange.Text = "是否启用ACIR极差功能";
            this.ckB_IS_Enable_ACIRRange.UseVisualStyleBackColor = true;
            // 
            // ckB_IS_Enable_DropRange
            // 
            this.ckB_IS_Enable_DropRange.AutoSize = true;
            this.ckB_IS_Enable_DropRange.Location = new System.Drawing.Point(15, 55);
            this.ckB_IS_Enable_DropRange.Name = "ckB_IS_Enable_DropRange";
            this.ckB_IS_Enable_DropRange.Size = new System.Drawing.Size(178, 24);
            this.ckB_IS_Enable_DropRange.TabIndex = 183;
            this.ckB_IS_Enable_DropRange.Text = "是否启用压降极差功能";
            this.ckB_IS_Enable_DropRange.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdb_ACIR_Min);
            this.groupBox5.Controls.Add(this.rdb_ACIR_Median);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtDownLMT_ACIRrange);
            this.groupBox5.Controls.Add(this.txtUpLMT_ACIRrange);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(244, 139);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(271, 109);
            this.groupBox5.TabIndex = 197;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ACIR极差";
            // 
            // rdb_ACIR_Min
            // 
            this.rdb_ACIR_Min.AutoSize = true;
            this.rdb_ACIR_Min.Location = new System.Drawing.Point(184, 80);
            this.rdb_ACIR_Min.Name = "rdb_ACIR_Min";
            this.rdb_ACIR_Min.Size = new System.Drawing.Size(72, 24);
            this.rdb_ACIR_Min.TabIndex = 188;
            this.rdb_ACIR_Min.Text = "最小值";
            this.rdb_ACIR_Min.UseVisualStyleBackColor = true;
            // 
            // rdb_ACIR_Median
            // 
            this.rdb_ACIR_Median.AutoSize = true;
            this.rdb_ACIR_Median.Checked = true;
            this.rdb_ACIR_Median.Location = new System.Drawing.Point(105, 81);
            this.rdb_ACIR_Median.Name = "rdb_ACIR_Median";
            this.rdb_ACIR_Median.Size = new System.Drawing.Size(72, 24);
            this.rdb_ACIR_Median.TabIndex = 187;
            this.rdb_ACIR_Median.TabStop = true;
            this.rdb_ACIR_Median.Text = "中位数";
            this.rdb_ACIR_Median.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(23, 82);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 20);
            this.label17.TabIndex = 184;
            this.label17.Text = "计算参数";
            // 
            // txtDownLMT_ACIRrange
            // 
            this.txtDownLMT_ACIRrange.Location = new System.Drawing.Point(130, 51);
            this.txtDownLMT_ACIRrange.Name = "txtDownLMT_ACIRrange";
            this.txtDownLMT_ACIRrange.Size = new System.Drawing.Size(81, 27);
            this.txtDownLMT_ACIRrange.TabIndex = 5;
            // 
            // txtUpLMT_ACIRrange
            // 
            this.txtUpLMT_ACIRrange.Location = new System.Drawing.Point(130, 22);
            this.txtUpLMT_ACIRrange.Name = "txtUpLMT_ACIRrange";
            this.txtUpLMT_ACIRrange.Size = new System.Drawing.Size(80, 27);
            this.txtUpLMT_ACIRrange.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 20);
            this.label12.TabIndex = 3;
            this.label12.Text = "下控制限(mΩ)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "上控制限(mΩ)";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.rdb_Drop_Min);
            this.groupBox17.Controls.Add(this.rdb_Drop_Median);
            this.groupBox17.Controls.Add(this.label19);
            this.groupBox17.Controls.Add(this.txtDownLMT_DropRange);
            this.groupBox17.Controls.Add(this.txtUpLMT_DropRange);
            this.groupBox17.Controls.Add(this.label11);
            this.groupBox17.Controls.Add(this.label18);
            this.groupBox17.Location = new System.Drawing.Point(248, 259);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(267, 109);
            this.groupBox17.TabIndex = 196;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "压降极差";
            // 
            // rdb_Drop_Min
            // 
            this.rdb_Drop_Min.AutoSize = true;
            this.rdb_Drop_Min.Location = new System.Drawing.Point(180, 79);
            this.rdb_Drop_Min.Name = "rdb_Drop_Min";
            this.rdb_Drop_Min.Size = new System.Drawing.Size(72, 24);
            this.rdb_Drop_Min.TabIndex = 190;
            this.rdb_Drop_Min.Text = "最小值";
            this.rdb_Drop_Min.UseVisualStyleBackColor = true;
            // 
            // rdb_Drop_Median
            // 
            this.rdb_Drop_Median.AutoSize = true;
            this.rdb_Drop_Median.Checked = true;
            this.rdb_Drop_Median.Location = new System.Drawing.Point(101, 79);
            this.rdb_Drop_Median.Name = "rdb_Drop_Median";
            this.rdb_Drop_Median.Size = new System.Drawing.Size(72, 24);
            this.rdb_Drop_Median.TabIndex = 189;
            this.rdb_Drop_Median.TabStop = true;
            this.rdb_Drop_Median.Text = "中位数";
            this.rdb_Drop_Median.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(21, 83);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 20);
            this.label19.TabIndex = 186;
            this.label19.Text = "计算参数";
            // 
            // txtDownLMT_DropRange
            // 
            this.txtDownLMT_DropRange.Location = new System.Drawing.Point(129, 52);
            this.txtDownLMT_DropRange.Name = "txtDownLMT_DropRange";
            this.txtDownLMT_DropRange.Size = new System.Drawing.Size(86, 27);
            this.txtDownLMT_DropRange.TabIndex = 5;
            // 
            // txtUpLMT_DropRange
            // 
            this.txtUpLMT_DropRange.Location = new System.Drawing.Point(130, 22);
            this.txtUpLMT_DropRange.Name = "txtUpLMT_DropRange";
            this.txtUpLMT_DropRange.Size = new System.Drawing.Size(86, 27);
            this.txtUpLMT_DropRange.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 20);
            this.label11.TabIndex = 3;
            this.label11.Text = "下控制限(mV)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(103, 20);
            this.label18.TabIndex = 2;
            this.label18.Text = "上控制限(mV)";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.txtDownLMT_ACIR);
            this.groupBox12.Controls.Add(this.txtUpLMT_ACIR);
            this.groupBox12.Controls.Add(this.label22);
            this.groupBox12.Controls.Add(this.label23);
            this.groupBox12.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox12.Location = new System.Drawing.Point(20, 139);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(218, 109);
            this.groupBox12.TabIndex = 195;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "ACIR判定";
            // 
            // txtDownLMT_ACIR
            // 
            this.txtDownLMT_ACIR.Location = new System.Drawing.Point(129, 66);
            this.txtDownLMT_ACIR.Name = "txtDownLMT_ACIR";
            this.txtDownLMT_ACIR.Size = new System.Drawing.Size(77, 25);
            this.txtDownLMT_ACIR.TabIndex = 157;
            // 
            // txtUpLMT_ACIR
            // 
            this.txtUpLMT_ACIR.Location = new System.Drawing.Point(129, 29);
            this.txtUpLMT_ACIR.Name = "txtUpLMT_ACIR";
            this.txtUpLMT_ACIR.Size = new System.Drawing.Size(76, 25);
            this.txtUpLMT_ACIR.TabIndex = 158;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 68);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 20);
            this.label22.TabIndex = 160;
            this.label22.Text = "下限ACIR(mΩ)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(19, 32);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 20);
            this.label23.TabIndex = 159;
            this.label23.Text = "上限ACIR(mΩ)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtUpLMT_SV);
            this.groupBox4.Controls.Add(this.txtDownLMT_SV);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox4.Location = new System.Drawing.Point(248, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 109);
            this.groupBox4.TabIndex = 194;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "壳压NG判定";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 160;
            this.label6.Text = "下限壳压(mV)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 20);
            this.label9.TabIndex = 159;
            this.label9.Text = "上限壳压(mV)";
            // 
            // txtUpLMT_SV
            // 
            this.txtUpLMT_SV.Location = new System.Drawing.Point(129, 29);
            this.txtUpLMT_SV.Name = "txtUpLMT_SV";
            this.txtUpLMT_SV.Size = new System.Drawing.Size(77, 25);
            this.txtUpLMT_SV.TabIndex = 158;
            // 
            // txtDownLMT_SV
            // 
            this.txtDownLMT_SV.Location = new System.Drawing.Point(129, 66);
            this.txtDownLMT_SV.Name = "txtDownLMT_SV";
            this.txtDownLMT_SV.Size = new System.Drawing.Size(77, 25);
            this.txtDownLMT_SV.TabIndex = 157;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label14);
            this.groupBox13.Controls.Add(this.label15);
            this.groupBox13.Controls.Add(this.txtTempPara);
            this.groupBox13.Controls.Add(this.txtTempBase);
            this.groupBox13.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox13.Location = new System.Drawing.Point(24, 426);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(218, 104);
            this.groupBox13.TabIndex = 193;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "温度设置";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label14.Location = new System.Drawing.Point(19, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 20);
            this.label14.TabIndex = 162;
            this.label14.Text = "温度系数(C)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label15.Location = new System.Drawing.Point(21, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 20);
            this.label15.TabIndex = 161;
            this.label15.Text = "基准温度(C)";
            // 
            // txtTempPara
            // 
            this.txtTempPara.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtTempPara.Location = new System.Drawing.Point(117, 68);
            this.txtTempPara.Name = "txtTempPara";
            this.txtTempPara.Size = new System.Drawing.Size(77, 25);
            this.txtTempPara.TabIndex = 1;
            this.txtTempPara.Text = "0";
            // 
            // txtTempBase
            // 
            this.txtTempBase.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtTempBase.Location = new System.Drawing.Point(117, 29);
            this.txtTempBase.Name = "txtTempBase";
            this.txtTempBase.Size = new System.Drawing.Size(77, 25);
            this.txtTempBase.TabIndex = 0;
            this.txtTempBase.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtUpLMT_V);
            this.groupBox1.Controls.Add(this.txtDownLMT_V);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox1.Location = new System.Drawing.Point(20, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 109);
            this.groupBox1.TabIndex = 173;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电压NG判定";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 160;
            this.label2.Text = "下限电压(mV)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 159;
            this.label7.Text = "上限电压(mV)";
            // 
            // txtUpLMT_V
            // 
            this.txtUpLMT_V.Location = new System.Drawing.Point(129, 29);
            this.txtUpLMT_V.Name = "txtUpLMT_V";
            this.txtUpLMT_V.Size = new System.Drawing.Size(77, 25);
            this.txtUpLMT_V.TabIndex = 158;
            // 
            // txtDownLMT_V
            // 
            this.txtDownLMT_V.Location = new System.Drawing.Point(129, 66);
            this.txtDownLMT_V.Name = "txtDownLMT_V";
            this.txtDownLMT_V.Size = new System.Drawing.Size(77, 25);
            this.txtDownLMT_V.TabIndex = 157;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtUpLMT_time);
            this.groupBox7.Controls.Add(this.txtDownLMT_time);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox7.Location = new System.Drawing.Point(20, 500);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(218, 30);
            this.groupBox7.TabIndex = 177;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "OCV工艺时间";
            this.groupBox7.Visible = false;
            // 
            // txtUpLMT_time
            // 
            this.txtUpLMT_time.Location = new System.Drawing.Point(118, 29);
            this.txtUpLMT_time.Name = "txtUpLMT_time";
            this.txtUpLMT_time.Size = new System.Drawing.Size(76, 25);
            this.txtUpLMT_time.TabIndex = 162;
            this.txtUpLMT_time.Text = "99999";
            // 
            // txtDownLMT_time
            // 
            this.txtDownLMT_time.Location = new System.Drawing.Point(117, 67);
            this.txtDownLMT_time.Name = "txtDownLMT_time";
            this.txtDownLMT_time.Size = new System.Drawing.Size(77, 25);
            this.txtDownLMT_time.TabIndex = 161;
            this.txtDownLMT_time.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 160;
            this.label3.Text = "下限时间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 20);
            this.label8.TabIndex = 159;
            this.label8.Text = "上限时间";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtVoltDrop);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.txtMinVoltDrop);
            this.groupBox8.Controls.Add(this.label33);
            this.groupBox8.Controls.Add(this.label31);
            this.groupBox8.Controls.Add(this.txtMaxVoltDrop);
            this.groupBox8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox8.Location = new System.Drawing.Point(20, 259);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(218, 161);
            this.groupBox8.TabIndex = 175;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "压降NG判定";
            // 
            // txtVoltDrop
            // 
            this.txtVoltDrop.Location = new System.Drawing.Point(130, 73);
            this.txtVoltDrop.Name = "txtVoltDrop";
            this.txtVoltDrop.Size = new System.Drawing.Size(76, 25);
            this.txtVoltDrop.TabIndex = 165;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(22, 76);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(83, 20);
            this.label20.TabIndex = 164;
            this.label20.Text = "特定值(mV)";
            // 
            // txtMinVoltDrop
            // 
            this.txtMinVoltDrop.Location = new System.Drawing.Point(129, 116);
            this.txtMinVoltDrop.Name = "txtMinVoltDrop";
            this.txtMinVoltDrop.Size = new System.Drawing.Size(76, 25);
            this.txtMinVoltDrop.TabIndex = 163;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(21, 119);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(97, 20);
            this.label33.TabIndex = 162;
            this.label33.Text = "最小压降(mV)";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(21, 31);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(97, 20);
            this.label31.TabIndex = 161;
            this.label31.Text = "最大压降(mV)";
            // 
            // txtMaxVoltDrop
            // 
            this.txtMaxVoltDrop.Location = new System.Drawing.Point(129, 28);
            this.txtMaxVoltDrop.Name = "txtMaxVoltDrop";
            this.txtMaxVoltDrop.Size = new System.Drawing.Size(77, 25);
            this.txtMaxVoltDrop.TabIndex = 158;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txt_k);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupBox3.Location = new System.Drawing.Point(922, 521);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(218, 61);
            this.groupBox3.TabIndex = 178;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 161;
            this.label1.Text = "K值";
            // 
            // txt_k
            // 
            this.txt_k.Location = new System.Drawing.Point(117, 22);
            this.txt_k.Name = "txt_k";
            this.txt_k.Size = new System.Drawing.Size(76, 25);
            this.txt_k.TabIndex = 158;
            // 
            // chkEnEditProc
            // 
            this.chkEnEditProc.AutoSize = true;
            this.chkEnEditProc.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.chkEnEditProc.Location = new System.Drawing.Point(12, 12);
            this.chkEnEditProc.Name = "chkEnEditProc";
            this.chkEnEditProc.Size = new System.Drawing.Size(148, 24);
            this.chkEnEditProc.TabIndex = 189;
            this.chkEnEditProc.Text = "启用编辑工艺信息";
            this.chkEnEditProc.UseVisualStyleBackColor = true;
            this.chkEnEditProc.CheckedChanged += new System.EventHandler(this.chkEnEditProc_CheckedChanged);
            this.chkEnEditProc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkEnEditProc_MouseClick);
            // 
            // chkEnLocalProcess
            // 
            this.chkEnLocalProcess.AutoSize = true;
            this.chkEnLocalProcess.Checked = true;
            this.chkEnLocalProcess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnLocalProcess.Enabled = false;
            this.chkEnLocalProcess.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.chkEnLocalProcess.Location = new System.Drawing.Point(201, 12);
            this.chkEnLocalProcess.Name = "chkEnLocalProcess";
            this.chkEnLocalProcess.Size = new System.Drawing.Size(118, 24);
            this.chkEnLocalProcess.TabIndex = 179;
            this.chkEnLocalProcess.Text = "启用本地工艺";
            this.chkEnLocalProcess.UseVisualStyleBackColor = true;
            this.chkEnLocalProcess.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkEnProjectSet_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(7, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 21);
            this.label4.TabIndex = 182;
            this.label4.Text = "OCV 号";
            // 
            // cmbOCVType
            // 
            this.cmbOCVType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOCVType.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cmbOCVType.FormattingEnabled = true;
            this.cmbOCVType.Items.AddRange(new object[] {
            "OCV1",
            "OCV2",
            "OCV3"});
            this.cmbOCVType.Location = new System.Drawing.Point(83, 36);
            this.cmbOCVType.Name = "cmbOCVType";
            this.cmbOCVType.Size = new System.Drawing.Size(115, 28);
            this.cmbOCVType.TabIndex = 183;
            this.cmbOCVType.SelectedIndexChanged += new System.EventHandler(this.cmb_OCVType_SelectedIndexChanged);
            // 
            // cmbBattType
            // 
            this.cmbBattType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbBattType.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cmbBattType.FormattingEnabled = true;
            this.cmbBattType.Location = new System.Drawing.Point(83, 79);
            this.cmbBattType.Name = "cmbBattType";
            this.cmbBattType.Size = new System.Drawing.Size(115, 28);
            this.cmbBattType.TabIndex = 184;
            this.cmbBattType.SelectedValueChanged += new System.EventHandler(this.cmb_BattType_SelectedIndexChanged);
            this.cmbBattType.TextChanged += new System.EventHandler(this.cmbBattType_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label5.Location = new System.Drawing.Point(7, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 185;
            this.label5.Text = "电池型号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_Info);
            this.groupBox2.Controls.Add(this.btnSetDefaultProc);
            this.groupBox2.Controls.Add(this.btnDelConfig);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnDelBattType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.cmbBattType);
            this.groupBox2.Controls.Add(this.cmbConfigName);
            this.groupBox2.Controls.Add(this.cmbOCVType);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.groupBox2.Location = new System.Drawing.Point(555, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 335);
            this.groupBox2.TabIndex = 186;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // txt_Info
            // 
            this.txt_Info.BackColor = System.Drawing.SystemColors.MenuText;
            this.txt_Info.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_Info.ForeColor = System.Drawing.Color.Lime;
            this.txt_Info.Location = new System.Drawing.Point(11, 157);
            this.txt_Info.Multiline = true;
            this.txt_Info.Name = "txt_Info";
            this.txt_Info.Size = new System.Drawing.Size(193, 125);
            this.txt_Info.TabIndex = 188;
            // 
            // btnSetDefaultProc
            // 
            this.btnSetDefaultProc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetDefaultProc.Location = new System.Drawing.Point(11, 350);
            this.btnSetDefaultProc.Name = "btnSetDefaultProc";
            this.btnSetDefaultProc.Size = new System.Drawing.Size(89, 42);
            this.btnSetDefaultProc.TabIndex = 187;
            this.btnSetDefaultProc.Text = "设为此型号使用工艺";
            this.btnSetDefaultProc.UseVisualStyleBackColor = true;
            this.btnSetDefaultProc.Visible = false;
            this.btnSetDefaultProc.Click += new System.EventHandler(this.SetDefaultProc_Click);
            // 
            // btnDelConfig
            // 
            this.btnDelConfig.Location = new System.Drawing.Point(120, 355);
            this.btnDelConfig.Name = "btnDelConfig";
            this.btnDelConfig.Size = new System.Drawing.Size(89, 31);
            this.btnDelConfig.TabIndex = 186;
            this.btnDelConfig.Text = "删除工艺";
            this.btnDelConfig.UseVisualStyleBackColor = true;
            this.btnDelConfig.Visible = false;
            this.btnDelConfig.Click += new System.EventHandler(this.DelConfig_Click);
            // 
            // lblNote
            // 
            this.lblNote.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(559, 414);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(205, 146);
            this.lblNote.TabIndex = 189;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FormPorcessSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 582);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.chkEnLocalProcess);
            this.Controls.Add(this.chkEnEditProc);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPorcessSet";
            this.Text = "工程设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPorcessSet_FormClosed);
            this.Load += new System.EventHandler(this.FormPorcessSet_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbConfigName;
        private System.Windows.Forms.Button btnDelBattType;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUpLMT_V;
        private System.Windows.Forms.TextBox txtDownLMT_V;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbBattType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelConfig;
        private System.Windows.Forms.Button btnSetDefaultProc;
        private System.Windows.Forms.TextBox txt_Info;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox chkEnLocalProcess;
        private System.Windows.Forms.CheckBox chkEnEditProc;
        private System.Windows.Forms.ComboBox cmbOCVType;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTempPara;
        private System.Windows.Forms.TextBox txtTempBase;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_k;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtUpLMT_time;
        private System.Windows.Forms.TextBox txtDownLMT_time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtMinVoltDrop;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtMaxVoltDrop;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUpLMT_SV;
        private System.Windows.Forms.TextBox txtDownLMT_SV;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtUpLMT_ACIR;
        private System.Windows.Forms.TextBox txtDownLMT_ACIR;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox ckB_IS_Enable_ACIRRange;
        private System.Windows.Forms.CheckBox ckB_IS_Enable_DropRange;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtDownLMT_ACIRrange;
        private System.Windows.Forms.TextBox txtUpLMT_ACIRrange;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.TextBox txtDownLMT_DropRange;
        private System.Windows.Forms.TextBox txtUpLMT_DropRange;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton rdb_ACIR_Min;
        private System.Windows.Forms.RadioButton rdb_ACIR_Median;
        private System.Windows.Forms.RadioButton rdb_Drop_Min;
        private System.Windows.Forms.RadioButton rdb_Drop_Median;
        private System.Windows.Forms.CheckBox chb_ENVoltDrop;
        private System.Windows.Forms.TextBox txtVoltDrop;
        private System.Windows.Forms.Label label20;
    }
}