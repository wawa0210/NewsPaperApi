using Exceptionless.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filter
{
    public class ExceptionMiddleware
    {
        public async Task HandleExceptionAsync(HttpContext context, Func<Task> next)
        {
            var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

            //when authorization has failed, should retrun a json message to client
            if (error != null && error.Error is SecurityTokenExpiredException)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    State = "Unauthorized",
                    Msg = "token expired"
                }));
            }
            //when orther error, retrun a error message json to client
            else if (error != null && error.Error != null)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    State = "Internal Server Error",
                    Msg = error.Error.Message
                }));
            }
            //when no error, do next.
            else await next();
        }
    }
}
