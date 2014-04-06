namespace HeptaSoft.SmartEntity.DataAccess
{
    public interface IDataSet<T>
    {
        void Add(T entityInstance);

        T AddNew();

        void Remove(T entityInstance);
    }

    public interface IDataSet
    {
        void Add(object entityInstance);

        object AddNew();

        void Remove(object entityInstance);
    }
}
