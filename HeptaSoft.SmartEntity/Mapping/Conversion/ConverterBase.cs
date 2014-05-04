using System;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.Mapping.Conversion
{
    public abstract class ConverterBase : IConverter
    {
        /// <summary>
        /// The possible input types.
        /// </summary>
        private readonly ICollection<Type> possibleInputTypes;

        /// <summary>
        /// The possible input types.
        /// </summary>
        private readonly ICollection<Type> possibleOutputTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterBase" /> class.
        /// </summary>
        /// <param name="possibleInputTypes">The possible input types.</param>
        /// <param name="possibleOutputTypes">The possible output types.</param>
        protected ConverterBase(ICollection<Type> possibleInputTypes, ICollection<Type> possibleOutputTypes)
        {
            this.possibleInputTypes = possibleInputTypes;
            this.possibleOutputTypes = possibleOutputTypes;
        }

        /// <summary>
        /// When override, does the actual conversion of provided value to the required type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="requiredType">Type of the required.</param>
        /// <returns>The converted object.</returns>
        protected abstract object DoConvert(object value, Type requiredType);

        #region IConverter

        /// <inheritdoc />
        public object ConvertTo(object value, Type requiredType)
        {
            if (value != null)
            {
                if (this.CanConvert(value.GetType(), requiredType))
                {
                    try
                    {
                        if (value.GetType() == requiredType)
                        {
                            return value;
                        }
                        else
                        {
                            return this.DoConvert(value, requiredType);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(string.Format("The {0} cannot convert <{1}> to <{2}>: {3}", this.GetType(), value.GetType(), requiredType, ex.Message));
                    }
                }
                else
                {
                    throw new InvalidOperationException(string.Format("This converter cannot convert to <{0}>.", requiredType));
                }
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc />
        public virtual bool CanConvert(Type from, Type to)
        {
            return (this.possibleInputTypes.Contains(from)) && (this.possibleOutputTypes.Contains(to));
        }

        #endregion
    }
}
