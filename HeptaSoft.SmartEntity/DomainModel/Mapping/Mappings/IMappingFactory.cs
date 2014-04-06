using HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Mappings
{
    internal interface IMappingFactory
    {
        IMapping Create(IValueGetter sourceValueGetter, IPropertyAccessor targetPropertyAccessor);
    }
}