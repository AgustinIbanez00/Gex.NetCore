using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gex.NetCore.Helpers;
using SmartFormat;

namespace Gex.NetCore.Utils;
public class GexResponse<T> where T : class
{
    public T Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = null;
    public IDictionary<string, string[]> ErrorMessages { get; set; } = null;
    public GexErrorMessage ErrorCode { get; set; }

    public static GexResponse<T> Error(string title, string message)
    {
        return new GexResponse<T> { ErrorCode = GexErrorMessage.Generic, Success = false, Message = title, ErrorMessages = new Dictionary<string, string[]>() { { "", new string[] { message } } } };
    }
    public static GexResponse<T> ErrorF(GexErrorMessage error, GexResponseOptions options, [Optional] string message)
    {
        return new GexResponse<T>
        {
            ErrorCode = error,
            Success = false,
            Message = Smart.Format(Enumerations.GetEnumDescription(error), options),
            ErrorMessages = string.IsNullOrEmpty(message) ? new Dictionary<string, string[]>() : new Dictionary<string, string[]>() { { "", new string[] { message } } }
        };
    }

    public static GexResponse<T> Error(string title, string key, string message)
    {
        return new GexResponse<T>
        {
            Success = false,
            Message = title,
            ErrorMessages = string.IsNullOrEmpty(message) ? new Dictionary<string, string[]>() : new Dictionary<string, string[]>() { { key, new string[] { message } } }
        };
    }
    public static GexResponse<T> Error(string message)
    {
        return new GexResponse<T>
        {
            Success = false,
            Message = message,
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }
    public static GexResponse<T> Ok(T data)
    {
        return new GexResponse<T>
        {
            Data = data,
            Success = true,
            Message = "Correcto.",
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }
    public static GexResponse<T> Ok(GexSuccessMessage message, GexResponseOptions options)
    {
        return new GexResponse<T>
        {
            Success = true,
            Message = Smart.Format(Enumerations.GetEnumDescription(message), options),
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }
    public static GexResponse<T> Ok(T data, string message)
    {
        return new GexResponse<T>
        {
            Data = data,
            Success = true,
            Message = message,
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }
    public static GexResponse<T> Ok(T data, GexSuccessMessage message, GexResponseOptions options)
    {
        return new GexResponse<T>
        {
            Data = data,
            Success = true,
            Message = Smart.Format(Enumerations.GetEnumDescription(message), options),
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }
    
}
