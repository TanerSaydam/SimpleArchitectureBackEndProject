using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(RegisterAuthDto authDto);
        List<User> GetList();
        User GetByEmail(string email);
    }
}
