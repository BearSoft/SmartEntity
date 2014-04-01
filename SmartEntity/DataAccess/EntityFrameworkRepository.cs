using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SmartEntity.Basic;

namespace SmartEntity.DataAccess
{
    /// <summary>
    /// The base of every repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class EntityFrameworkRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The context factory
        /// </summary>
        private readonly Func<IDbContext> contextFactory;

        /// <summary>
        /// The names of the entity's complex properties.
        /// </summary>
        private readonly IEnumerable<string> entityComplexPropertyNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public EntityFrameworkRepository(Func<IDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;

            entityComplexPropertyNames = ReflectionHelper.GetAllComplexProperties(typeof (TEntity));
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        protected DbSet<TEntity> Data
        {
            get
            {
                this.GetCurrentContext().Set(typeof(string));
                return this.GetCurrentContext().Set<TEntity>();
            }
        }

        public TEntity CreateAndAdd()
        {
            var newEntity = (TEntity)ReflectionHelper.CreateNewInstance(typeof (TEntity));
            this.Data.Add(newEntity);

            return newEntity;
        }

        /// <summary>
        /// It deletes the object from context.
        /// </summary>
        /// <param name="entity">
        /// The entity to be deleted.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// The method is not implemented.
        /// </exception>
        public virtual void Delete(TEntity entity)
        {
            this.Data.Remove(entity);
        }

        /// <summary>
        /// Gets all the entity data instances matching the specified filter expression.
        /// </summary>
        /// <param name="filterCondition">The filter condition.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filterCondition)
        {
            if (filterCondition != null)
            {
                return this.Data.Where(filterCondition).ToList();
            }
            else
            {
                return this.Data.ToList();
            }
            
        }

        /// <summary>
        /// Gets the single entity data instance matching the specified filter expression.
        /// If no match, a null is returned.
        /// If more than one match found, an exception is thrown.
        /// </summary>
        /// <param name="filterCondition">The filter condition.</param>
        /// <returns>The matching entity data or null, if none found.</returns>
        public TEntity GetSingleOrNullByFilter(Expression<Func<TEntity, bool>> filterCondition)
        {
            var allData = GetDataWithAllIncluded();
            return allData.SingleOrDefault(filterCondition);
        }

        /// <summary>
        /// It deletes the object from context.
        /// </summary>
        /// <param name="entity">
        /// The entity to be deleted.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// The method is not implemented.
        /// </exception>
        public void Delete(object entity)
        {
            this.Delete((TEntity)entity);
        }

        #region Private Methods

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <returns></returns>
        private IDbContext GetCurrentContext()
        {
            if (UnitOfWork.UnitOfWork.Current == null)
            {
                throw new InvalidOperationException("When a db context is needed, a UoW should already be in place. Currently there is no UoW.");
            }

            return UnitOfWork.UnitOfWork.Current.GetContext(this.contextFactory);
        }

        /// <summary>
        /// Gets the data with all included.
        /// </summary>
        /// <returns></returns>
        private IQueryable<TEntity> GetDataWithAllIncluded()
        {
            IQueryable<TEntity> data = this.Data;
            foreach (var entityComplexPropertyName in entityComplexPropertyNames)
            {
                data = data.Include(entityComplexPropertyName);
            }

            return data;
        } 
        #endregion

      
    }
}
