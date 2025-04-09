using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MesRepairStatusEnum;

namespace Susalem.Mam.MamProductRepairs
{
    /// <summary>
    /// 【领域实体】返修管理-主表
    /// </summary>
    public class Mam_ProductRepair : Entity<Guid>, IHasExtraProperties, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// 产品条码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string SnCode { get; set; }

        /// <summary>
        /// 拆解工单
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string WorkOrderNo { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public RepairStatusEnum Status { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        [StringLength(CommonConsts.StandardFiledMaxLength)]
        /// <summary>
        /// 返修类型 正常返修 和 偏差回用
        /// </summary>
        public string RepairType { get; set; }
        /// <summary>
        /// 下线时的工序
        /// </summary>
        public Guid NgProcessId { get; set; }
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string NgProcessCode { get; set; }
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string NgProcessName { get; set; }

        /// <summary>
        /// 下线时的工位
        /// </summary>
        public Guid NgEquipmentId { get; set; }
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string NgEquipmentCode { get; set; }
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string NgEquipmentName { get; set; }

        /// <summary>
        /// 下线时的工序号
        /// </summary>
        public int NgIndex { get; set; }

    }
}
