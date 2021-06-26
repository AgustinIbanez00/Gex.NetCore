
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gex.NetCore.Helpers
{
    public enum GexError
    {
        [Description("La dirección de correo o contraseña son incorrectas.")]
        InvalidCredentials,

        [Description("No se puede crear un(a) {0} con el mismo identificador.")]
        DuplicatedEntity,

        [Description("No se encontró ningun(a) {0} con los datos ingresados.")]
        NotFound,


    }

    public static class ResponseHelper
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
        public static ResponseSingle Response(GexError StatusCode, [Optional] string Message)
        {
            return new ResponseSingle()
            {
                Error = new ResponseError()
                {
                    Message = Message = !string.IsNullOrEmpty(Message) ? Message : Enumerations.GetEnumDescription(StatusCode),
                    StatusCode = StatusCode
                }                
            };
        }

        public static ResponseSingle InvalidCredentials([Optional] string message) 
            => Response(GexError.InvalidCredentials, message);
    }

    public class ResponseSingle
    {
        public ResponseError Error { get; set; }
    }

    public class ResponseError
    {
        public string Message { get; set; }
        public GexError StatusCode { get; set; }
    }

    public class ModelErrors
    {
        public string Key { get; set; }
        public List<string> Messages { get; set; }
    }

}
