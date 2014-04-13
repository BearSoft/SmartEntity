using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Mappings
{
    internal class Mapping : IMapping
    {
        #region IMapping

        /// <inheritdoc />
        public IValueGetter SourceValueGetter { get; private set; }

        /// <inheritdoc />
        public IPropertyAccessor TargetValueAccessor { get; private set; }
        
        #endregion

        public Mapping(IValueGetter sourceValueGetter, IPropertyAccessor targetValueAccessor)
        {
            this.SourceValueGetter = sourceValueGetter;
            this.TargetValueAccessor = targetValueAccessor;
        }
    }
}
