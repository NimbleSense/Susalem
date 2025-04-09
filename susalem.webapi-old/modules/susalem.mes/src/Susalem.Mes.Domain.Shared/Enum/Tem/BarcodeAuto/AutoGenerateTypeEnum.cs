using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Susalem.TemBarcodeAuto
{
    /// <summary>
    /// 自动生成编码类型 type
    /// </summary>
    public enum AutoGenerateTypeEnum
    {
        [Description("总成码")]
        SerialNo = 0,
        [Description("总成简码")]
        SimpleSerialNo = 1
    }
}
