﻿using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public virtual ApplicationUser User { get; set; } = default!;
    public virtual ApplicationRole Role { get; set; } = default!;
}
