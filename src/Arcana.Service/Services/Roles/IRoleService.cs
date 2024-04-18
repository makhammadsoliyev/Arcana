using Arcana.Domain.Entities.Users;
using Arcana.Service.Configurations;

namespace Arcana.Service.Services.Roles;

public interface IRoleService
{
    Task<Role> CreateAsync(Role role);
    Task<Role> UpdateAsync(long id, Role role);
    Task<Role> DeleteAsync(long id);
    Task<Role> GetByIdAsync(long id);
    Task<IEnumerable<Role>> GetAllAsync(PaginationParams @params,
                                        Filter filter,
                                        string search = null);
}
