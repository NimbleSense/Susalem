using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Fam.FamWorkShops
{
    /// <summary>
    /// 【领域实体】车间管理
    /// </summary>
    public class Fam_WorkShop : FullAuditedEntity<Guid>, IHasExtraProperties
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
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【领域实体】车间管理
        /// </summary>
        protected Fam_WorkShop()
        {

        }

        /// <summary>
        /// 【领域实体】车间管理
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="factoryId">【外键】工厂ID</param>
        public Fam_WorkShop(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            Guid factoryId,
            bool isEnable = true,
            [CanBeNull] string remark = null)
        {
            Id = id;
            Code = code;
            Name = name;
            IsEnable = isEnable;
            Remark = remark;
            FactoryId = factoryId;
        }
    }
}
