using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Wms.WmsMaterialPositionRules
{
    /// <summary>
    /// 【领域实体】料位规则
    /// </summary>
    public class Wms_MaterialPositionRule : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {

        /// <summary>
        /// 【外键】仓库ID 
        /// </summary>
        [Required]
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// 【外键】货架ID
        /// </summary>
        [Required]
        public Guid ShelfId { get; set; }

        /// <summary>
        /// 【外键】库位ID
        /// </summary>
        [Required]
        public Guid LocationId { get; set; }

        /// <summary>
        /// 【外键】产品类型ID
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 【外键】物料ID
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }


        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【领域实体】料位规则空构造函数
        /// </summary>
        public Wms_MaterialPositionRule()
        {
        }

        /// <summary>
        /// 【领域实体】料位规则
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="shelfId">货架ID</param>
        /// <param name="locationId">库位ID</param>
        /// <param name="productTypeId">产品类型ID</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="creationTime"></param>
        /// <param name="extraProperties">拓展字段</param>
        public Wms_MaterialPositionRule(
          [NotNull] Guid id,
          [NotNull] Guid warehouseId,
          [NotNull] Guid shelfId,
          [NotNull] Guid locationId,
          [NotNull] Guid productTypeId,
          [NotNull] Guid materialId,
          [NotNull] DateTime creationTime,
          [CanBeNull] ExtraPropertyDictionary extraProperties
          )
        {
            Id = id;
            WarehouseId = warehouseId;
            ShelfId = shelfId;
            LocationId = locationId;
            ProductTypeId = productTypeId;
            MaterialId = materialId;
            CreationTime = creationTime;
            ExtraProperties = extraProperties;
        }
    }
}
