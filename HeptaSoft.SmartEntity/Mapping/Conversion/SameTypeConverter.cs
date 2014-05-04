using System;

namespace HeptaSoft.SmartEntity.Mapping.Conversion
{
    internal class SameTypeConverter : ConverterBase
    {
        public SameTypeConverter()
            : base(null, null)
        {
        }

        /// <inheritdoc />
        protected override object DoConvert(object value, Type requiredType)
        {
            return value;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type from, Type to)
        {
            return (from == to);
        }
    }
}
