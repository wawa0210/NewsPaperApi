using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmergencyAccount.Application;
using EmergencyAccount.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("v0/accounts")]
    public class AccountController : BaseApiController
    {
        private IAccountService IAccountService { get; set; }

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public AccountController(IAccountService iAccountService)
        {
            IAccountService = iAccountService;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="entityAccountPwd"></param>
        /// <returns></returns>
        [HttpPut, HttpOptions]
        [Route("pwd")]
        public async Task<ResponseModel> UpdateAccountPwd([FromBody]EntityAccountPwd entityAccountPwd)
        {
            await IAccountService.UpdateAccountPwd(entityAccountPwd);
            return Success("更新成功");
        }
    }
}