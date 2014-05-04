using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal class PropertyAccessor : ValueGetter, IPropertyAccessor
    {
        private const string DtoInstanceSymbol = "x";

        private readonly Delegate setValueDelegate;

        public PropertyAccessor(Type dtoType, string propertyName)
            : base(BuildValueGetter(dtoType, propertyName))
        {
            this.PropertyName = propertyName;
            this.DtoType = dtoType;
            this.setValueDelegate = this.BuildValueSetter(dtoType, propertyName);
        }

        #region IPropertyAccessor

        /// <inheritdoc />
        public string PropertyName { get; private set; }

        /// <inheritdoc />
        public void SetValue(object instance, object value)
        {
            this.setValueDelegate.DynamicInvoke(instance, value);
        }

        /// <inheritdoc />
        public Type DtoType { get; private set; }

        /// <inheritdoc />
        public MemberExpression MemberExpression { get; private set; }

        #endregion

        /// <summary>
        /// Builds the value getter.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The compiled lambda expression.</returns>
        private static Delegate BuildValueGetter(Type dtoType, string propertyName)
        {
            var propertyInfo = dtoType.GetProperty(propertyName);

            var instanceExp = Expression.Parameter(dtoType, DtoInstanceSymbol);
            var propertyExp = Expression.Property(instanceExp, propertyInfo);
            var lambda = Expression.Lambda(propertyExp, instanceExp);

            return lambda.Compile();
        }

        /// <summary>
        /// Builds the value setter.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The compiled lambda expression.</returns>
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
