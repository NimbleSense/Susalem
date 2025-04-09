using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;

namespace Susalem.Tem.TemStepTypes
{
    /// <summary>
    /// 【实体】工步类型
    /// </summary>
    public class Tem_StepType : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 类型序号
        /// </summary>
        public int TypeNo { get; set; }


        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【实体】工步类型
        /// </summary>
        protected Tem_StepType() { }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【实体】工步类型
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="typeNo">类型序号</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        public Tem_StepType(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            [CanBeNull] int typeNo,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Code = code;
            Name = name;
            TypeNo = typeNo;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
