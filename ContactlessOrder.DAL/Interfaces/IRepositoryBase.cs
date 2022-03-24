using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface IRepositoryBase
    {
        Task SaveChanges();
        ValueTask<EntityEntry> Add<T>(T entity);

        Task AddRange<T>(IEnumerable<T> entities)
            where T : class;

        Task<IEnumerable<T>> GetAll<T>()
            where T : class;

        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task<IEnumerable<T>> GetAllAsTracking<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        ValueTask<T> Get<T>(int id)
            where T : class;

        Task<T> Get<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task Remove<T>(int id)
            where T : class;

        void Remove<T>(T entity)
            where T : class;

        void RemoveRange<T>(IEnumerable<T> entities)
            where T : class;
    }
}