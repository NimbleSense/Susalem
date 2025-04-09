using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;

using Susalem.Mam.MamMasterStatusEnum;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Mam.MamMasterRepairs
{
    /// <summary>
    /// 【领域实体】Master件维护-主表
    /// </summary>
    public class Mam_MasterRepair : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// SnCode
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string SnCode { get; set; }

        /// <summary>
        /// 总成码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string SerialNo { get; set; }

        /// <summary>
        /// 槽号
        /// </summary>
        public Guid? Slot { get; set; }

        /// <summary>
        /// 产品类型Id
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

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
