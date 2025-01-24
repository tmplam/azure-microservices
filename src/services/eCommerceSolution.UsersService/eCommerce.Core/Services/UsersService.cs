using eCommerce.Core.Dtos;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UsersService(
    IUsersRepository _usersRepository) 
    : IUsersService
{
    public async Task<AuthenticationResponse?> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _usersRepository.GetUserByEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

        if (user is null)
        {
            return null;
        }

        return new AuthenticationResponse(
            user.UserId,
            user.Email,
            user.PersonName!,
            user.Gender!,
            Token: "token",
            Success: true
        );
    }

    public async Task<AuthenticationResponse?> RegisterAsync(RegisterRequest registerRequest)
    {
        var user = new ApplicationUser
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            PersonName = registerRequest.PersonName,
            Gender = registerRequest.Gender.ToString(),
        };

        var registeredUser = await _usersRepository.AddUserAsync(user);

        if (registeredUser is null)
        {
            return null;
        }

        return new AuthenticationResponse(
            registeredUser.UserId,
            registeredUser.Email,
            registeredUser.PersonName!,
            registeredUser.Gender!,
            Token: "token",
            Success: true
        );
    }
}
