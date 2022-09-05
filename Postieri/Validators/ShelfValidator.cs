using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class ShelfValidator : AbstractValidator<Shelf>
    {
        public ShelfValidator()
        {
              RuleFor(x => x.MaxProducts
            ).NotEmpty().NotNull().WithMessage("Number");
        }
    }
}
