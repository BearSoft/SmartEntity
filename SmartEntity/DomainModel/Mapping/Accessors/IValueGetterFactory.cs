using System;

namespace SmartEntity.DomainModel.Mapping.Accessors
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