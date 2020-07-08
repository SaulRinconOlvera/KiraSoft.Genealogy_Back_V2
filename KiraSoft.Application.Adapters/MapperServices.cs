using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace KiraSoft.Application.Adapters
{
    public static class MapperServices
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton(GetMapper());
        }

        private static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
