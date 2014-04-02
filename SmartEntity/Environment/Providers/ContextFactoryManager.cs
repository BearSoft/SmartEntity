using System;
using System.Collections.Generic;
using HeptaSoft.SmartEntity.DataAccess;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal class ContextFactoryManager : IContextFactoryProvider, IContextFactoryContainer
    {
        private readonly Dictionary<Type, Func<IDataContext>> contextFactories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextFactoryManager"/> class.
        /// </summary>
        public ContextFactoryManager()
        {
            contextFactories = new Dictionary<Type, Func<IDataContext>>();
        }

        /// <summary>
        /// Adds the context factory and associates it to the provided entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="contextFactory">The context factory.</param>
        public void RegisterContextFactory(Type entityType, Func<IDataContext> contextFactory)
        {
            contextFactories.Add(entityType, contextFactory);
        }

        /// <summary>
        /// Gets the context factory associated with the specified entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The data context factory.</returns>
        public Func<IDataContext> GetContextFactory(Type entityType)
        {
            if (contextFactories.ContainsKey(entityType))
            {
                return contextFactories[entityType];
            }
            else
            {
                return null;
            }
        }
    }
}
