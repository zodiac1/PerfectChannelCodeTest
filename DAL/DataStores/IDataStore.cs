using System.Collections.Generic;

namespace DataAccess.DataStores
{
    public interface IDataStore<T>
    {
        string Id { get; set; }

        IEnumerable<T> GetAll();

        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        void Save(T entity);
        void Refresh();
        void Reset();
        int Count();

        T Find(object value);
    }
}