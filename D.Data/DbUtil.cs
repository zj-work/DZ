using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Z.Dapper.Plus;

namespace D.Data
{
    /// <summary>
    /// SQL utils base on Dapper
    /// </summary>
    public class DbUtil : DbBase
    {
        private IDbConnection Connection = null;
        public DbUtil(string connName)
        {
            Connection = GetDbConnection(connName);
        }

        /// <summary>
        /// Execute sql with parameters and return affect rows count
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, object para = null)
        {
            var affectedRows = Connection.Execute(sql, para, commandType: CommandType.Text);
            return affectedRows;
        }

        /// <summary>
        /// Query and return entity's collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object para = null)
        {
            var res = Connection.Query<T>(sql, para, commandType: CommandType.Text);
            return res;
        }

        /// <summary>
        /// Query and return data(int) which on datatable's first row and first column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public int ExecuteScalar(string sql, object para = null)
        {
            return Connection.ExecuteScalar<int>(sql, para, commandType: CommandType.Text);
        }

        /// <summary>
        /// Query and return data(String) which on datatable's first row and first column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public string ExecuteScalarString(string sql, object para = null)
        {
            return Connection.ExecuteScalar<string>(sql, para, commandType: CommandType.Text);
        }

        /// <summary>
        /// Execute Stored Procedure with parameters and return affect rows count
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public int ExecuteSPNonQuery(string spName, object para = null)
        {
            return Connection.Execute(spName, para, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Query Stored Procedure  and return entity's collection 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public IEnumerable<T> QuerySP<T>(string spName, object para = null)
        {
            return Connection.Query<T>(spName, para, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Query Stored Procedure and return data(int) which on datatable's first row and first column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public int ExecuteSPScalarInt(string spName, object para = null)
        {
            return Connection.ExecuteScalar<int>(spName, para, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Query Stored Procedure and return data(String) which on datatable's first row and first column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public string ExecuteSPScalarString(string spName, object para = null)
        {
            return Connection.ExecuteScalar<string>(spName, para, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 插入数据 并返回自动增长的标示性信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert<T>(T entity) where T : class
        {
            Connection.Open();
            var identity = Connection.Insert<T>(entity);
            Connection.Close();
            return (Int32)identity;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update<T>(T entity) where T : class
        {
            Connection.Open();
            var res = Connection.Update<T>(entity);
            Connection.Close();
            return res;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete<T>(T entity) where T : class
        {
            Connection.Open();
            var res = Connection.Delete<T>(entity);
            Connection.Close();
            return res;
        }

        /// <summary>
        /// 批量插入  待测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int InsertRange<T>(IEnumerable<T> entities)
        {
            var res = Connection.BulkInsert<T>(entities.ToArray());
            return res.Current.Count();
        }

        /// <summary>
        /// 批量删除 待测试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int DeleteRange<T>(IEnumerable<T> entities)
        {
            var res = Connection.BulkDelete<T>(entities);
            return res.Current.Count();
        }

    }
}
