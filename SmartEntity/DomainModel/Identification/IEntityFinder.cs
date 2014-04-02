using System;
using HeptaSoft.SmartEntity.DomainModel.Mapping;

namespace HeptaSoft.SmartEntity.DomainModel.Identification
{
    internal interface IEntityFinder
    {
        object FindByDto(Type entityType, object dtoInstance);

        /// <summary>
        /// Finds the by dto, considering the target entity properties having the prefix specified by <paramref name="offsetPath"/>.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="dtoInstance">The dto instance.</param>
        /// <param name="offsetPath">The offset path.</param>
        /// <returns></returns>
        object FindByDto(Type entityType, object dtoInstance, PropertyPath offsetPath);
    }
}