using System;

namespace HeptaSoft.SmartEntity.Mapping.Conversion
{
    internal class SameTypeConverter : ConverterBase
    {
        public SameTypeConverter()
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
            return value;
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
            return (@from == to);
        }
    }
}
