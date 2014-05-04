using HeptaSoft.Common.DataAccess;
using System;
using System.Collections.Generic;

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

        #region IContextFactoryContainer

        /// <inheritdoc />
        public void RegisterContextFactory(Type entityType, Func<IDataContext> contextFactory)
        {
            contextFactories.Add(entityType, contextFactory);
        }

        #endregion

        #region IContextFactoryProvider

        /// <inheritdoc />
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

        #endregion
    }
}
