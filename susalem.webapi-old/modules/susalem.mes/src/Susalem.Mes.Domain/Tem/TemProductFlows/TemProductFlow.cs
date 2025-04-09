using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Tem.TemProductFlows
{

    /// <summary>
    /// 【实体】产品工艺
    /// </summary>
    public class Tem_ProductFlow : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 【外键】产品类型ID
        /// </summary>
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 是否为测试工艺
        /// </summary>
        [Required] 
        public bool IsTest { get; set; }

        /// <summary>
        /// 默认产品工艺
        /// </summary>
        [Required]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// 工艺版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Version { get; set; }

        /// <summary>
        /// 标准节拍时间
        /// </summary>
        public int CycleTime { get; set; } = 0;

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        protected Tem_ProductFlow() { }

        /// <summary>
        /// 产品工艺
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="productTypeId">物料类型</param>
        /// <param name="isTest">是否是测试</param>
        /// <param name="version">版本号</param>
        /// <param name="cycleTime">标准节拍时间</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">描述</param>
        /// <param name="isDefault">默认产品工艺</param>
        public Tem_ProductFlow(
            Guid id,
           [NotNull] string code,
           [NotNull] string name, 
           [NotNull] Guid productTypeId, 
           [NotNull] bool isTest,
           [NotNull]bool isDefault = false,
           [CanBeNull]string version=null,
           [CanBeNull]int cycleTime=0,
           [CanBeNull] ExtraPropertyDictionary extraProperties=null,
           bool isEnable =true,
           [CanBeNull] string remark=null)
        {
            Id = id;
            Code = code;
            Name = name;
            ProductTypeId = productTypeId;
            IsTest = isTest;
            Version = version;
            CycleTime = cycleTime;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
            IsDefault = isDefault;
        }
    }
}
