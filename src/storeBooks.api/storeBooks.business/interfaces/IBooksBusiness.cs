using storeBooks.domain.models;
using System.Collections.Generic;

namespace storeBooks.business.interfaces
{
    public interface IBooksBusiness
    {
        BookStore GetById(int id);
        IEnumerable<BookStore> GetByTitle(string title);
        IEnumerable<BookStore> GetByAuthor(string author);
        IEnumerable<BookStore> GetAll(string currency = null);
        BookStore Insert(BookStore bookStore);
        BookStore Update(BookStore bookStore);
    }
}
