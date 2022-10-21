using FluentValidation;
using fluentValidatorExample.Command;
using fluentValidatorExample.Validator;
using Microsoft.AspNetCore.Mvc;

namespace fluentValidatorExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FluentValidatorController : ControllerBase
    {
        
        private IValidator<HotelCommand> _hotelValidator;
        private IValidator<BookingCommand> _bookingValidator;
        private readonly ILogger<WeatherForecastController> _logger;

        public FluentValidatorController(ILogger<WeatherForecastController> logger,
             IValidator<HotelCommand> hotelValidator
,
             IValidator<BookingCommand> bookingValidator
                    
            )
        {
            _logger = logger;
            _hotelValidator = hotelValidator;
            _bookingValidator = bookingValidator;
           
        }

        [HttpPost]
        [Route("validate-hotel")]
        public async Task<IActionResult> ValidateHotel(HotelCommand command)
        {
            var validationResult = await _hotelValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return Ok(validationResult);

            }

            return Ok();
        }
        [HttpPost]
        [Route("validate-booking")]
        public async Task<IActionResult> ValidateBooking(BookingCommand command)
        {
            var validationResult = await _bookingValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return Ok(validationResult);

            }

            return Ok();
        }
    }
}