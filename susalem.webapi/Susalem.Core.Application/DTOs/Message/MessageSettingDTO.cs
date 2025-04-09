using System.ComponentModel.DataAnnotations;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.DTOs.Message
{
    public class MessageSettingDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public MessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// 配置信息
        /// </summary>
        [Required]
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
