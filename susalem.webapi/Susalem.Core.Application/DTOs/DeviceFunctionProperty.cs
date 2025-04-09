namespace Susalem.Core.Application.DTOs
{
    public class DeviceFunctionProperty
    {
        /// <summary>
        /// 功能类型
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public int Factor { get; set; }

        /// <summary>
        /// Modbus 寄存器地址
        /// </summary>
        public int? Reg { get; set; }

        /// <summary>
        /// 寄存器读取长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }

    public class DeviceTypeProperty
    {
        public string Key { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public int Factor { get; set; }

        /// <summary>
        /// 寄存器地址(默认设置，用作添加设备时参考)
        /// </summary>
        public int? Reg { get; set; }

        /// <summary>
        /// 读取长度(默认设置，用作添加设备时参考)
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

    }
}
