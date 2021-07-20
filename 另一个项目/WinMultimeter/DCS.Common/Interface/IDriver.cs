using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCS.Common.Interface
{
    /// <summary>
    /// 定义通用的驱动函数
    /// </summary>
   public interface IDriver
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init(DictionaryEx dic);
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="cmd"></param>
        void WriteByte(byte[] buffer);
        byte[] ReadByte();
        string ErrorMsg { get; }
    }
}
