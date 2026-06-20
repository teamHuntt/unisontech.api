namespace unisontech.api.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public string? Error { get; init; }
        public T? Data { get; init; }

        public static ApiResponse<T> Ok(T data, string? message = null) => new()
        {
            Success = true,
            Message = message,
            Data = data
        };

        public static ApiResponse<T> Fail(string error) => new()
        {
            Success = false,
            Error = error
        };
    }

    // Non-generic version for responses with no data (e.g. register, logout)
    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse Ok(string message) => new()
        {
            Success = true,
            Message = message
        };

        public new static ApiResponse Fail(string error) => new()
        {
            Success = false,
            Error = error
        };
    }
}
