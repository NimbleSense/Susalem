using Microsoft.EntityFrameworkCore;

using Susalem.Fam;
using Susalem.Fam.FamDayShiftItems;
using Susalem.Fam.FamDayShifts;
using Susalem.Fam.FamEmployees;
using Susalem.Fam.FamPdLines;
using Susalem.Fam.FamShiftConfigItems;
using Susalem.Fam.FamShiftConfigs;
using Susalem.Fam.FamWorkShops;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class FamDbContextModelCreatingExtensions
{
    /// <summary>
    /// 【数据库表映射】MES工厂模型管理
    /// </summary>
    /// <param name="builder">模型生成对象</param>
    public static void ConfigureMesFam(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        //工厂管理
        builder.Entity<Fam_Factory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);
        });

        //车间管理
        builder.Entity<Fam_WorkShop>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);

            //创建索引
            b.HasIndex(z => z.FactoryId);
        });


        //产线管理
        builder.Entity<Fam_PdLine>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);

            //创建索引
            b.HasIndex(z => z.FactoryId);

            //创建索引
            b.HasIndex(z => z.WorkShopId);

            //创建索引
            b.HasIndex(z => z.CreationTime);
        });

        //员工管理
        builder.Entity<Fam_Employee>(b =>
        {
            

            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(z => z.Name);
            b.HasIndex(z => z.JobStatus);
            b.HasIndex(z => z.CreationTime);
        });

        
        //班次主表
        builder.Entity<Fam_ShiftConfig>(b =>
        {
            

            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.PdLineId);
            b.HasIndex(z => z.Tag);
            b.HasIndex(z => z.CreationTime);
        });

        //班次子表
        builder.Entity<Fam_ShiftConfigItem>(b =>
        {
            

            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.ShiftConfigId);
            b.HasIndex(z => z.Type);
            b.HasIndex(z => z.CreationTime);
        });

        //排班主表
        builder.Entity<Fam_DayShift>(b =>
        {
            

            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.PdLineId);
            b.HasIndex(z => z.Type);
            b.HasIndex(z => z.CreationTime);
        });

        //排班子表
        builder.Entity<Fam_DayShiftItem>(b =>
        {
            

            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.DayShiftId);
            b.HasIndex(z => z.ShiftTag);
            b.HasIndex(z => z.Type);
            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => new { z.EndTime,z.StartTime,z.Type,z.IsDeleted});
        });

    }
}
