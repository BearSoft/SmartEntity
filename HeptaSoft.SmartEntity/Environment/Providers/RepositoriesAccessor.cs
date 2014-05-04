using HeptaSoft.Common.DataAccess;
using System;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.Environment.Providers
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

        #region IRepositoryAccessorConfigurator

        /// <inheritdoc />
        public void RegisterRepository<TEntityData>(IEntityRepository<TEntityData> repository) where TEntityData : class
        {
            this.RegisterCreateDelegate(typeof(TEntityData), repository.CreateAndAdd);
            this.RegisterRemoveDelegate(typeof(TEntityData), repository.Delete);
        }

        #endregion

        #region IRepositoryAccessor

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        #endregion

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
