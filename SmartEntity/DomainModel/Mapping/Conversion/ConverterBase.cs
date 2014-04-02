using System;
using System.Collections.Generic;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Conversion
{
    public abstract class ConverterBase : IConverter
    {
        /// <summary>
        /// The possible input types.
        /// </summary>
        private readonly ICollection<Type> _possibleInputTypes;

        /// <summary>
        /// The possible input types.
        /// </summary>
        private readonly ICollection<Type> _possibleOutputTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterBase" /> class.
        /// </summary>
        /// <param name="possibleInputTypes">The possible input types.</param>
        /// <param name="possibleOutputTypes">The possible output types.</param>
        protected ConverterBase(ICollection<Type> possibleInputTypes, ICollection<Type> possibleOutputTypes)
        {
            this._possibleInputTypes = possibleInputTypes;
            this._possibleOutputTypes = possibleOutputTypes;
        }

        /// <summary>
        /// Converts the specified value to required type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="requiredType">Type of the required.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
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

        /// <summary>
        /// Determines whether this instance can convert from <paramref name="from"/> type to <paramref name="to"/> type.
        /// </summary>
        /// <param name="from">Source type</param>
        /// <param name="to">Required output type.</param>
        /// <returns>
        ///   <c>true</c> if this instance can perform the conversion between specified types; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanConvert(Type @from, Type to)
        {
            return (this._possibleInputTypes.Contains(@from)) && (this._possibleOutputTypes.Contains(to));
        }

        /// <summary>
        /// When overrides, does the actual conversion convertion of provided value to the required type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="requiredType">Type of the required.</param>
        /// <returns></returns>
        protected abstract object DoConvert(object value, Type requiredType);
    }
}
