using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Microsoft.EntityFrameworkCore;
using PRN231_Group.Assigment.API.Repo.Implement;
using PRN231_Group.Assigment.API.Repo.Interface;

namespace Assigment2.PRN231.BookStoreAPI.Controlers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<Assigment2Prn231Context>(options => options.UseSqlServer(GetConnectionString()));
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:BookStoreDB"];

            return strConn;
        }
       

    }
}
