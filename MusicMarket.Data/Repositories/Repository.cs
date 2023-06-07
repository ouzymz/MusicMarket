using Microsoft.EntityFrameworkCore;
using MusicMarket.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MusicMarketDbContext context;
        public Repository(MusicMarketDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddAsync(T item)
        {
            await context.Set<T>().AddAsync(item);
        }

        public async Task AddrangeAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().SingleOrDefaultAsync(filter);
        }
    }
}
