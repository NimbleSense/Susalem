using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Ecm.EcmStructs
{
    /// <summary>
    /// 【实体】 能耗分项管理管理
    /// </summary>
    public class Ecm_Struct :  Entity<Guid>, IHasExtraProperties, IHasCreationTime
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string Name { get; set; }

        /// <summary>
        /// 【外键】父级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 是否IOT设备
        /// </summary>
        [Required]
        public bool IsIot { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Ip { get; set; }

        /// <summary>
        /// 同步周期
        /// </summary>
        [Required]
        public int SyncTime { get; set; }

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
        /// 【实体】 能耗分项管理
        /// </summary>
        protected Ecm_Struct() { }

        /// <summary>
        /// 【实体】 能耗分项管理
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编号</param>
        /// <param name="name">名称</param>
        /// <param name="parentId">父级id</param>
        /// <param name="isIot">是否IOT设备</param>
        /// <param name="ip">IP地址</param>
        /// <param name="syncTime">同步周期</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="creationTime">创建时间</param>
        public Ecm_Struct(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            Guid? parentId,
            [NotNull] bool isIot,
            string ip,
            [NotNull] int syncTime,
            [CanBeNull] ExtraPropertyDictionary extraProperties,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            Code = code;
            Name = name;
            ParentId = parentId;
            IsIot = isIot;
            Ip = ip;
            SyncTime = syncTime;
            ExtraProperties = extraProperties;
            CreationTime = creationTime;
        }
    }
}
