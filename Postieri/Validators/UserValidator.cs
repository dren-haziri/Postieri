using FluentValidation;

namespace Postieri.Validators
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().NotNull().MinimumLength(2).MaximumLength(15);
            RuleFor(user => user.Email).EmailAddress().NotEmpty().NotNull();
            RuleFor(user => user.PhoneNumber).NotEmpty().NotNull();
            RuleFor(user => user.CompanyName).MinimumLength(2).NotEmpty().NotNull();
            RuleFor(user => user.RoleName).NotEmpty().NotNull();
        }
    }
}
