using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Mappings
{
    internal interface IMappingFactory
    {
        /// <summary>
        /// Creates a new mapper instance.
        /// </summary>
        /// <param name="sourceValueGetter">The source value getter.</param>
        /// <param name="targetPropertyAccessor">The target property accessor.</param>
        /// <returns>The new mapper instance.</returns>
        IMapping Create(IValueGetter sourceValueGetter, IPropertyAccessor targetPropertyAccessor);
    }
}