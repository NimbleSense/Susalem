using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Tem.TemProductFlowTimeSpans
{
    /// <summary>
    /// 【实体】工序时间管控
    /// </summary>
    public class Tem_ProductFlowTimeSpan : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】产品工艺工序
        /// </summary>
        [Required]
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 【外键】前置工序ID
        /// </summary>
        [Required]
        public Guid PreProcessId { get; set; }

        /// <summary>
        /// 【枚举】管控类型
        /// </summary>
        [Required]
        public ControlTypeEnum ControlType { get; set; }

        /// <summary>
        /// 时长（秒）
        /// </summary>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        [Required]
        public int TimeType { get; set; }

        /// <summary>
        /// 提示说明
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength1000)]
        public string Description { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="productFlowProcessId">产品工艺工序</param>
        /// <param name="preProcessId">前置工序ID</param>
        /// <param name="controlType">管控类型</param>
        /// <param name="duration">时长</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">描述</param>
        /// <param name="description">描述</param>
        /// <param name="extraProperties">拓展字段</param>
        public Tem_ProductFlowTimeSpan(
            [NotNull] Guid productFlowProcessId,
            [NotNull] Guid preProcessId,
            [NotNull] ControlTypeEnum controlType,
            [NotNull] int duration,
            [NotNull] int timeType,
            [CanBeNull] string description = null,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            ProductFlowProcessId = productFlowProcessId;
            PreProcessId = preProcessId;
            ControlType = controlType;
            Duration = duration;
            TimeType = timeType;
            Description = description;
            ExtraProperties = extraProperties;
            Remark = remark;
            IsEnable = isEnable;
        }
    }
}
