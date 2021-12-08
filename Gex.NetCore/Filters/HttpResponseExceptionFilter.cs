using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gex.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is Exception httpResponseException)
        {
            context.Result = new ObjectResult(httpResponseException)
            {
                Value = context.Exception.Message,
                StatusCode = -1
            };

            context.ExceptionHandled = true;
        }
    }
}
