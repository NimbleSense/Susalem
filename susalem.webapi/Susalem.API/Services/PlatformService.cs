using Susalem.Core.Application.Interfaces.Services;
using Susalem.Messages.Enumerations;
using System.IO;
using System;
using System.IO.Ports;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace Susalem.Api.Services;

/// <summary>
/// 平台功能
/// </summary>
public class PlatformService : IPlatformService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public string DbPath 
    { 
        get
        {
            var applicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MonitorServer");
            if (!Directory.Exists(applicationPath))
            {
                Directory.CreateDirectory(applicationPath);
            }
            return applicationPath;
        } 
    }

    public string WebRootPath => _webHostEnvironment.WebRootPath;

    public string LogsPath => Path.Combine(DbPath, "logs");

    public PlatformService( IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;

        // 20240920 anders
        //if (!Directory.Exists(DownloadTempFolder))
        //{
        //    Directory.CreateDirectory(DownloadTempFolder);
        //}
    }

    /// <summary>
    /// 获取可用的串口
    /// </summary>
    /// <returns></returns>
    public string[] AvailablePorts()
    {
        return SerialPort.GetPortNames();
    }

    /// <summary>
    /// 重启服务
    /// </summary>
    public void Restart()
    {
        Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "restart.bat"));
    }
}
