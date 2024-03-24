using Login.Application.Dtos;
using Login.Application.IReopositories;
using Login.Application.IServices;
using Login.Domain.Models;

namespace Login.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            this._userRepository = userRepository;
            this._tokenService = tokenService;
        }

        public User? CreateUser(SignUpDTO signupDto)
        {
            if(signupDto== null) throw new ArgumentNullException(nameof(signupDto));
            TokenDTO token = _tokenService.GenerateLoginToken(signupDto.Email, DateTime.UtcNow);
            User user = new()
            {
                UserName = signupDto.Username,
                Email = signupDto.Email,
                Password = signupDto.Password,
                Role = "xyz",
                CreatedAt = DateTime.UtcNow
            };
            UserToken ut = new()
            {
                Email = signupDto.Email,
                Token = token.Token,
            };
            _userRepository.AddUser(user);
            _userRepository.AddUserToken(ut);
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = _userRepository.GetAllUsers();
            return users;
        }

        public User? GetUser(string username)
        {
            User? user = _userRepository.GetUserByUserName(username);
            return user;
        }

        public TokenDTO? Login(LoginDTO loginDto)
        {
            if(loginDto == null)
            {
                return null;
            }
            string email = loginDto.Email;
            string password = loginDto.Password;
            User? user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            if(user.Password != password)
            {
                return null;
            }
            TokenDTO token = _tokenService.GenerateJwtToken(user);
            return token;
        }

        public User? SignUp(SignUpDTO singupDto)
        {
            if(singupDto == null)
            {
                return null;
            }
            User user = new()
            {
                UserName = singupDto.Username,
                Email = singupDto.Email,
                Password = singupDto.Password,
            };
            _userRepository.AddUser(user);
            return user;
        }
    }
}
