using Microsoft.EntityFrameworkCore;

using Susalem.Ecm.EcmRecordHistories;
using Susalem.Ecm.EcmRecordWips;
using Susalem.Ecm.EcmStructItems;
using Susalem.Ecm.EcmStructs;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;

public static class EcmDbContextModelCreatingExtensions
{
    public static void ConfigureMesEcm(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

  

        //能耗分项管理
        builder.Entity<Ecm_Struct>(b =>
        {

            b.ConfigureByConvention();
            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(z => z.ParentId);
            b.HasIndex(z => z.CreationTime);
        });
        //能耗分项管理子项
        builder.Entity<Ecm_StructItem>(b =>
        {
            

            b.ConfigureByConvention();
            //创建索引
            b.HasIndex(z => z.StructId);
            b.HasIndex(z => z.EquipmentId);
            b.HasIndex(z => z.Code);
        });
      
        //能耗实时记录
        builder.Entity<Ecm_RecordWip>(b =>
        {
            

            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.Value).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(z => z.StructItemId);
            b.HasIndex(z => z.DayShiftItemId);
        });

        //能耗历史记录
        builder.Entity<Ecm_RecordHistory>(b =>
        {
            
            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.Value).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.MeterRecordValue).HasColumnType("decimal(16,4)").HasDefaultValue(0).IsRequired();

            //创建索引
            b.HasIndex(z => z.StructItemId);
            b.HasIndex(z => z.DayShiftItemId);
        });
    }
}
