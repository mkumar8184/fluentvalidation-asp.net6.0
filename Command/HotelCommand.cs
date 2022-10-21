

using Domain.Entity;

namespace fluentValidatorExample.Command
{
    public class HotelCommand
    {
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public decimal RatePerNight { get; set; }
        public IEnumerable<HotelFeaturesCommand> Features { get; set; }        
    }
}
