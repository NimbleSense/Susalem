using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Susalem.Infrastructure.Device.Engine;

/// <summary>
/// 柜子
/// </summary>
public class Cabinet : DeviceEngine
{
    public IList<DoorStatus> Doors { get; } = new List<DoorStatus>();

    public IList<DebugData> DebugDatas { get; } = new List<DebugData>() 
    {
        new("氧气开机延时", "min", 0 ,1),
        new("氧气上限", "%", 1 ,2),
        new("氧气下限", "%", 3 ,2),   
        new("氧气修正值", "%", 7 ,2),   
        new("温度上限", "℃", 17 ,2),
        new("温度下限", "℃", 19 ,2),
        new("温度修正值", "℃", 23 ,2),
        new("湿度上限", "%", 33 ,2),
        new("湿度下限", "%", 35 ,2),
        new("湿度修正值", "%", 39 ,2),
        new("氮气启动延时", "s", 64 ,1),
        new("氮气停止延时", "s", 65 ,1),
        new("报警启动延时", "s", 68 ,1),
        new("报警停止延时", "s", 69 ,1),
        new("照明延时", "s", 75 ,1),
        new("累计流量", "L/min", 102 ,2),
    };

    public Cabinet(ushort address, ICommChannel commChannel, ILogger<EngineFactory> logger) : base(address, commChannel, logger)
    {
        Doors.Add(new DoorStatus("Door1"));
        Doors.Add(new DoorStatus("Door2"));
        Doors.Add(new DoorStatus("Door3"));
        Doors.Add(new DoorStatus("Door4"));
    }

    public override bool UpdateTelemetries()
    {
        var result = base.UpdateTelemetries();

        if (result)
        {
            return Driver.Read(BasicInfo.Address, Doors);
        }

        return result;  
    }

    public bool LoadDebugData()
    {
        if (!Property.EnableComm) return false;

        return Driver.ReadDebugData(BasicInfo.Address, DebugDatas);
    }

    public bool WriteDebugData(DebugData debugData)
    {
        if (!Property.EnableComm) return false;

        return Driver.WriteDebugData(BasicInfo.Address, debugData);
    }

    public bool SyncTime()
    {
        if (!Property.EnableComm) return false;

        var commands = new List<EngineCommand>
        {
            new(300, (ushort)DateTime.Now.Year),
            new(301, (ushort)DateTime.Now.Month),
            new(302, (ushort)DateTime.Now.Day),
            new(303, (ushort)DateTime.Now.DayOfWeek),
            new(304, (ushort)DateTime.Now.Hour),
            new(305, (ushort)DateTime.Now.Minute),
            new(306, (ushort)DateTime.Now.Second)
        };

        return Driver.Execute(BasicInfo.Address, commands);
    }
}
