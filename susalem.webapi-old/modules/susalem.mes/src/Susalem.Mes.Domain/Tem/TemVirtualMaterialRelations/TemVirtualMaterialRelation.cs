using System;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Tem.TemVirtualMaterialRelations
{
    /// <summary>
    /// 【实体】虚拟件关系对应表
    /// </summary>
    public class Tem_VirtualMaterialRelation : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 虚拟件Id
        /// </summary>
      public Guid  VirtualMaterialId { get; set; }

        /// <summary>
        /// 非虚拟件Id
        /// </summary>
      public Guid NonVirtualMaterialId { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
