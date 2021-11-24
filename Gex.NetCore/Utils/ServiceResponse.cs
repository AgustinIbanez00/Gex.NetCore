using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gex.NetCore.Helpers;

namespace Gex.NetCore.Utils;
public class ServiceResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; } = true;

    public string Message { get; set; } = null;
    public IDictionary<string, string[]> ErrorMessages { get; set; } = null;

    public GexError ErrorCode { get; set; }

    public static ServiceResponse<T> Error(string title, string message)
    {
        return new ServiceResponse<T> { ErrorCode = GexError.Generic, Success = false, Message = title, ErrorMessages = new Dictionary<string, string[]>() { { "", new string[] { message } } } };
    }

    public static ServiceResponse<T> FormattedError(GexError error, string entity, [Optional] string message)
    {
        return new ServiceResponse<T>
        {
            ErrorCode = error,
            Success = false,
            Message = !string.IsNullOrEmpty(message) ? message : string.Format(Enumerations.GetEnumDescription(error), entity),
            ErrorMessages = string.IsNullOrEmpty(message) ? new Dictionary<string, string[]>() : new Dictionary<string, string[]>() { { "", new string[] { message } } }
        };
    }


    public static ServiceResponse<T> Error(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = message,
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }

    public static ServiceResponse<T> Ok(T data)
    {
        return new ServiceResponse<T> { Data = data, Success = true, Message = "Correcto.", ErrorMessages = new Dictionary<string, string[]>() };
    }

    public static ServiceResponse<T> Ok(T data, string message)
    {
        return new ServiceResponse<T> { Data = data, Success = true, Message = message, ErrorMessages = new Dictionary<string, string[]>() };
    }
}
