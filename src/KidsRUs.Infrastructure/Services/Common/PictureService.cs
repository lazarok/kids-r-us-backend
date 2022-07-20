using KidsRUs.Application.Extensions;
using KidsRUs.Application.Helper;
using KidsRUs.Application.Models.Dtos;
using KidsRUs.Application.Options;
using KidsRUs.Application.Options.Common;
using KidsRUs.Application.Services;
using KidsRUs.Application.Services.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace KidsRUs.Infrastructure.Services.Common;

public abstract class PictureService : IPictureService
{
    private readonly IStorageService _storageService;
    private readonly IUriService _uriService;
    private readonly string _containerName;

    protected PictureService(IConfiguration configuration, IUriService uriService)
    {
        _uriService = uriService;
        _containerName = configuration["ProductServiceOptions:ContainerName"];
        _storageService = new FileSystemStorageService(configuration["ProductServiceOptions:Path"]);
    }

    public virtual async Task<MediaFileDto> GetPictureAsync(string name, CancellationToken cancellationToken = default)
    {
        var bytes = await _storageService.ReadAsync(_containerName, name, cancellationToken);
        return new MediaFileDto()
        {
            FileName = name,
            FileContent = FileHelpers.ToStream(bytes)
        };
    }
    
    public virtual async Task<MediaFileDto> GetPictureAsync(string containerName, string name, CancellationToken cancellationToken = default)
    {
        var exists = await _storageService.ExistsAsync(_containerName, name, cancellationToken);

        if (!exists)
        {
            return null;
        }
        
        var bytes = await _storageService.ReadAsync(containerName, name, cancellationToken);
        return new MediaFileDto()
        {
            FileName = name,
            FileContent = FileHelpers.ToStream(bytes)
        };
    }

    public virtual async Task<bool> HasPictureAsync(string name, CancellationToken cancellationToken = default)
    {
        var exists = await _storageService.ExistsAsync(_containerName, name, cancellationToken);
        return exists;
    }

    public virtual async Task SavePictureAsync(string name, byte[] picture, CancellationToken cancellationToken = default)
    {
        await _storageService.WriteAsync(_containerName, name, picture, cancellationToken);
    }

    public virtual async Task SavePictureAsync(string name, Stream stream, CancellationToken cancellationToken = default)
    {
        await _storageService.WriteAsync(_containerName, name, stream, cancellationToken);
    }

    public void DeletePictureAsync(string name, CancellationToken cancellationToken = default)
    {
        _storageService.DeleteAsync(_containerName, name, cancellationToken);
    }
}