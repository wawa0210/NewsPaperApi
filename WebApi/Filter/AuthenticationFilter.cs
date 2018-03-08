using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Exception;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filter
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //if (context.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(AllowAnonymousFilter))) return;
            //var request = context.HttpContext.Request.Headers;

            //var tokenKey = request.SingleOrDefault(x => x.Key.ToLower() == "token");
            //var token = tokenKey.Key == null ? string.Empty : tokenKey.Value.FirstOrDefault();

            //if (string.IsNullOrWhiteSpace(token))
           throw new ApiException(ApiStatusEnum.NotAuthenticated);

            //return;
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
