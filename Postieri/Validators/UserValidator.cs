using FluentValidation;
using System.Text.RegularExpressions;

namespace Postieri.Validators
{
    public class UserValidatorRegister : AbstractValidator<RegisterDto>
    {
        public UserValidatorRegister()
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

    public class EmailDtoValidator : AbstractValidator<EmailDto>
    {
        public EmailDtoValidator()
        {
            RuleFor(x => x.Body).NotEmpty().NotNull();
            RuleFor(x => x.From).NotEmpty().NotNull();
            RuleFor(x =>  x.Subject).NotEmpty().NotNull().MinimumLength(10);
        }
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.ConfirmPassword).Matches(x => x.Password);
            RuleFor(x => x.PasswordResetToken).NotEmpty().NotNull();
        }
    }

    public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordValidation()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}
