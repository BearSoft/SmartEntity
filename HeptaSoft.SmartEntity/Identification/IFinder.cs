using HeptaSoft.SmartEntity.Mapping.Accessors;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.Identification
{
    internal interface IFinder
    {
        /// <summary>
        /// Gets the required properties.
        /// </summary>
        /// <value>
        /// The required properties.
        /// </value>
        IEnumerable<IPropertyAccessor> RequiredProperties { get; }

        /// <summary>
        /// Identifies the specified parameters with values.
        /// </summary>
        /// <param name="keyPropertyValues">The key property values.</param>
        /// <returns></returns>
        object Find(IDictionary<IPropertyAccessor, object> keyPropertyValues);
    }
}