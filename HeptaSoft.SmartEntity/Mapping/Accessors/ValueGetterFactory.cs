using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal class ValueGetterFactory : IValueGetterFactory
    {
        /// <summary>
        /// Creates the value getter.
        /// </summary>
        /// <param name="getValueDelegate">The get value delegate.</param>
        /// <returns></returns>
        public IValueGetter CreateValueGetter(Delegate getValueDelegate)
        {
            return new ValueGetter(getValueDelegate);
        }
    }
}
