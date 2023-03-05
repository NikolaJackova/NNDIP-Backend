using Microsoft.EntityFrameworkCore;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using System.Linq.Expressions;

namespace NNDIP.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly NndipDbContext _context;
        public GenericRepository(NndipDbContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual async void AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual T GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
