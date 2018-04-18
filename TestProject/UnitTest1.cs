using CommonLib;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task DoGetTest()
        {
            var resuestUrl = "https://www.baidu.com";

            var result = await resuestUrl.DoGetAsync(null);

            var count = 0;

        }
    }
}
