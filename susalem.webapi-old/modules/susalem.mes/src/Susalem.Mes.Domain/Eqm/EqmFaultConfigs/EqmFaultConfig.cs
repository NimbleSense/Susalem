using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Eqm.EqmFaultConfigs
{
    /// <summary>
    /// 故障配置
    /// </summary>
    public class Eqm_FaultConfig : CreationAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】 设备I
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Sequence { get; set; }

        /// <summary>
        /// 故障码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 故障类型
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Type { get; set; }

        /// <summary>
        /// 故障等级
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Level { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        protected Eqm_FaultConfig() { }
        public Eqm_FaultConfig(
            Guid id,
           [NotNull] Guid equipmentId,
           [NotNull] string sequence,
           [NotNull] string name,
           [NotNull] string type,
           [NotNull] string level,
           [CanBeNull] string remark = null)
        {
            Id = id;
            EquipmentId = equipmentId;
            Sequence = sequence;
            Name = name;
            Type = type;
            Level = level;
            Remark = remark;
        }
    }
}
