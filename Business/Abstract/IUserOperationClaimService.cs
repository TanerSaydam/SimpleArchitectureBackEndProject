using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        void Add(UserOperationClaim userOperationClaim);
    }
}
