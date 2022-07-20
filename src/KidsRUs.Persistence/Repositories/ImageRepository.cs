using KidsRUs.Application.Extensions;
using KidsRUs.Application.Helper;
using KidsRUs.Application.Models.ViewModels;
using KidsRUs.Application.Repositories;
using KidsRUs.Application.Services;
using KidsRUs.Domain.Entities;
using KidsRUs.Persistence.Common;
using KidsRUs.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Persistence.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    private readonly KidsRUsContext _context;
    private const string _previewBaseUrl = "/api/v1/mediafiles/preview";
    
    public ImageRepository(KidsRUsContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<ImageVm> GetImageAsync(IUriService uriService, string containerName, int imageId)
    {
        var image = await _context.Images.SingleOrDefaultAsync(_ => _.Id == imageId);

        if (image == null)
        {
            return null;
        }

        return new ImageVm()
        {
            Sku = image.Id.ToHashId(),
            Url = GetUrl(uriService, containerName, image.FileName)
        };
    }
    
    public async Task<List<ImageVm>> GetImagesAsync(IUriService uriService, string containerName, int productId)
    {
        return (await _context.Images.Where(_ => _.ProductId == productId).ToListAsync())
            .Select(_ => new ImageVm() 
            {
                Sku = _.Id.ToHashId(), 
                Url = GetUrl(uriService, containerName, _.FileName)
            }).ToList();
    }
    
    private string GetUrl(IUriService uriService, string containerName, string fileName)
    {
        var actionUrl = $"{_previewBaseUrl}/{containerName.EncodeToBase64()}/{fileName.EncodeToBase64()}";
        return uriService.GetBaseUri(actionUrl).AbsoluteUri;
    }
}