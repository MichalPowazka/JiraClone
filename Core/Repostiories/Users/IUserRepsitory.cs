using Core.Models;

namespace Core.Repostiories.Users
{
    public interface IUserRepository
    {

        Task<long> CreateUser(User user);
        Task<long> UpdateUser(User user);
        Task<User> GetUser(long id);
        Task<User> GetUser(string email);

        Task<string> LoginUser(string email, string password);



    }
}
