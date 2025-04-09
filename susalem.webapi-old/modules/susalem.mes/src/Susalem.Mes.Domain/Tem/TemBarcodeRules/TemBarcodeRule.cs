using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemBarcodeRules
{
    /// <summary>
    /// 【领域实体】产品条码
    /// </summary>
    public class Tem_BarcodeRule : CreationAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】物料Id
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        ///  名字 
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 【领域实体】产品条码
        /// </summary>
        protected Tem_BarcodeRule()
        {

        }

    }
}
