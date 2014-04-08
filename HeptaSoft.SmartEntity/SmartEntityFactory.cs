using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Identification;
using HeptaSoft.SmartEntity.Mapping.Engines;

namespace HeptaSoft.SmartEntity
{
    public class SmartEntityFactory<TData> where TData : class, new()
    {
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
        
        #region Framework-Exposed Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEntity{TData}" /> class.
        /// The "exposed" constructor.
        /// </summary>
        public SmartEntityFactory()
            : this(ControlModule.OwnedResolver.Resolve<ITypeMapper>(), ControlModule.OwnedResolver.Resolve<IEntityFinder>(), ControlModule.OwnedResolver.Resolve<IRepositoryAccessor>())
        {
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEntityFactory{TData}" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="finder">The finder.</param>
        /// <param name="repositoryAccessor">The repository accessor.</param>
        internal SmartEntityFactory(ITypeMapper mapper, IEntityFinder finder, IRepositoryAccessor repositoryAccessor)
        {
            this.mapper = mapper;
            this.finder = finder;
            this.repositoryAccessor = repositoryAccessor;
        }

        /// <summary>
        /// Creates a <see cref="SmartEntity{T}"/> instance no data.
        /// </summary>
        /// <returns></returns>
        public SmartEntity<TData> CreateEmpty()
        {
            return new SmartEntity<TData>(this.mapper, this.finder, this.repositoryAccessor);
        }

        /// <summary>
        /// Creates a <see cref="SmartEntity{T}"/> instance with sepcified data.
        /// </summary>
        /// <returns>The <see cref="SmartEntity{T}"/> instnace.</returns>
        public SmartEntity<TData> Create(TData entityData)
        {
            var newInstance = new SmartEntity<TData>(this.mapper, this.finder, this.repositoryAccessor, entityData);
            return newInstance;
        }
    }
}
