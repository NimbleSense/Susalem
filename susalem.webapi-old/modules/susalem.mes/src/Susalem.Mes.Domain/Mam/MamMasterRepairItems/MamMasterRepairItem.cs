using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
using Susalem.Mam.MamMasterStatusEnum;

namespace Susalem.Mam.MamMasterRepairItems
{
    /// <summary>
    /// 【领域实体】Master件维护-子表
    /// </summary>
    public class Mam_MasterRepairItem : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 主表ID
        /// </summary>
        [Required]
        public Guid MasterId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [Required]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 工位ID
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 工序Index
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public MasterStatusEnum Status { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
