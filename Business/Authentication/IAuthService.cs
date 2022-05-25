using Core.Utilities.Result.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(RegisterAuthDto registerDto);
        IDataResult<Token> Login(LoginAuthDto loginDto);
    }
}
