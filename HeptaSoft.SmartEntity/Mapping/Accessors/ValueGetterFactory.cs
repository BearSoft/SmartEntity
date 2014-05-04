using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal class ValueGetterFactory : IValueGetterFactory
    {
        #region IValueGetterFactory

        /// <inheritdoc />
        public IValueGetter CreateValueGetter(Delegate getValueDelegate)
        {
            return new ValueGetter(getValueDelegate);
        }

        #endregion
    }
}
