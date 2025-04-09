using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Sys.Dictionaries
{
    /// <summary>
    /// 【领域实体】字典管理
    /// </summary>
    public class Sys_Dictionary : Entity<Guid>, IHasExtraProperties, IHasCreationTime
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
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }


        /// <summary>
        ///【领域实体】 字典管理 空构造函数
        /// </summary>
        public Sys_Dictionary() { }

        /// <summary>
        /// 【领域实体】字典配置子表
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="creationTime">创建时间</param>
        public Sys_Dictionary(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            [NotNull] bool isEnable,
            [CanBeNull] string remark,
            [CanBeNull] ExtraPropertyDictionary extraProperties,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            Code = code;
            Name = name;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
            CreationTime = creationTime;
        }
    }
}
