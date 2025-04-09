using JetBrains.Annotations;

using Susalem.Mam.OrderEnum;
using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Mam.MamOrders
{
    /// <summary>
    /// 【领域实体】实时订单表
    /// </summary>
    public class Mam_Order : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 订单号（唯一）
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 线体Id
        /// </summary>
        public Guid PdLineId { get; set; }

        /// <summary>
        /// ERP订单号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ErpOrderNo { get; set; }

        /// <summary>
        /// 产品类型ID
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 物料号
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        ///  产品工艺Id
        /// </summary>
        [Required]
        public Guid ProductFlowId { get; set; }

        /// <summary>
        /// 工艺变更记录
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength500)]
        public string ChangeRecord { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal OrderQty { get; set; }

        /// <summary>
        /// 班次编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ShiftCode { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ShiftName { get; set; }

        /// <summary>
        /// 已排产数量
        /// </summary>
        public decimal PlanedQty { get; set; }

        /// <summary>
        /// 上线数量
        /// </summary>
        public decimal UploadQty { get; set; }

        /// <summary>
        /// 合格数
        /// </summary>
        [Required]
        public decimal OkQty { get; set; }

        /// <summary>
        /// 不合格数
        /// </summary>
        [Required]
        public decimal NgQty { get; set; }

        /// <summary>
        /// 报废数
        /// </summary>
        [Required]
        public decimal ScrapQty { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        [Required]
        public DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        [Required]
        public DateTime PlanFinishTime { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualFinishTime { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public long Index { get; set; }

        /// <summary>
        /// 状态枚举  新建 、下达、
        /// </summary>
        public OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public Guid Occ { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        protected Mam_Order() { }


        /// <summary>
        /// 是实时订单
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="erpOrderNo">ERP订单号</param>
        /// <param name="shiftCode">ERP订单号</param>
        /// <param name="shiftName">ERP订单号</param>
        /// <param name="productTypeId">物料id</param>
        /// <param name="productFlowId">工艺id</param>
        /// <param name="orderQty">订单数量</param>
        /// <param name="planStartTime">计划开始时间</param>
        /// <param name="planFinishTime">计划结束时间</param>
        /// <param name="index">序号</param>
        /// <param name="status">状态</param>
        /// <param name="type">类型</param>
        /// <param name="sourceType">订单来源(枚举)</param>
        /// <param name="planedQty">已排产数量</param>
        /// <param name="uploadQty">上线数量</param>
        /// <param name="okQty">合格数</param>
        /// <param name="ngQty">不合格数</param>
        /// <param name="scrapQty">报废数</param>
        /// <param name="changeRecord">工艺变更记录</param>
        /// <param name="actualStartTime">实际开始时间</param>
        /// <param name="actualFinishTime">实际完成时间</param>
        /// <param name="productTypeCodeWithVer">产品类型-版本</param>
        /// <param name="extraProperties">拓展字段</param>
        public Mam_Order(
            Guid id,
           [NotNull] string orderNo,
           [NotNull] string erpOrderNo,
           [NotNull] string shiftCode,
           [NotNull] string shiftName,
           [NotNull] Guid productTypeId,
           [NotNull] Guid productFlowId,
           [NotNull] decimal orderQty,
           [NotNull] DateTime planStartTime,
           [NotNull] DateTime planFinishTime,
           [NotNull] long index,
           [NotNull] OrderStatusEnum status,
           [NotNull] string type,
           [NotNull] string sourceType,
           [NotNull] decimal okQty,
           [NotNull] decimal ngQty,
           [NotNull] decimal scrapQty,
           [NotNull] decimal planedQty = 0,
           [NotNull] decimal uploadQty = 0,
           [CanBeNull] string changeRecord = null,
           [CanBeNull] DateTime? actualStartTime = null,
           [CanBeNull] DateTime? actualFinishTime = null,
           [CanBeNull] string productTypeCodeWithVer = null,
           [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            OrderNo = orderNo;
            ErpOrderNo = erpOrderNo;
            ProductTypeId = productTypeId;
            ProductFlowId = productFlowId;
            ChangeRecord = changeRecord;
            OrderQty = orderQty;
            OkQty = okQty;
            NgQty = ngQty;
            ScrapQty = scrapQty;
            PlanedQty = planedQty;
            UploadQty = uploadQty;
            PlanStartTime = planStartTime;
            PlanFinishTime = planFinishTime;
            ActualStartTime = actualStartTime;
            ActualFinishTime = actualFinishTime;
            Index = index;
            Status = status;
            Type = type;
            SourceType = sourceType;
            ExtraProperties = extraProperties;
            ShiftCode= shiftCode;
            ShiftName= shiftName;
            ProductTypeCodeWithVer = productTypeCodeWithVer;


        }
    }
}
