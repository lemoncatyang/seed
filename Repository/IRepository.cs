using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
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
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true);

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
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// The find async.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TEntity> FindAsync(params object[] keyValues);

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
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Insert(TEntity entity);

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Insert(IEnumerable<TEntity> entities);

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
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task InsertAsync(params TEntity[] entities);

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
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Update(TEntity entity);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        void Delete(object id);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Delete(TEntity entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<TEntity> GetAll();
    }
}
