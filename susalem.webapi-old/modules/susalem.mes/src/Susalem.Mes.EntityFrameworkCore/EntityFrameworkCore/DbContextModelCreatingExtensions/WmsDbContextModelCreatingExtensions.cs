using Microsoft.EntityFrameworkCore;

using Susalem.Wms.WmsCallMaterialTaskHistories;
using Susalem.Wms.WmsCallMaterialTaskWips;
using Susalem.Wms.WmsLocations;
using Susalem.Wms.WmsMaterialPositionRules;
using Susalem.Wms.WmsShelfs;
using Susalem.Wms.WmsStockInOutItems;
using Susalem.Wms.WmsStockInOuts;
using Susalem.Wms.WmsStockWips;
using Susalem.Wms.WmsWarehouses;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class WmsDbContextModelCreatingExtensions
{
    public static void ConfigureMesWms(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

    

        //仓库管理
        builder.Entity<Wms_Warehouse>(b =>
        {
            //Configure table & schema name
            

            b.ConfigureByConvention();

            //Properties

            //Indexes
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.Code);
            b.HasIndex(q => q.CreationTime);
        });

        //货架管理
        builder.Entity<Wms_Shelf>(b =>
        {
            //Configure table & schema name
            

            b.ConfigureByConvention();

            //Properties

            //Indexes
            b.HasIndex(q => q.WarehouseId);
            b.HasIndex(q => q.Code);
            b.HasIndex(q => q.CreationTime);
        });

        //库位管理
        builder.Entity<Wms_Location>(b =>
        {
            //Configure table & schema name
            

            b.ConfigureByConvention();

            //Properties

            //Indexes
            b.HasIndex(q => q.WarehouseId);
            b.HasIndex(q => q.Code);
            b.HasIndex(q => q.CreationTime);
        });


        //料位规则
        builder.Entity<Wms_MaterialPositionRule>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.WarehouseId);
            b.HasIndex(q => q.ShelfId);
            b.HasIndex(q => q.LocationId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.MaterialId);
            b.HasIndex(q => q.CreationTime);
        });

        //实时库存
        builder.Entity<Wms_StockWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Qty).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.WarehouseId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.ShelfId);
            b.HasIndex(q => q.LocationId);
            b.HasIndex(q => q.MaterialId);
            b.HasIndex(q => q.CreationTime);
        });

        //出入库记录-主表
        builder.Entity<Wms_StockInOut>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.InWarehouseId);
            b.HasIndex(q => q.OutWarehouseId);
            b.HasIndex(q => q.BillNo);
            b.HasIndex(q => q.BillDate);
            b.HasIndex(q => q.BillType);
            b.HasIndex(q => q.BizType);
            b.HasIndex(q => q.CreationTime);
        });

        //出入库记录-子表
        builder.Entity<Wms_StockInOutItem>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(q => q.Qty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.Price).HasColumnType("decimal(16,8)");

            //创建索引
            b.HasIndex(q => q.MaterialId);
            b.HasIndex(q => q.CreationTime);
        });

        //叫料任务实时
        builder.Entity<Wms_CallMaterialTaskWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(q => q.Qty).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.MaterialId);
            b.HasIndex(q => q.LocationId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
        });

        //叫料任务历史
        builder.Entity<Wms_CallMaterialTaskHistory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(q => q.Qty).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.MaterialId);
            b.HasIndex(q => q.LocationId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
        });
    }
}
