using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CommonLib.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Object 转换成 Json 字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="camelCase">驼峰命名</param>
        /// <param name="indented">缩进</param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool camelCase = false, bool indented = false)
        {
            if (obj != null)
            {
                var options = new JsonSerializerSettings()
                {
                    //忽略循环引用
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (camelCase)
                {
                    options.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }

                if (indented)
                {
                    options.Formatting = Formatting.Indented;
                }

                return JsonConvert.SerializeObject(obj, options);
            }
            return String.Empty;
        }
    }
}
