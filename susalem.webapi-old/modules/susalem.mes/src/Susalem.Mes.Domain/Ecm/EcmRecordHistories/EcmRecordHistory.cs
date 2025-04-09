using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Ecm.EcmRecordHistoryEnum;

namespace Susalem.Ecm.EcmRecordHistories
{
    /// <summary>
    /// 【实体】 能耗历史记录
    /// </summary>
    public class Ecm_RecordHistory :  Entity<Guid>, IHasExtraProperties, IHasCreationTime
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
        /// 抄表值
        /// </summary>
        [Required,]
        public decimal MeterRecordValue { get; set; }

        /// <summary>
        /// 开始期间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 开始期间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 【外键】 排班子表Id
        /// </summary>
        [Required]
        public Guid DayShiftItemId { get; set; }

        /// <summary>
        /// 采集方式
        /// </summary>
        [Required]
        public RecordHistoryCollectTypeEnum CollectType{ get; set; }

        /// <summary>
        /// 【外键】 抄表人Id
        /// </summary>
        public Guid? EmployeeId { get; set; }

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
        /// 【实体】 能耗历史记录
        /// </summary>
        protected Ecm_RecordHistory() { }

        /// <summary>
        /// 【实体】 能耗历史记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="structItemId">能耗子表Id</param>
        /// <param name="value">用量</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="dayShiftItemId">排班子表Id</param>
        /// <param name="collectType">采集类型</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="creationTime">创建时间</param>
        public Ecm_RecordHistory(
            Guid id,
            Guid structItemId,
            decimal value,
            DateTime startTime,
            DateTime endTime,
            Guid dayShiftItemId,
            [NotNull]  RecordHistoryCollectTypeEnum collectType,
            [CanBeNull] ExtraPropertyDictionary extraProperties,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            StructItemId = structItemId;
            Value = value;
            StartTime = startTime;
            EndTime = endTime;
            DayShiftItemId = dayShiftItemId;
            CollectType = collectType;
            ExtraProperties = extraProperties;
            CreationTime = creationTime;
        }
    }
}
