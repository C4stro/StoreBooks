using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using storeBooks.api.Middleware;
using storeBooks.domain.models;
using storeBooks.infra.data.Dto;
using storeBooks.repository.Dto;
using Swashbuckle.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace storeBooks.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Database in Memory
            services.AddDbContext<DbContextModels>(opt => opt.UseInMemoryDatabase("BooksStoresShopping"));
            services.AddMvc();

            //Dependence on injection through middleware => Folder: Middleware
            ConfigureMiddleware(services, Configuration);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Store Books = API",
                    Version = "v1",
                    Description = "API for Buying and Listing Books",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Henrique Castro"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            //Enable CORS
            services.AddCors(cors =>
            {
                cors.AddPolicy("Privacy", options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {      

            if (env.IsDevelopment())
            {
                //Use Memory DataBase 
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DbContextModels>();

                    AddDataInMemory(context);
                }

                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Store Books");
                c.DefaultModelsExpandDepth(-1);
                c.DefaultModelExpandDepth(0);
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter();

            app.UseRouting();

            app.UseCors("Privacy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureMiddleware(IServiceCollection services, IConfiguration configuration)
        {
            services.InjectionDependencyMiddleware();
        }

        private void AddDataInMemory(DbContextModels context)
        {
            Random randNum = new Random();

            context.BooksStores.Add(new BookStoreModel { Title = "Orgulho e Preconceito", Id = 1, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Jane Austen" });
            context.BooksStores.Add(new BookStoreModel { Title = "1984", Id = 2, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "George Orwell" });
            context.BooksStores.Add(new BookStoreModel { Title = "Dom Quixote de la Mancha ", Id = 3, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Miguel de Cervantes" });
            context.BooksStores.Add(new BookStoreModel { Title = "O Pequeno Príncipe", Id = 4, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Antoine de Saint-Exupéry" });
            context.BooksStores.Add(new BookStoreModel { Title = "Dom Casmurro", Id = 5, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Machado de Assis" });
            context.BooksStores.Add(new BookStoreModel { Title = "O Bandolim do Capitão Corelli", Id = 6, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Louis de Bernières" });
            context.BooksStores.Add(new BookStoreModel { Title = "O Conde de Monte Cristo", Id = 7, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Alexandre Dumas" });
            context.BooksStores.Add(new BookStoreModel { Title = "Um Estudo em Vermelho", Id = 8, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Arthur Conan Doyle" });
            context.BooksStores.Add(new BookStoreModel { Title = "O Processo", Id = 9, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Franz Kafka" });
            context.BooksStores.Add(new BookStoreModel { Title = "Cem Anos de Solidão", Id = 10, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Gabriel García Márquez" });

            context.Sales.Add(new SalesBookStoreModel { Currency = "EUR", Customer = "MATHEUS CASTRO", DatePurchase = DateTime.Now.ToUniversalTime(), Id = 1, ValueTotalPurchaseEUR = Math.Round(Convert.ToDecimal(15.87), 2) });
            context.Sales.Add(new SalesBookStoreModel { Currency = "EUR", Customer = "CAROLINE ROSSETTI", DatePurchase = DateTime.Now.ToUniversalTime(), Id = 2, ValueTotalPurchaseEUR = Math.Round(Convert.ToDecimal(78.98), 2) });
            
            context.BooksPurchase.Add(new BooksPurchaseModel { Id = 1, IdBook = 1, IdSaleBook = 1, ValueBookEUR = Math.Round(Convert.ToDecimal(15.87), 2), Author = "Jane Austen", Title = "Orgulho e Preconceito" });
            context.BooksPurchase.Add(new BooksPurchaseModel { Id = 2, IdBook = 2, IdSaleBook = 1, ValueBookEUR = Math.Round(Convert.ToDecimal(15.87), 2), Author = "George Orwell", Title = "1984" });
            context.BooksPurchase.Add(new BooksPurchaseModel { Id = 3, IdBook = 3, IdSaleBook = 2, ValueBookEUR = Math.Round(Convert.ToDecimal(15.87), 2), Author = "Miguel de Cervantes", Title = "Dom Quixote de la Mancha" });
            context.BooksPurchase.Add(new BooksPurchaseModel { Id = 4, IdBook = 4, IdSaleBook = 2, ValueBookEUR = Math.Round(Convert.ToDecimal(15.87), 2), Author = "Antoine de Saint-Exupéry", Title = "O Pequeno Príncipe" });
            context.BooksPurchase.Add(new BooksPurchaseModel { Id = 5, IdBook = 5, IdSaleBook = 2, ValueBookEUR = Math.Round(Convert.ToDecimal(15.87), 2), Author = "Machado de Assis", Title = "Dom Casmurro" });
            context.SaveChanges();
        }
    }
}
