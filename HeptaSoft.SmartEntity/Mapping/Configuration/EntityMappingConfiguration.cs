using HeptaSoft.Common.Modularity;

namespace HeptaSoft.SmartEntity.Mapping.Configuration
{
    internal class EntityMappingConfiguration<TEntityData> : IEntityMappingConfiguration<TEntityData>
    {
        /// <summary>
        /// The functionality resolver.
        /// </summary>
        private readonly IFunctionalityResolver resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMappingConfiguration{TEntityData}"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public EntityMappingConfiguration(IFunctionalityResolver resolver)
        {
            this.resolver = resolver;
        }

        #region IEntityMappingConfiguration

        /// <inheritdoc />
        public ICustomMappingConfigurationBuilder<TEntityData, TDto> ToDto<TDto>()
        {
            return resolver.Resolve<ICustomMappingConfigurationBuilder<TEntityData, TDto>>();
        }

        /// <inheritdoc />
        public ICustomMappingConfigurationBuilder<TDto, TEntityData> FromDto<TDto>()
        {
            return resolver.Resolve<ICustomMappingConfigurationBuilder<TDto, TEntityData>>();
        }

        #endregion
    }
}
