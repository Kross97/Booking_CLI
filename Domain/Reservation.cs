using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   [Serializable]
   public class Reservation
    {
        public Guid IdReservation { get; set; }
        public Client ClientCurrent { get; set; }
        public Hotel HotelCurrent { get; set; }
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public Reservation()
        {
            IdReservation = Guid.NewGuid();
        }

        public string ReservAsString()
        {
            return $"ID: {IdReservation} Client: {ClientCurrent.Name} Hotel: {HotelCurrent.Name} {HotelCurrent.Country} Date In: {CheckInDate} Date out: {CheckOutDate}";
        }
    }
}
