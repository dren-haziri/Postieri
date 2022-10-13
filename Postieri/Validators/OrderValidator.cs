using FluentValidation;
using Postieri.Models;

namespace Postieri.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {

        public OrderValidator()
        {
            RuleFor(x => x.Address).NotEmpty().NotNull().Length(3, 30);
            RuleFor(x => x.OrderId).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThanOrEqualTo(0).WithMessage(" {PropertyName} shoud be a positive number!");
            RuleFor(x => x.CompanyId).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.CourierId).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotEmpty().NotNull();
            RuleFor(x => x.Date).NotNull().NotEmpty().Must(RuleBuilderValidationExtensions.BeAValidDate).WithMessage("Invalid {PropertyName}");
           // RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.ManagerId).NotEmpty().NotNull();
            RuleFor(x => x.Sign).NotEmpty().NotNull();
            RuleFor(x => x.OrderedOn).Must(RuleBuilderValidationExtensions.BeAValidDate).WithMessage("Invalid {PropertyName}").NotEmpty().NotNull();
        }


    }
}
