using Microsoft.Extensions.DependencyInjection;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.BLL.Repositories;

namespace ProjectMVC.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplictionServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
