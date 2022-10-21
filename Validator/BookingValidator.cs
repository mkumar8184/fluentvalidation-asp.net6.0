
using Domain.Entity;
using FluentValidation;
using fluentValidatorExample.Command;

namespace fluentValidatorExample.Validator
{
    public class BookingValidator : AbstractValidator<BookingCommand>
    {
        readonly DbTestContext _context;
     /// <summary>
            /// for data validation from db
            /// </summary>
          
            public BookingValidator(DbTestContext context)
            {
                _context = context;
                RuleFor(x => x.HotelId).NotEmpty()
                    //with DB check pass, object and properties
                    .Must((HotelId) => CheckHotel(HotelId)).WithMessage("Invalid hotel");
                //call validator from another validator
                RuleForEach(x => x.GuestDetails).SetValidator(new GuestValidator());
                //check null
                RuleFor(x => x.StartDate).NotNull();
                RuleFor(x => x.EndDate).NotNull();
                //compare start and end data
                RuleFor(x => x).Must(x => x.EndDate == default(DateTime)
                           || x.StartDate == default(DateTime) || 
                         x.EndDate > x.StartDate)
                        .WithMessage("EndDate must greater than StartDate");

            //check total number of entered guest and guest number
            RuleFor(x => x).Must(x => x.NumberOfGuest==x.GuestDetails.Count())
                       .WithMessage("EndDate must greater than StartDate");

            //check number of child and child details

            When(x => x.NumberOfChild>0, () => {
                RuleFor(x => x)
                .Must(x =>
                    x.GuestDetails.Count(x => x.GuestType == "child") 
                    == x.NumberOfChild).WithMessage("missing child details");

            });

        }

        //check hotel from db 
        //you can pass only whole command and do you validation
        private bool CheckHotel(string hotelId)
            {
                var hotel = _context.Hotels.FirstOrDefault(x => x.HotelId.ToString().ToUpper() == hotelId.ToUpper());
                if (hotel == null) return false;
                return true;
            }
          
     
    }
    public class GuestValidator : AbstractValidator<GuestDetails>
    {
        public GuestValidator()
        {
            RuleFor(x => x.GuestName).NotEmpty();
           
            // when guest type is main then phone number is required.
            RuleFor(x => x.GuestMobile).NotEmpty()
                .When(x=>x.IsMainGuest==true).WithMessage("Mobile must entered for main guest");
            
            RuleFor(x => x.GuestEmail).NotEmpty()
               .When(x => x.IsMainGuest == true).WithMessage("email must entered for main guest");

            RuleFor(x => x.GuestEmail).NotEmpty().EmailAddress();
            //less and equal
            RuleFor(x => x.Age).LessThanOrEqualTo(0);
            //check another field based on another
            When(x => x.GuestType=="Child", () => {
                RuleFor(x => x.Age).LessThan(12).WithMessage("child age must less than 12");
            });


        }

    }
}
