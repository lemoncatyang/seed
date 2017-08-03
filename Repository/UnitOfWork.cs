using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The unit of work.
    /// </summary>
    /// <typeparam name="TContext">
    /// </typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// The _disposed.
        /// </summary>
        private bool _disposed;

        //// public ITestModelRepository TestModelRepository { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            _disposed = false;

            // TestModelRepository = new TestModelRepository(DbContext);
        }

        /// <summary>
        /// Gets the db context.
        /// </summary>
        public TContext DbContext { get; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(sql, parameters);
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
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters)
            where TEntity : class
        {
            return DbContext.Set<TEntity>().FromSql(sql, parameters);
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <param name="ensureAutoHistory">
        /// The ensure auto history.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                DbContext.EnsureAutoHistory();
            }

            return DbContext.SaveChanges();
        }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <param name="ensureAutoHistory">
        /// The ensure auto history.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                DbContext.EnsureAutoHistory();
            }

            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                   DbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}