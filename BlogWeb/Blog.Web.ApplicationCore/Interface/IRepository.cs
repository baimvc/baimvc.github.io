using Blog.Web.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Web.ApplicationCore.Interface
{
    public interface IRepository<T> where T:BaseEntity
    {
        /// <summary>
        /// 根据Id查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(int id);
        /// <summary>
        /// 获取整个信息列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> List();

        /// <summary>
        /// 根据条件获取整个信息列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> List(Expression<Func<T,bool>> predicate);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Add(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(T entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Edit(T entity);
    }
}
