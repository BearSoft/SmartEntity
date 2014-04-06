using System;

namespace HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors
{
    internal interface IValueGetter
    {
        object GetValue(object instance);

        Type ValueType { get; }
    }
}
