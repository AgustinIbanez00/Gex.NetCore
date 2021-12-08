﻿using System.Linq;
using Gex.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Gex.Middlewares;
public static class BadRequestMiddleware
{
    public static IMvcBuilder AddBadRequestServices(this IMvcBuilder services)
    {
        services.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var modelState = actionContext.ModelState;

                return new BadRequestObjectResult(new GexResponse<string>
                {
                    Data = null,
                    ErrorMessages = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                ),
                    Success = false,
                    Message = "Se encontraron uno o más errores."
                }); ;
            };
        });

        return services;
    }
}