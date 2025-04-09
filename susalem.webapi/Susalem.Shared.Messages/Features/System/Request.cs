namespace Susalem.Shared.Messages.Features.System;
/// <summary>
/// 系统日志时间
/// </summary>
/// <param name="Name">名字</param>
/// <param name="Size">大小</param>
/// <param name="LatestTime">最新操作时间</param>
public record SystemLogQueryModel(string Name, string Size, DateTime LatestTime);

