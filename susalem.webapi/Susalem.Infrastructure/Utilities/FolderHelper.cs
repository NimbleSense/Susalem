using Susalem.Shared.Messages.Features.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Susalem.Infrastructure.Utilities;
public static class FolderHelper
{
    /// <summary>
    /// 获取目录日志文件
    /// </summary>
    /// <param name="folder"></param>
    /// <returns></returns>
    public static IEnumerable<SystemLogQueryModel> GetLogs(string folder)
    {
        var logs = new List<SystemLogQueryModel>();
        var directory = new DirectoryInfo(folder);
        var files = directory.GetFiles();
        foreach (var file in files)
        {
            logs.Add(new SystemLogQueryModel(Path.GetFileNameWithoutExtension(file.Name), ConvertLength(file.Length), file.LastWriteTime));
        }
        return logs;
    }

    private static string ConvertLength(double length)
    {
        var units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
        var mod = 1024.0;
        var i = 0;
        while(length >= mod)
        {
            length /= mod;
            i++;
        }
        return Math.Round(length) + units[i];
    }

    /// <summary>
    /// 压缩指定文件到指定目录
    /// </summary>
    /// <param name="sourceFolder">源文件夹</param>
    /// <param name="name">文件名</param>
    /// <param name="targetFolder">目标文件夹</param>
    /// <returns>文件名，带子文件目录名</returns>
    public static string CompressFile(string sourceFolder, string name, string targetFolder)
    {
        if (!Directory.Exists(targetFolder))
            Directory.CreateDirectory(targetFolder);

        var directory = new DirectoryInfo(sourceFolder);
        var fileToCompress = directory.GetFiles().FirstOrDefault(t => t.Name.Contains(name));
        if (fileToCompress == null) return string.Empty;

        using (FileStream originalFileStream = fileToCompress.OpenRead())
        {
            if ((File.GetAttributes(fileToCompress.FullName) &
               FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
            {
                var targetFile = Path.Combine(targetFolder, $"{name}.gz");
                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                using (FileStream compressedFileStream = File.Create(targetFile))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                       CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
                FileInfo info = new FileInfo(targetFile);
                return $"/temp/{info.Name}";
            }
        }
        return string.Empty;
    }
}
