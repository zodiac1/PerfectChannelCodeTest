using System.Collections.Generic;

namespace Persistence.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        int Update(T entity);
    }
}