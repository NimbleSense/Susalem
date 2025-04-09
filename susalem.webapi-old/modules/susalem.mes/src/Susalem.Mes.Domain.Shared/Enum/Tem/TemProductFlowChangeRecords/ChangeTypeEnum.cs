using System.ComponentModel;
namespace Susalem.Tem.TemProductFlowChangeRecords
{
    /// <summary>
    /// 工艺变更类型
    /// </summary>
    public enum ChangeTypeEnum
    {
        [Description("工艺")]
        Flow=0,
        [Description("工序")]
        Process =1,
        [Description("工步")]
        Step =2,
        [Description("设备")]
        Eqm =3,
        [Description("Bom")]
        Bom =4,
        [Description("工艺参数")]
        Para =5,
        [Description("工单工艺")]
        Mam_Plan =6
    }
}
