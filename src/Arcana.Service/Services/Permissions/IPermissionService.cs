using Arcana.Domain.Entities.Users;
using Arcana.Service.Configurations;

namespace Arcana.Service.Services.Permissions;

public interface IPermissionService
{
    Task<Permission> CreateAsync(Permission permission);
    Task<Permission> UpdateAsync(long id, Permission permission);
    Task<Permission> DeleteAsync(long id);
    Task<Permission> GetByIdAsync(long id);
    Task<IEnumerable<Permission>> GetAllAsync(PaginationParams @params,
                                              Filter filter,
                                              string search = null);
}