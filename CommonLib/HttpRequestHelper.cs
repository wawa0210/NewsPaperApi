using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace CommonLib
{
    public static class HttpRequestHelper
    {
        #region Get
        public static async Task<T> DoGetAsync<T>(this string requestUrl, object queryParam)
        {
            if (string.IsNullOrEmpty(requestUrl)) throw new ArgumentNullException("requestUrl");

            return await requestUrl
                 .SetQueryParams(queryParam)
                .GetJsonAsync<T>();
        }

        public static async Task<string> DoGetAsync(this string requestUrl, object queryParam)
        {
            if (string.IsNullOrEmpty(requestUrl)) throw new ArgumentNullException("requestUrl");

            return await requestUrl
                .SetQueryParams(queryParam)
                .GetJsonAsync();
        }
        #endregion


        #region Post
        #endregion
    }
}
