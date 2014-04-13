using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Mappings
{
    internal interface IMapping
    {
        /// <summary>
        /// Gets the source target value.
        /// </summary>
        /// <value>
        /// The source target value getter.
        /// </value>
        IValueGetter SourceValueGetter { get; }

        /// <summary>
        /// Gets the destination target value.
        /// </summary>
        /// <value>
        /// The destination target value.
        /// </value>
        IPropertyAccessor TargetValueAccessor { get; }
    }
}