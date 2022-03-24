using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ContactlessOrder.DAL.Repositories
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        protected RepositoryBase(ContactlessOrderContext context)
        {
            Context = context;
        }

        protected ContactlessOrderContext Context { get; }

        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        public ValueTask<EntityEntry> Add<T>(T entity)
        {
            return Context.AddAsync(entity);
        }

        public Task AddRange<T>(IEnumerable<T> entities)
            where T : class
        {
            return Context.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
            where T : class
        {
            return await Context.Set<T>().AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return await Context.Set<T>().AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsTracking<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return await Context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public ValueTask<T> Get<T>(int id)
            where T : class
        {
            return Context.Set<T>().FindAsync(id);
        }

        public Task<T> Get<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task Remove<T>(int id)
            where T : class
        {
            var entity = await Get<T>(id);

            Remove(entity);
        }

        public void Remove<T>(T entity)
            where T : class
        {
            Context.Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entities)
            where T : class
        {
            Context.RemoveRange(entities);
        }
    }
}