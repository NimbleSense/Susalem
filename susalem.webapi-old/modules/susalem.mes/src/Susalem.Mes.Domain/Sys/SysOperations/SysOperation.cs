using JetBrains.Annotations;

using Susalem.Mes;
using Susalem.Mes.Enum;
using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Sys.SysOperations
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [DisableAuditing]
    public class Sys_Operation : CreationAuditedEntity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 所属平台
        /// </summary>
        [Required]
        public OperatePlatformEnum Platform { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MethodName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Required]
        public OperateCategoryEnum OperateCategoryEnum { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 【实体】 操作日志
        /// </summary>
        public Sys_Operation() { }
        
        /// <summary>
        /// 【实体】 操作日志
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="platform">所属平台</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="operateCategoryEnum">操作类型</param>
        /// <param name="content">详细内容</param>
        public Sys_Operation(
            Guid id,
            [NotNull] OperatePlatformEnum platform,
            [NotNull] string moduleName,
            [NotNull] string methodName,
            [NotNull] OperateCategoryEnum operateCategoryEnum,
            [NotNull] string content
            )
        {
            Id = id;
            Platform = platform;
            ModuleName = moduleName;
            MethodName = methodName;
            OperateCategoryEnum = operateCategoryEnum;
            Content = content;
        }

    }
}
