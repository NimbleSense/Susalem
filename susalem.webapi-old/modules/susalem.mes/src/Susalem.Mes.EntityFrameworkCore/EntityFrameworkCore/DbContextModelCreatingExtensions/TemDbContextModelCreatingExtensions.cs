using Microsoft.EntityFrameworkCore;
using Susalem.Tem.TemBarcodeRules;
using Susalem.Tem.TemFlowEquipments;
using Susalem.Tem.TemMaterials;
using Susalem.Tem.TemMaterialUsedTimes;
using Susalem.Tem.TemParaTypes;
using Susalem.Tem.TemPfpsParaConfigs;
using Susalem.Tem.TemPfpsProductBoms;
using Susalem.Tem.TemProcesses;
using Susalem.Tem.TemProductFlowProcesses;
using Susalem.Tem.TemProductFlowProcessSteps;
using Susalem.Tem.TemProductFlows;
using Susalem.Tem.TemProductFlowTimeSpans;
using Susalem.Tem.TemStepTypes;
using Susalem.Tem.TemVirtualMaterialRelations;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class TemDbContextModelCreatingExtensions
{
    public static void ConfigureMesTem(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

         //物料管理
        builder.Entity<Tem_Material>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //设置默认值
            b.Property(z => z.ReservedQty).HasColumnType("decimal(16,4)").HasDefaultValue(0);
            b.Property(z => z.SafeQty).HasColumnType("decimal(16,4)").HasDefaultValue(0);
            b.Property(z => z.TryProcessCount).HasDefaultValue(1).IsRequired();
            
            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.Type);
            b.HasIndex(q => q.TraceType);

        });

        //工序管理
        builder.Entity<Tem_Process>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();


            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.Type);

        });

        //工艺参数类型
        builder.Entity<Tem_ParaType>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(q => q.CreationTime);

        });

        //工步类型
        builder.Entity<Tem_StepType>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(q => q.CreationTime);
        });

        //产品工艺
        builder.Entity<Tem_ProductFlow>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.Code);
            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(q => q.CreationTime);
        });

        //产品工艺
        builder.Entity<Tem_ProductFlowProcess>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.CycleTime).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.HourPrice).HasColumnType("decimal(16,4)");
            b.Property(z => z.PiecePrice).HasColumnType("decimal(16,4)");
            b.Property(z => z.RepairTime).HasDefaultValue(0);
            //创建索引
            b.HasIndex(z => z.ProductFlowId);
            b.HasIndex(z => z.ProcessId);
            b.HasIndex(q => q.CreationTime);
        });

        //工序时间管控
        builder.Entity<Tem_ProductFlowTimeSpan>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(z => z.PreProcessId);
            b.HasIndex(z => z.ControlType);
            b.HasIndex(q => q.CreationTime);
        });

        //工艺设备
        builder.Entity<Tem_FlowEquipment>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(z => z.EquipmentId);
            b.HasIndex(q => q.CreationTime);
        });

        //产品工艺工序工步
        builder.Entity<Tem_ProductFlowProcessStep>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(z => z.StepTypeId);
            b.HasIndex(z => z.CreationTime);
        });

        //工艺参数
        builder.Entity<Tem_PfpsParaConfig>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.StepId);
            b.HasIndex(z => z.ParaTypeId);
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(z => z.CreationTime);
        });

        //Bom
        builder.Entity<Tem_PfpsProductBom>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.MaterialId);
            b.HasIndex(z => z.StepId);
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(z => z.CreationTime);
        });

        //产品条码规则
        builder.Entity<Tem_BarcodeRule>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.ProductTypeId);

            //创建索引
            b.HasIndex(z => z.CreationTime);
        });


        // 虚拟件关系对应表
        builder.Entity<Tem_VirtualMaterialRelation>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();
            //创建索引
            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => z.VirtualMaterialId);
            b.HasIndex(z => z.NonVirtualMaterialId);
        });




        //物料次数管控
        builder.Entity<Tem_MaterialUsedTime>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();


            //创建索引
            b.HasIndex(z => z.MaterialId);
            b.HasIndex(q => q.CreationTime);

        });


    }
}
