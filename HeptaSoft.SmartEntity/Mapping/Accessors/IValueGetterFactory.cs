using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal interface IValueGetterFactory
    {
        /// <summary>
        /// Creates the value getter.
        /// </summary>
        /// <param name="getValueDelegate">The get value delegate.</param>
        /// <returns></returns>
        IValueGetter CreateValueGetter( Delegate getValueDelegate);
    }
}