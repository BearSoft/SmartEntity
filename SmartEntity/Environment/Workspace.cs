using System;
using System.Linq.Expressions;
using SmartEntity.DataAccess;
using SmartEntity.DomainModel;
using SmartEntity.DomainModel.Identification.Configuration;
using SmartEntity.DomainModel.Mapping.Configuration;
using SmartEntity.DomainModel.Mapping.Conversion;
using SmartEntity.Environment.Providers;
using SmartEntity.Modularity;

namespace SmartEntity.Environment
{
    public class Workspace : IWorkspace
    {
        /// <summary>
        /// The functionality resolver.
        /// </summary>
        private readonly IFunctionalityResolver resolver;

        /// <summary>
        /// The converters stack.
        /// </summary>
        private readonly IConverterStack convertersStack;

        /// <summary>
        /// The context factory container.
        /// </summary>
        private readonly IContextFactoryContainer contextFactoryContainer;

        /// <summary>
        /// The repository filter executor registrator
        /// </summary>
        private readonly IRepositoryFilterExecutorRegistrator repositoryFilterExecutorRegistrator;

        /// <summary>
        /// The entity data factory registrator.
        /// </summary>
        private readonly IRepositoryAccessorConfigurator repositoryAccessorConfigurator;

        /// <summary>
        /// Prevents a default instance of the <see cref="Workspace" /> class from being created.
        /// </summary>
        /// <param name="convertersStack">The converters stack.</param>
        /// <param name="contextFactoryContainer">The context factory container.</param>
        /// <param name="repositoryFilterExecutorRegistrator">The repository filter executor registrator.</param>
        /// <param name="repositoryAccessorConfigurator">The entity data factory registrator.</param>
        /// <param name="resolver">The resolver.</param>
        internal Workspace(IConverterStack convertersStack, IContextFactoryContainer contextFactoryContainer, IRepositoryFilterExecutorRegistrator repositoryFilterExecutorRegistrator, IRepositoryAccessorConfigurator repositoryAccessorConfigurator, IFunctionalityResolver resolver)
        {
            this.convertersStack = convertersStack;
            this.contextFactoryContainer = contextFactoryContainer;
            this.repositoryFilterExecutorRegistrator = repositoryFilterExecutorRegistrator;
            this.repositoryAccessorConfigurator = repositoryAccessorConfigurator;

            this.resolver = resolver;
        }

        /// <summary>
        /// Gets the global fullWorkspace.
        /// </summary>
        /// <value>
        /// The fullWorkspace.
        /// </value>
        public static IWorkspace Current
        {
            get
            {
                return new Workspace(ControlModule.OwnedResolver.Resolve<IConverterStack>(), ControlModule.OwnedResolver.Resolve<IContextFactoryContainer>(), ControlModule.OwnedResolver.Resolve<IRepositoryFilterExecutorRegistrator>(), ControlModule.OwnedResolver.Resolve<IRepositoryAccessorConfigurator>(), ControlModule.OwnedResolver);
            }
        }

        /// <summary>
        /// Pushes a new converter on top of the converteres stack.
        /// Whenever a converted is needed, the selection starts with the top-most instance in the stack.
        /// </summary>
        /// <param name="converter">The converter instance.</param>
        public void PushConverter(IConverter converter)
        {
            lock (this.convertersStack)
            {
                this.convertersStack.PushConverter(converter);
            }
        }

        /// <summary>
        /// Register an entity configurator.
        /// </summary>
        /// <returns>The configurator for the specified entity type.</returns>
        public void RegisterEntityConfigurator<TEntityData>(IEntityConfigurator<TEntityData> configurator) where TEntityData : class
        {
            // register the cotext factory
            var contextFactory = configurator.GetDataContextFactory();
            this.contextFactoryContainer.RegisterContextFactory(typeof(TEntityData), contextFactory);
            var repositoryFactory = this.resolver.Resolve<IEntityRepositoryFactory<TEntityData>>();
            var repositoryInstance = repositoryFactory.Create(contextFactory);
            Expression<Func<Expression<Func<TEntityData, bool>>, TEntityData>>  getFromRepositoryLambda = (filterExpression) => repositoryInstance.GetSingleOrNullByFilter(filterExpression);
            this.repositoryFilterExecutorRegistrator.RegisterFilterExecutor(typeof(TEntityData), getFromRepositoryLambda);

            // register the entity data factory

            this.repositoryAccessorConfigurator.RegisterRepository<TEntityData>(repositoryInstance);

            // run identification configuration
            var identificationConfigurationBuilder = this.resolver.Resolve<ICustomIdentificationConfigurationBuilder<TEntityData>>();
            configurator.ConfigureIdentification(identificationConfigurationBuilder);

            // run mapping configuration
            var entityMappingConfiguration = this.resolver.Resolve<IEntityMappingConfiguration<TEntityData>>();
            configurator.ConfigureMappings(entityMappingConfiguration);

            // run validation configuration

        }
    }
}
