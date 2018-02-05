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
            var imgUrl = "Imgs/" + GuidExtens.GuidTo16String() + ".jpg";
            var wordsArray = SplitByLen(entityNewsModel.Content, 20);
            var titleArray = SplitByLen(entityNewsModel.Title, 14);

            var titleHeight = titleArray.Length * 100 + 100;
            var contentHeight = wordsArray.Length * 80 + 80;
            var microsoftYaheiUi = "SimHei";
            var picHeight = titleHeight + contentHeight + 50;
            var lineColor = new MagickColor("gray");

            using (var image = new MagickImage(new MagickColor("#f8f8f6"), 1080, picHeight + 550))
            {
                using (var mainImgImage = new MagickImage(new MagickColor("#f8f8f6"), 900, picHeight))
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
                            .Text(30, 80 + 90 * i, titleArray[i])
                            .Draw(mainImgImage);

                    }
                    //头部间隔线
                    new Drawables().StrokeWidth(0.5).StrokeColor(lineColor).Line(50, titleHeight, 850, titleHeight).Draw(mainImgImage);

                    for (var i = 0; i < wordsArray.Length; i++)
                    {
                        new Drawables()
                            .TextEncoding(Encoding.UTF8)
                            .TextAntialias(true)
                            .FontPointSize(40)
                            .FillColor(new MagickColor("#4D4D4D"))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(30, titleHeight + 70 + 80 * i, wordsArray[i])
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
                using (var watermark = new MagickImage(client.DownloadData("http://img.xiaozhang.info/wechatCode.jpg")))
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
    }
}
