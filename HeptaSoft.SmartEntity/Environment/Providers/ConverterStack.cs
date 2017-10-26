using System.Linq;
using HeptaSoft.SmartEntity.Mapping.Conversion;
using System;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal class ConvertersStack : IConverterStack
    {
        /// <summary>
        /// The converters list
        /// </summary>
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
        public void PushConverter(params IConverter[] converter)
        {
            foreach (var conv in converter)
            {
                this.converters.Insert(0, conv);
            }
        }

        /// <inheritdoc />
        public bool RemoveConverter(params IConverter[] converter)
        {
            bool toReturn = true;
            foreach (var conv in converter)
            {
                toReturn = toReturn && this.converters.Remove(conv);
            }
            return toReturn;
        }

        /// <inheritdoc />
        public IList<IConverter> GetConverters()
        {
            return this.converters;
        }

        /// <inheritdoc />
        public void ClearConverters()
        {
            this.converters.Clear();
        }

        /// <inheritdoc />
        public IConverter GetTopMatchingConverter(Type fromType, Type toType)
        {
            return this.converters.FirstOrDefault(x => x.CanConvert(fromType, toType));
        }

        #endregion
    }
}
