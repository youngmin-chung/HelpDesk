using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HelpDeskDAL
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        List<T> GetByExpression(Expression<Func<T, bool>> lamdaExp);
        T Add(T entity);
        UpdateStatus Update(T enity);
        int Delete(int i);
    }
}
