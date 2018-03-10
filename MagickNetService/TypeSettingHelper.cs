using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MagickNetService
{
    /// <summary>
    /// 字符串排版管理类
    /// </summary>
    public class TypeSettingHelper
    {
        /// <summary>
        /// 把字符串按照指定长度切割(英文单词不拆分)
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <param name="splitLength">分割字节数</param>
        /// <returns></returns>
        public string[] GetTypeSettingArray(string strContent, int splitLength)
        {
            var strArray = GetSplitStrByte(strContent);
            var listJoinArray = GetJoinStrBytes(strArray, splitLength);
            return GetStrContentByByteArray(listJoinArray);
        }

        private static string[] GetStrContentByByteArray(List<byte[]> byteArray)
        {
            var result = new List<string>();
            byteArray.ForEach(item =>
            {
                var s = Encoding.GetEncoding("GB2312").GetString(item);
                result.Add(s);
            });
            return result.ToArray();
        }

        /// <summary>
        /// 字节数组拼接(汉字、英文)
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="joinCount"></param>
        /// <returns></returns>
        private static List<byte[]> GetJoinStrBytes(List<byte[]> byteArray, int joinCount)
        {
            var result = new List<byte[]>();
            var itemJoinList = new List<byte>();
            for (var i = 0; i < byteArray.Count; i++)
            {
                //如果空格在首位 移除
                if (!itemJoinList.Any() && byteArray[i][0] == 32)
                {
                    continue;
                }

                if (itemJoinList.Count + byteArray[i].Length > joinCount)
                {
                    result.Add(itemJoinList.ToArray());
                    itemJoinList.Clear();
                    --i;
                }
                else
                {
                    itemJoinList.AddRange(byteArray[i].ToList());
                    if (i != byteArray.Count - 1) continue;
                    //最后一个元素仍然未补齐，直接返回
                    result.Add(itemJoinList.ToArray());
                    itemJoinList.Clear();
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据包含中英文的字符串，得到分割后的字符串(单个汉字+连续英文)
        /// </summary>
        /// <param name="roughStr"></param>
        /// <returns></returns>
        private List<byte[]> GetSplitStrByte(string roughStr)
        {
            var result = new List<byte[]>();
            //英文 直接返回
            if (IsEnglish(roughStr)) return new List<byte[]> { Encoding.GetEncoding("GB2312").GetBytes(roughStr) };
            var roughItemArray = Encoding.GetEncoding("GB2312").GetBytes(roughStr);

            for (var i = 0; i < roughItemArray.Length; i++)
            {
                //汉字 汉字占两位，每一位都>127
                if (roughItemArray[i] > 127)
                {
                    var chineseArray = new List<byte> { roughItemArray[i], roughItemArray[++i] };
                    result.Add(chineseArray.ToArray());
                }
                //空格
                else if (roughItemArray[i] == 32)
                {
                    var chineseArray = new List<byte> { roughItemArray[i] };
                    result.Add(chineseArray.ToArray());
                }
                else
                {
                    //英文的操作
                    var listEngArray = new List<byte>();
                    while (roughItemArray[i] <= 127)
                    {
                        //空格 直接跳出
                        if (roughItemArray[i] == 32)
                        {
                            break;
                        }
                        listEngArray.Add(roughItemArray[i]);
                        //最后一位 不做操作
                        if (i == roughItemArray.Length - 1)
                            break;
                        ++i;
                    }
                    if (i != roughItemArray.Length - 1)
                    {
                        --i;
                    }
                    result.Add(listEngArray.ToArray());
                }
            }
            return result;
        }

        private static bool IsEnglish(string text)
        {
            var regex = new Regex("^[A-Za-z]+$");
            return regex.IsMatch(text);
        }

    }
}
