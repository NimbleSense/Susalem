namespace Susalem.Core.Application
{
    public static class Roles
    {
        /// <summary>
        /// Root management like super admin.
        /// </summary>
        public const string RootManagement = "ROOT";

        public const string UserManagement = "USER MANAGEMENT";

        public const string DeviceControl = "DEVICE CONTROL";

        public const string DashBoard = "DASH BOARD";

        public const string Notification = "NOTIFICATION";

    }

    public static class Permissions
    {
        public const string Name = "Permission";
        /// <summary>
        /// 设备管理操作
        /// </summary>
        public const string DeviceAll = "DeviceManagement.All";

        /// <summary>
        /// 角色管理查看和操作
        /// </summary>
        public const string RoleAll = "RoleManagement.All";

        /// <summary>
        /// 用户管理查看和操作
        /// </summary>
        public const string UserAll = "UserManagement.All";

        /// <summary>
        /// 点位控制
        /// </summary>
        public const string PositionControl = "PositionControl";

        /// <summary>
        /// 消息管理操作
        /// </summary>
        public const string NotificationAll = "Notification.All";

        /// <summary>
        /// 核心操作（修改数据等）
        /// </summary>
        public const string AdavncedSetting = "AdavncedSetting";
    }
}
