using System;
using System.Windows.Forms;
using OCV.SocketHelper;


namespace OCV
{
    /// <summary>
    /// 读一维/二维条形码类__阅读器MATRIX 220
    /// </summary>
    public class ClsSocketCodeScan
    {
        /// <summary>
        /// 变量
        /// </summary>
        private SocketClientHelper mSocketClientHelper = null;

        //构造函数
        public ClsSocketCodeScan(string serverIP, int serverPort)
        {
            mSocketClientHelper = new SocketClientHelper(serverIP, serverPort);
            ConnectServer();
        }
        /// <summary>
        /// Socket连接服务器
        /// </summary>
        /// <returns></returns>
        public bool ConnectServer()
        {
            try
            {
                if (mSocketClientHelper != null)
                {
                    return mSocketClientHelper.ConnectServer();
                }
                else
                {
                    throw new Exception("请输入IP及端口连接socket");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        ///断开socket连接
        /// </summary>
        /// <returns></returns>
        public void DisConnectServer()
        {
            try
            {
                if (mSocketClientHelper != null)
                {
                    mSocketClientHelper.DisconnectServer();
                }
                else
                {
                    MessageBox.Show("未连接Socket,断开失败");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// socket读取条码方法
        /// </summary>
        /// <returns></returns>
        public string SocketReadCode()
        {
            string resultCode;
            string result;
            if (this.mSocketClientHelper != null)
            {
                try
                {
                    resultCode = mSocketClientHelper.send("LON"+Environment.NewLine);
                    result=resultCode.Split('\n')[0];
                    result = result.Substring(0,result.Length - 1);
                    return result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return "ERROR";
                }
            }
            else
            {
                return "NG";
            }
        }
        /// <summary>
        /// socket关闭读取条码方法
        /// </summary>
        /// <returns></returns>
        public void SocketNotReadCode()
        {
            if (this.mSocketClientHelper != null)
            {
                try
                {
                    mSocketClientHelper.send("LOFF" + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                throw  new  Exception("Socket未打开");
            }
        }
    }

}
