using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.service.interfaces;
using System.Collections.Generic;

namespace storeBooks.service.models
{
    public class BooksService : IBooksService
    {
        private readonly IBooksBusiness _booksBusiness;

        public BooksService(IBooksBusiness booksBusiness)
        {
            _booksBusiness = booksBusiness;
        }

        public BookStore Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BookStore> GetAll(string currency)
        {
            return _booksBusiness.GetAll(currency);
        }

        public IEnumerable<BookStore> GetByAuthor(string author)
        {
            return _booksBusiness.GetByAuthor(author);
        }

        public BookStore GetById(int id)
        {
            return _booksBusiness.GetById(id);
        }

        public IEnumerable<BookStore> GetByTitle(string title)
        {
            return _booksBusiness.GetByTitle(title);
        }

        public BookStore Insert(BookStore book)
        {
            return _booksBusiness.Insert(book);
        }

        public BookStore Update(BookStore book)
        {
            return _booksBusiness.Update(book);
        }
    }
}
