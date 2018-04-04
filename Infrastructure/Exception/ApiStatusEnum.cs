using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infrastructure.Exception
{
    public enum ApiStatusEnum
    {
        /// <summary>
        /// 操作成功 
        /// </summary>
        [Description("操作成功")]
        Ok = 200,
        
        /// <summary>
        /// 请求错误
        /// </summary>
        [Description("{0}")]
        BadRequest = 400,

        /// <summary>
        /// 
        /// </summary>
        [Description("授权验证不通过，尝试重新登录")]
        NotAuthenticated = 401,
        /// <summary>
        /// 账户权限不足
        /// </summary>
        [Description("权限不足")]
        NotAuthorized = 403,
        /// <summary>
        /// 未找到资源 
        /// </summary>
        [Description("未找到")]
        NotFound = 404,

        /// <summary>
        /// 系统错误 StatusCode=500
        /// </summary>
        [Description("系统错误")]
        SystemError = 500,
    }
}
