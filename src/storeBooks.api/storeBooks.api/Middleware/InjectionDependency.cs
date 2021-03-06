using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using storeBooks.business.interfaces;
using storeBooks.business.models;
using storeBooks.repository.Dto;
using storeBooks.repository.interfaces;
using storeBooks.repository.models;
using storeBooks.service.interfaces;
using storeBooks.service.models;
using System;
using System.Data.Entity.Infrastructure;

namespace storeBooks.api.Middleware
{
    public static class InjectionDependency
    {
        public static void InjectionDependencyMiddleware(this IServiceCollection service)
        {
            service.AddScoped<IBooksRepository, BooksRepository>();
            service.AddTransient<IBooksBusiness, BookBusiness>();
            service.AddTransient<IBooksService, BooksService>();

            service.AddScoped<ISalesBookRepository, SalesBookRepository>();
            service.AddTransient<ISalesBooksBusiness, SalesBooksBusiness>();
            service.AddTransient<ISalesBookService, SalesBookService>();

            service.AddDbContext<DbContextModels>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()), ServiceLifetime.Scoped);
        }
    }
}
