using Entities.Concrete;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        void Add(OperationClaim operationClaim);
    }
}
