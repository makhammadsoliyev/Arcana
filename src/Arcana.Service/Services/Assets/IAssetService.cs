using Arcana.Domain.Entities.Commons;
using Arcana.Service.Configurations;
using Microsoft.AspNetCore.Http;

namespace Arcana.Service.Services.Assets;

public interface IAssetService
{
    Task<Asset> UploadAsync(IFormFile file, FileType type);
    Task<Asset> DeleteAsync(long id);
    Task<Asset> GetByIdAsync(long id);
}