using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Eqm.EqmModuleTypes
{
    /// <summary>
    /// 【实体】 设备组件类型 
    /// </summary>
    public class Eqm_ModuleType : FullAuditedEntity<Guid>, IHasExtraProperties
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
        /// 型号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Model { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Brand { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Category { get; set; }

        /// <summary>
        /// 更换次数 用于预防性维护0代表无更换次数限制
        /// </summary>
        [Required]
        public int ReplaceCountLimit { get; set; }

        /// <summary>
        /// 寿命次数 用于预防性维护0代表无更换次数限制
        /// </summary>
        [Required]
        public int LifeCountLimit { get; set; }

        /// <summary>
        /// 使用年限 用于预防性维护0代表无更换次数限制
        /// </summary>
        [Required]
        public decimal UsefulLifeLimit { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        protected Eqm_ModuleType() { }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="replaceCountLimit">更换次数</param>
        /// <param name="lifeCountLimit">寿命次数</param>
        /// <param name="usefulLifeLimit">使用年限</param>
        /// <param name="model">型号</param>
        /// <param name="brand">品牌</param>
        /// <param name="category">类别</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark"></param>
        public Eqm_ModuleType(
            [NotNull] string code,
            [NotNull] string name,
            [NotNull] int replaceCountLimit,
            [NotNull] int lifeCountLimit,
            [NotNull]decimal usefulLifeLimit,
            [CanBeNull] string model = null,
            [CanBeNull] string brand = null,
            [CanBeNull] string category = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null,
            bool isEnable = true,
            [CanBeNull] string remark = null)
        {
            Code = code;
            Name = name;
            Model = model;
            Brand = brand;
            Category = category;
            ReplaceCountLimit = replaceCountLimit;
            LifeCountLimit = lifeCountLimit;
            UsefulLifeLimit = usefulLifeLimit;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
        }
    }
}
