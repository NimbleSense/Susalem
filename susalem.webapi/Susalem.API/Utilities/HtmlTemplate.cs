namespace Susalem.Api.Utilities;

/// <summary>
/// Html 模板
/// </summary>
public class HtmlTemplate
{
    public const string AlarmTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        .header {
            text-align: center;
            color: #ffa463;
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
            background-color: #7b93c1;
            color: white;
        }
    </style>
</head>
<body>
    <div class=""header""><h1>告警记录</h1></div>
    <table align=""center"">
        <tr>
            <th>点位</th>
            <th>功能</th>
            <th>告警值</th>
            <th>告警时间</th>
            <th>等级</th>
            <th>操作用户</th>
            <th>确认内容</th>
        </tr>
        {TemplateContent}
    </table>
</body>
</html>";
}
