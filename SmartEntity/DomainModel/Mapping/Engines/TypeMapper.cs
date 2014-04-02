using System;
using HeptaSoft.SmartEntity.Basic;
using HeptaSoft.SmartEntity.DomainModel.Identification;
using HeptaSoft.SmartEntity.Environment.Providers;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Engines
{
    internal class TypeMapper : ITypeMapper
    {
        /// <summary>
        /// The maximum difference between current depth in source and current depth in target, while mapping.
        /// </summary>
        private const int MaxDepthDelta = 1;

        /// <summary>
        /// The property accessors provider.
        /// </summary>
        private readonly IPropertyAccessorsProvider propertyAccessorsProvider;

        /// <summary>
        /// The mapping manager.
        /// </summary>
        private readonly IMappingsManager mappingsManager;

        /// <summary>
        /// The entity finder.
        /// </summary>
        private readonly IEntityFinder entityFinder;

        /// <summary>
        /// The direct value mapper.
        /// </summary>
        private readonly IDirectValueMapper directValueMapper;

        /// <summary>
        /// The entity data factory.
        /// </summary>
        private readonly IRepositoryAccessor repositoryAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMapper" /> class.
        /// </summary>
        /// <param name="propertyAccessorsProvider">The property accessors provider.</param>
        /// <param name="mappingsManager">The mapping manager.</param>
        /// <param name="entityFinder">The entity finder.</param>
        /// <param name="directValueMapper">The direct value mapper.</param>
        /// <param name="repositoryAccessor">The entity data factory.</param>
        public TypeMapper(IPropertyAccessorsProvider propertyAccessorsProvider, IMappingsManager mappingsManager, IEntityFinder entityFinder, IDirectValueMapper directValueMapper, IRepositoryAccessor repositoryAccessor)
        {
            this.propertyAccessorsProvider = propertyAccessorsProvider;
            this.mappingsManager = mappingsManager;
            this.entityFinder = entityFinder;
            this.directValueMapper = directValueMapper;
            this.repositoryAccessor = repositoryAccessor;
        }

        /// <summary>
        /// Maps the specified dto to required target type.
        /// If the target is an entity, repository retrieval will be tried first.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>The new source instance (new or existing) updated with the Dto provided values.</returns>
        public object MapToEntity(object sourceInstance, Type targetType)
        {
            return this.MapToEntity(sourceInstance, targetType, false);
        }

        /// <summary>
        /// Maps the entity to dto.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>The dto instance.</returns>
        public object MapToDto(object sourceInstance, Type targetType)
        {
            return this.MapToDto(sourceInstance, targetType, null, 0, 0);
        }

        /// <summary>
        /// Maps the specified dto to required target type.
        /// If the target is an entity and <paramref name="disableSelfIdentification"/> is set to <c>false</c>, repository retrieval will be tried first.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="disableSelfIdentification">if set to <c>true</c> [disable self identification].</param>
        /// <returns>The new source instance (new or existing) updated with the Dto provided values.</returns>
        public object MapToEntity(object sourceInstance, Type targetType, bool disableSelfIdentification)
        {
            return this.MapToEntity(sourceInstance, targetType, null, disableSelfIdentification, 1, 1);
        }

        /// <summary>
        /// Updates all the properties in the target with the values of the corresponding properties from the provided source.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetInstance">The target instance.</param>
        public void MapToEntity(object sourceInstance, object targetInstance)
        {
            this.Map(sourceInstance, targetInstance, null, 1, 1, MappingDirection.FromDtoToEntity);
        }

        /// <summary>
        /// Maps the specified source instance to a new target instance.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="targetPropertyParent">The target property parent.</param>
        /// <param name="disableSelfIdentification">if set to <c>true</c> [disable self identification].</param>
        /// <param name="sourceDepthLevel">The source depth level.</param>
        /// <param name="targetDepthLevel">The target depth level.</param>
        /// <returns>
        /// The new source instance (new or existing) updated with the Dto provided values.
        /// </returns>
        private object MapToEntity(object sourceInstance, Type targetType, PropertyPath targetPropertyParent, bool disableSelfIdentification, int sourceDepthLevel, int targetDepthLevel)
        {
            // 1. Obtain a new instance for the Target: find it in the repository or create a new one
            object targetInstance = null;
            bool anythingMapped = false;
            bool newEntityDataInstance = false;

            if (!disableSelfIdentification)
            {
                targetInstance = this.entityFinder.FindByDto(targetType, sourceInstance, targetPropertyParent);
            }
            
            if (targetInstance == null)
            {
                targetInstance = this.repositoryAccessor.CreateEntityData(targetType);
                newEntityDataInstance = true;
            }

            // 2. Populate all the properties from the provided source dto.
            anythingMapped = this.Map(sourceInstance, targetInstance, targetPropertyParent, sourceDepthLevel, targetDepthLevel, MappingDirection.FromDtoToEntity);

            if ((!newEntityDataInstance) || anythingMapped)
            {
                return targetInstance;
            }
            else
            {
                // the entity instance was created by mapping process but nothing was mapped. Need to undo the creation.
                this.repositoryAccessor.RemoveEntityData(targetInstance);
                return null;
            }
        }

        private object MapToDto(object sourceInstance, Type targetType, PropertyPath targetPropertyParent, int sourceDepthLevel, int targetDepthLevel)
        {
            // 1. Obtain a new instance for the Target: find it in the repository or create a new one
            object targetInstance = null;
            bool anythingMapped = false;

            targetInstance = ReflectionHelper.CreateNewInstance(targetType);
            
            // 2. Populate all the properties from the provided source dto.
            anythingMapped = this.Map(sourceInstance, targetInstance, targetPropertyParent, sourceDepthLevel, targetDepthLevel, MappingDirection.FromEntityToDto);

            if (anythingMapped)
            {
                return targetInstance;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Populates the properties of the target with values from the source.
        /// </summary>
        /// <param name="sourceInstance">The source instance.</param>
        /// <param name="targetInstance">The target instance.</param>
        /// <param name="targetPropertyParent">The target property parent.</param>
        /// <param name="sourceDepthLevel">The source depth level.</param>
        /// <param name="targetDepthLevel">The target depth level.</param>
        /// <param name="mappingDirrection">The mapping dirrection.</param>
        /// <returns>
        ///   <c>True</c> in case any mapping have been done (any information from source caused an effect on destination).
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Mapping cannot be done; The mapping configuration is invalid.</exception>
        private bool Map(object sourceInstance, object targetInstance, PropertyPath targetPropertyParent, int sourceDepthLevel, int targetDepthLevel, MappingDirection mappingDirrection)
        {
            bool somethingMapped = false;

            // execute mapping process only if we are not too far in depth from one operand to the other (operands = source / target)
            if (Math.Abs(sourceDepthLevel-targetDepthLevel)<= MaxDepthDelta)
            {
                var targetProperties = this.propertyAccessorsProvider.GetPropertyAccessors(targetInstance.GetType());

                foreach (var targetProperty in targetProperties)
                {
                    var targetPropertyPath = new PropertyPath(targetPropertyParent, targetProperty.PropertyName);
                    var mapping = this.mappingsManager.GetMapping(targetProperty.DtoType, sourceInstance.GetType(), targetPropertyPath);
                    if (mapping != null)
                    {
                        var sourceValue = mapping.SourceValueGetter.GetValue(sourceInstance);
                        if (ReflectionHelper.IsEmptyOrNull(sourceValue))
                        {
                            targetProperty.SetValue(targetInstance, null);
                        }
                        else
                        {
                            if (ReflectionHelper.IsDirectValue(sourceValue.GetType()) && ReflectionHelper.IsDirectValue(targetProperty.ValueType))
                            {
                                // DirectValue to DirectValue mapping
                                var convertedValue = this.directValueMapper.GetConvertedDirectValue(sourceValue, targetProperty.ValueType);
                                targetProperty.SetValue(targetInstance, convertedValue);
                                somethingMapped = true;
                            }
                            else if (!ReflectionHelper.IsDirectValue(sourceValue.GetType()) && !ReflectionHelper.IsDirectValue(targetProperty.ValueType))
                            {
                                // Dto to Dto mapping
                                object targetPropertyValue = null;
                                if (mappingDirrection == MappingDirection.FromDtoToEntity)
                                {
                                    targetPropertyValue = this.MapToEntity(sourceValue, targetProperty.ValueType, null, false, sourceDepthLevel++, targetDepthLevel++);
                                }
                                else
                                {
                                    targetPropertyValue = this.MapToDto(sourceValue, targetProperty.ValueType, null, sourceDepthLevel++, targetDepthLevel++);
                                }

                                if (targetPropertyValue!= null)
                                {
                                    somethingMapped = true;
                                }
 
                                targetProperty.SetValue(targetInstance, targetPropertyValue);
                            }
                            else
                            {
                                throw new InvalidOperationException(string.Format("Cannot map source type <{0}> to destination type <{1}>: the mapping for target property {2}.{3} is invalid (dto type to direct value type).", sourceValue.GetType(), targetProperty.ValueType, targetProperty.DtoType, targetProperty.PropertyName));
                            }
                        }
                    }
                    else
                    {
                        // there is no source property matching this target one. 
                        if (!ReflectionHelper.IsDirectValue(targetProperty.ValueType))
                        {
                            // The target is a DTO and has no omolougue property in source: map this new Dto(the target property value) against the same source instance (there might be flaterned properties in the source).
                            var thisTargetPropertyPath = new PropertyPath(targetProperty.PropertyName);
                            
                            object targetPropertyValue = null;
                            if (mappingDirrection == MappingDirection.FromDtoToEntity)
                            {
                                targetPropertyValue = this.MapToEntity(sourceInstance, targetProperty.ValueType,thisTargetPropertyPath, false, sourceDepthLevel, targetDepthLevel++);
                            }
                            else
                            {
                                targetPropertyValue = this.MapToDto(sourceInstance, targetProperty.ValueType, thisTargetPropertyPath, sourceDepthLevel, targetDepthLevel++);
                            }

                            if (targetPropertyValue != null)
                            {
                                somethingMapped = true;
                            }

                            targetProperty.SetValue(targetInstance, targetPropertyValue);
                        }
                        else
                        {
                            // the target is direct value and has no corresponing property in source. This property cannot be mapped
                        }
                    }
                }
            }

            return somethingMapped;
        }

        private enum MappingDirection
        {
            FromDtoToEntity,
            FromEntityToDto
        }
    }
}
