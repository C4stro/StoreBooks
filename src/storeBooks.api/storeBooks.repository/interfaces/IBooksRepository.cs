using storeBooks.domain.models;
using System.Collections.Generic;

namespace storeBooks.repository.interfaces
{
    public interface IBooksRepository : IRepository<BookStore>
    {
        IEnumerable<BookStore> GetByName(string description);
        BookStore GetById(int id);
        IEnumerable<BookStore> GetByTitle(string title);
        IEnumerable<BookStore> GetByAuthor(string author);
    }
}
