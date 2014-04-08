using System;
using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Identification;
using HeptaSoft.SmartEntity.Mapping.Engines;

namespace HeptaSoft.SmartEntity
{
    public class SmartEntity<TData> where TData : class, new()
    {
        /// <summary>
        /// Wether the entity data was loaded from repository or not.
        /// </summary>
        private bool wasLoadedFromRepository;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly ITypeMapper mapper;

        /// <summary>
        /// The entity data finder.
        /// </summary>
        private readonly IEntityFinder finder;

        /// <summary>
        /// The repository accessor.
        /// </summary>
        private readonly IRepositoryAccessor repositoryAccessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEntity{TData}" /> class.
        /// The internal constructor, with visible dependencies.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="finder">The finder.</param>
        /// <param name="repositoryAccessor">The repository accessor.</param>
        internal SmartEntity(ITypeMapper mapper, IEntityFinder finder, IRepositoryAccessor repositoryAccessor)
            : this(mapper, finder, repositoryAccessor, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEntity{TData}" /> class.
        /// The internal constructor, with visible dependencies.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="finder">The finder.</param>
        /// <param name="repositoryAccessor">The repository accessor.</param>
        /// <param name="entityData">The entity data.</param>
        internal SmartEntity(ITypeMapper mapper, IEntityFinder finder, IRepositoryAccessor repositoryAccessor, TData entityData)
        {
            this.mapper = mapper;
            this.finder = finder;
            this.repositoryAccessor = repositoryAccessor;

            this.Data = entityData;
        }

        /// <summary>
        /// Gets or sets the data of this model entity.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TData Data { get; private set; }
        
        #region Methods

        /// <summary>
        /// Populates the entity's data from the data provided in the Dto.
        /// The populated entity is not searched in the repository.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="source">The source.</param>
        public void FillFromDto<TDto>(TDto source)
        {
            if (this.Data == null)
            {
                this.Data = new TData();
                this.wasLoadedFromRepository = false;
            }
            this.mapper.MapToEntity(source, this.Data);
        }

        /// <summary>
        /// Builds the specified dto's "surrogate" entity data (existing or new, updated with dto's values).
        /// The existing entity data is retrieved from the repository, based on the entitie's identification configuration.
        /// If not exisiting entity could be retirved, a new attached (added) entity is used.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="source">The source.</param>
        public void FromDto<TDto>(TDto source)
        {
            this.Data = (TData)this.mapper.MapToEntity(source, typeof(TData));
        }

        /// <summary>
        /// Finds the entity by dto.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="surrogateDto">The surrogate dto.</param>
        public void FindByDto<TDto>(TDto surrogateDto)
        {
            this.Data = this.finder.FindByDto(typeof (TData), surrogateDto) as TData;
        }

        /// <summary>
        /// Converts to the Dto of the specified type.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <returns>The populated Dto instance.</returns>
        public TDto ToDto<TDto>() where TDto : new()
        {
            TDto dtoInstance = default(TDto);
            if (this.Data != null)
            {
                dtoInstance = (TDto)this.mapper.MapToDto(this.Data, typeof(TDto));
            }

            return dtoInstance;
        }

        /// <summary>
        /// Removes the entity from repository.
        /// </summary>
        [Obsolete]
        public void Remove()
        {
            if (!this.wasLoadedFromRepository)
            {
                if (this.Data != null)
                {
                     //this.repository.Delete(this.Data);
                }
                else
                {
                    throw new InvalidOperationException("Cannot delete this entity, it does not contain any data.");
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot delete this entity, it was not loaded from the repository.");
            }
        }

        /// <summary>
        /// Identifies the entity by provided dto data and removes it from the repository.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="surrogateDto">The surrogate dto.</param>
        /// <returns><c>True</c> in case the entity was found and succesfully removed.</returns>
        public bool RemoveByDto<TDto>(TDto surrogateDto)
        {
            var existingEntity = this.finder.FindByDto(typeof(TData), surrogateDto) as TData;
            if (existingEntity != null)
            {
                this.repositoryAccessor.RemoveEntityData(existingEntity);
                return true;
            }

            return false;
        }
        #endregion
      
    }
}
