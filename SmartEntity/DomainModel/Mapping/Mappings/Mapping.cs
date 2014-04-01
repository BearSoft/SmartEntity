using SmartEntity.DomainModel.Mapping.Accessors;

namespace SmartEntity.DomainModel.Mapping.Mappings
{
    internal class Mapping : IMapping
    {
        public IValueGetter SourceValueGetter { get; private set; }

        public IPropertyAccessor TargetValueAccessor { get; private set; }

        public Mapping(IValueGetter sourceValueGetter, IPropertyAccessor targetValueAccessor)
        {
            this.SourceValueGetter = sourceValueGetter;
            this.TargetValueAccessor = targetValueAccessor;
        }
    }
}
