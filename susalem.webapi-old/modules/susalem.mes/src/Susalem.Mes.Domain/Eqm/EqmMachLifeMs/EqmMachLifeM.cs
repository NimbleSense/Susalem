using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;

namespace Susalem.Eqm.Susalem.MachLifeMs
{
    /// <summary>
    /// 【实体】预防性维护
    /// </summary>
    public class Eqm_MachLifeM : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】设备Id
        /// </summary>
        public Guid? EqmEquipmentId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string EqmEquipmentLabel { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Project { get; set; }

        /// <summary>
        /// 频率
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        protected Eqm_MachLifeM() { }
        /// <summary>
        /// 【实体】预防性维护
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="eqmEquipmentId">设备Id</param>
        /// <param name="eqmEquipmentLabel">设备编号</param>
        /// <param name="frequency">频率</param>
        /// <param name="project">项目</param>
        /// <param name="extraProperties">拓展字段</param>
        public Eqm_MachLifeM(
            Guid id,
            Guid eqmEquipmentId,
            [NotNull] string eqmEquipmentLabel,
            [NotNull] double frequency,
            [CanBeNull] string project = null,
            ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            EqmEquipmentId = eqmEquipmentId;
            EqmEquipmentLabel = eqmEquipmentLabel;
            Frequency = frequency;
            Project = project;
            ExtraProperties = extraProperties;
        }
    }
}
