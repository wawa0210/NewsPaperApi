using Newtonsoft.Json;

namespace CommonLib.Extensions
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtensions
    {
        public static int ToInt32(this string value, int defaultVal = 0)
        {
            if (string.IsNullOrEmpty(value)) return defaultVal;
            int.TryParse(value, out var result);
            return result;
        }

        public static T JsonToObj<T>(this string json)
        {
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
    }
}
