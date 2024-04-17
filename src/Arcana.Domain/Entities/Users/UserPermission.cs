using Arcana.Domain.Commons;

namespace Arcana.Domain.Entities.Users;

public class UserPermission : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}
