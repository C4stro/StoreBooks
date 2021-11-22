using storeBooks.repository.Dto;
using storeBooks.repository.entities;
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

        public IEnumerable<BookStoreModel> GetAllActive(string currency = null)
        {
            try
            {
                if(currency == null || currency == "EUR")
                    return _context.BooksStores.Where(x => x.IsDeleted == false);

                ExchangeValues valuesExchange = ExchangeLatest();

                IEnumerable<BookStoreModel> listBooks = _context.BooksStores.Where(x => x.IsDeleted == false);

                return ConvertBooksExchangeCurrency(valuesExchange, listBooks, currency);

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        private decimal GetRateCurrency(string currency, ExchangeValues valuesExchange)
        {
            try
            {
                decimal percentConversion = 1;
                
                if (currency == "USD")
                    return Convert.ToDecimal(valuesExchange.Rates.USD);

                if (currency == "GBP")
                    return Convert.ToDecimal(valuesExchange.Rates.GBP);

                if (currency == "BRL")
                    return Convert.ToDecimal(valuesExchange.Rates.BRL);

                return percentConversion;

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public IEnumerable<BookStoreModel> ConvertBooksExchangeCurrency(ExchangeValues valuesExchange, IEnumerable<BookStoreModel> listBooks, string currency)
        {
            try
            {
                decimal factor = GetRateCurrency(currency, valuesExchange);

                foreach (var item in listBooks)
                {
                    item.Price = Math.Round(item.Price * factor, 2);
                }

                return listBooks;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
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

        public bool Inactivate(BookStoreModel obj)
        {
            return Update(obj);
        }

        public int Insert(BookStoreModel obj)
        {
            try
            {
                int? id = _context.BooksStores.Max(x => x.Id) + 1;

                _context.Add(new BookStoreModel { Author = obj.Author, Id = id, Price = Math.Round(Convert.ToDecimal(obj.Price), 2), IsDeleted = false, Title = obj.Title });

                if (_context.SaveChanges() > 0)
                    return Convert.ToInt32(id);

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public bool Update(BookStoreModel obj)
        {
            try
            {
                if (_context.BooksStores.Where(x => x.Id == obj.Id) == null)
                    throw new Exception("No Records for this book");

                _context.BooksStores.Update(obj);

                if(_context.SaveChanges() > 0)
                    return true;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}