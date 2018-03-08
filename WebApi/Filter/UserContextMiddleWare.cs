using CommonLib.Extensions;
using EmergencyAccount.Entity;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filter
{
    public class UserContextMiddleWare
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userContext = context.User.Claims.FirstOrDefault().Value;
            if (!string.IsNullOrEmpty(userContext))
            {
                CallContext.SetData("userInfo", userContext.JsonToObj<EntityAccountManager>());
            }

            await _next(context);
        }
    }
}
