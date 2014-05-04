using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal class ValueGetter : IValueGetter
    {
        private readonly Delegate getValueDelegate;

        public ValueGetter(Delegate getValueDelegate)
        {
            this.getValueDelegate = getValueDelegate;
        }

        #region IValueGetter

        /// <inheritdoc />
        public object GetValue(object instance)
        {
            return this.getValueDelegate.DynamicInvoke(instance);
        }

        /// <inheritdoc />
        public Type ValueType
        {
            get
            {
                return this.getValueDelegate.Method.ReturnType;
            }
        }

        #endregion
    }
}
