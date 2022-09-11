using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class ShelfValidator : AbstractValidator<Shelf>
    {
        public ShelfValidator(Data.DataContext database)
        {
            RuleFor(x => x.MaxProducts).NotEmpty().NotNull().GreaterThanOrEqualTo(0).WithMessage(" {PropertyName} shoud be a positive number!");
            RuleFor(x => x.BinLetter.ToString() ).NotEmpty().NotNull().Length(1).Matches("[A-Z]").WithMessage(" {PropertyName} should contains only one capital letter!");
            RuleFor(x => x.WarehouseId).NotEmpty().NotNull();

            RuleFor(x => x.WarehouseId)
                .Must(warehouse => database.Warehouse.Any(w => w.WarehouseId == warehouse))
                .WithMessage("Warehouse with this id does not exist!");
           
        }
    }
   
}
