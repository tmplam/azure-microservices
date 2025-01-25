using AutoMapper;
using eCommerce.Core.Dtos;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UsersService(
    IUsersRepository _usersRepository,
    IMapper _mapper)
    : IUsersService
{
    public async Task<AuthenticationResponse?> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _usersRepository.GetUserByEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

        if (user is null)
            return null;

        return _mapper.Map<AuthenticationResponse>(user) with
        {
            Token = "token",
            Success = true,
        };
    }

    public async Task<AuthenticationResponse?> RegisterAsync(RegisterRequest registerRequest)
    {
        var user = _mapper.Map<ApplicationUser>(registerRequest);

        var registeredUser = await _usersRepository.AddUserAsync(user);

        if (registeredUser is null)
        {
            return null;
        }

        return _mapper.Map<AuthenticationResponse>(registeredUser) with
        {
            Token = "token",
            Success = true,
        };
    }
}
