using System.ComponentModel;
namespace Susalem.Mam.MesRepairStatusEnum
{
    /// <summary>
    /// 返修管理状态
    /// </summary>
    public enum RepairStatusEnum
    {
        /// <summary>
        /// 新建
        /// </summary>
        [Description("新建")]
        New = 1,

        /// <summary>
        /// 返修中
        /// </summary>
        [Description("返修中")]
        Repair = 2,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("返修完成")]
        Complete = 3,
        /// <summary>
        /// 拆解中
        /// </summary>
        [Description("拆解中")]
        Disassembling = 4,

        /// <summary>
        /// 拆解完成
        /// </summary>
        [Description("拆解完成")]
        Disassembled = 5,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Close = 6,

        /// <summary>
        /// 拆解零件
        /// </summary>
        [Description("拆解零件")]
        DisassembleParts = 7,
    }
}
