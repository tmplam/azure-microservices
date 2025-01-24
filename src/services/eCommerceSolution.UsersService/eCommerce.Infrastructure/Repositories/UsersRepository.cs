using eCommerce.Core.Dtos;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;

namespace eCommerce.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{
    public async Task<ApplicationUser?> AddUserAsync(ApplicationUser user)
    {
        user.UserId = Guid.NewGuid();

        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return new ApplicationUser
        {
            UserId = Guid.NewGuid(),
            Email = email,
            Passsword = password,
            PersonName = "John Doe",
            Gender = GenderOptions.Male.ToString()
        };
    }
}
