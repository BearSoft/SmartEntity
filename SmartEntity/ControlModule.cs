using SmartEntity.DataAccess;
using SmartEntity.DomainModel.Identification;
using SmartEntity.DomainModel.Identification.Configuration;
using SmartEntity.DomainModel.Mapping.Accessors;
using SmartEntity.DomainModel.Mapping.Configuration;
using SmartEntity.DomainModel.Mapping.Engines;
using SmartEntity.DomainModel.Mapping.Mappings;
using SmartEntity.Environment;
using SmartEntity.Environment.Providers;
using SmartEntity.Modularity;

namespace SmartEntity
{
    internal static class ControlModule
    {
        /// <summary>
        /// The _underlying container.
        /// </summary>
        private static DependencyContainer _underlyingContainer;

        /// <summary>
        /// Gets the owned resolver.
        /// </summary>
        /// <value>
        /// The owned resolver.
        /// </value>
        public static IFunctionalityResolver OwnedResolver
        {
            get
            {
                if (_underlyingContainer == null)
                {
                    _underlyingContainer = new DependencyContainer();
                    RegisterImplementations(_underlyingContainer);
                }

                return _underlyingContainer;
            }
        }

        /// <summary>
        /// Registers all internal the implementations using the registrant.
        /// </summary>
        /// <param name="containerRegistrant">The container registrant.</param>
        private static void RegisterImplementations(IFunctionalityRegistrator containerRegistrant)
        {
            // DataAccess
            containerRegistrant.RegisterAsSigleton<IRepositoryFilterExecutorProvider, RepositoryFilterExecutorsContainer>();
            containerRegistrant.RegisterAsSigleton<IRepositoryFilterExecutorRegistrator, RepositoryFilterExecutorsContainer>();
            containerRegistrant.RegisterAsSigleton(typeof(IEntityRepositoryFactory<>), typeof(EntityRepositoryFactory<>));
            
            // DomainModel
           
            // DomainModel.Identification
            containerRegistrant.RegisterAsSigleton (typeof(ICustomIdentificationConfigurationBuilder<>), typeof(CustomCustomIdentificationConfigurationBuilder<>));
            containerRegistrant.RegisterAsSigleton<IEntityFinder, EntityFinder>();
            containerRegistrant.RegisterAsSigleton<IFinder, Finder>();
            containerRegistrant.RegisterAsSigleton<IFinderFactory, FinderFactory>();
            
            // DomainModel.Mapping.Accessors
            containerRegistrant.RegisterAsSigleton<IPropertyAccessor, PropertyAccessor>();
            containerRegistrant.RegisterAsSigleton<IPropertyAccessorFactory, PropertyAccessorFactory>();
            containerRegistrant.RegisterAsSigleton<IValueGetter, ValueGetter>();
            containerRegistrant.RegisterAsSigleton<IValueGetterFactory, ValueGetterFactory>();

            // DomainModel.Mapping.Configuration
            containerRegistrant.RegisterAsSigleton(typeof(ICustomMappingConfigurationBuilder<,>), typeof(CustomCustomMappingConfigurationBuilder<,>));
            containerRegistrant.RegisterAsSigleton(typeof(IEntityMappingConfiguration<>), typeof(EntityMappingConfiguration<>));
            
            // DomainModel.Mapping.Engines
            containerRegistrant.RegisterAsSigleton<IDirectValueMapper, DirectValueMapper>();
            containerRegistrant.RegisterAsSigleton<IPropertyMatcher, PropertyMatcher>();
            containerRegistrant.RegisterAsSigleton<ITypeMapper, TypeMapper>();

            // DomainModel.Mapping.Mappings
            containerRegistrant.RegisterAsSigleton<IMapping, Mapping>();
            containerRegistrant.RegisterAsSigleton<IMappingFactory, MappingFactory>();

            // DomainModel.Validation
            
            
            // Environment
            containerRegistrant.RegisterAsSigleton<IWorkspace, Workspace>();
            
            // Environment.Providers:
            containerRegistrant.RegisterAsSigleton<IConverterStack, ConvertersStack>();
            containerRegistrant.RegisterAsSigleton<IPropertyAccessorsProvider, PropertyAccessorsProvider>();
            containerRegistrant.RegisterAsSigleton<IFinderProvider, FindersManager>();
            containerRegistrant.RegisterAsSigleton<IFindersRegistrator, FindersManager>();
            containerRegistrant.RegisterAsSigleton<IMappingsManager, MappingsManager>();
            containerRegistrant.RegisterAsSigleton<IContextFactoryProvider, ContextFactoryManager>();
            containerRegistrant.RegisterAsSigleton<IContextFactoryContainer, ContextFactoryManager>();
            containerRegistrant.RegisterAsSigleton<IRepositoryAccessorConfigurator, RepositoriesAccessor>();
            containerRegistrant.RegisterAsSigleton<IRepositoryAccessor, RepositoriesAccessor>();

            
            // Modularity
            containerRegistrant.RegisterAsSigleton<IFunctionalityRegistrator, DependencyContainer>();
            containerRegistrant.RegisterAsSigleton<IFunctionalityResolver, DependencyContainer>();
        }
    }
}
