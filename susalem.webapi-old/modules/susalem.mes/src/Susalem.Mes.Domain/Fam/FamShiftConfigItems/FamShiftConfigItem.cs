using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Fam.FamShiftTypeEnum;

namespace Susalem.Fam.FamShiftConfigItems
{
    /// <summary>
    /// 【领域实体】班次管理-子表
    /// </summary>
    public class Fam_ShiftConfigItem : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 班次主表ID
        /// </summary>
        [Required]
        public Guid ShiftConfigId { get; set; }

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
        /// 类型
        /// </summary>
        [Required]
        public ShiftTypeEnum Type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public int StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public int EndTime { get; set; }

        /// <summary>
        /// 垮天标记
        /// </summary>
        [Required]
        public bool IsCrossDay { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Fam_ShiftConfigItem() { }

        /// <summary>
        /// 【领域实体】班次管理-主表
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="shiftConfigId">班次主表Id</param>
        /// <param name="name">名称</param>
        /// <param name="index">序号</param>
        /// <param name="type">类型</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isCrossDay">垮天标记</param>
        /// <param name="extraProperties">拓展字段</param>
        public Fam_ShiftConfigItem(
            Guid id,
            Guid shiftConfigId,
            [NotNull] string name,
            [NotNull] int index,
            [NotNull] ShiftTypeEnum type,
            [NotNull] int startTime,
            [NotNull] int endTime,
            [NotNull] bool isCrossDay,
            [CanBeNull] ExtraPropertyDictionary extraProperties)
        {
            Id = id;
            ShiftConfigId = shiftConfigId;
            Name = name;
            Index = index;
            Type = type;
            StartTime = startTime;
            EndTime = endTime;
            IsCrossDay = isCrossDay;
            ExtraProperties = extraProperties;
        }
    }
}
