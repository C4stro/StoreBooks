using Microsoft.EntityFrameworkCore;
using storeBooks.infra.data.Dto;

namespace storeBooks.repository.Dto
{
    public class DbContextModels : DbContext
    {
        public DbContextModels(DbContextOptions<DbContextModels> options)
            : base(options)
        {
        }

        public DbSet<BookStoreModel> BooksStores { get; set; }
        public DbSet<SalesBookStoreModel> Sales { get; set; }
        public DbSet<BooksPurchaseModel> BooksPurchase { get; set; }
    }
}
