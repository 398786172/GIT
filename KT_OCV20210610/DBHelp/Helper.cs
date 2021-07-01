using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
namespace OCV
{
   public class Helper
    {
        /// <summary>
        /// 获得对象的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
       public static string getJsonStringByObject(object obj)
        {
            DataContractJsonSerializer serialier = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serialier.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            string msg = Encoding.UTF8.GetString(dataBytes);
            return msg;
        }
        /// <summary>
        /// 获得实例对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object getObjectByJson(string jsonString, object obj)
        {
            DataContractJsonSerializer serialier = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serialier.ReadObject(stream);
        }
       /// <summary>
        /// 获得实例对象
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="jsonString"></param>
       /// <returns></returns>
        public static T getObjectByJson<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        /// <summary>
        /// 将配置序列化XML保存
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        public static void Serializer<T>(object obj, string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(stream, obj);
            }

        }
        /// <summary>
        ///  对配置文件进行反序列化 
        /// </summary>
        /// <returns>List<List<TableInfo>></returns>
        public static List<T> DeSerializer<T>(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
                List<T> obj = (List<T>)formatter.Deserialize(sr);
                return obj;
            }
        }
       /// <summary>
       /// 将图片转为数组
       /// </summary>
       /// <param name="imagePath"></param>
       /// <returns></returns>
        public static byte[] GetPictureData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }
       /// <summary>
       /// 生成图片
       /// </summary>
       /// <param name="streamByte"></param>
       /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="objName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdateXmlNode(string filePath, string key, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            xmlDoc.SelectSingleNode("/root/" + key).InnerText = value;
            xmlDoc.Save(filePath);
        }
        public static string SelectXmlNode(string filePath, string name, string key)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            string value = doc.SelectSingleNode(name+"/"+key).InnerText;
            return value;
        }
        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetAttributeList(string fileName)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            foreach (XmlNode node in doc.SelectSingleNode("root").ChildNodes)
            {
                foreach (XmlNode node_child in node.ChildNodes)
                {
                    list.Add(node.Name, node.FirstChild.Value);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加xml指定节点给节点添加属性
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="objName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddXmlAttribute(string filePath, string key, string value)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("root");
            XmlElement xesub1 = xmlDoc.CreateElement(key);
            xesub1.InnerText = value;
            root.AppendChild(xesub1);
            xmlDoc.Save(filePath);

        }
        /// <summary>
        /// 删除指定属性
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="objName"></param>
        /// <param name="key"></param>
        public static void DelXml(string filePath, string key)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                XmlNodeList xnl = xmlDoc.SelectSingleNode("root").ChildNodes; //查找节点  
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.Name == key)
                    {
                        xn.ParentNode.RemoveChild(xn);
                        // xn.RemoveAll();  
                    }
                }
                xmlDoc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

}
