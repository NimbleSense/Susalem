using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Susalem.Ecm.EcmRecordHistoryEnum
{
    /// <summary>
    /// 采集方式
    /// </summary>
    public enum RecordHistoryCollectTypeEnum
    {
        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")]
        Auto = 0,

        /// <summary>
        /// 区间
        /// </summary>
        [Description("手动抄表")]
        Manual = 1,

    }
}
