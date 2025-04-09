using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MamMasterStatusEnum;
using Susalem.Qms.QmsQualityStatusEnum;

namespace Susalem.Mam.MamMasterRepairChecks
{
    /// <summary>
    /// 【领域实体】Master件维护-检测表
    /// </summary>
    public class Mam_MasterRepairCheck : Entity<Guid>, IHasExtraProperties, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// 主表ID
        /// </summary>
        [Required]
        public Guid MasterId { get; set; }

        /// <summary>
        /// 产品SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string SnCode { get; set; }

        /// <summary>
        /// 产品类型id
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 产品类型编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCode { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [Required]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessCode { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 工序序号
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 设备id 【外键】
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string EquipmentCode { get; set; }

        /// <summary>
        /// 参数编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ParaName { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ParaType { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        [Required]
        public decimal StandardValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength20)]
        public string Unit { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [Required]
        public decimal MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [Required]
        public decimal MinValue { get; set; }

        /// <summary>
        /// 一级序号
        /// </summary>
        [Required]
        public int FirstIndex { get; set; }

        /// <summary>
        /// 二级序号
        /// </summary>
        [Required]
        public int SecondIndex { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        public decimal Value { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public QualityStatusEnum QualityStatus { get; set; }

        /// <summary>
        /// 是否最新
        /// </summary>
        [Required]
        public bool IsNew { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string CycleCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public MasterStatusEnum StationStatus { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

    }
}
