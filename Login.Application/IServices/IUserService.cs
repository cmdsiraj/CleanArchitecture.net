using Login.Application.Dtos;
using Login.Domain.Models;

namespace Login.Application.IServices
{
    public interface IUserService
    {
        TokenDTO? Login(LoginDTO loginDto);
        User? SignUp(SignUpDTO singupDto);
        User? GetUser(string username);
        IEnumerable<User> GetAllUsers();
    }
}
