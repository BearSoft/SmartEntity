using HeptaSoft.SmartEntity.Mapping.Conversion;

namespace HeptaSoft.SmartEntity.Environment
{
    public interface IWorkspace
    {
        /// <inheritdoc />
        void PushConverter(params IConverter[] converter);

        /// <inheritdoc />
        bool RemoveConverter(params IConverter[] converter);

        /// <inheritdoc />
        void ResetConverters();

        /// <inheritdoc />
        void ClearConverters();

        /// <summary>
        /// Register an entity configuration.
        /// </summary>
        /// <returns>The configuration for the specified entity type.</returns>
        void RegisterEntityConfigurator<TEntityData>(IEntityConfigurator<TEntityData> configurator) where TEntityData : class; 
    }
}