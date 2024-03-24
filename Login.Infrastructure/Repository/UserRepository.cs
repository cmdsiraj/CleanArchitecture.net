using Login.Application.IReopositories;
using Login.Domain.Models;
using Login.Infrastructure.Data;

namespace Login.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;

        public UserRepository(DataContext db)
        {
            this._db = db;
        }

        public void AddUserToken(UserToken userToken)
        {
            if(userToken.Email == null) throw new ArgumentNullException("email");
            if(userToken.Token == null) throw new ArgumentNullException("token");
            _db.UserTokens.Add(userToken);
            _db.SaveChanges();
        }

        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = _db.Users.ToList();
            return users;
        }

        public User? GetUserByEmail(string email)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public User? GetUserByUserName(string username)
        {
            User? user = _db.Users.FirstOrDefault(u => u.UserName == username);
            return user;
        }
    }
}
