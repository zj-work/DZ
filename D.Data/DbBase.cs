using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Data
{
    public class DbBase
    {
        /// <summary>
        /// 注册数据库连接
        /// </summary>
        /// <param name="dbProvideName"></param>
        protected IDbConnection GetDbConnection(DbConfig dbConfig)
        {
            var _dbConnection = new SqlConnection(GetDbConnectionString(dbConfig));
            return _dbConnection;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dbConfig"></param>
        /// <returns></returns>
        protected string GetDbConnectionString(DbConfig dbConfig)
        {
            var dbStringName = ConfigurationManager.AppSettings[dbConfig.ToString()];
            var dbString = ConfigurationManager.ConnectionStrings[dbStringName].ConnectionString;
            return dbString;
        }
    }
}
