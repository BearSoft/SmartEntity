using System;
using System.Collections.Generic;
using SmartEntity.DataAccess;

namespace SmartEntity.UnitOfWork
{
    /// <summary>
    /// Unit of work implementation.
    /// Manages db changes on multiple contexts.
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        /// <summary>
        /// The thread's current unit of work instance.
        /// </summary>
        [ThreadStatic]
        private static UnitOfWork currentInstance;

        /// <summary>
        /// The db contexts this unit of work instance manages.
        /// </summary>
        private readonly Dictionary<Type, IDbContext> contexts;

        /// <summary>
        /// Flag indicates if this object is disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Gets the thread's current UoW.
        /// </summary>
        /// <value>
        /// The current unit of work for the calling thread.
        /// </value>
        public static UnitOfWork Current
        {
            get
            {
                return currentInstance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            if (currentInstance != null)
            {
                throw new InvalidOperationException("There already is a current instance of UoW for the current thread. Please dispose the first one if a new instance is needed.");
            }

            this.contexts = new Dictionary<Type, IDbContext>();
            currentInstance = this;
        }

        /// <summary>
        /// Gets the appropriate context. If no context is in place a new one will be created. 
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="contextFactory">The context factory.</param>
        /// <returns></returns>
        public TContext GetContext<TContext>(Func<TContext> contextFactory) where TContext : IDbContext
        {
            Type contextType = typeof(TContext);
            
            if (this.contexts.ContainsKey(contextType))
            {
                return ((TContext) this.contexts[contextType]);
            }
            else
            {
                // create a new entry for this Context Type
                var newContextInstance = contextFactory();

                // add this instance to the current dictionary
                this.contexts.Add(contextType, newContextInstance);

                return newContextInstance;
            }
        }

        /// <summary>
        /// Commits the changes in all the managed contexts.
        /// </summary>
        public void CommitChanges()
        {
            foreach (IDbContext context in this.contexts.Values)
            {
                context.SaveChanges();
            }
        }

        #region Disposing

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            currentInstance = null;
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposeUnmanagedResources">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposeUnmanagedResources)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposeUnmanagedResources)
                {
                    this.contexts.Clear();
                }

                // Call the appropriate methods to clean up
                // managed resources here.

                // Note disposing has been done.
                this.disposed = true;
            }
        }

        #endregion
    }
}
