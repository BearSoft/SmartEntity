using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal interface IValueGetter
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        object GetValue(object instance);

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        Type ValueType { get; }
    }
}
