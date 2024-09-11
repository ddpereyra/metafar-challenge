using System.Linq.Expressions;

namespace metafar_challenge.Repositories.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<T> GetById(int id);
            Task<IEnumerable<T>> GetAll();
            Task Add(T entity);
            Task Update(T entity);
            Task Delete(T entity);
            Task<bool> Exists(Expression<Func<T, bool>> predicate);
        }
    }
}
