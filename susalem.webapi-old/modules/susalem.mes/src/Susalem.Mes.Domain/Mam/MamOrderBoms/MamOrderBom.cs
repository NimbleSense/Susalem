using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Susalem.Mam.MamOrderBomTypeEnum;
using Susalem.Tem.TemMaterials;

namespace Susalem.Mam.MamOrderBoms
{
    /// <summary>
    /// 【实体】订单Bom
    /// </summary>
    public class Mam_OrderBom : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】工位Id
        /// </summary>
        [Required]
        public Guid WorkStationId { get; set; }

        /// <summary>
        /// 【外键】订单Id
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 【外键】产品类型Id
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 产品code-版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        /// 【外键】物料Id
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 物料编码-版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialCodeWithVer { get; set; }

        /// <summary>
        /// 订单BOM状态枚举
        /// </summary>
        public OrderBomTypeEnum Type { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public decimal Qty { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 物料追溯类型
        /// </summary>
        [Required]
        public MaterialTraceTypeEnum MaterialTraceType { get; set; }

        /// <summary>
        /// 物料追溯类型
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Mam_OrderBom() { }

        /// <summary>
        /// 【实体】订单Bom构造函数
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="workStationId">工厂Id</param>
        /// <param name="orderId">订单Id</param>
        /// <param name="productTypeId">产品类型Id</param>
        /// <param name="materialId">物料Id</param>
        /// <param name="materialCodeWithVer">物料编码-版本</param>
        /// <param name="productTypeCodeWithVer">产品类型-版本</param>
        /// <param name="qty">数量</param>
        /// <param name="startDateTime">生效时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <param name="type">订单BOM状态枚举</param>
        /// <param name="materialTraceType">物料追溯类型</param>
        /// <param name="dataSourceType">数据来源</param>
        public Mam_OrderBom(
            [NotNull] Guid id,
            [NotNull] Guid workStationId,
            [NotNull] Guid orderId,
            [NotNull] Guid productTypeId,
            [NotNull] Guid materialId,
            [NotNull] decimal qty,
            [CanBeNull] DateTime startDateTime,
            [CanBeNull] DateTime endDateTime,
            [NotNull] MaterialTraceTypeEnum materialTraceType,
            [NotNull] OrderBomTypeEnum type,
            [NotNull] string dataSourceType,
            [CanBeNull] string materialCodeWithVer = null,
            [CanBeNull] string productTypeCodeWithVer = null
            )
        {
            Id = id;
            WorkStationId = workStationId;
            OrderId = orderId;
            ProductTypeId = productTypeId;
            MaterialId = materialId;
            MaterialCodeWithVer = materialCodeWithVer;
            Qty = qty;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Type = Type;
            MaterialTraceType = materialTraceType;
            DataSourceType = dataSourceType;
            CreationTime = DateTime.Now;
            ProductTypeCodeWithVer = productTypeCodeWithVer;
        }
    }
}
