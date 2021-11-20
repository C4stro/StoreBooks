using storeBooks.domain.models;
using storeBooks.repository.interfaces;
using System;
using System.Collections.Generic;

namespace storeBooks.repository.models
{
    public class BooksRepository : Repository<BookStore>, IBooksRepository
    {
        public IEnumerable<BookStore> GetByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public BookStore GetById(int id)
        {
            return new BookStore
            {
                Author = "Castro",
                Id = 1,
                Price = Convert.ToDecimal(15.9880),
                Status = true,
                Title = "test"
            };
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