using System;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Engines
{
    internal interface ITypeMapper
    {
        /// <summary>
        /// Maps the specified dto to required target type.
        /// If the target is an entity, repository retrieval will be tried first.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>The new source instance (new or existing) updated with the Dto provided values.</returns>
        object MapToEntity(object sourceInstance, Type targetType);

        /// <summary>
        /// Maps the specified dto to required target type.
        /// If the target is an entity and <paramref name="disableSelfIdentification"/> is set to <c>false</c>, repository retrieval will be tried first.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="disableSelfIdentification">if set to <c>true</c> [disable self identification].</param>
        /// <returns>The new source instance (new or existing) updated with the Dto provided values.</returns>
        object MapToEntity(object sourceInstance, Type targetType, bool disableSelfIdentification);

        /// <summary>
        /// Updates all the properties in the target with the values of the corresponding properties from the provided source.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetInstance">The target instance.</param>
        void MapToEntity(object sourceInstance, object targetInstance);

        /// <summary>
        /// Maps the entity to dto.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>The dto instance.</returns>
        object MapToDto(object sourceInstance, Type targetType);
    }
}