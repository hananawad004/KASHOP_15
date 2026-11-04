
using KASHOP.BLL;
using KASHOP.BLL.Settings;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KASHOP.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.Configure<RequestLocalizationOptions>(options => {
                options.SetDefaultCulture(SupportedLanguges.Default);
                options.AddSupportedCultures(SupportedLanguges.Languages);
                options.AddSupportedUICultures(SupportedLanguges.Languages);
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {

                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });


            builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            

          

            MapsterConfig.RegisterMappings();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
