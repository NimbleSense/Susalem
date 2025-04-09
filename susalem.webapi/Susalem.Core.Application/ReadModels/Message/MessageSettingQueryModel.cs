using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.Queries.Message
{
    public class MessageSettingQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// 配置信息
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 启用/停用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
