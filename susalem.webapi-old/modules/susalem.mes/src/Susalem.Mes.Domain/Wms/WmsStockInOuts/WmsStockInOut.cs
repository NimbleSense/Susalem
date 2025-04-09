using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Wms.WmsBillStatusEnum;
using Susalem.Wms.WmsBillTypeEnum;
using Susalem.Wms.WmsBizTypeEnum;

namespace Susalem.Wms.WmsStockInOuts
{
    /// <summary>
    /// 【领域实体】出入库记录-主表
    /// </summary>
    public class Wms_StockInOut : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】入仓库ID 
        /// </summary>
        public Guid? InWarehouseId { get; set; }

        /// <summary>
        /// 【外键】出仓库ID 
        /// </summary>
        public Guid? OutWarehouseId { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string BillNo { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        [Required]
        public DateTime BillDate { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [Required]
        public BillTypeEnum BillType { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        [Required]
        public BillStatusEnum BillStatus { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [Required]
        public BizTypeEnum BizType { get; set; }

        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public Wms_StockInOut()
        {
        }

        /// <summary>
        /// 【领域实体】出入库记录-主表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inWarehouseId">入仓库ID</param>
        /// <param name="outWarehouseId">出仓库ID</param>
        /// <param name="billNo">单据号</param>
        /// <param name="billDate">单据日期</param>
        /// <param name="billType">单据类型</param>
        /// <param name="billStatus">单据状态</param>
        /// <param name="bizType">业务类型</param>
        /// <param name="creationTime">创建时间</param>
        public Wms_StockInOut(
            [NotNull] Guid id,
            [CanBeNull] Guid inWarehouseId,
            [CanBeNull] Guid outWarehouseId,
            [NotNull] string billNo,
            [NotNull] DateTime billDate,
            [NotNull] BillTypeEnum billType,
            [NotNull] BillStatusEnum billStatus,
            [NotNull] BizTypeEnum bizType,
            [NotNull] DateTime creationTime)
        {
            Id = id;
            InWarehouseId = inWarehouseId;
            OutWarehouseId = outWarehouseId;
            BillNo = billNo;
            BillDate = billDate;
            BillType = billType;
            BillStatus = billStatus;
            BizType = bizType;
            CreationTime = creationTime;
        }
    }
}
