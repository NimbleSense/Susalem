using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Microsoft.Extensions.Logging;

namespace Susalem.Core.Application
{
    public static class ServicesHelper
    {
        public static void HandleServiceError<T, P>(ref Result<T> serviceResult, ILogger<P> logger, Exception ex,
            string uiErrorMessage)
        {
            logger.LogError($"{nameof(HandleServiceError)}: {ex}");
            if (serviceResult.Errors.Any())
            {
                logger.LogError($"Result errors: {string.Join(Environment.NewLine, serviceResult.Errors)}");
            }

            serviceResult
                .Failed()
                .WithMessage(uiErrorMessage)
                .WithException(ex)
                .WithData(default);
        }

        public static void HandleServiceError<P>(ref Result serviceResult, ILogger<P> logger, Exception ex,
            string uiErrorMessage)
        {
            logger.LogError($"{nameof(HandleServiceError)}: {ex}");
            if (serviceResult.Errors.Any())
            {
                logger.LogError($"Result errors: {string.Join(Environment.NewLine, serviceResult.Errors)}");
            }
            serviceResult
                .Failed()
                .WithException(ex)
                .WithMessage(uiErrorMessage);
        }
    }
}
