using System;
using System.Linq.Expressions;

namespace HeptaSoft.SmartEntity.Environment.Providers
{
    internal interface IRepositoryFilterExecutorRegistrator
    {
        void RegisterFilterExecutor(Type entityType, LambdaExpression executeFilterExpression);
    }
}