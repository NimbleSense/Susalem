using DynamicExpresso;
using Microsoft.Extensions.Logging;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.ThingModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.ThingModel.Interface
{
    public interface IModbusThingDriver
    {
        public string DeviceId { get; }
        public bool IsConnected { get; }
        public int Timeout { get; }
        public uint MinPeriod { get; }

        public ILogger _logger { get; set; }

        public bool Connect();

        public bool Close();

        //标准数据读取
        public ModbusThingRetModel Read(ModbusThingIoModel ioArg, bool isMultiple);

        //Rpc写入
        public Task<RpcResponse> WriteAsync(string RequestId, string Method, ModbusThingIoModel ioArg);


    }


}
