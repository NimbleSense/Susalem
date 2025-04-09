using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Wms.WmsCallMaterialStatusEnum;
using Susalem.Wms.WmsCallMaterialTypeEnum;

namespace Susalem.Wms.WmsCallMaterialTaskHistories
{
    /// <summary>
    /// 【实体】叫料任务历史
    /// </summary>
    public class Wms_CallMaterialTaskHistory : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】库位ID
        /// </summary>
        [Required]
        public Guid LocationId { get; set; }

        /// <summary>
        /// 【外键】物料ID
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 物料编码-版本
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialCodeWithVer { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public decimal Qty { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public CallMaterialStatusEnum Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public CallMaterialTypeEnum Type { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string LocationCode { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 叫料开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 叫料结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        public Wms_CallMaterialTaskHistory() { }

        /// <summary>
        /// 【实体】叫料任务历史
        /// </summary>
        ///  <param name="id">主键Id</param>
        /// <param name="locationId">库位ID</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="materialCodeWithVer">物料编码-版本</param>
        /// <param name="qty">数量</param>
        /// <param name="status">状态</param>
        /// <param name="type">类型</param>
        /// <param name="locationCode">库位编码</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="extraProperties">拓展字段</param>
        public Wms_CallMaterialTaskHistory(
             [NotNull] Guid id,
             [NotNull] Guid locationId,
             [NotNull] Guid materialId,
             [NotNull] string materialCodeWithVer,
             [NotNull] decimal qty,
             [NotNull] CallMaterialStatusEnum status,
             [NotNull] CallMaterialTypeEnum type,
             [NotNull] DateTime creationTime,
             [NotNull] string locationCode,
             [CanBeNull] ExtraPropertyDictionary extraProperties
            )
        {
            Id = id;
            LocationId = locationId;
            MaterialId = materialId;
            MaterialCodeWithVer = materialCodeWithVer;
            Qty = qty;
            Status = status;
            Type = type;
            LocationCode = locationCode;
            CreationTime = creationTime;
            ExtraProperties = extraProperties;
        }
    }
}
