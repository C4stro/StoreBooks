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

            context.Add(new BookStoreModel { Title = "Orgulho e Preconceito", Id = 1, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Jane Austen" });
            context.Add(new BookStoreModel { Title = "1984", Id = 2, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "George Orwell" });
            context.Add(new BookStoreModel { Title = "Dom Quixote de la Mancha ", Id = 3, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Miguel de Cervantes" });
            context.Add(new BookStoreModel { Title = "O Pequeno Pr�ncipe", Id = 4, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Antoine de Saint-Exup�ry" });
            context.Add(new BookStoreModel { Title = "Dom Casmurro", Id = 5, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Machado de Assis" });
            context.Add(new BookStoreModel { Title = "O Bandolim do Capit�o Corelli", Id = 6, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Louis de Berni�res" });
            context.Add(new BookStoreModel { Title = "O Conde de Monte Cristo", Id = 7, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Alexandre Dumas" });
            context.Add(new BookStoreModel { Title = "Um Estudo em Vermelho", Id = 8, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Arthur Conan Doyle" });
            context.Add(new BookStoreModel { Title = "O Processo", Id = 9, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Franz Kafka" });
            context.Add(new BookStoreModel { Title = "Cem Anos de Solid�o", Id = 10, Price = Math.Round(Convert.ToDecimal(randNum.Next(99) + randNum.NextDouble()), 2), IsDeleted = false, Author = "Gabriel Garc�a M�rquez" });

            context.SaveChanges();
        
        }
    }
}
