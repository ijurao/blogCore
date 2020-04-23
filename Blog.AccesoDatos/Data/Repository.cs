using Blog.AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {

        internal readonly DbContext _dbContext;
        internal  DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T Entity)
        {
            this.dbSet.Add(Entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            // properties separated por ,

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }

            }

            if(orderBy != null)
            {
               return  orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }

            }

            return query.FirstOrDefault();
        }
    

        public void Remove(int id)
        {
            T entityToRemevo = dbSet.Find(id);
            Remove(entityToRemevo);
        }

        public void Remove(T Entity)
        {
            dbSet.Remove(Entity);
        }
    }
}
