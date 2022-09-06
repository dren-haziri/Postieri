using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class ShelfValidator : AbstractValidator<Shelf>
    {
        public ShelfValidator()
        {
            RuleFor(x => x.MaxProducts).NotEmpty().NotNull();
            RuleFor(x => x.BinLetter).NotEmpty().NotNull();
            RuleFor(x => x.WarehouseId).NotEmpty().NotNull();
        }
    }
}
