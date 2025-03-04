using Core.Models;

namespace Core.Repostiories.Users
{
    public interface IUserRepository
    {

        Task<int> CreateUser(User user);
        Task<int> UpdateUser(User user);
        Task<User> GetUser(int id);
        Task<User> GetUser(string email);


    }
}
