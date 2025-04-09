using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Infrastructure.Device.Constants;
using Susalem.Infrastructure.Models.Application;
using System.Collections.Generic;

namespace Susalem.Infrastructure.Services.DbInitializerService;

public static class DbContent
{
    #region Device Type

    /// <summary>
    /// 柜子
    /// </summary>
    public static DeviceTypeEntity Cabinet = new() 
    { 
        Name = DeviceTypeEnum.Cabinet, 
        Description = "柜子", 
        IsPublish = true, 
        Properties = new List<DeviceTypeProperty>
        {
            new() { Key = "Oxygen", Reg = 0, Length = 2, Factor = 1, Unit = "%VOL" },
            new() { Key = "Nitrogen", Reg = 2, Length = 2, Factor = 1, Unit = "%VOL" },
            new() { Key = DeviceConstants.Temperature, Reg = 4, Length = 2, Factor = 1, Unit = "℃" }, 
            new() { Key = DeviceConstants.Humidity, Reg = 6, Length = 2, Factor = 1, Unit = "%RH" },
            new() { Key = "Flow", Reg = 4, Length = 8, Factor = 2, Unit = "L/min" }            
        } 
    };

    /// <summary>
    /// 报警器
    /// </summary>
    public static DeviceTypeEntity Alerter = new()
    {
        Name = DeviceTypeEnum.Alerter,
        Description = "报警器",
        IsPublish = true,
        Properties = new List<DeviceTypeProperty>
                    {
                        new() {Key = DeviceConstants.LightRed, Reg = 0, Length = 1},
                        new() {Key = DeviceConstants.LightGreen,Reg = 1, Length = 1},                     
                        new() {Key = DeviceConstants.LightYellow, Reg = 2, Length = 1},
                        new() {Key = DeviceConstants.Buzzing, Reg = 3, Length = 1}
                    }
    };


    #endregion
}
