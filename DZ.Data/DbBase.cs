using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Data
{
    public class DbBase
    {
        /// <summary>
        /// 注册数据库连接
        /// </summary>
        /// <param name="dbProvideName"></param>
        protected IDbConnection GetDbConnection(DbConfig dbConfig)
        {
            var dbStringName = ConfigurationManager.AppSettings[dbConfig.ToString()];
            var dbString = ConfigurationManager.ConnectionStrings[dbStringName].ConnectionString;
            var _dbConnection = new SqlConnection(dbString);
            return _dbConnection;
        }
    }
}
