using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
   public interface IRepository<T> where T : class
    {
       T  Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        T GetFirstOrDefault(
               Expression<Func<T, bool>> filter = null,
               string includeProperties = null
            );

        void Add(T Entity);
        void Remove(int id);
        void Remove(T Entity);

        // upodate change as Identity for this reazon ww donr implement it in generic repository
    }
}
