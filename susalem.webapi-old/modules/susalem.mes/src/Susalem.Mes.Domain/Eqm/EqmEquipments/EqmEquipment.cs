using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Eqm.EqmEquipmentEmum;

namespace Susalem.Eqm.EqmEquipments
{
    /// <summary>
    /// 【实体】 设备基础信息管理
    /// </summary>
    public class Eqm_Equipment : FullAuditedEntity<Guid>, IHasExtraProperties
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 【外键】工厂ID
        /// </summary>
        public Guid FactoryId { get; set; }

        /// <summary>
        /// 【外键】 车间ID
        /// </summary>
        public Guid? WorkShopId { get; set; }


        /// <summary>
        /// 【外键】 产线ID
        /// </summary>
        public Guid? PdLineId { get; set; }
      

        /// <summary>
        /// cpu的ip地址
        /// </summary>
       [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string CpuIp { get; set; }

        /// <summary>
        /// 终端Ip
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ClientIp { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Model { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Brand { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Required]
        public EquipmentCategoryEnum Category { get; set; }

        /// <summary>
        /// Cpu/NC信息
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string CpuNc { get; set; }

        /// <summary>
        /// 返修岔道工位
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ReworkStation { get; set; }

        /// <summary>
        /// 屏蔽校验
        /// </summary>

        public bool IsSkipValidation { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【实体】 设备基础信息管理
        /// </summary>
        protected Eqm_Equipment() { }

        /// <summary>
        /// 【实体】 设备基础信息管理
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="code">编号</param>
        /// <param name="name">名称</param>
        /// <param name="factoryId">工厂id</param>
        /// <param name="workShopId">车间id</param>
        /// <param name="pdLineId">车间id</param>
        /// <param name="cpuIp">CPU的ip</param>
        /// <param name="clientIp">终端ip</param>
        /// <param name="model">型号</param>
        /// <param name="brand">品牌</param>
        /// <param name="category">类别</param>
        /// <param name="cpuNc">Cpu/NC信息</param>
        /// <param name="reworkStation">返修岔道工位</param>
        /// <param name="isSkipValidation">是否屏蔽校验</param>
        /// <param name="extraProperties">拓展字段</param>
        public Eqm_Equipment(
            Guid id,
            [NotNull] string code,
            [NotNull] string name,
            [NotNull] EquipmentCategoryEnum category,
            Guid factoryId,
            [CanBeNull] Guid? workShopId = null,
            [CanBeNull] Guid? pdLineId = null,
            [CanBeNull] string cpuIp = null,
            [CanBeNull] string clientIp = null,
            [CanBeNull] string model = null,
            [CanBeNull] string brand = null,
            [CanBeNull] string cpuNc = null,
            string reworkStation=null,
            bool isSkipValidation=false,
            ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Code = code;
            Name = name;
            FactoryId = factoryId;
            WorkShopId = workShopId;
            PdLineId = pdLineId;
            CpuIp = cpuIp;
            ClientIp = clientIp;
            Model = model;
            Brand = brand;
            Category = category;
            CpuNc = cpuNc;
            ReworkStation = reworkStation;
            IsSkipValidation = isSkipValidation;
            ExtraProperties = extraProperties;
        }
    }
}
