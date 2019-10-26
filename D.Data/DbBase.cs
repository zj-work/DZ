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
    public abstract class DbBase
    {
        /// <summary>
        /// 注册数据库连接
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        protected IDbConnection GetDbConnection(string connName)
        {
            var connString = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            return new SqlConnection(connString);
        }

        /// <summary>
        /// 注册数据库连接
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        protected string GetDbConnectionString(string connName)
        {
            return ConfigurationManager.ConnectionStrings[connName].ConnectionString;
        }
    }
}
