using Microsoft.Practices.Prism.Modularity;

namespace HeptaSoft.SmartEntity.Modularity
{
    public abstract class ModuleBase : IModule
    {
        private readonly IFunctionalityRegistrator _containerRegistrator;
        private readonly IFunctionalityResolver _containerResolver;

        protected ModuleBase(IFunctionalityRegistrator containerRegistrator, IFunctionalityResolver containerResolver)
        {
            _containerRegistrator = containerRegistrator;
            _containerResolver = containerResolver;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            this.RegisterImplementations(_containerRegistrator);

            var modulesList = _containerResolver.Resolve<ModuleList>();
            modulesList.Add(this);
        }

        #endregion

        public abstract void RegisterImplementations(IFunctionalityRegistrator containerRegistrator);

        public virtual void Start(IFunctionalityResolver containerResolver)
        {
            // no specific implementation
        }

        public virtual void ShutDown(IFunctionalityResolver containerResolver)
        {
            // no specific implementation
        }
    }
}
