using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal class PropertyAccessorFactory : IPropertyAccessorFactory
    {
        #region IPropertyAccessorFactory

        /// <inheritdoc />
        public IPropertyAccessor CreatePropertyAccessor(Type dtoType, string propertyName)
        {
            return new PropertyAccessor(dtoType, propertyName);
        }

        #endregion
    }
}
