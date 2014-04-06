using System;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors
{
    internal class PropertyAccessorFactory : IPropertyAccessorFactory
    {
        /// <summary>
        /// Creates the property accessor.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public IPropertyAccessor CreatePropertyAccessor(Type dtoType, string propertyName)
        {
            return new PropertyAccessor(dtoType, propertyName);
        }
    }
}
