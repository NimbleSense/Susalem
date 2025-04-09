using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
using Volo.Abp.Data;

namespace Susalem.Mam.MamMasterRepairConfigs
{
    /// <summary>
    /// Master件维护-质量参数表
    /// </summary>
    public class Mam_MasterRepairConfig : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 主表ID
        /// </summary>
        [Required]
        public Guid MasterId { get; set; }

        /// <summary>
        /// 子表ID
        /// </summary>
        [Required]
        public Guid MasterItemId { get; set; }

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
        /// 【外键】参数类型ID
        /// </summary>
        [Required]
        public Guid ParaTypeId { get; set; }

        /// <summary>
        /// 数据个数
        /// </summary>
        [Required]
        public int Count { get; set; } = 1;

        /// <summary>
        /// 装配次数 默认为：3，代表可以复拧2次，正常加工1次
        /// </summary>
        [Required]
        public int AssembleCount { get; set; } = 3;

        /// <summary>
        /// 标准值
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal StandardValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength20)]
        public string Unit { get; set; }

        /// <summary>
        /// 数据个数
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal MinValue { get; set; }

        /// <summary>
        /// 一级序号  
        /// </summary>
        [Required]
        public int FirstIndex { get; set; }

        /// <summary>
        /// 二级序号  界面中显示为参数序号
        /// </summary>
        [Required]
        public int SecondIndex { get; set; }

        /// <summary>
        /// 是否验证完整
        /// </summary>
        [Required]
        public bool IsIntegrity { get; set; } = true;

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【领域实体】工艺参数
        /// </summary>
        protected Mam_MasterRepairConfig()
        {

        }

        /// <summary>
        /// 【领域实体】工艺参数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="paraTypeId">参数类型ID</param>
        /// <param name="count">数据个数</param>
        /// <param name="assembleCount">装配次数</param>
        /// <param name="standardValue">单位</param>
        /// <param name="unit">单位</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="firstIndex">一级序号</param>
        /// <param name="secondIndex">二级序号</param>
        /// <param name="isIntegrity">是否验证完整</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        public Mam_MasterRepairConfig(
             Guid id,
            [NotNull] string code,
            [NotNull] string name,
            [NotNull] Guid paraTypeId,
            [NotNull] int count,
            [NotNull] int assembleCount,
            [NotNull] decimal standardValue,
            [NotNull] string unit,
            [NotNull] decimal maxValue,
            [NotNull] decimal minValue,
            [NotNull] int firstIndex,
            [NotNull] int secondIndex,
            [NotNull] bool isIntegrity,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Code = code;
            Name = name;
            ParaTypeId = paraTypeId;
            Count = count;
            AssembleCount = assembleCount;
            StandardValue = standardValue;
            Unit = unit;
            MaxValue = maxValue;
            MinValue = minValue;
            FirstIndex = firstIndex;
            SecondIndex = secondIndex;
            IsIntegrity = isIntegrity;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
