using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync ();
        IEnumerable<T> Find (Expression<Func<T,bool>> filter);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T item);
        Task AddrangeAsync(IEnumerable<T> entities);
        void remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
