using System;
using System.Linq.Expressions;

namespace SmartEntity.Environment.Providers
{
    internal interface IRepositoryFilterExecutorRegistrator
    {
        void RegisterFilterExecutor(Type entityType, LambdaExpression executeFilterExpression);
    }
}