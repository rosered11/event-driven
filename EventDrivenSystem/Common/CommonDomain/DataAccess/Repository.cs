using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Rosered11.OrderService.Common.DataAccess
{
    public abstract class Repository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void DeleteAllInBatch(Expression<Func<TEntity, bool>> predicate)
        {
            _dbSet.RemoveRange(_dbSet.Where(predicate));
        }
        public TEntity? Find(params object?[]? keyValues)
        {
            return _dbSet.Find(keyValues);
        }
        public void Flush()
        {
            foreach(var entity in _dbSet)
                _context.Entry(entity).State = EntityState.Detached;
        }

        public TEntity Save(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }
        public IEnumerable<TEntity> SaveAll(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }
    }
}