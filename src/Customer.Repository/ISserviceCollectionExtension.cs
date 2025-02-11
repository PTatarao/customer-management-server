using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CustomerManagement.Repository;
using CustomerManagement.Repository.Models;
namespace Customer.Repository
{
    public static class ISserviceCollectionExtension
    {
        public static IServiceCollection configureservice(this IServiceCollection service, IConfiguration Configuration)
        {
            //access the appsetting json file in your WebApplication File

            service.AddDbContext<ModelContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            return service;
        }
    }
}