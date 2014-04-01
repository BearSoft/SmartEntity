using System;
using System.Globalization;

namespace SmartEntity.DomainModel.Mapping.Conversion
{
    internal class ConvertibleConverter : ConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertibleConverter"/> class.
        /// </summary>
        public ConvertibleConverter()
            : base(null, null)
        {
        }

        /// <summary>
        /// Does the actual conversion convertion of provided value to the required type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="requiredType">Type of the required.</param>
        /// <returns>The converted value.</returns>
        protected override object DoConvert(object value, Type requiredType)
        {
            var valueAsConvertible = (IConvertible)value;
            var formatProvider = CultureInfo.CurrentCulture;

            return valueAsConvertible.ToType(requiredType, formatProvider);
        }

        /// <summary>
        /// Determines whether this instance can convert from <paramref name="from"/> type to <paramref name="to"/> type.
        /// </summary>
        /// <param name="from">Source type</param>
        /// <param name="to">Required output type.</param>
        /// <returns>
        ///   <c>true</c> if this instance can perform the conversion between specified types; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type @from, Type to)
        {
            return (typeof(IConvertible).IsAssignableFrom(@from)) && (typeof(IConvertible).IsAssignableFrom(to));
        }
    }
}
