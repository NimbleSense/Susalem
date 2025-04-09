using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Susalem.Core.Application.Enumerations
{
    /// <summary>
    /// 点位功能类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PositionFunctionEnum
    {
        /// <summary>
        /// 柜子参数
        /// </summary>
        CabinetParams
    }
}
