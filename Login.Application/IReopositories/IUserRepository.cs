using Login.Domain.Models;

namespace Login.Application.IReopositories
{
    public interface IUserRepository
    {
        void AddUser(User user);

        User? GetUserByUserName(string username);
        User? GetUserByEmail(string email);

        IEnumerable<User> GetAllUsers();
    }
}
