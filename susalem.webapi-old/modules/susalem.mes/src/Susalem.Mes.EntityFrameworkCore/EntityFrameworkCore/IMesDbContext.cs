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
public interface IMesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */

   DbSet<Fam_Factory> FamFactories { get; set; }
   DbSet<Fam_WorkShop> FamWorkShops { get; set; }
   DbSet<Fam_PdLine> FamPdLines { get; set; }
   DbSet<Fam_ShiftConfig> FamShiftConfigs { get; set; }
   DbSet<Fam_ShiftConfigItem> FamShiftConfigItems { get; set; }
   DbSet<Fam_DayShift> FamDayShifts { get; set; }
   DbSet<Fam_DayShiftItem> FamDayShiftItems { get; set; }
   DbSet<Fam_Employee> FamEmployees { get; set; }

   DbSet<Mam_Order> MamOrders { get; set; }
   DbSet<Mam_ProductionPlan> MamProductionPlans { get; set; }
   DbSet<Mam_Plan> MamPlans { get; set; }

   DbSet<Mam_OrderBom> MamOrderBoms { get; set; }
 
   DbSet<Mam_ProductRelation> MamProductRelations { get; set; }

   DbSet<Mam_SpotCheckRecord> MamSpotCheckRecords { get; set; }

   DbSet<Tem_MaterialUsedTime> TemMaterialUsedTimes { get; set; }
   DbSet<Tem_Material> TemMaterials { get; set; }
   DbSet<Tem_ParaType> TemParaTypes { get; set; }
   DbSet<Tem_Process> TemProcesses { get; set; }
   DbSet<Tem_StepType> TemStepTypes { get; set; }
   DbSet<Tem_ProductFlow> TemProductFlows { get; set; }
   DbSet<Tem_ProductFlowProcess> TemProductFlowProcesses { get; set; }
   DbSet<Tem_ProductFlowTimeSpan> TemProductFlowTimeSpans { get; set; }
   DbSet<Tem_FlowEquipment> TemFlowEquipments { get; set; }
   DbSet<Tem_ProductFlowProcessStep> TemProductFlowProcessSteps { get; set; }
   DbSet<Tem_PfpsParaConfig> TemPfpsParaConfigs { get; set; }
   DbSet<Tem_PfpsProductBom> TemPfpsProductBoms { get; set; }
   DbSet<Tem_BarcodeRule> TemBarcodeRules { get; set; }
 
   DbSet<Eqm_Equipment> EqmEquipments { get; set; }
   DbSet<Eqm_ModuleType> EqmModuleTypes { get; set; }
   DbSet<Eqm_Archive> EqmArchives { get; set; }
   DbSet<Eqm_FaultConfig> EqmFaultConfigs { get; set; }
   DbSet<Eqm_AccidentEntry> EqmAccidentEntrys { get; set; }
   DbSet<Tem_VirtualMaterialRelation> TemVirtualMaterialRelations { get; set; }
   DbSet<Wms_StockWip> WmsStockWips { get; set; }
   DbSet<Wms_StockInOut> WmsStockInOuts { get; set; }
   DbSet<Wms_StockInOutItem> WmsStockInOutItems { get; set; }
   DbSet<Wms_MaterialPositionRule> WmsMaterialPositionRules { get; set; }
   DbSet<Wms_Warehouse> WmsWarehouses { get; set; }
   DbSet<Wms_Shelf> WmsShelfs { get; set; }
   DbSet<Wms_Location> WmsLocations { get; set; }
   DbSet<Wms_CallMaterialTaskWip> WmsCallMaterialTaskWips { get; set; }
   DbSet<Wms_CallMaterialTaskHistory> WmsCallMaterialTaskHistories { get; set; }
   DbSet<Sys_CodeRule> SysCodeRules { get; set; }
   DbSet<Sys_Operation> SysOperations { get; set; }
   DbSet<Sys_Dictionary> SysDictionaries { get; set; }
   DbSet<Sys_DictionaryItem> SysDictionaryItems { get; set; }
   DbSet<Ecm_Struct> EcmStructs { get; set; }
   DbSet<Ecm_StructItem> EcmStructItems { get; set; }
   DbSet<Ecm_RecordWip> EcmRecordWips { get; set; }
   DbSet<Ecm_RecordHistory> EcmRecordHistories { get; set; }
   DbSet<Mam_RepairPolicy> MamRepairPolicies { get; set; }
   DbSet<Mam_MasterRepair> MamMasterRepairs { get; set; }
   DbSet<Mam_ProductRepair> MamProductRepairs { get; set; }
   DbSet<Mam_ProductRepairItem> MamProductRepairItems { get; set; }
   DbSet<Mam_MasterRepairItem> MamMasterRepairItems { get; set; }
   DbSet<Mam_MasterRepairCheck> MamMasterRepairChecks { get; set; }
  
   DbSet<Mam_ProductVerAdapt> MamProductVerAdapts { get; set; }//产品版本适配-表


   DbSet<Eqm_MachLifeM> MachLifeMs { get; set; }
}
