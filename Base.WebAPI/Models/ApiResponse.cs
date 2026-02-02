using Base.Domain.Common;

namespace Base.WebAPI.Models;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    ErrorCode? ErrorCode,
    string? ErrorMessage
)
{
    public static ApiResponse<T> Ok(T data) => new(true, data, null, null);
    public static ApiResponse<T> Failure(ErrorCode errorCode) => new(false, default, errorCode, errorCode.GetMessage());
}
