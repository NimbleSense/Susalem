using Microsoft.EntityFrameworkCore;

using Susalem.Eqm.EqmAccidentEntrys;
using Susalem.Eqm.EqmArchives;
using Susalem.Eqm.EqmEquipments;
using Susalem.Eqm.EqmFaultConfigs;
using Susalem.Eqm.EqmModuleTypes;
using Susalem.Eqm.Susalem.MachLifeMs;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class EqmDbContextModelCreatingExtensions
{
    /// <summary>
    /// 【数据库表映射】MES设备模型管理
    /// </summary>
    /// <param name="builder">模型生成对象</param>
    public static void ConfigureMesEqm(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        // 【实体】 设备基础信息
        builder.Entity<Eqm_Equipment>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);
            //创建索引
            b.HasIndex(z => z.FactoryId);
            b.HasIndex(z => z.WorkShopId);
            b.HasIndex(z => z.PdLineId);
            //b.HasIndex(z => z.StationId);
            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => z.Category);
        });


        // 【实体】 设备组件类型
        builder.Entity<Eqm_ModuleType>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);

            //创建索引
            b.HasIndex(z => z.CreationTime);
            b.Property(t => t.UsefulLifeLimit).HasColumnType("decimal(16, 4)").IsRequired();
        });

        // 【实体】 设备档案

        builder.Entity<Eqm_Archive>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);

            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => z.EquipmentId);
            b.HasIndex(z => z.ModuleTypeId);

            b.Property(t => t.UsefulLifeLimit).HasColumnType("decimal(16, 4)").IsRequired();
        });

        // 【实体】 设备组件类型
        builder.Entity<Eqm_FaultConfig>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Sequence);

            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => z.EquipmentId);
        });

        // 【实体】 事故录入
        builder.Entity<Eqm_AccidentEntry>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.CreationTime);
        });

        
        // 【实体】 预防性维护
        builder.Entity<Eqm_MachLifeM>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.CreationTime);
        });
        
    }
}
