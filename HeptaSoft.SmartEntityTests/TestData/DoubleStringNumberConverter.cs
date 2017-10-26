using HeptaSoft.SmartEntity.Mapping.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeptaSoft.SmartEntityTests.TestData
{
    internal class DoubleStringNumberConverter : ConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertibleConverter"/> class.
        /// </summary>
        public DoubleStringNumberConverter()
            : base(null, null)
        {
        }

        /// <inheritdoc />
        protected override object DoConvert(object value, Type requiredType)
        {
            if (requiredType == typeof(string) && value != null)
            {
                int? number = value as int?;
                number *= 2;
                return number.ToString();
            }
            else
            {
                return "Fail!";
            }
        }

        /// <inheritdoc />
        public override bool CanConvert(Type from, Type to)
        {
            return from == typeof(int) && to == typeof(string);
        }
    }
}