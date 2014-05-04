using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Mappings
{
    internal class MappingFactory : IMappingFactory
    {
        /// <inheritdoc />
        public IMapping Create(IValueGetter sourceValueGetter, IPropertyAccessor targetPropertyAccessor)
        {
            // create the new mapping instance
            return new Mapping(sourceValueGetter, targetPropertyAccessor);
        }
    }
}
