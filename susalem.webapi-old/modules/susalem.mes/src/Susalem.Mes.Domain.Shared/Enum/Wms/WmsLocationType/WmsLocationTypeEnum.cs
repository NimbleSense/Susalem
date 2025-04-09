using System.ComponentModel;
namespace Susalem.Wms.WmsLocationType
{
    /// <summary>
    /// 仓库库位类型枚举
    /// </summary>
    public enum WmsLocationTypeEnum
    {
        /// <summary>
        /// 上料位
        /// </summary>
        [Description("上料位")]
        UploadMaterial = 0,

        /// <summary>
        /// 返空位
        /// </summary>
        [Description("返空位")]
        DownloadPallet = 1,

        /// <summary>
        /// 上料|反空位
        /// </summary>
        [Description("上料|反空")]
        UpDownload = 2,
    }
}
