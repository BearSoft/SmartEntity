using System.Collections.Generic;
using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Identification
{
    internal interface IFinderFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFinder"/> instance.
        /// </summary>
        /// <param name="keyProperties">The key properties.</param>
        /// <returns></returns>
        IFinder Create(IEnumerable<IPropertyAccessor> keyProperties);
    }
}