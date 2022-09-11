using FluentValidation;
using Postieri.Controllers;
using Postieri.Models;

namespace Postieri.Validators
{ 
    public static class RuleBuilderValidationExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(" {PropertyName} is Required ")
                .MinimumLength(minimumLength).WithMessage("{PropertyName} must be at least 8 characters in length")
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain a minimum of 1 upper case letter!")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain a minimum of 1 lower case letter!")
                .Matches("[0-9]").WithMessage("{PropertyName} must contain a minimum of 1 numeric character!")
                .Matches("[^a-zA-Z0-9]").WithMessage("{PropertyName} must contain a minimum of 1 special character: ~`!@#$%^&*()-_+={}[]|;:\" <>,./? \\ ");
            return options;
        }
        public static bool BeAValidDate(DateTime date)
        {
            DateTime currentYear = DateTime.Now;
            DateTime orderDate = date;
            bool monthDay = orderDate.Month < 1  || orderDate.Day > 31  ;

            if (orderDate <= currentYear && orderDate.Year > (currentYear.Year - 120) && monthDay  )
            {
              
                return true;
            }

            return false;
        }
       
           
            
        


    }


}
