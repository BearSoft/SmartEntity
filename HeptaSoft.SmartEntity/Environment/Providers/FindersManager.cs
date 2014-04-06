using System;
using System.Collections.Generic;
using HeptaSoft.SmartEntity.DomainModel.Identification;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal class FindersManager : IFindersRegistrator, IFinderProvider
    {
        /// <summary>
        /// The entity key definitions
        /// </summary>
        private readonly Dictionary<Type, HashSet<IFinder>> identificationPredicates;

        /// <summary>
        /// The property accessors provider.
        /// </summary>
        private readonly IPropertyAccessorsProvider propertyAccessorsProvider;

        /// <summary>
        /// The finder factory.
        /// </summary>
        private readonly IFinderFactory finderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindersManager" /> class.
        /// </summary>
        /// <param name="propertyAccessorsProvider">The property accessors provider.</param>
        /// <param name="finderFactory">The finder factory.</param>
        public FindersManager(IPropertyAccessorsProvider propertyAccessorsProvider, IFinderFactory finderFactory)
        {
            this.identificationPredicates = new Dictionary<Type, HashSet<IFinder>>();
            this.propertyAccessorsProvider = propertyAccessorsProvider;
            this.finderFactory = finderFactory;
        }

        /// <summary>
        /// Adds the key.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="finder">The key definition.</param>
        public void RegisterFinder(Type entityType, IFinder finder)
        {
            if (!this.identificationPredicates.ContainsKey(entityType))
            {
                this.identificationPredicates.Add(entityType, new HashSet<IFinder>());
            }

            this.identificationPredicates[entityType].Add(finder);
        }

        /// <summary>
        /// Gets the identification predicates associated with the specified type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>All instances of <see cref="IFinder"/> defined for the specified type.</returns>
        public IEnumerable<IFinder> GetFinders(Type entityType)
        {
            if (!this.identificationPredicates.ContainsKey(entityType))
            {
                var defaultFinder = this.BuildDefaultFinder(entityType);
                this.RegisterFinder(entityType, defaultFinder);
            }

            return this.identificationPredicates[entityType];
        }

        /// <summary>
        /// Builds the default finder.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        IFinder BuildDefaultFinder(Type entityType)
        {
            var allAccessors = this.propertyAccessorsProvider.GetPropertyAccessors(entityType);
            var finder = this.finderFactory.Create(allAccessors);

            return finder;
        }
    }
}
