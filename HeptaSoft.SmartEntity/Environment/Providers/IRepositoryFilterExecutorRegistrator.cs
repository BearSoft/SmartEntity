using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal interface IRepositoryFilterExecutorRegistrator
    {
        /// <summary>
        /// Registers the filter executor.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="executeFilterExpression">The execute filter expression.</param>
        void RegisterFilterExecutor(Type entityType, LambdaExpression executeFilterExpression);
    }
}