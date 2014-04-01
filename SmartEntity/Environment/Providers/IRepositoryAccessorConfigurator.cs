using SmartEntity.DataAccess;

namespace SmartEntity.Environment.Providers
{
    internal interface IRepositoryAccessorConfigurator
    {
        /// <summary>
        /// Registers a repository and assigns it to the specified entity data type.
        /// </summary>
        /// <typeparam name="TEntityData">The type of the entity data.</typeparam>
        /// <param name="repository">The repository.</param>
        void RegisterRepository<TEntityData>(IEntityRepository<TEntityData> repository) where TEntityData : class;
    }
}