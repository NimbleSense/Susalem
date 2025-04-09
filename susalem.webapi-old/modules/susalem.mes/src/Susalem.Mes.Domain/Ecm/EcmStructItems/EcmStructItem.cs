using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Ecm.EcmStructItemEnum;

namespace Susalem.Ecm.EcmStructItems
{
    /// <summary>
    /// 【实体】 能耗分项管理子表
    /// </summary>
    public class Ecm_StructItem : FullAuditedEntity<Guid>, IHasExtraProperties
    {

        /// <summary>
        /// 【外键】 能耗主表Id
        /// </summary>
        [Required]
        public Guid StructId { get; set; }

        /// <summary>
        /// 【外键】 设备Id
        /// </summary>
        public Guid? EquipmentId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string Name { get; set; }

        /// <summary>
        /// 能耗类型
        /// </summary>
        [Required]
        public StructItemEnergyTypeEnum EnergyType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Unit { get; set; }

        /// <summary>
        /// 计量类型
        /// </summary>
        [Required]
        public StructItemCalcTypeEnum CalcType { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【实体】 能耗分项管理子表
        /// </summary>
        protected Ecm_StructItem() { }

        /// <summary>
        /// 【实体】 能耗分项管理子表
        /// </summary>
        /// <param name="structId">能耗主表Id</param>
        /// <param name="equipmentId">设备Id</param>
        /// <param name="id">主键</param>
        /// <param name="code">编号</param>
        /// <param name="name">名称</param>
        /// <param name="energyType">能耗类型</param>
        /// <param name="unit">单位</param>
        /// <param name="calcType">计量单位</param>
        /// <param name="extraProperties">拓展字段</param>
        public Ecm_StructItem(
            Guid structId,
            Guid equipmentId,
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            StructItemEnergyTypeEnum energyType,
            [NotNull] string unit,
            [NotNull] StructItemCalcTypeEnum calcType,
            [CanBeNull] ExtraPropertyDictionary extraProperties)
        {
            StructId = structId;
            EquipmentId = equipmentId;
            Id = id;
            Code = code;
            Name = name;
            EnergyType = energyType;
            Unit = unit;
            CalcType = calcType;
            ExtraProperties = extraProperties;
        }
    }
}
