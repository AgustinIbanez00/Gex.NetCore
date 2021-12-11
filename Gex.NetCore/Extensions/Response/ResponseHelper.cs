using Microsoft.AspNetCore.Http;

namespace Gex.Extensions.Response;
public static class ResponseHelper
{
    public static int GetHttpError(GexErrorMessage gexError)
    {
        return gexError switch
        {
            GexErrorMessage.NotFound => StatusCodes.Status404NotFound,
            GexErrorMessage.Generic or GexErrorMessage.CouldNotDelete or GexErrorMessage.CouldNotDelete or GexErrorMessage.CouldNotUpdate => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
