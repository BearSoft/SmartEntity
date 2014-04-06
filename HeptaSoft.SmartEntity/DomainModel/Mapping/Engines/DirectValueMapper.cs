using System;
using HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors;
using HeptaSoft.SmartEntity.Environment.Providers;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Engines
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
        }

        /// <summary>
        /// Maps the direct value.
        /// </summary>
        /// <param name="sourceDtoInstance">The source dto instance.</param>
        /// <param name="targetPropertyAccessor">The target property accessor.</param>
        /// <param name="targetOffsetPath">The target offset path.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">There is no registered Converter for the required conversion.</exception>
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

        /// <summary>
        /// Gets the converted direct value.
        /// </summary>
        /// <param name="originalSourceValue">The original source value.</param>
        /// <param name="targetValueType">Type of the target value.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">There is no registered Converter for the required conversion.</exception>
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
    }
}
