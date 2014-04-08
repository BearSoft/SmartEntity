using System.Collections.Generic;
using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Identification
{
    internal interface IFinder
    {
        /// <summary>
        /// Identifies the specified parameters with values.
        /// </summary>
        /// <param name="keyPropertyValues">The key property values.</param>
        /// <returns></returns>
        object Find(IDictionary<IPropertyAccessor, object> keyPropertyValues);

        /// <summary>
        /// Gets the required properties.
        /// </summary>
        /// <value>
        /// The required properties.
        /// </value>
        IEnumerable<IPropertyAccessor> RequiredProperties { get; }
    }
}