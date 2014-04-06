using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace HeptaSoft.SmartEntity.Modularity
{
    internal class DependencyContainer : UnityContainer, IFunctionalityRegistrator, IFunctionalityResolver
    {
        private readonly UnityContainer self;

        public DependencyContainer()
        {
            self = (UnityContainer)this;
        }

        #region Implementation of IFunctionalityResolver

        public object Resolve(Type t, params ResolverOverride[] overrides)
        {
            return self.Resolve(t, overrides);
        }

        public T Resolve<T>(params ResolverOverride[] overrides)
        {
            return self.Resolve<T>(overrides);
        }

        public T Resolve<T>(string name, params ResolverOverride[] overrides)
        {
            return self.Resolve<T>(name, overrides);
        }

        public IEnumerable<T> ResolveAll<T>(params ResolverOverride[] resolverOverrides)
        {
            return self.ResolveAll<T>(resolverOverrides);
        }

        public bool IsResolvable(Type type)
        {
            return this.IsRegistered(type) || (type.IsClass && (!type.IsAbstract));
        }

        #endregion

        #region Implementation of IFunctionalityRegistrator

        public void RegisterType<TFunctionalityContract, TFunctionalityImplememntation>() where TFunctionalityImplememntation : class, TFunctionalityContract
        {
            self.RegisterType<TFunctionalityContract, TFunctionalityImplememntation>();
        }

        public void RegisterType(Type contractType, Type implementingType)
        {
            self.RegisterType(contractType, implementingType);
        }

        public void RegisterInstance<TFunctionalityContract>(TFunctionalityContract instance)
        {
            self.RegisterInstance(instance);
        }

        public void RegisterAsSigleton<TFunctionalityContract, TFunctionalityImplememntation>() where TFunctionalityImplememntation : class, TFunctionalityContract
        {
            self.RegisterType<TFunctionalityContract, TFunctionalityImplememntation>(new ContainerControlledLifetimeManager());
        }

        public void RegisterAsSigleton(Type functionalityContract, Type implememntationType)
        {
            self.RegisterType(functionalityContract, implememntationType, new ContainerControlledLifetimeManager());
        }

        #endregion

        
    }
}
