using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal interface IRepositoryFilterExecutorProvider
    {
        /// <summary>
        /// Gets the filter executor.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The lambda expression.</returns>
        LambdaExpression GetFilterExecutor(Type entityType);
    }
}