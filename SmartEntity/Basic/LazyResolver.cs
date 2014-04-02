using System;

namespace HeptaSoft.SmartEntity.Basic
{
    public class LazyResolver<T>
    {
        /// <summary>
        /// The resolved instance.
        /// </summary>
        private T resolvedInstance;

        /// <summary>
        /// The actual resolver.
        /// </summary>
        private readonly Func<T> actualResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyResolver{T}"/> class.
        /// </summary>
        /// <param name="actualResolver">The actual resolver.</param>
        public LazyResolver(Func<T> actualResolver)
        {
            this.actualResolver = actualResolver;
        }

        public T Instance
        {
            get
            {
                if (object.Equals(resolvedInstance, default(T)))
                {
                    // resolve the instance and store it
                    resolvedInstance = this.actualResolver();
                }

                return resolvedInstance;
            }
         }
    }
}
