using Goognet.Application.AutoMapper;

namespace Goognet.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
        }
    }
}
