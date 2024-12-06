using SAL;
using SAL.EmployeeService;
using DAL.Repository;
using DAL.Models;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DemoDbContext to be injected wherever needed (with SQL Server connection)
            services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register AutoMapper with the MappingProfile
            services.AddAutoMapper(typeof(MappingProfile));  // Register MappingProfile for AutoMapper

            // Register the repositories, services, etc.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));  // Register generic repository
            services.AddScoped<IEmployeeService, EmployeeService>();  // Register EmployeeService
        }
    }
}
