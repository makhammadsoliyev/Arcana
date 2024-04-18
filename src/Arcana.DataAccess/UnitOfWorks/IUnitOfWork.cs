using Arcana.DataAccess.Repositories;
using Arcana.Domain.Entities.Commons;
using Arcana.Domain.Entities.Instructors;
using Arcana.Domain.Entities.Students;
using Arcana.Domain.Entities.Users;

namespace Arcana.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Student> Students { get; }
    IRepository<Instructor> Instructors { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<UserPermission> UserPermissions { get; }

    Task<bool> SaveAsync();
}