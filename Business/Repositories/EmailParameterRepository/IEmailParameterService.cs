using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Repositories.EmailParameterRepository
{
    public interface IEmailParameterService
    {
        IResult Add(EmailParameter emailParameter);
        IResult Update(EmailParameter emailParameter);
        IResult Delete(EmailParameter emailParameter);
        IDataResult<List<EmailParameter>> GetList();
        IDataResult<EmailParameter> GetById(int id);
        IResult SendEmail(EmailParameter emailParameter, string body, string subject, string emails);
    }
}
