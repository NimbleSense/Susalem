using System.ComponentModel;
namespace Susalem.Fam.FamEmployeeJobStatusEnum
{
    /// <summary>
    /// 员工在职状态
    /// </summary>
    public enum EmployeeJobStatusEnum
    {
        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        OnJob = 0,

        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        LeaveJob = 1,
    }
}
