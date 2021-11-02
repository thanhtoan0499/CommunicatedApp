using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Extensions
{
    public static class CorsExtention 
    {
        public static IServiceCollection AddCorsCustom(this IServiceCollection services)
        {
            services.AddCors(c => {
                c.AddPolicy("TCAPolicy", builder => {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
