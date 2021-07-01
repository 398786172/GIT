using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using OCV.OCVLogs;
using ClsDeviceComm.BasicFramework;
using ClsDeviceComm.LogNet;
using OCV.INI;
using System.Web.Script.Serialization;
using DB_OCV.DAL;
using System.Threading;
using System.IO;
using CCWin;

namespace OCV
{
    public partial class FormLoad : CCSkinMain  //Form
    {
        SoftAuthorize softAuthorize;
        ClsIniParameter IniParameter = null;
        JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
        public FormLoad()
        {
            InitializeComponent();
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
            try
            {

                //进程已经存在时退出
                if (ClsGlobal.CheckProcessOn("KT_OCV"))
                {
                    this.Close();
                }


                #region 初始化日志
                ClsLogs.LogNetINI();
                #endregion

                #region 加载参数
                IniParameter = new ClsIniParameter();
                IniParameter.IniParameter();
                #endregion

                #region 初始化日志
                ClsLogs.LogNet();
                #endregion

                //擎天数据库的OCV数据连接
                ClsGlobal.mDBCOM_OCV_QT = new DBCOM_OCV(ClsGlobal.Server_OCV_IP, ClsGlobal.Server_OCV_DB, ClsGlobal.Server_OCV_id, ClsGlobal.Server_OCV_Pwd);
                
                //物流系统接口
                ClsGlobal.WCSCOM = ClsWCSCOM.Instance;

                Thread UI = new Thread(UIinfo);
                UI.IsBackground = true;
                UI.Start();

                p = new PointF(this.label1.Size.Width, 0);
            }
            catch (Exception EX)
            {
                MessageBox.Show("软件启动失败" + EX.Message.ToString());
            }
        }

        private string text = "欢 迎 登 录  OCV 测 试 设 备";
        private PointF p;
        private Font f = new Font("微软雅黑", 24, FontStyle.Bold);
        private Color c = Color.SteelBlue;
        private string temp;
        private void UIinfo()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(200);
                    Invoke(new EventHandler(delegate
                    {

                        label4.Text = "系统时间：" + System.DateTime.Now.ToString();
                        Graphics g = this.label1.CreateGraphics();
                        SizeF s = new SizeF();
                        s = g.MeasureString(text, f);//测量文字长度  
                        Brush brush = Brushes.White;
                        g.Clear(c);//清除背景  
                        if (temp != text)//文字改变时,重新显示  
                        {
                            p = new PointF(this.label1.Size.Width, 0);
                            temp = text;
                        }
                        else
                        {
                            p = new PointF(p.X - 10, 0);//每次偏移10  
                        }

                        if (p.X <= -s.Width)
                        {
                            p = new PointF(this.label1.Size.Width, 0);
                        }
                        g.DrawString(text, f, brush, p);
                    }));
                }
                catch (Exception)
                {
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tetPwd.Text.Trim() == "")
                {
                    MessageBox.Show("密码不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                if (ClsGlobal.SystemPwd != this.tetPwd.Text.Trim())    //后门
                {
                    if (txtUserName.Text.Trim() == "")
                    {
                        MessageBox.Show("用户名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                    if (!ClsGlobal.mDBCOM_OCV_QT.ExistsUesrInfo(txtUserName.Text.Trim().ToUpper()))
                    {
                        MessageBox.Show("此用户不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int UserAuthority = ClsGlobal.mDBCOM_OCV_QT.GetUesrInfo(txtUserName.Text.Trim().ToUpper(), this.tetPwd.Text.Trim());
                    if (UserAuthority == 0)
                    {
                        MessageBox.Show("密码错误,请重新输入！");
                        return;
                    }
                    else if (UserAuthority < 5 && UserAuthority > 0)
                    {
                        ClsGlobal.USER_NO = txtUserName.Text.Trim().ToUpper();     //作业员
                        ClsGlobal.ProjectSetType = 1;
                        ClsGlobal.UserAuthority = UserAuthority;
                        ClsLogs.UserinfologNet.WriteInfo("员工登录", txtUserName.Text.Trim() + "登录成功");
                        this.Hide(); //隐藏当前窗体 
                        FrmSys frmSys = new FrmSys();
                        frmSys.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("此用户信息异常");
                        return;
                    }
                }
                else if (ClsGlobal.SystemPwd == this.tetPwd.Text.Trim())
                {
                    ClsGlobal.USER_NO = "BYD";     //作业员
                    ClsGlobal.ProjectSetType = 1;
                    ClsGlobal.UserAuthority = 4;
                    ClsLogs.UserinfologNet.WriteInfo("员工登录", "BYD登录成功");
                    this.Hide(); //隐藏当前窗体 
                    FrmSys frmSys = new FrmSys();
                    frmSys.ShowDialog();
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 一个自定义的加密方法，传入一个原始数据，返回一个加密结果
        /// </summary>
        /// <param name="origin">原始的</param>
        /// <returns></returns>
        private string AuthorizeEncrypted(string origin)
        {
            // 此处使用了组件支持的DES对称加密技术
            return SoftSecurity.MD5Encrypt(origin, "Kinte666");
        }

    }
}
