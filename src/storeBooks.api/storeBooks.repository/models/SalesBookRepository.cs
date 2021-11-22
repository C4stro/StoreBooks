using storeBooks.domain.models;
using storeBooks.infra.data.Dto;
using storeBooks.repository.Dto;
using storeBooks.repository.interfaces;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using storeBooks.repository.entities;

namespace storeBooks.repository.models
{
    public class SalesBookRepository : Repository<SalesBookStoreModel>, ISalesBookRepository
    {
        private readonly DbContextModels _context;

        public SalesBookRepository(DbContextModels context)
        {
            _context = context;
        }

        private decimal ConvertValueInEUR(ExchangeValues values, string currency, decimal price)
        {
            try
            {
                //Convert values for EUR
                if (currency == "EUR")
                    return price;

                if(currency == "USD")
                    return Math.Round(price / Convert.ToDecimal(values.Rates.USD), 2);

                if (currency == "BRL")
                    return Math.Round(price / Convert.ToDecimal(values.Rates.BRL), 2);

                if (currency == "GBP")
                    return Math.Round(price / Convert.ToDecimal(values.Rates.GBP), 2);

                return price;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        private bool ValidObjectSales(SalesBook sale)
        {
            try
            {
                if(String.IsNullOrEmpty(sale.Customer))
                    throw new Exception($"Customer cannot be null");

                if (sale.ValueTotalPurchase <= 0)
                    throw new Exception($"ValueTotalPurchase cannot be null");

                if (String.IsNullOrEmpty(sale.DatePurchase.ToString()))
                    throw new Exception($"DatePurchase cannot be null");

                foreach (var item in sale.Books)
                {
                    var book = _context.BooksStores.FirstOrDefault(x => x.Id == item.Id);

                    if (book == null)
                        throw new Exception($"Exception: No book is recorded for ID {item.Id}");

                    if(item.Price <= 0)
                        throw new Exception($"Exception: Book with ID {item.Id} with value 0, not permited");

                    if (String.IsNullOrEmpty(item.Author))
                        throw new Exception($"Exception: Author is null for Book with id {item.Id}, not permited");

                    if (String.IsNullOrEmpty(item.Title))
                        throw new Exception($"Exception: Title is null for Book with id {item.Id}, not permited");

                    if (String.IsNullOrEmpty(item.Currency))
                        throw new Exception($"Exception: Currency is null for Book with id {item.Id}, not permited");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SalesBook BuyMade(SalesBook sale, string currency)
        {
            try
            {
                ValidObjectSales(sale);

                ExchangeValues exchanges = ExchangeLatest();

                int? id = _context.Sales.Max(x => x.Id) + 1;
                
                _context.Add(
                    new SalesBookStoreModel 
                    { 
                        Id = id, 
                        Currency = 
                        sale.Currency, 
                        Customer = sale.Customer, 
                        DatePurchase = sale.DatePurchase, 
                        ValueTotalPurchaseEUR = ConvertValueInEUR(exchanges, currency, sale.ValueTotalPurchase)
                    });

                if (_context.SaveChanges() == 0)
                    throw new Exception($"Exception: Error in insert DataBase, try again later");

                int? idBookPurchase = _context.BooksPurchase.Max(x => x.Id);

                foreach(var item in sale.Books)
                {
                    idBookPurchase += 1;

                    _context.BooksPurchase.Add(
                        new BooksPurchaseModel 
                        { 
                            Id = idBookPurchase, 
                            IdBook = item.Id, 
                            IdSaleBook = id, 
                            ValueBookEUR = ConvertValueInEUR(exchanges, currency, item.Price) * exchanges.Rates.EUR, 
                            ValueBookBRL = ConvertValueInEUR(exchanges, currency, item.Price) * Convert.ToDecimal(exchanges.Rates.BRL), 
                            ValueBookGBP = ConvertValueInEUR(exchanges, currency, item.Price) * Convert.ToDecimal(exchanges.Rates.GBP),
                            ValueBookUSD = ConvertValueInEUR(exchanges, currency, item.Price) * Convert.ToDecimal(exchanges.Rates.USD),
                            Author = item.Author,
                            Title = item.Title
                        });
                }

                if (_context.SaveChanges() > 0)
                    return sale;

                return null;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public SalesBookStoreModel[] TakeAllShopping()
        {
            try
            {
                var sales = _context.Sales.Include(s => s.BooksPurchase).ToArray();
                var books = _context.BooksPurchase.ToList();

                sales.Select(b => new SalesBookStoreModel
                {
                    Currency = b.Currency,
                    Customer = b.Customer,
                    DatePurchase = b.DatePurchase,
                    ValueTotalPurchaseEUR = b.ValueTotalPurchaseEUR,
                    Id = b.Id,
                    BooksPurchase = b.BooksPurchase.Select(bb => bb).ToList()
                });

                return FillExchanges(sales);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        private SalesBookStoreModel[] FillExchanges(SalesBookStoreModel[] bookSales)
        {
            try
            {
                ExchangeValues exchanges = ExchangeLatest();

                foreach(var item in bookSales)
                {
                    foreach(var books in item.BooksPurchase)
                    {
                        books.ValueBookBRL = Math.Round(books.ValueBookEUR * Convert.ToDecimal(exchanges.Rates.BRL), 2);
                        books.ValueBookEUR = Math.Round(books.ValueBookEUR * Convert.ToDecimal(exchanges.Rates.EUR), 2);
                        books.ValueBookGBP = Math.Round(books.ValueBookEUR * Convert.ToDecimal(exchanges.Rates.GBP), 2);
                        books.ValueBookUSD = Math.Round(books.ValueBookEUR * Convert.ToDecimal(exchanges.Rates.USD), 2);
                    }
                }

                return bookSales;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}
