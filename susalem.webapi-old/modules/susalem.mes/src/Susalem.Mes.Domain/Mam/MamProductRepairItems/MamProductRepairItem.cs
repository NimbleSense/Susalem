using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MesProcessStatusEnum;
using Susalem.Mam.MesRepairStatusEnum;

namespace Susalem.Mam.MamProductRepairItems
{
    /// <summary>
    /// 【领域实体】返修管理-子表
    /// </summary>
    public class Mam_ProductRepairItem : Entity<Guid>, IHasExtraProperties, IHasCreationTime, ISoftDelete
    {
        /// <summary>
        /// 主表ID
        /// </summary>
        [Required]
        public Guid RepairId { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品条码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string SnCode { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid? ProcessId { get; set; }

        /// <summary>
        /// 工位Id
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 工序Index
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 加工状态
        /// </summary>
        [Required]
        public ProcessStatusEnum ProcessStatus { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public RepairStatusEnum Status { get; set; }

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

        public DateTime RepairTime { get; set; }

        /// <summary>
        /// 工位类型
        /// </summary>
        public RepairStationTypeEnum StationType { get; set; }


        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
