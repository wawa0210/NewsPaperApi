using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmergencyEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Nito.AsyncEx;
using WebApi.FrameWork;

namespace WebApi.Filter
{
    /// <summary>
    /// 授权判断
    /// </summary>
    public class AuthActionFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //匿名接口 直接返回
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            var request = context.HttpContext.Request.Headers;
            var tokenKey = request.SingleOrDefault(x => x.Key.ToLower() == "token");
            var token = tokenKey.Key == null ? "" : tokenKey.Value.FirstOrDefault();
            //根据token得到上下文信息
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("授权未通过,token是必须的");
            }
            var loginInfo = JwtManager.GetUserLogin(token);

            UserContext.Current.Value = new AccountContext
            {
                UserName = loginInfo.UserName
            };
        }
    }
}
