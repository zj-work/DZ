using System.Data;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace DZ.Data
{
    /// <summary>
    /// DataBase Operations base on SqlSugar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContext<T> : DbBase where T : class, new()
    {
        private SqlSugarClient Db;
        private SimpleClient<T> currentDb;
        public DbContext(DbConfig dbConfig = DbConfig.DbMain)
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = GetDbConnectionString(dbConfig),
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true
            });
            currentDb = new SimpleClient<T>(Db);
        }

        /// <summary>
        /// Add entity and inserted.id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            return currentDb.InsertReturnIdentity(entity);
        }

        /// <summary>
        /// AddRange entity and return success or not
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool AddRange(IEnumerable<T> entities)
        {
            return currentDb.InsertRange(entities.ToArray());
        }

        /// <summary>
        /// Update data and return success or not
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            return currentDb.Update(entity);
        }

        /// <summary>
        /// Delete data and return success ot not
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            return currentDb.Delete(entity);
        }

        /// <summary>
        /// delete data by id and return success or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(dynamic id)
        {
            return currentDb.DeleteById(id);
        }

        /// <summary>
        /// delete range data by id and return success or not
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteRange(IEnumerable<dynamic> ids)
        {
            return currentDb.DeleteByIds(ids.ToArray());
        }
        
        /// <summary>
        /// Query all data
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return currentDb.GetList();
        }

        /// <summary>
        /// Query data by condition
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T,bool>> whereExpression)
        {
            return currentDb.GetList(whereExpression);
        }

        /// <summary>
        /// Query Pagination data by condition
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> GetListByPage(Expression<Func<T,bool>> whereExpression,int pageIndex,int pageSize)
        {
            return currentDb.GetPageList(whereExpression, new PageModel()
            {
                PageIndex=pageIndex,
                PageSize = pageSize
            });
        }

        /// <summary>
        /// Query pagination data by condition and orderation 
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="IsAsc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> GetListByPage(Expression<Func<T,bool>> whereExpression,Expression<Func<T,object>> orderExpression,bool IsAsc = false, int pageIndex=1,int pageSize = 20)
        {
            return currentDb.GetPageList(whereExpression, new PageModel() { PageIndex = pageIndex, PageSize = pageSize }, orderExpression, IsAsc ? OrderByType.Asc : OrderByType.Desc);
        }

        /// <summary>
        /// Query Single data by condition
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T,bool>> whereExpression)
        {
            return currentDb.GetSingle(whereExpression);
        }

        /// <summary>
        /// Query count of data by condition
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T,bool>> whereExpression)
        {
            return currentDb.Count(whereExpression);
        }
    }
}
