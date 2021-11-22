using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.repository.Dto;
using storeBooks.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace storeBooks.business.models
{
    public class BookBusiness : IBooksBusiness
    {
        private readonly IBooksRepository _booksRepository;

        public BookBusiness(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEnumerable<BookStore> GetAll(string currency)
        {
            var books = _booksRepository.GetAllActive(currency);
            
            if (String.IsNullOrEmpty(currency))
                currency = "EUR";

            if (books == null)
                return null;

            return books.Select(b => new BookStore(b.Title, b.Author, b.Price, b.Id, currency)).ToList();
        }

        public IEnumerable<BookStore> GetByAuthor(string author)
        {
            var books = _booksRepository.GetByAuthor(author);

            if (books == null)
                return null;

            return books.Select(b => new BookStore(b.Title, b.Author, b.Price, b.Id, "EUR")).ToList();
        }

        public BookStore GetById(int id)
        {
            BookStoreModel book = _booksRepository.GetById(id);

            if (book == null)
                return null;

            return new BookStore(book.Title, book.Author, book.Price, book.Id, "EUR");
        }

        public IEnumerable<BookStore> GetByTitle(string title)
        {
            var books = _booksRepository.GetByTitle(title);

            if (books == null)
                return null;

            return books.Select(b => new BookStore(b.Title, b.Author, b.Price, b.Id, "EUR")).ToList();
        }

        private BookStoreModel CreateEntity(BookStore bookStore)
        {
            BookStoreModel book = new BookStoreModel();

            book.Id = bookStore.Id;
            book.Title = bookStore.Title;
            book.Author = bookStore.Author;
            book.Price = bookStore.Price;

            return book;
        }

        public BookStore Insert(BookStore bookStore)
        {
            string resultValidObject = ValidObjectAndValues(bookStore);

            if (!String.IsNullOrEmpty(resultValidObject))
                throw new Exception($"Exception: {resultValidObject}");

            BookStoreModel book = new BookStoreModel();
            book = CreateEntity(bookStore);

            var result = _booksRepository.Insert(book);

            if (result == 0)
                return null;

            return new BookStore(book.Title, book.Author, book.Price, result, "EUR");
        }

        public BookStore Update(BookStore bookStore)
        {
            string resultValidObject = ValidObjectAndValues(bookStore);

            if (!String.IsNullOrEmpty(resultValidObject))
                throw new Exception($"Exception: {resultValidObject}");

            bool result = _booksRepository.Update(CreateEntity(bookStore));

            if (result)
                return bookStore;

            return null;
        }

        private string ValidObjectAndValues(BookStore book)
        {
            string result = string.Empty;

            try
            {
                if (book.Price <= 0)
                    result += $"Price do book is smaller that 0, Only values ​​greater than 0 are allowed | ";
                
                if(String.IsNullOrEmpty(book.Author))
                    result += $"Author cannot be empty or null |";

                if (String.IsNullOrEmpty(book.Title))
                    result += $"Title cannot be empty or null |";

                return result;
            } 
            catch(Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}
