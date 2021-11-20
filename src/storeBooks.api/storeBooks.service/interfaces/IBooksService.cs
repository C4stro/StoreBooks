using storeBooks.business.interfaces;
using storeBooks.domain.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.service.interfaces
{
    public interface IBooksService
    {
        IEnumerable<BookStore> GetByName(string description);
        BookStore GetById(int id);
        IEnumerable<BookStore> GetByTitle(string title);
        IEnumerable<BookStore> GetByAuthor(string author);
    }
}
