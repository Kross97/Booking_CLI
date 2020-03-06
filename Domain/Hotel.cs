using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class Hotel
    {
        public Guid IdHotel { get; set; }
        public string Name { get; set; }

        public double Rating { get; set; }

        public string Country { get; set; }

        static int count = 0;
        public Hotel()
        {
            this.IdHotel = Guid.NewGuid();
            this.Name = $"Standart Hotel #{count}";
            count++;
        }
        public Hotel(string name): this()
        {
            this.Name = name;
        }
        public Hotel(string name, double rat) : this(name)
        {
            this.Rating = rat;
        }
        public Hotel(string name, double rat, string country) : this(name, rat)
        {
            this.Country = country;
        }

        public string HotelAsString()
        {
            return $"ID: {IdHotel}, Name: {Name}, Country: {Country}, Rating: {Rating}";
        }
    }
}
