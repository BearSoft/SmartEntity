using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors
{
    internal class PropertyAccessor : ValueGetter, IPropertyAccessor
    {
        private const string DtoInstanceSymbol = "x";

        private readonly Delegate setValueDelegate;
        
        public string PropertyName { get; private set; }

        public PropertyAccessor(Type dtoType, string propertyName)
            : base(BuildValueGetter(dtoType, propertyName))
        {
            this.PropertyName = propertyName;
            this.DtoType = dtoType;
            this.setValueDelegate = this.BuildValueSetter(dtoType, propertyName);
        }

        public void SetValue(object instance, object value)
        {
            this.setValueDelegate.DynamicInvoke(instance, value);
        }

        public MemberExpression MemberExpression { get; private set; }

        public Type DtoType { get; private set; }

        private static Delegate BuildValueGetter(Type dtoType, string propertyName )
        {
            var propertyInfo = dtoType.GetProperty(propertyName);

            var instanceExp = Expression.Parameter(dtoType, DtoInstanceSymbol);
            var propertyExp = Expression.Property(instanceExp, propertyInfo);
            var lambda = Expression.Lambda(propertyExp, instanceExp);

            return lambda.Compile();
        }

        private Delegate BuildValueSetter(Type dtoType, string propertyName)
        {
            const string ValueSymbol = "v";

            var propertyInfo = dtoType.GetProperty(propertyName);

            var instanceExp = Expression.Parameter(dtoType, DtoInstanceSymbol);
            var propertyExp = Expression.Property(instanceExp, propertyInfo);
            var valueExp = Expression.Parameter(propertyExp.Type, ValueSymbol);
            var propertyEqualValue = Expression.Assign(propertyExp, valueExp);
            var lambda = Expression.Lambda(propertyEqualValue, instanceExp, valueExp);

            this.MemberExpression = propertyExp;
            return lambda.Compile();
        } 
    }
}
