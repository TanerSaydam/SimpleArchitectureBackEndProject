using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.EmailParameterRepository.Validation.FluentValidation
{
    internal class EPValidator : AbstractValidator<EmailParameter>
    {
        public EPValidator()
        {
            RuleFor(p => p.Smtp).NotEmpty().WithMessage("SMTP adresi boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre adresi boş olamaz");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port adresi boş olamaz");
        }
    }
}
