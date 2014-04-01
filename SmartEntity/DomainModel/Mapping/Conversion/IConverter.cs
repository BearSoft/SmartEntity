using System;

namespace SmartEntity.DomainModel.Mapping.Conversion
{
    public interface IConverter
    {
        /// <summary>
        /// Converts the specified value to required type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="requiredType">Type of the required.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        object ConvertTo(object value, Type requiredType);
        
        /// <summary>
        /// Determines whether this instance can convert from <paramref name="from"/> type to <paramref name="to"/> type.
        /// </summary>
        /// <param name="from">Source type</param>
        /// <param name="to">Required output type.</param>
        /// <returns>
        ///   <c>true</c> if this instance can perform the conversion between specified types; otherwise, <c>false</c>.
        /// </returns>
        bool CanConvert(Type from, Type to);

    }
}
