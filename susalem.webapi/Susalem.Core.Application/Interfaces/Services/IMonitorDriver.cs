using System.Collections.Generic;

namespace Susalem.Core.Application.Interfaces.Services
{
    /// <summary>
    /// 引擎通信接口服务
    /// </summary>
    public interface IMonitorDriver
    {
        /// <summary>
        /// 通信连接
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// 通信断开
        /// </summary>
        /// <returns></returns>
        void Disconnect();

        /// <summary>
        /// Check is connected
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 执行单个引擎命令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="command"></param>
        bool Execute(int address, EngineCommand command);

        /// <summary>
        /// 执行多个引擎命令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commands"></param>
        bool Execute(int address, IList<EngineCommand> commands);

        /// <summary>
        /// 执行多个线圈命令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commands"></param>
        bool ExecuteCoil(int address, IList<EngineCommand> commands);

        /// <summary>
        /// 读取引擎遥测数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="telemetries"></param>
        bool Read(int address, IList<EngineTelemetry> telemetries);

        /// <summary>
        /// 读取引擎遥测数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="telemetries"></param>
        bool ReadCoil(int address, IList<EngineTelemetry> telemetries);

        bool ReadDebugData(int address, IList<DebugData> datas);

        bool WriteDebugData(int address, DebugData data);

        bool Read(int address, IList<DoorStatus> doors);

        /// <summary>
        /// 写入引擎遥测数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="telemetries"></param>
        bool Write(int address, IList<EngineTelemetry> telemetries);
    }
}
