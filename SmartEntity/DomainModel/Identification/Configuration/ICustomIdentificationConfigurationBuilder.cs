using System;
using System.Linq.Expressions;

namespace SmartEntity.DomainModel.Identification.Configuration
{
    public interface ICustomIdentificationConfigurationBuilder<TEntityData>
        where TEntityData : class
    {
        /// <summary>
        /// Adds an identification key based on one or more properties.
        /// </summary>
        /// <param name="keyProperties">The properties composing the key.</param>
        void AddKey(params Expression<Func<TEntityData, object>>[] keyProperties);
    }
}