using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class RoleValidator:AbstractValidator<Roles>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();

        }
    }
}
