using KidsRUs.Application.Services;
using KidsRUs.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KidsRUs.Infrastructure;

public static class Startup
{
    public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenService, TokenService>();           
        services.AddTransient<IProductPictureService, ProductPictureService>();
        
        services.AddHttpContextAccessor();
        services.AddSingleton<IUriService>(provider => {
            var accesor = provider.GetRequiredService<IHttpContextAccessor>();
            var request = accesor.HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            return new UriService(absoluteUri);
        });
    }
}