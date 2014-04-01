using System;
using System.Linq.Expressions;

namespace SmartEntity.DomainModel.Mapping.Configuration
{
    public interface ICustomMappingConfigurationBuilder<TSource, TDestination>
    {
        /// <summary>
        /// Adds a new custom defined mapping.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="destinationProperty">The destination property.</param>
        void AddMapping<TValue>(Expression<Func<TSource, TValue>> sourceProperty, Expression<Func<TDestination, TValue>> destinationProperty);

        /// <summary>
        /// Adds a new custom defined mapping.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sourceValueGetDelegate">The source value get delegate.</param>
        /// <param name="destinationProperty">The destination property.</param>
        void AddMapping<TValue>(Func<TSource, TValue> sourceValueGetDelegate, Expression<Func<TDestination, TValue>> destinationProperty);
    }
}