using Exceptionless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommonLib;
using log4net;
using Microsoft.Extensions.Logging;

namespace WebApi.Filter
{
    /// <summary>
    /// 异常捕捉器
    /// </summary>
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
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
            LogHelper.LogInfo("asdasdhakslj");


            //记录日志
            exception.ToExceptionless().Submit();
            //返回友好的提示
            var response = context.Response;

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

            response.ContentType = context.Request.Headers["Accept"];

            if (response.ContentType.ToLower() == "application/xml")
            {
                await response.WriteAsync(exception.Message).ConfigureAwait(false);
            }
            else
            {
                await response.WriteAsync(exception.Message);
            }
        }

        /// <summary>
        /// 对象转为Xml
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string Object2XmlString(object o)
        {
            StringWriter sw = new StringWriter();
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
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