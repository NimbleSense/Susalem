using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Wms.WmsShelfs
{
    /// <summary>
    /// 【实体】货架配置
    /// </summary>
    public class Wms_Shelf : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】仓库管理Id
        /// </summary>
        [Required]
        public Guid WarehouseId { get; set; }

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
        /// 顺序号
        /// </summary>
        [Required]
        public int Index { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Wms_Shelf() { }

        /// <summary>
        /// 【实体】货架配置
        /// </summary>
        ///  <param name="id">主键Id</param>
        /// <param name="warehouseId">仓库管理Id</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="index">顺序号</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="extraProperties">拓展字段</param>
        public Wms_Shelf(
             Guid id,
             [NotNull] Guid warehouseId,
             [NotNull] string code,
             [NotNull] string name,
             [NotNull] int index,
             [NotNull] DateTime creationTime,
             [CanBeNull] ExtraPropertyDictionary extraProperties
            )
        {
            Id = id;
            WarehouseId = warehouseId;
            Code = code;
            Name = name;
            Index = index;
            CreationTime = creationTime;
            ExtraProperties = extraProperties;
        }
    }
}
