using System;
using SmartEntity.DataAccess;
using SmartEntity.DomainModel.Identification.Configuration;
using SmartEntity.DomainModel.Mapping.Configuration;

namespace SmartEntity.DomainModel
{
    public interface IEntityConfigurator<TEntityData>
        where TEntityData : class
    {
        /// <summary>
        /// Provides the data context for this entity.
        /// </summary>
        /// <returns>The factory for the context factory.</returns>
        Func<IDataContext> GetDataContextFactory();

        /// <summary>
        /// Configures custom mappings from DTOs and to DTOs.
        /// </summary>
        /// <param name="mappingConfigurationBuilder">The mapping configuration builder.</param>
        void ConfigureMappings(IEntityMappingConfiguration<TEntityData> mappingConfigurationBuilder);

        /// <summary>
        /// Configures the identification deffinition of this entity.
        /// </summary>
        /// <param name="customIdentificationConfigurationBuilder">The identification configuration builder.</param>
        void ConfigureIdentification(ICustomIdentificationConfigurationBuilder<TEntityData> customIdentificationConfigurationBuilder);
    }
}