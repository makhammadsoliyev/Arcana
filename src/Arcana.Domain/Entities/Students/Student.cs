using Arcana.Domain.Commons;
using Arcana.Domain.Entities.Commons;
using Arcana.Domain.Entities.Users;

namespace Arcana.Domain.Entities.Students;

public class Student : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}
