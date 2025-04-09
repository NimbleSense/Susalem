using System.ComponentModel;
namespace Susalem.Qms.QmsEquipmentStatusEnum
{
    /// <summary>
    /// 设备状态类型
    /// </summary>
    public enum EquipmentStatusEnum
    {
        /// <summary>
        /// 循环运行
        /// </summary>
        [Description("循环运行")]
        Run = 1,

        /// <summary>
        /// 停机
        /// </summary>
        [Description("停机")]
        Stop = 2,

        /// <summary>
        /// 网络中断
        /// </summary>
        [Description("网络中断")]
        NetInterrupt = 3,

        /// <summary>
        /// 主线缺料
        /// </summary>
        [Description("主线缺料")]
        StarveMain = 4,

        /// <summary>
        /// 堵料
        /// </summary>
        [Description("堵料")]
        Block = 5,

        /// <summary>
        /// 报警
        /// </summary>
        [Description("报警")]
        Alarm = 6,

        /// <summary>
        /// AGV缺料
        /// </summary>
        [Description("AGV缺料")]
        StarveAGV = 7,

        /// <summary>
        /// 半成品缺料
        /// </summary>
        [Description("半成品缺料")]
        StanvesubPar = 8


    }
}
