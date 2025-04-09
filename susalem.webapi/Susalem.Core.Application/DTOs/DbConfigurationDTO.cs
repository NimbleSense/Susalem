using System;
using System.Collections.Generic;

namespace Susalem.Core.Application.DTOs
{
    public class DbConfigurationDTO
    {
        /// <summary>
        /// 是否允许备份
        /// </summary>
        public bool Backup { get; set; }

        /// <summary>
        /// 周定义
        /// </summary>
        public List<int> Weeks { get; set; } = new List<int>();

        /// <summary>
        /// 备份时间
        /// </summary>
        public DateTime BackupTime { get; set; }
    }
}
