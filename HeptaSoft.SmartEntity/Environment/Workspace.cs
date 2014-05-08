using HeptaSoft.Common.DataAccess;
using HeptaSoft.Common.Modularity;
using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Identification.Configuration;
using HeptaSoft.SmartEntity.Mapping.Configuration;
using HeptaSoft.SmartEntity.Mapping.Conversion;
using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Environment
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
        /// The entity data factory registration.
        /// </summary>
        private readonly IRepositoryAccessorConfigurator repositoryAccessorConfigurator;

        /// <summary>
        /// Prevents a default instance of the <see cref="Workspace" /> class from being created.
        /// </summary>
        /// <param name="convertersStack">The converters stack.</param>
        /// <param name="contextFactoryContainer">The context factory container.</param>
        /// <param name="repositoryFilterExecutorRegistrator">The repository filter executor registration.</param>
        /// <param name="repositoryAccessorConfigurator">The entity data factory registration.</param>
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

        #region IWorkspace

        /// <inheritdoc />
        public void PushConverters(params IConverter[] converter)
        {
            lock (this.convertersStack)
            {
                this.convertersStack.PushConverters(converter);
            }
        }

        /// <inheritdoc />
        public void ClearConverters()
        {
            lock (this.convertersStack)
            {
                this.convertersStack.ClearConverters();
            }
        }

        /// <inheritdoc />
        public void RegisterEntityConfigurator<TEntityData>(IEntityConfigurator<TEntityData> configurator) where TEntityData : class
        {
            // register the context factory
            var contextFactory = configurator.GetDataContextFactory();
            this.contextFactoryContainer.RegisterContextFactory(typeof(TEntityData), contextFactory);
            var repositoryFactory = this.resolver.Resolve<IEntityRepositoryFactory<TEntityData>>();
            var repositoryInstance = repositoryFactory.Create(contextFactory);
            Expression<Func<Expression<Func<TEntityData, bool>>, TEntityData>> getFromRepositoryLambda = (filterExpression) => repositoryInstance.GetSingleOrNullByFilter(filterExpression);
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

        #endregion

    }
}
