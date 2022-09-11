using FluentValidation;
using System.Text.RegularExpressions;

namespace Postieri.Validators
{
    public class UserValidator:AbstractValidator<RegisterDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().NotNull().MinimumLength(2).WithMessage("{PropertyName} must be a string with a minimum length of 2.").MaximumLength(15).WithMessage("{PropertyName} must be a string with a maximum length of 15.");
            RuleFor(user => user.Password).Password();
            RuleFor(user => user.ConfirmPassword).Matches(user => user.Password).WithMessage("The passwords do not match!");
            RuleFor(user => user.Email).EmailAddress().NotEmpty().NotNull();
            RuleFor(user => user.PhoneNumber).NotEmpty().NotNull();
            RuleFor(user => user.CompanyName).MinimumLength(2).WithMessage("{PropertyName} must have 2 or more characters ").NotEmpty().NotNull();
            RuleFor(user => user.RoleName).NotEmpty().NotNull().Length(3,25);
        }
    
       
    }

    public class UserValidatorLogin : AbstractValidator<LoginDto>
    {
        public UserValidatorLogin()
        {
            RuleFor(user => user.Password).NotEmpty().NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(user => user.Email).EmailAddress().NotEmpty().NotNull();
          
        }
    }
}
