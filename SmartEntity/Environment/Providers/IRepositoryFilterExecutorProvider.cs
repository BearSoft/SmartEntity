using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal interface IRepositoryFilterExecutorProvider
    {
        LambdaExpression GetFilterExecutor(Type entityType);
    }
}