using Daulatpride.Domain.Interface;
using Daulatpride.Infrastructure.DapperContext;
using Daulatpride.Infrastructure.Repository;
namespace Daulatpride.Extension
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperDbContext>();
            services.AddScoped<I_Login, LoginRepository>();
            services.AddScoped<I_Report, ReportRepository>();
      
            return services;
        }
    }
}
