using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OCV.SocketHelper
{
    public class SocketClientHelper
    {
        #region 变量

        private string _serverIP; //IP 
        private int _serverPort; //端口
        private Socket clientSocket; //Socket客户端
        private object _lockSend = new object(); //锁

        #endregion

        #region SocketClientHelper 构造函数

        public SocketClientHelper(string serverIP, int serverPort)
        {
            _serverIP = serverIP;
            _serverPort = serverPort;
        }

        #endregion

        #region 连接服务器

        /// <summary>
        /// 连接服务器
        /// </summary>
        public bool ConnectServer()
        {
            try
            {
                if (clientSocket == null || !clientSocket.Connected)
                {
                    if (clientSocket != null)
                    {
                        clientSocket.Close();
                        clientSocket.Dispose();
                    }
                    string ip = _serverIP;
                    int port = Convert.ToInt32(_serverPort);
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
                    clientSocket = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.SendTimeout = 20000;
                    clientSocket.ReceiveTimeout = 20000;
                    clientSocket.SendBufferSize = 10240;
                    clientSocket.ReceiveBufferSize = 10240;
                    try
                    {
                        clientSocket.Connect(ipep);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region 断开服务器

        /// <summary>
        /// 断开服务器
        /// </summary>
        public void DisconnectServer()
        {
            try
            {
                if (clientSocket != null)
                {
                    if (clientSocket.Connected)
                        clientSocket.Disconnect(false);
                    clientSocket.Close();
                    clientSocket.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 向服务器发送消息
        /// </summary>
        /// <param name="sendStr">消息内容</param>
        /// <returns></returns>
        public string send(string sendStr)
        {
            try
            {
                //检查连接状态
                if (clientSocket.Connected)
                {
                    //转换编码
                    byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                    //发送消息
                    int sendDataLength = clientSocket.Send(bs, bs.Length, 0);
                    //接收消息
                    byte[] receivedDataBytes = new byte[10240];
                    int receivedDataLength=clientSocket.Receive(receivedDataBytes);
                    //转换编码
                    string receivedData = Encoding.UTF8.GetString(receivedDataBytes,0, receivedDataLength);
                    return receivedData;
                }
                else
                {
                    DisconnectServer();
                    ConnectServer();
                    //转换编码
                    byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                    //发送消息
                    int sendDataLength = clientSocket.Send(bs, bs.Length, 0);
                    //接收消息
                    byte[] receivedDataBytes = new byte[1024];
                    int receivedDataLength = clientSocket.Receive(receivedDataBytes);
                    //转换编码
                    string receivedData = Encoding.UTF8.GetString(receivedDataBytes, 0, receivedDataLength);
                    return receivedData;
                    //return "与客户端通信失败，可能是电脑未开启或者客户端未开启！";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}






