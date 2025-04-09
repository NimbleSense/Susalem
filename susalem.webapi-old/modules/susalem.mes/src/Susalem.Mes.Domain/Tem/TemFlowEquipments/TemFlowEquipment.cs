using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemFlowEquipments
{
    /// <summary>
    /// 【实体】工艺设备
    /// </summary>
    public class Tem_FlowEquipment : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】产品工艺子表ID
        /// </summary>
        [Required]
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 【外键】设备ID
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【实体】工艺设备
        /// </summary>
        /// <param name="productFlowProcessId">产品工艺子表ID</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        public Tem_FlowEquipment(
            [NotNull] Guid productFlowProcessId,
            [NotNull] Guid equipmentId,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties=null)
        {

            ProductFlowProcessId = productFlowProcessId;
            EquipmentId = equipmentId;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
        }
    }
}
