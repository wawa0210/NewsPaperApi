using CommonLib;
using EmergencyAccount.Application;
using EmergencyAccount.Entity;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.FrameWork;
using WebApi.Models;
using WebApi.Qiniu;

namespace WebApi.Controllers
{
    [Route("v0/auth")]
    public class AuthController : BaseApiController
    {
        private IAccountService AccountService { get; set; }

        ///// <summary>
        ///// 初始化(autofac 已经注入)
        ///// </summary>
        //public AuthController(IAccountService _iAccountService)
        //{
        //    IAccountService = _iAccountService;
        //}

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public AuthController()
        {
            AccountService = new AccountService();
        }

        /// <summary>
        /// 获得token 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpOptions]
        [AllowAnonymous]
        [Route("token")]
        public ResponseModel GetAccountAuth([FromBody]EntityLoginModel loginModel)
        {
            var result = AccountService.GetAccountManager(loginModel.UserName);

            if (result == null) return Fail(ErrorCodeEnum.UserIsNull);

            var checkResult = AccountService.CheckLoginInfo(loginModel.UserPwd, result.UserSalt, result.UserPwd);
            if (!checkResult) return Fail(ErrorCodeEnum.UserPwdCheckFaild);

            loginModel.UserPwd = "";
            return Success(new
            {
                token = JwtManager.GenerateToken(loginModel),
                userInfo = result
            });
        }
    }
}