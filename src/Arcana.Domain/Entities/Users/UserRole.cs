﻿using Arcana.Domain.Commons;

namespace Arcana.Domain.Entities.Users;

public class Role : Auditable
{
    public string Name { get; set; }
}
