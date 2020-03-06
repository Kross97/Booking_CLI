using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class Reservation
    {

        public string HotelName { get; set; }
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }
    }
}
