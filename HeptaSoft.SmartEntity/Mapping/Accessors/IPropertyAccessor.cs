using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal interface IPropertyAccessor : IValueGetter
    {
        string PropertyName { get; }

        void SetValue(object instance, object value);

        Type DtoType { get; }

        MemberExpression MemberExpression { get; }
    }
}