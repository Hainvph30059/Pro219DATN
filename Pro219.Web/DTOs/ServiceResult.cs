namespace Pro219.Web.DTOs
{
    public class ServiceResult<T>
    {
        public T Data { get; init; }

        public bool IsSuccess { get; init; }

        public string ErrorCode { get; init; }

        public string ErrorMessage { get; init; }

        public string StatusCode { get; init; }

        public static ServiceResult<T> Success(T data) => new ServiceResult<T>
        {
            Data = data,
            IsSuccess = true,
            ErrorCode = null,
            ErrorMessage= null,
            StatusCode = null
        };

        public static ServiceResult<T> Failure(string errorCode, string errorMess, string statusCode) => new ServiceResult<T>
        {
            Data = default,
            IsSuccess = false,
            ErrorCode = errorCode,
            ErrorMessage = errorMess,
            StatusCode = statusCode
        };
    }
}
