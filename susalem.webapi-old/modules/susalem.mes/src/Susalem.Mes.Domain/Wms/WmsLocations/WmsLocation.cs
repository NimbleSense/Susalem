using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Wms.WmsLocationType;

namespace Susalem.Wms.WmsLocations
{
    /// <summary>
    /// 【实体】库位管理
    /// </summary>
    public class Wms_Location : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】仓库Id
        /// </summary>
        [Required]
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// 【外键】货架Id
        /// </summary>
        [Required]
        public Guid ShelfId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public WmsLocationTypeEnum Type { get; set; }

        /// <summary>
        /// 库位坐标X
        /// </summary>
        [Required]
        public int LocationX { get; set; }

        /// <summary>
        /// 库位坐标Y
        /// </summary>
        [Required]
        public int LocationY { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 当前叫料号
        /// </summary>
        public string CurrentMaterialNo { get; set; }

        public Wms_Location() { }

        /// <summary>
        /// 【实体】库位管理
        /// </summary>
        ///  <param name="id">主键Id</param>
        /// <param name="warehouseId">仓库Id</param>
        /// <param name="shelfId">货架Id</param>
        /// <param name="code">编码</param>
        /// <param name="locationX">库位坐标X</param>
        /// <param name="locationY">库位坐标Y</param>
        /// <param name="type">类型</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="extraProperties">拓展字段</param>
        public Wms_Location(
             Guid id,
             [NotNull] Guid warehouseId,
             [NotNull] Guid shelfId,
             [NotNull] string code,
             [NotNull] int locationX,
             [NotNull] int locationY,
             [NotNull] WmsLocationTypeEnum type,
             [NotNull] DateTime creationTime,
             [CanBeNull] ExtraPropertyDictionary extraProperties
            )
        {
            Id = id;
            WarehouseId = warehouseId;
            ShelfId = shelfId;
            Code = code;
            Type = type;
            LocationX = locationX;
            LocationY = locationY;
            CreationTime = creationTime;
            ExtraProperties = extraProperties;
        }
    }
}
