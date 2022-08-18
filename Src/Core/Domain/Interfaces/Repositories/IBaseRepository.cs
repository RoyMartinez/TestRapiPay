using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Edit(T entity);
    }
}
