using Arcana.DataAccess.UnitOfWorks;
using Arcana.Domain.Entities.Commons;
using Arcana.Service.Configurations;
using Arcana.Service.Exceptions;
using Arcana.Service.Helpers;
using Microsoft.AspNetCore.Http;

namespace Arcana.Service.Services.Assets;

public class AssetService(IUnitOfWork unitOfWork) : IAssetService
{
    public async Task<Asset> DeleteAsync(long id)
    {
        var asset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException($"Asset is not found with this id={id}");

        var deletedAsset = await unitOfWork.Assets.DeleteAsync(asset);
        await unitOfWork.SaveAsync();

        return deletedAsset;
    }

    public async Task<Asset> GetByIdAsync(long id)
    {
        var asset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException($"Asset is not found with this id={id}");

        return asset;
    }

    public async Task<Asset> UploadAsync(IFormFile file, FileType type)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, type.ToString());
        Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, file.FileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        await stream.FlushAsync();
        stream.Close();

        var asset = new Asset()
        {
            Path = fullPath,
            Name = file.Name,
            CreatedByUserId = 1
        };

        var createdAsset = await unitOfWork.Assets.InsertAsync(asset);
        await unitOfWork.SaveAsync();

        return createdAsset;
    }
}
