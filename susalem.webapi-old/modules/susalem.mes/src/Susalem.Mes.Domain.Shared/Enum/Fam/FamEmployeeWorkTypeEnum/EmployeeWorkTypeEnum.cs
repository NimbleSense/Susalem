
using System.ComponentModel;
namespace Susalem.Fam.FamEmployeeWorkTypeEnum
{
    /// <summary>
    /// 打卡类型
    /// </summary>
    public enum EmployeeWorkTypeEnum
    {
        /// <summary>
        /// 离岗
        /// </summary>
        [Description("离岗")]
        Logout = 0,

        /// <summary>
        /// 上岗
        /// </summary>
        [Description("上岗")]
        Logon = 1,
    }
}
