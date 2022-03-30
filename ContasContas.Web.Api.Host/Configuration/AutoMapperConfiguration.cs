using AutoMapper;
using Contas.Web.Api.Host.MappingConfigurations;

namespace Contas.Web.Api.Host.Configuration
{
    public static class AutoMapperConfiguration
    {

        public static IServiceCollection AddAutoMapperConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

    }
}
