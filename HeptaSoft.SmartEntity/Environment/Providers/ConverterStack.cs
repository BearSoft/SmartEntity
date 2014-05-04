using HeptaSoft.SmartEntity.Mapping.Conversion;
using System;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal class ConvertersStack : IConverterStack
    {
        private readonly List<IConverter> converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersStack"/> class.
        /// </summary>
        public ConvertersStack()
        {
            this.converters = new List<IConverter>();
        }

        #region IConverterStack

        /// <inheritdoc />
        public void PushConverter(IConverter converter)
        {
            this.converters.Insert(0, converter);
        }

        /// <inheritdoc />
        public IConverter GetTopMatchingConverter(Type fromType, Type toType)
        {
            foreach (var converter in this.converters)
            {
                if (converter.CanConvert(fromType, toType))
                {
                    return converter;
                }
            }

            return null;
        }

        #endregion
    }
}
