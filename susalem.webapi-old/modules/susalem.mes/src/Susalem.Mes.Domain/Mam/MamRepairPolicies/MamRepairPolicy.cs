using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Mam.MamRepairPolicies
{
    /// <summary>
    /// 【领域实体】返修策略
    /// </summary>
    public class Mam_RepairPolicy : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// Guid 用于状态修改
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string GuId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string AppId { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 产品类型Id
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 产品类型-版本
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string SnCode { get; set; }

        /// <summary>
        /// 总成码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string TraceCode { get; set; }

        /// <summary>
        /// 工位编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string WorkStationNo { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public DateTime OverTime { get; set; }

        /// <summary>
        /// 检查人
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessPeople { get; set; }

        /// <summary>
        /// 处置方式代码
        /// </summary>
        [Required]
        public string DisposalSuggestions { get; set; }

        /// <summary>
        /// 问题描述集合
        /// </summary>
        public string IssueList { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
