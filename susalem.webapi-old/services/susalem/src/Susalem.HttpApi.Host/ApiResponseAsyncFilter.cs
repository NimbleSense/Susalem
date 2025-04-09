using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Susalem.Susalem.Result;

using System;
using System.Text.Json;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace Susalem;

public class ApiResponseAsyncFilter : IAsyncAlwaysRunResultFilter
{
    [Obsolete]
    public async Task OnResultExecutionAsync1(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // 处理结果
        if (context.Result is ObjectResult objectResult)
        {
            var response = ApiResponse<object>.Success(objectResult.Value);
            context.Result = new JsonResult(response);
        }
        else if (context.Result is BadRequestObjectResult badRequestResult)
        {
            var response = ApiResponse<object>.Error(400, badRequestResult.Value?.ToString());
            context.Result = new JsonResult(response);
        }
        // 可以根据需要添加其他状态码的处理

        // 先执行下一步
        await next();
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        try
        {

            // 处理结果
            if (context.Result is ObjectResult objectResult)
            {
                 // 使用 WrapResult 包装成功结果（对应状态码 200）
                var wrapResult = new WrapResult<object>();
                wrapResult.SetSuccess(objectResult.Value);
                context.Result = new JsonResult(wrapResult);
            }
            // 判断 context.Result 的类型并进行相应处理
            else if (context.Result is OkObjectResult okResult)
            {
                // 使用 WrapResult 包装成功结果（对应状态码 200）
                var wrapResult = new WrapResult<object>();
                wrapResult.SetSuccess(okResult.Value);
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is BadRequestObjectResult badRequestResult)
            {
                // 使用 WrapResult 包装错误结果（400 错误）
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(badRequestResult.Value?.ToString(), 400);
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is NotFoundObjectResult notFoundResult)
            {
                // 使用 WrapResult 包装 404 错误结果
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(notFoundResult.Value?.ToString(), 404);
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is UnauthorizedObjectResult unauthorizedResult)
            {
                // 使用 WrapResult 包装 401 未授权错误结果
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(unauthorizedResult.Value?.ToString(), 401);
                context.Result = new JsonResult(wrapResult);
            }
            // ABP 相关报错类型处理开始
            else if (context.Result is AbpAuthorizationException abpAuthorizationExceptionResult)
            {
                var message = $"认证授权失败: {abpAuthorizationExceptionResult.Message}";
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(message, 401);
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is AbpValidationException abpValidationExceptionResult)
            {
                var message = $"参数验证失败: {abpValidationExceptionResult.Message}";
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(message, 400);
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is EntityNotFoundException entityNotFoundExceptionResult)
            {
                var message = $"实体未找到: {entityNotFoundExceptionResult.Message}";
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(message, 404);
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is BusinessException businessExceptionResult)
            {
                var message = $"业务异常: {businessExceptionResult.Message}";
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(message, 500);  // 可根据实际情况调整合适的状态码，这里暂设为 500
                context.Result = new JsonResult(wrapResult);
            }
            else if (context.Result is NotImplementedException notImplementedExceptionResult)
            {
                var message = $"功能未实现{notImplementedExceptionResult.Message}";
                var wrapResult = new WrapResult<object>();
                wrapResult.SetFail(message, 501);
                context.Result = new JsonResult(wrapResult);
            }
            // 可以根据实际情况继续添加其他状态码对应的 Result 类型处理逻辑

            // 先执行下一步，保持原有的执行顺序
            await next();
        }
        catch (System.Exception ex)
        {
            // 这里可以添加对在处理结果过程中出现异常情况的处理逻辑，比如记录日志等操作
            // 示例中简单打印异常信息，实际可根据需求完善
            System.Console.WriteLine($"在处理结果时出现异常: {ex.Message}");
        }
    }


 
}