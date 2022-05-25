using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Repositories.UserOperationClaimRepository
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim);
        IResult Delete(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetList();
        IDataResult<UserOperationClaim> GetById(int id);
    }
}
