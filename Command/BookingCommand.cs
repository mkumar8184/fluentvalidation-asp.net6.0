using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluentValidatorExample.Command
{
    public class BookingCommand
    {
        public string HotelId { get; set; }
        public string? GuestName { get; set; }
       
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumberOfGuest { get; set; }     
        public int NumberOfAdult { get; set; }
        public int NumberOfChild { get; set; }
        public IEnumerable<GuestDetails> GuestDetails { get; set; }
    }
    public class GuestDetails
    {
        public string GuestMobile { get; set; }
        public string? GuestEmail { get; set; }
        public string GuestName { get; set; }
        public int Age { get; set; }
        public string GuestType { get; set; }
       public bool IsMainGuest { get; set; }
    }
}
