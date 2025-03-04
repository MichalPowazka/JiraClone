using Core.Models;
using Core.Services.AppConfig;
using Dapper;
using System.Data.SqlClient;

namespace Core.Repostiories.Users;

public class UserRepository(IAppConfigService _appConfigService) : IUserRepository
{


    async Task<int> IUserRepository.CreateUser(User user)
    {
        var sql = @"Insert Into Project (Name,StartDate,EndDate)
                    Output Inserted.Id 
                    Values(@Name, @StartDate, @EndDate)";
        using (var connection = new SqlConnection(_appConfigService.ConnectionString))
        {
            var insertedId = await connection.QuerySingleAsync<int>(sql, user);
            return insertedId;
        }
    }

    Task<User> IUserRepository.GetUser(int id)
    {
        throw new NotImplementedException();
    }

    Task<User> IUserRepository.GetUser(string email)
    {
        throw new NotImplementedException();
    }

    Task<int> IUserRepository.UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
