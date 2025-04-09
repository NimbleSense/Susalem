using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;

namespace Susalem.Fam.FamPdLines
{
    /// <summary>
    /// 【领域实体】产线管理
    /// </summary>
    public class Fam_PdLine : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 【外键】工厂ID
        /// </summary>
        [Required]
        public Guid FactoryId { get; set; }

        /// <summary>
        /// 【外键】车间ID
        /// </summary>
        public Guid WorkShopId { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【领域实体】产线管理
        /// </summary>
        protected Fam_PdLine() { }

        /// <summary>
        /// 【领域实体】产线管理
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="factoryId">【外键】工厂ID</param>
        /// <param name="workShopId">【外键】车间ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        public Fam_PdLine(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            Guid factoryId,
            Guid workShopId,
            bool isEnable = true,
            [CanBeNull] string remark = null)
        {
            Id = id;
            Code = code;
            Name = name;
            IsEnable = isEnable;
            Remark = remark;
            FactoryId = factoryId;
            WorkShopId = workShopId;
        }
    }
}
