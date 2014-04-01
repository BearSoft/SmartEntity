using System;

namespace SmartEntity.Environment.Providers
{
    internal interface IRepositoryAccessor
    {
        /// <summary>
        /// Creates a new instance of entity data.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">No factory registered for the specified entity type.</exception>
        object CreateEntityData(Type entityType);

        /// <summary>
        /// Removes the entity data.
        /// </summary>
        /// <param name="entityDataInstance">The entity data instance.</param>
        void RemoveEntityData(object entityDataInstance);
    }
}