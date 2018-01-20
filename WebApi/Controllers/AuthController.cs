using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLib;
using EmergencyAccount.Application;
using EmergencyAccount.Entity;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("v0/auth")]
    public class AuthController : BaseApiController
    {
        private IAccountService IAccountService { get; set; }

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public AuthController(IAccountService _iAccountService)
        {
            IAccountService = _iAccountService;
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
            var result = IAccountService.GetAccountManager(loginModel.UserName);

            if (result == null) return Fail(ErrorCodeEnum.UserIsNull);

            var checkResult = IAccountService.CheckLoginInfo(loginModel.UserPwd, result.UserSalt, result.UserPwd);
            if (!checkResult) return Fail(ErrorCodeEnum.UserPwdCheckFaild);

            return Success(new
            {
                token = AesHelper.Encrypt(JsonConvert.SerializeObject(result)),
                userInfo = result
            });
        }


        /// <summary>
        /// 获得token 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpOptions]
        [AllowAnonymous]
        [Route("token")]
        public ResponseModel GetAccountAuth1()
        {
            using (var image = new MagickImage(new MagickColor("#ff00ff"), 512, 128))
            {
                new Drawables()
                  .FontPointSize(72)
                  .Font("Comic Sans")
                  .StrokeColor(new MagickColor("yellow"))
                  .FillColor(MagickColors.Orange)
                  .TextAlignment(TextAlignment.Center)
                  .Text(256, 64, "Magick.NET")
                  .StrokeColor(new MagickColor(0, Quantum.Max, 0))
                  .FillColor(MagickColors.SaddleBrown)
                  .Ellipse(256, 96, 192, 8, 0, 360)
                  .Draw(image);
                image.Write("wawa.jpg");
            };

            return Success("");
        }
    }
}