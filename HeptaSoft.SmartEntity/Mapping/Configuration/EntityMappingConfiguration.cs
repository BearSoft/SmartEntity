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

        /// <summary>
        /// Exposes the "to dto" mapping configuration builder.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <returns>A builder for the mapping configuration./></returns>
        public ICustomMappingConfigurationBuilder<TEntityData, TDto> ToDto<TDto>()
        {
            return resolver.Resolve<ICustomMappingConfigurationBuilder<TEntityData, TDto>>();
        }

        /// <summary>
        /// Exposes the "from dto" mapping configuration builder.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <returns>A builder for the mapping configuration./></returns>
        public ICustomMappingConfigurationBuilder<TDto, TEntityData> FromDto<TDto>()
        {
            return resolver.Resolve<ICustomMappingConfigurationBuilder<TDto, TEntityData>>();
        }
    }
}
