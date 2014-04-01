using System;
using SmartEntity.DataAccess;

namespace SmartEntity.Environment.Providers
{
    internal interface IContextFactoryProvider
    {
        /// <summary>
        /// Gets the context factory associated with the specified entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The data context factory.</returns>
        Func<IDataContext> GetContextFactory(Type entityType);
    }
}