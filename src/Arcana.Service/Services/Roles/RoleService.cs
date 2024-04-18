using Arcana.DataAccess.UnitOfWorks;
using Arcana.Domain.Entities.Users;
using Arcana.Service.Configurations;
using Arcana.Service.Exceptions;
using Arcana.Service.Extensions;
using Arcana.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Arcana.Service.Services.Roles;

public class RoleService(IUnitOfWork unitOfWork) : IRoleService
{
    public async Task<Role> CreateAsync(Role role)
    {
        var existRole = await unitOfWork.Roles.SelectAsync(r => r.Name.Equals(role.Name, StringComparison.OrdinalIgnoreCase));
        if (existRole is not null)
            throw new AlreadyExistException($"Role with this name: {role.Name} already exists");

        role.CreatedByUserId = HttpContextHelper.UserId;
        var createdRole = await unitOfWork.Roles.InsertAsync(role);
        await unitOfWork.SaveAsync();

        return createdRole;
    }

    public async Task<Role> DeleteAsync(long id)
    {
        var role = await unitOfWork.Roles.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Role is not found with this id: {id}");

        var deletedRole = await unitOfWork.Roles.DeleteAsync(role); 
        await unitOfWork.SaveAsync();

        return deletedRole;
    }

    public async Task<IEnumerable<Role>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var roles = unitOfWork.Roles.SelectAll().OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            roles = roles.Where(r => r.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await roles.ToPaginate(@params).ToListAsync();
    }

    public async Task<Role> GetByIdAsync(long id)
    {
        var role = await unitOfWork.Roles.SelectAsync(r => r.Id == id)
           ?? throw new NotFoundException($"Role is not found with this id: {id}");

        return role;
    }

    public async Task<Role> UpdateAsync(long id, Role role)
    {
        var existRole = await unitOfWork.Roles.SelectAsync(r => r.Id == id)
           ?? throw new NotFoundException($"Role is not found with this id: {id}");

        var alreadyExistRole = await unitOfWork.Roles.SelectAsync(r => r.Name.Equals(role.Name, StringComparison.OrdinalIgnoreCase));
        if (alreadyExistRole is not null)
            throw new AlreadyExistException($"Role with this name: {role.Name} already exists");

        role.Id = id;
        role.UpdatedByUserId = HttpContextHelper.UserId;
        var updatedRole = await unitOfWork.Roles.UpdateAsync(role);
        await unitOfWork.SaveAsync();

        return updatedRole;
    }
}
