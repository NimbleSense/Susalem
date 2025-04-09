using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Eqm.EqmArchives
{
    /// <summary>
    /// 【实体】设备档案
    /// </summary>
    public class Eqm_Archive : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 组件类型ID
        /// </summary>
        public Guid ModuleTypeId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 顺序值  
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 是否易损
        /// </summary>
        [Required]
        public bool IsVulnerable { get; set; }

        /// <summary>
        /// 是否备件
        /// </summary>
        [Required]
        public bool IsSpare { get; set; }

        /// <summary>
        /// 更换次数
        /// </summary>
        [Required]
        public int ReplaceCountLimit { get; set; }

        /// <summary>
        /// 寿命次数
        /// </summary>
        [Required]
        public int LifeCountLimit { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 使用年限
        /// </summary>

        public decimal UsefulLifeLimit { get; set; }

        /// <summary>
        /// 上次维护日期 新增字段
        /// </summary>

        public DateTime? LastMaintenanceDate { get; set; }


        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        protected Eqm_Archive() { }
        public Eqm_Archive(
            Guid id,
            Guid equipmentId,
            Guid moduleTypeId,
           [NotNull] string code,
           [NotNull] string serialNumber,
          [NotNull] int index,
           [NotNull] bool isVulnerable,
           [NotNull] bool isSpare,
           [NotNull] int replaceCountLimit,
           [NotNull] int lifeCountLimit,
           [NotNull] decimal usefulLifeLimit,
           int useCount,
           bool isEnable = true,
           [CanBeNull] ExtraPropertyDictionary extraProperties = null,
           [CanBeNull] string remark = null)
        {
            Id = id;
            EquipmentId = equipmentId;
            ModuleTypeId = moduleTypeId;
            Code = code;
            SerialNumber = serialNumber;
            Index = index;
            IsVulnerable = isVulnerable;
            IsSpare = isSpare;
            ReplaceCountLimit = replaceCountLimit;
            LifeCountLimit = lifeCountLimit;
            UsefulLifeLimit = usefulLifeLimit;
            UseCount = useCount;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
        }
    }
}
