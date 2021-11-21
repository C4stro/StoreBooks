using storeBooks.domain.models;
using System.Collections.Generic;

namespace storeBooks.repository.interfaces
{
    public interface IBooksRepository : IRepository<BookStoreModel>
    {
        IEnumerable<BookStoreModel> GetByName(string description);
        BookStoreModel GetById(int id);
        IEnumerable<BookStoreModel> GetByTitle(string title);
        IEnumerable<BookStoreModel> GetByAuthor(string author);
    }
}
