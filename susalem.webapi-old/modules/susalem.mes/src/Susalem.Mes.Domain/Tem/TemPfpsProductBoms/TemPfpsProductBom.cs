using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemPfpsProductBoms
{
    /// <summary>
    /// 【领域实体】Bom
    /// </summary>
    public class Tem_PfpsProductBom : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】物料ID
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 【外键】工艺工步ID
        /// </summary>
        [Required]
        public Guid StepId { get; set; }

        /// <summary>
        /// 【外键】产品工艺工序Id
        /// </summary>
        [Required]
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 类型（字典）
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Type { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 是否为父级
        /// </summary>
        [Required]
        public bool IsParentPart { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【领域实体】Bom
        /// </summary>
        protected Tem_PfpsProductBom()
        {

        }
        /// <summary>
        /// 【领域实体】Bom
        /// </summary>
        /// <param name="id"></param>
        /// <param name="materialId">物料ID</param>
        /// <param name="stepId">工艺工步ID</param>
        /// <param name="productFlowProcessId">产品工艺工序Id</param>
        /// <param name="type">类型</param>
        /// <param name="qty">数量</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        protected Tem_PfpsProductBom(
             Guid id,
            [NotNull] Guid materialId,
            [NotNull] Guid stepId,
            [NotNull] Guid productFlowProcessId,
            [NotNull] string type,
            [NotNull] decimal qty,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            MaterialId = materialId;
            StepId = stepId;
            ProductFlowProcessId = productFlowProcessId;
            Type = type;
            Qty = qty;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
