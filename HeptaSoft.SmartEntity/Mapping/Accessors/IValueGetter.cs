using System;

namespace HeptaSoft.SmartEntity.Mapping.Accessors
{
    internal interface IValueGetter
    {
        object GetValue(object instance);

        Type ValueType { get; }
    }
}
