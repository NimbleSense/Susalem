using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Wms.WmsStockInOutItems
{
    /// <summary>
    /// 【领域实体】出入库记录-子表
    /// </summary>
    public class Wms_StockInOutItem : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】出入库记录ID 
        /// </summary>
        [Required]
        public Guid StockInOutId { get; set; }

        /// <summary>
        /// 【外键】入库位ID
        /// </summary>
        public Guid? InLocationId { get; set; }

        /// <summary>
        /// 【外键】出库位ID
        /// </summary>
        public Guid? OutLocationId { get; set; }

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
        /// 数量
        /// </summary>
        [Required]
        public decimal Qty { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength128)]
        public string BatchNo { get; set; }

        /// <summary>
        /// 价格（未税）
        /// </summary>
        public decimal? Price { get; set; }

        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【领域实体】出入库记录-子表
        /// </summary>
        public Wms_StockInOutItem()
        {
        }

        /// <summary>
        /// 【领域实体】出入库记录-子表表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockInOutId">出入库记录ID</param>
        /// <param name="inLocationId">入库位ID</param>
        /// <param name="outLocationId">出库位ID</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="qty">数量</param>
        /// <param name="batchNo">批次号</param>
        /// <param name="price">价格（未税）</param>
        /// <param name="creationTime"></param>
        public Wms_StockInOutItem(
            [NotNull] Guid id,
            [NotNull] Guid stockInOutId,
            [CanBeNull] Guid inLocationId,
            [CanBeNull] Guid outLocationId,
            [NotNull] Guid materialId,
            [NotNull] decimal qty,
            [CanBeNull] string batchNo,
            [NotNull] decimal price,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            StockInOutId = stockInOutId;
            InLocationId = inLocationId;
            OutLocationId = outLocationId;
            MaterialId = materialId;
            Qty = qty;
            BatchNo = batchNo;
            Price = price;
            CreationTime = creationTime;
        }
    }
}
