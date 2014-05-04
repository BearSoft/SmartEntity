using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Mapping.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeptaSoft.SmartEntity.Identification
{
    internal class FinderFactory : IFinderFactory
    {
        /// <summary>
        /// The repository provider.
        /// </summary>
        private readonly IRepositoryFilterExecutorProvider repositoryFilterExecutorProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinderFactory" /> class.
        /// </summary>
        /// <param name="repositoryFilterExecutorProvider">The repository filter executor provider.</param>
        public FinderFactory(IRepositoryFilterExecutorProvider repositoryFilterExecutorProvider)
        {
            this.repositoryFilterExecutorProvider = repositoryFilterExecutorProvider;
        }

        #region IFinderFactory

        /// <inheritdoc />
        public IFinder Create(IEnumerable<IPropertyAccessor> keyProperties)
        {
            var entityType = keyProperties.First().DtoType;
            try
            {
                var getFromRepositoryLambda =
                    this.repositoryFilterExecutorProvider.GetFilterExecutor(entityType);
                return new Finder(keyProperties, getFromRepositoryLambda);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Cannot create the finder instance for entity type<{0}>.", entityType), ex);
            }
        }

        #endregion

    }
}
