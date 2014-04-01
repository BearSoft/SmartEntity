
namespace SmartEntity.DataAccess
{
    public interface IDataContext : IDbContext
    {
        /////// <summary>
        /////// The <see cref="IDataSet{T}"/> instance for accessing the store of entities of the specified type.
        /////// </summary>
        /////// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /////// <returns>A set for the given entity type.</returns>
        ////IDataSet<TEntity> DataSet<TEntity>() where TEntity : class;

        /////// <summary>
        /////// The <see cref="IDataSet" /> instance for accessing the store of entities of the specified type.
        /////// </summary>
        /////// <param name="entityType">Type of the entity.</param>
        /////// <returns>
        /////// A set for the given entity type.
        /////// </returns>
        ////IDataSet DataSet(Type entityType);

        /////// <summary>
        /////// Saves all changes made in this context to the underlying database.
        /////// </summary>
        /////// <returns>The number of objects written to the underlying database.</returns>
        /////// <exception cref="System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        ////int SaveChanges();
    }
}
