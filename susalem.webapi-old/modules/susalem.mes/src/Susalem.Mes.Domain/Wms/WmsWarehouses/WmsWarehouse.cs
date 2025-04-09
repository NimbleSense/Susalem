using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Wms.WmsWarehouses
{
    /// <summary>
    /// 【实体】仓库管理
    /// </summary>
    public class Wms_Warehouse : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】设备基础信息
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

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
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Wms_Warehouse() { }

        /// <summary>
        /// 【实体】仓库管理
        /// </summary>
        ///  <param name="id">主键Id</param>
        /// <param name="equipmentId">设备基础信息外键Id</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="extraProperties">拓展字段</param>
        public Wms_Warehouse(
             Guid id,
             [NotNull] Guid equipmentId,
             [NotNull] string code,
             [NotNull] string name,
             [NotNull] DateTime creationTime,
             [CanBeNull] ExtraPropertyDictionary extraProperties
            )
        {
            Id = id;
            EquipmentId = equipmentId;
            Code = code;
            Name = name;
            CreationTime = creationTime;
            ExtraProperties = extraProperties;
        }
    }
}
