using KidsRUs.Application.Options;
using KidsRUs.Application.Services;
using KidsRUs.Infrastructure.Services.Common;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Infrastructure.Services;

public class ProductPictureService : PictureService, IProductPictureService
{
    public ProductPictureService(IConfiguration configuration, IUriService uriService) : base(configuration, uriService)
    {
        
    }
}