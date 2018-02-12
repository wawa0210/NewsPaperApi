using System;
using System.Text;
using MagickNetService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TypeSetting()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            var strContent = @" ��������Ƿ�˿�����ǻ����Ʊ�������Ӹ�����������������798��խ��ͨ·����������˹����ֱ�ƹ�һ��������Ʒ��ֱ���ⶰ�����ݶ�����Senate Finance Committee by a 4-3 vote on Jan. 24. On Feb.��������Ƿ�˿�����ǻ����Ʊ�������Ӹ�����������������798��խ��ͨ·����������˹����ֱ�ƹ�һ��������Ʒ��ֱ���ⶰ�����ݶ����� 8, the Senate passed the bill by a 16-13 margin, with one no-vote.The bill has now been sent to Arizona��s House of Representatives.If the bill is adopted, Arizona would become the first state in the U.S. to accept cryptocurrency tax payments by the year of 2020, as stated on the public record. The bill would allow taxpayers of the state to use ��a payment gateway, such as Bitcoin, Litecoin or any other cryptocurrency recognized by the department, using electronic peer-to-peer systems.��According to the bill, the Arizona Department of Revenue, upon receiving payments in crypto for ��tax and any interest and penalties��, would be obligated to convert the cryptocurrency payments to U.S. dollars within 24 hours.";
            var strTitle = @"Arizona Senate Passes Bill To Allow Tax Payments In Bitcoin";
            //var strArray = RecursiveGetStrByte(strContent);
            ////foreach (var item in strArray)
            ////{
            ////    Console.WriteLine(Encoding.GetEncoding("GB2312").GetString(item));
            ////};

            //var listJoinArray = GetStrBytes(strArray,40);
            //var listStr = GetStrArray(listJoinArray);
            var listStr = new TypeSettingHelper().GetTypeSettingArray(strContent, 40);
            //var listTitleStr = new TypeSettingHelper().GetTypeSettingArray(strTitle, 28);

            foreach (var itemArray in listStr)
            {
                Console.WriteLine(itemArray.Length);
                Console.WriteLine(itemArray);
            }

            //foreach (var itemArray in listTitleStr)
            //{
            //    Console.WriteLine(itemArray.Length);
            //    Console.WriteLine(itemArray);
            //}

            //var strContent = "��";
            //var strByteArray = Encoding.GetEncoding("GB2312").GetBytes(strContent);

            //foreach (var itemArray in strByteArray)
            //{
            //    Console.WriteLine(itemArray);
            //}
            Console.ReadKey();
        }
    }
}
