using System.ComponentModel;
namespace Susalem.Wms.WmsBizTypeEnum
{
    /// <summary>
    /// 出入库记录业务类型
    /// </summary>
    public enum BizTypeEnum
    {
        /// <summary>
        /// AGV送料
        /// </summary>
        [Description("AGV送料")]
        LesWarehouse = 0,

        /// <summary>
        /// 清洗线
        /// </summary>
        [Description("清洗线")]
        CleaningLine = 1,

        /// <summary>
        /// 人工加料
        /// </summary>
        [Description("人工加料")]
        ManualFeeding = 2,

        /// <summary>
        /// 生产出库
        /// </summary>
        [Description("生产出库")]
        ProductionDelivery = 3,

        /// <summary>
        /// 报废
        /// </summary>
        [Description("报废")]
        Scrap = 4,

        /// <summary>
        /// 空轨
        /// </summary>
        [Description("空轨")]
        AerialTrack = 5,
    }
}
