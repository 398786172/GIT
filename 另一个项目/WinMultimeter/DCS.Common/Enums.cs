using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCS.Common
{
    /// <summary>
    /// 电池的排列类型：从左往右，从上往下,在电池选择界面使用
    /// </summary>
    public enum BatteryOrderType
    {
        LeftToRight,
        UpToDown
    }
    /// <summary>
    /// 通讯的接收状态
    /// </summary>
    public enum RecvResultType
    {
        /// <summary>
        /// 接收完成
        /// </summary>
        Success,
        /// <summary>
        /// 在规定时间内没有接收完
        /// </summary>
        NotCompleted,
        /// <summary>
        /// 没有返回的数据，可能是离线
        /// </summary>
        NoData,
        /// <summary>
        /// 不需要接收返回值
        /// </summary>
        NoNeedReceive
    }
    /// <summary>
    /// 校准、计量的状态变化
    /// </summary>
    public enum TestStatus
    {
        /// <summary>
        /// 没开始
        /// </summary>
        NotStart,
        /// <summary>
        /// 正在请求测试，还未开始测试
        /// </summary>
        RequestTest,
        /// <summary>
        /// 正在测试
        /// </summary>
        Testting,
        /// <summary>
        /// 正在请求暂停，还未暂停
        /// </summary>
        RequestPause,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 请求继续测试
        /// </summary>
        RequestContinue,
        /// <summary>
        /// 请求停止
        /// </summary>
        RequestStop,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 完成
        /// </summary>
        Complete

    }
    public enum TestMode
    {
        /// <summary>
        /// 整体一起测试
        /// </summary>
        All,
        /// <summary>
        /// 单步测试
        /// </summary>
        Step,
        /// <summary>
        /// 一个状态，例如CPU充电电压
        /// </summary>
        OneStatus,
        /// <summary>
        /// 一个通道测试
        /// </summary>
        OneChannel,
    }
    public enum TestType
    {
        /// <summary>
        /// 校准
        /// </summary>
        Adjust,
        /// <summary>
        /// 计量
        /// </summary>
        Mesurement
    }
    /// <summary>
    /// 三种需要经过电流分辨率计算的模式
    /// 1.发送设定的电流值给设备
    /// 2.从设备接收的电流值转换为实际值
    /// 3.从万用表读取的电流值发送到设备
    /// </summary>
    public enum GetValueFromCurrentRatioModes
    {
        /// <summary>
        /// 设定的电流值经过电流分辨率转换后发给设备，包括手动发送校准点，自动计量时发送设定值
        /// </summary>
        SendSetValueToDevice,
        /// <summary>
        /// 将从设备读取的电流值按照电流分辨率转换
        /// </summary>
        ReadCurrentFromDevice,
        /// <summary>
        /// 从万用表读取的电流值，经过电流分辨率转换发送给设备
        /// </summary>
        SendMultiValueToDevice
    }
    public enum AutoModeStatus
    {
        /// <summary>
        /// 等待工装进入库位
        /// </summary>
        WaitForModelIn=0,
        /// <summary>
        /// 工装已进入库位
        /// </summary>
        ModelIn,
        /// <summary>
        /// 汽缸下压
        /// </summary>
        Cylinder_Down,
        /// <summary>
        /// 工装通电
        /// </summary>
        Model_Power_On,
        /// <summary>
        /// 等待万用表、调试台连接
        /// </summary>
        WaitForConnect,
        /// <summary>
        /// 正在校准（计量）...
        /// </summary>
        Testting,
        /// <summary>
        /// 测试完成
        /// </summary>
        Complete
    }
}
