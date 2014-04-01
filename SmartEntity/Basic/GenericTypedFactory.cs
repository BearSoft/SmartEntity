using SmartEntity.Modularity;

namespace SmartEntity.Basic
{
    class GenericTypedFactory<T> 
    {
        private IFunctionalityResolver resolver;

        /// <summary>
        /// Resolves the instance of specified type.
        /// The specified type to be resolved has to be
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <returns></returns>
        public X Resolve<X>() where X : T
        {
            return resolver.Resolve<X>();
        }
    }
}
