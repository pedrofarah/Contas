using Contas.Web.Api.Service.PlanoContasService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Contas.Web.Api.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPlanoContasService, PlanoContasService.PlanoContasService>();

            return services;
        }
    }

}
