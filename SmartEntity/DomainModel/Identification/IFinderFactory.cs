using System.Collections.Generic;
using SmartEntity.DomainModel.Mapping.Accessors;

namespace SmartEntity.DomainModel.Identification
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