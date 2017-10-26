using HeptaSoft.SmartEntity.Environment.Providers;
using HeptaSoft.SmartEntity.Mapping.Accessors;
using HeptaSoft.SmartEntity.Mapping.Conversion;
using System;

namespace HeptaSoft.SmartEntity.Mapping.Engines
{
    internal class DirectValueMapper : IDirectValueMapper
    {
        /// <summary>
        /// The converter stack.
        /// </summary>
        private readonly IConverterStack converterStack;

        /// <summary>
        /// The mappings manager.
        /// </summary>
        private readonly IMappingsManager mappingsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectValueMapper" /> class.
        /// </summary>
        /// <param name="converterStack">The converter stack.</param>
        /// <param name="mappingsManager">The mappings manager.</param>
        public DirectValueMapper(IConverterStack converterStack, IMappingsManager mappingsManager)
        {
            this.converterStack = converterStack;
            this.mappingsManager = mappingsManager;

            // Register default base converters
            this.converterStack.PushConverter(new ConvertibleConverter(), new SameTypeConverter());
        }

        #region IDirectValueMapper

        /// <inheritdoc />
        public object GetConvertedDirectValue(object sourceDtoInstance, IPropertyAccessor targetPropertyAccessor, PropertyPath targetOffsetPath)
        {
            var targetPropertyPath = new PropertyPath(targetOffsetPath, targetPropertyAccessor.PropertyName);
            var mapping = this.mappingsManager.GetMapping(
                targetPropertyAccessor.DtoType,
                sourceDtoInstance.GetType(),
                targetPropertyPath);

            if (mapping != null)
            {
                var sourceValue = mapping.SourceValueGetter.GetValue(sourceDtoInstance);
                return this.GetConvertedDirectValue(sourceValue, targetPropertyAccessor.ValueType);
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc />
        public object GetConvertedDirectValue(object originalSourceValue, Type targetValueType)
        {
            object convertedSourceValue = null;

            if (originalSourceValue != null)
            {
                var sourceValueType = originalSourceValue.GetType();

                if (sourceValueType != targetValueType)
                {
                    var topConverter = this.converterStack.GetTopMatchingConverter(sourceValueType, targetValueType);
                    if (topConverter != null)
                    {
                        convertedSourceValue = topConverter.ConvertTo(originalSourceValue, targetValueType);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                "Cannot set-up the conversion delegate in the source value getter: there is no registered Converter that can convert from <{0}> to <{1}>.",
                                sourceValueType, targetValueType));
                    }
                }
                else
                {
                    convertedSourceValue = originalSourceValue;
                }
            }

            return convertedSourceValue;
        }

        #endregion
    }
}
