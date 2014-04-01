using System;
using SmartEntity.DataAccess;

namespace SmartEntity.Environment.Providers
{
    internal interface IContextFactoryContainer
    {
        /// <summary>
        /// Adds the context factory and associates it to the provided entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="contextFactory">The context factory.</param>
        void RegisterContextFactory(Type entityType, Func<IDataContext> contextFactory);
    }
}