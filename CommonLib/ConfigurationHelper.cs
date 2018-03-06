using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLib
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot configurationRoot { get; set; }
        //static ConfigurationHelper()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        //    configurationRoot = builder.Build();
        //}

        public static IConfigurationRoot GetInstance()
        {
            if (configurationRoot != null) return configurationRoot;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
