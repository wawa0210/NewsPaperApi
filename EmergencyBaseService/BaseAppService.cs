using EmergencyData.MicroOrm;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using EmergencyData.MicroOrm.SqlGenerator;

namespace EmergencyBaseService
{
    /// <summary>
    /// AppService基类
    /// </summary>
    public class BaseAppService
    {

        private IDbConnection _connection;
        public BaseAppService()
        {

        }

        /// <summary>
        /// 根据类型获取Repository对象
        /// </summary>
        /// <typeparam name="T">Repository对象类型</typeparam>
        /// <returns></returns>
        protected virtual DapperRepository<T> GetRepositoryInstance<T>(string connStr = null) where T : class, new()
        {
            if (_connection == null)
            {
                _connection = DbContextFactory.CreateDbConnection(ESqlConnector.MySql, connStr);
            }
            var repository = new DapperRepository<T>(_connection, ESqlConnector.MySql);
            return repository;
        }


    }
}
