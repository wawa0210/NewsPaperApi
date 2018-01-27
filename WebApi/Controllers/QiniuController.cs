using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Qiniu;

namespace WebApi.Controllers
{
    [Route("v0/qinius")]
    public class QiniuController : BaseApiController
    {
        /// <summary>
        /// 获得qiniutoken信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("token")]
        public ResponseModel GetQiniuToken()
        {
            return Success(new QiniuService().GetToken());
        }

       
    }
}