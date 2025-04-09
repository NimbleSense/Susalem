using Microsoft.EntityFrameworkCore;

using Susalem.Ecm.EcmRecordHistories;
using Susalem.Ecm.EcmRecordWips;
using Susalem.Ecm.EcmStructItems;
using Susalem.Ecm.EcmStructs;
using Susalem.Eqm.EqmAccidentEntrys;
using Susalem.Eqm.EqmArchives;
using Susalem.Eqm.EqmEquipments;
using Susalem.Eqm.EqmFaultConfigs;
using Susalem.Eqm.EqmModuleTypes;
using Susalem.Eqm.Susalem.MachLifeMs;
using Susalem.Fam;
using Susalem.Fam.FamDayShiftItems;
using Susalem.Fam.FamDayShifts;
using Susalem.Fam.FamEmployees;
using Susalem.Fam.FamPdLines;
using Susalem.Fam.FamShiftConfigItems;
using Susalem.Fam.FamShiftConfigs;
using Susalem.Fam.FamWorkShops;
using Susalem.Mam.MamMasterRepairChecks;
using Susalem.Mam.MamMasterRepairItems;
using Susalem.Mam.MamMasterRepairs;
using Susalem.Mam.MamOrderBoms;
using Susalem.Mam.MamOrders;
using Susalem.Mam.MamPlans;
using Susalem.Mam.MamProductionPlans;
using Susalem.Mam.MamProductRelations;
using Susalem.Mam.MamProductRepairItems;
using Susalem.Mam.MamProductRepairs;
using Susalem.Mam.MamProductVerAdapts;
using Susalem.Mam.MamRepairPolicies;
using Susalem.Mam.MamSpotCheckRecords;
using Susalem.Sys;
using Susalem.Sys.Dictionaries;
using Susalem.Sys.SysDictionaryItems;
using Susalem.Sys.SysOperations;
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
using Susalem.Wms.WmsCallMaterialTaskHistories;
using Susalem.Wms.WmsCallMaterialTaskWips;
using Susalem.Wms.WmsLocations;
using Susalem.Wms.WmsMaterialPositionRules;
using Susalem.Wms.WmsShelfs;
using Susalem.Wms.WmsStockInOutItems;
using Susalem.Wms.WmsStockInOuts;
using Susalem.Wms.WmsStockWips;
using Susalem.Wms.WmsWarehouses;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.Mes.EntityFrameworkCore;

[ConnectionStringName(MesDbProperties.ConnectionStringName)]
public class MesDbContext : AbpDbContext<MesDbContext>, IMesDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public MesDbContext(DbContextOptions<MesDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMes();
    }


    public DbSet<Fam_Factory> FamFactories { get; set; }
    public DbSet<Fam_WorkShop> FamWorkShops { get; set; }
    public DbSet<Fam_PdLine> FamPdLines { get; set; }
    public DbSet<Fam_ShiftConfig> FamShiftConfigs { get; set; }
    public DbSet<Fam_ShiftConfigItem> FamShiftConfigItems { get; set; }
    public DbSet<Fam_DayShift> FamDayShifts { get; set; }
    public DbSet<Fam_DayShiftItem> FamDayShiftItems { get; set; }
    public DbSet<Fam_Employee> FamEmployees { get; set; }

    public DbSet<Mam_Order> MamOrders { get; set; }
    public DbSet<Mam_ProductionPlan> MamProductionPlans { get; set; }
    public DbSet<Mam_Plan> MamPlans { get; set; }

    public DbSet<Mam_OrderBom> MamOrderBoms { get; set; }

    public DbSet<Mam_ProductRelation> MamProductRelations { get; set; }
    public DbSet<Mam_SpotCheckRecord> MamSpotCheckRecords { get; set; }
    public DbSet<Tem_MaterialUsedTime> TemMaterialUsedTimes { get; set; }
    public DbSet<Tem_Material> TemMaterials { get; set; }
    public DbSet<Tem_ParaType> TemParaTypes { get; set; }
    public DbSet<Tem_Process> TemProcesses { get; set; }
    public DbSet<Tem_StepType> TemStepTypes { get; set; }
    public DbSet<Tem_ProductFlow> TemProductFlows { get; set; }
    public DbSet<Tem_ProductFlowProcess> TemProductFlowProcesses { get; set; }
    public DbSet<Tem_ProductFlowTimeSpan> TemProductFlowTimeSpans { get; set; }
    public DbSet<Tem_FlowEquipment> TemFlowEquipments { get; set; }
    public DbSet<Tem_ProductFlowProcessStep> TemProductFlowProcessSteps { get; set; }
    public DbSet<Tem_PfpsParaConfig> TemPfpsParaConfigs { get; set; }
    public DbSet<Tem_PfpsProductBom> TemPfpsProductBoms { get; set; }
    public DbSet<Tem_BarcodeRule> TemBarcodeRules { get; set; }

    public DbSet<Eqm_Equipment> EqmEquipments { get; set; }
    public DbSet<Eqm_ModuleType> EqmModuleTypes { get; set; }
    public DbSet<Eqm_Archive> EqmArchives { get; set; }
    public DbSet<Eqm_FaultConfig> EqmFaultConfigs { get; set; }
    public DbSet<Eqm_AccidentEntry> EqmAccidentEntrys { get; set; }
    public DbSet<Tem_VirtualMaterialRelation> TemVirtualMaterialRelations { get; set; }
    public DbSet<Wms_StockWip> WmsStockWips { get; set; }
    public DbSet<Wms_StockInOut> WmsStockInOuts { get; set; }
    public DbSet<Wms_StockInOutItem> WmsStockInOutItems { get; set; }
    public DbSet<Wms_MaterialPositionRule> WmsMaterialPositionRules { get; set; }
    public DbSet<Wms_Warehouse> WmsWarehouses { get; set; }
    public DbSet<Wms_Shelf> WmsShelfs { get; set; }
    public DbSet<Wms_Location> WmsLocations { get; set; }
    public DbSet<Wms_CallMaterialTaskWip> WmsCallMaterialTaskWips { get; set; }
    public DbSet<Wms_CallMaterialTaskHistory> WmsCallMaterialTaskHistories { get; set; }
    public DbSet<Sys_CodeRule> SysCodeRules { get; set; }
    public DbSet<Sys_Operation> SysOperations { get; set; }
    public DbSet<Sys_Dictionary> SysDictionaries { get; set; }
    public DbSet<Sys_DictionaryItem> SysDictionaryItems { get; set; }
    public DbSet<Ecm_Struct> EcmStructs { get; set; }
    public DbSet<Ecm_StructItem> EcmStructItems { get; set; }
    public DbSet<Ecm_RecordWip> EcmRecordWips { get; set; }
    public DbSet<Ecm_RecordHistory> EcmRecordHistories { get; set; }
    public DbSet<Mam_RepairPolicy> MamRepairPolicies { get; set; }
    public DbSet<Mam_MasterRepair> MamMasterRepairs { get; set; }
    public DbSet<Mam_ProductRepair> MamProductRepairs { get; set; }
    public DbSet<Mam_ProductRepairItem> MamProductRepairItems { get; set; }
    public DbSet<Mam_MasterRepairItem> MamMasterRepairItems { get; set; }
    public DbSet<Mam_MasterRepairCheck> MamMasterRepairChecks { get; set; }

    public DbSet<Mam_ProductVerAdapt> MamProductVerAdapts { get; set; }

    public DbSet<Eqm_MachLifeM> MachLifeMs { get; set; }
}
