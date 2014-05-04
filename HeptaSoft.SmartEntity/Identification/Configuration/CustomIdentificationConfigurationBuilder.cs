using HeptaSoft.Common.Helpers;
using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Mapping.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Identification.Configuration
{
    internal class CustomCustomIdentificationConfigurationBuilder<TEntityData> : ICustomIdentificationConfigurationBuilder<TEntityData>
        where TEntityData : class
    {
        /// <summary>
        /// The identifications manager.
        /// </summary>
        private readonly IFindersRegistrator identificationsContainer;

        /// <summary>
        /// The property accessor provider.
        /// </summary>
        private readonly IPropertyAccessorsProvider propertyAccessorsProvider;

        /// <summary>
        /// The identification predicate factory.
        /// </summary>
        private readonly IFinderFactory finderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomCustomIdentificationConfigurationBuilder{TEntityData}" /> class.
        /// </summary>
        /// <param name="finderFactory">The identification predicate factory.</param>
        /// <param name="identificationsContainer">The identifications container.</param>
        /// <param name="propertyAccessorsProvider">The property accessors provider.</param>
        public CustomCustomIdentificationConfigurationBuilder(IFinderFactory finderFactory, IFindersRegistrator identificationsContainer, IPropertyAccessorsProvider propertyAccessorsProvider)
        {
            this.finderFactory = finderFactory;
            this.identificationsContainer = identificationsContainer;
            this.propertyAccessorsProvider = propertyAccessorsProvider;
        }

        #region ICustomIdentificationConfigurationBuilder

        /// <inheritdoc />
        public void AddKey(params Expression<Func<TEntityData, object>>[] keyProperties)
        {
            var keyPropertyAccessors = new List<IPropertyAccessor>();
            foreach (var keyProperty in keyProperties)
            {
                var propertyName = ExpressionHelper.ExtractMemberExpressions(keyProperty).First().Member.Name;
                var accessor = this.propertyAccessorsProvider.GetPropertyAccessor(typeof(TEntityData), propertyName);
                keyPropertyAccessors.Add(accessor);
            }

            var predicate = this.finderFactory.Create(keyPropertyAccessors);

            this.identificationsContainer.RegisterFinder(typeof(TEntityData), predicate);
        }

        #endregion
    }
}
