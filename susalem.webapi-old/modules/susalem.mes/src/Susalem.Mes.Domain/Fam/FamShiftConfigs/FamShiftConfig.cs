using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Fam.FamShiftTagEnum;

namespace Susalem.Fam.FamShiftConfigs
{
    /// <summary>
    /// 【领域实体】班次管理-主表
    /// </summary>
    public class Fam_ShiftConfig : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        public ShiftTagEnum Tag { get; set; }

        /// <summary>
        /// 产线ID
        /// </summary>
        [Required]
        public Guid PdLineId { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Fam_ShiftConfig() { }

        /// <summary>
        /// 【领域实体】班次管理-主表
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="tag">标识</param>
        /// <param name="pdLineId">产线ID</param>
        /// <param name="extraProperties">拓展字段</param>
        public Fam_ShiftConfig(
            Guid id,
            [NotNull] string name,
            [NotNull] int index,
            [NotNull] ShiftTagEnum tag,
            [NotNull] Guid pdLineId,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Name = name;
            Index = index;
            Tag = tag;
            PdLineId = pdLineId;
            ExtraProperties = extraProperties;
        }
    }
}
