using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

internal class UsersRepository(
    DapperDbContext _dbContext)
    : IUsersRepository
{
    public async Task<ApplicationUser?> AddUserAsync(ApplicationUser user)
    {
        user.UserId = Guid.NewGuid();

        string query = """
            INSERT INTO public."Users"("UserId", "Email", "PersonName", "Gender", "Password")
            VALUES (@UserId, @Email, @PersonName, @Gender, @Password)
            """;

        int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

        if (rowCountAffected > 0)
            return user;
        return null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        string query = """
            SELECT *
            FROM public."Users"
            WHERE "Email" = @Email AND "Password" = @Password
            """;

        var parameters = new { Email = email, Password = password };

        var user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user;
    }
}
