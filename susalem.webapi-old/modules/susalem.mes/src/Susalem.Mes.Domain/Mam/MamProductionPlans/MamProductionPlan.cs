using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Mam.MamProductionPlanEnum;

namespace Susalem.Mam.MamProductionPlans
{

    /// <summary>
    /// 【领域实体】外部接收订单表
    /// </summary>
    public class Mam_ProductionPlan : FullAuditedEntity<Guid>, IHasExtraProperties
    {

        /// <summary>
        /// 订单号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 物料号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        /// 物料号
        /// </summary>
        public Guid ProductTypeId { get; set; }


        /// <summary>
        /// 数量
        /// </summary>
        public int OrderQty { get; set; }

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
        /// 计划完成时间
        /// </summary>
        public DateTime PlanFinishTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualFinishTime { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Index { get; set; }

        /// <summary>
        /// 订单类型字典 OrderType
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Type { get; set; }

        /// <summary>
        /// 消息发送方唯一标识
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Guid { get; set; }

        /// <summary>
        /// 应用标识
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string AppId { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 状态  排队中、运行、已关闭、已完成
        /// </summary>
        public ReceiveOrderStatusEnum Status { get; set; }

        /// <summary>
        /// occ乐观锁
        /// </summary>
        public Guid Occ { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        protected Mam_ProductionPlan() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="productTypeId">物料号</param>
        /// <param name="orderQty">数量</param>
        /// <param name="planFinishTime">计划完成时间</param>
        /// <param name="index">排序值</param>
        /// <param name="type">订单类型字典</param>
        /// <param name="guid">消息发送方唯一标识</param>
        /// <param name="appId">应用标识</param>
        /// <param name="requestTime">请求时间</param>
        /// <param name="status">状态</param>
        /// <param name="actualFinishTime">实际完成时间</param>
        public Mam_ProductionPlan(
          Guid id,
          [NotNull] string orderNo,
          [NotNull] Guid productTypeId,
          [NotNull] int orderQty,
          [NotNull] DateTime planFinishTime,
          [NotNull] string index,
          [NotNull] string type,
          [NotNull] string guid,
          [NotNull] string appId,
          [NotNull] DateTime requestTime,
          [NotNull] ReceiveOrderStatusEnum status,
          [CanBeNull] DateTime? actualFinishTime = null)
        {
            Id = id;
            OrderNo = orderNo;
            ProductTypeId = productTypeId;
            OrderQty = orderQty;
            PlanFinishTime = planFinishTime;
            ActualFinishTime = actualFinishTime;
            Index = index;
            Type = type;
            Guid = guid;
            AppId = appId;
            RequestTime = requestTime;
            Status = status;
            CreationTime = DateTime.Now;
        }
    }
}
