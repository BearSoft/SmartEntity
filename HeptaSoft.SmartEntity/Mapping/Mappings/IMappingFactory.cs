using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Mappings
{
    internal interface IMappingFactory
    {
        IMapping Create(IValueGetter sourceValueGetter, IPropertyAccessor targetPropertyAccessor);
    }
}