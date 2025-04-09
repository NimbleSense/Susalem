using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Fam.FamShiftTypeEnum;

namespace Susalem.Fam.FamDayShifts
{
    /// <summary>
    /// 【领域实体 排班管理-主表
    /// </summary>
    public class Fam_DayShift : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 产品线ID
        /// </summary>
        [Required]
        public Guid PdLineId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public ShiftTypeEnum Type { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Fam_DayShift() { }

        /// <summary>
        /// 【领域实体】排班管理-主表
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="pdLineId">产品线Id</param>
        /// <param name="date">日期</param>
        /// <param name="type">类型</param>
        /// <param name="dataSourceType">类型</param>
        /// <param name="extraProperties">拓展字段</param>
        public Fam_DayShift(
            Guid id,
            [NotNull] Guid pdLineId,
            [NotNull] DateTime date,
            [NotNull] ShiftTypeEnum type,
            [NotNull] string dataSourceType,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            PdLineId = pdLineId;
            Date = date;
            Type = type;
            DataSourceType = dataSourceType;
            ExtraProperties = extraProperties;
        }
    }
}
