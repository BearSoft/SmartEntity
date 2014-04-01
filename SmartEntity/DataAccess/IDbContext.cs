using System;
using System.Data.Entity;

namespace SmartEntity.DataAccess
{
    /// <summary>
    /// A facade interface for the DbContext (for mock-ability).
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Returns a DbSet instance for access to entities of the given type in the context, the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        
        /// <summary>
        /// Returns a DbSet instance for access to entities of the given type in the context, the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <returns>A set for the given entity type.</returns>
        DbSet Set(Type entityType);

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        int SaveChanges();
    }
}
