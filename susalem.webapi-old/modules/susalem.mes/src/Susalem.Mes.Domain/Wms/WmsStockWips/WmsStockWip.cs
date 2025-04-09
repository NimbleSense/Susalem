using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Wms.WmsStockWips
{
    /// <summary>
    /// 【领域实体】实时库存
    /// </summary>
    public class Wms_StockWip : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】仓库ID 
        /// </summary>
        [Required]
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Required]
        public string WarehouseCode { get; set; }

        /// <summary>
        /// 【外键】设备ID
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string EquipmentCode { get; set; }

        /// <summary>
        /// 【外键】货架ID
        /// </summary>
        [Required]
        public Guid ShelfId { get; set; }

        /// <summary>
        /// 货架编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ShelfCode { get; set; }

        /// <summary>
        /// 【外键】库位ID
        /// </summary>
        [Required]
        public Guid LocationId { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string LocationCode { get; set; }

        /// <summary>
        /// 【外键】物料ID
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 物料编码-版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialCodeWithVer { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [Required]
        public decimal Qty { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength128)]
        public string BatchNo { get; set; }

        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【领域实体】实时库存构造函数
        /// </summary>
        public Wms_StockWip()
        {

        }

        /// <summary>
        /// 【领域实体】实时库存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="warehouseCode">仓库编码</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="equipmentCode">设备编码</param>
        /// <param name="shelfId">货架ID</param>
        /// <param name="shelfCode">货架编码</param>
        /// <param name="locationId">库位ID</param>
        /// <param name="locationCode">库位编码</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="qty">库存数量</param>
        /// <param name="batchNo">批次号</param>
        /// <param name="creationTime">创建时间</param>
        public Wms_StockWip(
        [NotNull] Guid id,
        [NotNull] Guid warehouseId,
        [NotNull] string warehouseCode,
        [NotNull] Guid equipmentId,
        [NotNull] string equipmentCode,
        [NotNull] Guid shelfId,
        [NotNull] string shelfCode,
        [NotNull] Guid locationId,
        [NotNull] string locationCode,
        [NotNull] Guid materialId,
        [NotNull] decimal qty,
        [CanBeNull] string batchNo,
        [NotNull] DateTime creationTime)
        {
            Id = id;
            WarehouseId = warehouseId;
            WarehouseCode = warehouseCode;
            EquipmentId = equipmentId;
            EquipmentCode = equipmentCode;
            ShelfCode = shelfCode;
            ShelfId = shelfId;
            LocationId = locationId;
            LocationCode = locationCode;
            MaterialId = materialId;
            Qty = qty;
            BatchNo = batchNo;
            CreationTime = creationTime;
        }
    }
}
