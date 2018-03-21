using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filter
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var filterDescriptors in context.ActionDescriptor.FilterDescriptors)
            {
                if (filterDescriptors.Filter.GetType() == typeof(AllowAnonymousFilter))
                {
                    return;
                }
            }

        }
    }
}
