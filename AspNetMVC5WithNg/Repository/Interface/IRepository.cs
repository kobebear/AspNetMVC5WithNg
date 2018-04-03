using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMVC5WithNg.Repository.Interface
{
    interface IRepository<T> : IDisposable where T : class
    {
        //查询所有记录
        //可遍历的                               //类似于ES6的rest
        IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters);
        //查询单个记录
        T ExecuteQuerySingle(string spQuery, object[] parameters);
        //执行其他增删改操作
        int ExecuteCommand(string spQuery, object[] parameters);
    }
}
