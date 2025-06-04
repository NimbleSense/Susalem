using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.ThingModel.Model
{
    public class ModbusThingRetModel
    {
        public ModbusThingRetModel()
        {
            Timestamp = DateTime.Now;
        }

        public ModbusThingRetModel(VaribaleStatusTypeEnum status)
        {
            Timestamp = DateTime.Now; ;
            StatusType = status;
        }

        public ModbusThingRetModel(VaribaleStatusTypeEnum status, string message)
        {
            Timestamp = DateTime.Now;
            StatusType = status;
            Message = message;
        }

        public object Value { get; set; }
        public object CookedValue { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VaribaleStatusTypeEnum StatusType { get; set; } = VaribaleStatusTypeEnum.UnKnow;

        public Guid VarId { get; set; }
    }
}
