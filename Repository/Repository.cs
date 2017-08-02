using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The _db context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// The _db set.
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo.Name).FindPrimaryKey().Properties.FirstOrDefault();
            if (key == null)
            {
                return;
            }

            var property = typeInfo.GetProperty(key.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// The from sql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSql(sql, parameters);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Insert(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task InsertAsync(params TEntity[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task InsertAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="disableTracking">
        /// The disable tracking.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true)
        {
            var set = disableTracking ? _dbSet.AsNoTracking() : _dbSet;

            if (predicate != null)
            {
                set = set.Where(predicate);
            }

            return set;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}