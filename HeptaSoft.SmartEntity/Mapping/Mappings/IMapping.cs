using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Mappings
{
    internal interface IMapping
    {
        IValueGetter SourceValueGetter { get; }

        IPropertyAccessor TargetValueAccessor { get; }
    }
}