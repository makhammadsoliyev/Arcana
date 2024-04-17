using Arcana.Domain.Commons;

namespace Arcana.Domain.Entities.Users;

public class Permission : Auditable
{
    public string Method { get; set; }
    public string Controller { get; set; }
}
