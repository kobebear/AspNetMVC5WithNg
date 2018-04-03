using AspNetMVC5WithNg.Models;
using AspNetMVC5WithNg.Repository.Interface;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC5WithNg.Repository.BaseRepository
{
    public class GenericRepository<T>: IRepository<T> where T:class
    {
        CRUD_SampleEntities context = null;
        //private DbSet<T> entities = null;

        //构造函数: 初始化操作实体的环境对象

        public GenericRepository(CRUD_SampleEntities context)
        {
            this.context = context;
            //entities = context.Set<T>();
        }

        /// <summary>
        /// Get Data From Database
        /// <para>Use it when to retive data through a stored procedure</para>
        /// </summary>
        public IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters)
        {
            //每次都临时创建一个新的实体环境对象，用于执行数据库查询
            using (context = new CRUD_SampleEntities())
            {
                return context.Database.SqlQuery<T>(spQuery, parameters).ToList();
            }
        }

        /// <summary>
        /// Get Single Data From Database
        /// <para>Use it when to retive single data through a stored procedure</para>
        /// </summary>
        public T ExecuteQuerySingle(string spQuery, object[] parameters)
        {
            using (context = new CRUD_SampleEntities())
            {
                return context.Database.SqlQuery<T>(spQuery, parameters).FirstOrDefault();
            }
        }

        /// <summary>
        /// Insert/Update/Delete Data To Database
        /// <para>Use it when to Insert/Update/Delete data through a stored procedure</para>
        /// </summary>
        public int ExecuteCommand(string spQuery, object[] parameters)
        {
            int result = 0;
            try
            {
                using (context = new CRUD_SampleEntities())
                {
                    result = context.Database.SqlQuery<int>(spQuery, parameters).FirstOrDefault();
                }
            }
            catch { }
            return result;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}