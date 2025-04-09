using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Fam.FamShiftTagEnum;
using Susalem.Fam.FamShiftTypeEnum;

namespace Susalem.Fam.FamDayShiftItems
{
    /// <summary>
    /// 【领域实体 排班管理-子表
    /// </summary>
    public class Fam_DayShiftItem : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 排版主表Id
        /// </summary>
        [Required]
        public Guid DayShiftId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 班次日期
        /// </summary>
        [Required]
        public DateTime ShiftDate { get; set; }

        /// <summary>
        /// 班次标识
        /// </summary>
        [Required]
        public ShiftTagEnum ShiftTag { get; set; }

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
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Fam_DayShiftItem() { }

        /// <summary>
        /// 【领域实体】排班管理-子表
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="dayShiftId">排班主表Id</param>
        /// <param name="name">名称</param>
        /// <param name="shiftDate">班次日期</param>
        /// <param name="shiftTag">班次标识</param>
        /// <param name="index">序号</param>
        /// <param name="type">类型</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="duration">时长</param>
        /// <param name="extraProperties">拓展字段</param>
        public Fam_DayShiftItem(
            Guid id,
            [NotNull] Guid dayShiftId,
            [NotNull] string name,
            [NotNull] DateTime shiftDate,
            [NotNull] ShiftTagEnum shiftTag,
            [NotNull] int index,
            [NotNull] ShiftTypeEnum type,
            [NotNull] DateTime startTime,
            [NotNull] DateTime endTime,
            [NotNull] int duration,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            DayShiftId = dayShiftId;
            Name = name;
            ShiftDate = shiftDate;
            ShiftTag = shiftTag;
            Index = index;
            Type = type;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            ExtraProperties = extraProperties;
        }
    }
}
