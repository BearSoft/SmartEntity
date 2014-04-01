using System;
using SmartEntity.DomainModel.Identification;

namespace SmartEntity.Environment.Providers
{
    internal interface IFindersRegistrator
    {
        /// <summary>
        /// Adds the specified <see cref="IFinder" /> instance to the container and associates it to the specified type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="finder">The identification predicate.</param>
        void RegisterFinder(Type entityType, IFinder finder);
    }
}