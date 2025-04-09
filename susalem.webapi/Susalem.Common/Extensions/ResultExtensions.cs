using System;
using System.Collections.Generic;
using Susalem.Common.Results;

namespace Susalem.Common.Extensions
{
    public static class ResultExtensions
    {
        public static Result WithMessage(this Result result, string message)
        {
            result.Message = message;
            return result;
        }

        public static Result WithError(this Result result, string error)
        {
            result.AddError(error);
            return result;
        }

        public static Result WithError(this Result result, string error, string code)
        {
            result.AddError(error, code);
            return result;
        }

        public static Result WithErrors(this Result result, List<IResultError> errors)
        {
            result.AddErrors(errors);
            return result;
        }

        public static Result WithErrors(this Result result, List<ResultError> errors)
        {
            result.AddErrors(errors);
            return result;
        }

        public static Result Failed(this Result result)
        {
            result.Succeeded = false;
            return result;
        }

        public static Result Successful(this Result result)
        {
            result.Succeeded = true;
            return result;
        }

        public static Result WithException(this Result result, Exception exception)
        {
            result.Exception = exception;
            return result;
        }

        public static Result<T> WithMessage<T>(this Result<T> result, string message)
        {
            result.Message = message;
            return result;
        }

        public static Result<T> WithData<T>(this Result<T> result, T data)
        {
            result.Data = data;
            return result;
        }

        public static Result<T> WithError<T>(this Result<T> result, string error)
        {
            result.AddError(error);
            return result;
        }

        public static Result<T> WithErrors<T>(this Result<T> result, List<ResultError> errors)
        {
            result.AddErrors(errors);
            return result;
        }

        public static Result<T> WithError<T>(this Result<T> result, string error, string code)
        {
            result.AddError(error, code);
            return result;
        }

        public static Result<T> Failed<T>(this Result<T> result)
        {
            result.Succeeded = false;
            return result;
        }

        public static Result<T> Successful<T>(this Result<T> result)
        {
            result.Succeeded = true;
            return result;
        }

        public static Result<T> WithException<T>(this Result<T> result, Exception exception)
        {
            result.Exception = exception;
            return result;
        }
    }
}
