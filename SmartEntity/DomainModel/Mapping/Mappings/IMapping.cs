using SmartEntity.DomainModel.Mapping.Accessors;

namespace SmartEntity.DomainModel.Mapping.Mappings
{
    internal interface IMapping
    {
        IValueGetter SourceValueGetter { get; }

        IPropertyAccessor TargetValueAccessor { get; }
    }
}