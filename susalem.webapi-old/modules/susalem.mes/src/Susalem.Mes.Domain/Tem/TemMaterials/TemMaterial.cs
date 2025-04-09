using JetBrains.Annotations;

using Newtonsoft.Json;

using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
using Volo.Abp.Data;

namespace Susalem.Tem.TemMaterials
{
    /// <summary>
    /// 【实体】物料管理
    /// </summary>
    public class Tem_Material : FullAuditedEntity<Guid>, IHasExtraProperties
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
        [StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string Model { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MaterialTypeEnum Type { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Property { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Unit { get; set; }

        /// <summary>
        /// 追溯类型
        /// </summary>
        public MaterialTraceTypeEnum TraceType { get; set; }

        /// <summary>
        /// 可装配次数
        /// </summary>
        public int TryProcessCount { get; set; } = 1;

        /// <summary>
        /// 零件号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string PartNumber { get; set; }

        /// <summary>
        /// 电机标签
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MotorTag { get; set; }

        /// <summary>
        /// 安全库存数
        /// </summary>
        [Required]
        public decimal SafeQty { get; set; }

        /// <summary>
        /// 零件损耗比// 配送预留量
        /// </summary>
        [Required]
        public decimal ReservedQty { get; set; }

        /// <summary>
        /// 断点版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string SubstituteCodeWithVer { get; set; }

        /// <summary>
        /// 断点生效时间
        /// </summary>
        public DateTime? UseSubstituteStartTime { get; set; }

        /// <summary>
        /// 断点结束时间
        /// </summary>
        public DateTime? UseSubstituteEndTime { get; set; }

        /// <summary>
        /// 大机型
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string LargeModel { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【实体】物料管理
        /// </summary>
        protected Tem_Material() { }

        /// <summary>
        /// 【实体】物料管理
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="unit">单位</param>
        /// <param name="traceType">追溯类型</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="property">属性</param>
        /// <param name="model">型号</param>
        /// <param name="tryProcessCount">可装配次数</param>
        /// <param name="largeModel">大机型</param>
        /// <param name="extraProperties">拓展字段</param>
        public Tem_Material(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            [NotNull] MaterialTypeEnum type,
            [NotNull] string unit,
            [NotNull] MaterialTraceTypeEnum traceType,
            bool isEnable = true,
            int tryProcessCount = 1,
            [CanBeNull] string remark = null,
            [CanBeNull] string property = null,
            [CanBeNull] string model = null,
            string largeModel=null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Code = code;
            Name = name;
            Model = model;
            Type = type;
            Property = property;
            Unit = unit;
            TraceType = traceType;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
            TryProcessCount = tryProcessCount;
            LargeModel = largeModel;
        }
    }
}
