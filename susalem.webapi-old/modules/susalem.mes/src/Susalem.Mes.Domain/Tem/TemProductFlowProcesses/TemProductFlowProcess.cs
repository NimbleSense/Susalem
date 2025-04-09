using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Tem.TemProductFlowProcesses
{

    /// <summary>
    /// 【实体】产品工艺工序
    /// </summary>
    public class Tem_ProductFlowProcess : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 主表ID
        /// </summary>
        public Guid ProductFlowId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 节拍（秒）
        /// </summary>
        public decimal CycleTime { get; set; }

        /// <summary>
        /// 计时单价
        /// </summary>
        public decimal? HourPrice { get; set; }

        /// <summary>
        /// 计件单价
        /// </summary>
        public decimal? PiecePrice { get; set; }

        /// <summary>
        /// 是否计件
        /// </summary>
        public bool IsPiecePay { get; set; } = false;

        /// <summary>
        /// 是否可选
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// 是否平行工序
        /// </summary>
        public bool IsParallelProcess { get; set; }

        /// <summary>
        /// 是否物料次数管控
        /// </summary>
        public bool IsCountMaterialTime { get; set; }

        /// <summary>
        /// 变更记录
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string ChangeRecord { get; set; }

        /// <summary>
        /// 返修次数
        /// </summary>
        public int RepairTime { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        protected Tem_ProductFlowProcess() { }
        public Tem_ProductFlowProcess(
            Guid productFlowId,
            Guid processId,
           [NotNull] int index,
           [NotNull] decimal cycleTime,
           [NotNull] bool isOptional,
           [NotNull] bool isParallelProcess,
           [CanBeNull] decimal? hourPrice = null,
           [CanBeNull] decimal? piecePrice = null,
           [CanBeNull] string changeRecord = null,
           [CanBeNull] ExtraPropertyDictionary extraProperties = null,
           [CanBeNull] bool isPiecePay=false,
           int repairTime=0,
           bool isEnable = true,
           string remark = null)
        {
            ProductFlowId = productFlowId;
            ProcessId = processId;
            Index = index;
            CycleTime = cycleTime;
            HourPrice = hourPrice;
            PiecePrice = piecePrice;
            IsPiecePay = isPiecePay;
            ChangeRecord = changeRecord;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
            IsOptional = isOptional;
            IsParallelProcess = isParallelProcess;
            RepairTime = repairTime;

        }

      
    }
}
