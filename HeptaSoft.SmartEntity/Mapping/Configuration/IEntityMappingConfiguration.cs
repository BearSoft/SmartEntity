namespace HeptaSoft.SmartEntity.Mapping.Configuration
{
    public interface IEntityMappingConfiguration<TEntityData>
    {
        /// <summary>
        /// Exposes the "to dto" mapping configuration builder.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <returns>A builder for the mapping configuration./></returns>
        ICustomMappingConfigurationBuilder<TEntityData, TDto> ToDto<TDto>();

        /// <summary>
        /// Exposes the "from dto" mapping configuration builder.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <returns>A builder for the mapping configuration./></returns>
        ICustomMappingConfigurationBuilder<TDto, TEntityData> FromDto<TDto>();
    }
}