using System;
using System.Collections.Generic;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Source { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int RoleId { get; set; }

    public int? PubId { get; set; }

    public DateTime? HireDate { get; set; }

    public virtual Publisher? Pub { get; set; }

    public virtual Role Role { get; set; } = null!;
}
