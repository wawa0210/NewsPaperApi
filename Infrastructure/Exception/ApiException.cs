using CommonLib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exception
{
    /// <summary>
    /// api 异常信息
    /// </summary>
    public class ApiException : System.Exception
    {
        public ApiException() : base(string.Empty)
        {
        }

        public ApiStatusEnum ApiStatusCodeEnum { get; set; }

        public ApiException(ApiStatusEnum apiStatusCodeEnum) : base(EnumExtensionHelper.GetEnumDescription(apiStatusCodeEnum))
        {
            this.ApiStatusCodeEnum = apiStatusCodeEnum;
        }

        public ApiException(ApiStatusEnum apiStatusCodeEnum,string exceptionMsg) : base(exceptionMsg)
        {
            this.ApiStatusCodeEnum = apiStatusCodeEnum;
        }
    }
}
