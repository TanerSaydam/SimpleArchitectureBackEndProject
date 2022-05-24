using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        public void Add(OperationClaim operationClaim)
        {
            //Kontroller
            //DA => Kayıt işlemini yap
            _operationClaimDal.Add(operationClaim);
        }
    }
}
