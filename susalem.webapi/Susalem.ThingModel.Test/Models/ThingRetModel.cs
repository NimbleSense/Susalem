using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Susalem.Infrastructure.ThingModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test.Models
{
    public class ThingRetModel
    {
        public ThingRetModel()
        {
            this.Timestamp = DateTime.Now;
        }

        public ThingRetModel(VaribaleStatusTypeEnum status)
        {
            this.Timestamp = DateTime.Now; ;
            StatusType = status;
        }

        public ThingRetModel(VaribaleStatusTypeEnum status, string message)
        {
            this.Timestamp = DateTime.Now;
            StatusType = status;
            this.Message = message;
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
