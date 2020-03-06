using System;
using Booking.Repository;
using System.Collections.Generic;
using System.Threading;
using Domain;
using Domain.Models;

namespace Booking.Manager
{
    public class HotelManager
    {

        public HotelRepository repo;

        public List<Hotel> hotels = new List<Hotel>();
        private Thread flow;
        public HotelManager()
        {   
            this.repo = new HotelRepository();
            flow = new Thread(GetHotels);
            flow.Start();
        }

        public HotelManager(HotelRepository repo) :this()
        {
            this.repo = repo;
        }

        public bool AddHotel(string name, double rat, string country)
        {
            if (!String.IsNullOrWhiteSpace(name) || !String.IsNullOrWhiteSpace(country) || rat != 0)
            {
                Hotel newHotel = new Hotel(name, rat, country);
                repo.AddHotel(newHotel);
                Thread newFlow = new Thread(GetHotels);
                newFlow.Start();
                return true;
            } else
            {
                Console.WriteLine("Не все данные отеля указаны!");
                return false;
            }
        }

        public void RemoveHotel(Guid id)
        {
            Hotel hotel = hotels.Find(h => h.IdHotel == id);
            hotels.Remove(hotel);
            repo.UpdatingListHotels(hotels);
        }

        public void UpdateHotel(Guid id, HotelParams param)
        {
          int currentHotelIndex = hotels.FindIndex(hotel => hotel.IdHotel == id);
            if(String.IsNullOrWhiteSpace(param.Name) || String.IsNullOrWhiteSpace(param.Country) || param.Rating == 0)
            {
                Console.WriteLine("Не все поля заполнены! Для изменения отеля заполните все поля!");
                return;
            }
            hotels[currentHotelIndex].Name = param.Name;
            hotels[currentHotelIndex].Rating = param.Rating;
            hotels[currentHotelIndex].Country = param.Country;
            repo.UpdatingListHotels(hotels);
        }

        public void CreateHotelsLog()
        {
            repo.CreateHotelsLog();
        }

        private void GetHotels()
        {
            hotels = repo.GetAllHotels();
        }

        public void GetAllHotels()
        {
            flow.Start();
        }
    }
}
