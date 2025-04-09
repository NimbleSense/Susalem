using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Fam
{
    /// <summary>
    /// 【实体】工厂管理
    /// </summary>
    public class Fam_Factory : FullAuditedEntity<Guid>, IHasExtraProperties
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
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 工厂实体
        /// </summary>
        protected Fam_Factory() { }

        /// <summary>
        /// 工厂实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">编码</param>
        /// <param name="name">姓名</param>
        /// <param name="isEnable">是否启用（默认：启用）</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        public Fam_Factory(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            bool isEnable = true,
            [CanBeNull] string remark = null,
             ExtraPropertyDictionary extraProperties=null)
        {
            Id = id;
            Code = code;
            Name = name;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
