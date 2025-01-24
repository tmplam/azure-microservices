namespace eCommerce.Core.Entities;

public class ApplicationUser
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}
