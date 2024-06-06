using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetAllQuerable();
        Task<T?> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> DeletePhysical(int id);
        Task<IEnumerable<T>> RemoveRange(IEnumerable<T> entities);
        Task<bool> Exist(Expression<Func<T, bool>> expression);

    }
}
