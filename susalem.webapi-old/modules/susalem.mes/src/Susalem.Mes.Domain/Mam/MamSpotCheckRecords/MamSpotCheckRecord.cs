using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Mam.MamProductStatusEnum;
namespace Susalem.Mam.MamSpotCheckRecords
{
    /// <summary>
    /// 【领域实体】产品抽检记录
    /// </summary>
    public class Mam_SpotCheckRecord : AuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】产品清单Id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string SnCode { get; set; }

        /// <summary>
        /// 【外键】工单ID(外)
        /// </summary>
        [Required]
        public Guid PlanId { get; set; }

        /// <summary>
        /// 【外键】产品类型ID
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 排版子表id
        /// </summary>
        public Guid DayShiftItemId { get; set; }

        /// <summary>
        ///  工序序号
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 【外键】工序ID
        /// </summary>
        [Required]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 【外键】设备ID（外）
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        ///  载体编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string CarrierCode { get; set; }

        /// <summary>
        ///  适配板编号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string AdapterPlateCode { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Required]
        public ProductStatusEnum Status { get; set; }

        /// <summary>
        /// 上线数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal UploadQty { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal OkQty { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal NgQty { get; set; }

        /// <summary>
        /// 报废数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal ScrapQty { get; set; }

        /// <summary>
        /// 加工人
        /// </summary>
        [Required]
        public Guid WorkEmployeeId { get; set; }

        /// <summary>
        /// 当前工步号
        /// </summary>
        [Required]
        public int CurStepNo { get; set; }

        /// <summary>
        /// 是否返修
        /// </summary>
        [Required]
        public bool IsRework { get; set; }

        /// <summary>
        ///产品工艺工序Id  使用情况待定
        /// </summary>
        [Required]
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 实际节拍
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal ActualCycleTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 加工次数
        /// </summary>
        [Required]
        public int ProcessWorkTimes { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【领域实体】产品抽检记录
        /// </summary>
        public Mam_SpotCheckRecord() { }

        /// <summary>
        /// 【领域实体】产品抽检记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productId">产品清单Id</param>
        /// <param name="snCode">SN编码</param>
        /// <param name="planId">工单ID</param>
        /// <param name="productTypeId">产品类型ID</param>
        /// <param name="droductTypeId">排版子表id</param>
        /// <param name="index">工序序号</param>
        /// <param name="processId">工序id</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="carrierCode">载体编码</param>
        /// <param name="adapterPlateCode">适配板编号</param>
        /// <param name="status">状态</param>
        /// <param name="uploadQty">上线数量</param>
        /// <param name="okQty">合格数量</param>
        /// <param name="ngQty">不合格数量</param>
        /// <param name="scrapQty">报废数量</param>
        /// <param name="workEmployeeId">加工人</param>
        /// <param name="curStepNo">当前工步号</param>
        /// <param name="isRework">是否返修</param>
        /// <param name="productFlowProcessId">产品工艺工序Id</param>
        /// <param name="actualCycleTime">实际节拍</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="processWorkTimes">加工次数</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="lastModificationTime">最后修改时间</param>
        /// <param name="lastModifierId">最后修改人</param>
        /// <param name="creatorId">创建人</param>
        /// <param name="extraProperties">拓展字段</param>
        public Mam_SpotCheckRecord(
           Guid id,
           Guid productId,
           [NotNull] string snCode,
           Guid planId,
           Guid productTypeId,
           Guid droductTypeId,
           [NotNull] int index,
           Guid processId,
           Guid equipmentId,
           [NotNull] string carrierCode,
           string adapterPlateCode,
           [NotNull] ProductStatusEnum status,
           [NotNull] decimal uploadQty,
           [NotNull] decimal okQty,
           [NotNull] decimal ngQty,
           [NotNull] decimal scrapQty,
            Guid workEmployeeId,
           [NotNull] int curStepNo,
           [NotNull] bool isRework,
            Guid productFlowProcessId,
           [NotNull] decimal actualCycleTime,
           [NotNull] DateTime startTime,
           [NotNull] DateTime endTime,
           [CanBeNull] int processWorkTimes,
           [NotNull] DateTime creationTime,
           [CanBeNull] DateTime? lastModificationTime,
           [CanBeNull] Guid? lastModifierId,
           [CanBeNull] Guid? creatorId,
           [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            ProductId = productId;
            SnCode = snCode;
            PlanId = planId;
            ProductTypeId = productTypeId;
            DayShiftItemId = droductTypeId;
            Index = index;
            ProcessId = processId;
            EquipmentId = equipmentId;
            CarrierCode = carrierCode;
            AdapterPlateCode = adapterPlateCode;
            Status = status;
            UploadQty = uploadQty;
            OkQty = okQty;
            NgQty = ngQty;
            ScrapQty = scrapQty;
            WorkEmployeeId = workEmployeeId;
            CurStepNo = curStepNo;
            IsRework = isRework;
            ProductFlowProcessId = productFlowProcessId;
            ActualCycleTime = actualCycleTime;
            StartTime = startTime;
            EndTime = endTime;
            CreationTime = creationTime;
            ProcessWorkTimes = processWorkTimes;
            LastModificationTime = lastModificationTime;
            CreatorId = creatorId;
            LastModifierId = lastModifierId;
            ExtraProperties = extraProperties;
        }
    }
}
