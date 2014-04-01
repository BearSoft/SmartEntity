using SmartEntity.DomainModel.Mapping.Accessors;

namespace SmartEntity.DomainModel.Mapping.Mappings
{
    internal interface IMappingFactory
    {
        IMapping Create(IValueGetter sourceValueGetter, IPropertyAccessor targetPropertyAccessor);
    }
}