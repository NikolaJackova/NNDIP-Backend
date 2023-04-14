using System.Linq.Expressions;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(long id);
        Task<T> GetByIdAsync(long id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
