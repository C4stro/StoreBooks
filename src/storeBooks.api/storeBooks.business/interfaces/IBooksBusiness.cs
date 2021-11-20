using storeBooks.domain.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.business.interfaces
{
    public interface IBooksBusiness
    {
        IEnumerable<BookStore> GetByName(string description);
        BookStore GetById(int id);
        IEnumerable<BookStore> GetByTitle(string title);
        IEnumerable<BookStore> GetByAuthor(string author);
    }
}
