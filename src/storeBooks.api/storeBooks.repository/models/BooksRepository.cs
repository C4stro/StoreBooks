using storeBooks.domain.models;
using storeBooks.repository.Dto;
using storeBooks.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace storeBooks.repository.models
{
    public class BooksRepository : Repository<BookStoreModel>, IBooksRepository
    {
        private readonly DbContextModels _context;

        public BooksRepository(DbContextModels context)
        {
            _context = context;
        }

        public IEnumerable<BookStoreModel> GetByAuthor(string author)
        {
            try
            {
                return _context.BooksStores.Where(x => x.Author.ToLower() == author.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public BookStoreModel GetById(int id)
        {
            try
            {
                return _context.BooksStores.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public IEnumerable<BookStoreModel> GetByName(string description)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookStoreModel> GetByTitle(string title)
        {
            try
            {
                return _context.BooksStores.Where(x => x.Title.ToLower() == title.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        private bool AddDataInMemory(BookStoreModel book)
        {
            try
            {
                _context.Add(new BookStoreModel { Author = book.Author, Id = 4, Price = Math.Round(Convert.ToDecimal(book.Price), 2), IsDeleted = false, Title = book.Title });

                if (_context.SaveChanges() > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }

        }
    }
}