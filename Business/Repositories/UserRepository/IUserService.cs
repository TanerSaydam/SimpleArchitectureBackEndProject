using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Repositories.UserRepository
{
    public interface IUserService
    {
        Task Add(RegisterAuthDto authDto);
        Task<IResult> Update(User user);
        Task<IResult> ChangePassword(UserChangePasswordDto userChangePasswordDto);
        Task<IResult> Delete(User user);
        Task<IDataResult<List<User>>> GetList();
        Task<User> GetByEmail(string email);
        Task<List<OperationClaim>> GetUserOperationClaims(int userId);
        Task<IDataResult<User>> GetById(int id);
        Task<User> GetByIdForAuth(int id);
    }
}
