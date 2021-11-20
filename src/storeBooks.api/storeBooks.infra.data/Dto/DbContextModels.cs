using Microsoft.EntityFrameworkCore;
using storeBooks.domain.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.repository.Dto
{
    public class DbContextModels : DbContext
    {
        public DbContextModels(DbContextOptions<DbContextModels> options)
            : base(options)
        {
        }

        public DbSet<BookStoreModel> BooksStores { get; set; }
    }
}
