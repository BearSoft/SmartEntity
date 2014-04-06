using HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Mappings
{
    internal interface IMapping
    {
        IValueGetter SourceValueGetter { get; }

        IPropertyAccessor TargetValueAccessor { get; }
    }
}