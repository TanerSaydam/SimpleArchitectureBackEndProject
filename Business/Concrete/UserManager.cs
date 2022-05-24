using Business.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async void Add(RegisterAuthDto registerDto)
        {
            string fileName = _fileService.FileSave(registerDto.Image, "./Content/Img/");

            var user = CreateUser(registerDto, fileName);

            _userDal.Add(user);
        }

        private User CreateUser(RegisterAuthDto registerDto, string fileName)
        {
            byte[] passwordHash, paswordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out passwordHash, out paswordSalt);

            User user = new User();
            user.Id = 0;
            user.Email = registerDto.Email;
            user.Name = registerDto.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = paswordSalt;
            user.ImageUrl = fileName;
            return user;
        }

        public User GetByEmail(string email)
        {
            var result = _userDal.Get(p => p.Email == email);
            return result;
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}
