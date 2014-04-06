using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace HeptaSoft.SmartEntity.Modularity
{
    public interface IFunctionalityResolver
    {
        object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides);

        object Resolve(Type t, params ResolverOverride[] overrides);

        T Resolve<T>(params ResolverOverride[] overrides);

        T Resolve<T>(string name, params ResolverOverride[] overrides);
        
        IEnumerable<T> ResolveAll<T>(params ResolverOverride[] resolverOverrides);
        
        IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Determines whether the specified type is resolvable.
        /// A type is resolvable either if it is register or if it is a non-abstract class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>True</c> in case an instance of the specified type can be resolved.</returns>
        bool IsResolvable(Type type);
    }
}
