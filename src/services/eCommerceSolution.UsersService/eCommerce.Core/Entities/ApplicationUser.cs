﻿namespace eCommerce.Core.Entities;

public class ApplicationUser
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Passsword { get; set; }
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}
