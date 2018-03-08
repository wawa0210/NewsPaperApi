using System.IO;
using Microsoft.Extensions.Configuration;

namespace CommonLib.Configuration
{
    public class ConfigurationHelper
    {
        private static IConfigurationRoot ConfigurationRoot { get; set; }
        //static ConfigurationHelper()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        //    configurationRoot = builder.Build();
        //}

        public static IConfigurationRoot GetInstance()
        {
            if (ConfigurationRoot != null) return ConfigurationRoot;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
