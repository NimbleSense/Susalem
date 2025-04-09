using System.ComponentModel;

namespace Susalem
{
    /// <summary>
    /// 【枚举】操作类型（操作日志）
    /// </summary>
    public enum OperateCategoryEnum
    {
        /// <summary>
        /// 资源新增
        /// </summary>
        [Description("新增")]
        Post = 0,

        /// <summary>
        /// 资源删除
        /// </summary>
        [Description("删除")]
        Delete = 1,

        /// <summary>
        /// 资源更新
        /// </summary>
        [Description("修改")]
        Put = 2,

        /// <summary>
        /// 标准
        /// </summary>
        [Description("标准")]
        Normal = 3,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 4,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Error = 5,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("警告")]
        Warning = 6
    }
}
