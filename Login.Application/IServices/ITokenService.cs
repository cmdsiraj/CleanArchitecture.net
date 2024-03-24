using Login.Application.Dtos;
using Login.Domain.Models;

namespace Login.Application.IServices
{
    public interface ITokenService
    {
         TokenDTO GenerateJwtToken(User user);

        TokenDTO GenerateLoginToken(string email, DateTime timestamp);
    }
}
