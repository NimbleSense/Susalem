using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
namespace Susalem.Tem.TemParaTypes
{
    /// <summary>
    /// 【实体】工艺参数类型管理
    /// </summary>
    [Serializable]
    public class Tem_ParaType : FullAuditedEntity<Guid>, IHasExtraProperties
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
        [Required]
        public int TypeNo { get; set; }

        /// <summary>
        /// 默认单位
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string DefaultUnit { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        public Tem_ParaType() { }

        /// <summary>
        /// 【实体】工艺参数类型管理
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="typeNo">类型序号</param>
        /// <param name="defaultUnit">默认单位</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">描述</param>
        /// <param name="extraProperties"></param>
        public Tem_ParaType(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            bool isEnable,
            [NotNull] int typeNo,
            [CanBeNull] string defaultUnit,
            [CanBeNull] string remark = null,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Code = code;
            Name = name;
            IsEnable = isEnable;
            Remark = remark;
            TypeNo = typeNo;
            DefaultUnit = defaultUnit;
            ExtraProperties = extraProperties;
        }
    }
}
