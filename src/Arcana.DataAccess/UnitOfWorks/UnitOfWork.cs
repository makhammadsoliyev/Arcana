using Arcana.DataAccess.Context;
using Arcana.DataAccess.Repositories;
using Arcana.Domain.Entities.Commons;
using Arcana.Domain.Entities.Instructors;
using Arcana.Domain.Entities.Students;
using Arcana.Domain.Entities.Users;

namespace Arcana.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ArcanaDbContext _context;

    public IRepository<User> Users { get; }

    public IRepository<Asset> Assets { get; }

    public IRepository<Student> Students { get; }

    public IRepository<Role> Roles { get; }

    public IRepository<Instructor> Instructors { get; }

    public IRepository<Permission> Permissions { get; }

    public IRepository<UserPermission> UserPermissions { get; }

    public UnitOfWork(ArcanaDbContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Assets = new Repository<Asset>(_context);
        Students = new Repository<Student>(_context);
        UserRoles = new Repository<Role>(_context);
        Instructors = new Repository<Instructor>(_context);
        Permissions = new Repository<Permission>(_context);
        UserPermissions = new Repository<UserPermission>(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveAsync()
        => await _context.SaveChangesAsync() > 0;
}
