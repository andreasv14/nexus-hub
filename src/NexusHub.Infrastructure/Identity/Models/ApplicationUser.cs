﻿using Microsoft.AspNetCore.Identity;

namespace NexusHub.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}