namespace Susalem.Core.Application.DTOs
{
    public class ValueRangeProperty
    {
        /// <summary>
        /// 头：
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 范围 尾： 头+范围
        /// </summary>
        public int Range { get; set; }
    }
}
