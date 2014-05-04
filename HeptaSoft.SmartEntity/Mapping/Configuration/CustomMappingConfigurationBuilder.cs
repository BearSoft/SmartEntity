using HeptaSoft.Common.Helpers;
using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Mapping.Accessors;
using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Mapping.Configuration
{
    internal class CustomCustomMappingConfigurationBuilder<TSource, TDestination> : ICustomMappingConfigurationBuilder<TSource, TDestination>
    {
        /// <summary>
        /// The mapping manager.
        /// </summary>
        private readonly IMappingsManager mappingsManager;

        /// <summary>
        /// The property accessors provider.
        /// </summary>
        private readonly IPropertyAccessorsProvider propertyAccessorsProvider;

        /// <summary>
        /// The value getter factory.
        /// </summary>
        private readonly IValueGetterFactory valueGetterFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomCustomMappingConfigurationBuilder{TSource,TDestination}" /> class.
        /// </summary>
        /// <param name="propertyAccessorsProvider">The property accessors provider.</param>
        /// <param name="valueGetterFactory">The value getter factory.</param>
        /// <param name="mappingsManager">The mapping manager.</param>
        public CustomCustomMappingConfigurationBuilder(IPropertyAccessorsProvider propertyAccessorsProvider, IValueGetterFactory valueGetterFactory, IMappingsManager mappingsManager)
        {
            this.propertyAccessorsProvider = propertyAccessorsProvider;
            this.mappingsManager = mappingsManager;
            this.valueGetterFactory = valueGetterFactory;
        }

        #region ICustomMappingConfigurationBuilder

        /// <inheritdoc />
        public void AddMapping<TValue>(Expression<Func<TSource, TValue>> sourceProperty, Expression<Func<TDestination, TValue>> destinationProperty)
        {
            var sourcePropertyAccessor = this.propertyAccessorsProvider.GetPropertyAccessor(typeof(TSource), ExpressionHelper.GetMemberName(sourceProperty));
            var targetPropertyAccessor = this.propertyAccessorsProvider.GetPropertyAccessor(typeof(TDestination), ExpressionHelper.GetMemberName(destinationProperty));

            this.mappingsManager.AddMapping(sourcePropertyAccessor, targetPropertyAccessor);
        }

        /// <inheritdoc />
        public void AddMapping<TValue>(Func<TSource, TValue> sourceValueGetDelegate, Expression<Func<TDestination, TValue>> destinationProperty)
        {
            var sourceValueGetter = this.valueGetterFactory.CreateValueGetter(sourceValueGetDelegate);
            var targetPropertyAccessor = this.propertyAccessorsProvider.GetPropertyAccessor(typeof(TDestination), ExpressionHelper.GetMemberName(destinationProperty));

            this.mappingsManager.AddMapping(sourceValueGetter, typeof(TSource), targetPropertyAccessor);
        }

        #endregion
    }
}
