namespace Susalem.Mam.MesRepairStatusEnum
{
    using System.ComponentModel;
    public enum RepairStationTypeEnum
    {
        /// <summary>
        /// 上线工位
        /// </summary>
        [Description("上线工位")]
        UpStation = 1,
        /// <summary>
        /// 返修工位
        /// </summary>
        [Description("返修工位")]
        RepairStation = 2,
        /// <summary>
        /// 返修上线岔口
        /// </summary>
        [Description("返修上线岔口")]
        RepairUp = 3,

        /// <summary>
        /// 返修下线岔口
        /// </summary>
        [Description("返修下线岔口")]
        RepairDown = 4,
    }
}
