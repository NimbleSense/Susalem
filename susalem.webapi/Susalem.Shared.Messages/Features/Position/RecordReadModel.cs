using System;
using System.Collections.Generic;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.ReadModels.Record
{
    public class RecordReadModel
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string PositionName { get; set; }

        public ICollection<RecordContent> Contents { get; set; } = new List<RecordContent>();
    }

    public class RecordContent
    {
        public string Key { get; set; }

        public double Value { get; set; }

        public DeviceStatus DeviceStatus { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>
        public int DeviceAddress { get; set; }

        public string Channel { get; set; }

        /// <summary>
        /// 记录构造函数
        /// </summary>
        /// <param name="key">对应的设备功能</param>
        /// <param name="value">功能值</param>
        /// <param name="deviceAddress">设备物理地址</param>
        /// <param name="channel">设备对应的通道</param>
        public RecordContent(string key, double value, int deviceAddress, string channel)
        {
            Key = key;
            Value = value;
            DeviceAddress = deviceAddress;
            Channel = channel;
            DeviceStatus = DeviceStatus.Normal;
        }

        public RecordContent(string key, double value)
        {
            Key = key;
            Value = value;
            DeviceStatus = DeviceStatus.Normal;
        }

        public RecordContent() 
        {
        }  

        public override string ToString()
        {
            return $"{Key} {Value} {DeviceStatus} {DeviceAddress} {Channel}";
        }
    }
}
