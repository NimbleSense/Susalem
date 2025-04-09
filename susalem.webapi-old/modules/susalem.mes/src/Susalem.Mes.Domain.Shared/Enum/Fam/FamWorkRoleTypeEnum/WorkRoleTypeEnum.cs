using System.ComponentModel;
namespace Susalem.Fam.FamWorkRoleTypeEnum
{
    /// <summary>
    /// 岗位角色类型
    /// </summary>
    public enum WorkRoleTypeEnum
    {
        /// <summary>
        /// 制造
        /// </summary>
        [Description("制造")]
        Produce = 0,

        /// <summary>
        /// 质量
        /// </summary>
        [Description("质量")]
        Quality = 1,

        /// <summary>
        /// 物料
        /// </summary>
        [Description("物料")]
        Material = 2,

        /// <summary>
        /// 设备
        /// </summary>
        [Description("设备")]
        Equipment = 3
    }
}
