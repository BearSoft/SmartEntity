using System;
using SmartEntity.DomainModel.Mapping;
using SmartEntity.DomainModel.Mapping.Accessors;
using SmartEntity.DomainModel.Mapping.Mappings;

namespace SmartEntity.Environment.Providers
{
    internal interface IMappingsManager
    {
        /// <summary>
        /// Adds a mapping to the manager's conatiner.
        /// </summary>
        /// <param name="sourceValueGetter">The source value getter.</param>
        /// <param name="sourceDtoType">Type of the source dto.</param>
        /// <param name="targetPropertyAccessor">The target property accessor.</param>
        void AddMapping(IValueGetter sourceValueGetter, Type sourceDtoType, IPropertyAccessor targetPropertyAccessor);

        /// <summary>
        /// Adds a mapping to the manager's conatiner.
        /// </summary>
        /// <param name="sourcePropertyAccessor">The source property accessor.</param>
        /// <param name="targetPropertyAccessor">The target property accessor.</param>
        void AddMapping(IPropertyAccessor sourcePropertyAccessor, IPropertyAccessor targetPropertyAccessor);

        /// <summary>
        /// Gets the mapping.
        /// </summary>
        /// <param name="targetDtoType">Type of the target dto.</param>
        /// <param name="sourceDtoType">Type of the source dto.</param>
        /// <param name="targetPropertyPath">The path of the target property.</param>
        /// <returns>The appropriate mapping, or null if none was explicitelly defined or possible to auto-generate.</returns>
        IMapping GetMapping(Type targetDtoType, Type sourceDtoType, PropertyPath targetPropertyPath);
    }
}