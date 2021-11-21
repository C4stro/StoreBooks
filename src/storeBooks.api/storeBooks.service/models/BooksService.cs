using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.service.interfaces;
using System;
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

        public IEnumerable<BookStore> GetByAuthor(string author)
        {
            return _booksBusiness.GetByAuthor(author);
        }

        public BookStore GetById(int id)
        {
            return _booksBusiness.GetById(id);
        }

        public IEnumerable<BookStore> GetByName(string description)
        {
            return _booksBusiness.GetByName(description);
        }

        public IEnumerable<BookStore> GetByTitle(string title)
        {
            return _booksBusiness.GetByTitle(title);
        }
    }
}
