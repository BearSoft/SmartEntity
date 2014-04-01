using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartEntity.DataAccess
{
    /// <summary>
    /// The IEntityRepository interface. 
    /// </summary>
    /// <typeparam name="TDomainObjectInterface">
    /// It represents the type of entity.
    /// </typeparam>
    public interface IEntityRepository<TDomainObjectInterface> : IEntityRepository where TDomainObjectInterface : class
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates a new data entity and adds it to context.
        /// </summary>
        TDomainObjectInterface CreateAndAdd();

        /// <summary>
        /// It deletes the object from context.
        /// </summary>
        /// <param name="entity">
        /// The entity to be deleted.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// The method is not implemented.
        /// </exception>
        void Delete(TDomainObjectInterface entity);

        /// <summary>
        /// Gets data by filter.
        /// </summary>
        /// <returns>A list of matching entity datas.</returns>
        IEnumerable<TDomainObjectInterface> GetByFilter(Expression<Func<TDomainObjectInterface, bool>> filterCondition);

        /// <summary>
        /// Gets a single element that should match the filter criteria. If no elemt is found, null is returned.
        /// If the filter result is more than one elemt, an exception is thrown.
        /// </summary>
        /// <returns>A list of matching entity datas.</returns>
        TDomainObjectInterface GetSingleOrNullByFilter(Expression<Func<TDomainObjectInterface, bool>> filterCondition);

        #endregion
    }

    public interface IEntityRepository
    {
        /// <summary>
        /// It deletes the object from context.
        /// </summary>
        /// <param name="entity">
        /// The entity to be deleted.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// The method is not implemented.
        /// </exception>
        void Delete(object entity);
    }
}
