using storeBooks.domain.models;
using storeBooks.repository.Dto;
using System.Collections.Generic;

namespace storeBooks.repository.interfaces
{
    public interface IBooksRepository : IRepository<BookStoreModel>
    {
        BookStoreModel GetById(int id);
        IEnumerable<BookStoreModel> GetByTitle(string title);
        IEnumerable<BookStoreModel> GetByAuthor(string author);
        int Insert(BookStoreModel obj);
        bool Update(BookStoreModel obj);
        bool Inactivate(BookStoreModel obj);
        IEnumerable<BookStoreModel> GetAllActive(string currency = null);
    }
}
