namespace Susalem;

public class ApiResponse<T>
{
    public int Code { get; set; }

    public T Data { get; set; }

    public string Message { get; set; }

    public static ApiResponse<T> Success(T data)
    {
        return new()
        {
            Code = 200,
            Data = data,
            Message = "Success"
        };
    }

    public static ApiResponse<T> Error(int code, string message)
    {
        return new()
        {
            Code = code,
            Data = default,
            Message = message
        };
    }
}