using Arcana.DataAccess.UnitOfWorks;
using Arcana.Domain.Entities.Users;
using Arcana.Service.Configurations;
using Arcana.Service.Exceptions;
using Arcana.Service.Extensions;
using Arcana.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Arcana.Service.Services.Permissions;

public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
{
    public async Task<Permission> CreateAsync(Permission permission)
    {
        var existPermission = await unitOfWork.Permissions
            .SelectAsync(p => string.Equals(p.Method, permission.Method, StringComparison.OrdinalIgnoreCase) && 
                              string.Equals(p.Controller, permission.Controller, StringComparison.OrdinalIgnoreCase));

        if (existPermission is not null)
            throw new AlreadyExistException("Permission already exists");

        permission.CreatedByUserId = HttpContextHelper.UserId;
        var createdPermission = await unitOfWork.Permissions.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        return createdPermission;
    }

    public async Task<Permission> DeleteAsync(long id)
    {
        var permission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this id: {id}");

        var deletedPermission = await unitOfWork.Permissions.DeleteAsync(permission);
        await unitOfWork.SaveAsync();

        return deletedPermission;
    }

    public async Task<IEnumerable<Permission>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = unitOfWork.Permissions.SelectAll().OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            permissions = permissions.Where(p => p.Method.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                                 p.Controller.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await permissions.ToPaginate(@params).ToListAsync();
    }

    public async Task<Permission> GetByIdAsync(long id)
    {
        var permission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this id: {id}");

        return permission;
    }

    public async Task<Permission> UpdateAsync(long id, Permission permission)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
           ?? throw new NotFoundException($"Permission is not found with this id: {id}");

        var alreadyExistPermission = await unitOfWork.Permissions
            .SelectAsync(p => string.Equals(p.Method, permission.Method, StringComparison.OrdinalIgnoreCase) &&
                              string.Equals(p.Controller, permission.Controller, StringComparison.OrdinalIgnoreCase));
        if (alreadyExistPermission is not null)
            throw new AlreadyExistException("Permission already exists");

        permission.Id = id;
        permission.UpdatedByUserId = HttpContextHelper.UserId;
        var updatedPermission = await unitOfWork.Permissions.UpdateAsync(permission);
        await unitOfWork.SaveAsync();

        return permission;
    }
}
