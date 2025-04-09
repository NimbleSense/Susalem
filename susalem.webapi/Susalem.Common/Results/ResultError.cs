namespace Susalem.Common.Results
{
    public class ResultError : IResultError
    {
        public string Error { get; private set; }
        public string Code { get; private set; }

        public ResultError()
        {

        }

        public ResultError(string error)
        {
            Error = error;
        }

        public ResultError(string error, string code) : this(error)
        {
            Code = code;
        }

        public override string ToString()
        {
            return $"Error[{Code}]:{Error}";
        }
    }
}
