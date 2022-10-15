using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.ToEmail).NotEmpty().NotNull();
            RuleFor(x => x.Subject).NotEmpty().NotNull();
            RuleFor(x => x.Body).NotEmpty().NotNull();
            RuleFor(x => x.Attachments).NotEmpty().NotNull();
        }
    }
}
