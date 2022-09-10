using FluentValidation;

namespace Postieri.Validators
{ 
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage("Required")
                .MinimumLength(minimumLength).WithMessage("Length")
                .Matches("[A-Z]").WithMessage("Uppercase msg")
                .Matches("[a-z]").WithMessage("Lowercase msg")
                .Matches("[0-9]").WithMessage("Digit msg")
                .Matches("[^a-zA-Z0-9]").WithMessage("Sepcial char msg");
            return options;
        }
        public static bool BeAValidDate(DateTime date)
        {
            DateTime currentYear = DateTime.Now;
            DateTime orderDate = date;

            if (orderDate <= currentYear && orderDate.Year > (currentYear.Year - 120) && orderDate.Month<=12 )
            {
              
                return true;
            }

            return false;
        }

    }


}
