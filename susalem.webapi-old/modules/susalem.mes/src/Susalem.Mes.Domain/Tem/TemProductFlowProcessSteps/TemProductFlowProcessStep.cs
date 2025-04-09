using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
using Volo.Abp.Data;

namespace Susalem.Tem.TemProductFlowProcessSteps
{
    /// <summary>
    /// 【领域实体】产品工艺工序工步
    /// </summary>
    public class Tem_ProductFlowProcessStep : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】产品工艺子表ID （产品工艺工序表
        /// </summary>
        [Required]
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 【外键】工步类型ID
        /// </summary>
        [Required]
        public Guid StepTypeId { get; set; }

        /// <summary>
        /// 工步号
        /// </summary>
        [Required]
        public int StepIndex { get; set; }

        /// <summary>
        /// 节拍（秒）
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal CycleTime { get; set; }

        /// <summary>
        /// 验证信息
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string VerifyInfo { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 工步程序号
        /// </summary>
        [Required]
        public int StepProgramNo { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【领域实体】产品工艺工序工步
        /// </summary>
        protected Tem_ProductFlowProcessStep()
        {

        }

        /// <summary>
        /// 【领域实体】产品工艺工序工步
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productFlowProcessId">产品工艺子表ID</param>
        /// <param name="stepTypeId">工步类型ID</param>
        /// <param name="stepIndex">工步号</param>
        /// <param name="cycleTime">节拍（秒）</param>
        /// <param name="verifyInfo">验证信息</param>
        /// <param name="description">操作描述</param>
        /// <param name="stepProgramNo">工步程序号</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        protected Tem_ProductFlowProcessStep(
             Guid id,
            [NotNull] Guid productFlowProcessId,
            [NotNull] Guid stepTypeId,
            [NotNull] int stepIndex,
            [NotNull] decimal cycleTime,
            [NotNull] string verifyInfo,
            [NotNull] string description,
            [NotNull] int stepProgramNo,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            ProductFlowProcessId = productFlowProcessId;
            StepTypeId = stepTypeId;
            StepIndex = stepIndex;
            CycleTime = cycleTime;
            VerifyInfo = verifyInfo;
            Description = description;
            StepProgramNo = stepProgramNo;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
