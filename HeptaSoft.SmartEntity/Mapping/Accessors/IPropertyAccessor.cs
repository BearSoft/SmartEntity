using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal interface IPropertyAccessor : IValueGetter
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        string PropertyName { get; }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="value">The value.</param>
        void SetValue(object instance, object value);

        /// <summary>
        /// Gets the type of the dto.
        /// </summary>
        /// <value>
        /// The type of the dto.
        /// </value>
        Type DtoType { get; }

        /// <summary>
        /// Gets the member expression.
        /// </summary>
        /// <value>
        /// The member expression.
        /// </value>
        MemberExpression MemberExpression { get; }
    }
}