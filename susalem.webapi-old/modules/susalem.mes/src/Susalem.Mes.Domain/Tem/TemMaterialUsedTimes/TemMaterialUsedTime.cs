using JetBrains.Annotations;

using System;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
using System.ComponentModel.DataAnnotations;
namespace Susalem.Tem.TemMaterialUsedTimes
{
    /// <summary>
    /// 【实体】物料次数管控
    /// </summary>
    public class Tem_MaterialUsedTime : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 产品工艺工序Id
        /// </summary>
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UsedTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【实体】物料管理
        /// </summary>
        protected Tem_MaterialUsedTime() { }

        /// <summary>
        /// 【实体】物料管理
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="materialId">物料Id</param>
        /// <param name="usedTime">使用次数</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        public Tem_MaterialUsedTime(
            Guid id,
            Guid materialId,
            int usedTime,
            bool isEnable,
            [CanBeNull] string remark,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            MaterialId = materialId;
            UsedTime = usedTime;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
