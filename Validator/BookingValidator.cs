
using FluentValidation;
using fluentValidatorExample.Command;

namespace fluentValidatorExample.Validator
{
    public class BookingValidator : AbstractValidator<BookingCommand>
    {

        public BookingValidator()
        {
            RuleFor(model => model.HotelId).NotEmpty();
            RuleFor(x => x.GuestCity).NotEmpty();
            RuleFor(x => x.GuestName).NotEmpty();
            RuleFor(x => x.GuestEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.NumberOfGuest).GreaterThan(0);
        }
    }
}
