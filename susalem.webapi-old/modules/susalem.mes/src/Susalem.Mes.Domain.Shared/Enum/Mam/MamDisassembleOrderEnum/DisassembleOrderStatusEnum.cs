using System.ComponentModel;
namespace Susalem.Mam.MamDisassembleOrderEnum
{
    public enum DisassembleOrderStatusEnum
    {
        [Description("已使用")]
        Used=0,
        [Description("未使用")]
        UnUse=1,
        [Description("弃用")]
        Discard=2
    }
}
