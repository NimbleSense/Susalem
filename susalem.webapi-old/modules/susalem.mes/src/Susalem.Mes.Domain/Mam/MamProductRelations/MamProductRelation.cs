using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Qms.QmsBarcodeStateEnum;

namespace Susalem.Mam.MamProductRelations
{
    /// <summary>
    /// 【领域实体】产品关联关系
    /// </summary>
    public class Mam_ProductRelation : Entity<Guid>, IHasExtraProperties, IHasCreationTime
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 父级产品Id
        /// </summary>
        [Required]
        public Guid ParentProductId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public BarcodeStateEnum Status { get; set; }

        /// <summary>
        /// 主产品SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string ProductSnCode { get; set; }

        /// <summary>
        /// 父级产品SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string ParentProductSnCode { get; set; }

        /// <summary>
        /// 序列号/总成码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string SerialNo { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string PlanNo { get; set; }

        /// <summary>
        /// 总成码简写
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string SimpleSerialNo { get; set; }

        /// <summary>
        /// 是否下线
        /// </summary>
        public bool IsDownLine { get; set; }


        /// <summary>
        /// 是否主工单
        /// 用于判断是否为成品或半成品
        /// </summary>
        [Required]
        public bool IsMain { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        ///<inheritdoc/>
        public DateTime CreationTime { get; set; }

        public Mam_ProductRelation() { }
        /// <summary>
        /// 【实体】产品关联关系
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <param name="parentProductId">父件产品ID</param>
        /// <param name="status">状态</param>
        /// <param name="productSnCode">产品SN编码</param>
        /// <param name="parentProductSnCode">父产品SN编码</param>
        /// <param name="planNo">工单号</param>
        /// <param name="extraProperties">工单号</param>
        /// <param name="isDownLine">是否下线</param>
        /// <param name="isMain">是否主工单</param>
        public Mam_ProductRelation(
            [NotNull] Guid productId,
            [NotNull] Guid parentProductId,
            [NotNull] BarcodeStateEnum status,
            [NotNull] string productSnCode,
            [NotNull] string parentProductSnCode,
            [NotNull] string planNo,
            [NotNull] bool isMain,
           ExtraPropertyDictionary extraProperties = null,
           [CanBeNull] bool isDownLine = false)
        {
            ProductId = productId;
            ParentProductId = parentProductId;
            Status = status;
            ProductSnCode = productSnCode;
            ParentProductSnCode = parentProductSnCode;
            PlanNo = planNo;
            ExtraProperties = extraProperties;
            IsDownLine = isDownLine;
            IsMain = isMain;
        }

    }
}
