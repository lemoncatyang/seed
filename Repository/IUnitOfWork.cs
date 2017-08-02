using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    /// <typeparam name="TContext">
    /// </typeparam>
    public interface IUnitOfWork<out TContext> : IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// Gets the db context.
        /// </summary>
        TContext DbContext { get; }

        /// <summary>
        /// The execute sql command.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// The from sql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters)
            where TEntity : class;

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <param name="ensureAutoHistory">
        /// The ensure auto history.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SaveChanges(bool ensureAutoHistory = false);

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <param name="ensureAutoHistory">
        /// The ensure auto history.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);
    }
}