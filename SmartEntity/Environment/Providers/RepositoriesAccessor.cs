using System;
using System.Collections.Generic;
using SmartEntity.DataAccess;

namespace SmartEntity.Environment.Providers
{
    internal class RepositoriesAccessor : IRepositoryAccessorConfigurator, IRepositoryAccessor
    {
        /// <summary>
        /// The entity data factories.
        /// </summary>
        private readonly Dictionary<Type, Delegate> createDelegates;

        /// <summary>
        /// The entity data factories.
        /// </summary>
        private readonly Dictionary<Type, Delegate> removeDelegates;


        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoriesAccessor"/> class.
        /// </summary>
        public RepositoriesAccessor()
        {
            createDelegates = new Dictionary<Type, Delegate>();
            removeDelegates = new Dictionary<Type, Delegate>();
        }

        /// <summary>
        /// Registers a repository and assigns it to the specified entity data type.
        /// </summary>
        /// <typeparam name="TEntityData">The type of the entity data.</typeparam>
        /// <param name="repository">The repository.</param>
        public void RegisterRepository<TEntityData>(IEntityRepository<TEntityData> repository) where TEntityData : class
        {
            this.RegisterCreateDelegate(typeof(TEntityData), repository.CreateAndAdd);
            this.RegisterRemoveDelegate(typeof(TEntityData), repository.Delete);
        }

        /// <summary>
        /// Creates a new instance of entity data.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">No factory registered for the specified entity type.</exception>
        public object CreateEntityData(Type entityType)
        {
            if (createDelegates.ContainsKey(entityType))
            {
                var createDelegate = createDelegates[entityType];

                return createDelegate.DynamicInvoke();
            }
            else
            {
                throw new InvalidOperationException(string.Format("Cannot create new entity data instance: no create delegate registered for entity type <{0}>.", entityType));
            }
        }

        /// <summary>
        /// Removes the entity data.
        /// </summary>
        /// <param name="entityDataInstance">The entity data instance.</param>
        public void RemoveEntityData(object entityDataInstance)
        {
            var entityType = entityDataInstance.GetType();
            if (removeDelegates.ContainsKey(entityType))
            {
                var removeDelegate = removeDelegates[entityType];

                removeDelegate.DynamicInvoke(entityDataInstance);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Cannot remove the entity data instance: no remove delegate registered for entity type <{0}>.", entityType));
            }
        }

        /// <summary>
        /// Registers the create delegate.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="createFunc">The create function.</param>
        private void RegisterCreateDelegate(Type entityType, Func<object> createFunc)
        {
            createDelegates.Add(entityType, createFunc);
        }

        /// <summary>
        /// Registers the remove delegate.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="removeFunc">The remove function.</param>
        private void RegisterRemoveDelegate(Type entityType, Action<object> removeFunc)
        {
            removeDelegates.Add(entityType, removeFunc);
        }
    }
}
