using System;
using System.Collections.Generic;

namespace Susalem.Common.Results
{
    public class Result:IResult
    {
        private readonly List<IResultError> _errors = new List<IResultError>();

        /// <summary>
        /// An collection of errors from the result
        /// </summary>
        public IReadOnlyCollection<IResultError> Errors => _errors.AsReadOnly();
        
        /// <summary>
        /// The result message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// An indication whether the result is successful.
        /// </summary>
        public bool Succeeded { get; internal set; }

        /// <summary>
        /// An indication whether the result has failed.
        /// </summary>
        public bool Failed => !Succeeded;

        /// <summary>
        /// Metadata which might be contained in the result.
        /// </summary>
        public Dictionary<string, object> Metadata { get; internal set; } = new Dictionary<string, object>();

        /// <summary>
        /// Helper proper to present the message with errors.
        /// </summary>
        public string MessageWithErrors => $"{Message}{Environment.NewLine}{string.Join(',', _errors)}";

        /// <summary>
        /// Any exception that might have been thrown.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Helper for adding error with message to result object.
        /// </summary>
        /// <param name="errorMessage"></param>
        public void AddError(string errorMessage)
        {
            _errors.Add(new ResultError(errorMessage));
        }

        /// <summary>
        /// Helper for adding multiple errors.
        /// </summary>
        /// <param name="errors"></param>
        public void AddErrors(IEnumerable<IResultError> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddError(string errorMessage, string errorCode)
        {
            _errors.Add(new ResultError(errorMessage, errorCode));
        }

        public void AddMetadata(string key, object value)
        {
            Metadata.Add(key,value);
        }

        public T GetMetadata<T>(string key) where T : struct
        {
            return (T)Metadata[key];
        }

        public Result()
        {
            Succeeded = true;
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public Result() : base()
        {
        }
    }
}
