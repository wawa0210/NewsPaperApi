using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace EmergencyBaseService
{
    public class DbContextFactory
    {
        public static IDbConnection CreateDbConnection(EnumDataBase enumDataBase, string connectionStr = "")
        {
            if (string.IsNullOrWhiteSpace(connectionStr))
                connectionStr = GetConnectionStr(enumDataBase);
            switch (enumDataBase)
            {
                case EnumDataBase.MySql:
                    return new MySqlConnection(connectionStr);
                case EnumDataBase.SqlServer:
                    return new SqlConnection(connectionStr);
                default:
                    throw new Exception("没用找的IDbConnection的实例类型");
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionStr(EnumDataBase enumDataBase)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configRoot = builder.Build();
            var conntectStr = "";
            switch (enumDataBase)
            {
                case EnumDataBase.MySql:
                    conntectStr = configRoot.GetSection("db").GetSection("mysql").GetSection("connectionStr").Value;
                    break;
                case EnumDataBase.SqlServer:
                    conntectStr = configRoot.GetSection("db").GetSection("sqlserver").GetSection("connectionStr").Value;
                    break;
                default:
                    throw new Exception("没用找的IDbConnection的实例类型");
            }
            return conntectStr;
        }
    }
}
