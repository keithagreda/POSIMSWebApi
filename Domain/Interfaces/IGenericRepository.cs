using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        T GetByGuid(Guid id);
        Task<IList<T>> GetAllAsync();
        IQueryable<T> GetQueryable();
        Task<T> GetByGuidAsync(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> FindAsyncQueryable(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(int id);
        Task<T> FirstOrDefaultAsync(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(int id);
        T FirstOrDefault(Guid id);

        void Add(T entity);
        Task<int> InsertAndGetIdAsync(T entity);
        void AddRange(IEnumerable<T> entities);

        Task<string> AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
