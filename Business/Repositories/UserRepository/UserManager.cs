using Business.Abstract;
using Business.Repositories.UserRepository.Contans;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Repositories.UserRepository
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
            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/Img/");
            //string fileName = _fileService.FileSaveToFtp(registerDto.Image);
            //byte[] fileByteArray = _fileService.FileConvertByteArrayToDatabase(registerDto.Image);

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

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == id));
        }

        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(p => p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(UserMessages.WrongCurrentPassword);
            }

            byte[] passwordHash, paswordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out paswordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = paswordSalt;
            _userDal.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);
        }
    }
}
