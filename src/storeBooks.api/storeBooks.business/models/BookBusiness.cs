using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.business.models
{
    public class BookBusiness : IBooksBusiness
    {
        private readonly IBooksRepository _booksRepository;

        public BookBusiness(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEnumerable<BookStore> GetByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public BookStore GetById(int id)
        {
            return _booksRepository.GetById(id);
        }

        public IEnumerable<BookStore> GetByName(string description)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookStore> GetByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
