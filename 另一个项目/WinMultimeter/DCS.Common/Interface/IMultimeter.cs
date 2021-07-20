using System.Collections.Generic;

namespace DCS.Common.Interface
{
    public interface IMultimeter
    {
        /// <summary>
        /// 万用表的驱动方式：串口或者GPIB
        /// </summary>
        IDriver Driver { get; }
        /// <summary>
        /// 万用表初始化
        /// </summary>
        void Init(Dictionary<string, object> dic);
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="needReadResponse">是否读取返回值</param>
        /// <returns></returns>
        string WriteString(string cmd, bool needReadResponse);
        /// <summary>
        /// 万用表读值，统一返回mV/mA,没有读取到返回null
        /// </summary>
        /// <returns></returns>
        double? ReadValue();
        /// <summary>
        /// 设定电压档量测
        /// </summary>
        /// <returns></returns>
        bool SetVoltageFunction();
        /// <summary>
        /// 设定电流档量测
        /// </summary>
        /// <returns></returns>
        bool SetCurrentFunction();
        /// <summary>
        /// 复位
        /// </summary>
        void Reset();
        /// <summary>
        /// 获取关于型号的信息
        /// </summary>
        /// <returns></returns>
        string GetProductInfo();
        DictionaryEx GetConfig();
        void SaveConfig(DictionaryEx dic);

        void ShowSetting();

    }
}
