using System;
using HeptaSoft.SmartEntity.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.Mapping.Engines
{
    internal interface IDirectValueMapper
    {
        /// <summary>
        /// Maps the direct value.
        /// </summary>
        /// <param name="sourceDtoInstance">The source dto instance.</param>
        /// <param name="targetPropertyAccessor">The target property accessor.</param>
        /// <param name="targetOffsetPath">The target offset path.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">There is no registered Converter for the required conversion.</exception>
        object GetConvertedDirectValue(object sourceDtoInstance, IPropertyAccessor targetPropertyAccessor, PropertyPath targetOffsetPath);

        /// <summary>
        /// Gets the converted direct value.
        /// </summary>
        /// <param name="originalSourceValue">The original source value.</param>
        /// <param name="targetValueType">Type of the target value.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">There is no registered Converter for the required conversion.</exception>
        object GetConvertedDirectValue(object originalSourceValue, Type targetValueType);
    }
}