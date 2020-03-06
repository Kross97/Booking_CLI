using System;
using Domain;
using Booking.Manager;
using System.Threading;

namespace Booking
{
    class Program
    {
        static void Main(string[] args)
        {
            HotelManager manager = new HotelManager();
            Thread.Sleep(800);
            manager.AddHotel("Ritz", 15.3, "Spain");
            manager.AddHotel("Savoy", 16.4, "Germany");
            manager.CreateHotelsLog();
        }
    }
}
