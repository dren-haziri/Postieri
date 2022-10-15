using FluentValidation;


namespace Postieri.Validators
{
    public class ShelfValidator : AbstractValidator<ShelfDto>
    {
        public ShelfValidator()
        {
            RuleFor(x => x.AvailableSlots).NotEmpty().NotNull().GreaterThanOrEqualTo(0).WithMessage(x=>$"{x.AvailableSlots} shoud be a positive number!");
            RuleFor(x => x.BinLetter).NotEmpty().NotNull().Length(1).Matches("[A-Z]").WithMessage(x=>$"{x.BinLetter} should contains only one capital letter!");
            RuleFor(x => x.WarehouseId).NotEmpty().NotNull().WithMessage(x => $"{x.WarehouseId} can not be empty!"); ;
            RuleFor(x => x.ShelfId).NotEmpty().NotNull().WithMessage(x=>$"{x.ShelfId} can not be empty!");

        }
    }
   
}
