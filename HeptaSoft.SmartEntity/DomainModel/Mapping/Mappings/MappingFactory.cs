using HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Mappings
{
    internal class MappingFactory : IMappingFactory
    {
        /// <summary>
        /// Creates a new mapper instance.
        /// </summary>
        /// <param name="sourceValueGetter">The source value getter.</param>
        /// <param name="targetPropertyAccessor">The target property accessor.</param>
        /// <returns>The new mapper instance.</returns>
        public IMapping Create(IValueGetter sourceValueGetter, IPropertyAccessor targetPropertyAccessor)
        {
            // create the new mapping instance
            return new Mapping(sourceValueGetter, targetPropertyAccessor);
        }
    }
}
