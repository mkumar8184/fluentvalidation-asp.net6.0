
using Domain.Entity;
using FluentValidation;
using fluentValidatorExample.Command;


namespace fluentValidatorExample.Validator
{
    public class HotelValidator : AbstractValidator<HotelCommand>
    {
        /// <summary>
        /// for data validation from db
        /// </summary>
        readonly DbTestContext _context;
        public HotelValidator(DbTestContext context)
        {
            _context = context;
            RuleFor(x => x.HotelName).NotEmpty().MinimumLength(10).WithMessage("max length 10")
                //with DB check pass, object and properties
                .Must((x,HotelName)=>CheckHotelName(x, HotelName)).WithMessage("Hotel name already exists for selected city");            
           //max length
            RuleFor(x => x.Address).MaximumLength(100).
                When(x => !string.IsNullOrEmpty(x.Address)).WithMessage("Address max length 100");
            //minumum and max length for option field
            RuleFor(x => x.Description).Length(10, 500)
           .When(x => !string.IsNullOrEmpty(x.Description));
           
            //with regex
            RuleFor(x => x.City).NotEmpty()
                .Matches("^[.a-zA-Z ]+$").WithMessage("City should not contains any numeric value");
            //greter than
            RuleFor(x => x.RatePerNight).GreaterThan(0).WithMessage("Hotel per night rate should entered");
            //child rule for each item in list
            RuleForEach(x => x.Features).ChildRules(feature =>
            {
                feature.RuleFor(x => x.FeaturesName).NotEmpty().WithMessage("Feture name should not be empty");
            });
            //checkDuplicate from list
            RuleFor(x => x.Features).Must((x,features) => !CheckDuplicate(x))
            .WithMessage("features are duplicates");
          
           


        }
        //check hotel from db 
        //you can pass only whole command and do you validation
        private bool CheckHotelName(HotelCommand command, string hotelName)
        {
            var hotel = _context.Hotels.FirstOrDefault(x => x.HotelName.ToUpper() == hotelName.ToUpper()
                                                        && command.City==x.City
                                                        );
            if (hotel == null) return true;
            return false;
        }
        public bool CheckDuplicate(HotelCommand command )
        {
            return command.Features.GroupBy(s => s.FeaturesName)
                    .Where(g => g.Count() > 1).Count() > 0;
           
        }


    }
 
}
