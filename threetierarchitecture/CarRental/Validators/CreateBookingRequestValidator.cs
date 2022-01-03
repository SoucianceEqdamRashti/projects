using BookingApi.Dtos.Requests;
using BookingApi.DataAccess.Models;
using FluentValidation;

namespace BookingApi.Validators
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequestDto>
    {
        private const string PersonNumberFormat = "YYYYMMDD-DDDD";
        public CreateBookingRequestValidator()
        {
            RuleFor(p => p.PersonNumber)
                .NotEmpty()
                .Must(IsValidPersonNumber)
                .WithMessage("{PropertyName} must be in the format" + " " + PersonNumberFormat);
            
        }
        
        //Extremely crude validation - for demo purposes
        //Otherwise have to use regex or Personnummer nuget package
        //Just validating that we use single format and that the input is of that format and contains only numbers and dash.
        //For example 19811103-4444 is valid but 19811103-abc is not valid
        private bool IsValidPersonNumber(string personNumber)
        {
            if (personNumber.Length != 13)
                return false;
            else
            {
                string lastFour = personNumber.Substring(personNumber.Length - 4);
                Console.WriteLine(lastFour);
                if (Int32.TryParse(lastFour, out _) == false)
                    return false;
                string birthday = personNumber.Substring(0, 7);
                if (Int32.TryParse(birthday, out _) == false)                
                    return false;
                if (personNumber.IndexOf("-") != 8)
                    return false;                
                return true;
            }
          
        }
    }
}
