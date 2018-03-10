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

            //内容
            var wordsArray = typeSettingHelper.GetTypeSettingArray(entityNewsModel.Content, 36);
            var titleArray = typeSettingHelper.GetTypeSettingArray(entityNewsModel.Title, 28);

            //主图左边距
            var mainImgMarginLeft = 93;

            //标题距离顶部间距
            var titleTopMrgin = 63;

            //标题字体高度
            var titleFontHeight = 50;

            //标题字体上下间距
            var titleFontPadding = 40;

            //标题距离横线间距
            var titleSpaceLine = 88;

            //内容填充边距
            var contentPadding = 62;

            //主图上边距
            var mainImgMarginTop = 140;

            //主图宽度
            var mainImgWidth = 894;

            //内容距离横线上边距
            var contentTopMrgin = 90;

            //内容字体上下间距
            var contentFontPadding = 49;

            //内容字体高度
            var contentFontHeight = 40;

            //内容距离下边距
            var contentBottomMargin = 102;

            //底部高度，包含二维码
            var bottomHeight = 321;

            //二维码上边距
            var wechatCodeTopMargin = 71;

            //二维码左边距
            var wechatCodeLeftMargin = 337;

            //微信二维码图片高度
            var wechatCodeImgHeight = 168;

            //标题图片总高度
            var titleHeight = titleTopMrgin + titleSpaceLine + titleFontHeight * titleArray.Length + (titleArray.Length - 1) * titleFontPadding;

            //新闻内容模块总高度
            var contentHeight = contentTopMrgin + contentBottomMargin + wordsArray.Length * contentFontHeight + (wordsArray.Length - 1) * contentFontPadding;

            var microsoftYaheiUi = "SimHei";
            var picHeight = titleHeight + contentHeight;
            var lineColor = new MagickColor("gray");

            using (var image = new MagickImage(new MagickColor("#F9F8F6"), 1080, picHeight + bottomHeight + mainImgMarginTop))
            {
                using (var mainImgImage = new MagickImage(new MagickColor("#FFFFFF"), mainImgWidth, picHeight))
                {
                    //title
                    for (var i = 0; i < titleArray.Length; i++)
                    {
                        new Drawables()
                            .TextEncoding(Encoding.UTF8)
                            .TextAntialias(true)
                            .FontPointSize(52)
                            .FillColor(new MagickColor(126, 99, 43))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(contentPadding, titleTopMrgin + ((titleFontHeight + titleFontPadding) * i), titleArray[i])
                            .Draw(mainImgImage);
                    }

                    for (var i = 2; i < 81; i++)
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
                            .FontPointSize(43)
                            .FillColor(new MagickColor(102, 102, 102))
                            .Gravity(Gravity.Northwest)
                            .Font(microsoftYaheiUi)
                            .Text(contentPadding, titleHeight + contentTopMrgin + (contentFontPadding + contentFontHeight) * i, wordsArray[i])
                            .Draw(mainImgImage);
                    }
                    //尾部
                    //new Drawables().StrokeWidth(0.5).StrokeColor(lineColor).Line(20, picHeight - 180, 880, picHeight - 180).Draw(mainImgImage);
                    mainImgImage.Border(1);
                    mainImgImage.BorderColor = new MagickColor("#4D4D4D");
                    image.Composite(mainImgImage, mainImgMarginLeft, mainImgMarginTop, CompositeOperator.Over);
                }

                //小程序二维码 
                var client = new WebClient();
                using (var watermark = new MagickImage(client.DownloadData("http://img.blockcomet.com/wechatCode.png")))
                {
                    var size = new MagickGeometry(wechatCodeImgHeight, wechatCodeImgHeight) { IgnoreAspectRatio = true };
                    watermark.Resize(size);
                    watermark.Evaluate(Channels.Black, EvaluateOperator.Divide, 4);

                    image.Composite(watermark, wechatCodeLeftMargin, picHeight + wechatCodeTopMargin + mainImgMarginTop, CompositeOperator.Over);
                }

                new Drawables()
                        .TextEncoding(Encoding.UTF8)
                        .TextAntialias(true)
                        .FontPointSize(50)
                        .FillColor(new MagickColor(102, 102, 102))
                        .Gravity(Gravity.Northwest)
                        .Font(microsoftYaheiUi)
                        .Text(wechatCodeImgHeight + wechatCodeLeftMargin + 30, picHeight + mainImgMarginTop + 123, "彗星播报")
                        .Draw(image);

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
