using System;

namespace CommonLib.Extensions
{
    public class GuidExtens
    {
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            var i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= b + 1;
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongId()
        {
            var buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
