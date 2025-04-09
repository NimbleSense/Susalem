using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Sys
{
    /// <summary>
    /// 编码规则
    /// </summary>
    public class Sys_CodeRule : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [Required]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块标记
        /// </summary>
        [StringLength(50)]
        public string ModuleTag { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Prefix { get; set; }

        /// <summary>
        /// 规则类型（日期格式）
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string CodeRuleType { get; set; }

        /// <summary>
        /// 流水号长度（小于11位）
        /// </summary>
        [Required, Range(1, 11)]
        public int SerialNumberLength { get; set; }

        /// <summary>
        /// 当前流水号
        /// </summary>
        public long CurrentSerialNumber { get; set; }

        /// <summary>
        /// 间隔值（大于0，小于1000）
        /// </summary>
        [Required, Range(1, 1000)]
        public int IntervalValue { get; set; }

        /// <summary>
        /// 补位类型
        /// </summary>
        [Required]
        public ComplementTypeEnum ComplementType { get; set; }

        /// <summary>
        /// 补位符（默认：0）
        /// </summary>
        [Required, StringLength(5)]
        public string ComplementMark { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 编码规则
        /// </summary>
        protected Sys_CodeRule() { }

        public Sys_CodeRule(
            [NotNull] string moduleCode,
            [NotNull] string codeRuleType,
            [NotNull] string prefix,
            [NotNull] int serialNumberLength,
            [NotNull] long currentSerialNumber,
            [NotNull] int intervalValue,
            [NotNull] ComplementTypeEnum complementType,
            [NotNull] string complementMark = null,
            [CanBeNull] string moduleTag = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            ModuleCode = moduleCode;
            CodeRuleType = codeRuleType;
            Prefix = prefix;
            SerialNumberLength = serialNumberLength;
            CurrentSerialNumber = currentSerialNumber;
            IntervalValue = intervalValue;
            ComplementType = complementType;
            ComplementMark = complementMark;
            ModuleTag = moduleTag;
            ExtraProperties = extraProperties;
        }
    }
}
