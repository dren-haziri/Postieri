
using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class WarehouseValidator:AbstractValidator<Warehouse>
    {
        public WarehouseValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3,20);
            RuleFor(x => x.Area).NotEmpty().GreaterThanOrEqualTo(0).WithMessage(" {PropertyName} shoud be a positive number!"); 
            RuleFor(x => x.NumOfShelves).NotEmpty().GreaterThanOrEqualTo(0).WithMessage(" {PropertyName} shoud be a positive number!"); 
            RuleFor(x => x.Location).NotEmpty().MinimumLength(4);
      
        }
    }
}
