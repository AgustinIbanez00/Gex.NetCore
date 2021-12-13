using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Gex.Attributes;
using Gex.Extensions;
using Gex.Extensions.Response;
using Humanizer;
using SmartFormat;

namespace Gex.Utils;
public static class GexResponse
{
    /*
    public static GexResult Error(string title, string message)
    {
        return new GexResult { ErrorCode = GexErrorMessage.Generic, Success = false, Message = title, ErrorMessages = new Dictionary<string, string[]>() { { "", new string[] { message } } } };
    }
    */
    /*
    public static GexResult Error(GexErrorMessage error, GexResponseOptions options, [Optional] string message)
    {
        return new GexResult
        {
            ErrorCode = error,
            Success = false,
            Message = Smart.Format(EnumExtensions.GetDescription(error), options),
            ErrorMessages = string.IsNullOrEmpty(message) ? new Dictionary<string, string[]>() : new Dictionary<string, string[]>() { { error == GexErrorMessage.Generic ? "Error" : "", new string[] { message } } }
        };
    }
    */

    public static GexResult<TResult> KeyError<TResult>(string key, string message) where TResult : class
    {
        GexErrorMessage error = GexErrorMessage.GenericValidation;
        return new GexResult<TResult>
        {
            ErrorCode = error,
            Success = false,
            Message = error.GetDescription(),
            ErrorMessages = string.IsNullOrEmpty(message) ? new Dictionary<string, string[]>() : new Dictionary<string, string[]>() { { new string(key.ToSnakeCase().ToArray()), new string[] { message } } }
        };
    }

    public static GexResult<TResult> KeyError<TEntity, TResult>(string key, GexErrorMessage error) where TResult : class
    {
        return new GexResult<TResult>
        {
            ErrorCode = error,
            Data = default(TResult),
            Success = false,
            Message = "Se encontraron uno o más errores.",
            ErrorMessages =  new Dictionary<string, string[]>() { { new string(key.ToSnakeCase().ToArray()), new string[] { Smart.Format(EnumExtensions.GetDescription(error), Options<TEntity>()) } } }
        };
    }

    public static GexResult<TResult> KeyError<TEntity, TResult>(string key, GexErrorMessage error, TResult result) where TResult : class
    {
        return new GexResult<TResult>
        {
            ErrorCode = error,
            Data = result,
            Success = false,
            Message = "Se encontraron uno o más errores.",
            ErrorMessages = new Dictionary<string, string[]>() { { new string(key.ToSnakeCase().ToArray()), new string[] { Smart.Format(EnumExtensions.GetDescription(error), Options<TEntity>()) } } }
        };
    }

    public static GexResult<TResult> KeyOk<TEntity, TResult>(string key, GexSuccessMessage message) where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = default(TResult),
            Success = true,
            Message = "Correcto.",
            ErrorMessages = new Dictionary<string, string[]>() { { new string(key.ToSnakeCase().ToArray()), new string[] { Smart.Format(EnumExtensions.GetDescription(message), Options<TEntity>()) } } }
        };
    }

    public static GexResult<TResult> KeyOk<TEntity, TResult>(string key, GexSuccessMessage message, TResult result) where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = result,
            Success = true,
            Message = "Correcto.",
            ErrorMessages = new Dictionary<string, string[]>() { { new string(key.ToSnakeCase().ToArray()), new string[] { Smart.Format(EnumExtensions.GetDescription(message), Options<TEntity>()) } } }
        };
    }


    /// <summary>
    /// Devuelve un objeto personalizado del sistema del tipo TResult con el mensaje de error computado de TEntity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="error"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static GexResult<TResult> Error<TEntity, TResult>(GexErrorMessage error, [Optional] string message) where TResult : class
    {
        return new GexResult<TResult>
        {
            ErrorCode = error,
            Data = default(TResult),
            Success = false,
            Message = Smart.Format(EnumExtensions.GetDescription(error), Options<TEntity>()),
            ErrorMessages = string.IsNullOrEmpty(message) ? new Dictionary<string, string[]>() : new Dictionary<string, string[]>() { { "error", new string[] { message } } }
        };
    }

    private static GexResponseOptions Options<TEntity>()
    {
        var options = new GexResponseOptions()
        {
            Entity = typeof(TEntity).GetAttributeValue<GexDescriptionAttribute, string>(c => c.Name),
            Gender = typeof(TEntity).GetAttributeValue<GexDescriptionAttribute, GrammaticalGender>(c => c.Gender)
        };

        if(string.IsNullOrEmpty(options.Entity))
        {
            options.Entity = typeof(TEntity).Name.Humanize(LetterCasing.LowerCase);
            options.Gender = GrammaticalGender.Masculine;
        }
        return options;
    }

    public static GexResult<TResult> Ok<TResult>(TResult data) where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = data,
            Success = true,
            Message = "Correcto.",
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }

    public static GexResult<TResult> Ok<TResult>() where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = default(TResult),
            Success = true,
            Message = "Correcto.",
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }

    /*
    public static GexResult Ok(GexSuccessMessage message, GexResponseOptions options)
    {
        return new GexResult
        {
            Success = true,
            Message = Smart.Format(EnumExtensions.GetDescription(message), options),
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }
    */
    public static GexResult<TResult> Ok<TResult>(TResult data, string message) where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = data,
            Success = true,
            Message = message,
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }

    public static GexResult<TResult> Ok<TEntity, TResult>(TResult data, GexSuccessMessage message) where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = data,
            Success = true,
            Message = Smart.Format(message.GetDescription(), Options<TEntity>()),
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }

    public static GexResult<TResult> Ok<TEntity, TResult>(GexSuccessMessage message) where TResult : class
    {
        return new GexResult<TResult>
        {
            Data = default(TResult),
            Success = true,
            Message = Smart.Format(message.GetDescription(), Options<TEntity>()),
            ErrorMessages = new Dictionary<string, string[]>()
        };
    }

}
