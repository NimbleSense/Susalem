using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;
using Susalem.Api.Interfaces;
using Susalem.Api.Utilities;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Core.Application.ReadModels.Audit;
using Susalem.Core.Application.ReadModels.Record;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;

namespace Susalem.Api.Services
{
    public class ReportService: IReportService
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReportService(IStringLocalizer<Resource> localizer, IConverter converter, IWebHostEnvironment hostEnvironment)
        {
            _localizer = localizer;
            _converter = converter;
            _hostEnvironment = hostEnvironment;
            CreateSubDirectory("Report");
        }

        public string GenerateAuditReport(string companyName, string reportHeader, IEnumerable<string> headers,
            IEnumerable<AuditQueryModel> records)
        {
            return CreatePdf(companyName,
                TemplateGenerator.GetHtmlString(reportHeader, headers,
                    TemplateGenerator.GenerateTableContent(records)));
        }

        public string GenerateAlarmReport(string companyName, IEnumerable<AlarmDetailQueryModel> records)
        {
            var contentBuilder = new StringBuilder();
            foreach(var alarmDetail in records)
            {
                var tempAlarms = alarmDetail.AlarmDetails.ToList();
                for(var i = 0; i< tempAlarms.Count; i++) 
                {
                    var alarmContent = tempAlarms[i];
                    if (i == 0)
                    {
                        var line = $"<tr><td rowspan={tempAlarms.Count}>{alarmDetail.PositionName}</td><td>{_localizer[alarmContent.AlarmProperty]}</td><td>{alarmContent.AlarmValue}</td><td>{alarmContent.ReportTime}</td><td rowspan={tempAlarms.Count}>{_localizer[alarmDetail.Level.ToString()]}</td><td rowspan={tempAlarms.Count}>{alarmDetail.ConfirmUser}</td><td rowspan={tempAlarms.Count}>{alarmDetail.ConfirmContent}</td></tr>";
                        contentBuilder.Append(line);
                    }
                    else
                    {
                        var line = $"<tr><td>{_localizer[alarmContent.AlarmProperty]}</td><td>{alarmContent.AlarmValue}</td><td>{alarmContent.ReportTime}</td></tr>";
                        contentBuilder.Append(line);
                    }
                }
            }

            return CreatePdf(companyName, HtmlTemplate.AlarmTemplate.Replace("{TemplateContent}", contentBuilder.ToString()));
        }

        public string GeneratePdfReport(string companyName, string reportHeader,IEnumerable<string> headers, IEnumerable<RecordReadModel> records)
        {
            return CreatePdf(companyName,
                TemplateGenerator.GetHtmlString(reportHeader, headers, TemplateGenerator.GenerateTableContent(records)));
        }

        private string CreatePdf(string companyName, string html)
        {
            var fileName = $"Report\\{Guid.NewGuid()}.pdf";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10,Bottom=10 },
                    Out= $"{_hostEnvironment.WebRootPath}\\{fileName}"
                },
                Objects = {
                    new ObjectSettings {
                        PagesCount = true,
                        HtmlContent =  html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = $"{companyName}", Line = true, Spacing = 2.812},
                        FooterSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
            _converter.Convert(doc);
            return fileName;
        }

        private void CreateSubDirectory(string directoryName)
        {
            try
            {
                var subPath = Path.Combine(_hostEnvironment.WebRootPath, directoryName);
                if (!Directory.Exists(subPath))
                {
                    Directory.CreateDirectory(subPath);
                }
                else
                {
                    var files = new DirectoryInfo(subPath).GetFiles();
                    foreach (var fileInfo in files)
                    {
                        fileInfo.Delete();
                    }
                }
            }
            catch
            {
                
            }
            
        }
    }

    /// <summary>
    /// Generate html to string 
    /// </summary>
    public class TemplateGenerator
    {
        private static string GenerateHeader()
        {
            return @"<head>
                <style>
                .header {
                text-align: center;
                color: ffa463;
            }
            table {
                width: 90%;
                border-collapse: collapse;
            }
            th {
                border: 1px solid gray;
                padding: 15px;
                font-size: 15px;
                text-align: center;
            }
            td {
                border: 1px solid gray;
                padding: 5px;
                font-size: 15px;
                text-align: center;
            }
            table th {
                background-color: 7b93c1;
                color: white;
            }
            </style>
                </head> ";
        }

        private static string GenerateTableHeader(IEnumerable<string> headers)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<tr>");
            foreach (var header in headers)
            {
                stringBuilder.AppendFormat(@"<th>{0}</th>", header);
            }

            stringBuilder.Append("</tr>");
            return stringBuilder.ToString();
        }

        public static string GenerateTableContent(IEnumerable<RecordReadModel> records)
        {

            var stringBuilder = new StringBuilder();
            foreach (var recordReadModel in records)
            {
                stringBuilder.AppendFormat(@"<tr><td>{0}</td><td>{1}</td>", recordReadModel.PositionName, recordReadModel.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                foreach (var recordContent in recordReadModel.Contents)
                {
                    stringBuilder.AppendFormat(@"<td>{0}</td>", recordContent.Value);
                }

                stringBuilder.Append("</tr>");
            }
            return stringBuilder.ToString();
        }

        public static string GenerateTableContent(IEnumerable<AuditQueryModel> records)
        {

            var stringBuilder = new StringBuilder();
            foreach (var recordReadModel in records)
            {
                

                //stringBuilder.AppendFormat(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", recordReadModel.StartTime.ToString("yyyy-MM-dd HH:mm:ss"), recordReadModel.UserName,recordReadModel.EventType,recordReadModel.Description);
            }
            return stringBuilder.ToString();
        }

        public static string GenerateTableContent(IEnumerable<AlarmDetailQueryModel> records, IStringLocalizer localizer)
        {
            //var alarmPropDic = new Dictionary<string, string>
            //{
            //    {"Micron05", "0.5μm"},
            //    {"Micron50", "5.0μm"},
            //    {"Flow", "流量"},
            //    {"Volume", "体积"},
            //    {"Temperature", "温度(℃)"},
            //    {"RH", "湿度(%RH)"},
            //    {"Wind", "风速(m/s)"},
            //    {"Press", "压差(Pa)"}
            //};

            //var alarmLevelDic = new Dictionary<AlarmLevelEnum, string>
            //{
            //    {AlarmLevelEnum.Alarm, "报警"},
            //    {AlarmLevelEnum.Warning, "预警"}
            //};

            var stringBuilder = new StringBuilder();
            foreach (var recordReadModel in records)
            {
                //stringBuilder.AppendFormat(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>", recordReadModel.ReportTime.ToString("yyyy-MM-dd HH:mm:ss"), recordReadModel.PositionName, localizer[recordReadModel.AlarmProperty], recordReadModel.AlarmValue, localizer[recordReadModel.Level.ToString()], recordReadModel.ConfirmUser, recordReadModel.ConfirmContent);
            }
            return stringBuilder.ToString();
        }

        private static string GenerateBody(string header, IEnumerable<string> tableHeaders, string content)
        {
            return $"<body>" +
                   $"<div class='header'><h1>{header}</h1></div>" +
                   $"<table align='center'>" +
                   $"{GenerateTableHeader(tableHeaders)}" +
                   $"{content}" +
                   $"</table>" +
                   $"</body>";
        }

        public static string GetHtmlString(string header, IEnumerable<string> tableHeaders, string content)
        {
            return $"<html>{GenerateHeader()}{GenerateBody(header, tableHeaders, content)}</html>";
        }
    }
}
