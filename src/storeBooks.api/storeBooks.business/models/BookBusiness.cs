using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var books = _booksRepository.GetByAuthor(author);

            if (books == null)
                return null;

            return books.Select(b => new BookStore(b.Title, b.Author, b.Price, b.Id)).ToList();
        }

        public BookStore GetById(int id)
        {
            BookStoreModel book = _booksRepository.GetById(id);

            if (book == null)
                return null;

            return new BookStore(book.Title, book.Author, book.Price, book.Id);
        }

        public IEnumerable<BookStore> GetByName(string description)
        {
            //BookStoreModel book = _booksRepository.GetByName(description);

            //if (book == null)
            //    return null;

            return null;
        }

        public IEnumerable<BookStore> GetByTitle(string title)
        {
            var books = _booksRepository.GetByTitle(title);

            if (books == null)
                return null;

            return books.Select(b => new BookStore(b.Title, b.Author, b.Price, b.Id)).ToList();
        }
    }
}
