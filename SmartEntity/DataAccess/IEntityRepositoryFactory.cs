using System;

namespace SmartEntity.DataAccess
{
    internal interface IEntityRepositoryFactory<TEntityData> where TEntityData:class 
    {
        /// <summary>
        /// Creates an entity repository.
        /// </summary>
        /// <typeparam name="TEntityData">The type of the entity data.</typeparam>
        /// <param name="contextFactory">The context factory.</param>
        /// <returns>
        /// A new instance of repository for the entity.
        /// </returns>
        IEntityRepository<TEntityData> Create(Func<IDataContext> contextFactory);
    }
}