using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Ecm.EcmRecordWips
{
    /// <summary>
    /// 【实体】 能耗实时记录
    /// </summary>
    public class Ecm_RecordWip :  Entity<Guid>, IHasExtraProperties, IHasCreationTime
    {

        /// <summary>
        /// 【外键】能耗分项子表ID
        /// </summary>
        public Guid StructItemId { get; set; }

        /// <summary>
        /// 使用量
        /// </summary>
        [Required,]
        public decimal Value { get; set; }

        /// <summary>
        /// 开始期间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 【外键】 排班子表Id
        /// </summary>
        [Required]
        public Guid DayShiftItemId { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 【实体】 能耗实时记录
        /// </summary>
        protected Ecm_RecordWip() { }

        /// <summary>
        /// 【实体】 能耗实时记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="structItemId">能耗子表Id</param>
        /// <param name="value">用量</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="dayShiftItemId">排班子表Id</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="creationTime">创建时间</param>
        public Ecm_RecordWip(
            Guid id,
            Guid structItemId,
            decimal value,
            DateTime startTime,
            Guid dayShiftItemId,
            [CanBeNull] ExtraPropertyDictionary extraProperties,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            StructItemId = structItemId;
            Value = value;
            StartTime = startTime;
            DayShiftItemId = dayShiftItemId;
            ExtraProperties = extraProperties;
            CreationTime = creationTime;
        }
    }
}
