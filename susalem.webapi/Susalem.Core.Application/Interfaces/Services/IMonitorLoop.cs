using System;
using System.Collections.Generic;
using System.Linq;
using DynamicExpresso;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Messages.Enumerations;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IMonitorLoop
    {
        bool IsMonitoring { get; }

        /// <summary>
        /// 监控中的点位ID
        /// </summary>
        ICollection<int> MonitoringPositionIds { get; }

        /// <summary>
        /// 离线的设备ID
        /// </summary>
        ICollection<int> OfflineDevices { get; }

        /// <summary>
        /// 开始监控点位
        /// 首先会启动对应的设备
        /// </summary>
        /// <returns></returns>
        void StartMonitorLoop();

        /// <summary>
        /// 停止监控
        /// </summary>
        /// <returns></returns>
        void StopMonitorLoop();

        /// <summary>
        /// 蜂鸣声
        /// </summary>
        bool IsEnableBuzzing { get; }

        /// <summary>
        /// 报警灯
        /// </summary>
        bool IsEnableLighting { get; }

        /// <summary>
        /// 设置所有报警器的蜂鸣状态
        /// </summary>
        /// <param name="isEnableBuzzing">是否允许蜂鸣</param>
        /// <returns></returns>
        void SetAlerterBuzzing(bool isEnableBuzzing);

        /// <summary>
        /// 设置所有报警灯的状态
        /// </summary>
        /// <param name="isEnableLighting">是否启用报警灯</param>
        void SetAlerterLighting(bool isEnableLighting);
    }

    /// <summary>
    /// 报警器
    /// </summary>
    public interface IAlerter
    {
        /// <summary>
        /// 物理地址
        /// </summary>
        ushort Address { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 报警器Id
        /// </summary>
        int Id { get; }

        bool Online { get; }

        /// <summary>
        /// 蜂鸣器是否启用
        /// </summary>
        bool BuzzingEnabled { get; set; }

        /// <summary>
        /// 报警灯是否启用
        /// </summary>
        bool LightingEnabled { get; }

        /// <summary>
        /// 亮红灯
        /// </summary>
        void ToAlarm();

        /// <summary>
        /// 亮黄灯
        /// </summary>
        void ToWarning();

        /// <summary>
        /// 普通状态，亮绿灯
        /// </summary>
        void ToIdle();

        /// <summary>
        /// 恢复到初始化状态
        /// </summary>
        void ToUndefined();

        /// <summary>
        /// 设备报警灯可用状态
        /// </summary>
        /// <param name="isLightingEnabled">是否可用</param>
        /// <param name="isMonitoring">是否在监控中</param>
        void SetLightingStatus(bool isLightingEnabled, bool isMonitoring);
    }

    /// <summary>
    /// 设备引擎，针对唯一的地址
    /// </summary>
    public interface IEngine
    {
        EngineInfo BasicInfo { get; }

        EngineProperty Property { get; }

        /// <summary>
        /// 通信通道
        /// </summary>
        ICommChannel CommChannel { get; }

        /// <summary>
        /// 更新遥测数据
        /// </summary>
        /// <returns></returns>
        bool UpdateTelemetries();

        /// <summary>
        /// 遥测数据
        /// </summary>
        IList<EngineTelemetry> Telemetries { get; }

        event EventHandler<DeviceStatusEventArgs> EngineStatusHandler;

        /// <summary>
        /// 绑定设备
        /// </summary>
        void BindDevice(DeviceQueryModel deviceQueryModel);
    }

    /// <summary>
    /// 单个设备控制命令
    /// </summary>
    public class EngineCommand
    {
        /// <summary>
        /// 寄存器地址
        /// </summary>
        public ushort Reg { get; }

        /// <summary>
        /// 单个寄存器写入值
        /// </summary>
        public ushort Value
        {
            get
            {
                if (Values!=null && Values.Length > 0)
                {
                    return Values[0];
                }
                return 0;
            }
        }

        /// <summary>
        /// 多个寄存器数组
        /// </summary>
        public ushort[] Values { get; }

        public EngineCommand(ushort reg, ushort value)
        {
            Reg = reg;
            Values = new ushort[] { value };
        }

        public EngineCommand(ushort reg, ushort[] value)
        {
            Reg = reg;
            Values = value;
        }

        public void Update(ushort value)
        {
            Values[0] = value;
        }
    }

    public class DeviceStatusEventArgs : EventArgs
    {
        public ICollection<int> DeviceIds { get; }

        public bool Active { get; }

        public DeviceStatusEventArgs(ICollection<int> deviceIds,bool active)
        {
            DeviceIds = deviceIds;
            Active = active;
        }
    }

    /// <summary>
    /// 引擎基本信息
    /// </summary>
    public class EngineInfo
    {
        /// <summary>
        /// 物理地址
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChannelId { get; set; }

        /// <summary>
        /// 绑定的设备信息
        /// </summary>
        public IList<DeviceQueryModel> BondedDevices =new List<DeviceQueryModel>();

        /// <summary>
        /// 物理地址对应的设备列表ID
        /// </summary>
        public IList<int> DeviceIds => BondedDevices.Select(t=>t.Id).ToList();

        /// <summary>
        /// 物理地址对应的设备名称
        /// </summary>
        public IList<string> Names => BondedDevices.Select(t=>t.Name).ToList();

        public EngineInfo(ushort address, int channelId)
        {
            Address = address;
            ChannelId = channelId;
        }
    }

    /// <summary>
    /// 引擎状态
    /// </summary>
    public class EngineProperty
    {
        /// <summary>
        /// 通信失败次数
        /// </summary>
        private int _failNumber;
        private bool _online = true;
        private readonly ICommChannel _commChannel;

        public EventHandler<bool> ActiveChanged { get; set; }

        /// <summary>
        /// 设备是否在线。
        /// 如果通信失败连续三次，代表离线。
        /// 离线时间超过三分钟，允许重试1次，重试成功恢复在线，否则继续离线状态
        /// </summary>
        public bool Online 
        { 
            get
            {
                //如果通道离线, 则设显示备离线
                if (_commChannel.Status == ChannelStatus.Offline) return false;

                return _online;
            }
            private set
            {
                if (_online == value)
                    return;

                _online = value;
                if (_online)
                    LastConnectTime = DateTime.Now;
                else
                    LastDisconnectTime = DateTime.Now;

                ActiveChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// 是否允许通信
        /// 条件：
        /// 1. 设备在线
        /// 2. 距离上次通信时间超过3分钟，允许尝试1次
        /// </summary>
        public bool EnableComm
        {
            get => Online || DateTime.Now.Subtract(LastActivityTime).TotalSeconds >= 60*3;
        }

        /// <summary>
        /// 设备最后连接时间
        /// </summary>
        public DateTime LastConnectTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 设备断开连接时间
        /// </summary>
        public DateTime LastDisconnectTime { get; set; }

        /// <summary>
        /// 设备进行通信时间
        /// </summary>
        public DateTime LastActivityTime { get; set; }

        /// <summary>
        /// 设备启动时间
        /// </summary>
        public DateTime LastTurnOnTime { get; set; }

        /// <summary>
        /// 设备暂停时间
        /// </summary>
        public DateTime LastPauseTime { get; set; }

        /// <summary>
        /// 设备设备状态
        /// </summary>
        /// <param name="success">通信状态是否成功</param>
        public void Update(bool success)
        {
            LastActivityTime = DateTime.Now;

            if (success)
            {
                Online = true;
                _failNumber = 0; 
            }
            else
            {
                _failNumber++;
                if (_failNumber >= 3)
                {
                    Online= false;
                }
            }
        } 
        
        public EngineProperty(ICommChannel commChannel)
        {
            _commChannel = commChannel;
        }

        public override string ToString()
        {
            return $"EnableComm: {EnableComm}, Online: {Online}, LastConnectTime: {LastConnectTime}, LastDisconnectTime: {LastDisconnectTime}, LastActivityTime: {LastActivityTime}";
        }
    }

    /// <summary>
    /// 柜门状态
    /// </summary>
    public class DoorStatus
    {
        public string Key { get; }

        public bool Open { get; set; }

        public DoorStatus(string key)
        {
            Key = key;
        }
    }

    public class DebugData
    {
        public string Key { get; set; }

        public ushort Reg {  get; set; }

        public int Length {  get; set; }

        public string Unit {  get; set; }

        public ushort[] Data { get; set; }

        public double Value { get; set; }

        public DebugData(string key, string unit, ushort reg, int length)
        {
            Key = key;
            Unit = unit;
            Reg = reg;
            Length = length;
        }
    }


    /// <summary>
    /// 设备引擎的遥测数据
    /// </summary>
    public class EngineTelemetry
    {
        /// <summary>
        /// 数据名称
        /// </summary>
        public string Key { get;}

        /// <summary>
        /// 寄存器地址
        /// </summary>
        public byte Reg { get; set; }

        /// <summary>
        /// 读取或者写入的长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public int Factor { get; set; }

        /// <summary>
        /// 计算后的显示数据值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 读取到的原始值
        /// </summary>
        public uint OriginalValue { get; set; }

        public EngineTelemetry(string key)
        {
            Key = key;
        }

        /// <summary>
        /// 将原始值通过计算，变成真实数据值
        /// </summary>
        /// <param name="initialValue"></param>
        public void Cal(uint initialValue)
        {
            OriginalValue = initialValue;

            //先乘以系数
            initialValue *= (uint)Factor;

            if (string.IsNullOrEmpty(Expression))
            {
                Value = initialValue;            
                return;
            }

            var target = new Interpreter();
            var result =
                target.Eval<double>(Expression, new DynamicExpresso.Parameter("raw", typeof(double), initialValue));

            Value = Math.Round(result, 2);
        }

        /// <summary>
        /// 获取写入值
        /// </summary>
        /// <returns></returns>
        public ushort[] GetWriteData()
        {
            var datas = new ushort[Length];

            //先乘以系数
            var data = Value * Factor;

            if (!string.IsNullOrEmpty(Expression))
            {
                var target = new Interpreter();
                data = target.Eval<double>(Expression, new DynamicExpresso.Parameter("raw", typeof(double), data));
            }

            byte[] byteDatas;
            if (data < 0)
            {
                var intData = Convert.ToInt32(data);
                byteDatas = BitConverter.GetBytes(intData);
            }
            else
            {
                var intData = Convert.ToUInt32(data);
                byteDatas = BitConverter.GetBytes(intData);
            }

            for (var i = 0; i < Length; i++)
            {
                datas[i] = BitConverter.ToUInt16(byteDatas, i * 2);
            }
            return datas;
        }

        /// <summary>
        /// 拷贝属性数据
        /// </summary>
        /// <returns></returns>
        public EngineTelemetry Copy()
        {
            return new EngineTelemetry(Key)
            {
                Factor = Factor,
                Reg = Reg,
                Expression = Expression,
                Value = Value,
                Length = Length,
                OriginalValue = OriginalValue
            };
        }
    }
}
