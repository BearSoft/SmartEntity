using System;
using HeptaSoft.SmartEntity.Mapping.Conversion;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal interface IConverterStack
    {
        /// <summary>
        /// Adds a converter on top of the stack. The last added has the highest priority on retrieval.
        /// </summary>
        /// <param name="converter">The converter.</param>
        void PushConverter(IConverter converter);

        /// <summary>
        /// Gets the top matching converter.
        /// </summary>
        /// <param name="fromType">From type.</param>
        /// <param name="toType">To type.</param>
        /// <returns>The top matching converter, or null if no matching converter exists in the stack.</returns>
        IConverter GetTopMatchingConverter(Type fromType, Type toType);
    }
}