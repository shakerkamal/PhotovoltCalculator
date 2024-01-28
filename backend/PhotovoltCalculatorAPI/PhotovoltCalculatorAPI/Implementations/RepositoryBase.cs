using Microsoft.EntityFrameworkCore;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using System.Linq.Expressions;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, ISoftDelete
    {
        protected readonly DataContext _context;
        public RepositoryBase(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll() =>
                       _context.Set<T>()
                       .AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
                         _context.Set<T>()
                         .Where(expression)
                         .AsNoTracking();

        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void CreateRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);
        public void Update(T entity) => _context.Set<T>().Update(entity).State = EntityState.Modified;
        public void Delete(T entity)
        {
            entity.Deleted = true;
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
