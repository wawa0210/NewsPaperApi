using System;
using CommonLib;
using CommonLib.Extensions;

namespace WebApi.Models
{
    /// <summary>
    /// 请求响应(Api)
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel()
        {
            TimeStamp = DateTime.Now.ToTimeStamp();
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据实体
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        ///时间戳
        /// </summary>
        public long TimeStamp
        {
            get; set;
        }

    }

    /// <summary>
    /// 返回序列化的api结果
    /// </summary>
    public class ResponseSerializationModel<T> where T : class
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据实体
        /// </summary>
        public T Data { get; set; }
    }
}
