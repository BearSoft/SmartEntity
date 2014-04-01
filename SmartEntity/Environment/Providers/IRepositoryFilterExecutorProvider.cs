using System;
using System.Linq.Expressions;

namespace SmartEntity.Environment.Providers
{
    internal interface IRepositoryFilterExecutorProvider
    {
        LambdaExpression GetFilterExecutor(Type entityType);
    }
}