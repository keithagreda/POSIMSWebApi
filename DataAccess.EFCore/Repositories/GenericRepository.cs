using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<int> InsertAndGetIdAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Add(entity);

            await _context.SaveChangesAsync();

            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
            {
                throw new InvalidOperationException($"The entity type {typeof(T).Name} does not have a property named 'Id'.");
            }

            return (int)idProperty.GetValue(entity);
        }

        public async Task<string> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                return "Success!";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public async Task<T> FirstOrDefaultAsync(int Id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == Id);
        }

        public async Task<T> FirstOrDefaultAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T FirstOrDefault(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        public T FirstOrDefault(Guid id)
        {
            return _context.Set<T>().FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public async Task<IQueryable<T>> FindAsyncQueryable(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetByGuid(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
