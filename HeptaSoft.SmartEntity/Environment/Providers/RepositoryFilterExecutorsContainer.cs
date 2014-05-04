using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal class RepositoryFilterExecutorsContainer : IRepositoryFilterExecutorProvider, IRepositoryFilterExecutorRegistrator
    {
        /// <summary>
        /// The repository execute filter expressions
        /// </summary>
        private readonly Dictionary<Type, LambdaExpression> repositoryExecuteFilterExpressions;

        public RepositoryFilterExecutorsContainer()
        {
            repositoryExecuteFilterExpressions = new Dictionary<Type, LambdaExpression>();
        }

        #region IRepositoryFilterExecutorProvider

        /// <inheritdoc />
        public LambdaExpression GetFilterExecutor(Type entityType)
        {
            if (repositoryExecuteFilterExpressions.ContainsKey(entityType))
            {
                return repositoryExecuteFilterExpressions[entityType];
            }
            else
            {
                throw new InvalidOperationException(string.Format("No repository filter executor registered for entity type <{0}>.", entityType));
            }
        }

        #endregion

        #region IRepositoryFilterExecutorRegistrator

        /// <inheritdoc />
        public void RegisterFilterExecutor(Type entityType, LambdaExpression executeFilterExpression)
        {
            repositoryExecuteFilterExpressions.Add(entityType, executeFilterExpression);
        }

        #endregion
    }
}
