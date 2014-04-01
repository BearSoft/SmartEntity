using System;

namespace SmartEntity.Modularity
{
    public interface IFunctionalityRegistrator
    {
        void RegisterType<TFunctionalityContract, TFunctionalityImplememntation>()
            where TFunctionalityImplememntation : class, TFunctionalityContract;

        void RegisterType(Type contractType, Type implementingType);
 
        void RegisterInstance<TFunctionalityContract>(TFunctionalityContract instance);

        void RegisterAsSigleton<TFunctionalityContract, TFunctionalityImplememntation>() where TFunctionalityImplememntation : class, TFunctionalityContract;

        void RegisterAsSigleton(Type functionalityContract, Type implememntationType);

    }

  
}
