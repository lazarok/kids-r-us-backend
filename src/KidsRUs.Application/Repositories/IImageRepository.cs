using KidsRUs.Application.Services;

namespace KidsRUs.Application.Repositories;

public interface IImageRepository : IRepository<Image>
{
    Task<List<ImageVm>> GetImagesAsync(IUriService uriService, string containerName, int productId);
    Task<ImageVm> GetImageAsync(IUriService uriService, string containerName, int imageId);
}