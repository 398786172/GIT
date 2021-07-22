using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCS.Model2700_COM.ComDriver
{
    public interface IDriver
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init(Dictionary<string,string> dic);
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="cmd"></param>
        void WriteByte(byte[] buffer);
        byte[] ReadByte();
        string ErrorMsg { get; }

    }
}
