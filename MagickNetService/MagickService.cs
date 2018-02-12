using CommonLib;
using ImageMagick;
using MagickNetService.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MagickNetService
{
    public class MagickService
    {
        public byte[] GenerateNewImg(EntityNewsModel entityNewsModel)
        {
            var typeSettingHelper = new TypeSettingHelper();

            var wordsArray = typeSettingHelper.GetTypeSettingArray(entityNewsModel.Content, 40);
            var titleArray = typeSettingHelper.GetTypeSettingArray(entityNewsModel.Title, 28);

            var titleHeight = titleArray.Length * 100 + 100;
            var contentHeight = wordsArray.Length * 80 + 80;
            var microsoftYaheiUi = "SimHei";
            var picHeight = titleHeight + contentHeight + 50;
            var lineColor = new MagickColor("gray");

            using (var image = new MagickImage(new MagickColor("#F9F8F6"), 1080, picHeight + 550))
            {
                using (var mainImgImage = new MagickImage(new MagickColor("#FFFFFF"), 900, picHeight))
                {
                    //title
                    for (var i = 0; i < titleArray.Length; i++)
                    {
                        new Drawables()
                            .TextEncoding(Encoding.UTF8)
                            .TextAntialias(true)
                            .FontPointSize(58)
                            .FillColor(new MagickColor("#856D32"))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(50, 80 + 90 * i, titleArray[i])
                            .Draw(mainImgImage);

                    }

                    for (var i = 0; i < 82; i++)
                    {
                        var beginWidth = 30 + 10 * i;

                        var endWidth = 35 + 10 * i;

                        //头部间隔线
                        new Drawables().StrokeWidth(0.5).StrokeColor(lineColor).Line(beginWidth, titleHeight, endWidth, titleHeight).Draw(mainImgImage);
                    }

                    for (var i = 0; i < wordsArray.Length; i++)
                    {
                        new Drawables()
                            .TextEncoding(Encoding.UTF8)
                            .TextAntialias(true)
                            .FontPointSize(40)
                            .FillColor(new MagickColor("#4D4D4D"))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(50, titleHeight + 70 + 80 * i, wordsArray[i])
                            .Draw(mainImgImage);
                    }
                    //尾部
                    //new Drawables().StrokeWidth(0.5).StrokeColor(lineColor).Line(20, picHeight - 180, 880, picHeight - 180).Draw(mainImgImage);
                    mainImgImage.Border(1);
                    mainImgImage.BorderColor = new MagickColor("#4D4D4D");

                    image.Composite(mainImgImage, 90, 150, CompositeOperator.Over);
                }

                //小程序二维码 
                var client = new WebClient();
                using (var watermark = new MagickImage(client.DownloadData("http://img.blockcomet.com/wechatCode.png")))
                {
                    var size = new MagickGeometry(200, 200) { IgnoreAspectRatio = true };
                    watermark.Resize(size);
                    watermark.Evaluate(Channels.Black, EvaluateOperator.Divide, 4);
                    image.Composite(watermark, 440, picHeight + 230, CompositeOperator.Over);
                }
                return image.ToByteArray(MagickFormat.Jpg);
            }
            //return imgUrl;
        }

        private string[] SplitByLen(string str, int separatorCharNum)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= separatorCharNum)
            {
                return new[] { str };
            }
            var tempStr = str;
            var strList = new List<string>();
            var iMax = Convert.ToInt32(Math.Ceiling(str.Length / (separatorCharNum * 1.0)));//获取循环次数    
            for (var i = 1; i <= iMax; i++)
            {
                var currMsg = tempStr.Substring(0, tempStr.Length > separatorCharNum ? separatorCharNum : tempStr.Length);
                strList.Add(currMsg);
                if (tempStr.Length > separatorCharNum)
                {
                    tempStr = tempStr.Substring(separatorCharNum, tempStr.Length - separatorCharNum);
                }
            }
            return strList.ToArray();
        }

        /// <summary>  
        /// 根据字符集,在字节级别分割为等长字符串数组  
        /// create by lz 2017-11-24  
        /// </summary>  
        /// <param name="strcnt"></param>  
        /// <param name="lenByteCount">注意这是字节长度</param>  
        /// <returns></returns>  
        private static string[] SubStrToSaLen(string strcnt, int lenByteCount)
        {
            var lstStr = new List<string>();
            var retStr = strcnt;
            if (retStr.Length == 0)
            {
                return new[] { retStr };
            }
            var byteArray = Encoding.GetEncoding("GB2312").GetBytes(strcnt);//.GetBytes(str);  
            if (byteArray.Length < lenByteCount)
                return new[] { strcnt };


            var orgMod = (byteArray.Length / lenByteCount);
            var lenCod = byteArray.Length % lenByteCount; //余数  
            if (lenCod > 0)
            {
                var lstbyte = new List<byte>();

                lstbyte.AddRange(byteArray);
                for (var i = lstbyte.Count; i < (orgMod + 1) * lenByteCount; i++)
                {
                    lstbyte.Add(32);//用空格填充成整倍数  
                }
                byteArray = lstbyte.ToArray();
            }
            var lenmod = byteArray.Length / lenByteCount; //整倍数  
            var nstart = 0;
            for (var i = 1; i <= lenmod; i++)
            {
                var start = nstart;
                var ba = new byte[lenByteCount];
                var sublen = lenByteCount;
                var nCount = 0;
                var eCount = 0;
                for (var j = 0; j < lenByteCount; j++)
                {
                    ba[j] = byteArray[start + j];
                    if (ba[j] > 127)
                        nCount++;//是汉字就统计数量  
                    else
                        eCount++;
                }
                if (nCount % 2 != 0 && eCount % 2 != 0) //如果是奇数，就有半个汉字，丢失最后一个字节，  
                {
                    sublen = sublen - 1;
                }
                nstart = start + sublen;//设置下一个字符开始位置  
                retStr = Encoding.GetEncoding("GB2312").GetString(byteArray, start, sublen);
                var arbyte = Encoding.GetEncoding("GB2312").GetBytes(retStr);
                if (arbyte.Length < lenByteCount)
                {
                    var lsttmp = new List<byte>();
                    lsttmp.AddRange(arbyte);
                    lsttmp.Add(32);
                    retStr = Encoding.GetEncoding("GB2312").GetString(lsttmp.ToArray());
                }
                lstStr.Add(retStr);
            }

            return lstStr.ToArray();
        }
    }
}
