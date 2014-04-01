using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace SmartEntity.Modularity
{
    public class FlexBootstrapper
    {
        private readonly UnityBootstrapperWrapper _underlyingBootstrapper;

        private bool isBusy;

        private readonly ModuleList modules;

        private readonly DependencyContainer injector;

        public EventHandler ModulesInitialized;
        public EventHandler ModulesStarted;
        public EventHandler ModulesStopped;

        public FlexBootstrapper(string moduleDirectoryPath)
            : this(new DirectoryModuleCatalog() { ModulePath = moduleDirectoryPath })
        {
        }

        public FlexBootstrapper(IModuleCatalog moduleCatalog)
        {
            this.modules = new ModuleList();
            this.injector = new DependencyContainer();
            this._underlyingBootstrapper = new UnityBootstrapperWrapper(moduleCatalog);

            this._underlyingBootstrapper.Container.RegisterInstance<IFunctionalityRegistrator>(this.injector);
            this._underlyingBootstrapper.Container.RegisterInstance<IFunctionalityResolver>(this.injector);
            this.injector.RegisterInstance<ModuleList>(this.modules);
        }

        public IFunctionalityResolver Resolver
        {
            get
            {
                return this.injector;
            }
        }

        #region Specific implementation

        /// <summary>
        /// Runs the bootstrapping process, initializing and than starting all the modules.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The Bootstrapper is busy.</exception>
        public void Run()
        {
            if (!isBusy)
            {
                isBusy = true;
                
                // Init all the modulse
                this.InitModules();
                if (ModulesInitialized != null)
                {
                    ModulesInitialized(this, null);
                }

                // Start Modulse
                this.StartModules();
                if (ModulesStarted != null)
                {
                    ModulesStarted(this, null);
                }
            }
            else
            {
                throw new InvalidOperationException("The Bootstrapper is busy.");
            }
        }

        /// <summary>
        /// Shuts down all the modules.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The Bootstrapper is busy.</exception>
        public void ShutDown()
        {
            if (!isBusy)
            {
                // Stop Modulse
                this.StopModules();
                if (ModulesStopped != null)
                {
                    ModulesStopped(this, null);
                }
                else
                {
                    throw new InvalidOperationException("The Bootstrapper is busy.");
                }
            }
        }

        /// <summary>
        /// Inits all the modules.
        /// </summary>
        private void InitModules()
        {
            this.modules.Clear();
            this._underlyingBootstrapper.Run();
        }

        /// <summary>
        /// Starts all the modules.
        /// </summary>
        private void StartModules()
        {
            foreach (var moduleBase in modules)
            {
                moduleBase.Start(this.injector);
            }       
        }

        /// <summary>
        /// Stops all the modules.
        /// </summary>
        private void StopModules()
        {
            foreach (var moduleBase in modules)
            {
                moduleBase.ShutDown(this.injector);
            }
        }

        #endregion
    }
}
