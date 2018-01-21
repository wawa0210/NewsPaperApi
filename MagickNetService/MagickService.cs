using CommonLib;
using ImageMagick;
using MagickNetService.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagickNetService
{
    public class MagickService
    {
        public string GenerateNewImg(EntityNewsModel entityNewsModel)
        {
            var imgUrl = GuidExtens.GuidTo16String() + ".jpg";
            var wordsArray = SplitByLen(entityNewsModel.Content, 23);
            var titleArray = SplitByLen(entityNewsModel.Title, 14);

            var titleHeight = titleArray.Length * 40 + 10;
            var contentHeight = wordsArray.Length * 40 + 40;
            var microsoftYaheiUi = "SimHei";

            using (var mainImgImage = new MagickImage(new MagickColor("#f8f8f6"), 500, titleHeight + contentHeight))
            {
                using (var image = new MagickImage(new MagickColor("#f8f8f6"), 500, titleHeight))
                {
                    for (var i = 0; i < titleArray.Length; i++)
                    {
                        new Drawables()
                            .TextEncoding(Encoding.UTF8)
                            .TextAntialias(true)
                            .FontPointSize(30)
                            .FillColor(new MagickColor(255, 0, 0))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(30, 20 + 40 * i, titleArray[i])
                            .Draw(image);
                    }
                    mainImgImage.Composite(image, 0, 0, CompositeOperator.Over);
                }

                using (var image = new MagickImage(new MagickColor("#f8f8f6"), 500, contentHeight))
                {
                    for (var i = 0; i < wordsArray.Length; i++)
                    {
                        new Drawables()
                            .TextEncoding(Encoding.UTF8)
                            .TextAntialias(true)
                            .FontPointSize(20)
                            .FillColor(new MagickColor(255, 0, 0))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(30, 20 + 40 * i, wordsArray[i])
                            .Draw(image);
                    }

                    mainImgImage.Composite(image, 0, titleHeight + 20, CompositeOperator.Over);
                }
                mainImgImage.Write("Imgs/" + imgUrl);
            }

            return imgUrl;
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
