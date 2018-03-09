using System;
using System.Linq;
using System.Threading.Tasks;
using CommonLib.Extensions;
using EmergencyAccount.Entity;
using Infrastructure.Context;
using Infrastructure.Exception;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filter
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(AuthorizeFilter)))
            {
                var currentUser = context.HttpContext.User;
                var userContextStr = currentUser.Claims.FirstOrDefault()?.Value;
                if(string.IsNullOrWhiteSpace(userContextStr)) throw new ApiException(ApiStatusEnum.NotAuthenticated);
                try
                {
                    var userContext = userContextStr.JsonToObj<EntityAccountManager>();
                    if (userContext == null) throw new ApiException(ApiStatusEnum.NotAuthenticated);
                    CallContext<EntityAccountManager>.SetData("userContext",userContext);
                }
                catch (Exception e)
                {
                    throw new ApiException(ApiStatusEnum.NotAuthenticated);
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
