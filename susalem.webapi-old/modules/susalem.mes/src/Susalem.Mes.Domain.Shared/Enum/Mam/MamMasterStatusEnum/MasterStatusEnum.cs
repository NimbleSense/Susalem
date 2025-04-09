using System.ComponentModel;
namespace Susalem.Mam.MamMasterStatusEnum
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum MasterStatusEnum
    {
        /// <summary>
        /// 合格
        /// </summary>
        [Description("合格")]
        OK = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        [Description("不合格")]
        NG = 2,

        /// <summary>
        /// Master件
        /// </summary>
        [Description("Master件")]
        Master = 3,
        /// <summary>
        /// Master件
        /// </summary>
        [Description("Gold件")]
        Gold = 4,
    }
}
