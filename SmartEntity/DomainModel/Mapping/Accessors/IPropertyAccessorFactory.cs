using System;

namespace SmartEntity.DomainModel.Mapping.Accessors
{
    internal interface IPropertyAccessorFactory
    {
        /// <summary>
        /// Creates the property accessor.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        IPropertyAccessor CreatePropertyAccessor(Type dtoType, string propertyName);
    }
}