using System.Collections.Generic;

namespace storeBooks.repository.interfaces
{
    public interface IRepository<T>
    {
        int Insert(T obj);
        bool Update(T obj);
        bool Inactivate(T obj);
        IEnumerable<T> GetAllActive();
    }
}