using System.Data;

namespace DZ.Data
{
    /// <summary>
    /// DataBase Operations base on SqlSugar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContext<T> : DbBase where T : class, new()
    {
        private IDbConnection Connection = null;
        public DbContext(DbConfig dbConfig = DbConfig.DbMain)
        {
            Connection = GetDbConnection(dbConfig);
        }


    }
}
