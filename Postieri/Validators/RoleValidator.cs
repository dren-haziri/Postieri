using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class RoleValidator:AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().NotNull().Length(3,25);
            RuleFor(x => x.Description).NotEmpty().NotNull();

        }
    }
}
