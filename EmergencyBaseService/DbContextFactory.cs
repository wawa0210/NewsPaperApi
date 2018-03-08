using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using CommonLib.Configuration;
using EmergencyData.MicroOrm.SqlGenerator;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace EmergencyBaseService
{
    public class DbContextFactory
    {
        public static IDbConnection CreateDbConnection(ESqlConnector enumDataBase, string connectionStr = "")
        {
            if (string.IsNullOrWhiteSpace(connectionStr))
                connectionStr = GetConnectionStr(enumDataBase);
            switch (enumDataBase)
            {
                case ESqlConnector.MySql:
                    return new MySqlConnection(connectionStr);
                case ESqlConnector.Mssql:
                    return new SqlConnection(connectionStr);
                default:
                    throw new Exception("没用找的IDbConnection的实例类型");
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionStr(ESqlConnector enumDataBase)
        {
            var conntectStr = "";
            switch (enumDataBase)
            {
                case ESqlConnector.MySql:
                    conntectStr = ConfigurationHelper.GetInstance().GetSection("db").GetSection("mysql").GetSection("connectionStr").Value;
                    break;
                case ESqlConnector.Mssql:
                    conntectStr = ConfigurationHelper.GetInstance().GetSection("db").GetSection("sqlserver").GetSection("connectionStr").Value;
                    break;
                case ESqlConnector.PostgreSql:
                    break;
                default:
                    throw new Exception("没用找的IDbConnection的实例类型");
            }
            return conntectStr;
        }
    }
}
