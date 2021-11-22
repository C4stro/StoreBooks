using storeBooks.business.interfaces;
using storeBooks.domain.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.service.interfaces
{
    public interface IBooksService
    {
        BookStore GetById(int id);
        IEnumerable<BookStore> GetByTitle(string title);
        IEnumerable<BookStore> GetByAuthor(string author);
        IEnumerable<BookStore> GetAll(string currency);
        BookStore Insert(BookStore book);
        BookStore Update(BookStore book);
        BookStore Delete(int id);
    }
}
