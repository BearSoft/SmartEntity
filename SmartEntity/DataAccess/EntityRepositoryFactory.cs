using System;

namespace SmartEntity.DataAccess
{
    internal class EntityRepositoryFactory<TEntityData> : IEntityRepositoryFactory<TEntityData> 
        where TEntityData : class 
    {
        /// <summary>
        /// Creates an entity repository.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        /// <returns>A new instance of repository for the entity.</returns>
        public IEntityRepository<TEntityData> Create(Func<IDataContext> contextFactory)
        {
            var dbContextFactory = contextFactory as Func<IDbContext>;

            if (dbContextFactory != null)
            {
                return new EntityFrameworkRepository<TEntityData>(dbContextFactory);
            }
            else
            {
                throw new InvalidOperationException("Can't create yet a repository for Data Context different than EF IDbContext. Sorry!");
            }
        }
    }
}
