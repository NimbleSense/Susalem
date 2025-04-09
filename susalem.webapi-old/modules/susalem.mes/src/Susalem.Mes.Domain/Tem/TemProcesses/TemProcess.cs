using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemProcesses
{
    /// <summary>
    /// 【实体】工序管理
    /// </summary>
    public class Tem_Process : FullAuditedEntity<Guid>, IHasExtraProperties
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
        /// 工序类型
        /// </summary>
        public ProcessTypeEnum Type { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }

        protected Tem_Process() { }

        /// <summary>
        /// 无参构造
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">描述</param>
        /// <param name="extraProperties">拓展字段</param>
        public Tem_Process(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            [NotNull] ProcessTypeEnum type,
            bool isEnable = true,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Code = code;
            Name = name;
            Type = type;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
        }
    }
}
