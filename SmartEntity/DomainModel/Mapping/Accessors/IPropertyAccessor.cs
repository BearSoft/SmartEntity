using System;
using System.Linq.Expressions;

namespace SmartEntity.DomainModel.Mapping.Accessors
{
    internal interface IPropertyAccessor : IValueGetter
    {
        string PropertyName { get; }

        void SetValue(object instance, object value);

        Type DtoType { get; }

        MemberExpression MemberExpression { get; }
    }
}