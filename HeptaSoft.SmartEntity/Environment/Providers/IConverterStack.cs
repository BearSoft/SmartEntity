using System;
using HeptaSoft.SmartEntity.Mapping.Conversion;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal interface IConverterStack
    {
        /// <summary>
        /// Adds converters on top of the stack. The last added has the highest priority on retrieval.
        /// </summary>
        /// <param name="converter">The converter.</param>
        void PushConverter(params IConverter[] converter);

        /// <summary>
        /// Removes a converter from the stack (if found).
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <returns>True if sucessfully removed, False otherwise.</returns>
        bool RemoveConverter(params IConverter[] converter);

        /// <summary>
        /// Gets all the converters that are currently registered.
        /// </summary>
        /// <returns>The current converters</returns>
        IList<IConverter> GetConverters();

        /// <summary>
        /// Clears the converters (empties).
        /// </summary>
        void ClearConverters();

        /// <summary>
        /// Gets the top matching converter.
        /// </summary>
        /// <param name="fromType">From type.</param>
        /// <param name="toType">To type.</param>
        /// <returns>The top matching converter, or null if no matching converter exists in the stack.</returns>
        IConverter GetTopMatchingConverter(Type fromType, Type toType);
    }
}