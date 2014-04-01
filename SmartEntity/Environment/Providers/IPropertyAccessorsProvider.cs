using System;
using System.Collections.Generic;
using SmartEntity.DomainModel.Mapping.Accessors;

namespace SmartEntity.Environment.Providers
{
    internal interface IPropertyAccessorsProvider
    {
        /// <summary>
        /// Gets the accessors for all the public properties of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The list of property accessors.</returns>
        IEnumerable<IPropertyAccessor> GetPropertyAccessors(Type type);

        /// <summary>
        /// Gets the property accessor for specified property name of the specified dto type.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        IPropertyAccessor GetPropertyAccessor(Type dtoType, string propertyName);
    }
}