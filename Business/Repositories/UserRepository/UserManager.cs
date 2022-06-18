using Business.Abstract;
using Business.Aspects.Secured;
using Business.Repositories.UserRepository.Contans;
using Business.Repositories.UserRepository.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
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

        [RemoveCacheAspect("IUserService.Get")]
        public async Task Add(RegisterAuthDto registerDto)
        {
            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/Img/");
            //string fileName = _fileService.FileSaveToFtp(registerDto.Image);
            //byte[] fileByteArray = _fileService.FileConvertByteArrayToDatabase(registerDto.Image);

            var user = CreateUser(registerDto, fileName);

            await _userDal.Add(user);
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

        public async Task<User> GetByEmail(string email)
        {
            var result = await _userDal.Get(p => p.Email == email);
            return result;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(UserValidator))]
        [RemoveCacheAspect("IUserService.Get")]
        public async Task<IResult> Update(User user)
        {
            await _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("IUserService.Get")]
        public async Task<IResult> Delete(User user)
        {
            await _userDal.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        [SecuredAspect()]
        [CacheAspect(60)]
        [PerformanceAspect(3)]
        public async Task<IDataResult<List<User>>> GetList()
        {
            return new SuccessDataResult<List<User>>(await _userDal.GetAll());
        }

        public async Task<IDataResult<User>> GetById(int id)
        {
            return new SuccessDataResult<User>(await _userDal.Get(p => p.Id == id));
        }

        public async Task<User> GetByIdForAuth(int id)
        {
            return await _userDal.Get(p => p.Id == id);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public async Task<IResult> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = await _userDal.Get(p => p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(UserMessages.WrongCurrentPassword);
            }

            byte[] passwordHash, paswordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out paswordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = paswordSalt;
            await _userDal.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);
        }

        public async Task<List<OperationClaim>> GetUserOperationClaims(int userId)
        {
            return await _userDal.GetUserOperatinonClaims(userId);
        }
    }
}
