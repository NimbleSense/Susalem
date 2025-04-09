using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Susalem.Sys.SysDictionaryItems
{
    /// <summary>
    ///【领域实体】 字典配置子表
    /// </summary>
    public class Sys_DictionaryItem : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 编码名
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 【外键】字典表Id
        /// </summary>
        [Required]
        public Guid DictionaryId { get; set; }

        ///<inheritdoc/>
        [Required]
        public bool IsEnable { get; set; }

        ///<inheritdoc/>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///  【领域实体】 空字典配置子表构造函数
        /// </summary>
        public Sys_DictionaryItem() { }

        /// <summary>
        /// 【领域实体】字典配置子表
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">编码名</param>
        /// <param name="index">排序</param>
        /// <param name="dictionaryId">字典表Id</param>
        /// <param name="creationTime">创建时间</param>
        public Sys_DictionaryItem(
            Guid id,
            [NotNull] string name,
            [NotNull] int index,
            [NotNull] Guid dictionaryId,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            Index = index;
            Name = name;
            DictionaryId = dictionaryId;
            CreationTime = creationTime;
        }
    }
}
