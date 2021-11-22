using storeBooks.repository.entities;

namespace storeBooks.repository.interfaces
{
    public interface IRepository<T>
    {
        public ExchangeValues ExchangeLatest();
    }
}