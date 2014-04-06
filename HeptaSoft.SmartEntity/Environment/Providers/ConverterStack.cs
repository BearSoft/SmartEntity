using System;
using System.Collections.Generic;
using HeptaSoft.SmartEntity.DomainModel.Mapping.Conversion;

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
        /// <summary>
        /// Adds a converter on top of the stack. The last added has the highest priority on retrieval.
        /// </summary>
        /// <param name="converter">The converter.</param>
        public void PushConverter(IConverter converter)
        {
            this.converters.Insert(0, converter);
        }

        /// <summary>
        /// Gets the top matching converter.
        /// </summary>
        /// <param name="fromType">From type.</param>
        /// <param name="toType">To type.</param>
        /// <returns>The top matching converter, or null if no matching converter exists in the stack.</returns>
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
    }
}
