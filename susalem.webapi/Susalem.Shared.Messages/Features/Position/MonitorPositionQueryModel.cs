using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.ReadModels.Position
{
    public class MonitorAreaQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<MonitorPositionQueryModel> Positions { get; set; }
    }

    public class MonitorPositionQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Opened { get; set; }

        public bool ShowDoor {  get; set; }

        public ICollection<MonitorPositionFunctionQueryModel> PositionFunctions { get; set; }

        public bool Door1 { get; set; }

        public bool Door2 { get; set; }

        public bool Door3 { get; set; }

        public bool Door4 { get; set; }
    }

    public class MonitorPositionFunctionQueryModel
    {
        public PositionFunctionEnum Key { get; set; }

        public ICollection<MonitorDeviceFunctionQueryModel> DeviceFunctions { get; set; }
    }

    public class MonitorDeviceFunctionQueryModel
    {
        public string Key { get; set; }

        public int Id { get; set; }

        public double UpdatedValue { get; set; }

        public DeviceStatus DeviceStatus { get; set; }

        public string Unit { get; set; }
    }

}
