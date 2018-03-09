using Exceptionless;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommonLib.Logger;
using Exceptionless.Json;
using Infrastructure.Exception;
using WebApi.Models;

namespace WebApi.Filter
{
    /// <summary>
    /// 异常捕捉器
    /// </summary>
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                var responseStatusCode = context.Response.StatusCode;

                if (responseStatusCode != (int)ApiStatusEnum.Ok)
                {
                    throw new ApiException((ApiStatusEnum)responseStatusCode);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            LogHelper.LogError("", exception);
            //记录日志
            exception.ToExceptionless().Submit();
            //返回友好的提示
            var response = context.Response;
            response.ContentType = context.Request.Headers["Accept"];

            var objMsg = "";

            if (exception is ApiException apiException)
            {
                response.StatusCode = (int)apiException.ApiStatusCodeEnum;

                var responseObj = new ResponseModel
                {
                    Message = apiException.Message,
                    Code = (int)ErrorCodeEnum.Unauthorized
                };

                objMsg = JsonConvert.SerializeObject(responseObj);
            }
            else
            {
                //状态码
                switch (exception)
                {
                    case UnauthorizedAccessException _:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case Exception _:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                }
                objMsg = exception?.Message;
            }


            if (response.ContentType.ToLower() == "application/xml")
            {
                await response.WriteAsync(exception?.Message).ConfigureAwait(false);
            }
            else
            {
                response.ContentType = "application/json";
                await response.WriteAsync(objMsg);
            }

        }

        /// <summary>
        /// 对象转为Xml
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string Object2XmlString(object o)
        {
            var sw = new StringWriter();
            try
            {
                var serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(sw, o);
            }
            catch
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Dispose();
            }
            return sw.ToString();
        }
    }
}