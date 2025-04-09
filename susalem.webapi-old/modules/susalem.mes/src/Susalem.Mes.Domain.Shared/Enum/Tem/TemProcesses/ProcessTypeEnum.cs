using System.ComponentModel;
namespace Susalem.Tem.TemProcesses
{
    /// <summary>
    /// 【枚举】 工序类型枚举
    /// </summary>
    public enum ProcessTypeEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -999,
        /// <summary>
        /// 上线工序
        /// </summary>
        [Description("上线工序")]
        Upload = 0,

        /// <summary>
        /// 加工工序
        /// </summary>
        [Description("加工工序")]
        Process = 1,

        /// <summary>
        /// 下线工序    
        /// </summary>
        [Description("下线工序")]
        Download = 2,

        /// <summary>
        /// 返修工序
        /// </summary>
        [Description("返修工序")]
        Rework = 3,

        /// <summary>
        /// 返修上线工序
        /// </summary>
        [Description("返修上线工序")]
        ReworkUpload = 4,

        /// <summary>
        /// 返修下线工序
        /// </summary>
        [Description("返修下线工序")]
        ReworkDownload = 5,

        /// <summary>
        /// 合装工序
        /// </summary>
        [Description("合装工序")]
        Combination = 6,

        /// <summary>
        /// 平行工序
        /// </summary>
        [Description("平行工序")]
        Parallel = 7,

        /// <summary>
        /// 赋码工序
        /// </summary>
        [Description("赋码工序")]
        AssignCode = 8,

        /// <summary>
        /// 再上线工序
        /// </summary>
        [Description("再上线工序")]
        UploadAgain = 9,

        /// <summary>
        /// 线外校验工序
        /// </summary>
        [Description("线外校验工序")]
        LineCalibration = 10,

        /// <summary>
        /// 质量门工序
        /// </summary>
        [Description("质量门工序")]
        QualityGate =11 
    }
}
