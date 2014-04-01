using System;
using System.Collections.Generic;
using SmartEntity.DomainModel.Mapping.Accessors;

namespace SmartEntity.Environment.Providers
{
    internal class PropertyAccessorsProvider : IPropertyAccessorsProvider
    {
        /// <summary>
        /// The chached property accessors.
        /// </summary>
        private readonly Dictionary<Type,HashSet<IPropertyAccessor>> propertyAccessors;

        /// <summary>
        /// The property accessor factory.
        /// </summary>
        private readonly IPropertyAccessorFactory accessorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAccessorsProvider"/> class.
        /// </summary>
        /// <param name="accessorFactory">The accessor factory.</param>
        public PropertyAccessorsProvider(IPropertyAccessorFactory accessorFactory)
        {
            this.propertyAccessors = new Dictionary<Type, HashSet<IPropertyAccessor>>();
            this.accessorFactory = accessorFactory;
        }

        /// <summary>
        /// Gets the accessors for all the public properties of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The list of property accessors.</returns>
        public IEnumerable<IPropertyAccessor> GetPropertyAccessors(Type type)
        {
            if (!propertyAccessors.ContainsKey(type))
            {
                var generatedAccessors = this.GeneratePropertyAccessors(type);
                propertyAccessors.Add(type, generatedAccessors);
            }

            return propertyAccessors[type];
        }

        /// <summary>
        /// Gets the property accessor for specified property name of the specified dto type.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public IPropertyAccessor GetPropertyAccessor(Type dtoType, string propertyName)
        {
            var propertyAccessorsForType = GetPropertyAccessors(dtoType);
            foreach (var propertyAccessor in propertyAccessorsForType)
            {
                if (propertyAccessor.PropertyName == propertyName)
                {
                    return propertyAccessor;
                }
            }

            return null;
        }

        /// <summary>
        /// Generates the property accessors.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private HashSet<IPropertyAccessor> GeneratePropertyAccessors(Type type)
        {
            var accessors = new HashSet<IPropertyAccessor>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var accessor = accessorFactory.CreatePropertyAccessor(type, propertyInfo.Name);
                accessors.Add(accessor);
            }

            return accessors;
        } 

    }
}
