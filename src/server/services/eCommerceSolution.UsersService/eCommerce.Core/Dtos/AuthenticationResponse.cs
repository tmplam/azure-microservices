namespace eCommerce.Core.Dtos;

public record AuthenticationResponse(
    Guid UserId,
    string Email,
    string PersonName,
    string Gender,
    string Token,
    bool Success
)
{
    public AuthenticationResponse() : this(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false) { }
}