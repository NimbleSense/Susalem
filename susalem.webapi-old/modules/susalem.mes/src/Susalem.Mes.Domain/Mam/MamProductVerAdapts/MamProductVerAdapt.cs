using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Mam.MamProductVerAdapts
{
    /// <summary>
    /// 【领域实体】产品版本适配表
    /// </summary>
    public class Mam_ProductVerAdapt : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 物料id
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 产品物料版本
        /// </summary>
        [Required,StringLength(50)]
        public string ProductTypeNo { get; set; }

        /// <summary>
        /// 产品物料适配版本
        /// </summary>
        [MaxLength]
        public string ProductVerAdapts { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        [MaxLength]
        public ExtraPropertyDictionary ExtraProperties { get; }
    }
}
