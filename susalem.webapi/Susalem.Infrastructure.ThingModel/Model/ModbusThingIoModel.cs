using Quartz.Util;
using Susalem.Infrastructure.ThingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.ThingModel.Model
{
    public class ModbusThingIoModel
    {
        public Guid ID { get; set; }

        public string[] PropertyKeys { get; set; }

        public int FunctionCode { get; set; }
        public int Length { get; set; }

        public int[] BatchLength { get; set; }
        public string Address { get; set; }
        public object Value { get; set; }

        public DataTypeEnum ValueType { get; set; }
        public EndianEnum EndianType { get; set; }

        public override string ToString()
        {
            return $"变量ID:{ID},Address:{Address},Value:{Value},ValueType:{ValueType},Endian:{EndianType}";
        }


    }

    public class ModbusCommandIoModel: ModbusThingIoModel
    {
        public event EventHandler<object> OnWriteCommand;
    }
}
