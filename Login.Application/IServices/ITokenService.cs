using Login.Application.Dtos;
using Login.Domain.Models;

namespace Login.Application.IServices
{
    public interface ITokenService
    {
         TokenDTO GenerateToken(User user);
    }
}
