using Core.Models;
using Core.Services.AppConfig;
using Dapper;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;



using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;


namespace Core.Repostiories.Users;

public class UserRepository(IAppConfigService appConfigService) : IUserRepository
{
    private readonly IAppConfigService _appConfigService = appConfigService;

    async Task<long> IUserRepository.CreateUser(User user)
    {
        var sql = @"Insert Into [JiraClone].[dbo].[User] (UserName,FirstName,LastName,Email,PasswordHash,PaswordChangeRequired,IsActive)
                    Output Inserted.Id 
                    Values(@Email ,@FirstName,@LastName,@Email,@PaswordHash,@PaswordChangeRequired, @IsActive)";
        using SqlConnection connection = new SqlConnection(_appConfigService.ConnectionString);
        var insertedId = await connection.QuerySingleAsync<int>(sql, user);
        return insertedId;
    }

    async Task<User> IUserRepository.GetUser(long id)
    {
        var sql = @"SELECT u.* FROM [JiraClone].[dbo].[User]  u WHERE u.Id = @Id;";

        using var connection = new SqlConnection(_appConfigService.ConnectionString);
        connection.Open();
        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id }) ?? throw new NotImplementedException();
        connection.Close();
        return user;

    }

    async Task<User> IUserRepository.GetUser(string email)
    {
        var sql = @"SELECT u.* FROM User u WHERE u.Email = @Email;";

        using SqlConnection connection = new(_appConfigService.ConnectionString);
        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { email = email }) ?? throw new NotImplementedException();
        return user;
    }
    //dlaczego tutaj nie moge uzyc porownaj sobie z project repository
    async Task<long> IUserRepository.UpdateUser(Models.User user)
    {
        var sql = @"Update User SET FirstName = @FirstName, LastName = @LastName, Email = @Email Where Id = @Id";

        using (SqlConnection connection = new(_appConfigService.ConnectionString))
        {
            await connection.ExecuteAsync(sql, user);
        }
        return user.Id;
    }


    public async Task<string> LoginUser(string email, string password)
    {
        var sql = "SELECT * FROM User WHERE Email = @Email";
        using SqlConnection connection = new (_appConfigService.ConnectionString);
        var user = await connection.QuerySingleOrDefaultAsync<Models.User>(sql, new { Email = email });

        if (user == null || !VerifyPassword(password, user.PaswordHash))
            return "zesrało sie"; // Niepoprawne dane logowania

        return GenerateJwtToken(user);
    }

    private bool VerifyPassword(string enteredPassword, string storedHash)
    {
        var bytes = Encoding.UTF8.GetBytes(enteredPassword);
        byte[] hash = SHA256.HashData(bytes);
        var inputPassword = Convert.ToBase64String(hash);

        return inputPassword.Equals(storedHash);
    }

    private string GenerateJwtToken(Models.User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TwojSuperTajnyKluczJWT"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        var token = new JwtSecurityToken(
            issuer: "TwojaAplikacja",
            audience: "TwojaAplikacja",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
