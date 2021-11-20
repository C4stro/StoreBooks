using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using storeBooks.api.Middleware;
using storeBooks.domain.models;
using storeBooks.repository.Dto;
using System;

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

            context.Add(new BookStoreModel { Author = "Orgulho e Preconceito", Id = 1, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Jane Austen" });
            context.Add(new BookStoreModel { Author = "1984", Id = 2, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "George Orwell" });
            context.Add(new BookStoreModel { Author = "Dom Quixote de la Mancha ", Id = 3, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Miguel de Cervantes" });
            context.Add(new BookStoreModel { Author = "O Pequeno Príncipe", Id = 4, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Antoine de Saint-Exupéry" });
            context.Add(new BookStoreModel { Author = "Dom Casmurro", Id = 5, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Machado de Assis" });
            context.Add(new BookStoreModel { Author = "O Bandolim do Capitão Corelli", Id = 6, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Louis de Bernières" });
            context.Add(new BookStoreModel { Author = "O Conde de Monte Cristo", Id = 7, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Alexandre Dumas" });
            context.Add(new BookStoreModel { Author = "Um Estudo em Vermelho", Id = 8, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Arthur Conan Doyle" });
            context.Add(new BookStoreModel { Author = "O Processo", Id = 9, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Franz Kafka" });
            context.Add(new BookStoreModel { Author = "Cem Anos de Solidão", Id = 10, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Title = "Gabriel García Márquez" });

            context.SaveChanges();
        
        }
    }
}
