using eCommerce.Core.Dtos;

namespace eCommerce.Core.ServiceContracts;

public interface IUsersService
{
    Task<AuthenticationResponse?> LoginAsync(LoginRequest loginRequest);
    Task<AuthenticationResponse?> RegisterAsync(RegisterRequest registerRequest);
}
