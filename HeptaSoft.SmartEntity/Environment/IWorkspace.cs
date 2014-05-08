using HeptaSoft.SmartEntity.Mapping.Conversion;

namespace HeptaSoft.SmartEntity.Environment
{
    public interface IWorkspace
    {
        /// <summary>
        /// Pushes a new converter on top of the converters stack.
        /// Whenever a converted is needed, the selection starts with the top-most instance in the stack.
        /// </summary>
        /// <param name="converter">The converter instance.</param>
        void PushConverters(params IConverter[] converter);

        /// <summary>
        /// Clears the converters (empties).
        /// </summary>
        void ClearConverters();

        /// <summary>
        /// Register an entity configuration.
        /// </summary>
        /// <returns>The configuration for the specified entity type.</returns>
        void RegisterEntityConfigurator<TEntityData>(IEntityConfigurator<TEntityData> configurator) where TEntityData : class; 
    }
}