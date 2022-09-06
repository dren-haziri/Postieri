
using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class WarehouseValidator:AbstractValidator<Warehouse>
    {
        public WarehouseValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.NumOfShelves).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.WarehouseId).NotEmpty();
        }
    }
}
