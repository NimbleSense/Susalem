using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.OrderEnum;

namespace Susalem.Mam.MamPlans
{
    /// <summary>
    /// 【实体】实时生产工单
    /// </summary>
    public class Mam_Plan : Entity<Guid>, IHasExtraProperties, IHasCreationTime
    {
        /// <summary>
        /// 工单号（唯一）
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string PlanNo { get; set; }

        /// <summary>
        /// 订单ID（外）
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 线体Id
        /// </summary>
        public Guid PdLineId { get; set; }

        /// <summary>
        /// 产品类型ID（外）
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 产品工艺Id（外）
        /// </summary>
        [Required]
        public Guid ProductFlowId { get; set; }

        /// <summary>
        /// 上线工序ID（外）
        /// </summary>
        public Guid? UploadProcessId { get; set; }

        /// <summary>
        /// 物料-版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        /// 工艺变更记录
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength500)]
        public string ChangeRecord { get; set; }

        /// <summary>
        /// 工单数量
        /// </summary>
        [Required]
        public decimal PlanQty { get; set; }

        /// <summary>
        /// 上线数量
        /// </summary>
        [Required]
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
        /// 班次编码
        /// </summary>
        public string ShiftCode { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ShiftName { get; set; }

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
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 类型（字典）
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Type { get; set; }

        /// <summary>
        /// 是否主工单
        /// </summary>
        [Required]
        public bool IsMain { get; set; }

        /// <summary>
        /// occ锁
        /// </summary>
    //    public Guid Occ { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        protected Mam_Plan() { }

        /// <summary>
        /// 【实体】实时生产工单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productTypeId"></param>
        /// <param name="productFlowId"></param>
        /// <param name="planQty"></param>
        /// <param name="uploadQty"></param>
        /// <param name="okQty">合格数</param>
        /// <param name="ngQty">不合格数</param>
        /// <param name="scrapQty">报废数</param>
        /// <param name="planStartTime"></param>
        /// <param name="planFinishTime"></param>
        /// <param name="actualStartTime"></param>
        /// <param name="actualFinishTime"></param>
        /// <param name="index"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="isMain"></param>
        /// <param name="productTypeCodeWithVer">产品编码-版本</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="planNo"></param>
        /// <param name="changeRecord"></param>
        /// <param name="extraProperties"></param>
        public Mam_Plan(
            [NotNull] Guid orderId,
            [NotNull] Guid productTypeId,
            [NotNull] Guid productFlowId,
            [NotNull] decimal planQty,
            [NotNull] decimal uploadQty,
            [NotNull] decimal okQty,
            [NotNull] decimal ngQty,
            [NotNull] decimal scrapQty,
            [NotNull] DateTime planStartTime,
            [NotNull] DateTime planFinishTime,
            [CanBeNull] DateTime actualStartTime,
            [CanBeNull] DateTime actualFinishTime,
            [NotNull] int index,
            [NotNull] OrderStatusEnum status,
            [NotNull] string type,
            [NotNull] bool isMain,
            // bool isEnable = true,
            [CanBeNull] string productTypeCodeWithVer,
            [NotNull] DateTime creationTime,
            [CanBeNull] string planNo = null,
            [CanBeNull] string changeRecord = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            PlanNo = planNo;
            OrderId = orderId;
            ProductTypeId = productTypeId;
            ProductFlowId = productFlowId;
            ChangeRecord = changeRecord;
            PlanQty = planQty;
            UploadQty = uploadQty;
            OkQty = okQty;
            NgQty = ngQty;
            ScrapQty = scrapQty;
            PlanStartTime = planStartTime;
            PlanFinishTime = planFinishTime;
            ActualStartTime = actualStartTime;
            ActualFinishTime = actualFinishTime;
            Index = index;
            Status = status;
            Type = type;
            IsMain = isMain;
            CreationTime = creationTime;
            ExtraProperties = extraProperties;
            // IsEnable = isEnable;
            ProductTypeCodeWithVer = productTypeCodeWithVer;
        }
    }
}
