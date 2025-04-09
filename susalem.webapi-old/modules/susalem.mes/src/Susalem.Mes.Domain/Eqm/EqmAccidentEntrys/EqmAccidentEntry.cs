using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Susalem.Eqm.EqmAccidentEntrys
{
    public class Eqm_AccidentEntry : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 事故情况
        /// </summary>
        public string AccidentSituation { get; set; }

        /// <summary>
        /// 事故类型 字典
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string TypeOfAccident { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>

        public DateTime CreationTime { get; set; }
    }
}
