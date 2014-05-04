using System;
using System.Globalization;

namespace HeptaSoft.SmartEntity.Mapping.Conversion
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

        /// <inheritdoc />
        protected override object DoConvert(object value, Type requiredType)
        {
            var valueAsConvertible = (IConvertible)value;
            var formatProvider = CultureInfo.CurrentCulture;

            return valueAsConvertible.ToType(requiredType, formatProvider);
        }

        /// <inheritdoc />
        public override bool CanConvert(Type from, Type to)
        {
            return (typeof(IConvertible).IsAssignableFrom(from)) && (typeof(IConvertible).IsAssignableFrom(to));
        }
    }
}
