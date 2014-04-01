using System;
using System.Collections.Generic;
using SmartEntity.DomainModel.Mapping;
using SmartEntity.DomainModel.Mapping.Accessors;
using SmartEntity.DomainModel.Mapping.Engines;
using SmartEntity.Environment.Providers;

namespace SmartEntity.DomainModel.Identification
{
    internal class EntityFinder : IEntityFinder
    {
        /// <summary>
        /// The identifications provider.
        /// </summary>
        private readonly IFinderProvider identificationsProvider;

        /// <summary>
        /// The direct value mapper.
        /// </summary>
        private readonly IDirectValueMapper directValueMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFinder" /> class.
        /// </summary>
        /// <param name="identificationsProvider">The identifications provider.</param>
        /// <param name="directValueMapper">The direct value mapper.</param>
        public EntityFinder(IFinderProvider identificationsProvider, IDirectValueMapper directValueMapper)
        {
            this.identificationsProvider = identificationsProvider;
            this.directValueMapper = directValueMapper;
        }

        /// <summary>
        /// Finds an entity the by identifying it using the values from the provided dto instance.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="dtoInstance">The dto instance.</param>
        /// <returns></returns>
        public object FindByDto(Type entityType, object dtoInstance)
        {
            return this.FindByDto(entityType, dtoInstance, null);
        }

        /// <summary>
        /// Finds the by dto, considering the target entity properties having the prefix specified by <paramref name="offsetPath"/>.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="dtoInstance">The dto instance.</param>
        /// <param name="offsetPath">The offset path.</param>
        /// <returns></returns>
        public object FindByDto(Type entityType, object dtoInstance, PropertyPath offsetPath)
        {
            var identificationPredicates = this.identificationsProvider.GetFinders(entityType);

            if (identificationPredicates == null)
            {
                throw new InvalidOperationException(string.Format("Cannot search for entity data: the identity for entity type <{0}> was not defined.", entityType));
            }

            foreach (var identificationPredicate in identificationPredicates)
            {
                var keyValuesSet = this.GetKeyValuesForPredicate(dtoInstance, identificationPredicate, offsetPath);
                if (keyValuesSet != null)
                {
                    var foundEntity = identificationPredicate.Find(keyValuesSet);
                    return foundEntity;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the key values for specified predicate.
        /// </summary>
        /// <param name="dtoInstance">The dto instance.</param>
        /// <param name="identificationPredicate">The identification predicate.</param>
        /// <param name="targetOffsetPath">The target offset path.</param>
        /// <returns>
        /// The all the values for the required properties as exposed by the specified <see cref="IFinder" />.
        /// </returns>
        private IDictionary<IPropertyAccessor, object> GetKeyValuesForPredicate(object dtoInstance, IFinder identificationPredicate, PropertyPath targetOffsetPath)
        {
            bool keyValueMissing = false;
            var keyValues = new Dictionary<IPropertyAccessor, object>();
            foreach (var keyPropertyAccessor in identificationPredicate.RequiredProperties)
            {
                var propertyConvertedValue = this.directValueMapper.GetConvertedDirectValue(dtoInstance, keyPropertyAccessor, targetOffsetPath);
                if (propertyConvertedValue != null)
                {
                    keyValues.Add(keyPropertyAccessor, propertyConvertedValue);
                }
                else
                {
                    keyValueMissing = true;
                    break;
                }
            }

            if (!keyValueMissing)
            {
                return keyValues;
            }
            else
            {
                return null;
            }            
        }
    }
}
