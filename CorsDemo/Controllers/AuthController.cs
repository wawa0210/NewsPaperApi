using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmergencyAccount.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorsDemo.Controllers
{
    [Route("v0/auth")]
    public class AuthController : Controller
    {
        /// <summary>
        /// 获得token 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpOptions]
        [AllowAnonymous]
        [Route("token")]
        public string GetAccountAuth([FromBody]EntityLoginModel loginModel)
        {
            return "";
        }
    }
}