using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
namespace Susalem.Qms.QmsBarcodeStateEnum
{
    /// <summary>
    ///  条码枚举
    /// </summary>
    public enum BarcodeStateEnum
    {
        /// <summary>
        /// 使用
        /// </summary>
        [Description("使用")]
        Use = 0,

        /// <summary>
        /// 解绑
        /// </summary>
        [Description("解绑")]
        Unlink = 1,
        /// <summary>
        /// 报废
        /// </summary>
        [Description("报废")]
        Scrap = 2,

    }
}
