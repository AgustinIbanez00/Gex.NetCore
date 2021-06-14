
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gex.NetCore.Helpers
{
    public static class Errors
    {
        public static ModelStateDictionary AddErrorsToModelState(IdentityResult identityResult, ModelStateDictionary modelState)
        {
            foreach (var e in identityResult.Errors)
            {
                modelState.TryAddModelError(e.Code, e.Description);
            }

            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string code, string description, ModelStateDictionary modelState)
        {
            modelState.TryAddModelError(code, description);
            return modelState;
        }

        public static List<ModelErrors> GetModelStateErrors(ModelStateDictionary Model)
        {
            return Model.Select(x => new ModelErrors() { Key = x.Key, Messages = x.Value.Errors.Select(y => y.ErrorMessage).ToList() }).ToList();
        }
        public static ResponseError Response(int StatusCode, string Message)
        {
            return new ResponseError()
            {
                StatusCode = StatusCode,
                Message = Message
            };
        }

        public static ResponseError InvalidCredentials(string customMessage = "La dirrección de correo o contraseña son incorrectas.") => Response(404, customMessage);

    }

    public class ResponseError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ModelErrors
    {
        public string Key { get; set; }
        public List<string> Messages { get; set; }
    }

}
