using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtensions
    {
        public static int ToInt32(this string value, int defaultVal = 0)
        {
            if (string.IsNullOrEmpty(value)) return defaultVal;

            var result = defaultVal;
            int.TryParse(value, out result);

            return result;
        }
    }
}
