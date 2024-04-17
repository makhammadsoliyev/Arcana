using Arcana.Domain.Commons;
using Arcana.Domain.Entities.Commons;
using Arcana.Domain.Entities.Users;

namespace Arcana.Domain.Entities.Instructors;

public class Instructor : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public string Profession { get; set; }
    public string About { get; set; }
}