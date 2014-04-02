using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace HeptaSoft.SmartEntity.Modularity
{
    internal class UnityBootstrapperWrapper: UnityBootstrapper
    {
        private readonly IModuleCatalog _moduleCatalog;

        public new IUnityContainer Container { get; private set; }

        public UnityBootstrapperWrapper(IModuleCatalog moduleCatalog)
        {
            this._moduleCatalog = moduleCatalog;
            this.Container = new UnityContainer();
        }



        protected override DependencyObject CreateShell()
        {
            return null;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return this._moduleCatalog;
        }

        protected override IUnityContainer CreateContainer()
        {
            return this.Container;
        }

        
    }
}
